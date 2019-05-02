using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Odbc;
using FERHRI.Amur.MetaData;
using FERHRI.Amur.Meta;
using FERHRI.Amur.Data;

namespace ImportMSSQL2Postgres
{
    class Import
    {
        //internal static void Station(string cnnspg, string cnnsms)
        //{
        //    System.Data.SqlClient.SqlConnection cnnms = new SqlConnection(cnnsms);
        //    cnnms.Open();

        //    // SELECT MS SITES -> MAKE PG STATION, SITE PAIRS
        //    List<FERHRI.Amur.Meta.Station> stations = new List<FERHRI.Amur.Meta.Station>();
        //    List<FERHRI.Amur.Meta.Site> sites = new List<FERHRI.Amur.Meta.Site>();
        //    List<string> siteCode = new List<string>();

        //    using (SqlCommand cmdms = new SqlCommand("select * from sites", cnnms))
        //    {
        //        using (SqlDataReader rdrms = cmdms.ExecuteReader())
        //        {
        //            while (rdrms.Read())
        //            {
        //                FERHRI.Amur.Meta.Site site = new FERHRI.Amur.Meta.Site(
        //                    (int)rdrms["SiteId"], (int)rdrms["SiteId"],
        //                    (int)rdrms["SiteTypeId"]);
        //                FERHRI.Amur.Meta.Station station = new FERHRI.Amur.Meta.Station(
        //                            (int)rdrms["SiteId"],
        //                            rdrms["SiteCode"].ToString(),
        //                            rdrms["SiteName"].ToString(),
        //                            site.SiteTypeId);

        //                switch (site.SiteTypeId)
        //                {
        //                    case (int)Enums.SiteType.HydroPost:
        //                        site.SiteTypeId = (int)EnumSiteType.StandartObsLocation;
        //                        station.TypeId = (int)EnumStationType.HydroPost;
        //                        break;
        //                    case (int)Enums.SiteType.GaugingPost:
        //                        site.SiteTypeId = (int)EnumSiteType.StandartObsLocation;
        //                        station.TypeId = (int)EnumStationType.GaugingPost;
        //                        break;
        //                    case (int)Enums.SiteType.HydrometStation:
        //                        site.SiteTypeId = (int)EnumSiteType.StandartObsLocation;
        //                        station.TypeId = (int)EnumStationType.MeteoStation;
        //                        break;
        //                    case (int)Enums.SiteType.AutoHydroSystem:
        //                        site.SiteTypeId = (int)EnumSiteType.AHK;
        //                        //site.StationId = stations.Find(x => x.Code == station.Code).Id;
        //                        station = null;
        //                        break;
        //                    default:
        //                        throw new Exception("switch (site.SiteTypeId) - " + site.SiteTypeId);
        //                }
        //                sites.Add(site);
        //                siteCode.Add(rdrms["SiteCode"].ToString());
        //                if (station != null) stations.Add(station);

        //                Console.WriteLine(site);
        //            }
        //        }
        //    }

        //    // CREATE STATION FOR AHK ONLY SITE
        //    for (int i = 0; i < sites.Count; i++)
        //    {
        //        if (sites[i].SiteTypeId == (int)EnumSiteType.AHK)
        //        {
        //            Station station = stations.Find(x => x.Code == siteCode[i]);
        //            if (station == null)
        //            {
        //                station = new Station(stations.Max(x => x.Id) + 1, siteCode[i], siteCode[i], (int)EnumStationType.AHK);
        //                stations.Add(station);
        //            }
        //            sites[i].StationId = station.Id;
        //        }
        //    }

        //    // INSERT PG STATION, SITE PAIRS
        //    FERHRI.Amur.Meta.StationRepository srp = new StationRepository(cnnspg);
        //    foreach (var item in stations)
        //    {
        //        srp.Insert(item);
        //    }
        //    FERHRI.Amur.Meta.SiteRepository ssrp = new FERHRI.Amur.Meta.SiteRepository(cnnspg);
        //    foreach (var item in sites)
        //    {
        //        ssrp.Insert(item);
        //    }
        //}
        //internal static void Method(string cnnspg, string cnnsms)
        //{
        //    System.Data.SqlClient.SqlConnection cnnms = new SqlConnection(cnnsms);
        //    cnnms.Open();
        //    FERHRI.Amur.Meta.MethodRepository repout = new FERHRI.Amur.Meta.MethodRepository(cnnspg);

        //    // SELECT & INSERT
        //    using (SqlCommand cmdms = new SqlCommand("select * from methods", cnnms))
        //    {
        //        using (SqlDataReader rdr = cmdms.ExecuteReader())
        //        {
        //            while (rdr.Read())
        //            {
        //                FERHRI.Amur.Meta.Method method = new FERHRI.Amur.Meta.Method(
        //                    (int)rdr["MethodID"],
        //                    rdr["MethodDescription"].ToString(),
        //                    2,
        //                    rdr["MethodLink"].ToString()
        //                );
        //                Console.WriteLine(method);

