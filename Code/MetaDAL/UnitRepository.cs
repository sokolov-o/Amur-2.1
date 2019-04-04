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
    public class UnitRepository : BaseRepository<Unit>
    {
        internal UnitRepository(Common.ADbNpgsql db)
            : base(db, "meta.unit")
        {
        }
        public static List<Unit> GetCash()
        {
            return GetCash(DataManager.GetInstance().UnitRepository);
        }
        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return new Unit
                (
                (int)rdr["id"],
                rdr["type"].ToString(),
                rdr["name"].ToString(),
                rdr["abbr"].ToString(),
                rdr["name_eng"].ToString(),
                rdr["abbr_eng"].ToString(),
                rdr.IsDBNull(rdr.GetOrdinal("si_convertion")) ? null : (double?)(double)rdr["si_convertion"]
                );
        }

        public void Insert(Unit item)
        {
            var fields = new Dictionary<string, object>()
            {
                {"id", item.Id},
                {"name", item.Name},
                {"abbr", item.Abbreviation},
                {"name_eng", item.NameEng},
                {"abbr_eng", item.AbbreviationEng},
                {"type", item.Type},
                {"si_convertion", item.SIConvertion}
            };
            Insert(fields);
        }
        public List<Unit> Select(List<int> id = null, string type = null)
        {
            var fields = new Dictionary<string, object>()
            {
                {"id", id},
                {"type", type}
            };
            return Select(fields);

            //List<Unit> ret = new List<Unit>();

            //using (NpgsqlConnection cnn = _db.Connection)
            //{
            //    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from meta.unit"
            //        + " where (:id is null or id = any(:id)) or (:type is null or type = :type)", cnn))
            //    {
            //        cmd.Parameters.AddWithValue("id", (object)id ?? DBNull.Value);
            //        cmd.Parameters.AddWithValue("type", (object)type ?? DBNull.Value);

            //        using (NpgsqlDataReader rdr = cmd.ExecuteReader())
            //        {
            //            while (rdr.Read())
            //            {
            //                ret.Add((Unit)ParseData(rdr));
            //            }
            //        }
            //    }
            //}
            //return ret;
        }
    }
}
