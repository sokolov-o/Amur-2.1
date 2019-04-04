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
    public class StationGeoObjectRepository
    {
        Common.ADbNpgsql _db;
        internal StationGeoObjectRepository(Common.ADbNpgsql db)
        {
            _db = db;
        }
        /// <summary>
        /// Связать станцию с географическим объектом.
        /// </summary>
        /// <param name="item">Экземпляр класса.</param>
        /// <returns></returns>
        public void Insert(StationGeoObject item)
        {
            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("insert into meta.station_x_geoobject"
                                + "(station_id, geo_object_id, \"order\")"
                                + " values (:station_id,:geo_object_id,:order)", cnn))
                {
                    cmd.Parameters.AddWithValue("station_id", item.StationId);
                    cmd.Parameters.AddWithValue("geo_object_id", item.GeoObjectId);
                    cmd.Parameters.Add(ADbNpgsql.GetParameter("order", item.Order));

                    cmd.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// Вставка с неизвестным порядком. Порядок будет установлен на 1 меньше минимального существующего
        /// или 0, если к объекту не привязана ни одна станция.
        /// </summary>
        /// <param name="geoObjectId">Код объекта.</param>
        /// <param name="stationId">Код станции.</param>
        public void Insert(int geoObjectId, int stationId)
        {
            int? order = SelectMinOrder(geoObjectId);
            Insert(new StationGeoObject(stationId, geoObjectId, order.HasValue ? (int)order - 1 : 0));
        }

        private int? SelectMinOrder(int geoObjectId)
        {
            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select min(\"order\") from meta.station_x_geoobject"
                                + " where geo_object_id = " + geoObjectId, cnn))
                {
                    object o = cmd.ExecuteScalar();
                    return (o == System.DBNull.Value) ? null : (int?)int.Parse(o.ToString());
                }
            }
        }

        public Dictionary<Station, List<GeoObject>> SelectWithFK(int stationId)
        {
            return SelectByStationsFK(new List<int>(new int[] { stationId }));
        }
        public Dictionary<Station, List<GeoObject>> SelectByStationsFK(List<int> stationIdList)
        {
            Dictionary<Station, List<GeoObject>> ret = new Dictionary<Station, List<GeoObject>>();

            List<StationGeoObject> sgo = SelectByStations(stationIdList);
            foreach (var item in sgo)
            {
                IEnumerable<Station> stationi = ret.Keys.Where(x => x.Id == item.StationId);
                Station station;
                if (stationi.Count() == 0)
                {
                    station = DataManager.GetInstance(_db.ConnectionString).StationRepository.Select(item.StationId);
                    ret.Add(station, new List<GeoObject>());
                }
                else
                    station = stationi.ElementAt(0);

                if (!ret[station].Exists(x => x.Id == item.GeoObjectId))
                {
                    ret[station].Add(DataManager.GetInstance(_db.ConnectionString).GeoObjectRepository.Select(item.GeoObjectId));
                }
            }
            return ret;
        }
        public List<StationGeoObject> SelectByStations(List<int> stationIdList)
        {
            List<StationGeoObject> ret = new List<StationGeoObject>();
            if (stationIdList != null && stationIdList.Count > 0)
            {
                string sqlIn = StrVia.ToString(stationIdList);

                using (NpgsqlConnection cnn = _db.Connection)
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from meta.station_x_geoobject where station_id in (" + sqlIn + ")", cnn))
                    {
                        using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                ret.Add(new StationGeoObject(
                                    (int)rdr["station_id"],
                                    (int)rdr["geo_object_id"],
                                    (int)rdr["order"]
                                ));
                            }
                        }
                    }
                }
            }
            return ret;
        }
        /// <summary>
        /// Выборка с сортировкой по order.
        /// </summary>
        /// <param name="goIds">Коды гео-объектов для выборки.</param>
        /// <returns></returns>
        public List<StationGeoObject> SelectByGeoObjects(List<int> goIds = null)
        {
            List<StationGeoObject> ret = new List<StationGeoObject>();

            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select * from meta.station_x_geoobject"
                    + ((goIds == null) ? "" : " where geo_object_id = ANY(:geo_object_id)")
                    + " order by \"order\"", cnn))
                {
                    cmd.Parameters.AddWithValue("geo_object_id", goIds);
                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ret.Add(new StationGeoObject(
                                (int)rdr["station_id"],
                                (int)rdr["geo_object_id"],
                                (int)rdr["order"]
                            ));
                        }
                        return ret;
                    }
                }
            }
        }
        /// <summary>
        /// Выборка с сортировкой по order.
        /// </summary>
        /// <param name="goIds">Коды гео-объектов для выборки.</param>
        /// <returns></returns>
        public Dictionary<GeoObject, List<Station>> SelectByGeoObjectsFK(List<int> goIds = null)
        {
            Dictionary<GeoObject, List<Station>> ret = new Dictionary<GeoObject, List<Station>>();

            List<StationGeoObject> sgo = SelectByGeoObjects(goIds);
            foreach (var item in sgo)
            {
                IEnumerable<GeoObject> goi = ret.Keys.Where(x => x.Id == item.GeoObjectId);
                GeoObject go;
                if (goi.Count() == 0)
                {
                    go = DataManager.GetInstance(_db.ConnectionString).GeoObjectRepository.Select(item.GeoObjectId);
                    ret.Add(go, new List<Station>());
                }
                else
                    go = goi.ElementAt(0);

                if (!ret[go].Exists(x => x.Id == item.StationId))
                {
                    ret[go].Add(DataManager.GetInstance(_db.ConnectionString).StationRepository.Select(item.StationId));
                }
            }
            return ret;
        }

        public void UpdateStationOrder(int geoObjectId, List<int> stationIds)
        {
            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("update meta.station_x_geoobject"
                    + " set \"order\" = :order"
                    + " where station_id = :station_id and geo_object_id = :geo_object_id", cnn))
                {
                    cmd.Parameters.AddWithValue("station_id", 0);
                    cmd.Parameters.AddWithValue("geo_object_id", geoObjectId);
                    cmd.Parameters.AddWithValue("order", 0);

                    try
                    {
                        cmd.Transaction = cnn.BeginTransaction();

                        for (int i = 0; i < stationIds.Count; i++)
                        {
                            cmd.Parameters[0].Value = stationIds[i];
                            cmd.Parameters[2].Value = i + 1;

                            cmd.ExecuteNonQuery();
                        }

                        cmd.Transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            cmd.Transaction.Rollback();
                        }
                        catch (Exception ex1)
                        {
                            throw new Exception(ex1.ToString(), ex);
                        }
                        throw new Exception("Порядок пунктов в пределах водного объекта не сохранён.", ex);
                    }
                }
            }
        }

        public void Delete(int geoObjectId, int stationId)
        {
            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("delete from meta.station_x_geoobject"
                    + " where station_id = " + stationId + " and geo_object_id = " + geoObjectId, cnn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public Dictionary<GeoObject, List<Station>> SelectGeoObjectsXStations(List<int> goIds = null)
        {
            Dictionary<GeoObject, List<Station>> ret = new Dictionary<GeoObject, List<Station>>();
            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    "select * from meta.geo_object_x_stations_view where :goids is null or geo_object_id = ANY(:goids)", cnn))
                {
                    cmd.Parameters.AddWithValue("goids", goIds);

                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            int goId = (int)rdr["geo_object_id"];
                            List<Station> stations = null;
                            GeoObject go = ret.Keys.FirstOrDefault(x => x.Id == goId);
                            if (go == null)
                            {
                                stations = new List<Station>();
                                ret.Add(
                                    new GeoObject(
                                        goId,
                                        (int)rdr["geo_type_id"],
                                        rdr["geo_object_name"].ToString(),
                                        (rdr.IsDBNull(rdr.GetOrdinal("fall_into_id"))) ? null : (int?)(int)rdr["fall_into_id"],
                                        (int)rdr["geo_object_order"]),
                                    stations);
                            }
                            else
                            {
                                stations = ret[go];
                            }
                            if (!rdr.IsDBNull(rdr.GetOrdinal("station_id")))
                                stations.Add(new Station(
                                    (int)rdr["station_id"],
                                    rdr["station_code"].ToString(),
                                    rdr["station_name"].ToString(),
                                    (int)rdr["station_type_id"],
                                    rdr["station_name_eng"].ToString(),
                                    ADbNpgsql.GetValueInt(rdr, "addr_region_id"),
                                    ADbNpgsql.GetValueInt(rdr, "org_id")
                                ));
                        }
                        return ret;
                    }
                }
            }
        }
        public Dictionary<Site, List<GeoObject>> SelectSiteXGeoObjects(List<int> siteIdList = null)
        {
            Dictionary<Site, List<GeoObject>> ret = new Dictionary<Site, List<GeoObject>>();
            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    "select * from meta.site_x_geo_objects_view"
                    + ((siteIdList == null) ? "" : " where site_id = ANY(:siteids)"), cnn))
                {
                    cmd.Parameters.AddWithValue("siteids", siteIdList);

                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            int siteId = (int)rdr["site_id"];
                            List<GeoObject> gos = null;
                            Site site = ret.Keys.FirstOrDefault(x => x.Id == siteId);
                            if (site == null)
                            {
                                gos = new List<GeoObject>();
                                ret.Add(
                                    new Site(
                                        siteId,
                                        (int)rdr["station_id"],
                                        (int)rdr["site_type_id"],
                                        ADbNpgsql.GetValueString(rdr, "site_code"),
                                        ADbNpgsql.GetValueString(rdr, "description")
                                    ),
                                    gos);
                            }
                            else
                            {
                                gos = ret[site];
                            }
                            if (!rdr.IsDBNull(rdr.GetOrdinal("geo_object_id")))
                                gos.Add((GeoObject)ParseDataGeoob(rdr));
                        }
                        return ret;
                    }
                }
            }
        }
        public object ParseDataGeoob(NpgsqlDataReader rdr)
        {
            return new GeoObject
            (
                (int)rdr["geo_object_id"],
                (int)rdr["geo_type_id"],
                rdr["geo_object_name"].ToString(),
                (rdr.IsDBNull(rdr.GetOrdinal("fall_into_id"))) ? null : (int?)(int)rdr["fall_into_id"],
                (int)rdr["geo_object_order"]
            )
            {
                Shape = (double[,])rdr["geo_shape"]
            };
        }
    }
}
