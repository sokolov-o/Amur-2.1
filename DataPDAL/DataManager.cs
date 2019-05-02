using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace SOV.Amur.DataP
{
    public class DataManager : Common.ADbNpgsql
    {
        DataManager(string connectionString)
            : base(connectionString)
        {
        }
        static Dictionary<string, DataManager> _dm = new Dictionary<string, DataManager>();
        /// <summary>
        /// Экземпляр со строкой подключения по умолчанию.
        /// </summary>
        /// <returns></returns>
        public static DataManager GetInstance()
        {
            return GetInstance(global::SOV.Amur.DataP.Properties.Settings.Default.ConnectionString);
        }

        static public void SetDefaultConnectionString(string cnns)
        {
            SOV.Amur.DataP.Properties.Settings.Default["ConnectionString"] = cnns;
        }
        /// <summary>
        /// Экземпляр с заданной строкой подключения.
        /// </summary>
        /// <returns></returns>
        public static DataManager GetInstance(string connectionString)
        {
            DataManager ret;
            if (!_dm.TryGetValue(connectionString, out ret))
            {
                ret = new DataManager(connectionString);
                _dm.Add(connectionString, ret);
            }
            return ret;
        }
        private AQCRepository _AQCRepository;
        public AQCRepository AQCRepository
        {
            get
            {
                if (_AQCRepository == null)
                    _AQCRepository = new AQCRepository(this);
                return _AQCRepository;
            }
        }
        private DerivedTasksRepository _DerivedTasksRepository;
        public DerivedTasksRepository DerivedTasksRepository
        {
            get
            {
                if (_DerivedTasksRepository == null)
                    _DerivedTasksRepository = new DerivedTasksRepository(this);
                return _DerivedTasksRepository;
            }
        }
        private DerivedDayAttrRepository _DerivedDayAttrRepository;
        public DerivedDayAttrRepository DerivedDayAttrRepository
        {
            get
            {
                if (_DerivedDayAttrRepository == null)
                    _DerivedDayAttrRepository = new DerivedDayAttrRepository(this);
                return _DerivedDayAttrRepository;
            }
        }
        private DataValueProcessRepository _DataValueProcessRepository;
        public DataValueProcessRepository DataValueProcessRepository
        {
            get
            {
                if (_DataValueProcessRepository == null)
                    _DataValueProcessRepository = new DataValueProcessRepository(this);
                return _DataValueProcessRepository;
            }
        }
        private RegionXMeteoZoneRepository _RegionXMeteoZoneRepository;
        public RegionXMeteoZoneRepository RegionXMeteoZoneRepository
        {
            get
            {
                if (_RegionXMeteoZoneRepository == null)
                    _RegionXMeteoZoneRepository = new RegionXMeteoZoneRepository(this);
                return _RegionXMeteoZoneRepository;
            }
        }
    }
}
