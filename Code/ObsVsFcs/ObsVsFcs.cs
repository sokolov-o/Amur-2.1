using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Amur.Data;
using SOV.Amur.Meta;
using SOV.Common;
using System.Diagnostics;

namespace SOV.Amur.ObsVsFcs
{
    public partial class ObsVsFcs
    {
        ///// <summary>
        ///// Оценить прогноз для всего периода и каждой из заблаговременностей.
        ///// </summary>
        ///// <param name="obsCtl"></param>
        ///// <param name="dateSUTC"></param>
        ///// <param name="dateFUTC"></param>
        ///// <param name="fcsMethodId"></param>
        ///// <param name="fcsSourceId"></param>
        //public static Dictionary<double, List<Estimation>> GetEstimationsByLag(Catalog obsCatalog, DateTime dateIniSUTC, DateTime dateIniFUTC,
        //    int fcsMethodId, int fcsSourceId, List<double> fcsLags, List<EnumMathVar> estimateMathVars)
        //{
        //    Dictionary<double, List<Estimation>> ret = new Dictionary<double, List<Estimation>>();

        //    // GET FCS OFFSET
        //    int[] fcsOffset = GetFcsOffset(obsCatalog.VariableId);
        //    if (fcsOffset == null)
        //    {
        //        throw new Exception("Для выбранного наблюдённого параметра " + obsCatalog.VariableId + " отсутствует соответствующий ему offset в классе Obs2Fcs." +
        //        "\nObsVariableId=" + obsCatalog.VariableId);
        //    }

        //    // GET FCS CATALOG
        //    Catalog fcsCatalog = Meta.DataManager.GetInstance().CatalogRepository.SelectForecastCatalog(
        //        obsCatalog.SiteId, obsCatalog.VariableId, fcsMethodId, fcsSourceId, fcsOffset[0], fcsOffset[1]);
        //    if ((object)fcsCatalog == null)
        //    {
        //        throw new Exception("Для выбранного наблюдённого параметра " + obsCatalog.VariableId + "отсутствует соответствующая ему запись каталога с прогнозом." +
        //        "\nObsCatalog=" + obsCatalog);
        //    }

        //    // GET OBS DATA
        //    List<DataValue> obsData = Data.DataManager.GetInstance().DataValueRepository.SelectA(dateIniSUTC, dateIniFUTC, new List<int>(new int[] { obsCatalog.Id }), true, false, null);

        //    // GET FCS DATA
        //    List<DataForecast> fcsData = Data.DataManager.GetInstance().DataForecastRepository.Select(fcsCatalog.Id,
        //        dateIniSUTC, dateIniFUTC, null, false);
        //    if (fcsLags != null)
        //        fcsData.RemoveAll(x => fcsLags.Exists(y => y != x.LagFcs));
        //    else
        //        fcsLags = fcsData.Select(x => x.LagFcs).Distinct().ToList();

        //    foreach (double lag in fcsLags)
        //    {
        //        // CONFORM OBS & FCS
        //        List<double> obs = new List<double>();
        //        List<double> fcs = new List<double>();
        //        List<DateTime> dates = new List<DateTime>();
        //        foreach (DataValue dv in obsData)
        //        {
        //            DataForecast df = fcsData.FirstOrDefault(x => x.DateFcs == dv.DateUTC && x.LagFcs == lag);
        //            if (df != null)
        //            {
        //                dates.Add(dv.DateLOC);
        //                obs.Add(dv.Value);
        //                fcs.Add(df.Value);
        //            }
        //        }
        //        dates = dates.OrderBy(x => x).ToList();
        //        // ESTIMATE
        //        List<Estimation> ests = Estimation.GetEstimations(obs.ToArray(), fcs.ToArray(), estimateMathVars.ToArray());
        //        ret.Add(lag, ests);
        //    }
        //    return ret;
        //}

