using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using SOV.Common;
using Npgsql;

namespace SOV.Social
{
    public class AddrRepository : BaseRepository<Addr>
    {
        internal AddrRepository(Common.ADbNpgsql db)
            : base(db, "social.addr")
        {
            _db = db;
        }
        public static List<Addr> GetCash()
        {
            return GetCash(DataManager.GetInstance().AddrRepository);
        }

        /// <summary>
        /// Выборка данных c дочерними объектами (с рекурсией).
        /// </summary>
        /// <param name="addrRegionIds">Список кодов объектов для выборки.</param>
        /// <returns>Словарь где ключ - головной объект, значения - список дочерних объектов. Значение м.б. null.</returns>
        public List<Addr> Select(List<int> addrRegionIds, bool isWithChilds)
        {
            List<Addr> ret = new List<Addr>();
            foreach (var parentId in addrRegionIds)
            {
                List<Addr> ars = Select(parentId, true);
                if (ars.Count > 0)
                {
                    Addr arParent = ars.Find(x => x.Id == parentId);
                    if (isWithChilds) arParent.FillChilds(ars);
                    ret.Add(arParent);
                }
            }
            return ret;
        }
        /// <summary>
        /// Выборка данных с или без дочерних объектов.
        /// </summary>
        /// <param name="addrRegionId">Список кодов объектов для выборки.</param>
        /// <returns></returns>
        List<Addr> Select(int addrRegionId, bool isWithChilds)
        {
            List<Addr> ret = new List<Addr>();

            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select * from meta.select_addr_region(:id, :is_with_childs)", cnn))
                {
                    cmd.Parameters.AddWithValue("id", addrRegionId);
                    cmd.Parameters.AddWithValue("is_with_childs", isWithChilds);
                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ret.Add((Addr)ParseData(rdr));
                        }
                    }
                }
            }
            return ret;
        }
        ///// <summary>
        ///// Выборка всех объектов.
        ///// </summary>
        ///// <returns></returns>
        //public List<Addr> Select()
        //{
        //    List<Addr> ret = new List<Addr>();

        //    using (NpgsqlConnection cnn = _db.Connection)
        //    {
        //        using (NpgsqlCommand cmd = new NpgsqlCommand("select * from social.addr", cnn))
        //        {
        //            using (NpgsqlDataReader rdr = cmd.ExecuteReader())
        //            {
        //                while (rdr.Read())
        //                {
        //                    ret.Add((Addr)ParseData(rdr));
        //                }
        //            }
        //        }
        //    }
        //    return ret;
        //}
        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return new Addr(
                (int)rdr["id"],
                (int)rdr["addr_type_id"],
                rdr["name"].ToString(),
                rdr["name_short"].ToString(),
                 ADbNpgsql.GetValueInt(rdr, "parent_id")
            );
        }

        public int Insert(Addr ar)
        {
            int id;

            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("insert into social.addr (name, name_short, addr_type_id, parent_id)" +
                    " values (:name, :name_short, :addr_type_id, :parent_id)", cnn))
                {
                    cmd.Parameters.AddWithValue("name", ar.Name);
                    cmd.Parameters.AddWithValue("name_short", ar.NameShort.Length > 10 ? ar.NameShort.Substring(0, 10) : ar.NameShort);
                    cmd.Parameters.AddWithValue("addr_type_id", ar.TypeId);
                    cmd.Parameters.AddWithValue("parent_id", ar.ParentId);

                    id = int.Parse(cmd.ExecuteNonQuery().ToString());
                }
            }
            return id;
        }

        public void Update(Addr ar)
        {
            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("update social.addr set" +
                    " name=:name, name_short=:name_short, addr_type_id=:addr_type_id, parent_id=:parent_id" +
                    " where id=:id", cnn))
                {
                    cmd.Parameters.AddWithValue("name", ar.Name);
                    cmd.Parameters.AddWithValue("name_short", ar.NameShort);
                    cmd.Parameters.AddWithValue("addr_type_id", ar.TypeId);
                    cmd.Parameters.AddWithValue("parent_id", ar.ParentId);
                    cmd.Parameters.AddWithValue("id", ar.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<DicItem> SelectTree(List<int> ids = null)
        {
            return InitTree(Select(ids), null, null);
        }

        private List<DicItem> InitTree(List<Addr> addr, int? parent_id, DicItem parentItem)
        {
            var childs = new List<DicItem>();
            var arr = parent_id.HasValue
                ? addr.FindAll(x => x.ParentId == parent_id.Value)
                : addr.FindAll(x => x.ParentId == null);
            foreach (var elm in arr)
            {
                var newItem = new DicItem(elm.Id, elm.Name, (object)string.Format("{0}{1}", 
                                                                parentItem == null ? "" : (parentItem.Entity + ", "), elm.Name)
                );
                newItem.Childs = InitTree(addr, elm.Id, newItem);
                childs.Add(newItem);
            }
            return childs;
        }
    }
}
