using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using FERHRI.Amur.Data;
using FERHRI.Common;
using System.Diagnostics;

namespace Import
{
    /// <summary>
    /// Импорт расходных кривых из БД ПУГМС в БД "Амур"
    /// OSokolov@201712
    /// </summary>
    public class CurveImport
    {
        /// <summary>
        /// Соответствие переменных ПУГМС <-> АМУР
        /// </summary>
        Dictionary<int, int> var_x_var = new Dictionary<int, int>() { { 2, 2 }, { 14, 14 } };

        static internal void Run(string pugmsConnectionString)
        {
            // READ CURVES FROM PUGMS
            Console.WriteLine("READ CURVES FROM PUGMS...");
            List<PUGMSNS.Curve> pCurves = PUGMSNS.Curve.ReadAll(pugmsConnectionString);

            // Convert curves PUGMS -> AMUR
            Console.WriteLine("Convert curves PUGMS -> AMUR...");
            List<FERHRI.Amur.Data.Curve> aCurves = ConvertCurves_PUGMS2Amur(pCurves, pugmsConnectionString);

            // WRITE CURVES TO AMUR
            Console.Write("WRITE CURVES TO AMUR...");
            DataManager.GetInstance().CurveRepository.InsertCurves(aCurves);
            Console.WriteLine(aCurves.Count + " inserted.");
        }

