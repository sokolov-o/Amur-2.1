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
    public class CategoryItemRepository : BaseRepository<CategoryItem>
    {
        internal CategoryItemRepository(Common.ADbNpgsql db) : base(db, "meta.category_item") { }

        protected override object ParseData(NpgsqlDataReader rdr)
        {
            int i1 = rdr.GetOrdinal("value1");
            int i2 = rdr.GetOrdinal("value2");
            return new CategoryItem()
            {
                Id = (int)rdr["id"],
                CategorySetId = (int)rdr["category_set_id"],
                CategoryItemNameSetId = (int)rdr["category_item_name_set_id"],
                Code = (int)rdr["code"],
                OrderBy = (int)rdr["order_by"],
                Value1 = rdr.IsDBNull(i1) ? null : (double?)(double)rdr[i1],
                Value2 = rdr.IsDBNull(i2) ? null : (double?)(double)rdr[i2]
            };

        }
        public List<CategoryItem> SelectByCategorySetId(int categoryId)
        {
            return SelectByCategorySetId(new List<int>() { categoryId });
        }
        public List<CategoryItem> SelectByCategorySetId(List<int> categoryIds = null)
        {
            return base.Select(new Dictionary<string, object>() { { "category_set_id", categoryIds } });
        }
        public int Insert(CategoryItem item)
        {
            var fields = new Dictionary<string, object>()
            {
                {"category_set_id", item.CategorySetId},
                {"category_item_name_set_id", item.CategoryItemNameSetId},
                {"code", item.Code},
                   {"order_by", item.OrderBy},
                {"value1", item.Value1},
                {"value2", item.Value2}
            };
            return InsertWithReturn(fields);
        }
        // <summary>
        // Выбрать существующий максимальный код (code - порядковый номер) для заданного набора категорий.
        // </summary>
        // <param name="categorySetId">Код набора категорий.</param>
        /// <returns>Макс значение или null, если для указанной категории отсутствуют её элементы.</returns>
        public int? SelectMaxCode(int categorySetId)
        {
            string ret = ExecSimpleQuery("select max(count) from" + TableName + " where category_set_id=:category_set_id", new Dictionary<string, object>() { { "category_set_id", categorySetId }, }, true);
            return string.IsNullOrEmpty(ret) ? null : (int?)int.Parse(ret);
        }
        public int? SelectMaxOrderBy(int categorySetId)
        {
            string ret = ExecSimpleQuery("select max(order_by) from" + TableName + " where category_set_id=:category_set_id", new Dictionary<string, object>() { { "category_set_id", categorySetId }, }, true);
            return string.IsNullOrEmpty(ret) ? null : (int?)int.Parse(ret);
        }

        public void Update(CategoryItem item)
        {
            var fields = new Dictionary<string, object>()
                {
                    {"id", item.Id},
                    {"category_set_id", item.CategorySetId},
                    {"category_item_name_set_id", item.CategoryItemNameSetId},
                    {"code", item.Code},
                    {"order_by", item.OrderBy},
                    {"value1", item.Value1},
                    {"value2", item.Value2}
                };
            Update(fields);
        }
    }
}
