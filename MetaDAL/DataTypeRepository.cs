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
    public class DataTypeRepository : BaseRepository<DataType>
    {
        internal DataTypeRepository(Common.ADbNpgsql db)
            : base(db, "meta.data_type")
        {
            _db = db;
        }
        public static List<DataType> GetCash()
        {
            return GetCash(DataManager.GetInstance().DataTypeRepository);
        }
        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return new DataType
                (
                    (int)rdr["id"],
                    rdr["name"].ToString(),
                    (rdr.IsDBNull(rdr.GetOrdinal("name_short"))) ? null : rdr["name_short"].ToString(),
                    (rdr.IsDBNull(rdr.GetOrdinal("description"))) ? null : rdr["description"].ToString()
                );
        }
        public void Insert(DataType item)
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
