using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using SOV.Amur.Meta;
using SOV.Amur.Data;
using System.Configuration;

namespace SOV.Amur.Service
{
    //[ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    /// <summary>
    /// Общие методы сервиса.
    /// 
    /// TODO: !!! перед service-publish убедиться в корректности строки подключения к БД в web.config !!!
    /// </summary>
    public partial class Service : IService
    {
        static Dictionary<long/*Session Id*/, string/*Connection string*/> _svcConfig = new Dictionary<long, string>();
        static int MaxUsers = 10;
        static bool _isOpening = false;
        static string _logFilePath = @"c:\temp\amur_service_rw_log.txt";
        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public Service()
        {
            SOV.Amur.Meta.DataManager.SetDefaultConnectionString(System.Configuration.ConfigurationManager.ConnectionStrings["AmurConnectionString"].ConnectionString);
        }
        /// <summary>
        /// Открытие рабочей сессии.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        /// <param name="password">Пароль.</param>
        /// <returns>
        /// Дескриптор открытой сессии - целое число большее 0 или меньше 0 в случае ошибки:
        /// -1 - превышено допустимое количество пользователей сервиса;
        /// -2 - сервис занят открытием другой сессии;
        /// -3 - некорректное имя, пароль пользователя;
        /// или Exception()
        /// </returns>
        public long Open(string userName, string password)
        {
            System.IO.File.AppendAllText(_logFilePath, string.Format("Open {0}\t{1}\n", userName, DateTime.Now));

            // Сервис в настоящий момент открываеся другим пользователем?
            if (_isOpening)
            {
                System.IO.File.AppendAllText(_logFilePath, "Сервис в настоящий момент открываеся другим пользователем. Отказано в открытии сессии.\n");
                return -2;
            }

            _isOpening = true;

            try
            {
                // Превышено макс. число пользователей сервиса?
                if (_svcConfig.Count == MaxUsers)
                {
                    System.IO.File.AppendAllText(_logFilePath, "Превышено макс. число пользователей сервиса. Отказано в открытии сессии.\n");
                    return -1;
                }

                Common.User user = new Common.User(userName, password);
                var constr = System.Configuration.ConfigurationManager.ConnectionStrings["AmurConnectionString"].ConnectionString;
                string cnns = Common.ADbNpgsql.ConnectionStringUpdateUser(constr, user);

                //System.IO.File.AppendAllText(logPath, string.Format("{0}\t{1}\n", DateTime.Now, cnns));

                if (!_svcConfig.Values.Any(x => x == cnns))
                {
                    //System.IO.File.AppendAllText(logPath, string.Format("{0}\tbefore test\n", DateTime.Now));
                    // Ошибка в имени, пароле пользователя?
                    if (!Common.ADbNpgsql.IsConnectionOK(cnns))
                    {
                        System.IO.File.AppendAllText(_logFilePath, "Ошибка в имени и/или пароле пользователя? Отказано в открытии сессии.\n");
                        return -3;
                    }

                    //System.IO.File.AppendAllText(logPath, string.Format("{0}\tafter test\n", DateTime.Now));

                    long hSvc = Math.Abs(DateTime.Now.ToBinary());
                    _svcConfig.Add(hSvc, cnns);

                    System.IO.File.AppendAllText(_logFilePath, string.Format("Сессия пользователя {0} успешно открыта.\n", userName));
                    return hSvc;
                }
                else
                {
                    return _svcConfig.First(x => x.Value == cnns).Key;
                }
            }
            finally
            {
                _isOpening = false;
            }
        }

        /// <summary>
        /// Закрытие рабочей сессии по её идентификатору.
        /// </summary>
        /// <param name="hSvc">Идентификатор сессии, полученный методом Open.</param>
        public void Close(long hSvc)
        {
            _svcConfig.Remove(hSvc);
        }

        static public Data.DataManager DataManagerData(long hSvc)
        {
            return Data.DataManager.GetInstance(GetConnectionString(hSvc));
        }
        static private string GetConnectionString(long hSvc)
        {
            string ret = null;
            if (_svcConfig.TryGetValue(hSvc, out ret))
                return ret;
            throw new Exception("Указанный идентификатор сессии не зарегистрирован в сервисе " + hSvc);
        }
        static public DataP.DataManager DataManagerDataP(long hSvc)
        {
            return DataP.DataManager.GetInstance(GetConnectionString(hSvc));
        }
        static public Meta.DataManager DataManagerMeta(long hSvc)
        {
            return Meta.DataManager.GetInstance(GetConnectionString(hSvc));
        }
        static public Social.DataManager DataManagerSocial(long hSvc)
        {
            return Social.DataManager.GetInstance(GetConnectionString(hSvc));
        }
        static public Parser.DataManager DataManagerParser(long hSvc)
        {
            return Parser.DataManager.GetInstance(GetConnectionString(hSvc));
        }
    }
}
