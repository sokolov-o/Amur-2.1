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
    public class NameItemRepository : BaseRepository<NameItem>
    {
        internal NameItemRepository(Common.ADbNpgsql db) : base(db, "meta.name_item") { }

        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return new NameItem()
            {
                Id = (int)rdr["id"],
                NameSetId = (int)rdr["name_set_id"],
                LangId = (int)rdr["lang_id"],
                NameTypeId = (int)rdr["name_type_id"],
                Name = rdr["name"].ToString()
            };

        }

        public List<NameItem> SelectLike(string like, int langId, int nameTypeId)
        {
            Dictionary<string, object> whereFields = new Dictionary<string, object>()
            {
                { "name", like },
                { "lang_id", langId },
                { "name_type_id", nameTypeId }
            };
            string sql = "Select * from " + TableName + " where upper(name) like upper(:name) and lang_id = :lang_id and name_type_id = :name_type_id";
            return ExecQuery<NameItem>(sql, whereFields);

        }

        public List<NameItem> SelectByNameSetId(int nameSetIds)
        {
            return SelectByNameSetId(new List<int>() { nameSetIds });
        }
        public List<NameItem> SelectByNameSetId(List<int> nameSetIds)
        {
            return Select(new Dictionary<string, object>()
                {
                    {"name_set_id", nameSetIds}
                });
        }
        public int Insert(NameItem item)
        {
            if (item.NameSetId < 1)
            {
                if (item.Id > 0)
                    throw new Exception("item.Id must be not initialized.");
                item.NameSetId = int.Parse(ExecSimpleQuery("insert into meta.name_set (attr) values (:attr);", new Dictionary<string, object>() { { "attr", null } }, true, "select lastval();"));
            }
            item.Id = InsertWithReturn(new Dictionary<string, object>()
            {
                {"name_set_id", item.NameSetId},
                {"lang_id", item.LangId},
                {"name_type_id", item.NameTypeId},
                {"name", item.Name}
            });
            return item.Id;
        }
        public void Update(NameItem item)
        {
            Update(new Dictionary<string, object>()
            {
                {"id", item.Id},
                {"name_set_id", item.NameSetId},
                {"lang_id", item.LangId},
                {"name_type_id", item.NameTypeId},
                {"name", item.Name}
            });
        }
        public int InsertUpdate(NameItem item)
        {
            try
            {
                return Insert(item);
            }
            catch
            {
                Update(item);
                return item.Id;
            }
        }
    }
}
