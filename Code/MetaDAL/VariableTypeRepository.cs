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
    public class VariableTypeRepository : BaseRepository<VariableType>
    {
        internal VariableTypeRepository(Common.ADbNpgsql db)
            : base(db, "meta.variable_type")
        {
        }

        public static List<VariableType> GetCash()
        {
            return GetCash(DataManager.GetInstance().VariableTypeRepository);
        }
        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return new VariableType
                (
                (int)rdr["id"],
                rdr["name"].ToString(),
                rdr["name_short"].ToString(),
                rdr["name_eng"].ToString(),
                (rdr.IsDBNull(rdr.GetOrdinal("description"))) ? null : rdr["Description"].ToString()
                );
        }

        public void Insert(VariableType item)
        {
            var fields = new Dictionary<string, object>()
            {
                {"id", item.Id},
                {"name", item.Name},
                {"name_short", item.NameShort},
                {"name_eng", item.NameEng},
                {"description", item.Description}
            };
            Insert(fields);
        }

        //public List<VariableType> Select(List<int> id = null)
        //{
        //    List<VariableType> ret = new List<VariableType>();
        //    using (NpgsqlConnection cnn = _db.Connection)
        //    {
        //        using (NpgsqlCommand cmd = new NpgsqlCommand("select * from meta.variable_type"
        //            + ((id == null) ? "" : " where id in (" + StrVia.ToString(id) + ")"), cnn))
        //        {
        //            using (NpgsqlDataReader rdr = cmd.ExecuteReader())
        //            {
        //                while (rdr.Read())
        //                {
        //                    ret.Add((VariableType)ParseData(rdr));
        //                }
        //                return ret;
        //            }
        //        }
        //    }
        //}

        public void Update(VariableType item)
        {
            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("update meta.\"VariableType\" set" +
                                "\"name\" = ?, \"nameShort\" = ?, \"nameEng\" = ?, \"description\" = ?"
                                + " where \"id\" = ?", cnn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("?", item.Name);
                    cmd.Parameters.AddWithValue("?", item.NameShort);
                    cmd.Parameters.AddWithValue("?", item.NameEng);
                    cmd.Parameters.AddWithValue("?", item.Description);
                    cmd.Parameters.AddWithValue("?", item.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
