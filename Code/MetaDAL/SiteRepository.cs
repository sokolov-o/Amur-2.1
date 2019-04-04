using System;
using System.Collections.Generic;
using System.Linq;
using SOV.Common;
using Npgsql;

namespace SOV.Amur.Meta
{
    public class SiteRepository : BaseRepository<Site>
    {
        internal SiteRepository(Common.ADbNpgsql db) : base(db, "meta.site") { }

        public static List<Site> GetCash()
        {
            return GetCash(Meta.DataManager.GetInstance().SiteRepository);
        }
        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return new Site(
                 (int)rdr["id"],
                 (int)rdr["station_id"],
                 (int)rdr["site_type_id"],
                 ADbNpgsql.GetValueString(rdr, "code"),
                 ADbNpgsql.GetValueString(rdr, "description")
             );
        }

        /// <summary>
        /// Создать пункт.
        /// </summary>
        /// <param name="site"></param>
        /// <returns>New id.</returns>
        public int Insert(Site site)
        {
            var fields = new Dictionary<string, object>()
            {
                {"station_id", site.StationId},
                {"site_type_id", site.TypeId},
                {"code", site.Code},
                {"description", site.Description}
            };
            return InsertWithReturn(fields);
        }
        /// <summary>
        /// Изменить тип, привязку к станции и описание пункта.
        /// </summary>
        /// <param name="site">Изменяемый пункт.</param>
        /// <returns></returns>
        public void Update(Site site)
        {
            var fields = new Dictionary<string, object>()
            {
                {"id", site.Id},
                {"station_id", site.StationId},
                {"site_type_id", site.TypeId},
                {"code", site.Code},
                {"description", site.Description}
            };
            Update(fields);
        }

        /// <summary>
        /// Выборка пункта стандартных, основных наблюдений.
        /// </summary>
        /// <param name="autoSite">Не основной пункт неблюдений, например АГК, АМС.</param>
        /// <returns></returns>
        public Site SelectReferenceSite(Site autoSite)
        {
            List<Site> sites = Select(autoSite.StationId, (int)EnumStationType.MeteoStation);
            if (sites.Count > 1)
                throw new Exception("Обнаружено более одного основного/ссылочного сайта для сайта " + autoSite);
            if (sites.Count == 1) return sites[0];

            sites = Select(autoSite.StationId, (int)EnumStationType.HydroPost);
            if (sites.Count > 1)
                throw new Exception("Обнаружено более одного основного/ссылочного сайта для сайта " + autoSite);
            if (sites.Count == 1) return sites[0];

            //SiteType st = DataManager.GetInstance(db.ConnectionString).SiteTypeRepository.Select(autoSite.SiteTypeId)[0];
            //if (st.RefSiteTypeId.HasValue)
            //{
            //    List<Site> sites = Select(autoSite.StationId, (int)st.RefSiteTypeId);
            //    if (sites.Count > 1)
            //        throw new Exception("Обнаружено более одного основного/ссылочного сайта для сайта " + autoSite);
            //    return sites[0];
            //}
            return null;
        }

        public virtual List<Site> Select(int stationId, int? siteTypeId)
        {
            var fields = new Dictionary<string, object>() { { "station_id", stationId } };
            if (siteTypeId.HasValue)
                fields.Add("site_type_id", siteTypeId.Value);
            return Select(fields);
        }

        private object ParseSiteView(NpgsqlDataReader reader)
        {
            return new Site(
                (int)reader["id"],
                (int)reader["station_id"],
                (int)reader["site_type_id"],
                ADbNpgsql.GetValueString(reader, "site_code"),
                ADbNpgsql.GetValueString(reader, "site_description")
            );
        }

        public List<Site> Select(SiteFilter siteFilter)
        {
            var fields = new Dictionary<string, object>()
            {
                { "station_type_id", siteFilter.StationTypeId },
                { "site_type_id", siteFilter.SiteTypeId },
                { "station_code", siteFilter.StationCodeLike  },
                { "station_name", siteFilter.StationNameLike },
                //{ "station_code", siteFilter.StationCodeLike ?? "" },
                //{ "station_name", siteFilter.StationNameLike ?? "" },
                { "addr_region_id", siteFilter.AddrId },
                { "org_id", siteFilter.OrgId }
            };
            string sql = "Select * From meta.site_view " +
                         QueryBuilder.Where(fields);
            return ExecQuery<Site>(sql, fields, ParseSiteView);
        }
        public List<Site> SelectByType(int siteTypeId)
        {
            return SelectByType(new List<int>(new int[] { siteTypeId }));
        }
        public virtual List<Site> SelectByType(List<int> siteTypeIds)
        {
            var fields = new Dictionary<string, object>() { { "site_type_id", siteTypeIds } };
            return Select(fields);
        }
        public virtual List<Site> SelectByParents(List<int> parentIds)
        {
            var fields = new Dictionary<string, object>() { { "parent_id", parentIds } };
            return Select(fields);
        }
        public List<Site> SelectByTypes(List<int> siteTypeIds)
        {
            var fields = new Dictionary<string, object>()
            {
                {"site_type_id", siteTypeIds}
            };
            return Select(fields);
        }
        public List<Site> SelectInBox(double south, double north, double west, double east)
        {
            var fields = new Dictionary<string, object>() { { "south", south }, { "north", north }, { "west", west }, { "east", east } };
            string sql = "Select * From meta.select_site_with_actual_attr" +
                         "(null::integer[], now()::timestamp) " +
                         "Where lat between :south and :north and lon between :west and :east";
            return ExecQuery<Site>(sql, fields);
        }
        /// <summary>
        /// Выборка всех пунктов связанных с данной станцией
        /// </summary>
        /// <param name="station_id"></param>
        /// <returns></returns>
        public List<Site> SelectRelated(int station_id)
        {
            var fields = new Dictionary<string, object>() { { "_station_id", station_id } };
            string sql = "meta.select_related_sites";
            return ExecQuery<Site>(sql, fields, ParseData, System.Data.CommandType.StoredProcedure);
        }

        public List<Site> SelectByIndeces(List<string> siteIndices)
        {
            return Select(new Dictionary<string, object>()
                {
                    {"code", siteIndices}
                }
            );
        }
        public List<Site> SelectByAddrRegionIds(List<int> addrRegionIds)
        {
            return Select(new Dictionary<string, object>()
                {
                    {"addr_region_id", addrRegionIds}
                }
            );

        }
    }
}
