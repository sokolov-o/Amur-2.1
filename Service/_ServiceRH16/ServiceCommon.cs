using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using FERHRI.Amur.Meta;
using FERHRI.Amur.Data;
using System.Configuration;

namespace FERHRI.Amur.ServiceRH16
{
    /// <summary>
    /// TODO: Change Web.config Amur Db connection after re-compilation!
    /// </summary>
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public partial class Service : IService
    {
        static public Dictionary<long, string> _svcConfig = new Dictionary<long, string>();
        static public int MaxUsers = 4;
        static public bool _isOpening = false;

        // CONFIG
        /// <summary>
        /// Станции, доступные на клиенте.
        /// Инициализируются в конструкторе.
        /// </summary>
        static List<Station> _stations = null;
        /// <summary>
        /// Переменные, доступные на клиенте.
        /// Инициализируются в конструкторе.
        /// </summary>
        static List<Variable> _clientAggrVariables = null;
        /// <summary>
        /// Прогностические переменные для агрегации.
        /// Инициализируются в конструкторе.
        /// </summary>
        static List<Variable> _fcsIniVariables = null;
        /// <summary>
        /// MethodId, SourceId, OffsetId, OffsetValue для _fcsIniVariablesId
        /// </summary>
        static object[] _fcsKeyMSOO = new object[4];
        /// <summary>
        /// Наблюдённые переменные для агрегации.
        /// Инициализируются в конструкторе.
        /// </summary>
        static List<Variable> _obsIniVariables = null;
        /// <summary>
        /// MethodId, SourceId, OffsetId, OffsetValue для _obsIniVariablesId
        /// </summary>
        static object[] _obsKeyMSOO = new object[4];
        /// <summary>
        /// Период данных, доступный для клиента.
        /// </summary>
        static DateTime[] _dateSFLimit = null;
        /// <summary>
        /// Разрешить клиенту читать прогностические данные?
        /// </summary>
        static bool allowReadForecastData = false;

        static string DATE_FORMAT = "yyyyMMdd HH:mm";

        Service()
        {
            if (_stations == null)
            {
                Data.DataManager.SetDefaultConnectionString(FERHRI.Amur.ServiceRH16.Properties.Settings.Default.AmurConnectionString);
                Meta.DataManager.SetDefaultConnectionString(FERHRI.Amur.ServiceRH16.Properties.Settings.Default.AmurConnectionString);

                // SITES WITH UTC_OFFSET etc.
                Meta.DataManager dm = Meta.DataManager.GetInstance();
                // Может быть несколько групп станций
                List<Meta.Station> stations = new List<Meta.Station>();
                List<Meta.Site> sites = new List<Meta.Site>();
                foreach (var item in Common.StrVia.ToListInt(ConfigurationManager.AppSettings["SITE_GROUP_IDS"], ";"))
                {
                    Meta.SiteGroup sg = dm.SiteGroupRepository.SelectGroupFK(item);
                    if (sg != null)
                    {
                        stations.AddRange(sg.StationList);
                        sites.AddRange(sg.SiteList);
                    }
                }
                _stations = Station.GetStations(stations, sites);

                List<EntityAttrValue> eavs = dm.EntityAttrRepository.SelectAttrValuesActual
                    ("site", _stations.Select(x => x.Id).ToList(), new List<int>(new int[] { 1003/*UTCOffset*/, 1007/*MeteoZoneId*/ }), DateTime.Now);
                List<MeteoZone> mz = dm.MeteoZoneRepository.Select();

                foreach (var station in _stations)
                {
                    EntityAttrValue eav = eavs.Find(x => x.EntityId == station.Id && x.AttrTypeId == 1003);
                    if (eav == null)
                        throw new Exception(string.Format("Для пункта [{0}] отсутствует атрибут \"UTCOffset\".", station));
                    station.UTCOffset = int.Parse(eav.Value);

                    eav = eavs.Find(x => x.EntityId == station.Id && x.AttrTypeId == 1007);
                    if (eav == null)
                        throw new Exception(string.Format("Для пункта [{0}] отсутствует атрибут \"Код Метеозоны\".", station));
                    int mzId = int.Parse(eav.Value);

                    station.UTCHourDayStart = mz.Find(x => x.Id == mzId).UTCHourDayStart;
                }

                // DATE SF
                string[] sdateSFLimit = ConfigurationManager.AppSettings["DATE_SF"].Split(';');
                _dateSFLimit = new DateTime[] { DateTime.MinValue, DateTime.MaxValue };

                if (!string.IsNullOrEmpty(sdateSFLimit[0].Trim()))
                    if (!Common.DateTimeProcess.TryParse(sdateSFLimit[0], DATE_FORMAT, out _dateSFLimit[0]))
                        throw new Exception("Ошибка в конфигурации экспорта: строка ограничения по времени " + sdateSFLimit[0]);
                if (!string.IsNullOrEmpty(sdateSFLimit[1].Trim()))
                    if (!Common.DateTimeProcess.TryParse(sdateSFLimit[1], DATE_FORMAT, out _dateSFLimit[1]))
                        throw new Exception("Ошибка в конфигурации экспорта: строка ограничения по времени " + sdateSFLimit[1]);
                // ALLOW_READ_FORECAST_DATA
                string s = ConfigurationManager.AppSettings["ALLOW_READ_FORECAST_DATA"];
                allowReadForecastData = s == "1" ? true : false;

                // CLIENT VARIABLES
                _clientAggrVariables = SelectVariableOrdered(dm, Common.StrVia.ToListInt(ConfigurationManager.AppSettings["CLIENT_VARIABLE_ID"], ";"));

                // FCS VARIBLES & KEY
                _fcsIniVariables = SelectVariableOrdered(dm, Common.StrVia.ToListInt(ConfigurationManager.AppSettings["FCS_INI_VARIABLE_ID"], ";"));

                string[] methodsource = ConfigurationManager.AppSettings["FCS_INI_METHOD_SOURCE_OFFSET"].Split(';');
                _fcsKeyMSOO[0] = int.Parse(methodsource[0]);
                _fcsKeyMSOO[1] = int.Parse(methodsource[1]);
                _fcsKeyMSOO[2] = int.Parse(methodsource[2]);
                double ov;
                if (!double.TryParse(methodsource[3].Replace(".", ","), out ov))
                    if (!double.TryParse(methodsource[3].Replace(",", "."), out ov))
                        ov = double.NaN;
                _fcsKeyMSOO[3] = ov;

                // OBS VARIBLES & KEY
                _obsIniVariables = SelectVariableOrdered(dm, Common.StrVia.ToListInt(ConfigurationManager.AppSettings["OBS_INI_VARIABLE_ID"], ";"));

                methodsource = ConfigurationManager.AppSettings["OBS_INI_METHOD_SOURCE_OFFSET"].Split(';');
                _obsKeyMSOO[0] = int.Parse(methodsource[0]);
                _obsKeyMSOO[1] = int.Parse(methodsource[1]);
                _obsKeyMSOO[2] = int.Parse(methodsource[2]);

                if (!double.TryParse(methodsource[3].Replace(".", ","), out ov))
                    if (!double.TryParse(methodsource[3].Replace(",", "."), out ov))
                        ov = double.NaN;
                _obsKeyMSOO[3] = ov;
            }
        }
        private List<Variable> SelectVariableOrdered(Meta.DataManager dm, List<int> varId)
        {
            List<Variable> ret = new List<Variable>();
            foreach (var item in varId)
            {
                ret.Add(new Variable(dm.VariableRepository.Select(item)));
            }
            return ret;
        }
        /// <summary>
        /// Открытие рабочей сессии.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        /// <param name="password">Пароль.</param>
        /// <returns>
        /// Дескриптор открытой сессии - целое число > 0 или < 0 в случае ошибки:
        /// -1 - превышено допустимое количество пользователей сервиса;
        /// -2 - сервис занят открытие другой сессии;
        /// -3 - возможна ошибка в имени, пароле пользователя..
        /// </returns>
        public long Open(string userName, string password)
        {
            // Сервис в настоящий момент не открываеся другим пользователем?
            if (!_isOpening)
            {
                try
                {
                    _isOpening = true;

                    Common.User user = new Common.User(userName, password);
                    string cnns = Common.ADbNpgsql.ConnectionStringUpdateUser(FERHRI.Amur.ServiceRH16.Properties.Settings.Default.AmurConnectionString, user);

                    // Возможно, ошибка в имени, пароле пользователя.
                    if (!Common.ADbNpgsql.IsConnectionOK(cnns))
                        return -3;

                    long hSvc;
                    if (!string.IsNullOrEmpty(_svcConfig.Values.FirstOrDefault(x => x == cnns)))
                    {
                        hSvc = _svcConfig.First(x => x.Value == cnns).Key;
                    }
                    else
                    {
                        // Превышено макс. число пользователей сервиса?
                        if (_svcConfig.Count == MaxUsers) return -1;

                        hSvc = Math.Abs(DateTime.Now.ToBinary());
                        _svcConfig.Add(hSvc, cnns);
                    }
                    return hSvc;
                }
                finally
                {
                    _isOpening = false;
                }
            }
            return -2;
        }
        /// <summary>
        /// Закрытие рабочей сессии по её идентификатору.
        /// </summary>
        /// <param name="hSvc">Идентификатор сессии, полученный методом Open.</param>
        public void Close(int hSvc)
        {
            _svcConfig.Remove(hSvc);
        }
        static public Data.DataManager DataManagerData(long hSvc)
        {
            string cnns = null;
            if (_svcConfig.TryGetValue(hSvc, out cnns))
            {
                var ret = Data.DataManager.GetInstance(cnns);
                if (ret == null)
                    throw new Exception("Ошибка создания экземпляра менеджера базы данных Data.");
                return ret;
            }
            throw new Exception("Указанный идентификатор сессии не зарегистрирован в сервисе.");
        }
        static public Meta.DataManager DataManagerMeta(long hSvc)
        {
            string cnns = null;
            if (_svcConfig.TryGetValue(hSvc, out cnns))
            {
                var ret = Meta.DataManager.GetInstance(cnns);
                if (ret == null)
                    throw new Exception("Ошибка создания экземпляра менеджера базы данных Meta.");
                return ret;
            }
            throw new Exception("Указанный идентификатор сессии не зарегистрирован в сервисе.");
        }
    }
}
