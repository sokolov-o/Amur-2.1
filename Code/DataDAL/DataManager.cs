using System;

namespace SOV.Amur.Data
{
    public class DataManager : Common.BaseDataManager
    {
        public readonly DataValueRepository DataValueRepository;
        public readonly DataValue2017Repository DataValue2017Repository;
        public readonly DataSourceRepository DataSourceRepository;
        public readonly DataForecastRepository DataForecastRepository;
        public readonly ClimateRepository ClimateRepository;
        public readonly CurveRepository CurveRepository;
        public readonly CurveSeriaRepository CurveSeriaRepository;

        public DataManager(string connectionString) : base(connectionString)
        {
            DataValueRepository = new DataValueRepository(this);
            DataValue2017Repository = new DataValue2017Repository(this);
            DataSourceRepository = new DataSourceRepository(this);
            DataForecastRepository = new DataForecastRepository(this);
            ClimateRepository = new ClimateRepository(this);
            CurveRepository = new CurveRepository(this);
            CurveSeriaRepository = new CurveSeriaRepository(this);
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
            return (DataManager)GetInstance(GetDefaultConnectionString(), Type.GetType("SOV.Amur.Data.DataManager"));
        }
        /// <summary>
        /// Экземпляр с заданной строкой подключения.
        /// </summary>
        public static DataManager GetInstance(string connectionString)
        {
            return (DataManager)GetInstance(connectionString, Type.GetType("SOV.Amur.Data.DataManager"));
        }
    }
}
