using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using System.IO;
using FERHRI.Amur.Importer.HBRK15.ServiceReferenceAmur;
using FERHRI.Common;

namespace FERHRI.Amur.Importer
{
    /// <summary>
    /// Программа импорта данных из текстовых файлов в БД "Амур".
    /// 
    /// Реализован в разное время импорт данных:
    /// - прогнозов модели WRF-HBRK15 (Вербицкая, Романский);
    /// - прогнозов модели WRF-VVO (Крохин);
    /// - прогнозов модели уровня моря (Любицкий);
    /// - прогнозов волнения модели WWIII-VVO (Вражкин)
    /// - и др.
    /// 
    /// Код парсеров см. в файлах вида File***.cs
    /// 
    /// 
    /// </summary>
    class Program
    {
        static System.Threading.Timer timer;
        static bool isBusy = false;

        static string EVENT_LOG_NAME = "FERHRI";
        static string EVENT_LOG_SOURCE = "Amur.Import.HBRK15";

        /// <summary>
        /// Клиент сервиса записи в БД.
        /// </summary>
        internal static ServiceClient _svc;
        internal static long _hSvc;
        static User _user;

        static void Main(string[] args)
        {
            Console.Title = EVENT_LOG_NAME + "." + EVENT_LOG_SOURCE;

            // Initialize Windows EventLog
            EventLog = GetEventLog(EVENT_LOG_NAME, EVENT_LOG_SOURCE);
            if (EventLog != null)
            {
                // Initialize Amur service
                _user = Common.ADbNpgsql.GetUser(ConfigurationManager.AppSettings["USER"]);
                //(global::FERHRI.Amur.Importer.HBRK15.Properties.Settings.Default.AmurConnectionString);
                _svc = new ServiceClient();
                _svc.Open();
                _hSvc = _svc.Open(_user.Name, _user.Password);
                if (_hSvc < 1)
                    throw new Exception("Не удалось открыть сессию доступа к сервису данных БД Амур: " + _hSvc);

                _svc.Close();

                // Get importer settings
                int periodInMinutes = Convert.ToInt32(ConfigurationManager.AppSettings["TIMER_CALLBACK_PERIOD_MINUTES"]);

                FilesConfig filesConfig = FilesConfig.Parse();

                timer = new System.Threading.Timer(TimerCallback, filesConfig, 0, periodInMinutes * 60 * 1000);
            }
            Console.ReadKey();
        }