        static List<Curve> ConvertCurves_PUGMS2Amur(List<PUGMSNS.Curve> pugmsCurves, string pugmsConnectionString)
        {
            List<Curve> aCurves = new List<Curve>();
            // Соответствие id пункта ПУГМС <-> Амур
            Dictionary<int, int> site_x_site = new Dictionary<int, int>();
            bool ok = true;

            // SCAN PCURVES

            foreach (var pCurve in pugmsCurves)
            {
                // GET Amur SITE ID 4 CURVE

                int aSiteId;
                if (!site_x_site.TryGetValue(pCurve.SiteID, out aSiteId))
                {
                    using (SqlConnection cnn = new SqlConnection(pugmsConnectionString))
                    {
                        cnn.Open();
                        using (SqlCommand cmd = new SqlCommand("select sitecode from sites where sitetypeid = 2 and siteid = " + pCurve.SiteID, cnn))
                        {
                            using (SqlDataReader rdr = cmd.ExecuteReader())
                            {
                                if (!rdr.Read())
                                    throw new Exception("В ПУГМС отсутствует ГП с id=" + pCurve.SiteID);
                                string pSiteCode = rdr["sitecode"].ToString();

                                // Amur station
                                FERHRI.Amur.Meta.Station station = FERHRI.Amur.Meta.DataManager.GetInstance().StationRepository.Select(pSiteCode);
                                if (station == null)
                                    throw new Exception("В БД Амур отсутствует станция с индексом " + pSiteCode);
                                if (station.TypeId != (int)FERHRI.Amur.Meta.EnumStationType.HydroPost)
                                {
                                    //throw new Exception("В БД Амур станция с индексом " + pSiteCode + " не является ГП.");
                                    Debug.Write("* В БД Амур станция с индексом " + pSiteCode + " не является ГП.");
                                    ok = false;
                                }

                                // Amur station site's
                                List<FERHRI.Amur.Meta.Site> sites = FERHRI.Amur.Meta.DataManager.GetInstance().SiteRepository.SelectByStations(new List<int>() { station.Id });
                                if (sites.Count == 0)
                                    throw new Exception("В БД Амур отсутствуют пункты для станции id = " + station.Id);

                                // Amur site
                                sites = sites.FindAll(x => x.SiteTypeId == (int)FERHRI.Amur.Meta.EnumStationType.HydroPost);
                                if (sites == null || sites.Count == 0)
                                {
                                    //throw new Exception("В БД Амур отсутствует пункт типа ГП для станции id = " + station.Id);
                                    Debug.Write("В БД Амур отсутствует пункт типа ГП для станции index = " + pSiteCode);
                                    ok = false;
                                }
                                if (sites.Count > 1)
                                    throw new Exception("В БД Амур присутствует не один пункт типа ГП для станции id = " + station.Id);

                                aSiteId = 0;
                                if (ok)
                                {
                                    aSiteId = sites[0].Id;
                                    site_x_site.Add(pCurve.SiteID, aSiteId);
                                }
                            }
                        }
                    }
                }

                FERHRI.Amur.Data.Curve aCurve = new Curve()
                {
                    Id = -1,
                    Description = "Imported from PUGMS at " + DateTime.Now,
                    Series = new List<Curve.Seria>(),
                    CatalogIdX = -1,
                    CatalogIdY = -1
                };

                // GET AMUR CURVE CATALOGS 4 SITE & VAR'S

                if (ok)
                {
                    // X
                    FERHRI.Amur.Meta.Catalog ctl = new FERHRI.Amur.Meta.Catalog()
                    {
                        Id = -1,
                        SiteId = aSiteId,
                        VariableId = (int)FERHRI.Amur.Meta.EnumVariable.GageHeightF,
                        MethodId = (int)FERHRI.Amur.Meta.EnumMethod.ObservationInSitu,
                        SourceId = 243, // ПУГМС
                        OffsetTypeId = (int)FERHRI.Amur.Meta.EnumOffsetType.NoOffset,
                        OffsetValue = 0,
                        ParentId = 0
                    };
                    FERHRI.Amur.Meta.Catalog ctl1 = FERHRI.Amur.Meta.DataManager.GetInstance().CatalogRepository.Select(ctl.SiteId, ctl.VariableId, ctl.MethodId, ctl.SourceId, ctl.OffsetTypeId, ctl.OffsetValue);
                    if ((object)ctl1 == null)
                    {
                        ctl = FERHRI.Amur.Meta.DataManager.GetInstance().CatalogRepository.Insert(ctl);
                        aCurve.CatalogIdX = ctl.Id;
                    }
                    else
                        aCurve.CatalogIdX = ctl1.Id;

                    // Y
                    ctl.Id = -1;
                    ctl.VariableId = (int)FERHRI.Amur.Meta.EnumVariable.Discharge;
                    ctl1 = FERHRI.Amur.Meta.DataManager.GetInstance().CatalogRepository.Select(ctl.SiteId, ctl.VariableId, ctl.MethodId, ctl.SourceId, ctl.OffsetTypeId, ctl.OffsetValue);
                    if ((object)ctl1 == null)
                    {
                        ctl = FERHRI.Amur.Meta.DataManager.GetInstance().CatalogRepository.Insert(ctl);
                        aCurve.CatalogIdY = ctl.Id;
                    }
                    else
                        aCurve.CatalogIdY = ctl1.Id;
                }

                FERHRI.Amur.Data.Curve aCurve1 = aCurves.FirstOrDefault(x => x.CatalogIdX == aCurve.CatalogIdX && x.CatalogIdY == aCurve.CatalogIdY);
                if (aCurve1 == null)
                    aCurves.Add(aCurve);
                else
                    aCurve = aCurve1;

                // GET AMUR CURVE SERIES

                foreach (var pSeria in pCurve.Series)
                {
                    if (!aCurve.Series.Exists(x => x.DateS.Date == pSeria.BeginDate.Date))
                    {
                        FERHRI.Amur.Data.Curve.Seria aSeria = new Curve.Seria()
                        {
                            Id = -1,
                            CurveId = -1,
                            CurveSeriaTypeId = (int)pCurve.CurveTypeId,
                            Description = "Imported from PUGMS at " + DateTime.Now,
                            DateS = pSeria.BeginDate,
                            Coefs = new List<Curve.Seria.Coef>(),
                            Points = new List<Curve.Seria.Point>()
                        };

                        foreach (var pCoef in pSeria.Coefs)
                        {
                            aSeria.Coefs.Add(new Curve.Seria.Coef() { Day = (int)pCoef[0], Month = (int)pCoef[1], Value = pCoef[2] });
                        }

                        foreach (var pPoint in pSeria.Points)
                        {
                            aSeria.Points.Add(new Curve.Seria.Point() { X = pPoint[0], Y = pPoint[1] });
                        }

                        aCurve.Series.Add(aSeria);
                    }
                }
            }

            return ok ? aCurves : null;
        }
    }
}

namespace PUGMSNS
{
    internal class Curve
    {
        internal int CurveID;
        internal string Description;
        internal DateTime? BeginDate;
        internal DateTime? EndDate;
        internal int SiteID;
        internal int SiteXID;
        internal int SiteYID;
        internal int VariableXID;
        internal int VariableYID;
        internal int? CurveTypeId;

