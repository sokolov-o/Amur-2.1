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
    public class OffsetTypeRepository : BaseRepository<OffsetType>
    {
        internal OffsetTypeRepository(Common.ADbNpgsql db)
            : base(db, "meta.offset_type")
        {
        }

        public static List<OffsetType> GetCash()
        {
            return GetCash(DataManager.GetInstance().OffsetTypeRepository);
        }
        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return new OffsetType(
                                (int)rdr["id"],
                                rdr["name"].ToString(),
                                (int)rdr["unit_id"]
                            );
        }
    }
}
