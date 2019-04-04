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
    public class MeteoZoneRepository
    {
        Common.ADbNpgsql _db;
        internal MeteoZoneRepository(Common.ADbNpgsql db)
        {
            _db = db;
        }
        public MeteoZone Select(int meteoZoneId)
        {
            List<MeteoZone> ret = Select(new List<int>() { meteoZoneId });
            return (ret.Count == 0) ? null : ret[0];
        }
        public List<MeteoZone> Select(List<int> meteoZonesId = null)
        {
            List<MeteoZone> ret = new List<MeteoZone>();

            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select * from meta.meteo_zone where :id is null or id = any(:id)", cnn))
                {
                    cmd.Parameters.AddWithValue("id", meteoZonesId);

                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ret.Add(new MeteoZone((int)rdr["id"], rdr["name"].ToString(), (int)rdr["hour_meteoday_s"]));
                        }
                    }
                }
            }
            return ret;
        }
    }
}
