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
    public class SiteTypeRepository : BaseRepository<SiteType>
    {
        internal SiteTypeRepository(Common.ADbNpgsql db) : base(db, "meta.site_type") { }

        public static List<SiteType> GetCash() { return GetCash(DataManager.GetInstance().SiteTypeRepository); }

        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return new SiteType(
                (int)(long)rdr["id"],
                rdr["name"].ToString(),
                rdr["name_short"].ToString()
                );
        }
    }
}
