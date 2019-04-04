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
    public class StationAddrRegionRepository
    {
        Common.ADbNpgsql _db;
        internal StationAddrRegionRepository(Common.ADbNpgsql db)
        {
            _db = db;
        }
        /// <summary>
        /// Создать сайт.
        /// </summary>
        /// <param name="stationId">Код для выборки или все, если null.</param>
        /// <returns></returns>
        public List<SiteAddrRegion> Select(List<int> stationId = null)
        {
            List<SiteAddrRegion> ret = new List<SiteAddrRegion>();

            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select * from meta.station_x_addr_region where :station_id is null or station_id = any(:station_id)", cnn))
                {
                    cmd.Parameters.AddWithValue("station_id", stationId);
                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ret.Add(new SiteAddrRegion((int)rdr["station_id"], (int)rdr["addr_region_id"]));
                        }
                    }
                }
            }
            return ret;
        }
        public void InsertUpdate(SiteAddrRegion item)
        {
            if (Select(new List<int>(new int[] { item.StationId })) == null)
                Insert(item);
            else
                Update(item);
        }
        public void Insert(SiteAddrRegion item)
        {
            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("insert into meta.station_x_addr_region"
                    + "(station_id, addr_region_id)"
                    + " values (:station_id,:addr_region_id)", cnn))
                {
                    cmd.Parameters.AddWithValue("station_id", item.StationId);
                    cmd.Parameters.AddWithValue("addr_region_id", item.AddrRegionId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void Update(SiteAddrRegion item)
        {
            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("update meta.station_x_addr_region"
                    + " set addr_region_id = :addr_region_id where station_id=:station_id", cnn))
                {
                    cmd.Parameters.AddWithValue("station_id", item.StationId);
                    cmd.Parameters.AddWithValue("addr_region_id", item.AddrRegionId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
