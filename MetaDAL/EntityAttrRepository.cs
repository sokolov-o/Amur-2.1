using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Common;
using Npgsql;
using SOV.Amur.Meta;


namespace SOV.Amur.Meta
{
    public class EntityAttrRepository : BaseRepository<EntityAttrValue>
    {
        internal EntityAttrRepository(Common.ADbNpgsql db) : base(db, "") { }

        public EntityAttrValue SelectAttrValue(string EntityName, int entityId, int attrTypeId, DateTime dateActual)
        {
            List<EntityAttrValue> ret = SelectAttrValuesActual(EntityName, new List<int>(new int[] { entityId }), new List<int>(new int[] { attrTypeId }), dateActual);
            return ret.Count == 1 ? ret[0] : null;
        }
        /// <summary>
        /// Выбрать все атрибуты экземпляра сущности за все даты.
        /// </summary>
        /// <param name="entityId">Код экземпляра.</param>
        /// <returns></returns>
        public List<EntityAttrValue> SelectAttrValues(string EntityName, int entityId)
        {
            return SelectAttrValues(EntityName, new List<int>(new int[] { entityId }));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityName">Название сущности/таблицы.</param>
        /// <param name="entityId">Все, если null.</param>
        /// <param name="attrTypeId">Все, если null.</param>
        /// <param name="dateActual">Все, если null.</param>
        /// <returns></returns>
        public List<EntityAttrValue> SelectAttrValuesActual(string entityName, List<int> entityId, List<int> attrTypeId = null, DateTime? dateActual = null)
        {
            var fields = new Dictionary<string, object>()
            {
                {"entity_id", entityId ?? new List<int>()},
                {"attr_type_id", attrTypeId ?? new List<int>()},
                {"date_actual", dateActual}
            };
            string sql1 = (dateActual == null)
                ?
                "select * from meta." + entityName + "_attr_value sav where " +
                " (:entity_id is null or entity_id = any(:entity_id))" +
                " and (:attr_type_id is null or attr_type_id = any(:attr_type_id));"
                :
                "select * from meta." + entityName + "_attr_value sav " +
                " inner join (select entity_id, attr_type_id, max(date_s) date_s from meta." + entityName + "_attr_value" +
                " where date_s <= :date_actual " +
                " and (:entity_id is null or entity_id = any(:entity_id))" +
                " and (:attr_type_id is null or attr_type_id = any(:attr_type_id))" +
                " group by entity_id, attr_type_id) t on sav.entity_id=t.entity_id" +
                " and sav.attr_type_id = t.attr_type_id and sav.date_s = t.date_s;";

            return ExecQuery<EntityAttrValue>(sql1, fields, ParseSiteAttr, System.Data.CommandType.Text);

            //List<EntityAttrValue> ret = new List<EntityAttrValue>();
            //string sql = (dateActual == null)
            //    ?
            //    "select * from meta." + entityName + "_attr_value sav where " +
            //    " (:entity_id is null or entity_id = any(:entity_id))" +
            //    " and (:attr_type_id is null or attr_type_id = any(:attr_type_id));"
            //    :
            //    "select * from meta." + entityName + "_attr_value sav " +
            //    " inner join (select entity_id, attr_type_id, max(date_s) date_s from meta." + entityName + "_attr_value" +
            //    " where date_s <= :date_actual " +
            //    " and (:entity_id is null or entity_id = any(:entity_id))" +
            //    " and (:attr_type_id is null or attr_type_id = any(:attr_type_id))" +
            //    " group by entity_id, attr_type_id) t on sav.entity_id=t.entity_id" +
            //    " and sav.attr_type_id = t.attr_type_id and sav.date_s = t.date_s;";
            ////: "select * from meta." + entityName + "_attr_value" +
            ////" where date_s <= :date_actual and (:entity_id is null or entity_id = any(:entity_id)) and (:attr_type_id is null or attr_type_id = any(:attr_type_id)) group by entity_id, attr_type_id) t on sav.entity_id=t.entity_id and sav.attr_type_id = t.attr_type_id and sav.date_s = t.date_s;";

            //using (NpgsqlConnection cnn = _db.Connection)
            //{
            //    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, cnn))
            //    {
            //        cmd.Parameters.AddWithValue("entity_id", entityId);
            //        //cmd.Parameters.AddWithValue("attr_type_id", attrTypeId);
            //        cmd.Parameters.Add(ADbNpgsql.GetParameter("attr_type_id", attrTypeId));
            //        cmd.Parameters.AddWithValue("date_actual", dateActual);
            //        using (NpgsqlDataReader rdr = cmd.ExecuteReader())
            //        {
            //            while (rdr.Read())
            //            {
            //                ret.Add(new EntityAttrValue(
            //                    (int)rdr["entity_id"],
            //                    (int)rdr["attr_type_id"],
            //                    (DateTime)rdr["date_s"],
            //                    ((rdr.IsDBNull(rdr.GetOrdinal("value"))) ? null : rdr["value"].ToString())
            //                    ));
            //            }
            //            return ret;
            //        }
            //    }
            //}
        }

        private object ParseSiteAttr(NpgsqlDataReader reader)
        {
            return new EntityAttrValue(
                (int)reader["entity_id"],
                (int)reader["attr_type_id"],
                (DateTime)reader["date_s"],
                ((reader.IsDBNull(reader.GetOrdinal("value"))) ? null : reader["value"].ToString())
            );
        }

        public List<EntityAttrValue> SelectAttrValues(string entityName, List<int> entityId, List<int> attrTypeId = null, DateTime? dateS = null)
        {
            var fields = new Dictionary<string, object>()
            {
                {"_site_id", entityId ?? new List<int>()},
                {"_site_attr_type_id", attrTypeId ?? new List<int>()},
                {"_date_s", dateS}
            };
            string sql = "meta.abc_get_site_attr";
            return ExecQuery<EntityAttrValue>(sql, fields, ParseSiteAttr, System.Data.CommandType.StoredProcedure);

            //List<EntityAttrValue> ret = new List<EntityAttrValue>();

            //using (NpgsqlConnection cnn = _db.Connection)
            //{
            //    using (NpgsqlCommand cmd = new NpgsqlCommand("meta.get_site_attr", cnn))
            //    {
            //        cmd.CommandType = System.Data.CommandType.StoredProcedure;

            //        cmd.Parameters.AddWithValue("_site_id", entityId);
            //        cmd.Parameters.AddWithValue("_site_attr_type_id", attrTypeId);
            //        cmd.Parameters.AddWithValue("_date_s", dateS);

            //        using (NpgsqlDataReader rdr = cmd.ExecuteReader())
            //        {
            //            while (rdr.Read())
            //            {
            //                ret.Add((EntityAttrValue)ParseSiteAttr(rdr));
            //            }
            //            return ret;
            //        }
            //    }
            //}
        }
        public List<EntityAttrType> SelectAttrTypes(string EntityName)
        {
            List<EntityAttrType> ret = new List<EntityAttrType>();
            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    "select * from meta." + EntityName + "_attr_type", cnn))
                {
                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ret.Add(new EntityAttrType((int)rdr["id"], rdr["name"].ToString(), rdr["site_type_id_mandatory"].ToString()));
                        }
                        return ret;
                    }
                }
            }
        }
        public void InsertUpdateValue(string EntityName, EntityAttrValue eav)
        {
            InsertUpdateValue(EntityName, eav.EntityId, eav.AttrTypeId, eav.DateS, eav.Value);
        }
        public void InsertUpdateValue(string EntityName, int entityId, int attrTypeId, DateTime dateS, string value)
        {
            List<EntityAttrValue> sav = SelectAttrValuesActual(EntityName, new List<int>(new int[] { entityId }), new List<int>(new int[] { attrTypeId }), dateS);
            if (sav.Count == 0)
            {
                InsertValue(EntityName, entityId, attrTypeId, dateS, value);
            }
            else if (sav.Count == 1)
            {
                if (sav[0].Value != value)
                {
                    if (sav[0].DateS != dateS)
                        InsertValue(EntityName, entityId, attrTypeId, dateS, value);
                    else
                        UpdateValue(EntityName, entityId, attrTypeId, dateS, value);
                }
            }
            else
                throw new Exception("Ошибка алгоритма! OSokolov@SOV.ru");
        }
        public void InsertValue(string EntityName, int entityId, int attrTypeId, DateTime dateS, string value)
        {
            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("", cnn))
                {
                    cmd.CommandText = "insert into meta." + EntityName + "_attr_value"
                        + " (entity_id, attr_type_id, date_s, value) values (:entity_id, :attr_type_id, :date_s, :value)";

                    cmd.Parameters.AddWithValue(":entity_id", entityId);
                    cmd.Parameters.AddWithValue(":attr_type_id", attrTypeId);
                    cmd.Parameters.AddWithValue(":date_s", dateS);
                    cmd.Parameters.AddWithValue(":value", value);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void UpdateValue(string EntityName, int entityId, int attrTypeId, DateTime dateS, string value)
        {
            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("", cnn))
                {
                    cmd.CommandText = "update meta." + EntityName + "_attr_value"
                    + " set value = :value"
                    + " where entity_id=:entity_id and attr_type_id=:attr_type_id"
                    + " and date_s = :date_s"
                    ;

                    cmd.Parameters.AddWithValue("entity_id", entityId);
                    cmd.Parameters.AddWithValue("attr_type_id", attrTypeId);
                    cmd.Parameters.AddWithValue("date_s", dateS);
                    cmd.Parameters.AddWithValue("value", value);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteValue(string EntityName, int entityId, int attrTypeId, DateTime dateS)
        {
            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("", cnn))
                {
                    cmd.CommandText = "delete from meta." + EntityName + "_attr_value"
                    + " where entity_id=:entity_id and attr_type_id = :attr_type_id and date_s=:date_s";

                    cmd.Parameters.AddWithValue(":entity_id", entityId);
                    cmd.Parameters.AddWithValue(":attr_type_id", attrTypeId);
                    cmd.Parameters.AddWithValue(":date_s", dateS);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
