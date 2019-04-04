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
    /// Файлы формата WWIII.VVO: прогнозы волнения до 72 ч.в пунктах с разрешением 6 ч.
    /// Формируются в ДВНИГМИ с апреля 2016 г.(Вражкин).
    /// 
    /// Формат файла:
    /// 
    /// Дата время(ВСВ)
    ///Идетинфикатор ff H Hm Tz Dd Hw Tw Dw Hs Ts Ds
    ///
    ///где 
    ///
    /// Дата время - от данной даты и времени (старт)
    /// Идетинфикатор - идентификатор (имя) точки
    ///
    /// ff  - заблаговременность (часы)
    /// H   - высота смешанного волнения (м)
    /// Hm  - максимальная высота волны (0.1% обеспеченности) (м)
    /// Tz  - частота пика (сек.)
    /// Dd  - направления доминирующего волнения (град.)
    /// Hw  - высота ветровой волны (м)
    /// Tw  - средний период ветровой волны (сек.)
    /// Dw  - направление ветровой волны (град.)
    /// Hs  - высота зыби (м)
    /// Ts  - средний период зыби (сек.)
    /// Ds  - направление зыби (град.)
    /// </summary>
    public class FileWWIIIVVO
    {
        static int _methodId = 105;
        static int _sourceId = 777;
        static int _offsetTypeId = 101;
        static double _offsetValue = 0;
        static Dictionary<string, int> _sites = new Dictionary<string, int>()
            {
                {"Магадан-1", 10018},
                {"Лисянский", 10017},
                {"Нагаево", 10019},
                {"HYSY982",10419},
                {"Test", -1 } // Не импортировать
            };
        /// <summary>
        /// Коды переменных - последовательно, как они идут в файле
        /// </summary>
        /// <returns></returns>
        static int[] GetVariables()
        {
            return new int[]
            {
                1028,// H   - высота смешанного волнения (м)
                1048,// Hm  - максимальная высота волны (0.1% обеспеченности) (м)
                1051,// Tz  - частота пика (сек.)
                1035,// Dd  - направления доминирующего волнения (град.)
                1033,// Hw  - высота ветровой волны (м)
                1026,// Tw  - средний период ветровой волны (сек.)
                1032,// Dw  - направление ветровой волны (град.)
                1027,// Hs  - высота зыби (м)
                1049,// Ts  - средний период зыби (сек.)
                1050// Ds  - направление зыби (град.)
            };
        }
        static internal object[/*dateUTCIni;Dictionary;fileLog*/] Parse(string filePath, string fileSubType)//, FileType fileType)
        {
            System.IO.StreamReader sr = null;

            try
            {

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
                //FilesConfig.KeepFile(fileInfo, "Arj");

                // SWITCH FILE SUBTYPE

                string line;
                string[] cells;
                char splitter = ';';
                DateTime dateIni;
                List<DataForecast> dataForecasts = new List<DataForecast>();

                switch (fileSubType)
                {
                    case "WW_VVO_2016":
                        #region PARSE WW_VVO_2016

                        sr = new System.IO.StreamReader(filePath, Encoding.GetEncoding("windows-1251"));
                        List<string[]> dataLines = new List<string[]>();

                        // READ DATE_INI 20160414;12

                        line = sr.ReadLine();
                        cells = line.Split(splitter);
                        if (!Common.DateTimeProcess.TryParse(cells[0], "yyyyMMdd", out dateIni))
                        {
                            throw new Exception("Не распознана исх. дата прогноза.\n" + line);
                        }
                        dateIni = dateIni.AddHours(int.Parse(cells[1]));

                        // READ FILE DATA
                        while (!sr.EndOfStream)
                        {
                            line = sr.ReadLine().Trim();
                            if (string.IsNullOrEmpty(line))
                                continue;

                            dataLines.Add(line.Split(splitter));
                        }

                        // CONVERT FILE DATA 2 DB DATA FORECAST

                        int[] varsId = GetVariables();
                        Dictionary<string, List<Catalog>> siteCatalogs = new Dictionary<string, List<Catalog>>();

                        foreach (string[] cells_ in dataLines)
                        {
                            if (cells_.Length - 2 != varsId.Length)
                                throw new Exception("(dataLine.Length - 2 != vars.Length)");

                            List<Catalog> catalogs;
                            if (!siteCatalogs.TryGetValue(cells_[0], out catalogs))
                            {
                                catalogs = new List<Catalog>();
                                siteCatalogs.Add(cells_[0], catalogs);
                            }

                            for (int i = 0; i < varsId.Length; i++)
                            {
                                // ICE?
                                if (i > 0 && cells_[2] == "-999.9") break;
                                // Нет ветровой или зыби?
                                if (i > 0 && cells_[2 + i] == "-999.9") continue;

                                // GET DATALINE SITE ID
                                int siteId;
                                if (!_sites.TryGetValue(cells_[0], out siteId))
                                    throw new Exception("Указанный в файле пункт отсутствует в словаре: " + cells_[0]);
                                if (siteId < 0) continue;

                                // GET CATALOG
                                List<Catalog> ctls = catalogs.FindAll(x =>
                                    x.SiteId == siteId
                                    && x.VariableId == varsId[i]
                                    && x.MethodId == _methodId
                                    && x.SourceId == _sourceId
                                    && x.OffsetTypeId == _offsetTypeId
                                    && x.OffsetValue == _offsetValue);
                                Catalog catalog = null;
                                if (ctls == null || ctls.Count == 0)
                                {
                                    Catalog[] ctlss = Program._svc.GetCatalogList(Program._hSvc,
                                        new int[] { siteId },
                                        new int[] { varsId[i] },
                                        new int[] { _methodId },
                                        new int[] { _sourceId },
                                        new int[] { _offsetTypeId },
                                        new double[] { _offsetValue });
                                    if (ctlss.Length == 0)
                                        throw new Exception(string.Format(
                                            "В БД отсутствует запрошенная запись каталога для пункта {0} переменной {1}.", siteId, varsId[i]));
                                    if (ctlss.Length > 1)
                                    {
                                        throw new Exception("ALGORITHMIC ERRORO: (ctlss.Length > 1)");
                                    }
                                    catalog = ctlss[0];
                                    catalogs.Add(catalog);
                                }
                                else if (ctls.Count > 1)
                                {
                                    throw new Exception("ALGORITHMIC ERRORO: (ctls.Count > 1)");
                                }
                                else
                                {
                                    catalog = ctls[0];
                                }

                                // CONVERT
                                dataForecasts.Add(new DataForecast()
                                {
                                    CatalogId = catalog.Id,
                                    LagFcs = int.Parse(cells_[1]),
                                    DateFcs = dateIni.AddHours(int.Parse(cells_[1])),
                                    Value = Common.StrVia.ParseDouble(cells_[i + 2])
                                });
                            }
                        }
                        #endregion PARSE WW_VVO_2016
                        break;
                    default:
                        throw new Exception("Неизвестный подтип файла для иморта: " + fileSubType);
                }
                //fileType.Log.Add(fileInfo.Name, fileInfo.LastWriteTime.ToString());

                return new object[] { dateIni, dataForecasts };
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
        //static object[/*DateTime, List<DataForecast>*/] ParseFilesBUREA(string file, Station[] stations, Site[] sites)
        //{
        //    object[] o = FileHBRK15_BUREA.Parse(file, stations, sites);

        //    DateTime dateIni = (DateTime)o[0];
        //    Dictionary<string/*stationCode;stationName*/, double[/*Ta,Td,R,Rf*/][/*lag hours [1...72]*/]> dataFile
        //        = (Dictionary<string, double[][]>)o[1];

        //    dataFile.Remove(dataFile.Keys.FirstOrDefault(x => x.ToUpper().IndexOf("БЫССА") >= 0));

        //    // CONVERT TO DATAFORECAST
        //    int i = 0;
        //    List<DataForecast> ret = null;
        //    foreach (var kvp in dataFile)
        //    {
        //        ret = Convert2DataForecast(dateIni, GetSiteId(kvp.Key, stations, sites), kvp.Value);
        //        ret = ret.FindAll(x => !double.IsNaN(x.Value));
        //    }
        //    return new object[/*DateTime, List<DataForecast>*/] { dateIni, ret };
        //}
    }
}
