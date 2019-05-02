using System;

namespace SOV.Amur.Sys
{
    public class DataManager : Common.BaseDataManager
    {
        public readonly SysEntityRepository SysEntityRepository;
        public readonly LogRepository LogRepository;

        public DataManager(string connectionString) : base(connectionString)
        {
            SysEntityRepository = new SysEntityRepository(this);
            LogRepository = new LogRepository(this);
        }

        public static string GetDefaultConnectionString()
        {
            return Properties.Settings.Default.ConnectionString;
        }

        public static void SetDefaultConnectionString(string cnns)
        {
            Properties.Settings.Default["ConnectionString"] = cnns;
        }

        /// <summary>
        /// Экземпляр со строкой подключения по умолчанию.
        /// </summary>
        /// <returns></returns>
        public static DataManager GetInstance()
        {
            return (DataManager)GetInstance(GetDefaultConnectionString(), Type.GetType("SOV.Amur.Sys.DataManager"));
        }
        /// <summary>
        /// Экземпляр с заданной строкой подключения.
        /// </summary>
        public static DataManager GetInstance(string connectionString)
        {
            return (DataManager)GetInstance(connectionString, Type.GetType("SOV.Amur.Sys.DataManager"));
        }
    }
}
