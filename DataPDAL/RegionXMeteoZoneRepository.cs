using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using SOV.Common;
using SOV.Amur.Meta;

namespace SOV.Amur.DataP
{
    public class RegionXMeteoZoneRepository
    {
        Common.ADbNpgsql _db;
        internal RegionXMeteoZoneRepository(Common.ADbNpgsql db)
        {
            _db = db;
        }
        public RegionXMeteoZone SelectByRegionId(int regionId)
        {
            List<RegionXMeteoZone> ret = SelectByRegionsId(new List<int>() { regionId });
            return ret.Count == 0 ? null : ret[0];
        }
        public List<RegionXMeteoZone> SelectByRegionsId(List<int> regionsId)
        {
            List<RegionXMeteoZone> ret = new List<RegionXMeteoZone>();
            using (var cnn = _db.Connection)
            {
                using (Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand("select * from datap.region_x_meteo_zone"
                    + " where :addr_region_id is null or addr_region_id = any(:addr_region_id)", cnn))
                {
                    cmd.Parameters.AddWithValue("addr_region_id", regionsId);
                    using (Npgsql.NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ret.Add(new RegionXMeteoZone()
                            {
                                Id = (int)rdr["id"],
                                RegionId = (int)rdr["addr_region_id"],
                                MeteoZoneId = (int)rdr["meteo_zone_id"],

                            });
                        }
                    }
                }
                return ret;
            }
        }

    }
}
