using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using SOV.Common;

namespace SOV.Amur.Parser
{
    public class DataManager : Common.ADbNpgsql
    {
        DataManager(string connectionString)
            : base(connectionString)
        {
            ConnectionString = connectionString;
        }
        static Dictionary<string, DataManager> _dm = new Dictionary<string, DataManager>();

        /// <summary>
        /// Экземпляр с заданной строкой подключения.
        /// </summary>
        /// <param name="connectionString">Строка подключения. Если null, используется строка подключения по умолчанию из настроек проекта.</param>
        /// <returns></returns>
        public static DataManager GetInstance(string connectionString = null)
        {
            DataManager ret;
            string cnns = connectionString == null ? global::SOV.Amur.Parser.Properties.Settings.Default.ConnectionString : connectionString;

            if (!_dm.TryGetValue(cnns, out ret))
            {
                ret = new DataManager(cnns);
                _dm.Add(cnns, ret);
            }
            return ret;
        }

        private SysObjRepository _SysObjRepository;
        public SysObjRepository SysObjRepository
        {
            get
            {
                if (_SysObjRepository == null)
                    _SysObjRepository = new SysObjRepository(this);
                return _SysObjRepository;
            }
        }
        private SysParsersXSitesRepository _SysParsersXSitesRepository;
        public SysParsersXSitesRepository SysParsersXSitesRepository
        {
            get
            {
                if (_SysParsersXSitesRepository == null)
                    _SysParsersXSitesRepository = new SysParsersXSitesRepository(this);
                return _SysParsersXSitesRepository;
            }
        }
        private SysParsersParamsRepository _SysParsersParamsRepository;
        public SysParsersParamsRepository SysParsersParamsRepository
        {
            get
            {
                if (_SysParsersParamsRepository == null)
                    _SysParsersParamsRepository = new SysParsersParamsRepository(this);
                return _SysParsersParamsRepository;
            }
        }
        private SysObjTypeRepository _SysObjTypeRepository;
        public SysObjTypeRepository SysObjTypeRepository
        {
            get
            {
                if (_SysObjTypeRepository == null)
                    _SysObjTypeRepository = new SysObjTypeRepository(this);
                return _SysObjTypeRepository;
            }
        }

        public static void SetDefaultConnectionString(string cnns)
        {
            SOV.Amur.Parser.Properties.Settings.Default["ConnectionString"] = cnns;
        }
    }
}