        internal List<Seria> Series = new List<Seria>();

        /// <summary>
        /// READ CURVES FROM PUGMS
        /// </summary>
        /// <param name="connectionString">2 mssql db</param>
        /// <returns></returns>
        static internal List<Curve> ReadAll(string connectionString)
        {
            List<Curve> ret = new List<Curve>();

            // READ CURVE HEADERS

            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                using (SqlCommand cmd = new SqlCommand("select * from curves", cnn))
                {
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            PUGMSNS.Curve curve = new Curve()
                            {
                                CurveID = (int)rdr["curveid"],
                                CurveTypeId = ADbMSSql.GetValueInt(rdr, "curvetypeid"),
                                BeginDate = ADbMSSql.GetValueDateTime(rdr, "begindate"),
                                EndDate = ADbMSSql.GetValueDateTime(rdr, "enddate"),
                                Description = rdr["Description"].ToString(),
                                SiteID = (int)rdr["SiteID"],
                                SiteXID = (int)rdr["SiteXID"],
                                SiteYID = (int)rdr["SiteYID"],
                                VariableXID = (int)rdr["VariableXID"],
                                VariableYID = (int)rdr["VariableYID"]
                            };
                            curve.CurveTypeId = curve.CurveTypeId.HasValue ? curve.CurveTypeId : 1;

                            if (curve.SiteID != curve.SiteXID || curve.SiteID != curve.SiteYID
                                || curve.VariableXID != 2 || curve.VariableYID != 14
                                || curve.CurveTypeId != 1)
                                throw new Exception("Непонятная кривая в БД ПУГМС.");
                            ret.Add(curve);
                        }
                    }
                }
            }
            // READ CURVE SERIES

            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                using (SqlCommand cmd = new SqlCommand("select * from curveseries where curveid = @curveid", cnn))
                {
                    cmd.Parameters.AddWithValue("@curveid", 0);

                    for (int i = 0; i < ret.Count; i++)
                    {
                        cmd.Parameters[0].Value = ret[i].CurveID;

                        // READ SERIES
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                Curve.Seria seria = new Curve.Seria()
                                {
                                    SeriesID = (int)rdr["SeriesID"],
                                    CurveID = (int)rdr["CurveID"],
                                    BeginDate = (DateTime)ADbMSSql.GetValueDateTime(rdr, "begindate"),
                                    EndDate = ADbMSSql.GetValueDateTime(rdr, "enddate"),
                                    Description = rdr["Description"].ToString()
                                };
                                ret[i].Series.Add(seria);

                            }
                        }

                        for (int j = 0; j < ret[i].Series.Count; j++)
                        {
                            // READ POINTS
                            using (SqlCommand cmd1 = new SqlCommand("select * from curveseriespoints where seriesid = @seriesid", cnn))
                            {
                                cmd1.Parameters.AddWithValue("@seriesid", ret[i].Series[j].SeriesID);

                                using (SqlDataReader rdr1 = cmd1.ExecuteReader())
                                {
                                    while (rdr1.Read())
                                    {
                                        ret[i].Series[j].Points.Add(new double[] { (double)rdr1["x"], (double)rdr1["y"] });
                                    }
                                }
                            }
                            // READ COEFS
                            using (SqlCommand cmd1 = new SqlCommand("select * from curveseriescoeffs where curveserieid = @curveserieid", cnn))
                            {
                                cmd1.Parameters.AddWithValue("@curveserieid", ret[i].Series[j].SeriesID);

                                using (SqlDataReader rdr1 = cmd1.ExecuteReader())
                                {
                                    while (rdr1.Read())
                                    {
                                        ret[i].Series[j].Coefs.Add(new double[] { (int)rdr1["day"], (int)rdr1["month"], (double)rdr1["value"] });
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return ret;
        }

        internal class Seria
        {
            internal int CurveID;
            internal int SeriesID;
            internal string Description;
            internal DateTime BeginDate;
            internal DateTime? EndDate;

            internal List<double[/*day,month,value tripplets*/]> Coefs = new List<double[]>();
            internal List<double[/*x,y pairs*/]> Points = new List<double[]>();
        }
    }
}