        //                repout.Insert(method);
        //            }
        //        }
        //    }
        //}
        //internal static void GeoType(string cnnspg, string cnnsms)
        //{
        //    System.Data.SqlClient.SqlConnection cnnms = new SqlConnection(cnnsms);
        //    cnnms.Open();
        //    FERHRI.Amur.Meta.GeoTypeRepository repout = new FERHRI.Amur.Meta.GeoTypeRepository(cnnspg);

        //    // SELECT & INSERT
        //    using (SqlCommand cmdms = new SqlCommand("select * from GeoTypes", cnnms))
        //    {
        //        using (SqlDataReader rdr = cmdms.ExecuteReader())
        //        {
        //            while (rdr.Read())
        //            {
        //                FERHRI.Amur.Meta.GeoType item = new FERHRI.Amur.Meta.GeoType(
        //                    (int)rdr["GeoTypeID"],
        //                    rdr["RuName"].ToString(),
        //                    rdr["Term"].ToString(),
        //                    rdr["Definition"].ToString()
        //                );
        //                Console.WriteLine(item);

        //                repout.Insert(item);
        //            }
        //        }
        //    }
        //}
        //internal static void GeoObject(string cnnspg, string cnnsms)
        //{
        //    System.Data.SqlClient.SqlConnection cnnms = new SqlConnection(cnnsms);
        //    cnnms.Open();
        //    FERHRI.Amur.Meta.GeoObjectRepository repout = new FERHRI.Amur.Meta.GeoObjectRepository(cnnspg);

        //    // SELECT & INSERT
        //    using (SqlCommand cmdms = new SqlCommand("select * from WaterObject", cnnms))
        //    {
        //        using (SqlDataReader rdr = cmdms.ExecuteReader())
        //        {
        //            while (rdr.Read())
        //            {
        //                FERHRI.Amur.Meta.GeoObject item = new FERHRI.Amur.Meta.GeoObject(
        //                    (int)rdr["waterObjectID"],
        //                    (int)rdr["geoTypeID"],
        //                    rdr["name"].ToString(),
        //                    (rdr.IsDBNull(rdr.GetOrdinal("fallInto"))) ? null : (int?)(int)rdr["fallInto"],
        //                    (int)rdr["order"]
        //                );

        //                repout.Insert(item);
        //            }
        //        }
        //    }
        //}
        //internal static void SiteGroup(string cnnspg, string cnnsms)
        //{
        //    System.Data.SqlClient.SqlConnection cnnms = new SqlConnection(cnnsms);
        //    cnnms.Open();
        //    //FERHRI.Amur.Meta.SiteGroupRepository repout = new FERHRI.Amur.Meta.SiteGroupRepository(cnnspg);
        //    FERHRI.Amur.Meta.EntityGroupRepository repout = new FERHRI.Amur.Meta.EntityGroupRepository(cnnspg);

        //    // SELECT & INSERT Group0
        //    using (SqlCommand cmdms = new SqlCommand("select * from SiteGroupDescriptions", cnnms))
        //    {
        //        using (SqlDataReader rdr = cmdms.ExecuteReader())
        //        {
        //            while (rdr.Read())
        //            {
        //                FERHRI.Amur.Meta.SiteGroup item = new FERHRI.Amur.Meta.SiteGroup(
        //                    (int)rdr["SiteGroupID"],
        //                    rdr["SiteGroupDescriptions"].ToString()
        //                );
        //                repout.InsertGroup(item.Id, item.Name, "site");
        //            }
        //        }
        //    }
        //    // SELECT & INSERT Group1
        //    using (SqlCommand cmdms = new SqlCommand("select * from SiteGroups", cnnms))
        //    {
        //        using (SqlDataReader rdr = cmdms.ExecuteReader())
        //        {
        //            while (rdr.Read())
        //            {
        //                repout.InsertGroupEntity((int)rdr["SiteGroupID"], (int)rdr["SiteID"]);
        //            }
        //        }
        //    }
        //}

        //internal static void StationGeoObject(string cnnspg, string cnnsms)
        //{
        //    System.Data.SqlClient.SqlConnection cnnms = new SqlConnection(cnnsms);
        //    cnnms.Open();
        //    FERHRI.Amur.Meta.StationGeoObjectRepository repout = new FERHRI.Amur.Meta.StationGeoObjectRepository(cnnspg);
        //    FERHRI.Amur.Meta.StationRepository repout1 = new FERHRI.Amur.Meta.StationRepository(cnnspg);
        //    FERHRI.Amur.Meta.SiteRepository repout2 = new FERHRI.Amur.Meta.SiteRepository(cnnspg);