        ///// <summary>
        ///// Оценить прогноз для всего периода, каждой из заблаговременностей:
        ///// 
        ///// - для суток, дня и ночи отдельно.
        ///// - для исходных 00 и 12 отдельно.
        ///// 
        ///// День: [8:00;20:00[ LOC.
        ///// 
        ///// </summary>
        ///// <param name="obsCtl"></param>
        ///// <param name="dateSUTC"></param>
        ///// <param name="dateFUTC"></param>
        ///// <param name="fcsMethodId"></param>
        ///// <param name="fcsSourceId"></param>
        ///// <returns>Dictionary<string TimePeriodName, Dictionary<double FcsLag, List<Estimation> - estimation list>></returns>
        //public static Dictionary<string, Dictionary<double, List<Estimation>>> GetEstimationsByDayNight(Catalog obsCatalog, DateTime dateIniSUTC, DateTime dateIniFUTC,
        //    int fcsMethodId, int fcsSourceId, List<double> fcsLags, List<EnumMathVar> estimateMathVars)
        //{
        //    Dictionary<string, Dictionary<double, List<Estimation>>> ret = new Dictionary<string, Dictionary<double, List<Estimation>>>();

        //    // GET FCS OFFSET
        //    int[] fcsOffset = GetFcsOffset(obsCatalog.VariableId);
        //    if (fcsOffset == null)
        //    {
        //        throw new Exception("Для выбранного наблюдённого параметра " + obsCatalog.VariableId + " отсутствует соответствующий ему offset в классе Obs2Fcs." +
        //        "\nObsVariableId=" + obsCatalog.VariableId);
        //    }

        //    // GET FCS CATALOG
        //    Catalog fcsCatalog = Meta.DataManager.GetInstance().CatalogRepository.SelectForecastCatalog(
        //        obsCatalog.SiteId, obsCatalog.VariableId, fcsMethodId, fcsSourceId, fcsOffset[0], fcsOffset[1]);
        //    if ((object)fcsCatalog == null)
        //    {
        //        throw new Exception("Для выбранного наблюдённого параметра " + obsCatalog.VariableId + "отсутствует соответствующая ему запись каталога с прогнозом." +
        //        "\nObsCatalog=" + obsCatalog);
        //    }

        //    // GET OBS DATA
        //    List<DataValue> obsData = Data.DataManager.GetInstance().DataValueRepository.SelectA(dateIniSUTC, dateIniFUTC, new List<int>(new int[] { obsCatalog.Id }), true, false, null);

        //    // GET FCS DATA
        //    List<DataForecast> fcsData = Data.DataManager.GetInstance().DataForecastRepository.Select(fcsCatalog.Id,
        //        dateIniSUTC, dateIniFUTC, null, false);
        //    if (fcsLags != null)
        //        fcsData.RemoveAll(x => fcsLags.Exists(y => y != x.LagFcs));
        //    else
        //        fcsLags = fcsData.Select(x => x.LagFcs).Distinct().ToList();

        //    // LOOP BY DAY/NIGHT
        //    string[] daynight = new string[] { "24h", "day", "night" };
        //    for (int i = 0; i < daynight.Length; i++)
        //    {
        //        List<DataValue> obsData1 =
        //            (i == 0) ? obsData
        //            : (i == 1) ? obsData.FindAll(x => x.DateLOC.Hour >= 8 && x.DateLOC.Hour < 20)
        //            : obsData.FindAll(x => !(x.DateLOC.Hour >= 8 && x.DateLOC.Hour < 20));

        //        // LOPP BY LAGS
        //        Dictionary<double, List<Estimation>> lagEstims = new Dictionary<double, List<Estimation>>();
        //        foreach (double lag in fcsLags)
        //        {
        //            // CONFORM OBS & FCS
        //            List<double> obs = new List<double>();
        //            List<double> fcs = new List<double>();
        //            List<DateTime> dates = new List<DateTime>();
        //            foreach (DataValue dv in obsData1)
        //            {
        //                DataForecast df = fcsData.FirstOrDefault(x => x.DateFcs == dv.DateUTC && x.LagFcs == lag);
        //                if (df != null)
        //                {
        //                    dates.Add(dv.DateLOC);
        //                    obs.Add(dv.Value);
        //                    fcs.Add(df.Value);
        //                }
        //            }
        //            dates = dates.OrderBy(x => x).ToList();
        //            // ESTIMATE
        //            List<Estimation> ests = Estimation.GetEstimations(obs.ToArray(), fcs.ToArray(), estimateMathVars.ToArray());
        //            lagEstims.Add(lag, ests);
        //        }
        //        ret.Add(daynight[i], lagEstims);

