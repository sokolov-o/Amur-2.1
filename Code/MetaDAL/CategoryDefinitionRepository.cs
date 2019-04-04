using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using FERHRI.Common;
using Npgsql;

namespace FERHRI.Amur.Meta
{
    public class CategoryDefinitionRepository : BaseRepository<CategoryDefinition>
    {
        internal CategoryDefinitionRepository(Common.ADbNpgsql db) : base(db, "meta.category_definition") { }

        protected override object ParseData(NpgsqlDataReader rdr)
        {
            int i1 = rdr.GetOrdinal("value1");
            int i2 = rdr.GetOrdinal("value2");
            return new CategoryDefinition()
            {
                CategoryId = (int)rdr["category_definition_id"],
                CategoryItemId = (int)rdr["category_item_id"],
                Code = (int)rdr["code"],
                Value1 = rdr.IsDBNull(i1) ? null : (double?)(double)rdr[i1],
                Value2 = rdr.IsDBNull(i2) ? null : (double?)(double)rdr[i2]
            };

        }
        public List<CategoryDefinition> SelectByCategoryId(int categoryId)
        {
            return SelectByCategoryId(new List<int>() { categoryId });
        }
        public List<CategoryDefinition> SelectByCategoryId(List<int> categoryIds = null)
        {
            return base.Select(new Dictionary<string, object>() { { "category_definition_id", categoryIds } });
        }
        public void Insert(CategoryDefinition item)
        {
            var fields = new Dictionary<string, object>()
            {
                {"category_definition_id", item.CategoryId},
                {"category_item_id", item.CategoryItemId},
                {"code", item.Code},
                {"value1", item.Value1},
                {"value2", item.Value2}
            };
            Insert(fields);
        }

    }
}
