using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Common;
using Npgsql;

namespace SOV.Social
{
    public class LegalEntityRepository : BaseRepository<LegalEntity>
    {
        internal LegalEntityRepository(Common.ADbNpgsql db) : base(db, "social.legal_entity") { }

        private string x_images_db = "social.legal_entity_x_image";

        public static List<LegalEntity> GetCash()
        {
            return GetCash(DataManager.GetInstance().LegalEntityRepository);
        }

        public void InsertImages(int entityId, List<int> imgs)
        {
            var fieldList = new List<Dictionary<string, object>>();
            foreach (var img in imgs)
                fieldList.Add(new Dictionary<string, object>()
                {
                    {"org_id", entityId}, {"image_id", img}
                });
            string insert = string.Format("Insert Into {0}(org_id, image_id) Values(:org_id, :image_id)", x_images_db);
            ExecSimpleQuery(insert, fieldList);
        }

        public void DeleteImages(int entityId, List<int> imgs)
        {
            if (imgs.Count == 0)
                return;
            var fields = new Dictionary<string, object>() { { "image_id", imgs }, { "org_id", entityId } };
            string sql = string.Format("Delete From {0} {1}", x_images_db, QueryBuilder.Where(fields));
            ExecSimpleQuery(sql, fields);
            DataManager.GetInstance().ImageRepository.Delete(imgs);
        }

        private void UpdateImagesList(object entityId, List<int> imgs)
        {
            string delete = string.Format("Delete From {0} Where org_id = :org_id", x_images_db);
            ExecSimpleQuery(delete, new Dictionary<string, object>() { { "org_id", entityId } });
            InsertImages((int)entityId, imgs);
        }

        public Dictionary<string, object>[] GetFieldDictionaryes(LegalEntity item)
        {
            Dictionary<string, object>[] ret = new Dictionary<string, object>[3];
            ret[0] = new Dictionary<string, object>()
            {
                {"name_rus", item.NameRus},
                {"name_eng", item.NameEng},
                {"name_rus_short", item.NameRusShort},
                {"name_eng_short", item.NameEngShort},
                {"address_id", item.AddrId},
                {"address_add", item.AddrAdd},
                {"email", item.Email},
                {"phones", item.Phones},
                {"web_site", item.WebSite},
                {"parent_id", item.ParentId},
                {"type", item.Type}
            };
            if (item.Id > -1)
                ret[0].Add("id", item.Id);

            if (item.Entity != null)
            {
                if (item.Type == 'o')
                {
                    Social.Org entity = (Social.Org)item.Entity;
                    ret[1] = new Dictionary<string, object>()
                    {
                        {"legal_entity_id", entity.Id},
                        {"staff_id_first_face", entity.StaffIdFirstFace}
                    };
                }
                else if (item.Type == 'p')
                {
                    Social.Person entity = (Social.Person)item.Entity;
                    ret[1] = new Dictionary<string, object>()
                    {
                        {"legal_entity_id", entity.LegalEntityId},
                        {"sex", entity.Sex}
                    };
                }
            }
            ret[2] = new Dictionary<string, object>() { { "imgs", item.Imgs } };
            return ret;
        }
        public int Insert(LegalEntity le)
        {
            Dictionary<string, object>[] fields = GetFieldDictionaryes(le);

            return Insert(fields[0], fields[1], (List<int>)fields[2]["imgs"]);
        }
        public int Insert(
                Dictionary<string, object> entityFields,
                Dictionary<string, object> entitySpecFields,
                List<int> imgs)
        {
            int id = InsertWithReturn(entityFields);
            if (id > -1)
            {
                if (entitySpecFields != null)
                {
                    entitySpecFields["legal_entity_id"] = id;
                    if ((char)entityFields["type"] == 'o')
                        DataManager.GetInstance().OrgRepository.Insert(entitySpecFields);
                    else
                        DataManager.GetInstance().PersonRepository.Insert(entitySpecFields);
                }
                if (imgs != null)
                    UpdateImagesList(id, imgs);
            }
            return id;
        }
        public void Update(LegalEntity le)
        {
            Dictionary<string, object>[] fields = GetFieldDictionaryes(le);
            Update(fields[0], fields[1], (List<int>)fields[2]["imgs"]);
        }
        public void Update(
            Dictionary<string, object> entityFields,
            Dictionary<string, object> entitySpecFields,
            List<int> imgs)
        {
            Update(entityFields);

            if (entitySpecFields != null && entitySpecFields.Count > 0)
            {
                entitySpecFields["legal_entity_id"] = entityFields["id"];
                if ((char)entityFields["type"] == 'o')
                    DataManager.GetInstance().OrgRepository.Update(entitySpecFields);
                else
                    DataManager.GetInstance().PersonRepository.Update(entitySpecFields);
            }

            if (imgs != null)
                UpdateImagesList(entityFields["id"], imgs);
        }

        protected override object ParseData(NpgsqlDataReader reader)
        {
            return new LegalEntity(DataManager.ParseIdNames(reader))
            {
                AddrId = Common.ADbNpgsql.GetValueInt(reader, "address_id"),
                AddrAdd = reader["address_add"].ToString(),
                Email = reader["email"].ToString(),
                Phones = reader["phones"].ToString(),
                WebSite = reader["web_site"].ToString(),
                ParentId = Common.ADbNpgsql.GetValueInt(reader, "parent_id"),
                Type = reader["type"].ToString()[0]
            };
        }

