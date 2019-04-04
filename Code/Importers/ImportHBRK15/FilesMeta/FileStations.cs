using FERHRI.Amur.Importer.HBRK15.ServiceReferenceAmur;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERHRI.Amur.Importer
{
    /// <summary>
    /// Файл *.csv с мета-данными станций (от Гончукова, 2016).
    /// В шапке указаны названия полей.
    /// </summary>
    public class FileStations
    {
        static internal object[/*fileSubType;Dictionary<Station, double[lat,lon]>*/] Parse(string filePath, string fileSubType)//, FileType fileType)
        {
            System.IO.StreamReader sr = null;

            try
            {
                //Dictionary<string, string> fileLogBefore = fileType.ReadLog();

                #region IS FILE NEEDED?
                //// SCAN DIR FILES

                //FileInfo fileInfo = new FileInfo(filePath);
                //Console.Write(fileInfo.Name + "\t");

                //// GET FILE SUBTYPE

                //string fileSubType = null;
                //foreach (var item in fileType.FileSubTypes)
                //{
                //    if (item.FileNames.FirstOrDefault(x => x == fileInfo.Name) != null)
                //    {
                //        fileSubType = item.Type;
                //        break;
                //    }
                //}
                //if (fileSubType == null)
                //{
                //    Console.WriteLine(" Пропущен...");
                //    return null;
                //}

                //string dateImport;
                //if (fileLogBefore.TryGetValue(fileInfo.Name, out dateImport))
                //{
                //    if (dateImport == fileInfo.LastWriteTime.ToString())
                //    {
                //        fileType.Log.Add(fileInfo.Name, dateImport);
                //        Console.WriteLine(" Импортирован ранее...");
                //        return null;
                //    }
                //}
                //if (!string.IsNullOrEmpty(fileType.SubDir2Keep))
                //    FilesConfig.KeepFile(fileInfo, fileType.SubDir2Keep);

                #endregion IS FILE NEEDED?

                // SWITCH FILE SUBTYPE

                Dictionary<Station, double[]> ret = new Dictionary<Station, double[]>();

                switch (fileSubType)
                {
                    case "META_CSV_STATIONS25_GONCHUKOV2016": ret = Parse_META_CSV_GONCHUKOV2016(filePath); break;
                    default:
                        throw new Exception("Неизвестный подтип файла для иморта: " + fileSubType);
                }
                //fileType.Log.Add(fileInfo.Name, fileInfo.LastWriteTime.ToString());

                return new object[] { fileSubType, ret };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
            finally
            {
                if (sr != null) sr.Close();
            }
        }

        private static Dictionary<Station, double[/*lat,lon*/]> Parse_META_CSV_GONCHUKOV2016(string filePath)
        {
            char splitter = ';';
            string line = "EMPTY";
            string[] file_columns = new string[] { "station_code", "station_type_id", "station_name", "lat", "lon", "utc_offset" };

            Dictionary<Station, double[]> ret = new Dictionary<Station, double[]>();
            System.IO.StreamReader sr = new System.IO.StreamReader(filePath, Encoding.GetEncoding("windows-1251"));

            try
            {
                // READ FILE CAPTION

                line = sr.ReadLine();
                string[] columns = line.Split(splitter);
                for (int i = 0; i < file_columns.Length; i++)
                {
                    if (file_columns[i] != columns[i])
                        throw new Exception("File columns error.");
                }
                string[] cells;

                // READ FILE ROWS

                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine().Trim();
                    if (string.IsNullOrEmpty(line)) continue;
                    cells = line.Split(splitter);

                    ret.Add(
                        new Station() { Code = cells[0].Trim(), TypeId = int.Parse(cells[1]), Name = cells[2] },
                        cells[3] != "NULL" ? new double[] { double.Parse(cells[3]), double.Parse(cells[4]) } : null);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(line + "\n\n" + ex.ToString());
                return null;
            }
            finally
            {
                if (sr != null) sr.Close();
            }
            return ret;
        }
    }
}
