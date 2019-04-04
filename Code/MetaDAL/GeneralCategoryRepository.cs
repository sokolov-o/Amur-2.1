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
    public class GeneralCategoryRepository : BaseRepository<GeneralCategory>
    {
        internal GeneralCategoryRepository(Common.ADbNpgsql db)
            : base(db, "meta.general_category")
        {
        }
        public static List<GeneralCategory> GetCash()
        {
            return GetCash(DataManager.GetInstance().GeneralCategoryRepository);
        }
        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return new GeneralCategory
                (
                    (int)rdr["id"],
                    rdr["name"].ToString(),
                    (rdr.IsDBNull(rdr.GetOrdinal("name_short"))) ? null : rdr["name_short"].ToString(),
                    (rdr.IsDBNull(rdr.GetOrdinal("description"))) ? null : rdr["description"].ToString()
                );
        }

        public void Insert(GeneralCategory item)
        {
            var fields = new Dictionary<string, object>()
            {
                {"id", item.Id},
                {"name", item.Name},
                {"name_short", item.NameShort},
                {"description", item.Description}
            };
            Insert(fields);
        }
    }
}
