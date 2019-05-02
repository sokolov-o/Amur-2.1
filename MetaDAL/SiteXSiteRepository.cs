using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using SOV.Common;
using Npgsql;

namespace SOV.Amur.Meta
{
    public class SiteXSiteRepository
    {
        Common.ADbNpgsql _db;
        internal SiteXSiteRepository(Common.ADbNpgsql db)
        {
            _db = db;
        }
        /// <summary>
        /// Выбрать связи по коду второго пункта (site2Id).
        /// </summary>
        public List<SiteXSite> SelectBySite2(int site2Id, int? siteXSiteTypeId = null)
        {
            return Select(site2Id, 2, siteXSiteTypeId);
        }
        /// <summary>
        /// Выбрать связи по коду первого пункта (site1Id).
        /// </summary>
        public List<SiteXSite> SelectBySite1(int site1Id, int? siteXSiteTypeId = null)
        {
            return Select(site1Id, 1, siteXSiteTypeId);
        }
        /// <summary>
        /// Выбрать связи.
        /// </summary>
        public List<SiteXSite> Select(int siteId, int siteNum1Or2, int? siteXSiteTypeId = null)
        {
            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select * from meta.site_x_site"
                    + " where ((site_id1 = :site_id and :site_num = 1) or (site_id2 = :site_id and :site_num = 2)) and (:site_x_site_type_id is null or site_x_site_type_id=:site_x_site_type_id)", cnn))
                {
                    cmd.Parameters.AddWithValue("site_id", siteId);
                    //cmd.Parameters.AddWithValue("site_x_site_type_id", siteXSiteTypeId);
                    cmd.Parameters.Add(new NpgsqlParameter("site_x_site_type_id", NpgsqlTypes.NpgsqlDbType.Integer) { IsNullable = true, Value = siteXSiteTypeId.HasValue ? (object)siteXSiteTypeId : DBNull.Value });
                    //cmd.Parameters.Add(ADbNpgsql.GetParameter("site_x_site_type_id", NpgsqlTypes.NpgsqlDbType.Integer, siteXSiteTypeId));
                    cmd.Parameters.AddWithValue("site_num", siteNum1Or2);
                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        List<SiteXSite> ret = new List<SiteXSite>();
                        while (rdr.Read())
                        {
                            ret.Add(new SiteXSite()
                            {
                                SiteId1 = (int)rdr["site_id1"],
                                SiteId2 = (int)rdr["site_id2"],
                                RelationTypeId = (int)rdr["site_x_site_type_id"]
                            });
                        }
                        return ret;
                    }
                }
            }
        }
        public List<SiteXSiteType> SelectRelationTypes()
        {
            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select id, name from meta.site_x_site_type", cnn))
                {
                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        List<SiteXSiteType> ret = new List<SiteXSiteType>();
                        while (rdr.Read())
                        {
                            ret.Add(new SiteXSiteType((int)rdr[0], rdr[1].ToString()));
                        }
                        return ret;
                    }
                }
            }
        }
    }
}
