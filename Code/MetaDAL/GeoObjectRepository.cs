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
    public class GeoObjectRepository : BaseRepository<GeoObject>
    {
        internal GeoObjectRepository(Common.ADbNpgsql db)
            : base(db, "meta.geo_object")
        {
        }
        public static List<GeoObject> GetCash()
        {
            return GetCash(DataManager.GetInstance().GeoObjectRepository);
        }

        /// <summary>
        /// Создать географический тип.
        /// </summary>
        /// <param name="item">Экземпляр класса GeoType.</param>
        /// <returns></returns>
        public int Insert(GeoObject item)
        {
            int ret;
            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("insert into meta.geo_object"
                                + "(name, geo_type_id, fall_into_id, \"order_by\")"
                                + " values (:name, :geo_type_id, :fall_into_id, :order); select max(id) from meta.geo_object", cnn))
                {
                    cmd.Parameters.AddWithValue(":name", item.Name);
                    cmd.Parameters.AddWithValue(":geo_type_id", item.GeoTypeId);
                    cmd.Parameters.Add(ADbNpgsql.GetParameter(":fall_into_id", item.FallIntoId));
                    cmd.Parameters.AddWithValue(":order", item.OrderBy);

                    ret = int.Parse(cmd.ExecuteScalar().ToString());
                }
            }
            return ret;
        }

        public List<GeoObject> SelectByType(int geoobTypeId)
        {
            return Select(new Dictionary<string, object>()
            {
                { "geo_type_id", geoobTypeId}
            });
        }

        public void Update(GeoObject go)
        {
            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("update meta.geo_object"
                                + " set name = :name, geo_type_id = :geo_type_id, fall_into_id=:fall_into_id, \"order\" = :order"
                                + " where id = :id", cnn))
                {
                    if (go.Id >= 0)
                        cmd.Parameters.AddWithValue(":id", go.Id);
                    cmd.Parameters.AddWithValue(":name", go.Name);
                    cmd.Parameters.AddWithValue(":geo_type_id", go.GeoTypeId);
                    cmd.Parameters.Add(ADbNpgsql.GetParameter(":fall_into_id", go.FallIntoId));
                    cmd.Parameters.AddWithValue(":order", go.OrderBy);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return new GeoObject(
                (int)rdr["id"],
                (int)rdr["geo_type_id"],
                rdr["name"].ToString(),
                (rdr.IsDBNull(rdr.GetOrdinal("fall_into_id"))) ? null : (int?)(int)rdr["fall_into_id"],
                (int)rdr["order_by"]
            );
        }

        /// <summary>
        /// Выстроить указанные объекты по порядку от 0.
        /// </summary>
        /// <param name="goIds"></param>
        public void UpdateGeoObjectOrder(List<int> goIds)
        {
            using (NpgsqlConnection cnn = _db.Connection)
            {
                NpgsqlTransaction tran = cnn.BeginTransaction();
                using (NpgsqlCommand cmd = new NpgsqlCommand("update meta.geo_object"
                    + " set \"order_by\" = :order"
                    + " where id = :id", cnn))
                {
                    cmd.Parameters.AddWithValue("id", 0);
                    cmd.Parameters.AddWithValue("order", 0);

                    try
                    {
                        for (int i = 0; i < goIds.Count; i++)
                        {
                            cmd.Parameters[0].Value = goIds[i];
                            cmd.Parameters[1].Value = i + 1;

                            cmd.ExecuteNonQuery();
                        }
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            tran.Rollback();
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

        public List<GeoObject> SelectInfluentTo(int geoObjectId)
        {
            List<int> ids = new List<int>();

            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select id from meta.geo_object"
                    + " where fall_into_id = :fall_into_id", cnn))
                {
                    cmd.Parameters.AddWithValue("fall_into_id", geoObjectId);

                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ids.Add((int)rdr["id"]);
                        }
                    }
                }
            }
            return ids.Count == 0 ? new List<GeoObject>() : Select(ids);
        }

        public bool[] IntersectPoints(int geoid, double[,] points)
        {
            int n = points.GetLength(0);
            bool[] result = new bool[n];

            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("meta.in_shape", cnn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_id_geo_object", geoid);
                    cmd.Parameters.AddWithValue("_points", points);

                    var rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        result = rdr[0] as bool[];
                    }
                }
            }

            return result;
        }
    }
}
