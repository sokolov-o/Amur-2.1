using System;
using System.Collections.Generic;
using System.Linq;
using SOV.Common;
using Npgsql;

namespace SOV.Amur.Meta
{
    public class SiteNewRepository : BaseRepository<SiteNew>
    {
        internal SiteNewRepository(Common.ADbNpgsql db) : base(db, "meta.site_new") { }

        public static List<SiteNew> GetCash()
        {
            return GetCash(Meta.DataManager.GetInstance().SiteNewRepository);
        }
        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return new SiteNew
            {
                Id = (int)rdr["id"],
                Code = ADbNpgsql.GetValueString(rdr, "code"),
                Name = ADbNpgsql.GetValueString(rdr, "name"),

                SiteTypeId = (int)rdr["site_type_id"],
                OrgId = ADbNpgsql.GetValueInt(rdr, "org_id"),
                AddrRegionId = ADbNpgsql.GetValueInt(rdr, "addr_region_id"),
                Description = ADbNpgsql.GetValueString(rdr, "description"),
                ParentId = ADbNpgsql.GetValueInt(rdr, "parent_id")
                // TODO: parse lat, lon
            };
        }

        /// <summary>
        /// Создать пункт.
        /// </summary>
        /// <param name="site"></param>
        /// <returns>New id.</returns>
        public int Insert(SiteNew site)
        {
            var fields = new Dictionary<string, object>()
            {
                {"code", site.Code},
                {"name", site.Name},
                {"site_type_id", site.SiteTypeId},
                {"org_id", site.OrgId},
                {"addr_region_id", site.AddrRegionId},
                {"parent_id", site.ParentId},
                {"description", site.Description}
            };
            return InsertWithReturn(fields);
        }
        /// <summary>
        /// Изменить тип, привязку к станции и описание пункта.
        /// </summary>
        /// <param name="site">Изменяемый пункт.</param>
        /// <returns></returns>
        public void Update(SiteNew site)
        {
            var fields = new Dictionary<string, object>()
            {
                {"id", site.Id},
                {"code", site.Code},
                {"name", site.Name},
                {"site_type_id", site.SiteTypeId},
                {"org_id", site.OrgId},
                {"addr_region_id", site.AddrRegionId},
                {"parent_id", site.ParentId},
                {"description", site.Description}
            };
            Update(fields);
        }

        public List<SiteNew> Select(SiteNewFilter siteFilter)
        {
            var fields = new Dictionary<string, object>()
            {
                { "code", siteFilter.CodeLike},
                { "name", siteFilter.NameLike},
                { "site_type_id", siteFilter.SiteTypeId },
                { "addr_region_id", siteFilter.AddrId },
                { "org_id", siteFilter.OrgId }
            };
            string sql = "Select * from meta.site " + QueryBuilder.Where(fields);
            return ExecQuery<SiteNew>(sql, fields);
        }
        public List<SiteNew> SelectByType(int siteTypeId)
        {
            return SelectByType(new List<int>(new int[] { siteTypeId }));
        }
        public virtual List<SiteNew> SelectByType(List<int> siteTypeIds)
        {
            var fields = new Dictionary<string, object>() { { "site_type_id", siteTypeIds } };
            return Select(fields);
        }
        public List<SiteNew> SelectExtent(double south, double north, double west, double east)
        {
            //TODO: Доработать 31.01.2019
            var fields = new Dictionary<string, object>() { { "south", south }, { "north", north }, { "west", west }, { "east", east } };
            string sql = "Select * From meta.select_site_with_actual_attr" +
                         "(null::integer[], now()::timestamp) " +
                         "Where lat between :south and :north and lon between :west and :east";
            return ExecQuery<SiteNew>(sql, fields);
        }
    }
}
