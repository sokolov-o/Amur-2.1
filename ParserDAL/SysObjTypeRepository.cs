using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using SOV.Common;
using Npgsql;

namespace SOV.Amur.Parser
{
    public class SysObjTypeRepository
    {
        Common.ADbNpgsql _db;
        internal SysObjTypeRepository(Common.ADbNpgsql db)
        {
            _db = db;
        }

        /// <summary>
        /// Выборка всех типов парсер-объектов
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, string> Select()
        {
            Dictionary<int, string> ret = new Dictionary<int, string>();

            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select id, name from parser.sysObjType", cnn))
                {
                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ret.Add((int)rdr[0], rdr[1].ToString());
                        }
                    }
                }
            }
            return ret;
        }
    }
}