        //    // SELECT & INSERT
        //    using (SqlCommand cmdms = new SqlCommand("select * from Site_WaterObject", cnnms))
        //    {
        //        using (SqlDataReader rdr = cmdms.ExecuteReader())
        //        {
        //            while (rdr.Read())
        //            {
        //                FERHRI.Amur.Meta.StationGeoObject item = new FERHRI.Amur.Meta.StationGeoObject(
        //                    (int)rdr["siteID"],
        //                    (int)rdr["waterObjectID"],
        //                    (int)rdr["order"]
        //                );
        //                if (repout1.Select(item.StationId) == null)
        //                {
        //                    FERHRI.Amur.Meta.Site site = repout2.Select(item.StationId);
        //                    if (site == null)
        //                        throw new Exception("Отсутствует станция с id = " + item.StationId);
        //                    item.StationId = site.StationId;
        //                }
        //                if (repout.Select(item.StationId, item.GeoObjectId) == null)
        //                    repout.Insert(item);
        //            }
        //        }
        //    }
        //}
        //internal static void Source(string cnnspg, string cnnsms)
        //{
        //    System.Data.SqlClient.SqlConnection cnnms = new SqlConnection(cnnsms);
        //    cnnms.Open();
        //    FERHRI.Amur.Meta.SourceRepository repout = new FERHRI.Amur.Meta.SourceRepository(cnnspg);

        //    // SELECT & INSERT
        //    using (SqlCommand cmdms = new SqlCommand("select * from Sources", cnnms))
        //    {
        //        using (SqlDataReader rdr = cmdms.ExecuteReader())
        //        {
        //            while (rdr.Read())
        //            {
        //                FERHRI.Amur.Meta.Source item = new FERHRI.Amur.Meta.Source(
        //                    (int)rdr["SourceID"],
        //                    rdr["Organization"].ToString(),
        //                    rdr["SourceID"].ToString(),
        //                    rdr["SourceDescription"].ToString()
        //                );
        //                Console.WriteLine(item);

        //                repout.Insert(item);
        //            }
        //        }
        //    }
        //}
        //internal static void DataType(string cnnspg, string cnnsms)
        //{
        //    System.Data.SqlClient.SqlConnection cnnms = new SqlConnection(cnnsms);
        //    cnnms.Open();
        //    FERHRI.Amur.Meta.DataTypeRepository repout = new DataTypeRepository(cnnspg);

        //    // SELECT & INSERT
        //    using (SqlCommand cmdms = new SqlCommand("select * from DataTypeCV", cnnms))
        //    {
        //        using (SqlDataReader rdr = cmdms.ExecuteReader())
        //        {
        //            while (rdr.Read())
        //            {
        //                FERHRI.Amur.Meta.DataType item = new FERHRI.Amur.Meta.DataType(
        //                    (int)rdr["id"],
        //                    rdr["Term"].ToString(),
        //                    rdr["id"].ToString(),
        //                    rdr["Definition"].ToString()
        //                );
        //                Console.WriteLine(item);

        //                repout.Insert(item);
        //            }
        //        }
        //    }
        //}
        //internal static void GeneralCategory(string cnnspg, string cnnsms)
        //{
        //    System.Data.SqlClient.SqlConnection cnnms = new SqlConnection(cnnsms);
        //    cnnms.Open();
        //    FERHRI.Amur.Meta.GeneralCategoryRepository repout = new GeneralCategoryRepository(cnnspg);

        //    // SELECT & INSERT
        //    using (SqlCommand cmdms = new SqlCommand("select * from GeneralCategoryCV", cnnms))
        //    {
        //        using (SqlDataReader rdr = cmdms.ExecuteReader())
        //        {
        //            while (rdr.Read())
        //            {
        //                FERHRI.Amur.Meta.GeneralCategory item = new FERHRI.Amur.Meta.GeneralCategory(
        //                    (int)rdr["id"],
        //                    rdr["Term"].ToString(),
        //                    rdr["id"].ToString(),
        //                    rdr["Definition"].ToString()
        //                );
        //                Console.WriteLine(item);

        //                repout.Insert(item);
        //            }
        //        }
        //    }
        //}
        //internal static void ValueType(string cnnspg, string cnnsms)
        //{
        //    System.Data.SqlClient.SqlConnection cnnms = new SqlConnection(cnnsms);
        //    cnnms.Open();
        //    FERHRI.Amur.Meta.ValueTypeRepository repout = new ValueTypeRepository(cnnspg);