        //    } return ret;
        //}

        /// <summary>
        /// Оценить прогноз для всего периода, каждой из заблаговременностей:
        /// 
        /// - для суток, дня и ночи отдельно.
        /// - для исходных обоих, 00 и 12 отдельно.
        /// 
        /// День: [8:00;20:00[ LOC.
        /// 
        /// </summary>
        /// <param name="obsCtl"></param>
        /// <param name="dateSUTC"></param>
        /// <param name="dateFUTC"></param>
        /// <param name="fcsMethodId"></param>
        /// <param name="fcsSourceId"></param>
        /// <returns>Dictionary<string TimePeriodName, Dictionary<double FcsLag, List<Estimation> - estimation list>></returns>
        public static Dictionary<string, Dictionary<double, List<Estimation>>>[/*date fcs ini 00 & 12 */] GetEstimationsByDayNight0012(Catalog obsCatalog, DateTime dateIniSUTC, DateTime dateIniFUTC,
            int fcsMethodId, int fcsSourceId, List<double> fcsLags, List<EnumMathVar> estimateMathVars)
        {
            Dictionary<string, Dictionary<double, List<Estimation>>>[] ret = new Dictionary<string, Dictionary<double, List<Estimation>>>[3];

            // GET FCS OFFSET
            int[] fcsOffset = GetFcsOffset(obsCatalog.VariableId);
            if (fcsOffset == null)
            {
                throw new Exception("Для выбранного наблюдённого параметра " + obsCatalog.VariableId + " отсутствует соответствующий ему offset в классе Obs2Fcs." +
                "\nObsVariableId=" + obsCatalog.VariableId);
            }

            // GET FCS CATALOG
            Catalog fcsCatalog = Meta.DataManager.GetInstance().CatalogRepository.SelectForecastCatalog(
                obsCatalog.SiteId, obsCatalog.VariableId, fcsMethodId, fcsSourceId, fcsOffset[0], fcsOffset[1]);
            if ((object)fcsCatalog == null)
            {
                throw new Exception("Для выбранного наблюдённого параметра " + obsCatalog.VariableId + "отсутствует соответствующая ему запись каталога с прогнозом." +
                "\nObsCatalog=" + obsCatalog);
            }

            // GET OBS DATA
            List<DataValue> obsData = Data.DataManager.GetInstance().DataValueRepository.SelectA(dateIniSUTC, dateIniFUTC, new List<int>(new int[] { obsCatalog.Id }), true, false, null);

            // GET FCS DATA
            List<DataForecast> fcsData = Data.DataManager.GetInstance().DataForecastRepository.Select(fcsCatalog.Id,
                dateIniSUTC, dateIniFUTC, null, false);
            if (fcsLags != null)
                fcsData.RemoveAll(x => fcsLags.Exists(y => y != x.LagFcs));
            else
                fcsLags = fcsData.Select(x => x.LagFcs).Distinct().ToList();

            // DAY/NIGHT
            string[] daynightPeriod = new string[] { "24h", "day", "night" };

            for (int i0012 = 0; i0012 < 3; i0012++)
            {
                Dictionary<string, Dictionary<double, List<Estimation>>> ret0012 = new Dictionary<string, Dictionary<double, List<Estimation>>>();
                List<DataForecast> fcsData1 =
                    (i0012 == 0) ? fcsData
                    : (i0012 == 1) ? fcsData.FindAll(x => x.DateIni.Hour == 00)
                    : fcsData.FindAll(x => x.DateIni.Hour == 12);

                // LOOP BY DAY/NIGHT
                for (int iPeriod = 0; iPeriod < daynightPeriod.Length; iPeriod++)
                {
                    List<DataValue> obsData1 =
                        (iPeriod == 0) ? obsData
                        : (iPeriod == 1) ? obsData.FindAll(x => x.DateLOC.Hour >= 8 && x.DateLOC.Hour < 20)
                        : obsData.FindAll(x => !(x.DateLOC.Hour >= 8 && x.DateLOC.Hour < 20));

                    // LOPP BY LAGS
                    Dictionary<double, List<Estimation>> lagEstims = new Dictionary<double, List<Estimation>>();
                    foreach (double lag in fcsLags)
                    {
                        // CONFORM OBS & FCS
                        List<double> obs = new List<double>();
                        List<double> fcs = new List<double>();
                        List<DateTime> dates = new List<DateTime>();
                        foreach (DataValue dv in obsData1)
                        {
                            DataForecast df = fcsData1.FirstOrDefault(x => x.DateFcs == dv.DateUTC && x.LagFcs == lag);
                            if (df != null)
                            {
                                dates.Add(dv.DateLOC);
                                obs.Add(dv.Value);
                                fcs.Add(df.Value);
                            }
                        }
                        dates = dates.OrderBy(x => x).ToList();
                        // ESTIMATE
                        List<Estimation> ests = Estimation.GetEstimations(obs.ToArray(), fcs.ToArray(), estimateMathVars.ToArray());
                        if (ests == null)
                        {
                            Debug.WriteLine("(ests == null)");
                        }
                        lagEstims.Add(lag, ests);
                    }
                    ret0012.Add(daynightPeriod[iPeriod], lagEstims);
                }
                ret[i0012] = ret0012;
            }
            return ret;
        }

