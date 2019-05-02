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
    public class GeoTypeRepository : BaseRepository<GeoType>
    {
        internal GeoTypeRepository(Common.ADbNpgsql db)
            : base(db, "meta.geo_type")
        {
        }
        public static List<GeoType> GetCash()
        {
            return GetCash(DataManager.GetInstance().GeoTypeRepository);
        }
        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return new GeoType
                (
                (int)rdr["id"],
                rdr["name"].ToString(),
                rdr["name_eng"].ToString(),
                (rdr.IsDBNull(rdr.GetOrdinal("description"))) ? null : rdr["description"].ToString()
                );
        }

        /// <summary>
        /// Создать географический тип.
        /// </summary>
        /// <param name="item">Экземпляр класса GeoType.</param>
        /// <returns></returns>
        public int Insert(GeoType item)
        {
            int ret;
            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("insert into meta.geo_type"
                                + "(name, description, name_eng)"
                                + " values (?,?,?)", cnn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    if (item.Id >= 0)
                        cmd.Parameters.AddWithValue("", item.Id);
                    cmd.Parameters.AddWithValue("", item.Name);
                    cmd.Parameters.Add(ADbNpgsql.GetParameter("", item.Description));
                    cmd.Parameters.AddWithValue("", item.NameEng);

                    ret = int.Parse(cmd.ExecuteScalar().ToString());
                }
            }
            return ret;
        }
    }
}