        //    // SELECT & INSERT
        //    using (SqlCommand cmdms = new SqlCommand("select * from ValueTypeCV", cnnms))
        //    {
        //        using (SqlDataReader rdr = cmdms.ExecuteReader())
        //        {
        //            while (rdr.Read())
        //            {
        //                FERHRI.Amur.Meta.ValueType item = new FERHRI.Amur.Meta.ValueType(
        //                    (int)rdr["id"],
        //                    (rdr.IsDBNull(rdr.GetOrdinal("RuName")) || rdr["RuName"].ToString().Trim().Length == 0) ? rdr["Term"].ToString() : rdr["RuName"].ToString(),
        //                    rdr["Term"].ToString(),
        //                    rdr["Definition"].ToString()
        //                );
        //                Console.WriteLine(item);

        //                repout.Insert(item);
        //            }
        //        }
        //    }
        //}
        //internal static void SampleMedium(string cnnspg, string cnnsms)
        //{
        //    System.Data.SqlClient.SqlConnection cnnms = new SqlConnection(cnnsms);
        //    cnnms.Open();
        //    FERHRI.Amur.Meta.SampleMediumRepository repout = new SampleMediumRepository(cnnspg);

        //    // SELECT & INSERT
        //    using (SqlCommand cmdms = new SqlCommand("select * from SampleMediumCV", cnnms))
        //    {
        //        using (SqlDataReader rdr = cmdms.ExecuteReader())
        //        {
        //            while (rdr.Read())
        //            {
        //                FERHRI.Amur.Meta.SampleMedium item = new FERHRI.Amur.Meta.SampleMedium(
        //                    (int)rdr["id"],
        //                    rdr["Term"].ToString(),
        //                    rdr["Definition"].ToString()
        //                );
        //                Console.WriteLine(item);

        //                repout.Insert(item);
        //            }
        //        }
        //    }
        //}
        //internal static void Unit(string cnnspg, string cnnsms)
        //{
        //    System.Data.SqlClient.SqlConnection cnnms = new SqlConnection(cnnsms);
        //    cnnms.Open();
        //    FERHRI.Amur.Meta.UnitRepository repout = new UnitRepository(cnnspg);

        //    // SELECT & INSERT
        //    using (SqlCommand cmdms = new SqlCommand("select * from Units", cnnms))
        //    {
        //        using (SqlDataReader rdr = cmdms.ExecuteReader())
        //        {
        //            while (rdr.Read())
        //            {
        //                string nameRus = rdr.IsDBNull(rdr.GetOrdinal("RuName")) ? null : rdr["RuName"].ToString().Trim();
        //                string abbrRus = rdr.IsDBNull(rdr.GetOrdinal("RuAbbrevation")) ? null : rdr["RuAbbrevation"].ToString().Trim();
        //                string nameEng = rdr["UnitsName"].ToString().Trim();
        //                string abbrEng = rdr["UnitsAbbreviation"].ToString().Trim();

        //                FERHRI.Amur.Meta.Unit item = new FERHRI.Amur.Meta.Unit(
        //                    (int)rdr["UnitsID"],
        //                    rdr["UnitsType"].ToString(),
        //                    string.IsNullOrEmpty(nameRus) ? nameEng : nameRus,
        //                    string.IsNullOrEmpty(abbrRus) ? abbrEng : abbrRus,
        //                    nameEng, abbrEng
        //                );

        //                repout.Insert(item);
        //            }
        //        }
        //    }
        //}
        //internal static void VariableType(string cnnspg, string cnnsms)
        //{
        //    System.Data.SqlClient.SqlConnection cnnms = new SqlConnection(cnnsms);
        //    cnnms.Open();
        //    FERHRI.Amur.Meta.VariableTypeRepository repout = new VariableTypeRepository(cnnspg);

        //    // SELECT & INSERT
        //    using (SqlCommand cmdms = new SqlCommand("select * from VariableNameCV", cnnms))
        //    {
        //        using (SqlDataReader rdr = cmdms.ExecuteReader())
        //        {
        //            while (rdr.Read())
        //            {
        //                string nameRus = rdr.IsDBNull(rdr.GetOrdinal("RuName")) ? null : rdr["RuName"].ToString().Trim();
        //                string nameEng = rdr["Term"].ToString().Trim();
        //                string description = rdr["Definition"].ToString().Trim();

        //                FERHRI.Amur.Meta.VariableType item = new FERHRI.Amur.Meta.VariableType(
        //                    (int)rdr["id"],
        //                    string.IsNullOrEmpty(nameRus) ? nameEng : nameRus,
        //                    rdr["id"].ToString(),
        //                    nameEng,
        //                    description
        //                );

