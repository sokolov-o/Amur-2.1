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
    public class _DELME_StationRepository : BaseRepository<_DELME_Station>
    {
        internal _DELME_StationRepository(Common.ADbNpgsql db) : base(db, "meta.station") { }

        //public static List<_DELME_Station> GetCash()
        //{
        //    return GetCash(DataManager.GetInstance()._DELME_StationRepository);
        //}

        /// <summary>
        /// Создать станцию.
        /// </summary>
        /// <param name="station">Станция (id игнорируется).</param>
        /// <returns>New _DELME_Station id.</returns>
        public int Insert(_DELME_Station station)
        {
            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    "insert into meta._DELME_Station (code,name, name_eng, station_type_id, addr_region_id, org_id)"
                    + " values (:code,:name, :name_eng, :station_type_id, :addr_region_id, :org_id);"
                    + "select max(id) from meta.station", cnn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.Parameters.AddWithValue("code", station.Code);
                    cmd.Parameters.AddWithValue("name", station.Name);
                    cmd.Parameters.AddWithValue("name_eng", station.NameEng);
                    cmd.Parameters.AddWithValue("station_type_id", station.TypeId);
                    cmd.Parameters.AddWithValue("addr_region_id", station.AddrRegionId);
                    cmd.Parameters.AddWithValue("org_id", station.OrgId);

                    station.Id = int.Parse(cmd.ExecuteScalar().ToString());
                }
            }
            return station.Id;
        }
        public void Update(_DELME_Station station)
        {
            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("update meta.station"
                    + " set code = :code, name = :name, name_eng = :name_eng, station_type_id=:station_type_id,"
                    + " addr_region_id = :addr_region_id, org_id = :org_id"
                    + " where id = :id", cnn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.Parameters.AddWithValue("code", station.Code);
                    cmd.Parameters.AddWithValue("name", station.Name);
                    cmd.Parameters.AddWithValue("name_eng", station.NameEng);
                    cmd.Parameters.AddWithValue("station_type_id", station.TypeId);
                    cmd.Parameters.AddWithValue("addr_region_id", station.AddrRegionId);
                    cmd.Parameters.AddWithValue("org_id", station.OrgId);
                    cmd.Parameters.AddWithValue("id", station.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public Dictionary<_DELME_Station, List<Site>> SelectStationXSites(List<int> stationIds = null)
        {
            Dictionary<_DELME_Station, List<Site>> ret = new Dictionary<_DELME_Station, List<Site>>();

            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select * from meta.station_x_sites_view"
                    + ((stationIds == null || stationIds.Count == 0) ? "" : " where id ANY(:stationId)"), cnn))
                {
                    cmd.Parameters.AddWithValue("stationId", stationIds);

                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            int id = (int)rdr["id"];
                            _DELME_Station station = ret.FirstOrDefault(x => x.Key.Id == id).Key;
                            List<Site> sites;
                            if (station == null)
                            {
                                station = (_DELME_Station)ParseData(rdr);
                                sites = new List<Site>();
                                ret.Add(station, sites);
                            }
                            else
                            {
                                sites = ret[station];
                            }
                            if (!rdr.IsDBNull(rdr.GetOrdinal("site_id")))
                                sites.Add(new Site()
                                {
                                    Id = (int)rdr["site_id"],
                                    TypeId = (int)rdr["site_type_id"],
                                    Code = ADbNpgsql.GetValueString(rdr, "site_code"),
                                    Description = ADbNpgsql.GetValueString(rdr, "site_description")
                                });
                        }
                    }
                }
            }
            return ret;
        }
        /// <summary>
        /// Выборать станцию по индексу/коду станции.
        /// </summary>
        /// <param name="stationIds">Список станций для выборки. Все, если null или пусто.</param>
        /// <returns></returns>
        public _DELME_Station Select(string index)
        {
            List<_DELME_Station> ret = SelectStationsByIndeces(new List<string>() { index });
            return ret.Count == 1 ? ret[0] : null;
        }
        public List<_DELME_Station> SelectStationsByIndeces(List<string> indeces)
        {
            List<_DELME_Station> ret = new List<_DELME_Station>();

            using (NpgsqlConnection cnn = _db.Connection)
            {
                //using (NpgsqlCommand cmd = new NpgsqlCommand("select * from meta.station_view"
                //    + " where code = any(:code)", cnn))
                using (NpgsqlCommand cmd = new NpgsqlCommand("meta.select_stations", cnn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue(":code", indeces);
                    cmd.Parameters.AddWithValue("indeces", indeces);

                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ret.Add((_DELME_Station)ParseData(rdr));
                        }
                    }
                }
            }
            return ret;
        }
        /// <summary>
        /// Выбрать станции, которые не привязаны к водным и/или географическим объектам.
        /// </summary>
        /// <returns>Список станций.</returns>
        public List<_DELME_Station> SelectWithoutGeoObject()
        {
            List<_DELME_Station> ret = new List<_DELME_Station>();

            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select * from meta._DELME_Station s"
                    + " where s.id not in (select distinct station_id from meta.station_x_geoobject)", cnn))
                {
                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ret.Add((_DELME_Station)ParseData(rdr));
                        }
                    }
                }
            }
            return ret;
        }
        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return new _DELME_Station(
                (int)rdr["id"],
                rdr["code"].ToString(),
                rdr["name"].ToString(),
                (int)rdr["station_type_id"],
                rdr["name_eng"].ToString(),
                ADbNpgsql.GetValueInt(rdr, "addr_region_id"),
                ADbNpgsql.GetValueInt(rdr, "org_id")
            );
        }
        /// <summary>
        /// Выбрать станции, у которых нет наблюдательных пунктов.
        /// </summary>
        /// <returns></returns>
        public List<_DELME_Station> SelectWithoutSites()
        {
            List<_DELME_Station> ret = new List<_DELME_Station>();

            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select * from meta.station_view s left outer join meta.site on site.station_id = s.id where site.id is null", cnn))
                {
                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ret.Add((_DELME_Station)ParseData(rdr));
                        }
                    }
                }
            }
            return ret;
        }
        /// <summary>
        /// Выбрать станции указанного региона.
        /// </summary>
        /// <param name="addrRegionId">Код региона.</param>
        /// <param name="stationTypeId">Тип станции. Все, если null.</param>
        /// <returns></returns>
        public List<_DELME_Station> SelectByAddrRegion(int addrRegionId, int? stationTypeId = null)
        {
            List<_DELME_Station> ret = new List<_DELME_Station>();

            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select * from meta.station_view where" +
                    " (:station_type_id is null or station_type_id = :station_type_id)" +
                    " and (addr_region_id = :addr_region_id)", cnn))
                {
                    cmd.Parameters.AddWithValue("station_type_id", stationTypeId);
                    cmd.Parameters.AddWithValue("addr_region_id", addrRegionId);

                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ret.Add((_DELME_Station)ParseData(rdr));
                        }
                    }
                }
            }
            return ret;
        }
    }
}
