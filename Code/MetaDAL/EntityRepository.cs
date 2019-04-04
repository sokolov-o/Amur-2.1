using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace SOV.Amur.Meta
{
    public class EntityRepository
    {
        Common.ADbNpgsql _db;
        internal EntityRepository(Common.ADbNpgsql db)
        {
            _db = db;
        }
        public List<Entity> SelectEntityes()
        {
            List<Entity> ret = new List<Entity>();

            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select * from meta.entity", cnn))
                {
                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ret.Add(new Entity(rdr["name"].ToString(), rdr["name_rus"].ToString()));
                        }
                        return ret;
                    }
                }
            }
        }
        public Dictionary<int, string> SelectEntityItems(string entityName, List<int> entityId)
        {
            Dictionary<int, string> ret = new Dictionary<int, string>();
            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select * from meta." + entityName, cnn))
                {
                    if (entityId != null)
                    {
                        cmd.CommandText += " where id = ANY(:id)";
                        cmd.Parameters.AddWithValue("id", entityId);
                    }
                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ret.Add((int)rdr["id"], rdr["name"].ToString());
                        }
                        return ret;
                    }
                }
            }
        }
        public Dictionary<int, string> SelectEntityItemsAll(string entityName)
        {
            return SelectEntityItems(entityName, null);
        }
    }
}
