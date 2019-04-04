using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Import.AmurServiceReference;
//using FERHRI.Amur.Meta;

namespace Import
{
    /// <summary>
    /// Импорт метео-данных по Тунгуске, которые подготовлены 
    /// ДВ УГМС в 2017 г. по договору.
    /// </summary>
    class Tungus
    {
        //1.	В период с 1980 по 1984 годы осадки измерялись 4 раза в сутки – в сроки 00,03,12,15 московского времени.
        //2.	С 1985 года – 2 срока измерения осадков (00,12 – по московскому времени, 21,09 – по ВСВ)
        //3.	С 1993 года станции перешли с московского времени на время ВСВ. Сроки до 1993 по московскому времени, с 1993 – по ВСВ.

        static Dictionary<int/*coordnum*/, int/*station index*/> _stationCoordNumXCode = new Dictionary<int, int>()
        {
            {4983461,31632},
            {4993461,31632},
            {4943321,31624},
            {4863381,31725},
            {4923521,31647},
            {5053701,31563},
            {5013361,31548},
            {4843281,31713},
            {4873291,31713},
            {4873301,31713}
        };
        static Dictionary<Site, double/*utc_offset*/> _sites = new Dictionary<Site, double>();
        static List<Station> _stations = new List<Station>();
        /// <summary>
        /// Импорт разных файлов метео и гидро.
        /// </summary>
        /// <param name="dirImport">Директорий с файлами *.csv для импорта. В имени файла должен содержаться тип данных в файле.</param>
        internal static void Run(string dirImport, ServiceClient client, long hSvc)
        {
            // CREATE DIR 4 MOVE IMPORTED
            string dirImported = dirImport + @"\Imported";
            if (!Directory.Exists(dirImported))
                Directory.CreateDirectory(dirImported);

            string[] fileNames = Directory.GetFiles(dirImport);
            foreach (var fileName in fileNames)
            {
                try
                {
                    List<DataValue> dataValues = null;

                    if (fileName.IndexOf("СнегПоле") >= 0) dataValues = ParseMeteoSnow(fileName, client, hSvc);
                    if (fileName.IndexOf("Rh") >= 0) dataValues = ParseMeteoRh(fileName, client, hSvc);

                    // SAVE DATA
                    if (dataValues != null)
                    {
                        //client.SaveDataValueList(hSvc, dataValues.ToArray(), null);

                        // MOVE IMPORTED
                        FileInfo fi = new FileInfo(fileName);
                        File.Move(fileName, dirImported + "\\" + fi.Name);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        const int _METHOD_ID = (int)FERHRI.Amur.Meta.EnumMethod.ObservationInSitu;
        const int _SOURCE_ID = 0; // ДВ УГМС
        static List<Catalog> _catalogs = new List<Catalog>();

        private static List<DataValue> ParseMeteoSnow(string fileName, ServiceClient client, long hSvc)
        {
            Dictionary<string, int[/*col_name;variable_id;offset_type_id;offset_value*/]> _snowField_ColumnXVar = new Dictionary<string, int[]>()
            {
                {string.Empty,                                      null}, // название станции (может быть разным) под которым в столбце координатный номер
                {"Год",                                             null},
                {"месяц",                                           null},
                {"Ср. высота снега на маршруте, см",                new int[]{19,1,0}},
                {"Наибольшая Высота снега на маршруте, см",         new int[]{1112,1,0}},
                {"Наименьшая Высота снега на маршруте, см",         new int[]{1113,1,0}},
                {"Степень покрытия маршрута снегом, в баллах",      new int[]{39,1,0}},
                {"Степень покрытия маршрута лед. Коркой, в баллах", new int[]{1114,1,0}},
                {"характер залегания сн. Покрова (шифр)",           new int[]{1115,1,0}},
                {"Характер (структура) Сн. Покрова (шифр)",         new int[]{1116,1,0}},
                {"Ср. плотность снега,  г/см?",                     new int[]{36,1,0}},
                {"Степень покрытия окрестности станции снегом, баллы",new int[]{39,(int)FERHRI.Amur.Meta.EnumOffsetType.StationNearby,0}},
                {"дата производства снегосъемки",                   null},
                {"Запас воды в слое снега",                         new int[]{37,1,0}}
            };

            StreamReader sr = null;
            char[] spl = new char[] { ';' };
            try
            {
                sr = new StreamReader(fileName, Encoding.GetEncoding(1251));
                List<DataValue> ret = new List<DataValue>();

                // TEST HEADER
                string[] cells = sr.ReadLine().Split(spl);
                for (int i = 0; i < cells.Length; i++)
                {
                    string key = _snowField_ColumnXVar.ElementAt(i).Key;
                    if (!string.IsNullOrEmpty(key) && key != cells[i].Trim())
                        throw new Exception(string.Format("Неизвестный столбец [{0}] с индексом {1} в файле {2}", cells[i], i, fileName));
                }

                // SCAN DATA ROWS
                int iRow = 1;
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    Console.WriteLine("Snow #{0} {1}", iRow++, line);
                    cells = line.Split(spl);
                    if (string.IsNullOrEmpty(cells[12].Trim())) continue;

                    // GET DATE
                    string smonth = cells[2].Trim();
                    int month = smonth == "a" ? 10 : smonth == "b" ? 11 : smonth == "c" ? 12 : int.Parse(smonth);
                    DateTime dateUTC = new DateTime(int.Parse(cells[1]), month, int.Parse(cells[12]));

                    KeyValuePair<Site, double/*utcOffset*/> siteUtcoff = GetSite(cells[0], dateUTC, client, hSvc);

                    // SCAN DATA COLUMNS
                    for (int i = 3; i < cells.Length; i++)
                    {
                        // Skip date column
                        if (i == 12 || string.IsNullOrEmpty(cells[i].Trim())) continue;

                        int[] varoff = _snowField_ColumnXVar.ElementAt(i).Value;

                        ret.Add(new DataValue()
                            {
                                Id = -1,
                                CatalogId = GetCatalog(siteUtcoff.Key.Id, varoff[0], _METHOD_ID, _SOURCE_ID, varoff[1], varoff[2], client, hSvc).Id,
                                DateUTC = dateUTC,
                                DateLOC = dateUTC.AddHours(siteUtcoff.Value),
                                FlagAQC = (int)FERHRI.Amur.Meta.EnumFlagAQC.NoAQC,
                                UTCOffset = (float)siteUtcoff.Value,
                                Value = double.Parse(cells[i])
                            }
                        );
                    }
                }
                return ret;
            }
            finally
            {
                if (sr != null) sr.Close();
            }
        }
        private static List<DataValue> ParseMeteoRh(string fileName, ServiceClient client, long hSvc)
        {
            StreamReader sr = null;
            char[] spl = new char[] { ';' };

            int variableId = (int)FERHRI.Amur.Meta.EnumVariable.Rh;
            int offsetTypeId = (int)FERHRI.Amur.Meta.EnumOffsetType.NoOffset;
            double offsetValue = 0;

            try
            {
                sr = new StreamReader(fileName, Encoding.GetEncoding(1251));
                List<DataValue> ret = new List<DataValue>();

                // TEST HEADER
                string[] cells = sr.ReadLine().Split(spl);
                if (cells.Length != 6)
                    throw new Exception(string.Format("Кол. столбцов файла Rh [{0}] не равно 6.", fileName));

                // SCAN DATA ROWS
                int iRow = 1;
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    Console.WriteLine("Rh #{0} {1}", iRow++, line);
                    cells = line.Split(spl);
                    if (string.IsNullOrEmpty(cells[5].Trim())) continue;

                    // GET DATE & SITE
                    DateTime dateUTC = GetDateTimeUTC(int.Parse(cells[1]), cells[2].Trim(), int.Parse(cells[3]), int.Parse(cells[4]));
                    KeyValuePair<Site, double/*utcOffset*/> siteUtcoff = GetSite(cells[0], dateUTC, client, hSvc);

                    #region GET STATION & SITE & UTCOFFSET

                    //int stationCode = _stationCoordNumXCode[int.Parse(cells[0])];
                    //Station station = _stations.FirstOrDefault(x => x.Code == stationCode.ToString());
                    //if (station == null)
                    //{
                    //    station = client.GetStationByIndex(hSvc, stationCode.ToString());
                    //    _stations.Add(station);
                    //}

                    //Site site = _sites.Keys.FirstOrDefault(x => x.StationId == station.Id);
                    //if (site == null)
                    //{
                    //    Site[] sites = client.GetSitesByStation(hSvc, station.Id, station.TypeId);
                    //    if (sites.Length != 1)
                    //        throw new Exception(string.Format("Кол. пунктов для станции {0} не равно 1.", station));
                    //    site = sites[0];
                    //    EntityAttrValue eav = client.GetSiteAttrValue(hSvc, site.Id, (int)EnumSiteAttrType.UTCOffset, dateUTC);
                    //    if (eav == null)
                    //        throw new Exception(string.Format("Для пункта {0} отсутствует utc_offset на дату {1}.", site, dateUTC));
                    //    _sites.Add(site, double.Parse(eav.Value));
                    //}
                    //double utcOffset = _sites[site];

                    #endregion GET STATION & SITE

                    // NEW DATAVALUE
                    ret.Add(new DataValue()
                        {
                            Id = -1,
                            CatalogId = GetCatalog(siteUtcoff.Key.Id, variableId, _METHOD_ID, _SOURCE_ID, offsetTypeId, offsetValue, client, hSvc).Id,
                            DateUTC = dateUTC,
                            DateLOC = dateUTC.AddHours(siteUtcoff.Value),
                            FlagAQC = (int)FERHRI.Amur.Meta.EnumFlagAQC.NoAQC,
                            UTCOffset = (float)siteUtcoff.Value,
                            Value = double.Parse(cells[5])
                        }
                    );
                }
                return ret;
            }
            finally
            {
                if (sr != null) sr.Close();
            }
        }

        private static List<DataValue> ParseMeteoTa(string fileName, ServiceClient client, long hSvc)
        {
            StreamReader sr = null;
            char[] spl = new char[] { ';' };

            int offsetTypeId = (int)FERHRI.Amur.Meta.EnumOffsetType.NoOffset;
            double offsetValue = 0;
            string fileAbbr = "Ta";

            try
            {
                sr = new StreamReader(fileName, Encoding.GetEncoding(1251));
                List<DataValue> ret = new List<DataValue>();

                // TEST HEADER
                string[] cells = sr.ReadLine().Split(spl);
                if (cells.Length != 8)
                    throw new Exception(string.Format("Кол. столбцов файла {0} [{1}] не равно 8.", fileAbbr, fileName));

                // SCAN DATA ROWS
                int iRow = 1;
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    Console.WriteLine("{0} #{1} {2}", fileAbbr, iRow++, line);
                    cells = line.Split(spl);
                    if (string.IsNullOrEmpty(cells[5].Trim())) continue;

                    // GET DATE & SITE
                    DateTime dateUTC = GetDateTimeUTC(int.Parse(cells[1]), cells[2].Trim(), int.Parse(cells[3]), int.Parse(cells[4]));
                    KeyValuePair<Site, double/*utcOffset*/> siteUtcoff = GetSite(cells[0], dateUTC, client, hSvc);

                    // NEW DATAVALUE
                    int variableId = (int)FERHRI.Amur.Meta.EnumVariable.TempAirObs;
                    ret.Add(new DataValue()
                    {
                        Id = -1,
                        CatalogId = GetCatalog(siteUtcoff.Key.Id, variableId, _METHOD_ID, _SOURCE_ID, offsetTypeId, offsetValue, client, hSvc).Id,
                        DateUTC = dateUTC,
                        DateLOC = dateUTC.AddHours(siteUtcoff.Value),
                        FlagAQC = (int)FERHRI.Amur.Meta.EnumFlagAQC.NoAQC,
                        UTCOffset = (float)siteUtcoff.Value,
                        Value = double.Parse(cells[5])
                    }
                    );
                    throw new NotImplementedException();
                    //variableId = (int)EnumVariable.TempAirMin;
                    //ret.Add(new DataValue()
                    //{
                    //    Id = -1,
                    //    CatalogId = GetCatalog(siteUtcoff.Key.Id, variableId, _METHOD_ID, _SOURCE_ID, offsetTypeId, offsetValue, client, hSvc).Id,
                    //    DateUTC = dateUTC,
                    //    DateLOC = dateUTC.AddHours(siteUtcoff.Value),
                    //    FlagAQC = (int)EnumFlagAQC.NoAQC,
                    //    UTCOffset = (float)siteUtcoff.Value,
                    //    Value = double.Parse(cells[5])
                    //}
                    //);
                }
                return ret;
            }
            finally
            {
                if (sr != null) sr.Close();
            }
        }

        private static Catalog GetCatalog(int siteId, int variableId, int _METHOD_ID, int _SOURCE_ID, int offsetTypeId, double offsetValue,
            ServiceClient client, long hSvc)
        {
            Catalog ctlFind = new Catalog()
            {
                Id = -1,
                SiteId = siteId,
                VariableId = variableId,
                MethodId = _METHOD_ID,
                SourceId = _SOURCE_ID,
                OffsetTypeId = offsetTypeId,
                OffsetValue = offsetValue
            };
            Catalog ret = _catalogs.FirstOrDefault(x =>
                x.SiteId == ctlFind.SiteId
                && x.VariableId == ctlFind.VariableId
                && x.OffsetTypeId == ctlFind.OffsetTypeId
                && x.OffsetValue == ctlFind.OffsetValue
                && x.MethodId == ctlFind.MethodId
                && x.SourceId == ctlFind.SourceId
            );
            if ((object)ret == null)
            {
                ret = client.GetCatalog(hSvc, ctlFind.SiteId, ctlFind.VariableId, ctlFind.OffsetTypeId, ctlFind.MethodId, ctlFind.SourceId, ctlFind.OffsetValue);
                if ((object)ret == null)
                {
                    ret = client.SaveCatalog(hSvc, ctlFind);
                    Console.WriteLine("Catalog insert: {0}", ret);
                }
            }
            _catalogs.Add(ret);
            return ret;
        }
        /// <summary>
        /// 
        /// 1.	В период с 1980 по 1984 годы осадки измерялись 4 раза в сутки – в сроки 00,03,12,15 московского времени.
        /// 2.	С 1985 года – 2 срока измерения осадков (00,12 – по московскому времени, 21,09 – по ВСВ)
        /// 3.	С 1993 года станции перешли с московского времени на время ВСВ. Сроки до 1993 по московскому времени, с 1993 – по ВСВ.
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="hour"></param>
        /// <returns></returns>
        private static DateTime GetDateTimeUTC(int year, string smonth, int day, int hour)
        {
            int month = smonth == "a" ? 10 : smonth == "b" ? 11 : smonth == "c" ? 12 : int.Parse(smonth);

            DateTime ret = new DateTime(year, month, day, hour, 0, 0);
            if (year < 1993)
                ret = ret.AddHours(-3);
            return ret;
        }
        private static KeyValuePair<Site, double> GetSite(string coordNum, DateTime dateUTC, ServiceClient client, long hSvc)
        {
            int stationCode = _stationCoordNumXCode[int.Parse(coordNum)];
            Station station = _stations.FirstOrDefault(x => x.Code == stationCode.ToString());
            if (station == null)
            {
                station = client.GetStationByIndex(hSvc, stationCode.ToString());
                _stations.Add(station);
            }

            Site site = _sites.Keys.FirstOrDefault(x => x.StationId == station.Id);
            if (site == null)
            {
                Site[] sites = client.GetSitesByStation(hSvc, station.Id, station.TypeId);
                if (sites.Length != 1)
                    throw new Exception(string.Format("Кол. пунктов для станции {0} не равно 1.", station));
                site = sites[0];
                EntityAttrValue eav = client.GetSiteAttrValue(hSvc, site.Id, (int)FERHRI.Amur.Meta.EnumSiteAttrType.UTCOffset, dateUTC);
                if (eav == null)
                    throw new Exception(string.Format("Для пункта {0} отсутствует utc_offset на дату {1}.", site, dateUTC));
                _sites.Add(site, double.Parse(eav.Value));
            }
            return new KeyValuePair<Site, double>(site, _sites[site]);
        }
    }
}