        static public string ToString(Dictionary<double, List<Estimation>> lagEstims, char splitter = ';')
        {
            string ret = "Lag;MathVar;Value;Count";

            foreach (KeyValuePair<double, List<Estimation>> kvp in lagEstims)
            {
                foreach (Estimation est in kvp.Value)
                {
                    ret += "\r\n" + kvp.Key + ";" + est;
                }
            }
            return ret.Replace(';', splitter);
        }
        static public string ToString(Dictionary<string, Dictionary<double, List<Estimation>>> dnLagEstims, char splitter = ';')
        {
            string ret = "DayNight;Lag;MathVar;Value;Count";

            foreach (KeyValuePair<string, Dictionary<double, List<Estimation>>> kvp1 in dnLagEstims)
            {
                foreach (KeyValuePair<double, List<Estimation>> kvp2 in kvp1.Value)
                {
                    foreach (Estimation est in kvp2.Value)
                    {
                        ret += "\r\n" + kvp1.Key + ";" + +kvp2.Key + ";" + est;
                    }
                }

            } return ret.Replace(';', splitter);
        }
        static public string ToString(Dictionary<string, Dictionary<double, List<Estimation>>>[] dnLag0012Estims, char splitter = ';')
        {
            string ret = "IniTime;DayNight;Lag;MathVar;Value;Count";
            string[] iniTime = new string[] { "00_12", "00", "12" };

            for (int i = 0; i < dnLag0012Estims.Length; i++)
            {
                foreach (KeyValuePair<string, Dictionary<double, List<Estimation>>> kvp1 in dnLag0012Estims[i])
                {
                    foreach (KeyValuePair<double, List<Estimation>> kvp2 in kvp1.Value)
                    {
                        if (kvp2.Value == null)
                            ret += "\r\n" + iniTime[i] + ";" + kvp1.Key + ";" + +kvp2.Key;
                        else
                        {
                            foreach (Estimation est in kvp2.Value)
                            {
                                ret += "\r\n" + iniTime[i] + ";" + kvp1.Key + ";" + +kvp2.Key + ";" + (est == null ? "" : est.ToString());
                            }
                        }
                    }
                }
            }
            return ret.Replace(';', splitter);
        }
        static public void ToFile(string filePath, Dictionary<string, Dictionary<double, List<Estimation>>> obj, char splitter = ';')
        {
            ToFile(filePath, ToString(obj, splitter));
        }
        static public void ToFile(string filePath, Dictionary<string, Dictionary<double, List<Estimation>>>[] obj, char splitter = ';')
        {
            ToFile(filePath, ToString(obj, splitter));
        }
        static public void ToFile(string filePath, string str)
        {
            System.IO.StreamWriter sw = null;
            try
            {
                sw = new System.IO.StreamWriter(filePath, false);
                sw.WriteLine(str);
            }
            finally
            {
                if (sw != null) sw.Close();
            }
        }
        static public void ToFile(string filePath, Dictionary<double, List<Estimation>> obj, char splitter = ';')
        {
            ToFile(filePath, ToString(obj, splitter));
        }
    }
}
