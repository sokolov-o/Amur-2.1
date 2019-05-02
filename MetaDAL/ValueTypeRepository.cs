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
    public class ValueTypeRepository : BaseRepository<ValueType>
    {
        internal ValueTypeRepository(Common.ADbNpgsql db)
            : base(db, "meta.value_type")
        {
        }
        public static List<ValueType> GetCash()
        {
            return GetCash(DataManager.GetInstance().ValueTypeRepository);
        }
        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return new ValueType
                (
                    (int)rdr["id"],
                    rdr["name"].ToString(),
                    (rdr.IsDBNull(rdr.GetOrdinal("name_short"))) ? null : rdr["name_short"].ToString(),
                    (rdr.IsDBNull(rdr.GetOrdinal("description"))) ? null : rdr["description"].ToString()
                );
        }

        public void Insert(ValueType item)
        {
            var fields = new Dictionary<string, object>()
            {
                {"id", item.Id},
                {"name", item.Name},
                {"name_short", item.NameShort},
                {"description", item.Description}
            };
            Insert(fields);
        }
    }
}
