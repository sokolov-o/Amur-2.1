using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Common;
using Npgsql;

namespace SOV.Amur.Meta
{
    public class VariableCodeRepository : BaseRepository<VariableCode>
    {
        Common.ADbNpgsql _db;
        internal VariableCodeRepository(Common.ADbNpgsql db) : base(db, "meta.variable_code")
        {
            _db = db;
        }

        public new List<VariableCode> Select(int variableId)
        {
            return Select(new List<int>(new int[] { variableId }));
        }
        public new List<VariableCode> Select(List<int> variableIds = null)
        {
            List<VariableCode> ret = new List<VariableCode>();

            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select * from meta.variable_code"
                + ((variableIds != null && variableIds.Count > 0) ? " where variable_id = ANY (:variable_ids)" : ""), cnn))
                {
                    cmd.Parameters.AddWithValue("variable_ids", variableIds);
                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ret.Add(new VariableCode(
                               (int)rdr["variable_id"],
                               (int)rdr["code"],
                               rdr["name"].ToString(),
                               (rdr.IsDBNull(rdr.GetOrdinal("name_short"))) ? null : rdr["name_short"].ToString(),
                               (rdr.IsDBNull(rdr.GetOrdinal("description"))) ? null : rdr["description"].ToString()
                           ));
                        }
                        return ret;
                    }
                }
            }
        }
        //public void Insert(VariableCode varCode)
        //{
        //    using (NpgsqlConnection cnn = _db.Connection)
        //    {
        //        using (NpgsqlCommand cmd = new NpgsqlCommand(
        //            "insert into meta.variable_code"
        //            + " (variable_id, code, name, name_short, description)"
        //            + " values (:variable_id, :code, :name, :name_short, :description)", cnn))
        //        {
        //            cmd.Parameters.AddWithValue("variable_id", varCode.VariableId);
        //            cmd.Parameters.AddWithValue("code", varCode.Code);
        //            cmd.Parameters.AddWithValue("name", varCode.Name);
        //            cmd.Parameters.AddWithValue("name_short", varCode.NameShort);
        //            cmd.Parameters.AddWithValue("description", varCode.Description);

        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //}
        public void Insert(VariableCode item)
        {
            var fields = new Dictionary<string, object>()
            {
                {"variable_id", item.VariableId},
                {"code", item.Code},
                {"name", item.Name},
                {"name_short", item.NameShort},
                {"description", item.Description}
            };
            Insert(fields);
        }

        public void Update(VariableCode item)
        {
            if (Select(item.VariableId).Exists(x => x.Code == item.Code))
            {
                var fields = new Dictionary<string, object>()
                {
                    {"variable_id", item.VariableId},
                    {"code", item.Code},
                    {"name", item.Name},
                    {"name_short", item.NameShort},
                    {"description", item.Description}
                };
                Update(new List<Dictionary<string, object>>() { fields }, new List<string>() { "variable_id", "code" });
            }
            else
                Insert(item);
        }

        public void Delete(VariableCode item)
        {
            var fields = new Dictionary<string, object>()
                {
                    {"variable_id", item.VariableId},
                    {"code", item.Code},
                    {"name", item.Name},
                    {"name_short", item.NameShort},
                    {"description", item.Description}
                };
            ExecSimpleQuery("delete from " + base.TableName + " where variable_id=:variable_id and code=:code", new List<Dictionary<string, object>>() { fields });
        }
    }
}
