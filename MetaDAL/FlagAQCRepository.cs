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
    public class FlaAQCRepository
    {
        Common.ADbNpgsql _db;
        internal FlaAQCRepository(Common.ADbNpgsql db)
        {
            _db = db;
        }
        public Dictionary<short, string[/*name, name_short*/]> Select()
        {
            Dictionary<short, string[/*name, name_short*/]> ret = new Dictionary<short, string[/*name, name_short*/]>();

            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select flag_aqc, name, name_short from meta.flag_aqc", cnn))
                {
                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ret.Add((short)rdr["id"], new string[] { rdr["name"].ToString(), rdr["name_short"].ToString() });
                        }
                    }
                }
            }
            return ret;
        }
    }
}
