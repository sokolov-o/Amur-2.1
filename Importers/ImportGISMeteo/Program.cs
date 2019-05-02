using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using SOV.Amur.Importer.GISMeteo;
using SOV.Amur.Importer.GISMeteo.AmurService;
using System.Diagnostics;
using SOV.GISMeteo;

namespace SOV.Amur.Importer.GISMeteo
{
    /// <summary>
    /// TODO: !!! перед publish убедиться в App.config: 1) в корректности строки <add key="Setting.xml path" value="SettingsKHB.xml" />2) в ip сервиса !!!
    /// </summary>
    class Program
    {
        static System.Threading.Timer timer;
        static List<Settings> configList = null;
        static bool isBusy = false;
        static SOV.Common.User user = null;

        static readonly string EVENT_LOG_NAME = "SOV";
        static readonly string EVENT_LOG_SOURCE = "Amur.Import.GisMeteo";

        /// <summary>
        /// Клиент сервиса записи в БД.
        /// </summary>
        static ServiceClient svc;
        static long hSvc = 0;

        static void Main(string[] args)
        {
            Console.Title = EVENT_LOG_NAME + "." + EVENT_LOG_SOURCE;

            // INITIALIZE Amur service

            string[] suser = Properties.Settings.Default.User.Split(';');
            user = new SOV.Common.User(suser[0], suser[1]);
            svc = new ServiceClient();
            svc.Open();
            hSvc = svc.Open(user.Name, user.Password);
            if (hSvc < 0)
                throw new Exception("Пользователь " + user.Name + "|" + user.Password + " не зарегистрирован в сервисе. Код возврата " + hSvc);


            try
            {
                // Get importer settings
                configList = Settings.Parse(svc, hSvc,
                    ConfigurationManager.AppSettings["Setting.xml path"],
                    ConfigurationManager.ConnectionStrings["MdbCheckPointDirectory"].ConnectionString);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press any key for exit...");
                Console.ReadKey();
                return;
            }
            finally
            {
                svc.Close();
            }
            svc.Close();

            // INIT TIMER
            int periodInMinutes = Convert.ToInt32(ConfigurationManager.AppSettings["periodInMinutes"]);
            timer = new System.Threading.Timer(TimerCallback, null, 0, periodInMinutes * 60 * 1000);
            
            Console.ReadKey();
        }

