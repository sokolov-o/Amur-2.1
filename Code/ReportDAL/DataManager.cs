using System;

namespace SOV.Amur.Report
{
    public class DataManager : Common.BaseDataManager
    {
        public readonly ReportRepository ReportRepository;
        public readonly OrgRepository OrgRepository;

        public DataManager(string connectionString) : base(connectionString)
        {
            ReportRepository = new ReportRepository(this);
            OrgRepository = new OrgRepository(this);
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
            return (DataManager)GetInstance(GetDefaultConnectionString(), Type.GetType("SOV.Amur.Report.DataManager"));
        }
        /// <summary>
        /// Экземпляр с заданной строкой подключения.
        /// </summary>
        public static DataManager GetInstance(string connectionString)
        {
            return (DataManager)GetInstance(connectionString, Type.GetType("SOV.Amur.Report.DataManager"));
        }
    }
}