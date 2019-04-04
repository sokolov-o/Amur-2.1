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
    public class CategorySetRepository : BaseRepository<CategorySet>
    {
        internal CategorySetRepository(Common.ADbNpgsql db) : base(db, "meta.category_set") { }

        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return new CategorySet() { Id = (int)rdr["id"] };

        }
        public void Insert(CategorySet item)
        {
            Insert(new Dictionary<string, object>() { { "id", item.Id } });
        }
    }
}
