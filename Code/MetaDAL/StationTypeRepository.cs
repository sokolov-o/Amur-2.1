using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using FERHRI.Common;
using Npgsql;

namespace FERHRI.Amur.Meta
{
    public class StationTypeRepository : BaseRepository<SiteType>
    {
        internal StationTypeRepository(Common.ADbNpgsql db) : base(db, "meta.station_type") { }

        public static List<SiteType> GetCash() { return GetCash(DataManager.GetInstance().StationTypeRepository); }

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