        //                VariableType vt;
        //                if ((vt = repout.Select(item.Id)) == null)
        //                    repout.Insert(item);
        //                else
        //                {
        //                    item.Name = vt.Name;
        //                    item.NameShort = vt.NameShort;
        //                    repout.Update(item);
        //                }
        //            }
        //        }
        //    }
        //}

        internal static void Variable()
        {
            System.Data.SqlClient.SqlConnection cnnms = new SqlConnection(Program.CNNS_MS);
            cnnms.Open();
            FERHRI.Amur.Meta.VariableRepository repout = FERHRI.Amur.Meta.VariableRepository.Instance;

            // SELECT & INSERT
            using (SqlCommand cmdms = new SqlCommand("select * from Variables25", cnnms))
            {
                using (SqlDataReader rdr = cmdms.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        string nameRus = (rdr.IsDBNull(rdr.GetOrdinal("nameRus")) ? rdr["id"].ToString() : rdr["nameRus"].ToString());

                        FERHRI.Amur.Meta.Variable item = new FERHRI.Amur.Meta.Variable(
                            (int)rdr["id"],
                            (int)rdr["variableNameId"],
                            (int)rdr["timeUnitsId"],
                            (int)rdr["variableUnitsId"],
                            (int)rdr["dataTypeId"],
                            (int)rdr["valueTypeId"],
                            (int)rdr["generalCategoryId"],
                            (int)rdr["sampleMediumId"],
                            (int)rdr["TimeSupport"],
                            nameRus
                        );
                        Console.WriteLine("Variable id = " + item.Id + " readed.");

                        repout.InsertWithId(item);
                    }
                }
            }
        }

        //internal static void DataValue(string cnnspg, string cnnsms)
        //{
        //    System.Data.SqlClient.SqlConnection cnnms = new SqlConnection(cnnsms);
        //    cnnms.Open();
        //    FERHRI.Amur.Data.DataValueRepository repout = new DataValueRepository(cnnspg);

        //    // SELECT & INSERT
        //    using (SqlCommand cmdms = new SqlCommand("select * from DataValues25", cnnms))
        //    {
        //        using (SqlDataReader rdr = cmdms.ExecuteReader())
        //        {
        //            int i = 0;
        //            while (rdr.Read())
        //            {
        //                FERHRI.Amur.Data.DataValue item = new FERHRI.Amur.Data.DataValue(
        //                    (int)rdr["ValueId"],
        //                    new Catalog(-1,
        //                        (int)rdr["SiteID"],
        //                        (int)rdr["VariableID"],
        //                        (int)rdr["MethodId"],
        //                        (int)rdr["SourceId"],
        //                        (int)rdr["OffsetTypeID"],
        //                        (double)rdr["OffsetValue"]),
        //                    (double)rdr["DataValue"],
        //                    (DateTime)rdr["LocalDateTime"],
        //                    (byte)rdr["flagAQC"],
        //                    (float)(double)rdr["UTCOffset"]
        //                );
        //                repout.Insert(item);

        //                if (Math.IEEERemainder(i++, 1000) == 0)
        //                    Console.WriteLine(i + " rowsQ imported/");
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// вызывать после заполнения табл DataValue
        /// </summary>
        /// <param name="cnnspg"></param>
        /// <param name="cnnsms"></param>
        //internal static void DataSource(string cnnspg, string cnnsms)
        //{
        //    System.Data.SqlClient.SqlConnection cnnms = new SqlConnection(cnnsms);
        //    cnnms.Open();
        //    FERHRI.Amur.Data.DataValueRepository repout = new DataValueRepository();
        //    FERHRI.Amur.Data.DataSourceRepository repout1 = new DataSourceRepository();
        //    Npgsql.NpgsqlConnection cnn = repout.Connection;
        //    List<long> dsId = new List<long>();

        //    // SELECT & INSERT
        //    using (SqlCommand cmdms = new SqlCommand("SELECT ds.*, dv.ValueId dataValueId FROM [dbo].[DataValues25] dv inner join datasources ds on ds.DataSourceID = dv.DataSourceID", cnnms))
        //    {
        //        int skippedQ = 0;
        //        using (SqlDataReader rdr = cmdms.ExecuteReader())
        //        {
        //            int i = 0;
        //            while (rdr.Read())
        //            {
        //                FERHRI.Amur.Data.DataSource item = new FERHRI.Amur.Data.DataSource(
        //                    (long)(int)rdr["DataSourceID"],
        //                    (int)rdr["SiteID"],
        //                    (int)(byte)rdr["CodeFormID"],
        //                    (DateTime)rdr["DateTimeUTC"],
        //                    (DateTime)rdr["ReceiveDateUTC"],
        //                    (DateTime)rdr["CollectDate"],
        //                    rdr["Value"].ToString(),
        //                    rdr["Hash"].ToString()
        //                );
        //                if (!dsId.Exists(x => x == item.Id))
        //                {
        //                    repout1.InsertWithId(item, cnn);
        //                    dsId.Add(item.Id);
        //                }
        //                try
        //                {
        //                    repout.InsertDataSource((long)(int)rdr["dataValueId"], item.Id, cnn);
        //                }
        //                catch (Exception ex)
        //                {
        //                    Console.WriteLine("skipped " + (++skippedQ));
        //                }
        //                if (Math.IEEERemainder(++i, 1000) == 0)
        //                    Console.WriteLine(i + " rows imported.");
        //            }
        //        }
        //    }
        //}

