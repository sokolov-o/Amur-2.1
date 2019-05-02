using System;
using Npgsql;
using SOV.Common;

namespace SOV.Social
{
    public class DataManager : SOV.Common.BaseDataManager
    {
        public readonly LegalEntityRepository LegalEntityRepository;
        public readonly StaffRepository StaffRepository;
        public readonly StaffPositionRepository StaffPositionRepository;
        public readonly StaffEmployeeRepository StaffEmployeeRepository;
        public readonly AddrTypeRepository AddrTypeRepository;
        public readonly AddrRepository AddrRepository;
        public readonly OrgRepository OrgRepository;
        public readonly PersonRepository PersonRepository;
        public readonly ImageRepository ImageRepository;
        public readonly LegalEntityXImageRepository LegalEntityXImageRepository;
        public readonly DivisionRepository DivisionRepository;

        static public void ClearCashs()
        {
            LegalEntityRepository.ClearCache();
            StaffRepository.ClearCache();
            StaffEmployeeRepository.ClearCache();
            StaffPositionRepository.ClearCache();
            AddrTypeRepository.ClearCache();
            AddrRepository.ClearCache();
            OrgRepository.ClearCache();
            PersonRepository.ClearCache();
            ImageRepository.ClearCache();
            LegalEntityXImageRepository.ClearCache();
            DivisionRepository.ClearCache();
        }
        public DataManager(string connectionString)
            : base(connectionString)
        {
            LegalEntityRepository = new LegalEntityRepository(this);
            StaffRepository = new StaffRepository(this);
            StaffEmployeeRepository = new StaffEmployeeRepository(this);
            StaffPositionRepository = new StaffPositionRepository(this);
            AddrTypeRepository = new AddrTypeRepository(this);
            AddrRepository = new AddrRepository(this);
            OrgRepository = new OrgRepository(this);
            PersonRepository = new PersonRepository(this);
            ImageRepository = new ImageRepository(this);
            LegalEntityXImageRepository = new LegalEntityXImageRepository(this);
            DivisionRepository = new DivisionRepository(this);
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
            return (DataManager)GetInstance(GetDefaultConnectionString());//, Type.GetType("SOV.Infores.DataManager"));
        }
        /// <summary>
        /// Экземпляр с заданной строкой подключения.
        /// </summary>
        public static DataManager GetInstance(string connectionString)
        {
            return (DataManager)GetInstance(connectionString, Type.GetType("SOV.Social.DataManager"));
        }

        /// <summary>
        /// Parse reader where fields are {name_rus,name_rus_short,name_eng,name_eng_short}
        /// </summary>
        /// <param name="rdr">Reader where fields are {name_rus,name_rus_short,name_eng,name_eng_short}</param>
        /// <returns></returns>
        internal static IdNames ParseIdNames(NpgsqlDataReader rdr)
        {
            return new IdNames()
            {
                Id = (int)rdr["id"],
                NameRus = rdr["name_rus"].ToString(),
                NameEng = rdr["name_eng"].ToString(),
                NameRusShort = rdr["name_rus_short"].ToString(),
                NameEngShort = rdr["name_eng_short"].ToString(),
            };
        }
    }
}
