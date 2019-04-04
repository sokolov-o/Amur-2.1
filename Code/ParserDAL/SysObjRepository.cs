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
    public class SysObjRepository
    {
        Common.ADbNpgsql _db;
        internal SysObjRepository(Common.ADbNpgsql db)
        {
            _db = db;
        }

        public SysObj Select(int sysObjId)
        {
            List<SysObj> ret = Select(new List<int>(new int[] { sysObjId }));
            return ret.Count == 0 ? null : ret[0];
        }
        /// <summary>
        /// Выборка парсер-объектов
        /// </summary>
        /// <param name="sysObjIds">Коды объектов или все, если null.</param>
        /// <returns></returns>
        public List<SysObj> Select(List<int> sysObjIds = null)
        {
            List<SysObj> ret = new List<SysObj>();

            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select * from parser.sysobj where :ids is null or id = any(:ids)", cnn))
                {
                    cmd.Parameters.AddWithValue("ids", sysObjIds);
                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ret.Add(new SysObj()
                            {
                                Id = (int)rdr["id"],
                                SysObjTypeId = (int)rdr["sysobjtypeid"],
                                Name = rdr["name"].ToString(),
                                Heap = (rdr.IsDBNull(rdr.GetOrdinal("heap"))) ? null : rdr["heap"].ToString(),
                                Notes = (rdr.IsDBNull(rdr.GetOrdinal("notes"))) ? null : rdr["notes"].ToString(),
                                LastStartParam = (rdr.IsDBNull(rdr.GetOrdinal("laststartparam"))) ? null : rdr["laststartparam"].ToString()
                            });
                        }
                    }
                }
            }
            return ret;
        }
        public void UpdateLastStartParam(int sysObjId, string lastStartParam)
        {
            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("update parser.sysobj set  lastStartParam=:lastStartParam where id=:id", cnn))
                {
                    cmd.Parameters.AddWithValue("id", sysObjId);
                    cmd.Parameters.AddWithValue("lastStartParam", lastStartParam);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
