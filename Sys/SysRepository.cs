using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using FERHRI.Common;
using Npgsql;

namespace FERHRI.Amur.Sys
{
    public class Repository
    {
        internal static string ConnectionString = global::FERHRI.Amur.Sys.Properties.Settings.Default.ConnectionString;

        public static void SetConnectionString(string newConnectionString)
        {
            ConnectionString = newConnectionString;
        }
        public string GetUserName()
        {
            return ADbNpgsql.GetUser(ConnectionString).Name;
        }
    }

    public class SysRepository : ADbNpgsql
    {
        SysRepository()
            : base(Repository.ConnectionString)
        {
        }
        SysRepository(string ConnectionString)
            : base(ConnectionString)
        {
        }

        static public SysRepository Instance { get { return new SysRepository(); } }
        static public SysRepository GetInstance(User user)
        {
            SysRepository ret = Instance;
            ret.ConnectionString = ADbNpgsql.ConnectionStringUpdateUser(ret.ConnectionString, user);
            return ret;
        }
        /// <summary>
        /// Выбрать значения всех атрибутов экземпляра сущности/объекта.
        /// </summary>
        /// <param name="entityId">Код сущности.</param>
        /// <param name="entityInstance">Наименование сущности.</param>
        /// <returns></returns>
        public AttrValueCollection SelectAllAttr(int entityId, string entityInstance)
        {
            AttrValueCollection ret = new AttrValueCollection();

            using (NpgsqlConnection cnn = Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    "select * from sys.entity_instance_value ev"
                    + " inner join sys.entity_attr ea on ea.id = ev.entity_attr_id"
                    + " where :entity_id = ea.entity_id"
                    + "     and :entity_instance = ev.entity_instance", cnn))
                {
                    cmd.Parameters.AddWithValue("entity_id", entityId);
                    cmd.Parameters.AddWithValue("entity_instance", entityInstance);

                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ret.Add(new EntityInstanceValue(
                                (int)rdr["entity_attr_id"],
                                rdr["entity_instance"].ToString(),
                                rdr["value"].ToString()
                            ));
                        }
                    }
                }
            }
            return ret;
        }
        public EntityAttr SelectAttr(int attrId)
        {
            using (NpgsqlConnection cnn = Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    "select * from sys.entity_attr where id = " + attrId, cnn))
                {
                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            return new EntityAttr(
                                attrId,
                                (int)rdr["entity_id"],
                                rdr["name"].ToString()
                            );
                        }
                        return null;
                    }
                }
            }
        }
        public List<EntityAttr> SelectAttrs(int entityId)
        {
            List<EntityAttr> ret = new List<EntityAttr>();

            using (NpgsqlConnection cnn = Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    "select * from sys.entity_attr where entity_id = " + entityId, cnn))
                {
                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ret.Add(new EntityAttr(
                                (int)rdr["id"],
                                entityId,
                                rdr["name"].ToString()
                            ));
                        }
                        return ret;
                    }
                }
            }
        }
        public EntityInstanceValue SelectValue(int attrId, string entityInstance)
        {
            using (NpgsqlConnection cnn = Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    "select * from sys.entity_instance_value "
                    + " where entity_attr_id = @entity_attr_id and entity_instance = @entity_instance", cnn))
                {
                    cmd.Parameters.AddWithValue("entity_attr_id", attrId);
                    cmd.Parameters.AddWithValue("entity_instance", entityInstance);

                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        List<EntityInstanceValue> ret = new List<EntityInstanceValue>();
                        if (rdr.Read())
                        {
                            return new EntityInstanceValue(
                                attrId,
                                rdr["entity_instance"].ToString(),
                                rdr["value"].ToString()
                            );
                        }
                        return null;
                    }
                }
            }
        }
        public void InsertUpdate(List<EntityInstanceValue> items)
        {
            foreach (var item in items)
            {
                InsertUpdate(item.AttrId, item.EntityInstance, item.Value);
            }
        }
        /// <summary>
        /// Вставка записи при её отсутствии или изменение значения при наличии записи.
        /// </summary>
        /// <param name="entityId">Код сущности.</param>
        /// <param name="entityInstance">Имя сущности.</param>
        /// <param name="entityAttrTypeId">Код типа атрибута сущности.</param>
        /// <returns></returns>
        public void Insert(int entityAttrId, string entityInstance, string value)
        {
            List<EntityInstanceValue> ret = new List<EntityInstanceValue>();

            using (NpgsqlConnection cnn = Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    "insert into sys.entity_instance_value (entity_attr_id, entity_instance, value) values(:entity_attr_id, :entity_instance, :value);", cnn))
                {
                    cmd.Parameters.AddWithValue("entity_attr_id", entityAttrId);
                    cmd.Parameters.AddWithValue("entity_instance", entityInstance);
                    cmd.Parameters.AddWithValue("value", value);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void Update(int entityAttrId, string entityInstance, string value)
        {
            List<EntityInstanceValue> ret = new List<EntityInstanceValue>();

            using (NpgsqlConnection cnn = Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("sys.entity_instance_value_upd", cnn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("_entity_type_id", entityAttrId);
                    cmd.Parameters.AddWithValue("_entity_instance", entityInstance);
                    cmd.Parameters.AddWithValue("_value", value);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void InsertUpdate(int entityAttrId, string entityInstance, string value)
        {
            List<EntityInstanceValue> ret = new List<EntityInstanceValue>();

            using (NpgsqlConnection cnn = Connection)
            {
                if (SelectValue(entityAttrId, entityInstance) == null)
                    Insert(entityAttrId, entityInstance, value);
                else
                    Update(entityAttrId, entityInstance, value);
            }
        }
        public int InsertLog(int entityId, string message, int? parentId = null, bool? isUrgent = false)
        {
            using (NpgsqlConnection cnn = Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    "insert into sys.log (entity_id, message, is_urgent, parent_id) values(:entity_id, :message, :is_urgent, :parent_id);" +
                    "select max(id) from sys.log;", cnn))
                {
                    cmd.Parameters.AddWithValue("entity_id", entityId);
                    cmd.Parameters.AddWithValue("message", message);
                    cmd.Parameters.AddWithValue("is_urgent", isUrgent);
                    cmd.Parameters.AddWithValue("parent_id", parentId);

                    return int.Parse(cmd.ExecuteScalar().ToString());
                }
            }
        }
        public List<Log> SelectLog(DateTime dateS, DateTime dateF, int? entityId = null, bool isUrgentOnly = false)
        {
            List<Log> ret = new List<Log>();

            List<Entity> entityes = SysRepository.Instance.SelectEntity();

            using (NpgsqlConnection cnn = Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    "select * from sys.log where date between :date_s and :date_f"
                    + " and (:entity_id is null or entity_id= :entity_id)"
                    + " and (:is_urgent_only is false or (:is_urgent_only and is_urgent))", cnn))
                {
                    cmd.Parameters.AddWithValue("date_s", dateS);
                    cmd.Parameters.AddWithValue("date_F", dateF);
                    cmd.Parameters.AddWithValue("entity_id", entityId);
                    cmd.Parameters.AddWithValue("is_urgent_only", isUrgentOnly);

                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ret.Add(new Log((int)rdr["id"],
                                entityes.Find(x => x.Id == (int)rdr["entity_id"]),
                                (DateTime)rdr["date"], (bool)rdr["is_urgent"], rdr["message"].ToString(),
                                rdr.IsDBNull(rdr.GetOrdinal("parent_id")) ? null : (int?)(int)rdr["parent_id"]));
                        }
                        return ret;
                    }
                }
            }
        }

        internal List<Entity> SelectEntity()
        {
            List<Entity> ret = new List<Entity>();

            using (NpgsqlConnection cnn = Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    "select * from sys.entity", cnn))
                {
                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ret.Add(new Entity(
                                (int)rdr["id"],
                                rdr["name"].ToString(),
                                rdr.IsDBNull(rdr.GetOrdinal("description")) ? null : rdr["description"].ToString()));
                        }
                        return ret;
                    }
                }
            }
        }

        internal void DeleteLog(List<Log> logId)
        {
            using (NpgsqlConnection cnn = Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    "delete from sys.log where id = :id", cnn))
                {
                    cmd.Parameters.AddWithValue("id", DBNull.Value);
                    foreach (var item in logId.Where(x => x.ParentId.HasValue))
                    {
                        cmd.Parameters[0].Value = item.Id;
                        cmd.ExecuteNonQuery();
                    }
                    foreach (var item in logId.Where(x => !x.ParentId.HasValue))
                    {
                        cmd.Parameters[0].Value = item.Id;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public List<string> SelectInstances(int entitId)
        {
            List<string> ret = new List<string>();

            using (NpgsqlConnection cnn = Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    "select distinct entity_instance from sys.entity_instance_value_view where entity_id = " + entitId, cnn))
                {
                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ret.Add(rdr[0].ToString());
                        }
                        return ret;
                    }
                }
            }
        }
        public List<string> SelectValues(int entitId)
        {
            List<string> ret = new List<string>();

            using (NpgsqlConnection cnn = Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    "select distinct entity_instance from sys.entity_instance_value_view where entity_id = " + entitId, cnn))
                {
                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ret.Add(rdr[0].ToString());
                        }
                        return ret;
                    }
                }
            }
        }

        internal List<EntityInstanceValue> SelectValues(int entityId, string instance)
        {
            List<EntityInstanceValue> ret = new List<EntityInstanceValue>();
            List<EntityAttr> attrs = SysRepository.Instance.SelectAttrs(entityId);

            using (NpgsqlConnection cnn = Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    "select * from sys.entity_instance_value_view where entity_id = " + entityId
                    + " and entity_instance ='" + instance + "'", cnn))
                {
                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ret.Add(new EntityInstanceValue((int)rdr["entity_attr_id"], instance, rdr["value"].ToString()));
                        }
                        return ret;
                    }
                }
            }
        }

        internal void DeleteLog(DateTime dateS, DateTime dateF, int entityId)
        {
            string sql = "delete from sys.log where entity_id = :entity_id and date between :date_s and :date_f";

            using (NpgsqlConnection cnn = Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("", cnn))
                {
                    cmd.Parameters.AddWithValue("entity_id", entityId);
                    cmd.Parameters.AddWithValue("date_s", dateS);
                    cmd.Parameters.AddWithValue("date_f", dateF);

                    cmd.CommandText = sql + " and parent_id is not null";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = sql + " and parent_id is     null";
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