        /// <summary>
        /// Импорт файлов.
        /// </summary>
        /// <param name="stateInfo">Путь к директорию импорта (string).</param>
        static void TimerCallback(Object stateInfo)
        {
            if (isBusy) return; else isBusy = true;

            DateTime dateTimerS = DateTime.UtcNow;
            Console.WriteLine("Start " + dateTimerS);

            _svc = new ServiceClient();
            _svc.Open();
            _hSvc = _svc.Open(_user.Name, _user.Password);
            if (_hSvc < 1)
                throw new Exception(string.Format("Не удалось открыть сессию доступа к сервису данных БД Амур: {0} {1} {2}", _hSvc, _user.Name, _user.Password));

            try
            {
                foreach (var fileType in ((FilesConfig)stateInfo).FileTypes)
                {
                    if (!Directory.Exists(fileType.Dir))
                    {
                        Console.WriteLine("Директорий импорта {0}, указанный в файле конфигурации, не существует. Пропущен.", fileType.Dir);
                        continue;
                    }

                    List<LogFile.Row> logRows = fileType.LogFile.ReadLog(LogFile.ActionType.Import);

                    foreach (var filePath in System.IO.Directory.GetFiles(fileType.Dir))
                    {

                        FileInfo fileInfo = new FileInfo(filePath);

                        // Is log-file? 
                        if (fileInfo.Name == fileType.LogFile.Name)
                            continue;

                        Console.Write(fileInfo.Name + "\t");

                        // Is imported?
                        LogFile.Row row = logRows.FirstOrDefault(x =>
                            x.ActionType == LogFile.ActionType.Import
                            && x.FilePath == fileInfo.FullName
                            && x.FileLastWriteTime.Ticks == (new DateTime(fileInfo.LastWriteTime.Year, fileInfo.LastWriteTime.Month, fileInfo.LastWriteTime.Day, fileInfo.LastWriteTime.Hour, fileInfo.LastWriteTime.Minute, fileInfo.LastWriteTime.Second)).Ticks
                        );

                        if (row != null)
                        {
                            Console.WriteLine(" Импортирован ранее...");
                            continue;
                        }

                        // Is FILE SUBTYPE needed?

                        string fileSubType = null;
                        foreach (var item in fileType.FileSubTypes)
                        {
                            if (item.FileNames.FirstOrDefault(x => x == fileInfo.Name) != null)
                            {
                                fileSubType = item.Type;
                                break;
                            }
                        }
                        if (fileSubType == null)
                        {
                            Console.WriteLine(" Пропущен...");
                            continue;
                        }

                        object[] ret = null;
                        switch (fileType.Type)
                        {
                            case "WRF_HBR_K15": ret = FileHBRK15.Parse(filePath, fileSubType); break;
                            case "WW_VVO_2016": ret = FileWWIIIVVO.Parse(filePath, fileSubType); break;
                            case "WRF_VVO_OS15": ret = FileWRF_VVO.Parse(filePath, fileSubType); break;
                            case "CSV_WITH_CTLID": ret = FileNH9.Parse(filePath, fileSubType); break;
                            case "META_CSV": ret = FileStations.Parse(filePath, fileSubType); break;
                            default:
                                throw new Exception("Неизвестный тип файла " + fileType.Type);
                        }

                        #region DB WRITE

                        if (ret != null)
                        {
                            switch (fileType.DataType)
                            {
                                case "forecasts":
                                    {
                                        List<DataForecast> data = (List<DataForecast>)ret[1];
                                        DateTime dateIni = (DateTime)ret[0];

                                        if (data == null)
                                        {
                                            Console.WriteLine(". Нет данных в файле.");
                                        }
                                        else if (data.Count > 0)
                                        {
                                            Console.Write(string.Format(". Запись {0} значений...", data.Count));

                                            int[] catalogIds = data.Select(x => x.CatalogId).Distinct().ToArray();
                                            Dictionary<int, bool> existsFcs = _svc.ExistsDataForecasts(_hSvc, catalogIds, dateIni);

                                            int count = 0;
                                            foreach (int catalogId in catalogIds)
                                            {
                                                if (existsFcs[catalogId] != true)
                                                {
                                                    DataForecast[] data4Insert = data.Where(x => x.CatalogId == catalogId).ToArray();
                                                    _svc.SaveDataForecastList(_hSvc, data4Insert);
                                                    count += data4Insert.Count();
                                                }
                                            }
                                            Console.WriteLine(string.Format(" Записано {0} значений.", count));
                                        }
                                    }
                                    break;
                                case "observations":
                                    {
                                        List<DataValue> data = (List<DataValue>)ret[0];
                                        foreach (DataValue dv in data)
                                        {
                                            _svc.SaveDataValue(_hSvc, dv);
                                        }
                                        Console.WriteLine(string.Format("... Записано {0} значений.", data.Count));
                                    }
                                    break;
                                case "meta.stations":
                                    {
                                        int i = InsertUpdateFileStations(_svc, _hSvc, (string)ret[0], (Dictionary<Station, double[/*lat,lon*/]>)ret[1]);
                                        Console.WriteLine(string.Format("... Записано {0} значений.", i));
                                    }
                                    break;
                                default:
                                    throw new Exception("Неизвестный тип данных файла: forecasts or observations.");
                            }
                        }
                        #endregion DB WRITE

                        fileType.KeepFile(fileInfo);

                        row = new LogFile.Row { ActionType = LogFile.ActionType.Import, ActionTime = DateTime.Now, FilePath = fileInfo.FullName, FileLastWriteTime = fileInfo.LastWriteTime };
                        fileType.LogFile.WriteLog(new List<LogFile.Row>() { row });
                        Console.WriteLine(" {0}", row.ActionType, fileInfo.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n" + ex.ToString());
            }
            finally
            {
                isBusy = false;
            }
            Console.WriteLine("End " + DateTime.UtcNow + ", " + (dateTimerS - DateTime.UtcNow).TotalMinutes + " min elapsed");
        }

        private static int InsertUpdateFileStations(ServiceClient _svc, long _hSvc, string fileSubType, Dictionary<Station, double[]> stations)
        {
            int iCount = 0;
            switch (fileSubType)
            {
                case "META_CSV_STATIONS25_GONCHUKOV2016":
                    // Атрибуты Владивостока
                    List<EntityAttrValue> eavSiteVVO = _svc.GetSitesAttrValues(_hSvc, new int[] { 332 }, null, null).ToList();
                    DateTime attrDateS = new DateTime(1900, 01, 01);
                    int siteTypeId = 1;

                    int idStation = -1;
                    int idSite = -1;
                    foreach (KeyValuePair<Station, double[]> station in stations)
                    {
                        Station stationDB = _svc.GetStationByIndex(_hSvc, station.Key.Code);
                        // Есть уже станция в БД?
                        if (stationDB == null)
                        {
                            idStation = _svc.SaveStation(_hSvc, station.Key);
                            idSite = _svc.SaveSite(_hSvc, new Site() { StationId = idStation, SiteTypeId = siteTypeId });
                        }
                        else
                        {
                            // Обновить имя станции
                            stationDB.Name = station.Key.Name;
                            _svc.UpdateStation(_hSvc, stationDB);

                            // Обновить атрибуты пункта
                            Site[] sites = _svc.GetSitesByStation(_hSvc, stationDB.Id, siteTypeId);
                            if (sites.Length != 1)
                                throw new Exception("(sites.Count != 1)");
                            idSite = sites[0].Id;
                        }

                        // Атрибуты пункта: Удалить и вставить lat, lon пункта
                        eavSiteVVO.RemoveAll(x => x.AttrTypeId == 1000 || x.AttrTypeId == 1001);
                        if (station.Value != null)
                        {
                            eavSiteVVO.Add(new EntityAttrValue() { AttrTypeId = 1000, DateS = attrDateS, Value = station.Value[0].ToString() });
                            eavSiteVVO.Add(new EntityAttrValue() { AttrTypeId = 1001, DateS = attrDateS, Value = station.Value[1].ToString() });
                        }
                        // Записать атрибуты пункта
                        for (int i = 0; i < eavSiteVVO.Count; i++)
                        {
                            eavSiteVVO[i].EntityId = idSite;
                            _svc.SaveSiteAttribute(_hSvc, eavSiteVVO[i]);
                        }

                        iCount++;
                        Console.WriteLine(string.Format("\tЗаписан {0} пункт.", iCount));
                    }
                    break;
                default:
                    throw new Exception("Неизвестный подтип файла " + fileSubType);
            }
            return iCount;
        }
        static EventLog GetEventLog(string logName, string source)
        {
            //if (!EventLog.SourceExists(source))
            //{
            try
            {
                EventLog.CreateEventSource(source, logName);
                Console.WriteLine("Создан сервис событий источника " + source + " в журнале " + logName);
                Console.WriteLine("Закройте приложение и перезапустите его для использования источника.");
                return null;
            }
            catch (Exception) { }
            //}
            return new EventLog(logName, ".", source);
        }
        static public EventLog EventLog { get; set; }

        static List<Station> _stations = new List<Station>();
        static List<Site> _sites = new List<Site>();

        internal static Site GetSiteByStationIndex(string stationIndex, int siteTypeId)
        {
            Station station = _stations.FirstOrDefault(x => x.Code == stationIndex);
            if (station == null)
            {
                station = _svc.GetStationByIndex(_hSvc, stationIndex);
                if (station == null) throw new Exception("В БД \"Амур\" отсутствует станция с индексом " + stationIndex);
                _stations.Add(station);
            }

            Site site = _sites.FirstOrDefault(x => x.StationId == station.Id && x.SiteTypeId == siteTypeId);
            if (site == null)
            {
                Site[] sites = _svc.GetSitesByStation(_hSvc, station.Id, siteTypeId);
                if (sites == null || sites.Length != 1) throw new Exception("В БД Амур отсутствует пункт типа " + siteTypeId + " для станции с индексом " + stationIndex);
                site = sites[0];
                _sites.Add(site);
            }
            return site;
        }
    }
}
