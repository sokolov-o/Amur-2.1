using System;
using System.Collections.Generic;
using System.Linq;
using SOV.Common;
using Npgsql;

namespace SOV.Amur.Data
{
    public class CurveSeriaRepository
    {
        Common.ADbNpgsql _db;
        internal CurveSeriaRepository(ADbNpgsql db)
        {
            _db = db;
        }

        public List<Curve.Seria> SelectSeries4CurveIds(List<int> curveIds, int? curve_seria_type_id, DateTime seriaDateSS, DateTime seriaDateSF,
            bool headersOnly = false)
        {
            List<Curve.Seria> ret = new List<Curve.Seria>();
            List<int> seriaIds = new List<int>();

            using (var cnn = _db.Connection)
            {
                using (Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand(
                    "select id from data.curve_seria cs"
                    + " LEFT join"
                    + "   (select curve_id, curve_seria_type_id, max(date_s) max_date from data.curve_seria where date_s <= :date_s_s"
                    + "    group by curve_id, curve_seria_type_id"
                    + "   ) maxd on maxd.curve_id = cs.curve_id and maxd.curve_seria_type_id = cs.curve_seria_type_id"
                    + " where (cs.curve_id = any(:curve_id))"
                    + " and   (:curve_seria_type_id is null or cs.curve_seria_type_id = :curve_seria_type_id::integer)"
                    + " and   (date_s = maxd.max_date or (date_s > :date_s_s and date_s <= :date_s_f))"
                    , cnn))
                {
                    cmd.Parameters.AddWithValue("curve_id", curveIds.ToArray());
                    cmd.Parameters.AddWithValue("date_s_s", seriaDateSS);
                    cmd.Parameters.Add(ADbNpgsql.GetParameter("curve_seria_type_id", curve_seria_type_id));
                    cmd.Parameters[2].DbType = System.Data.DbType.Int32;
                    cmd.Parameters.AddWithValue("date_s_f", seriaDateSF);

                    using (Npgsql.NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            seriaIds.Add((int)rdr[0]);
                        }
                    }
                }
            }
            return SelectSeries(seriaIds, headersOnly);
        }
        public List<Curve.Seria> SelectSeries(List<int> seriaIds, bool headersOnly = false)
        {
            List<Curve.Seria> ret = new List<Curve.Seria>();

            using (var cnn = _db.Connection)
            {
                using (Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand("select * from data.curve_seria where (id = any(:ids))", cnn))
                {
                    cmd.Parameters.AddWithValue("ids", seriaIds);

                    using (Npgsql.NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Curve.Seria seria = (Curve.Seria)Parse(rdr);
                            if (headersOnly)
                            {
                                seria.Points.Clear();
                                seria.Coefs.Clear();
                            }
                            ret.Add(seria);
                        }
                    }
                }
            }
            return ret;
        }

        internal List<Curve> SelectSeries4Curves(List<Curve> curves, int? curve_seria_type_id, DateTime seriaDateSS, DateTime seriaDateSF)
        {
            List<Curve.Seria> series = SelectSeries4CurveIds(curves.Select(x => x.Id).ToList(), curve_seria_type_id, seriaDateSS, seriaDateSF, false);
            curves.ForEach(x => x.Series = series.FindAll(y => y.CurveId == x.Id));
            return curves;
        }

        public Object Parse(NpgsqlDataReader rdr)
        {

            Curve.Seria seria = new Curve.Seria()
            {
                Id = (int)rdr["id"],
                CurveId = (int)rdr["curve_id"],
                CurveSeriaTypeId = (int)rdr["curve_seria_type_id"],
                DateS = (DateTime)rdr["date_s"],
                Description = ADbNpgsql.GetValueString(rdr, "description")
            };
            object o = ADbNpgsql.GetValue(rdr, "points");
            if (o != null)
            {
                seria.Points = new List<Curve.Seria.Point>();
                foreach (var point in (NpgsqlTypes.NpgsqlPoint[])o)
                {
                    seria.Points.Add(new Curve.Seria.Point() { X = point.X, Y = point.Y });
                }
            }
            o = ADbNpgsql.GetValue(rdr, "coefs");
            if (o != null)
            {
                seria.Coefs = Curve.Seria.Coef.FromDouble((double[])o);
            }
            return seria;
        }
        ///// <summary>
        ///// Выбрать кривые.
        ///// </summary>
        ///// <param name="catalog_id_x">wysiwyg</param>
        ///// <param name="catalog_id_y">wysiwyg</param>
        ///// <param name="seriaDateSInterval">Интервал дат начала действия кривой.</param>
        ///// <returns>Набор кривых для указанного интервала.</returns>
        //public int InsertCurve(Curve curve)
        //{
        //    using (var cnn = _db.Connection)
        //    {
        //        using (Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand("insert into data.curve (catalog_id_x, catalog_id_y, description) values (:catalog_id_x, :catalog_id_y, :description); select lastval();", cnn))
        //        {
        //            cmd.Parameters.AddWithValue("catalog_id_x", curve.CatalogIdX);
        //            cmd.Parameters.AddWithValue("catalog_id_y", curve.CatalogIdY);
        //            cmd.Parameters.AddWithValue("description", curve.Description);

        //            curve.Id = int.Parse(cmd.ExecuteScalar().ToString());

        //            InsertCurveSeries(curve.Series);

        //            return curve.Id;
        //        }
        //    }
        //}
        public void InsertCurveSeries(List<Curve.Seria> series)
        {
            if (series == null || series.Count == 0)
                return;

            using (var cnn = _db.Connection)
            {
                using (Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand(
                    "insert into data.curve_seria (curve_id, curve_seria_type_id, date_s, description, points, coefs)" +
                    " values (:curve_id, :curve_seria_type_id, :date_s, :description, :points, :coefs);", cnn))
                {
                    cmd.Parameters.AddWithValue("curve_id", 0);
                    cmd.Parameters.AddWithValue("curve_seria_type_id", 0);
                    cmd.Parameters.AddWithValue("date_s", DateTime.Today);
                    cmd.Parameters.AddWithValue("description", "");
                    cmd.Parameters.AddWithValue("points", new NpgsqlTypes.NpgsqlPoint[1]);
                    cmd.Parameters.AddWithValue("coefs", new double[1]);

                    foreach (var seria in series)
                    {
                        int i = 0;
                        cmd.Parameters[i++].Value = seria.CurveId;
                        cmd.Parameters[i++].Value = seria.CurveSeriaTypeId;
                        cmd.Parameters[i++].Value = seria.DateS;
                        cmd.Parameters[i++].Value = seria.Description;
                        cmd.Parameters[i++].Value = seria.Points == null || seria.Points.Count == 0 ? DBNull.Value : (object)ToNpgsqlPoints(seria.Points);
                        cmd.Parameters[i++].Value = seria.Coefs == null || seria.Coefs.Count == 0 ? DBNull.Value : (object)Curve.Seria.Coef.ToDouble(seria.Coefs);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        public void UpdateCurveSeria(int seriaId, DateTime dateS, string description, List<Curve.Seria.Point> points, List<Curve.Seria.Coef> coefs)
        {
            using (var cnn = _db.Connection)
            {
                using (Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand(
                    "update data.curve_seria set date_s=:date_s, description=:description, points=:points, coefs=:coefs" +
                    " where id = :seria_id", cnn))
                {
                    cmd.Parameters.AddWithValue("seria_id", seriaId);
                    cmd.Parameters.AddWithValue("date_s", dateS);
                    cmd.Parameters.AddWithValue("description", description);
                    cmd.Parameters.AddWithValue("points", ToNpgsqlPoints(points));
                    cmd.Parameters.AddWithValue("coefs", Curve.Seria.Coef.ToDouble(coefs));

                    cmd.ExecuteNonQuery();
                }
            }
        }
        private NpgsqlTypes.NpgsqlPoint[] ToNpgsqlPoints(List<Curve.Seria.Point> points)
        {
            if (points == null || points.Count == 0) return null;

            List<NpgsqlTypes.NpgsqlPoint> ret = new List<NpgsqlTypes.NpgsqlPoint>();
            foreach (var point in points)
            {
                ret.Add(new NpgsqlTypes.NpgsqlPoint() { X = (float)point.X, Y = (float)point.Y });
            }
            return ret.ToArray();
        }
    }
}
