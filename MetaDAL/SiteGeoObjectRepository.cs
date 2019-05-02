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
    public class SiteGeoObjectRepository : BaseRepository<SiteGeoObject>
    {
        internal SiteGeoObjectRepository(Common.ADbNpgsql db) : base(db, "meta.site_x_geoobject") { }

        /// <summary>
        /// Связать станцию с географическим объектом.
        /// </summary>
        /// <param name="item">Экземпляр класса.</param>
        /// <returns></returns>
        public void Insert(SiteGeoObject item)
        {
            Insert(new Dictionary<string, object>()
                {
                    { "site_id",item.SiteId},
                    { "geo_object_id",item.GeoObjectId},
                    { "order_by",item.OrderBy}
                }
            );
        }

        /// <summary>
        /// Вставка с неизвестным порядком. Порядок будет установлен на 1 меньше минимального существующего
        /// или 0, если к объекту не привязана ни одна станция.
        /// </summary>
        /// <param name="geoObjectId">Код объекта.</param>
        /// <param name="siteId">Код станции.</param>
        public void Insert(int geoObjectId, int siteId)
        {
            int? order_by = SelectMinOrderBy(geoObjectId);
            Insert(new SiteGeoObject(siteId, geoObjectId, order_by.HasValue ? (int)order_by - 1 : 0));
        }

        private int? SelectMinOrderBy(int geoObjectId)
        {
            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select min(\"order_by\") from meta.site_x_geoobject"
                                + " where geo_object_id = " + geoObjectId, cnn))
                {
                    object o = cmd.ExecuteScalar();
                    return (o == System.DBNull.Value) ? null : (int?)int.Parse(o.ToString());
                }
            }
        }

        //////public Dictionary<Site, List<GeoObject>> SelectWithFK(int siteId)
        //////{
        //////    return SelectByStationsFK(new List<int>(new int[] { siteId }));
        //////}

        //public Dictionary<Site, List<GeoObject>> SelectByStationsFK(List<int> stationIdList)
        //{
        //    Dictionary<Site, List<GeoObject>> ret = new Dictionary<Site, List<GeoObject>>();

        //    List<SiteGeoObject> sgo = SelectByStations(stationIdList);
        //    foreach (var item in sgo)
        //    {
        //        IEnumerable<Site> stationi = ret.Keys.Where(x => x.Id == item.SiteId);
        //        Site site;
        //        if (stationi.Count() == 0)
        //        {
        //            site = DataManager.GetInstance(_db.ConnectionString).SiteRepository.Select(item.SiteId);
        //            ret.Add(site, new List<GeoObject>());
        //        }
        //        else
        //            site = stationi.ElementAt(0);

        //        if (!ret[site].Exists(x => x.Id == item.GeoObjectId))
        //        {
        //            ret[site].Add(DataManager.GetInstance(_db.ConnectionString).GeoObjectRepository.Select(item.GeoObjectId));
        //        }
        //    }
        //    return ret;
        //    return null;
        //}
        public static Dictionary<GeoObject, List<Site>> ToDictionaryGeoobSites(List<SiteGeoObject> siteGeoObjects)
        {
            // READ ALL GEOOBS & SITES
            List<GeoObject> geoobs = DataManager.GetInstance().GeoObjectRepository.Select(siteGeoObjects.Select(x => x.GeoObjectId).Distinct().ToList());
            List<Site> sites = DataManager.GetInstance().SiteRepository.Select(siteGeoObjects.Select(x => x.SiteId).Distinct().ToList());

            // MAKE Dictionary
            Dictionary<GeoObject, List<Site>> ret = new Dictionary<GeoObject, List<Site>>();

            foreach (var geoob in geoobs)
            {
                List<int> siteIds = siteGeoObjects.Where(y => y.GeoObjectId == geoob.Id).Select(x => x.SiteId).ToList();
                List<Site> siteList = sites.Where(x => siteIds.Exists(y => y == x.Id)).ToList();

                ret.Add(geoob, siteList);
            }

            return ret;
        }
        public static Dictionary<Site, List<GeoObject>> ToDictionarySiteGeoobs(List<SiteGeoObject> siteGeoObjects)
        {
            // READ ALL GEOOBS & SITES
            List<GeoObject> geoobs = DataManager.GetInstance().GeoObjectRepository.Select(siteGeoObjects.Select(x => x.GeoObjectId).Distinct().ToList());
            List<Site> sites = DataManager.GetInstance().SiteRepository.Select(siteGeoObjects.Select(x => x.SiteId).Distinct().ToList());

            // MAKE Dictionary
            Dictionary<Site, List<GeoObject>> ret = new Dictionary<Site, List<GeoObject>>();

            foreach (var site in sites)
            {
                List<int> geoobIds = siteGeoObjects.Where(y => y.SiteId == site.Id).Select(x => x.GeoObjectId).ToList();
                List<GeoObject> geoobList = geoobs.Where(x => geoobIds.Exists(y => y == x.Id)).ToList();

                ret.Add(site, geoobList);
            }

            return ret;
        }

        public Dictionary<GeoObject, List<Site>> SelectGeoobSites()
        {
            return ToDictionaryGeoobSites(Select());

        }
        public Dictionary<GeoObject, List<Site>> SelectByGeoobs(List<int> idGeoobs)
        {
            return ToDictionaryGeoobSites(Select(idGeoobs));

        }

        public List<SiteGeoObject> SelectBySites(List<int> siteIds)
        {
            return Select(new Dictionary<string, object>()
                {
                    {"site_id", siteIds}
                }
            );
        }

        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return new SiteGeoObject((int)rdr["site_id"], (int)rdr["geoob_id"], (int)rdr["order_by"]);
        }

        ///////// <summary>
        ///////// Выборка с сортировкой по order_by.
        ///////// </summary>
        ///////// <param name="goIds">Коды гео-объектов для выборки.</param>
        ///////// <returns></returns>
        //////public List<SiteGeoObject> SelectByGeoObjects(List<int> goIds = null)
        //////{
        //////    List<SiteGeoObject> ret = new List<SiteGeoObject>();

        //////    using (NpgsqlConnection cnn = _db.Connection)
        //////    {
        //////        using (NpgsqlCommand cmd = new NpgsqlCommand("select * from meta.station_x_geoobject"
        //////            + ((goIds == null) ? "" : " where geo_object_id = ANY(:geo_object_id)")
        //////            + " order_by by \"order_by\"", cnn))
        //////        {
        //////            cmd.Parameters.AddWithValue("geo_object_id", goIds);
        //////            using (NpgsqlDataReader rdr = cmd.ExecuteReader())
        //////            {
        //////                while (rdr.Read())
        //////                {
        //////                    ret.Add(new SiteGeoObject(
        //////                        (int)rdr["site_id"],
        //////                        (int)rdr["geo_object_id"],
        //////                        (int)rdr["order_by"]
        //////                    ));
        //////                }
        //////                return ret;
        //////            }
        //////        }
        //////    }
        //////}

        ///////// <summary>
        ///////// Выборка с сортировкой по order_by.
        ///////// </summary>
        ///////// <param name="goIds">Коды гео-объектов для выборки.</param>
        ///////// <returns></returns>
        //////public Dictionary<GeoObject, List<Site>> SelectByGeoObjectsFK(List<int> goIds = null)
        //////{
        //////    Dictionary<GeoObject, List<Site>> ret = new Dictionary<GeoObject, List<Site>>();

        //////    List<SiteGeoObject> sgo = SelectByGeoObjects(goIds);
        //////    foreach (var item in sgo)
        //////    {
        //////        IEnumerable<GeoObject> goi = ret.Keys.Where(x => x.Id == item.GeoObjectId);
        //////        GeoObject go;
        //////        if (goi.Count() == 0)
        //////        {
        //////            go = DataManager.GetInstance(_db.ConnectionString).GeoObjectRepository.Select(item.GeoObjectId);
        //////            ret.Add(go, new List<Site>());
        //////        }
        //////        else
        //////            go = goi.ElementAt(0);

        //////        if (!ret[go].Exists(x => x.Id == item.SiteId))
        //////        {
        //////            ret[go].Add(DataManager.GetInstance(_db.ConnectionString).SiteRepository.Select(item.SiteId));
        //////        }
        //////    }
        //////    return ret;
        //////}

        public void UpdateSitesOrderBy(int geoObjectId, List<int> stationIds)
        {
            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("update meta.station_x_geoobject"
                    + " set order_by = :order_by"
                    + " where site_id = :site_id and geo_object_id = :geo_object_id", cnn))
                {
                    cmd.Parameters.AddWithValue("site_id", 0);
                    cmd.Parameters.AddWithValue("geo_object_id", geoObjectId);
                    cmd.Parameters.AddWithValue("order_by", 0);

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

        //////public void Delete(int geoObjectId, int stationId)
        //////{
        //////    using (NpgsqlConnection cnn = _db.Connection)
        //////    {
        //////        using (NpgsqlCommand cmd = new NpgsqlCommand("delete from meta.station_x_geoobject"
        //////            + " where site_id = " + stationId + " and geo_object_id = " + geoObjectId, cnn))
        //////        {
        //////            cmd.ExecuteNonQuery();
        //////        }
        //////    }
        //////}

        //////public Dictionary<GeoObject, List<Site>> SelectGeoObjectsXSites(List<int> goIds = null)
        //////{
        //////    Dictionary<GeoObject, List<Site>> ret = new Dictionary<GeoObject, List<Site>>();
        //////    List<Site> sites;

        //////    using (NpgsqlConnection cnn = _db.Connection)
        //////    {
        //////        using (NpgsqlCommand cmd = new NpgsqlCommand(
        //////            "select site_id, geoob_id, order_by from meta.site_x_geo_object where :goids is null or geo_object_id = ANY(:goids)", cnn))
        //////        {
        //////            cmd.Parameters.AddWithValue("goids", goIds);

        //////            using (NpgsqlDataReader rdr = cmd.ExecuteReader())
        //////            {
        //////                while (rdr.Read())
        //////                {
        //////                    int goId = (int)rdr[1];
        //////                    sites = null;
        //////                    GeoObject go = ret.Keys.FirstOrDefault(x => x.Id == goId);
        //////                    if (go == null)
        //////                    {
        //////                        sites = new List<Site>();
        //////                        ret.Add(new GeoObject() { Id = goId, OrderBy }, sites);
        //////                    }
        //////                    else
        //////                    {
        //////                        sites = ret[go];
        //////                    }
        //////                    sites.Add(new Site() { Id = (int)rdr[0] });
        //////                }
        //////            }
        //////        }
        //////    }

        //////    // UPDATE GEOOB & SITES
        //////    sites = DataManager.GetInstance().SiteRepository.Select( ret.SelectMany(x => x.Value).Select(x=>x.Id).Distinct().ToList());

        //////    foreach (var kvp in ret)
        //////    {
        //////        kvp.Value.ForEach(x => x = sites.FirstOrDefault(y => y.Id == x.Id));
        //////    }
        //////    return ret;
        //////}

        //////public Dictionary<Site, List<GeoObject>> SelectSiteXGeoObjects(List<int> siteIdList = null)
        //////{
        //////    Dictionary<Site, List<GeoObject>> ret = new Dictionary<Site, List<GeoObject>>();
        //////    using (NpgsqlConnection cnn = _db.Connection)
        //////    {
        //////        using (NpgsqlCommand cmd = new NpgsqlCommand(
        //////            "select * from meta.site_x_geo_objects_view"
        //////            + ((siteIdList == null) ? "" : " where site_id = ANY(:siteids)"), cnn))
        //////        {
        //////            cmd.Parameters.AddWithValue("siteids", siteIdList);

        //////            using (NpgsqlDataReader rdr = cmd.ExecuteReader())
        //////            {
        //////                while (rdr.Read())
        //////                {
        //////                    int siteId = (int)rdr["site_id"];
        //////                    List<GeoObject> gos = null;
        //////                    Site site = ret.Keys.FirstOrDefault(x => x.Id == siteId);
        //////                    if (site == null)
        //////                    {
        //////                        gos = new List<GeoObject>();
        //////                        ret.Add(
        //////                            new Site(
        //////                                siteId,
        //////                                (int)rdr["site_id"],
        //////                                (int)rdr["site_type_id"],
        //////                                ADbNpgsql.GetValueString(rdr, "site_code"),
        //////                                ADbNpgsql.GetValueString(rdr, "description")
        //////                            ),
        //////                            gos);
        //////                    }
        //////                    else
        //////                    {
        //////                        gos = ret[site];
        //////                    }
        //////                    if (!rdr.IsDBNull(rdr.GetOrdinal("geo_object_id")))
        //////                        gos.Add((GeoObject)ParseDataGeoob(rdr));
        //////                }
        //////                return ret;
        //////            }
        //////        }
        //////    }
        //////}

        //////public object ParseDataGeoob(NpgsqlDataReader rdr)
        //////{
        //////    return new GeoObject
        //////    (
        //////        (int)rdr["geo_object_id"],
        //////        (int)rdr["geo_type_id"],
        //////        rdr["geo_object_name"].ToString(),
        //////        (rdr.IsDBNull(rdr.GetOrdinal("fall_into_id"))) ? null : (int?)(int)rdr["fall_into_id"],
        //////        (int)rdr["geo_object_order_by"]
        //////    )
        //////    {
        //////        Shape = (double[,])rdr["geo_shape"]
        //////    };
        //////}
    }
}
