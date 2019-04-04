using System;
using System.Collections.Generic;
using System.Linq;
using SOV.Common;
using Npgsql;

namespace SOV.Amur.Sys
{
    public class SysEntityRepository : BaseRepository<EntityInstanceValue>
    {
        internal SysEntityRepository(Common.ADbNpgsql db) : base(db, "sys.entity_instance_value") { }

        public string EntityTableName = "sys.entity";
        public string EntityAttrTableName = "sys.entity_attr";
        public string EntityViewName = "sys.entity_instance_value_view";
        public string EntityInstanceTableName = "sys.entity_instance_value";

        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return new EntityInstanceValue(
                (int)rdr["entity_attr_id"],
                rdr["entity_instance"].ToString(),
                rdr["value"].ToString()
            );
        }

        private object ParseEntityAttr(NpgsqlDataReader rdr)
        {
            return new EntityAttr(
                (int)rdr["id"],
                (int)rdr["entity_id"],
                rdr["name"].ToString()
            );
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
            foreach (var attr in SelectValues(entityId, entityInstance))
                ret.Add(attr);
            return ret;
        }
        public EntityAttr SelectAttr(int attrId)
        {
            var fields = new Dictionary<string, object>() { { "id", attrId } };
            string sql = string.Format("Select * From {0} {1}", EntityAttrTableName, QueryBuilder.Where(fields));
            var ret = ExecQuery<EntityAttr>(sql, fields, ParseEntityAttr);
            return ret.Count > 0 ? ret[0] : null;
        }
        public List<EntityAttr> SelectAttrs(int entityId)
        {
            var fields = new Dictionary<string, object>() { { "entity_id", entityId } };
            string sql = string.Format("Select * From {0} {1}", EntityAttrTableName, QueryBuilder.Where(fields));
            var tmp = ExecQuery<EntityAttr>(sql, fields, ParseEntityAttr);
            return tmp;
        }

        public List<EntityInstanceValue> SelectValues(int entityId, string instance)
        {
            var fields = new Dictionary<string, object>()
            {
                {"entity_id", entityId},
                {"entity_instance", instance}
            };
            string sql = string.Format("Select * From {0} {1} ", EntityViewName, QueryBuilder.Where(fields));
            return ExecQuery<EntityInstanceValue>(sql, fields);
        }

        public EntityInstanceValue SelectValue(int attrId, string entityInstance)
        {
            var fields = new Dictionary<string, object>()
            {
                {"entity_attr_id", attrId},
                {"entity_instance", entityInstance}
            };
            string sql = string.Format("Select * From {0} {1} ", EntityInstanceTableName, QueryBuilder.Where(fields));
            var ret = ExecQuery<EntityInstanceValue>(sql, fields);
            return ret.Count > 0 ? ret[0] : null;
        }
        public void InsertUpdate(List<EntityInstanceValue> items)
        {
            foreach (var item in items)
            {
                InsertUpdate(item.AttrId, item.EntityInstance, item.Value);
            }
        }
        public void InsertUpdate(EntityInstanceValue item)
        {
            InsertUpdate(new List<EntityInstanceValue>() { item });
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
            var fields = new Dictionary<string, object>()
            {
                {"entity_attr_id", entityAttrId},
                {"entity_instance", entityInstance},
                {"value", value}
            };
            Insert(fields);
        }

        public void Update(int entityAttrId, string entityInstance, string value)
        {
            var fields = new Dictionary<string, object>()
            {
                {"entity_attr_id", entityAttrId},
                {"entity_instance", entityInstance},
                {"value", value}
            };
            var whereFields = new List<string>() { "entity_attr_id", "entity_instance" };
            Update(new List<Dictionary<string, object>>() { fields }, whereFields);
        }

        public void InsertUpdate(int entityAttrId, string entityInstance, string value)
        {
            if (SelectValue(entityAttrId, entityInstance) == null)
                Insert(entityAttrId, entityInstance, value);
            else
                Update(entityAttrId, entityInstance, value);
        }

        private object ParseEntity(NpgsqlDataReader rdr)
        {
            return new Entity(
                (int)rdr["id"],
                rdr["name"].ToString(),
                rdr.IsDBNull(rdr.GetOrdinal("description")) ? null : rdr["description"].ToString());
        }

        internal List<Entity> SelectEntity()
        {
            string sql = string.Format("Select * From {0}", EntityTableName);
            return ExecQuery<Entity>(sql, new Dictionary<string, object>(), ParseEntity);
        }

        private object ParseString(NpgsqlDataReader rdr)
        {
            return rdr[0].ToString();
        }

        public List<string> SelectInstances(int entitId)
        {
            var fields = new Dictionary<string, object>() { { "entity_id", entitId } };
            string sql = string.Format("Select distinct entity_instance From {0} {1}", EntityViewName, QueryBuilder.Where(fields));
            return ExecQuery<string>(sql, fields, ParseString);
        }
        public List<string> SelectValues(int entitId)
        {
            var fields = new Dictionary<string, object>() { { "entity_id", entitId } };
            string sql = string.Format("Select distinct value From {0} {1}", EntityViewName, QueryBuilder.Where(fields));
            return ExecQuery<string>(sql, fields, ParseString);
        }
    }
}
