using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using FERHRI.Amur.Meta;
using FERHRI.Amur.Data;

namespace FERHRI.Amur.Service
{
    //[ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public partial class Service : IService
    {
        static public Dictionary<long, string> _svcConfig = new Dictionary<long, string>();
        static public int MaxUsers = 10;
        static public bool _isOpening = false;

        //string ConnectionString { get; set; }
        public Service()
        {
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
        /// -3 - другая ошибка при открытии сервиса.
        /// </returns>
        public long Open(string userName, string password)
        {
            // Сервис в настоящий момент открываеся другим пользователем?
            if (_isOpening) return -2;
            _isOpening = true;

            try
            {
                // Превышено макс. число пользователей сервиса?
                if (_svcConfig.Count == MaxUsers) return -1;

                Common.User user = new Common.User(userName, password);
                string cnns = Common.ADbNpgsql.ConnectionStringUpdateUser(FERHRI.Amur.Service.Properties.Settings.Default.AmurConnectionString, user);

                if (string.IsNullOrEmpty(_svcConfig.Values.FirstOrDefault(x => x == cnns)))
                {
                    // Ошибка в имени, пароле пользователя?
                    Common.ADbNpgsql.TestOpenConnection(cnns);

                    long hSvc = Math.Abs(DateTime.Now.ToBinary());
                    _svcConfig.Add(hSvc, cnns);
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
        static public Parser.DataManager DataManagerParser(long hSvc)
        {
            return Parser.DataManager.GetInstance(GetConnectionString(hSvc));
        }
    }
}
