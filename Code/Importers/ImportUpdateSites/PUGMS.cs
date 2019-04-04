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
    /// 
    /// Импорт суточных метео-данных по станциям и постам ПУГМС, 
    /// которые подготовлены Гончуковым Л. В. в 2017.
    /// 
    /// OSokolov@201710: Как аггрегировались данные в пределах суток? А хз. 
    /// 
    /// </summary>
    class PUGMS
    {
        internal enum InputFileType { TaR_201708 = 1, TaRd_201710 = 2 }

        static internal void Run(InputFileType fileType, string dirYearsImport, ServiceAmurWCF srvc)
        {
            List<DataValue> dvs;

            switch (fileType)
            {
                case InputFileType.TaR_201708:
                    dvs = Parse201708(dirYearsImport, srvc);
                    break;
                case InputFileType.TaRd_201710:
                    dvs = Parse201710(dirYearsImport, srvc);
                    break;
                default:
                    throw new NotImplementedException();
            }
            if (dvs != null)
            {
                srvc.Client.SaveDataValueList(srvc.h, dvs.Where(x => !double.IsNaN(x.Value)).ToArray(), null);
                Console.WriteLine("В БД записано {0} значений.", dvs.Count);
            }
        }

        /// <summary>
        /// 
        /// Импорт суточных метео-данных по станциям и постам ПУГМС, 
        /// которые подготовлены Гончуковым Л. В. в 2017.10.
        /// 
        /// Темп и дефицит - расчётные, Осадки - со станции.
        /// 
        /// Sample:
        ///Precipitation, mm
        ///23 365
        ///31878 31884 31931 31935 31938 31939 31942 31981 5761 5085 5092 5094 5105 5122 5128 5132 5135 5148 5151 5160 5167 5171 5166 
        ///
        ///
        ///
        ///20150101    0.60    0.50    0.90    0.50  -99.00    0.20    0.10    0.50  -99.00  -99.00  -99.00  -99.00  -99.00  -99.00  -99.00  -99.00  -99.00  -99.00  -99.00  -99.00  -99.00  -99.00  -99.00
        ///20150102    0.20    0.00    0.00    0.00  -99.00    0.30    0.00    0.00  -99.00  -99.00  -99.00  -99.00  -99.00  -99.00  -99.00  -99.00  -99.00  -99.00  -99.00  -99.00  -99.00  -99.00  -99.00
        ///20150103    0.00    0.00    0.00    0.00  -99.00    0.00    0.00    0.00  -99.00  -99.00  -99.00  -99.00  -99.00  -99.00  -99.00  -99.00  -99.00  -99.00  -99.00  -99.00  -99.00  -99.00  -99.00
        ///20150104    0.00    0.00    0.00    0.00  -99.00    0.00    0.00    0.00  -99.00  -99.00  -99.00  -99.00  -99.00  -99.00  -99.00  -99.00  -99.00  -99.00  -99.00  -99.00  -99.00  -99.00  -99.00
        ///20150105    0.00    0.00    0.00    0.00  -99.00    0.00    0.00    0.00  -99.00  -99.00  -99.00  -99.00  -99.00  -99.00  -99.00  -99.00  -99.00  -99.00  -99.00  -99.00  -99.00  -99.00  -99.00
        /// </summary>
        static private List<DataValue> Parse201710(string dirImport, ServiceAmurWCF srvc)
        {
            List<DataValue> ret = new List<DataValue>();

            // CREATE DIR 4 MOVE IMPORTED
            string dirImported = dirImport + @"\Imported";
            if (!Directory.Exists(dirImported))
                Directory.CreateDirectory(dirImported);

            // SCAN YEARS

            foreach (var fileName in Directory.GetFiles(dirImport))
            {
                Console.WriteLine(fileName);
                StreamReader sr = null;
                try
                {
                    sr = new StreamReader(fileName, Encoding.GetEncoding(1251));

                    object[] o = DataRow201710.ParseHeader(sr);
                    int[] varmeth = (int[])o[0];
                    string[] stationIndeces = (string[])o[1];

                    Catalog[] catalogs = GetCatalogs201710(varmeth[0], varmeth[1], stationIndeces, srvc);

                    // SCAN DATA ROWS

                    while (!sr.EndOfStream)
                    {
                        string row = sr.ReadLine().Trim();
                        if (!string.IsNullOrEmpty(row))
                        {
                            DataRow201710 datas = DataRow201710.ParseRow(row);
                            if (datas != null)
                                ret.AddRange(Convert2DataValue(catalogs, datas));
                        }
                    }
                    // MOVE IMPORTED
                    sr.Close();
                    sr = null;
                    File.Move(fileName, dirImported + "\\" + (new FileInfo(fileName)).Name);

                }
                finally
                {
                    if (sr != null) sr.Close();
                }
            }
            return ret;
        }


        /// <summary>
        /// 
        /// ДАННЫЕ БРАК!!! После импорта остались в БД.
        /// 
        /// Импорт суточных метео-данных по приморским метео-станциям, которые подготовлены 
        /// Гончуковым Л. В. в 2017.08.
        /// 
        /// 1) def и rh в файле косые (Лёня...), не импортируем.
        /// 2) В имени файла - индекс станции: meteo31878.csv
        /// 3) В имени деректория - год. Хотя, в столбце файла год тоже есть. Такие дела...
        ///
        /// Sample meteo31878.csv:
        /// 
        ///dt	t2	precip	def	rh
        ///01.01.2015 0:00	-22.6	0.6	0.2987891	0.7309768
        ///02.01.2015 0:00	-21.2125	0.2	0.3837773	0.7036707
        ///03.01.2015 0:00	-19.1875	0	0.6277878	0.6330199
        ///04.01.2015 0:00	-17.5875	0	0.5116487	0.7038695
        ///05.01.2015 0:00	-11.95	0	0.8599855	0.7407778
        ///
        /// </summary>
        static private List<DataValue> Parse201708(string dirImport, ServiceAmurWCF srvc)
        {
            List<DataValue> ret = new List<DataValue>();

            // CREATE DIR 4 MOVE IMPORTED
            string dirImported = dirImport + @"\Imported";
            if (!Directory.Exists(dirImported))
                Directory.CreateDirectory(dirImported);

            // SCAN YEARS

            foreach (var fileName in Directory.GetFiles(dirImport))
            {
                StreamReader sr = null;
                try
                {
                    sr = new StreamReader(fileName, Encoding.GetEncoding(1251));

                    DataRow201708.CheckHeaderRow(sr.ReadLine());

                    FileInfo fi = new FileInfo(fileName);
                    string fname = fi.Name.Replace(fi.Extension, "");
                    Catalog[] catalogs = GetCatalogs201708(fname.Substring(fname.Length - 5, 5), srvc);

                    // SCAN DATA ROWS

                    for (int iRow = 1; !sr.EndOfStream; iRow++)
                    {
                        DataRow201708 dataRow = DataRow201708.ParseRow(sr.ReadLine());
                        if (dataRow != null)
                            ret.AddRange(Convert2DataValue(catalogs, dataRow));
                    }
                    // MOVE IMPORTED
                    sr.Close();
                    sr = null;
                    File.Move(fileName, dirImported + "\\" + (fname + fi.Extension));

                }
                finally
                {
                    if (sr != null) sr.Close();
                }
            }
            return ret;
        }

        static List<Catalog> _catalogs = new List<Catalog>();
        static Dictionary<Station, Site> _stations = new Dictionary<Station, Site>();

        private static Catalog[/*site*/] GetCatalogs201710(int varId, int methodId, string[] stationIndeces, ServiceAmurWCF srvc)
        {
            List<Catalog> ret = new List<Catalog>();
            AmurServiceReference.Station[] stations = srvc.Client.GetStationsByIndeces(srvc.h, stationIndeces);
            foreach (var station in srvc.Client.GetStationsByIndeces(srvc.h, stationIndeces))
            {
                AmurServiceReference.Site[] sites = srvc.Client.GetSitesByStation(srvc.h, station.Id, (int)FERHRI.Amur.Meta.EnumStationType.MeteoStation);
                Catalog ctl = null;
                if (sites.Length == 1)
                {
                    ctl = new Catalog()
                    {
                        SiteId = sites[0].Id,
                        VariableId = varId,
                        MethodId = methodId,
                        SourceId = _SOURCE_ID,
                        OffsetTypeId = _OFFSETTYPE_ID,
                        OffsetValue = _OFFSET_VALUE
                    };
                    Catalog c;
                    if ((c = srvc.Client.GetCatalog(srvc.h, ctl.SiteId, ctl.VariableId, ctl.OffsetTypeId, ctl.MethodId, ctl.SourceId, ctl.OffsetValue)) == null)
                        ctl = srvc.Client.SaveCatalog(srvc.h, ctl);
                    else ctl = c;
                }
                ret.Add(ctl);
            }
            return ret.ToArray();
        }

        private static Catalog[/*Ta, Precip*/] GetCatalogs201708(string stationIndex, ServiceAmurWCF srvc)
        {
            Site site;
            Station station = _stations.Keys.FirstOrDefault(x => x.Code == stationIndex);
            if (station == null)
            {
                station = srvc.Client.GetStationByIndex(srvc.h, stationIndex);
                if (station == null)
                    throw new Exception(string.Format("В БД Амур отсутствует станция с индексом {0}. Создайте станцию.", stationIndex));
                Site[] sites = srvc.Client.GetSitesByStation(srvc.h, station.Id, (int)FERHRI.Amur.Meta.EnumStationType.MeteoStation);
                if (sites.Length != 1)
                    throw new Exception(string.Format("(site.Count!=1) - {0}", sites.Length));
                site = sites[0];
                _stations.Add(station, site);
            }
            else
                site = _stations[station];

            List<Catalog> ret = _catalogs.FindAll(x => x.SiteId == site.Id);
            if (ret.Count == 0)
            {
                // Ta
                Catalog c = new Catalog()
                {
                    SiteId = site.Id,
                    VariableId = (int)FERHRI.Amur.Meta.EnumVariable.TempAirObsDaylyMean,
                    MethodId = _METHOD_ID,
                    SourceId = _SOURCE_ID,
                    OffsetTypeId = _OFFSETTYPE_ID,
                    OffsetValue = _OFFSET_VALUE
                };
                Catalog cc;

                if ((cc = srvc.Client.GetCatalog(srvc.h, c.SiteId, c.VariableId, c.OffsetTypeId, c.MethodId, c.SourceId, c.OffsetValue)) == null)
                    cc = srvc.Client.SaveCatalog(srvc.h, c);
                ret.Add(cc);

                // Precip
                c = new Catalog()
                {
                    SiteId = site.Id,
                    VariableId = (int)FERHRI.Amur.Meta.EnumVariable.PrecipDay24F,
                    MethodId = _METHOD_ID,
                    SourceId = _SOURCE_ID,
                    OffsetTypeId = _OFFSETTYPE_ID,
                    OffsetValue = _OFFSET_VALUE
                };
                if ((cc = srvc.Client.GetCatalog(srvc.h, c.SiteId, c.VariableId, c.OffsetTypeId, c.MethodId, c.SourceId, c.OffsetValue)) == null)
                    cc = srvc.Client.SaveCatalog(srvc.h, c);
                ret.Add(cc);

                _catalogs.AddRange(ret);
            }
            if (ret.Count != 2)
                throw new Exception("Ошибка алгоритма OSokolov@201709 : (ret.Count != 2)");
            return new Catalog[] {
                ret.First(x => x.VariableId == (int)FERHRI.Amur.Meta.EnumVariable.TempAirObsDaylyMean) ,
                ret.First(x => x.VariableId == (int)FERHRI.Amur.Meta.EnumVariable.PrecipDay24F)
            };
        }

        private static List<DataValue> Convert2DataValue(Catalog[/*Ta, R*/] catalogs, DataRow201708 dataRow)
        {
            List<DataValue> ret = new List<DataValue>();

            if (!double.IsNaN(dataRow.T2))
                ret.Add(new DataValue()
                {
                    CatalogId = catalogs[0].Id,
                    Value = dataRow.T2,
                    DateLOC = dataRow.Date,
                    DateUTC = dataRow.Date,
                    UTCOffset = 0,
                    FlagAQC = (byte)FERHRI.Amur.Meta.EnumFlagAQC.NoAQC
                });
            if (!double.IsNaN(dataRow.Precip))
                ret.Add(new DataValue()
                {
                    CatalogId = catalogs[1].Id,
                    Value = dataRow.Precip,
                    DateLOC = dataRow.Date,
                    DateUTC = dataRow.Date,
                    UTCOffset = 0,
                    FlagAQC = (byte)FERHRI.Amur.Meta.EnumFlagAQC.NoAQC
                });
            return ret;
        }

        private static List<DataValue> Convert2DataValue(Catalog[] catalogs, DataRow201710 data)
        {
            List<DataValue> ret = new List<DataValue>();

            for (int i = 0; i < data.Values.Length; i++)
            {
                if (catalogs[i] != null && !double.IsNaN(data.Values[i]))
                {
                    ret.Add(new DataValue()
                    {
                        Id = -1,
                        CatalogId = catalogs[i].Id,
                        FlagAQC = 0,
                        DateUTC = data.Date,
                        DateLOC = data.Date,
                        UTCOffset = 0,
                        Value = data.Values[i]
                    });
                }
            }

            return ret;
        }

        const int _METHOD_ID = (int)FERHRI.Amur.Meta.EnumMethod.ObservationInSitu;
        const int _SOURCE_ID = 243; // PUGMS
        const int _OFFSETTYPE_ID = (int)FERHRI.Amur.Meta.EnumOffsetType.NoOffset;
        const double _OFFSET_VALUE = 0;

        /// <summary>
        /// Строка суточных данных формата 2017.08.
        /// </summary>
        private class DataRow201708
        {
            public DateTime Date { get; set; }
            public double T2 { get; set; }
            public double Precip { get; set; }

            static string[] _columnNames = new string[] { "dt", "t2", "precip" };

            static internal int VariableCount = 2;

            internal static DataRow201708 ParseRow(string row)
            {
                if (string.IsNullOrEmpty(row))
                    return null;

                string[] s = row.Split(_div);
                return new DataRow201708()
                {
                    Date = DateTime.Parse(s[0]),
                    T2 = double.Parse(s[1].Replace(".", System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator)),
                    Precip = double.Parse(s[2].Replace(".", System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator))
                };
            }
            static char _div = ';';

            internal static void CheckHeaderRow(string row)
            {
                string[] s = row.Split(_div);
                for (int i = 0; i < _columnNames.Length; i++)
                {
                    if (_columnNames[i] != s[i].Trim())
                        throw new Exception("(!_columnNames[i] == s[i].Trim())");
                }
            }
        }

        /// <summary>
        /// Строка суточных данных формата 2017.10.
        /// </summary>
        private class DataRow201710
        {
            public DateTime Date { get; set; }
            public double[] Values { get; set; }

            static int[] _columnsLength = new[] { 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8 };
            static double _codeNoData = -99; // to NaN
            static int _stationsCount = _columnsLength.Length - 1;

            internal static DataRow201710 ParseRow(string row)
            {
                if (!string.IsNullOrEmpty(row))
                {
                    string[] s = FERHRI.Common.StrVia.Parse(row, _columnsLength);
                    DataRow201710 ret = new DataRow201710()
                    {
                        Date = FERHRI.Common.DateTimeProcess.Parse(s[0], "yyyyMMdd"),
                        Values = new double[_stationsCount]
                    };
                    for (int i = 1; i < s.Length; i++)
                    {
                        ret.Values[i - 1] = double.Parse(s[i].Replace(".", System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator));
                        if (ret.Values[i - 1] == _codeNoData)
                            ret.Values[i - 1] = double.NaN;
                    }
                    return ret;
                }
                return null;
            }
            static Dictionary<int[/*VariableId,MethodId*/], string/*VarType name in file*/> _knownVarTypes = new Dictionary<int[], string>()
            {
                { new int[] { 1000,1500 },"Temperature, oC" }, // Расч ср. сут, метод ДВНИГМИ
                { new int[] { 1021, 1500 }, "Deficit, hPa" }, // Расч ср. сут, метод ДВНИГМИ
                { new int[] { 23, 0 },"Precipitation, mm" } // Набл сут, Метод набл
            };

            internal static object[/*int VariableTypeId; string[stationCode1...stationCodeN]*/] ParseHeader(StreamReader sr)
            {
                string row = sr.ReadLine();

                // GET VARIABLE_TYPE ID
                int[] ids = null;
                foreach (var item in _knownVarTypes)
                {
                    if (item.Value == row)
                    {
                        ids = item.Key;
                        break;
                    }
                }
                if (ids == null)
                    throw new Exception("Неизвестный тип переменной в файле.");

                // CHECK STATION COUNT
                char[] div = new char[] { ' ' };

                string[] cells = sr.ReadLine().Split(div);
                if (int.Parse(cells[0]) != _stationsCount)
                    throw new Exception("(stationsCount != stationsCount)");

                // GET STATION INDECES
                string[] stationIndeces = sr.ReadLine().Split(div).Where(x => !string.IsNullOrEmpty(x)).ToArray();
                if (stationIndeces.Length != _stationsCount)
                    throw new Exception("(stationIndeces.Length!=  stationsCount)");

                return new object[] { ids, stationIndeces };
            }
        }

    }
}
