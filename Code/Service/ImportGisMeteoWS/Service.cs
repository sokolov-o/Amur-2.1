using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using SOV.Amur.Importer.GISMeteo.AmurService;
using SOV.Common;
using SOV.GISMeteo;

namespace SOV.Amur.GISMeteoWS
{
    public partial class Service : ServiceBase
    {
        Timer timer = new Timer(); // name space(using System.Timers;)  
        /// <summary>
        /// Клиент сервиса записи в БД.
        /// </summary>
        static ServiceClient svc;
        static long hSvc = 0;

        static User user = null;
        static List<Settings> configList = null;
        static bool isBusy = false;

        public Service()
        {
            InitializeComponent();
        }
        protected override void OnStart(string[] args)
        {
            WriteToLog("Service is started at " + DateTime.Now);

            // INITIALIZE Amur service

            try
            {
                string[] suser = Properties.Settings.Default.User.Split(';');
                user = new User(suser[0], suser[1]);
                svc = new ServiceClient();
                svc.Open();
                hSvc = svc.Open(user.Name, user.Password);

                if (hSvc < 0)
                {
                    WriteToLog(string.Format("{0} Пользователь {1} не зарегистрирован в сервисе. Код возврата {2}.", DateTime.Now, user.Name, hSvc));
                    return;
                }
                WriteToLog(string.Format("{0} Пользователь {1} зарегистрирован в сервисе.", DateTime.Now, user.Name));

                // Get importer settings
                configList = Settings.Parse(svc, hSvc,
                    ConfigurationManager.AppSettings["Setting.xml path"],
                    ConfigurationManager.ConnectionStrings["MdbCheckPointDirectory"].ConnectionString);
            }
            catch (Exception ex )
            {
                WriteToLog(ex.ToString());
                return;
            }
            finally
            {
                svc.Close();
            }

            // INIT TIMER

            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = Convert.ToInt32(ConfigurationManager.AppSettings["periodInMinutes"]) * 60 * 1000; //number in milisecinds  
            timer.Enabled = true;
            //timer = new System.Threading.Timer(TimerCallback, null, 0, periodInMinutes * 60 * 1000);
        }
        protected override void OnStop()
        {
            WriteToLog("Service is stopped at " + DateTime.Now);
        }
        private void OnElapsedTime(object source, ElapsedEventArgs e)
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
                        WriteToLog(mess1);
                    }
                    catch (Exception ex)
                    {
                        string mess1 = string.Format(" Ошибка при обращении к БД ГисМетео.\n{0}", ex.Message);
                        WriteToLog(mess1);
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
                                WriteToLog(mess1);
                            }
                        }
                        WriteToLog(string.Format(" {0}", ok ? "Успешно" : "Есть ошибки"));
                    }

                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                WriteToLog(ex.Message);
            }
            finally
            {
                WriteToLog(string.Format("End. {1} min elapsed.", DateTime.UtcNow, (DateTime.UtcNow - dateTimerS).TotalMinutes));

                svc.Close();
                isBusy = false;
            }
        }
        static public void WriteToLog(string Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {
                // Create a file to write to.   
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
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
                            string mess = string.Format("Неизвестное значение члена diff={0} структуры MDBDATA для порыва ветра в телеграмме\n{1}" +
                                "\nЗначение порыва отброшено.", dRecord.diff, dataGM.TelegramText);
                            WriteToLog(mess);
                            //EventLog.WriteEntry(EVENT_LOG_SOURCE, mess, EventLogEntryType.Error);
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

                            WriteToLog(string.Format("Создана запись каталога данных [{0}]", ctl));

                        }
                        catch (Exception ex)
                        {
                            WriteToLog(string.Format("Ошибка создания записи каталога [{0}]: {1}", ctl, ex.ToString()));
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
    }
}
