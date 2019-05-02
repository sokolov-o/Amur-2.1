using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;

namespace SOV.GISMeteo
{
    public class GISMeteoRepository
    {
        public string ConnectionString { get; set; }
        public Encoding Encoding { get; set; }

        private GISMeteoRepository()
        {
            ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Gismeteo"].ConnectionString;
            try
            {
                this.Encoding = Encoding.GetEncoding(System.Configuration.ConfigurationManager.AppSettings["GismeteoEncoding"]);
            }
            catch
            {
                this.Encoding = Encoding.Default;
            }
        }

        static public GISMeteoRepository Instance { get { return new GISMeteoRepository(); } }

        public byte[] CheckPointBuffer { get; set; }

        public List<Telegram> GetTelegrams(int codeForm, string checkPointPath, List<StationGM> stationList, List<Param> paramList, DateTime? dateS)
        {
            int hDB = GismeteoDLL.MdbOpenR(ConnectionString);
            if (hDB == 0) throw new Exception("HDB = 0");
                        
            GismeteoDLL.MdbSetClear(hDB);
            GismeteoDLL.MdbSetCodeForm(hDB, codeForm);

            // Выбирает все записи с датой наблюдения больше заданной  
            if (dateS.HasValue)
            {
                GismeteoDLL.MdbSetObsStart(hDB, new DateTimeStruct(dateS.Value)); 
            }
            else
            {
                byte[] buffer = new byte[0];
                try
                {
                    buffer = GetLastCheckPoint(checkPointPath);
                }
                catch { }

                if (buffer.Length == 0)
                {
                    GismeteoDLL.MdbSetObsStart(hDB, new DateTimeStruct(DateTime.UtcNow.AddDays(-100))); // Выбирает все записи с датой наблюдения больше заданной          
                }
                else
                {
                    GismeteoDLL.MdbSeek(hDB, 0, buffer, buffer.Length);
                }
            }
            foreach (var item in paramList)
            {
                GismeteoDLL.MdbAddPname(hDB, item.GisParamId);
            }

            List<Telegram> ret = new List<Telegram>();

            while (GismeteoDLL.MdbNext(hDB) != 0)
            {
                int stationIndex = GismeteoDLL.MdbGetIndex(hDB);

                StationGM station = stationList.FirstOrDefault(s => s.StationIndex == stationIndex);
                if (station == null) continue;

                byte[] byteTelegram = new byte[GismeteoDLL.MdbGetTextSize(hDB)];
                GismeteoDLL.MdbGetText(hDB, byteTelegram, byteTelegram.Length);

                DateTimeStruct observDate = new DateTimeStruct(); // дата наблюдения
                DateTimeStruct recieveDate = new DateTimeStruct(); // дата получения
                GismeteoDLL.MdbGetObsTime(hDB, observDate);
                GismeteoDLL.MdbGetRcvTime(hDB, recieveDate);

                station.Height = GismeteoDLL.MdbGetHeight(hDB);
                LatLon latlon = GismeteoDLL.MdbGetMcoords(hDB);
                station.Lon = (short)latlon.Lon;
                station.Lat = (short)latlon.Lat;

                Telegram dataGM = new Telegram(observDate.ToDateTime(), recieveDate.ToDateTime(),
                    station, this.Encoding.GetString(byteTelegram));

                MDBDATA dRecord = new MDBDATA();
                while (GismeteoDLL.MdbGetData(hDB, dRecord))
                {
                    dataGM.MDBDatas.Add(dRecord);
                    dRecord = new MDBDATA();
                }
                if (dataGM.MDBDatas.Count > 0)
                    ret.Add(dataGM);

            }

            // SAVE CHECKPOINT
            try
            {
                CheckPointBuffer = new byte[GismeteoDLL.MdbGetPosSize(hDB, 0)];
                GismeteoDLL.MdbTell(hDB, 0, CheckPointBuffer, CheckPointBuffer.Length);
                SaveCheckPoint(checkPointPath);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Ошибка при сохранении checkPoint'а: {0}", ex.ToString()));
            }
            GismeteoDLL.MdbClose(hDB);
            return ret;
        }

        public void SaveCheckPoint(string checkPointPath)
        {
            try
            {
                using (FileStream stream = new FileStream(checkPointPath, FileMode.Create, FileAccess.Write, FileShare.Read))
                {
                    stream.Write(CheckPointBuffer, 0, CheckPointBuffer.Length);
                }
            }
            catch { }
        }

        private byte[] GetLastCheckPoint(string path)
        {
            byte[] buffer = new byte[0];

            if (File.Exists(path))
            {
                using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);
                }
            }
            return buffer;
        }
    }
}
