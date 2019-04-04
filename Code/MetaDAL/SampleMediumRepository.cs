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
    public class SampleMediumRepository : BaseRepository<SampleMedium>
    {
        internal SampleMediumRepository(Common.ADbNpgsql db)
            : base(db, "meta.sample_medium")
        {
        }
        public static List<SampleMedium> GetCash()
        {
            return GetCash(DataManager.GetInstance().SampleMediumRepository);
        }
        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return new SampleMedium
                (
                (int)rdr["id"],
                rdr["name"].ToString(),
                (rdr.IsDBNull(rdr.GetOrdinal("description"))) ? null : rdr["description"].ToString()
                );
        }
        /// <summary>
        /// Создать метод.
        /// </summary>
        /// <param name="item">Method instance.</param>
        /// <returns></returns>
        public void Insert(SampleMedium item)
        {
            var fields = new Dictionary<string, object>()
            {
                {"id", item.Id},
                {"name", item.Name},
                {"description", item.Description}
            };
            Insert(fields);
        }
    }
}
