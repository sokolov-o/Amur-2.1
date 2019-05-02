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
    public class CodeFormRepository 
    {
        Common.ADbNpgsql _db;
        internal CodeFormRepository(Common.ADbNpgsql db)
        {
            _db = db;
        }
        public List<CodeForm> Select(List<int> sourceIds = null)
        {
            List<CodeForm> ret = new List<CodeForm>();

            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select * from meta.code_form"
                + ((sourceIds != null && sourceIds.Count > 0) ? " where id in (" + StrVia.ToString(sourceIds) + ")" : ""), cnn))
                {
                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ret.Add(new CodeForm(
                                (int)rdr["id"],
                                rdr["name"].ToString()
                            ));
                        }
                    }
                }
            }
            return ret;
        }
    }
}
