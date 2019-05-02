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
    public class AddrTypeRepository : BaseRepository<AddrType>
    {
        internal AddrTypeRepository(Common.ADbNpgsql db)
            : base(db, "social.addr_type")
        {
            _db = db;
        }

        public static List<AddrType> GetCash()
        {
            return GetCash(DataManager.GetInstance().AddrTypeRepository);
        }

        ///// <summary>
        ///// Выборка типа адреса.
        ///// </summary>
        //public AddrType Select(int addrRegionTypeId)
        //{
        //    List<AddrType> ret = Select(new List<int>(new int[] { addrRegionTypeId }));
        //    return ret.Count == 0 ? null : ret[0];
        //}
        /// <summary>
        /// Выборка типов адресов.
        /// </summary>
        /// <param name="addrRegionTypeIds">Список адресов для выборки. Все, если null или пусто.</param>
        /// <returns></returns>
        //public List<AddrType> Select(List<int> addrRegionTypeIds = null)
        //{
        //    List<AddrType> ret = new List<AddrType>();

        //    using (NpgsqlConnection cnn = _db.Connection)
        //    {
        //        using (NpgsqlCommand cmd = new NpgsqlCommand("select * from social.addr_type where :id is null or id = ANY(:id)", cnn))
        //        {
        //            cmd.Parameters.AddWithValue(":id", addrRegionTypeIds);

        //            using (NpgsqlDataReader rdr = cmd.ExecuteReader())
        //            {
        //                while (rdr.Read())
        //                {
        //                    ret.Add((AddrType)ParseData(rdr));
        //                }
        //            }
        //        }
        //    }
        //    return ret;
        //}
        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return new AddrType() { Id = (int)rdr["id"], Name = rdr["name"].ToString(), NameShort = rdr["name_short"].ToString() };
        }

    }
}