        internal static void Climate()
        {
            System.Data.SqlClient.SqlConnection cnnms = new SqlConnection(Program.CNNS_MS);
            cnnms.Open();

            List<FERHRI.Amur.Meta.Variable> vars = (new VariableRepository()).Select();
            List<FERHRI.Amur.Data.Catalog> ctls = (new CatalogRepository()).Select();
            List<Catalog> ctl1;
            Catalog ctl;

            #region ИМПОРТ ИЗ ТАБЛИЦ CLIMATE В CLIMATE
            //FERHRI.Amur.Data.ClimateRepository repclm = new ClimateRepository();
            //List<Climate> clms = new List<FERHRI.Amur.Data.Climate>();
            //using (SqlCommand cmdms = new SqlCommand("SELECT * from climate0", cnnms))
            //{
            //    using (SqlDataReader rdr = cmdms.ExecuteReader())
            //    {
            //        while (rdr.Read())
            //        {
            //            clms.Add(new Climate(
            //                (int)rdr["id"],
            //                (int)rdr["siteId"],
            //                (int)rdr["dataTypeId"],
            //                (int)rdr["yearS"],
            //                (int)rdr["yearF"],
            //                (int)rdr["variableId"],
            //                (int)rdr["offsetTypeId"],
            //                (double)rdr["offsetValue"]
            //                ));
            //        }
            //    }
            //}
            //foreach (var clm in clms)
            //{
            //    using (SqlCommand cmdms = new SqlCommand("SELECT * from climate1 where climate0Id = " + clm.Id, cnnms))
            //    {
            //        using (SqlDataReader rdr = cmdms.ExecuteReader())
            //        {
            //            while (rdr.Read())
            //            {
            //                clm.Data.Add((int)rdr["timeNum"], (double)rdr["value"]);
            //            }
            //        }
            //    }
            //    repclm.InsertWithId(clm);
            //}
            #endregion ИМПОРТ ИЗ ТАБЛИЦ CLIMATE

            #region <<<<<< ИМПОРТ ИЗ ТАБЛИЦ CLIMATE В DATA_VALUE >>>>>>

            //List<DataValue> dvs = new List<DataValue>();

            //using (SqlCommand cmdms = new SqlCommand("SELECT * from climate01view where value is not null", cnnms))
            //{
            //    using (SqlDataReader rdr = cmdms.ExecuteReader())
            //    {
            //        while (rdr.Read())
            //        {
            //            FERHRI.Amur.Meta.Variable var = vars.Find(x => x.Id == (int)rdr["variableId"]);
            //            ClimateAttr clma = new ClimateAttr((int)rdr["dataTypeId"], (int)rdr["yearS"]);

            //            ctl1 = ctls.FindAll(x =>
            //               x.MethodId == (int)EnumMethod.Unknown
            //               && x.OffsetTypeId == (int)rdr["offsetTypeId"]
            //               && x.OffsetValue == (double)rdr["offsetValue"]
            //               && x.SiteId == (int)rdr["siteId"]
            //               && x.SourceId == 0
            //               && x.VariableId == var.Id
            //               && x.ClimateAttr != null
            //               && x.ClimateAttr == clma
            //               ); ;
            //            if (ctl1.Count > 1)
            //                throw new Exception("(ctl1.Count != 1)");
            //            if (ctl1.Count == 1)
            //                ctl = ctl1[0];
            //            else
            //            {
            //                ctl = (new CatalogRepository()).Insert(new Catalog(
            //                    -1,
            //                    (int)rdr["siteId"],
            //                    var.Id,
            //                    (int)EnumMethod.Unknown,
            //                    0,
            //                    (int)rdr["offsetTypeId"],
            //                    (double)rdr["offsetValue"],

            //                    clma
            //                ));
            //                ctls.Add(ctl);
            //            }
            //            (new DataValueRepository()).Insert(new DataValue(
            //                -1,
            //                ctl,
            //                (double)rdr["value"],
            //                FERHRI.Amur.Meta.Time.GetDateSTimeNum((int)rdr["yearF"], (int)rdr["timeNum"], var.TimeId),
            //                1,
            //                0));
            //        }
            //    }
            //}
            #endregion ИМПОРТ ИЗ ТАБЛИЦ CLIMATE В DATA_VALUE

            #region <<<<<< ИМПОРТ ИЗ CRITERIAS_HISTORY В DATA_VALUE >>>>>>

            using (SqlCommand cmdms = new SqlCommand("SELECT * from CriteriasView", cnnms))
            {
                using (SqlDataReader rdr = cmdms.ExecuteReader())
                {
                    double[] value = new double[2];

                    while (rdr.Read())
                    {
                        ctl1 = new List<Catalog>();
                        int ct = (int)(byte)rdr["criteriaTypeId"];
                        switch (ct)
                        {
                            #region POYMA, NYa, OYa
                            case 1: // Poyma
                            case 2: // NYa
                            case 3: // OYa
                                if (!rdr.IsDBNull(rdr.GetOrdinal("beginValue")))
                                {
                                    EnumDataType edt = ct == 1 ? EnumDataType.Poyma : ct == 2 ? EnumDataType.NYa : EnumDataType.OYa;
                                    ctl1.Add(new Catalog(-1,
                                        (int)rdr["siteId"],
                                        (int)rdr["variableId"],
                                        0, 0, 0, 0.0,

                                        new ClimateAttr(
                                            (int)edt,
                                            ((DateTime)rdr["beginDate"]).Year,
                                            (int)EnumTime.YearCommon
                                        )
                                    ));
                                    value[0] = (double)rdr["beginValue"];
                                }
                                break;
                            #endregion POYMA

                            #region Max-Min
                            case 10: // Интервал значения
                                if (!rdr.IsDBNull(rdr.GetOrdinal("beginValue")))
                                {
                                    ctl1.Add(new Catalog(-1,
                                        (int)rdr["siteId"],
                                        (int)rdr["variableId"],
                                        0, 0, 0, 0.0,

                                        new ClimateAttr(
                                            (int)EnumDataType.Minimum,
                                            ((DateTime)rdr["beginDate"]).Year,
                                            (int)rdr["timeIdCriteriaValidity"]
                                        )
                                    ));
                                    value[0] = (double)rdr["beginValue"];
                                }
                                if (!rdr.IsDBNull(rdr.GetOrdinal("endValue")))
                                {
                                    ctl1.Add(new Catalog(-1,
                                        (int)rdr["siteId"],
                                        (int)rdr["variableId"],
                                        0, 0, 0, 0.0,

                                        new ClimateAttr(
                                            (int)EnumDataType.Maximum,
                                            ((DateTime)rdr["beginDate"]).Year,
                                            (int)rdr["timeIdCriteriaValidity"]
                                        )
                                    ));
                                    value[0] = (double)rdr["endValue"];
                                }
                                break;
                            #endregion Max-Min

                            #region Tendention
                            case 11: // 1 час
                            case 12: // 12 ч
                            case 13: // 24 ч

                                int timesupport = ct == 11 ? 1 : ct == 12 ? 12 : 24;

                                FERHRI.Amur.Meta.Variable var = vars.Find(x => x.Id == (int)rdr["variableId"]);
                                FERHRI.Amur.Meta.Variable varten = new FERHRI.Amur.Meta.Variable(
                                        -1,
                                        var.VariableTypeId,
                                        (int)EnumTime.Hour,
                                        -1,
                                        (int)EnumDataType.Incremental,
                                        (int)EnumValueType.DerivedValue,
                                        var.GeneralCategoryId,
                                        var.SampleMediumId,
                                        timesupport,
                                        ""
                                );
                                List<FERHRI.Amur.Meta.Variable> vars1 = vars.FindAll(x =>
                                    x.VariableTypeId == varten.VariableTypeId
                                    && x.ValueTypeId == varten.ValueTypeId
                                        //&& x.UnitId = var.UnitId
                                    && x.TimeSupport == varten.TimeSupport
                                    && x.TimeId == varten.TimeId
                                    && x.SampleMediumId == varten.SampleMediumId
                                    && x.GeneralCategoryId == varten.GeneralCategoryId
                                    && x.DataTypeId == varten.DataTypeId
                                );
                                if (vars1.Count > 1)
                                    throw new Exception("(vars.Count)>1");
                                if (vars1.Count == 0)
                                    throw new Exception(
                                        "Отсутствует переменная для тенденции за " + timesupport
                                        + " часов для переменной " + var.ToStringWithFKNames()
                                        + "\nИскомая переменная " + varten.ToStringWithFKNames());
                                var = vars1[0];

                                ctl1.Add(new Catalog(-1,
                                    (int)rdr["siteId"],
                                    var.Id,
                                    0, 0, 0, 0.0,

                                    new ClimateAttr(
                                        (int)EnumDataType.Maximum,
                                        ((DateTime)rdr["beginDate"]).Year,
                                        (int)rdr["timeIdCriteriaValidity"]
                                    )
                                ));
                                value[0] = (double)rdr["endValue"];

                                break;
                            #endregion Tendention

                            case 14: break; // Разница ГП - АГК     - записан в атрибуты поста
                            case 15: break; // Поправка на значение - записан в атрибуты поста

                            default:
                                throw new Exception("Неизвестный критерий " + (int)rdr["criteriaTypeId"]);
                        }
                        #region WRITE CATALOG & DATAVALUE

                        for (int i = 0; i < ctl1.Count; i++)
                        {
                            // GET || INSERT & GET CATALOG
                            List<Catalog> ctl2 = ctls.FindAll(x => x == ctl1[i]);
                            if (ctl2.Count > 1) throw new Exception("(ctl.Count != 1)");
                            if (ctl2.Count == 0)
                            {
                                ctl1[i] = (new CatalogRepository()).Insert(ctl1[i]);
                                ctls.Add(ctl1[i]);
                            }
                            else
                                ctl1[i] = ctl2[0];

                            // GET HYDROSEASON MONTHES
                            List<EntityAttrValue> savs = (new EntityAttrRepository("site"))
                                .SelectAttrValues(ctl1[i].SiteId, (int)EnumSiteAttrType.HydroSeasonMonthStart, null);
                            int[] hydroSeasonMonthStart = null;
                            if (savs.Count > 0 && savs[0].Value != null)
                            {
                                savs[0].Value = (savs[0].Value[savs[0].Value.Length - 1] == ';')
                                    ? savs[0].Value.Substring(0, savs[0].Value.Length - 1)
                                    : savs[0].Value;
                                hydroSeasonMonthStart = FERHRI.Common.StrVia.ToListInt(savs[0].Value, ";").ToArray();
                            }

                            // GET DATE
                            DateTime date = FERHRI.Amur.Meta.Time.GetDateSTimeNum(
                                ctl1[i].ClimateAttr.YearS,
                                (int)rdr["timeNumCriteriaValidity"],
                                (int)ctl1[i].ClimateAttr.ValueTimeslotId,
                                hydroSeasonMonthStart
                            );
                            date = new DateTime(ctl1[i].ClimateAttr.YearS, date.Month, date.Day, date.Hour, date.Minute, date.Second);

                            // INSERT DATAVALUE
                            (new DataValueRepository()).Insert(new DataValue(
                                    -1,
                                    ctl1[i],
                                    value[i],
                                    date,
                                    1, 0
                                )
                            );
                        }
                        #endregion WRITE CATALOG & DATAVALUE
                    }
                }
            }
            #endregion ИМПОРТ ИЗ CRITERIAS_HISTORY В DATA_VALUE
        }