        static void TimerCallback(Object stateInfo)
        {
            if (isBusy) return; else isBusy = true;

            DateTime dateTimerS = DateTime.UtcNow;
            Console.WriteLine("Start " + dateTimerS);
            string mess = "";

            try
            {
                svc = new ServiceClient();
                svc.Open();
                hSvc = svc.Open(user.Name, user.Password);

                // SCAN CODE FORMS
                GISMeteoRepository repo = GISMeteoRepository.Instance;
                foreach (Settings config in configList)
                {
                    mess = string.Format("Кодовая форма {0}.", config.CodeFormGis);
                    Console.Write(mess);
                    bool ok = true;

                    // ------ GET GM DATA

                    List<Telegram> telegrams = new List<Telegram>();
                    try
                    {
                        telegrams = repo.GetTelegrams(config.CodeFormGis, config.CheckPointPath, config.StationGMList, config.ParamList, config.DateS);

                        string mess1 = string.Format(" Телеграмм {0}, знач {1}.", telegrams.Count, telegrams.Sum(x => x.MDBDatas.Count));
                        Console.Write(mess1);
                        EventLog.WriteEntry(EVENT_LOG_SOURCE, mess + mess1, EventLogEntryType.Information);
                    }
                    catch (Exception ex)
                    {
                        string mess1 = string.Format(" Ошибка при обращении к БД ГисМетео.\n{0}", ex.Message);
                        Console.WriteLine(mess1);
                        EventLog.WriteEntry(EVENT_LOG_SOURCE, mess + mess1, EventLogEntryType.Error);
                        ok = false;
                    }

                    // GM DATA --> AMUR DB

                    if (ok && telegrams.Count > 0)
                    {
                        Console.Write(" Запись в БД Амур...");

                        for (int i = 0; i < telegrams.Count; i++)
                        {
                            DataValue dataValueTemp;
                            try
                            {
                                long? dataSourceId = null;
                                {
                                    DataSource dataSource = Convert2DataSource(telegrams[i], config);
                                    if (dataSource != null)
                                        dataSourceId = svc.SaveDataSource(hSvc, dataSource);
                                }

                                List<DataValue>[] dataValues = Convert2DataValues(telegrams[i], config);

                                // Обычные
                                if (dataValues[0].Count > 0)
                                {
                                    svc.SaveDataValueList(hSvc, dataValues[0], dataSourceId);
                                }
                                // Следы осадков
                                if (dataValues[1].Count > 0)
                                {
                                    foreach (var dv in dataValues[1])
                                    {
                                        dataValueTemp = dv;

                                        long id = svc.SaveValue(hSvc, dv.CatalogId, dv.DateUTC, dv.DateLOC, 0, dv.FlagAQC, dataSourceId);
                                        if (svc.GetDataPDataValueAQC(hSvc, id).FirstOrDefault(x => x.AQCRoleId == 4) == null)
                                            svc.SaveDataPRole(hSvc, id, 4, true);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                ok = false;
                                string mess1 = string.Format(" Ошибка записи данных телеграммы:\n{0}\n\n{1}", telegrams[i].ToString(), ex.ToString());
                                Console.WriteLine(mess1);
                                EventLog.WriteEntry(EVENT_LOG_SOURCE, mess + mess1, EventLogEntryType.Error);
                            }
                        }
                        string mess2 = string.Format(" {0}", ok ? "Успешно" : "Есть ошибки");
                        Console.Write(mess2);
                        EventLog.WriteEntry(EVENT_LOG_SOURCE, mess + mess2, ok ? EventLogEntryType.Information : EventLogEntryType.Error);
                    }

                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(EVENT_LOG_SOURCE, ex.Message, EventLogEntryType.Error);
                Console.WriteLine(ex.Message);
            }
            finally
            {
                mess = string.Format("End. {1} min elapsed.", DateTime.UtcNow, (DateTime.UtcNow - dateTimerS).TotalMinutes);
                Console.WriteLine(mess);
                EventLog.WriteEntry(EVENT_LOG_SOURCE, mess, EventLogEntryType.Information);

                svc.Close();
                isBusy = false;
            }
        }

        private static DataSource Convert2DataSource(Telegram dataGM, Settings config)
        {
            int codeForm = 0; // Unknown;
            switch (config.CodeFormGis)
            {
                case (int)EnumCodeForm.KH01: codeForm = 1/*Meta.EnumCodeForm.KH01*/; break;
                case (int)EnumCodeForm.KH15: codeForm = 2/*Meta.EnumCodeForm.KH15*/; break;
                case (int)EnumCodeForm.KH24: codeForm = 6/*Meta.EnumCodeForm.KH24*/; break;
                case (int)EnumCodeForm.KH02: codeForm = 6/*Meta.EnumCodeForm.KH24*/; break;
                case (int)EnumCodeForm.KH13: codeForm = 105/*Meta.EnumCodeForm.KH24*/; break;
                default:
                    throw new Exception("switch (config.CodeFormGis) : " + config.CodeFormGis);
            }
            return new DataSource()
            {
                Id = -1,
                SiteId = dataGM.Station.SiteIdAmur,
                CodeFormId = codeForm,
                DateUTC = dataGM.DateObserv,
                DateUTCRecieve = dataGM.DateRecieve,
                DateLOCInsert = DateTime.Now,
                Value = dataGM.TelegramText,
                Hash = dataGM.TelegramText
            };
        }

        /// <summary>
        /// Преобразование данных ГИС-метео в список DataValue с
        /// созданием записей каталога данных (Catalog) при необходимости.
        /// </summary>
        /// <param name="dataGM">Данные ГИС-метео</param>
        /// <param name="config">Конфигурация импорта</param>
        /// <returns></returns>
        private static List<DataValue>[] Convert2DataValues(Telegram dataGM, Settings config)
        {
            List<DataValue>[/*DataValue обычное;DataValue следов осадков*/] ret = new List<DataValue>[2];
            ret[0] = new List<DataValue>();
            ret[1] = new List<DataValue>();

            int methodId = 0; // Наблюдённые
            int offsetTypeId = 0; // Нет смещения
            double offsetValue = 0;

            foreach (MDBDATA dRecord in dataGM.MDBDatas)
            {
                // VARIABLE
                IEnumerable<Param> pl = config.ParamList.Where(p => p.GisParamId == dRecord.paramId);
                if (pl.Any(p => p.GisLevelId.HasValue))
                {
                    pl = pl.Where(p => p.GisLevelId.HasValue && p.GisLevelId == dRecord.ltype);
                }
                // TODO: Посмотреть ещё раз этот блок с Леонидом
                if (pl.Count() != 1)
                    continue;
                Param param = pl.ElementAt(0);

                //DataValue dv = new DataValue(-1,
                //int level = dRecord.lvalue;
                //dv.CollectDate = cd.ToDateTime();
                //dv.VariableId = currentParam.HydroParamId;
                //dv.Value = ((float)(dRecord.values)) * currentParam.Multip;
                //dv.Date = od.ToDateTime();
                //dv.DateUTC = currentStation.HoursOffset.HasValue ? od.ToDateTime().AddHours(-currentStation.HoursOffset.Value) : od.ToDateTime();

                DateTime obsDateLOC = dataGM.DateObserv;
                DateTime obsDateUTC = dataGM.Station.UTCOffset.HasValue
                    ? obsDateLOC.AddHours(-dataGM.Station.UTCOffset.Value)
                    : DateTime.FromBinary(obsDateLOC.ToBinary());
                double value = Math.Round(((double)(dRecord.values)) * param.Multip, param.Decimals);

                switch (config.CodeFormGis)
                {
                    #region EnumCodeForm.KH24
                    case (int)EnumCodeForm.KH24:
                        offsetTypeId = 1; // Маршрут: 0 - поле, 1 - лес
                        offsetValue = dRecord.ltype; // Указатель - лес/поле
                        if (param.GisParamId != 174)
                        {
                            obsDateUTC = DateTime.FromBinary(dataGM.DateObserv.ToBinary());
                        }
                        else // группы 7YYMM,8YYMM,9YYMM,0YYMM
                        {
                            string[] dateFormats = { "dd.MM.yyyy", "d.M.yyyy", "dd.M.yyyy", "d.MM.yyyy",
                                                     "dd.MM.yy", "dd.M.yy", "d.M.yy", "d.MM.yy"};
                            DateTime dateValue = DateTime.UtcNow;

                            if (DateTime.TryParseExact(string.Format("{0}.{1}.{2}",
                                dRecord.values / 100,
                                dRecord.values % 100,
                                dataGM.DateObserv.Year),
                                dateFormats, System.Globalization.CultureInfo.GetCultureInfo("ru-RU"),
                                System.Globalization.DateTimeStyles.None,
                                out dateValue))
                            {
                                if (dateValue > dataGM.DateRecieve.AddDays(10))
                                {
                                    dateValue = dateValue.AddYears(-1);
                                }

                                obsDateLOC = DateTime.FromBinary(dateValue.ToBinary());
                                obsDateUTC = DateTime.FromBinary(dateValue.ToBinary());
                            }
                            else
                            {
                                continue;
                            }
                            // указатель: 
                            // 7/8-образование снежного покрова в поле/лесу, 
                            // 9/0 - сход снежного рокрова в поле/лесу
                            value = dRecord.check;
                        }
                        break;
                    #endregion

                    #region EnumCodeForm.KH15
                    case (int)EnumCodeForm.KH15:
                        //                       if (dRecord.check != 0) { continue; }
                        if (dRecord.paramId == 55) // расход воды
                        {
                            int power = ((int)value) / 1000 - 3;
                            value = Convert.ToSingle((value % 1000) * Math.Pow(10, power));
                        }
                        if (dRecord.lvalue != 0)
                        {
                            int localDay = dRecord.lvalue / 100;
                            int localHour = dRecord.lvalue % 100;

                            if (localHour == 20 && obsDateLOC.Hour != localHour)
                            {
                                if (obsDateLOC.Day == 1)
                                {
                                    obsDateLOC = new DateTime(obsDateLOC.AddMonths(-1).Year, obsDateLOC.AddMonths(-1).Month,
                                        localDay, localHour, 0, 0);
                                }
                                else
                                {
                                    obsDateLOC = new DateTime(obsDateLOC.AddDays(-1).Year, obsDateLOC.AddDays(-1).Month,
                                        localDay, localHour, 0, 0);
                                }
                            }
                            obsDateUTC = dataGM.Station.UTCOffset.HasValue ? obsDateLOC.AddHours(-dataGM.Station.UTCOffset.Value) : obsDateLOC;
                        }
                        break;
                    #endregion

                    case (int)EnumCodeForm.KH01:
                    case (int)EnumCodeForm.KH02:
                    case (int)EnumCodeForm.KH13:
                        obsDateUTC = DateTime.FromBinary(obsDateLOC.ToBinary());
                        obsDateLOC = dataGM.Station.UTCOffset.HasValue
                            ? obsDateUTC.AddHours(dataGM.Station.UTCOffset.Value)
                            : obsDateUTC;
                        break;
                }
                // Проверка на опоздавшие
                if (obsDateUTC > dataGM.DateRecieve.AddDays(1))
                {
                    if ((obsDateUTC - dataGM.DateRecieve).TotalDays > 11)
                    {
                        continue;
                    }
                    else
                    {
                        try
                        {
                            obsDateLOC = obsDateLOC.AddMonths(-1);
                            obsDateUTC = obsDateUTC.AddMonths(-1);
                        }
                        catch { }
                    }
                }

                int varId = param.VariableIdAmur;

                //
                // КОНТРОЛЬ : скорость порывов ветра в срок и между сроками.
                //
                // Со слов ДВ УГМС (Герасимов, так ему сказали в Мапмэйкерс):

                // Diff = 0 – максимальный порыв за полусутки
                // Diff = 10 – максимальный порыв в срок
                // Diff = 11 – максимальный порыв между сроками

                // Kод переменной для порывов ветра В СРОК?
                if (varId == 8)
                {
                    switch (dRecord.diff)
                    {
                        case 0:
                            varId = config.VariableIdWindGustHalfDay;
                            break;
                        case 10:
                            break;
                        case 11:
                            varId = config.VariableIdWindGustBetweenObs;
                            break;
                        default:
                            //throw new Exception("Неизвестное значение члена diff структуры MDBDATA для порыва ветра: diff=" + dRecord.diff);
                            string mess = string.Format("Неизвестное значение члена diff={0} структуры MDBDATA для порыва ветра в телеграмме\n{1}" +
                                "\nЗначение порыва отброшено.", dRecord.diff, dataGM.TelegramText);
                            Console.WriteLine(mess);
                            EventLog.WriteEntry(EVENT_LOG_SOURCE, mess, EventLogEntryType.Error);
                            varId = -1;
                            break;
                    }
                }

                if (varId > 0)
                {
                    // CALALOG ID

                    Catalog ctl = config.CatalogList.FirstOrDefault(x =>
                           x.SiteId == dataGM.Station.SiteIdAmur
                        && x.VariableId == varId
                        && x.OffsetTypeId == offsetTypeId
                        && x.OffsetValue == offsetValue
                        && x.SourceId == config.SourceId
                        && x.MethodId == methodId
                        );

                    if (ctl == null)
                    {
                        string msg;
                        try
                        {
                            ctl = new Catalog()
                            {
                                Id = -1,
                                SiteId = dataGM.Station.SiteIdAmur,
                                VariableId = varId,
                                MethodId = methodId,
                                SourceId = config.SourceId,
                                OffsetTypeId = offsetTypeId,
                                OffsetValue = offsetValue
                            };
                            ctl = svc.SaveCatalog(hSvc, ctl);
                            config.CatalogList.Add(ctl);

                            msg = string.Format("Создана запись каталога данных:\n\t" + ctl);
                            EventLog.WriteEntry(EVENT_LOG_SOURCE, msg, EventLogEntryType.Information);
                            //LoggerManager.Instance.WriteInfo("Создана запись каталога данных: " + ctlNew);

                        }
                        catch (Exception ex)
                        {
                            msg = string.Format("Ошибка создания записи каталога {0}\n\n{1}", ctl, ex.ToString());
                            EventLog.WriteEntry(EVENT_LOG_SOURCE, msg, EventLogEntryType.Information);
                        }
                    }
                    if (ctl.Id == -1) continue;

                    DataValue dv = new DataValue()
                    {
                        Id = -1,
                        CatalogId = ctl.Id,
                        Value = value,
                        DateLOC = obsDateLOC,
                        DateUTC = obsDateUTC,
                        FlagAQC = 0, // NoAQC
                        UTCOffset = dataGM.Station.UTCOffset.HasValue ? (float)dataGM.Station.UTCOffset : float.NaN
                    };

                    // КОНТРОЛЬ : Следы осадков (сек,час,сутки,полусутки)?
                    int k = 0;
                    if ((ctl.VariableId == 3 || ctl.VariableId == 22 || ctl.VariableId == 23 || ctl.VariableId == 45) && dv.Value == 990)
                        k = 1;

                    ret[k].Add(dv);
                }
            }
            return ret;
        }

        static EventLog GetEventLog(string logName, string source)
        {
            EventLog ret = null;
            try
            {
                //if (EventLog.SourceExists(source))
                //{
                ret = new EventLog(logName, ".", source);
                //}
                //An event log source should not be created and immediately used.
                //There is a latency time to enable the source, it should be created
                //prior to executing the application that uses the source.
                //Execute this sample a second time to use the new source.
                EventLog.CreateEventSource(source, logName);

                // The source is created.Exit the application to allow it to be registered.
                Console.WriteLine("Создан сервис событий источника {0} в системном журнале {1}.", source, logName);
                Console.WriteLine("Закройте приложение и перезапустите его для использования журнала и источника.");
                return null;
            }
            catch (System.Security.SecurityException sex)
            {
                EventLog.CreateEventSource(source, logName);

                Console.WriteLine("Создан сервис событий источника " + source + " в журнале " + logName);
                // The source is created.  Exit the application to allow it to be registered.
                Console.WriteLine("Закройте приложение и перезапустите его для использования источника.");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetEventLog" + logName + source);
                throw ex;
            }
        }

        /// <summary>
        /// Задача найти запись каталога, по которой потом ("не в нашем районе") будет записано значение за конкретную дату.
        /// Пример заточен для кодовой формы КН-02.
        /// Пример не тестировался, писался с ходу. 
        /// Нужно потренироваться с ним на копии БД Амур.
        /// 
        /// OSokolov@ferhri.ru
        /// 
        /// </summary>
        /// <param name="svc">Клиент сервиса.</param>
        /// <param name="hSvc">Ручка сервиса.</param>
        /// <param name="catalogs">Список может быть пустым (Count = 0), но  не null.</param>
        /// <param name="variableId">Код переменной из списка переменных кода КН-02 БД Амур. Он берётся из известного документа...</param>
        /// <param name="observObjectCode">Код объекта наблюдений. Из телеграммы.</param>
        /// <param name="stationIndex">Индекс станции. Из телеграммы.</param>
        /// <param name="stationName">Наименование станции. Не должно быть null.</param>
        /// <returns>Экземпляр каталога из которого для записи значения в БД Амур будет нужен только Id.</returns>
        //////static Catalog GetCatalog_KH02_4_DIstomin(ServiceClient svc, long hSvc, List<Catalog> catalogs, int stationIndex, string stationName, int observObjectCode, int variableId)
        //////{
        //////    // Определяем ключ записи каталога.

        //////    // 1. Код типа смещения в пункте измерения (табл. offset_type). 0 - нет смещения.
        //////    int offsetTypeId = 0;
        //////    // 2. Значение смещения в пункте измерения. 
        //////    int offsetValue = 0;
        //////    // 3. Код метода измерения (табл. method). 0 - наблюдения.
        //////    int methodId = 0;
        //////    // 4. Код источника данных (табл. source). 0 - ДВ УГМС. 
        //////    // Можно заменить на источник "Телеграмма КН-02". Не принципиально.
        //////    int sourceId = 0;
        //////    // 5. Код переменной известен из вх. параметров и равен variableId. Он берётся из известного документа...
        //////    // 6. Код наблюдательного пункта - ниже будем его искать, 
        //////    // используя информацию об индексе станции (stationIndex) и кода объекта наблюдения (observObjectCode) из вх. параметров метода.
        //////    int siteId;

        //////    // Всё, что выше - это и есть ключ записи каталога данных.

        //////    // Найдём или создадим запись каталога, если её нет. 
        //////    // А потом, в другом месте, "не в нашем районе",  запишем значение из телеграммы с этим кодом.

        //////    //
        //////    // Единственное что неизвестно в записи каталога - код пункта.
        //////    // Ищем станцию и её пункт (station & site) в базе или создадим их.
        //////    //

        //////    // Определяем тип станции. Это морской пост, понятно. 
        //////    // Но, он может идти под индексом метеорологической станции.
        //////    // 1 - мет. станция, 3 - морской пост. Код из таблицы station_type.
        //////    int stationType = stationIndex >= 90000 ? 3 : 1;

        //////    Station station = svc.GetStationByIndex(hSvc, stationIndex.ToString());

        //////    // Нет такой станции?
        //////    if (station == null)
        //////    {
        //////        // Нет станции. Создаём её.
        //////        int stationId = svc.SaveStation(hSvc, new Station() { Id = -1, Code = stationIndex.ToString(), Name = stationName, TypeId = stationType });
        //////        // Создаём пункт наблюдений для станции. В код пункта вставляем объект наблюдений.
        //////        // Нужно сделать методом, т.к. дублируется ниже.
        //////        siteId = svc.SaveSite(hSvc, new Site()
        //////        {
        //////            Id = -1,
        //////            SiteCode = "KH02-" + observObjectCode, // Например, так. Можно как попало, лишь бы потом можно было взять код объекта.
        //////            StationId = stationId,
        //////            SiteTypeId = 3, // Всегда морской пост
        //////            Description = "Import procedure auto-created site." // Например, так. Можно как попало. Можно null.
        //////        });
        //////    }
        //////    // Есть такая станция
        //////    else
        //////    {
        //////        // Ищем пункт (сайт) станции с нужным кодом, в котором "сидит" объект наблюдений.
        //////        List<Site> sites = svc.GetSitesByStation(hSvc, station.Id, 3);
        //////        Site site = sites.FirstOrDefault(x => x.SiteCode == "КН02-" + observObjectCode);

        //////        // Нет пункта?
        //////        if (site == null)
        //////        {
        //////            // Создаём пункт наблюдений для станции. В код которого вставляем объект наблюдений.
        //////            // Нужно сделать методом, т.к. дублируется выше.
        //////            siteId = svc.SaveSite(hSvc, new Site()
        //////            {
        //////                Id = -1,
        //////                SiteCode = "KH02-" + observObjectCode,
        //////                StationId = station.Id,
        //////                SiteTypeId = 3, // Всегда морской пост
        //////                Description = "Import procedure auto-created site."
        //////            });
        //////        }
        //////        // Есть пункт. Ну, хорошо.
        //////        else
        //////        {
        //////            siteId = site.Id;
        //////        }
        //////    }

        //////    // Нашли или создали пункт (сайт).
        //////    // Теперь ищем в списке или создаём в базе запись каталога для полного ключа.

        //////    Catalog catalog = catalogs.FirstOrDefault(x =>
        //////        x.OffsetTypeId == offsetTypeId
        //////        && x.OffsetValue == offsetValue
        //////        && x.MethodId == methodId
        //////        && x.SourceId == sourceId
        //////        && x.SiteId == siteId
        //////        && x.VariableId == variableId);

        //////    // Нет такой записи каталога? 
        //////    if (catalog == null)
        //////    {
        //////        // Нет. Создаём запись каталога
        //////        catalog = svc.SaveCatalog(hSvc, new Catalog()
        //////        {
        //////            Id = -1,
        //////            OffsetTypeId = offsetTypeId,
        //////            OffsetValue = offsetValue,
        //////            MethodId = methodId,
        //////            SourceId = sourceId,
        //////            SiteId = siteId,
        //////            VariableId = variableId
        //////        });
        //////        // И добавляем созданную запись каталога в список, чтобы потом не читать по 100 раз.
        //////        // Можно не держать этот список и не таскать его за собой, 
        //////        // но тогда придётся каждый раз читать таблицу catalog. На каждое значение из телеграммы... а это дорого.
        //////        catalogs.Add(catalog);
        //////    }

        //////    return catalog;
        //////    // Для вставки значения из телеграммы в таблицу data_value нужен catalog.id, дата и само значение.
        //////    // Это где-то в другом месте.
        //////}
    }
}
