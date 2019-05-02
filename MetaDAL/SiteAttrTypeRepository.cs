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
    public class SiteAttrTypeRepository
    {
        Common.ADbNpgsql _db;
        internal SiteAttrTypeRepository(Common.ADbNpgsql db)
        {
            _db = db;
        }
        /// <summary>
        /// Выбрать все типы атрибутов сайтов.
        /// </summary>
        /// <returns></returns>
        public List<SiteAttrType> Select()
        {
            List<SiteAttrType> ret = new List<SiteAttrType>();

            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select * from meta.site_attr_type", cnn))
                {
                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ret.Add(new SiteAttrType(
                                (int)rdr["id"],
                                rdr["name"].ToString(),
                                rdr["site_type_id_mandatory"].ToString())
                            );
                        }
                    }
                }
            }
            return ret;
        }
    }
}