        internal static void SiteAttr()
        {
            System.Data.SqlClient.SqlConnection cnnms = new SqlConnection(Program.CNNS_MS);
            cnnms.Open();
            List<EntityAttr> attrs = (EntityAttrRepository.Instance("site")).SelectAttrTypes();

            using (SqlCommand cmdms = new SqlCommand("SELECT * from SiteAttributeView", cnnms))
            {
                using (SqlDataReader rdr = cmdms.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        if (attrs.Find(x => x.Id == (Int16)rdr["attributeTypeID"]) == null) continue;

                        EntityAttrValue sav = new EntityAttrValue(
                            (int)rdr["siteID"],
                            (Int16)rdr["attributeTypeID"],
                            (DateTime)rdr["beginDate"],
                            rdr["value"].ToString()
                        );

                        (new EntityAttrRepository("site")).InsertUpdateValue(
                            sav.EntityId, sav.AttrTypeId, (DateTime)sav.DateS, sav.Value);
                    }
                }
            }
        }
        internal static void Categories()
        {
            System.Data.SqlClient.SqlConnection cnnms = new SqlConnection(Program.CNNS_MS);
            cnnms.Open();

            using (SqlCommand cmdms = new SqlCommand("SELECT * from Categories", cnnms))
            {
                using (SqlDataReader rdr = cmdms.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        VariableCode var = new VariableCode(
                            (int)rdr["VariableId"],
                            (int)rdr["DataValue"],
                            rdr["CategoryDescription"].ToString(),
                            (rdr.IsDBNull(rdr.GetOrdinal("ShortName"))) ? null : rdr["ShortName"].ToString(),
                            (rdr.IsDBNull(rdr.GetOrdinal("FullName"))) ? null : rdr["FullName"].ToString()
                        );

                        FERHRI.Amur.Meta.VariableCodeRepository.Instance.Insert(var);
                    }
                }
            }
        }
    }
}