        public List<LegalEntity> SelectAll(bool withEntityes = false)
        {
            return SelectById(null, withEntityes);
        }
        /// <summary>
        /// Получить список по списку кодов сущностей.
        /// </summary>
        /// <param name="ids">Список кодов ресурсов.</param>
        /// <returns>Список ресурсов.</returns>
        public List<LegalEntity> SelectById(List<int> ids, bool withEntityes = false)
        {
            List<LegalEntity> ret = base.Select(ids);
            if (withEntityes)
                SelectEntityes(ret);
            return ret;
        }

        public LegalEntity SelectByNameRus(string nameRus)
        {
            Dictionary<string, object> fields = new Dictionary<string, object>()
                {
                    { "name_rus", nameRus}
                };
            List<LegalEntity> ret = ExecQuery<LegalEntity>("select * from " + TableName + " where name_rus = :name_rus", fields, ParseData);
            return ret.Count == 0 ? null : ret[0];
        }

        /// <summary>
        /// Выбрать и добавить в элементы списка сущности (entity {org, person}).
        /// </summary>
        /// <param name="ret">Список объектов права.</param>
        void SelectEntityes(List<LegalEntity> ret)
        {
            List<int> ids = ret.Where(x => x.Type == 'o').Select(x => x.Id).ToList();
            foreach (var item in DataManager.GetInstance().OrgRepository.SelectById(ids))
            {
                ret.FirstOrDefault(x => x.Id == item.Id).Entity = item;
            }
            ids = ret.Where(x => x.Type == 'p').Select(x => x.Id).ToList();
            foreach (var item in DataManager.GetInstance().PersonRepository.SelectById(ids))
            {
                ret.FirstOrDefault(x => x.Id == item.LegalEntityId).Entity = item;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">'o' or 'p' - organization or person.</param>
        /// <param name="nameRusShortLike"></param>
        /// <param name="nameEngShortLike"></param>
        /// <param name="nameRusLike"></param>
        /// <param name="nameEngLike"></param>
        /// <returns></returns>
        public List<LegalEntity> Select(char? type, string nameRusShortLike = null, string nameEngShortLike = null, string nameRusLike = null, string nameEngLike = null)
        {
            Dictionary<string, object> fields = new Dictionary<string, object>()
            {
                { "type", type },
                { "name_rus", nameRusLike},
                { "name_eng", nameEngLike},
                { "name_rus_short", nameRusShortLike},
                { "name_eng_short", nameEngShortLike}

            };
            return SelectAllFields(fields);

            //List<LegalEntity> ret = new List<LegalEntity>();
            //using (NpgsqlConnection cnn = _db.Connection)
            //{
            //    using (Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand(
            //        "select * from social.legalEntity where" +
            //        "     :type is null or type = :type" +
            //        " and (:name_rus is null or name_rus like :name_rus)" +
            //        " and (:name_eng is null or name_eng like :name_eng)" +
            //        " and (:name_rus_short is null or name_rus_short like :name_rus_short)" +
            //        " and (:name_eng_short is null or name_eng_short like :name_eng_short)"
            //        , cnn))
            //    {
            //        cmd.Parameters.AddWithValue("type", type);
            //        cmd.Parameters.AddWithValue("name_rus", nameRusLike);
            //        cmd.Parameters.AddWithValue("name_eng", nameEngLike);
            //        cmd.Parameters.AddWithValue("name_rus_short", nameRusShortLike);
            //        cmd.Parameters.AddWithValue("name_eng_short", nameEngShortLike);

            //        using (Npgsql.NpgsqlDataReader rdr = cmd.ExecuteReader())
            //        {
            //            while (rdr.Read())
            //            {
            //                ret.Add((LegalEntity)ParseData(rdr));
            //            }
            //        }
            //    }
            //}
            //return ret;
        }

        public List<LegalEntity> SelectByType(char type)
        {
            return Select(new Dictionary<string, object>() { { "type", type } });
        }

        public override List<LegalEntity> SelectAllFields()
        {
            return SelectAllFields(new Dictionary<string, object>() { { "l.id", null } });
        }

        public override List<LegalEntity> SelectAllFields(Dictionary<string, object> fields)
        {
            string sql = "Select l.*, p.name_rus as parent, social.get_full_addr(l.address_id) as full_addr" +
                         " From {0} as l Left Join (select id, name_rus from {1}) as p on p.id = l.parent_id";
            sql = string.Format(sql, TableName, TableName);
            return SelectAllFields(sql, fields, AllFieldsParse);
        }

        private object AllFieldsParse(NpgsqlDataReader reader)
        {
            var res = (LegalEntity)ParseData(reader);
            res.TypeName = LegalEntity.Types.Find(x => x.Entity.Equals(res.Type)).Name;
            res.Parent = ADbNpgsql.GetValueString(reader, "parent");
            res.FullAddr = ADbNpgsql.GetValueString(reader, "full_addr");
            return res;
        }

        public List<DicItem> SelectTree(List<int> ids = null)
        {
            var p = Select(ids);
            return InitTree(p, null).OrderBy(x => ((LegalEntity)x.Entity).Type).ThenBy(x => x.Name).ToList();
        }

        private List<DicItem> InitTree(List<LegalEntity> data, int? parent_id)
        {
            var childs = new List<DicItem>();
            var arr = parent_id.HasValue
                ? data.FindAll(x => x.ParentId == parent_id.Value)
                : data.FindAll(x => x.ParentId == null);
            foreach (var elm in arr)
                childs.Add(new DicItem(elm.Id, elm.NameRus, elm) { Childs = InitTree(data, elm.Id) });
            return childs;
        }
    }
}
