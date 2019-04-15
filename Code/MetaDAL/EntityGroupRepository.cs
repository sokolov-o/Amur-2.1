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
    public class EntityGroupRepository : BaseRepository<EntityGroup>
    {
        internal EntityGroupRepository(Common.ADbNpgsql db) : base(db, "meta.entity_group")
        {
        }

        /// <summary>
        /// Вставка группы.
        /// </summary>
        /// <param name="name">Группа.</param>
        /// <param name="entityTabName">Таблица группы.</param>
        /// <returns></returns>
        public int InsertGroup(string name, string entityTabName)
        {
            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("insert into meta.entity_group (name, entity_tab_name) values (:name, :entity_tab_name);"
                    + "select max(id) from meta.entity_group;", cnn))
                {
                    cmd.Parameters.AddWithValue("name", name);
                    cmd.Parameters.AddWithValue("entity_tab_name", entityTabName);

                    return int.Parse(cmd.ExecuteScalar().ToString());
                }
            }
        }
        /// <summary>
        /// Добавить сайты к группе сайтов.
        /// </summary>
        public void InsertGroupEntities(int groupId, List<int[/*entity_id;order_by*/]> entityIdList)
        {
            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("insert into meta.entity_group_entities"
                               + "(entity_group_id, entity_id, order_by) values (:entity_group_id, :entity_id, :order_by)", cnn))
                {
                    cmd.Parameters.AddWithValue("entity_group_id", groupId);
                    cmd.Parameters.AddWithValue("entity_id", DBNull.Value);
                    cmd.Parameters.AddWithValue("order_by", DBNull.Value);
                    foreach (var item in entityIdList)
                    {
                        cmd.Parameters[1].Value = item[0];
                        cmd.Parameters[2].Value = item[1];

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public void InsertGroupEntity(int groupId, int entityId, int orderBy)
        {
            int[] eo = new int[] { entityId, orderBy };
            InsertGroupEntities(groupId, new List<int[]>(new int[][] { eo }));
        }

        public Dictionary<EntityGroup, List<int[/*код объекта; его порядок*/]>> SelectGroupsFK(List<int> groupId)
        {
            Dictionary<EntityGroup, List<int[]>> ret = new Dictionary<EntityGroup, List<int[]>>();

            List<EntityGroup> groups = Select(groupId);
            foreach (var group in groups)
            {
                ret.Add(group, SelectEntities(group.Id));
            }
            return ret;
        }
        /// <summary>
        /// Выборка групп сущности и членов этих групп с сортировкой в заданном порядке в пределах группы.
        /// </summary>
        /// <param name="entityTabName"></param>
        /// <returns>Словарь или пустой словарь.</returns>
        public Dictionary<EntityGroup, List<int[]>> SelectGroupsFK(string entityTabName)
        {
            return SelectGroupsFK(SelectByEntityTableName(entityTabName).Select(x => x.Id).ToList());
        }

        //////public List<EntityGroup> SelectGroups(List<int> groupId)
        //////{
        //////    List<EntityGroup> ret = new List<EntityGroup>();
        //////    if (groupId != null && groupId.Count > 0)
        //////    {
        //////        using (NpgsqlConnection cnn = _db.Connection)
        //////        {
        //////            using (NpgsqlCommand cmd = new NpgsqlCommand("select * from meta.entity_group where id = ANY(:groupId)", cnn))
        //////            {
        //////                cmd.Parameters.AddWithValue("groupId", groupId);

        //////                using (NpgsqlDataReader rdr = cmd.ExecuteReader())
        //////                {
        //////                    while (rdr.Read())
        //////                    {
        //////                        ret.Add(new EntityGroup((int)rdr["id"], rdr["name"].ToString(), rdr["entity_tab_name"].ToString()));
        //////                    }
        //////                }
        //////            }
        //////        }
        //////    }
        //////    return ret;
        //////}
        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return new EntityGroup((int)rdr["id"], rdr["name"].ToString(), rdr["entity_tab_name"].ToString());
        }

        /// <summary>
        /// Все группы.
        /// </summary>
        /// <param name="entityTabName">Название таблицы с сущностями.</param>
        /// <returns>Список или пустой список.</returns>
        public List<EntityGroup> SelectByEntityTableName(string entityTabName)
        {
            return ExecQuery<EntityGroup>(
                "select * from meta.entity_group where entity_tab_name = :entity_tab_name",
                new Dictionary<string, object>() { { "entity_tab_name", entityTabName } }
                );
            ////List<int> ids = new List<int>();

            ////using (NpgsqlConnection cnn = _db.Connection)
            ////{
            ////    using (NpgsqlCommand cmd = new NpgsqlCommand("select id from meta.entity_group where entity_tab_name = '" + entityTabName + "'", cnn))
            ////    {
            ////        using (NpgsqlDataReader rdr = cmd.ExecuteReader())
            ////        {
            ////            while (rdr.Read())
            ////            {
            ////                ids.Add((int)rdr["id"]);
            ////            }
            ////        }
            ////    }
            ////}
            ////return Select(ids);
        }
        /// <summary>
        /// Считывание кодов сущностей и их порядка в указанной группе.
        /// Возврат: список из двух элементов: кода и его порядка/приоритета.
        /// </summary>
        /// <param name="groupId">Группа.</param>
        /// <returns>Список из двух элементов: кода и его порядка/приоритета.</returns>
        public List<int[]> SelectEntities(int groupId)
        {
            List<int[]> ret = new List<int[]>();

            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select * from meta.entity_group_entities where entity_group_id = :entity_group_id order by order_by", cnn))
                {
                    cmd.Parameters.AddWithValue("entity_group_id", groupId);
                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ret.Add(new int[] { (int)rdr["entity_id"], (int)rdr["order_by"] });
                        }
                        return ret;
                    }
                }
            }
        }

        /// <summary>
        /// Вставка группы.
        /// </summary>
        /// <param name="groupName">Группа.</param>
        /// <param name="entityTabName">Таблица группы.</param>
        /// <returns></returns>
        public void UpdateGroup(int entityGroupId, string groupName)
        {
            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("update meta.entity_group set name=:name"
                    + " where id = " + entityGroupId, cnn))
                {
                    cmd.Parameters.AddWithValue("name", groupName);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void UpdateGroupItems(int entityGroupId, List<int> items)
        {
            NpgsqlTransaction tran = null;
            using (NpgsqlConnection cnn = _db.Connection)
            {
                try
                {
                    tran = cnn.BeginTransaction();
                    using (NpgsqlCommand cmd = new NpgsqlCommand("delete from meta.entity_group_entities where entity_group_id = " + entityGroupId, cnn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    using (NpgsqlCommand cmd = new NpgsqlCommand("insert into meta.entity_group_entities (entity_group_id, entity_id, order_by) values (:entity_group_id, :entity_id, :order_by)", cnn))
                    {
                        cmd.Parameters.AddWithValue("entity_group_id", entityGroupId);
                        cmd.Parameters.AddWithValue("entity_id", DBNull.Value);
                        cmd.Parameters.AddWithValue("order_by", DBNull.Value);

                        for (int i = 0; i < items.Count; i++)
                        {
                            cmd.Parameters[1].Value = items[i];
                            cmd.Parameters[2].Value = i;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    if (tran != null) tran.Rollback();
                    throw ex;
                }
            }
        }

        internal void DeleteGroupItem(int groupId, int entityId)
        {
            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("delete from meta.entity_group_entities"
                    + " where entity_group_id = " + groupId + " and entity_Id = " + entityId, cnn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteGroup(int groupId)
        {
            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("delete from meta.entity_group"
                    + " where id = " + groupId, cnn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
