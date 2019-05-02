using System;
using System.Collections.Generic;
using SOV.Common;
using Npgsql;

namespace SOV.Amur.Data
{
    public class CurveRepository
    {
        ADbNpgsql _db;
        internal CurveRepository(Common.ADbNpgsql db)
        {
            _db = db;
        }
        /// <summary>
        /// Выбрать кривые.
        /// </summary>
        /// <param name="catalog_id_x">wysiwyg</param>
        /// <param name="catalog_id_y">wysiwyg</param>
        /// <param name="seriaDateSInterval">Интервал дат начала действия кривой.</param>
        /// <returns>Набор кривых для указанного интервала.</returns>
        public Dictionary<int, List<Curve>> SelectCurvesBySites(List<int> sitesId, int variableIdX, int variableIdY,
            int curve_seria_type_id, DateTime seriaDateSS, DateTime seriaDateSF)
        {
            Dictionary<int, List<Curve>> ret = new Dictionary<int, List<Curve>>();

            // LOOP SITES

            foreach (var siteId in sitesId)
            {
                List<Curve> curves = new List<Curve>();

                // GET CATALOGS

                Meta.CatalogFilter cf = new Meta.CatalogFilter()
                {
                    Sites = new List<int>() { siteId },
                    Variables = new List<int>() { variableIdX },
                };
                List<Meta.Catalog> catalogsX = Meta.DataManager.GetInstance().CatalogRepository.Select(cf);
                cf.Variables = new List<int>() { variableIdY };
                List<Meta.Catalog> catalogsY = Meta.DataManager.GetInstance().CatalogRepository.Select(cf);

                // GET CURVES

                if (catalogsX.Count != 0 && catalogsY.Count != 0)
                {
                    foreach (var catalogX in catalogsX)
                    {
                        foreach (var catalogY in catalogsY)
                        {
                            List<Curve> curve = SelectCurvesByCatalog(catalogX.Id, catalogY.Id, curve_seria_type_id, seriaDateSS, seriaDateSF);
                            curves.AddRange(curve);
                        }
                    }
                }
                ret.Add(siteId, curves);
            }
            return ret;
        }
        /// <summary>
        /// Выбрать шапки всех кривых без серий значений (каталог кривых)
        /// для заданного периода времени.
        /// </summary>
        /// <param name="seriaDateSS">Начало периода.</param>
        /// <param name="seriaDateSF">Окончание периода.</param>
        /// <returns></returns>
        public List<Curve> SelectAllCurvesNoSeries(DateTime seriaDateSS, DateTime seriaDateSF)
        {
            return SelectCurvesByCatalog(null, null, null, seriaDateSS, seriaDateSF, false);
        }
        /// <summary>
        /// Выбрать кривые для заданных id каталогов, типа серии кривой и периода времени действия кривой.
        /// Выборка производится с сериями значений или без них (каталог кривых).
        /// </summary>
        /// <param name="catalog_id_x">wysiwyg</param>
        /// <param name="catalog_id_y">wysiwyg</param>
        /// <param name="<param name="curve_seria_type_id">">Тип серий кривых или все типы, если null.</param>
        /// <param name="seriaDateSInterval">Интервал дат начала действия кривой.</param>
        /// <returns>Набор кривых для указанного интервала.</returns>
        public List<Curve> SelectCurvesByCatalog(int? catalog_id_x, int? catalog_id_y, int? curve_seria_type_id,
             DateTime seriaDateSS, DateTime seriaDateSF, bool withSeries = true)
        {
            List<Curve> ret = new List<Curve>();
            using (var cnn = _db.Connection)
            {
                using (Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand(
                    "select * from data.curve" +
                    " where (:catalog_id_x::integer is null or catalog_id_x = :catalog_id_x::integer)" +
                    " and   (:catalog_id_y::integer is null or catalog_id_y = :catalog_id_y::integer)"
                    , cnn))
                {
                    cmd.Parameters.Add(ADbNpgsql.GetParameter("catalog_id_x", catalog_id_x));
                    cmd.Parameters.Add(ADbNpgsql.GetParameter("catalog_id_y", catalog_id_y));

                    using (Npgsql.NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ret.Add((Curve)Parse(rdr));
                        }
                    }
                }
            }

            return withSeries
                ? DataManager.GetInstance().CurveSeriaRepository.SelectSeries4Curves(ret, curve_seria_type_id, seriaDateSS, seriaDateSF)
                : ret;

        }
        public Object Parse(NpgsqlDataReader rdr)
        {
            return new Curve()
            {
                Id = (int)rdr["id"],
                CatalogIdX = (int)rdr["catalog_id_x"],
                CatalogIdY = (int)rdr["catalog_id_y"],
                Description = ADbNpgsql.GetValueString(rdr, "description"),
                Series = new List<Curve.Seria>()
            };
        }
        /// <summary>
        /// Записать кривые.
        /// </summary>
        /// <returns>Набор кривых для указанного интервала.</returns>
        public void InsertCurves(List<Curve> curves)
        {
            foreach (var curve in curves)
            {
                InsertCurve(curve);
            }
        }
        /// <summary>
        /// Записать кривую.
        /// </summary>
        /// <returns>Набор кривых для указанного интервала.</returns>
        public int InsertCurve(Curve curve)
        {
            using (var cnn = _db.Connection)
            {
                using (Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand("insert into data.curve (catalog_id_x, catalog_id_y, description) values (:catalog_id_x, :catalog_id_y, :description); select lastval();", cnn))
                {
                    cmd.Parameters.AddWithValue("catalog_id_x", curve.CatalogIdX);
                    cmd.Parameters.AddWithValue("catalog_id_y", curve.CatalogIdY);
                    cmd.Parameters.AddWithValue("description", curve.Description);

                    curve.Id = int.Parse(cmd.ExecuteScalar().ToString());
                    curve.Series.ForEach(x => x.CurveId = curve.Id);

                    DataManager.GetInstance().CurveSeriaRepository.InsertCurveSeries(curve.Series);

                    return curve.Id;
                }
            }
        }
    }
}
