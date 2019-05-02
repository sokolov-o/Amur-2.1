using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOV.Common
{
    public class Estimation
    {
        public EnumMathVar MathVar;
        public int ElemQ;
        public double Value;

        public Estimation(EnumMathVar mathVar, double value, int elemQ = -1)
        {
            this.MathVar = mathVar;
            this.Value = value;
            this.ElemQ = elemQ;
        }
        public override string ToString()
        {
            return MathVar + ";" + Value.ToString("F2") + ";" + ElemQ;
        }

        public static Estimation Corr(double[] dataA, double[] dataB)
        {
            double[] dataCorr = MathSupport.Corr(dataA, dataB);
            double value = double.NaN;
            int elemQ = 0;
            if (dataCorr != null)
            {
                value = dataCorr[0];
                elemQ = (int)dataCorr[1];
                return new Estimation(EnumMathVar.R, value, elemQ);
            }
            return null;
        }
        //41	SIGMA_WDIR_SIN	Оценка направления ветра (Наставление по службе прог., 1981)
        /// <summary>
        /// Оценка направления ветра (входные данные в градусах)
        /// </summary>
        /// <param name="dataA"></param>
        /// <param name="dataB"></param>
        /// <returns></returns>
        public static Estimation SigmaWdirSin(double[] dataA, double[] dataB)
        {
            double value = 0;
            int elemQ = 0;

            for (int i = 0; i < dataA.Length; i++)
            {
                if (!Double.IsNaN(dataB[i]) && !Double.IsNaN(dataA[i]))
                {
                    double r = Math.Abs(Vector.GradBetweenSign(dataA[i], dataB[i]));
                    if (r <= 45)
                        value++;
                    else
                        if (r <= 90)
                            value += 0.5;
                    elemQ++;
                }
            }
            if (elemQ == 0) { return null; }
            value = 100 * value / elemQ;
            return new Estimation(EnumMathVar.SIGMA_WDIR_SIN, value, elemQ);
        }
        public static Estimation SigmaWdirRD(double[] dataA, double[] dataB, double minAngl, double maxAngl, EnumMathVar mathvar)
        {
            double value = 0;
            int elemQ = 0, q = 0;

            for (int i = 0; i < dataA.Length; i++)
            {
                if (!Double.IsNaN(dataB[i]) && !Double.IsNaN(dataA[i]))
                {
                    double r = Math.Abs(Vector.GradBetweenSign(dataA[i], dataB[i]));
                    if (r <= maxAngl && r >= minAngl)
                        q++;
                    elemQ++;
                }
            }
            if (elemQ == 0) { return null; }
            value = 100.0 * q / elemQ;
            return new Estimation(mathvar, value, elemQ);
        }
        public static Estimation DevAbs(double[] dataA, double[] dataB)
        {
            double value = 0;
            int elemQ = 0;

            for (int i = 0; i < dataA.Length; i++)
            {
                if (!Double.IsNaN(dataB[i]) && !Double.IsNaN(dataA[i]))
                {
                    value += Math.Abs(dataA[i] - dataB[i]);
                    elemQ++;
                }
            }
            return (elemQ == 0) ? null : new Estimation(EnumMathVar.ABSDEV, value / elemQ, elemQ);
        }
        public static Estimation Dev2(double[] dataA, double[] dataB)
        {
            double value = 0.0;
            int elemQ = 0;
            for (int i = 0; i < dataA.Length; i++)
            {
                if (!Double.IsNaN(dataB[i]) && !Double.IsNaN(dataA[i]))
                {
                    value += Math.Pow((dataA[i] - dataB[i]), 2);
                    elemQ++;
                }
            }
            if (elemQ == 0) { return null; }
            value = value / elemQ;
            return new Estimation(EnumMathVar.DEV2, value, elemQ);
        }
        public static Estimation DevAvg(double[] dataA, double[] dataB)
        {
            double value = 0;
            int elemQ = 0;
            for (int i = 0; i < dataA.Length; i++)
            {
                if (!Double.IsNaN(dataB[i]) && !Double.IsNaN(dataA[i]))
                {
                    value += dataA[i] - dataB[i];
                    elemQ++;
                }
            }
            if (elemQ == 0) { return null; }
            value = value / elemQ;
            return new Estimation(EnumMathVar.DEVAVG, value, elemQ);
        }
        public static Estimation DevAbsMax(double[] dataA, double[] dataB)
        {
            double value = 0;
            int elemQ = 0;
            for (int i = 0; i < dataA.Length; i++)
            {
                if (!Double.IsNaN(dataB[i]) && !Double.IsNaN(dataA[i]))
                {
                    value = Math.Max(value, Math.Abs(dataA[i] - dataB[i]));
                    elemQ++;
                }
            }
            if (elemQ == 0) { return null; }
            return new Estimation(EnumMathVar.ABSDEVMAX, value, elemQ);
        }
        public static Estimation DevAbsMin(double[] dataA, double[] dataB)
        {
            double value = int.MaxValue;
            int elemQ = 0;
            for (int i = 0; i < dataA.Length; i++)
            {
                if (!Double.IsNaN(dataB[i]) && !Double.IsNaN(dataA[i]))
                {
                    value = Math.Min(value, Math.Abs(dataA[i] - dataB[i]));
                    elemQ++;
                }
            }
            if (elemQ == 0) { return null; }
            return new Estimation(EnumMathVar.ABSDEVMIN, value, elemQ);
        }
        public static Estimation DevMax(double[] dataA, double[] dataB)
        {
            double value = 0;
            int elemQ = 0;
            for (int i = 0; i < dataA.Length; i++)
            {
                if (!Double.IsNaN(dataB[i]) && !Double.IsNaN(dataA[i]))
                {
                    value = Math.Max(value, dataA[i] - dataB[i]);
                    elemQ++;
                }
            }
            if (elemQ == 0) { return null; }
            return new Estimation(EnumMathVar.DEVAVG, value, elemQ);
        }
        /// <summary>
        /// Параметр P - показатель качества прогноза месячной суммы осадков (стр. 19, ф. 4  "Наставление..., 1986, р.2, ч.VI, Москва, ГМИздат").
        /// </summary>
        /// <param name="dataFct">фактические данные</param>
        /// <param name="dataFcs">прогностические данные</param>
        /// <param name="avgFct">климат фактических данных</param>
        /// <returns></returns>
        static public Estimation P82RRMonth(double[] dataFct, double[] dataFcs, double avgFct)
        {
            if (Double.IsNaN(avgFct))
            {
                return null;
            }
            double[] avgFctArr = Array.allocFill(dataFct.Length, avgFct);
            return P82RRMonth(dataFct, dataFcs, avgFctArr);
        }
        /// <summary>
        /// Параметр P - показатель качества прогноза месячной суммы осадков (стр. 19, ф. 4  "Наставление..., 1986, р.2, ч.VI, Москва, ГМИздат").
        /// </summary>
        /// <param name="dataFct">фактические данные</param>
        /// <param name="dataFcs">прогностические данные</param>
        /// <param name="avgFct">климат фактических данных</param>
        /// <returns></returns>
        static public Estimation P82RRMonth(double[] dataFct, double[] dataFcs, double[] avgFct)
        {
            int elemQ = 0;
            double est = 0;
            for (int i = 0; i < dataFct.Length; i++)
            {
                double estI = P82RRMonth(dataFct[i], dataFcs[i], avgFct[i]);
                if (!double.IsNaN(estI))
                {
                    est += estI;
                    elemQ++;
                }
            }
            double value = double.NaN;
            if (elemQ > 0)
            {
                value = est / elemQ;
            }
            return new Estimation(EnumMathVar.P82RRMONTH, value, elemQ);
        }
        /// <summary>
        /// Параметр P - показатель качества прогноза месячной суммы осадков (стр. 19, ф. 4  "Наставление..., 1986, р.2, ч.VI, Москва, ГМИздат").
        /// </summary>
        /// <param name="dataFct"></param>
        /// <param name="dataFcs"></param>
        /// <param name="avgFct"></param>
        /// <returns></returns>
        static public double P82RRMonth(double dataFct, double dataFcs, double avgFct)
        {
            double ft = dataFct / avgFct;
            double fs = dataFcs / avgFct;
            int Nft, Nfs;
            if (ft > 1.2)
            {
                Nft = 1;
            }
            else
            {
                Nft = ((ft >= 0.8)) ? 2 : 3;
            }
            if (fs > 1.2)
            {
                Nfs = 1;
            }
            else
            {
                Nfs = ((fs >= 0.8)) ? 2 : 3;
            }

            if (Math.Abs(Nft - Nfs) == 0)
            {
                return 100;
            }
            else
            {
                return (Math.Abs(Nft - Nfs) == 1) ? 50 : 0;
            }

        }
        /// <summary>
        /// Разность оценки качества месячных прогнозов осадков (P82RRMONTH) и оценки климатического прогноза
        /// </summary>
        /// <param name="dataFct">фактические данные</param>
        /// <param name="dataFcs">прогностические данные</param>
        /// <param name="avgFct">климат фактических данных</param>
        /// <returns></returns>
        static public Estimation P82RR_minus_P_Clm(double[] dataFct, double[] dataFcs, double avgFct)
        {
            if (Double.IsNaN((double)avgFct))
            {
                return null;
            }
            double[] avgFctArr = Array.allocFill(dataFct.Length, (double)avgFct);
            return P82RR_minus_P_Clm(dataFct, dataFcs, avgFctArr);
        }
        /// <summary>
        /// Разность оценки качества месячных прогнозов осадков (P82RRMONTH) и оценки климатического прогноза
        /// </summary>
        /// <param name="dataFct">фактические данные</param>
        /// <param name="dataFcs">прогностические данные</param>
        /// <param name="avgFct">климат фактических данных</param>
        /// <returns></returns>
        static public Estimation P82RR_minus_P_Clm(double[] dataFct, double[] dataFcs, double[] avgFct)
        {
            int elemQ = 0;
            Estimation u, uo;
            u = P82RRMonth(dataFct, dataFcs, avgFct);
            double[] dataAvgFct = new double[dataFct.Length];
            for (int i = 0; i < dataFct.Length; i++)
            {
                dataAvgFct[i] = avgFct[i];
            }
            uo = P82RRMonth(dataFct, dataAvgFct, avgFct);
            double value = double.NaN;
            if (!double.IsNaN(u.Value) && !double.IsNaN(uo.Value))
            {
                value = u.Value - uo.Value;
                elemQ = u.ElemQ;
            }
            return new Estimation(EnumMathVar.P82RR_minus_P_CLM, value, elemQ);

        }
        /// <summary>
        /// Отношение оценки качества месячных прогнозов осадков (P82RRMONTH) к оценке климатического прогноза
        /// </summary>
        /// <param name="dataFct">фактические данные</param>
        /// <param name="dataFcs">прогностические данные</param>
        /// <param name="avgFct">климат фактических данных</param>
        /// <returns></returns>
        static public Estimation P82RR_P_Clm(double[] dataFct, double[] dataFcs, double avgFct)
        {
            if (Double.IsNaN((double)avgFct))
            {
                return null;
            }
            double[] avgFctArr = Array.allocFill(dataFct.Length, (double)avgFct);
            return P82RR_P_Clm(dataFct, dataFcs, avgFctArr);
        }
        /// <summary>
        /// Отношение оценки качества месячных прогнозов осадков (P82RRMONTH) к оценке климатического прогноза
        /// </summary>
        /// <param name="dataFct">фактические данные</param>
        /// <param name="dataFcs">прогностические данные</param>
        /// <param name="avgFct">климат фактических данных</param>
        /// <returns></returns>
        static public Estimation P82RR_P_Clm(double[] dataFct, double[] dataFcs, double[] avgFct)
        {
            int elemQ = 0;
            Estimation u, uo;
            u = P82RRMonth(dataFct, dataFcs, avgFct);
            double[] dataAvgFct = new double[dataFct.Length];
            for (int i = 0; i < dataFct.Length; i++)
            {
                dataAvgFct[i] = avgFct[i];
            }
            uo = P82RRMonth(dataFct, dataAvgFct, avgFct);
            double value = double.NaN;
            if (!double.IsNaN(u.Value) && !double.IsNaN(uo.Value))
            {
                value = u.Value / uo.Value;
                elemQ = u.ElemQ;
            }
            return new Estimation(EnumMathVar.P82RR_P_CLM, value, elemQ);

        }
        public static Estimation PKQQQ(double[] dataA, double[] dataB, double? stdNm1, double stdNm1Tend)
        {
            if (stdNm1 == null || Double.IsNaN((double)stdNm1))
            {
                return null;
            }
            double sigma = (double)stdNm1;
            if (!Double.IsNaN((double)stdNm1Tend))
            {
                sigma = Math.Min(sigma, (double)stdNm1Tend);
            }
            double[] est = PKQQQ(dataA, dataB, sigma);
            return new Estimation(EnumMathVar.P_KQQQ, est[0], (int)est[1]);
        }
        /// <summary>
        /// Оценка прогноза  аномалий ср. мес. Та по классам (стр. 15 "Наставление..., 1986, р.2, ч.VI, Москва, ГМИздат")
        /// </summary>
        /// <param name="dataFct">фактические данные</param>
        /// <param name="dataFcs">прогностические данные</param>
        /// <param name="avgFct">климат фактических данных</param>
        /// <returns></returns>
        static public Estimation P86TAMonthCls(double[] dataFct, double[] dataFcs, double avgFct)
        {
            if (Double.IsNaN((double)avgFct))
            {
                return null;
            }
            double[] avgFctArr = Array.allocFill(dataFct.Length, (double)avgFct);
            return P86TAMonthCls(dataFct, dataFcs, avgFctArr);
        }
        /// <summary>
        /// Оценка прогноза  аномалий ср. мес. Та по классам (стр. 15 "Наставление..., 1986, р.2, ч.VI, Москва, ГМИздат")
        /// </summary>
        /// <param name="dataFct">фактические данные</param>
        /// <param name="dataFcs">прогностические данные</param>
        /// <param name="avgFct">климат фактических данных</param>
        /// <returns></returns>
        static public Estimation P86TAMonthCls(double[] dataFct, double[] dataFcs, double[] avgFct)
        {
            int elemQ = 0;
            double dfct, dfcs;
            int Nfct, Nfcs, S = 0;
            for (int i = 0; i < dataFct.Length; i++)
            {
                if (!double.IsNaN(dataFct[i]) & !double.IsNaN(dataFcs[i]))
                {
                    dfct = dataFct[i] - avgFct[i];
                    dfcs = dataFcs[i] - avgFct[i];
                    if (dfct > 1)
                    {
                        Nfct = 1;
                    }
                    else
                    {
                        Nfct = ((dfct >= -1)) ? 2 : 3;
                    }
                    if (dfcs > 1)
                    {
                        Nfcs = 1;
                    }
                    else
                    {
                        Nfcs = ((dfcs >= -1)) ? 2 : 3;
                    }
                    if (Nfcs == 2)
                    {
                        switch (Nfct)
                        {
                            case 1:
                                S += 25;
                                break;
                            case 2:
                                S += 100;
                                break;
                            case 3:
                                S += 25;
                                break;
                        }
                    }
                    else if (Math.Abs(Nfcs - Nfct) == 0)
                    {
                        S += 100;
                    }
                    else
                    {
                        S += (Math.Abs(Nfcs - Nfct) == 1) ? 50 : 0;
                    }
                    elemQ++;
                }
            }
            double value = double.NaN;
            if (elemQ > 0)
            {
                value = (double)S / elemQ;
            }
            return new Estimation(EnumMathVar.P86TAMONTH_cls, value, elemQ);
        }
        /// <summary>
        /// Параметр P - оправд-ть аномалии месячной Та (стр.14,  "Наставление..., 1986, р.2, ч.VI, Москва, ГМИздат").
        /// </summary>
        /// <param name="dataFct">фактические данные</param>
        /// <param name="dataFcs">прогностические данные</param>
        /// <param name="avgFct">климат фактических данных</param>
        /// <returns></returns>
        static public Estimation P86TaMonth(double[] dataFct, double[] dataFcs, double avgFct)
        {
            if (Double.IsNaN((double)avgFct))
            {
                return null;
            }
            double[] avgFctArr = Array.allocFill(dataFct.Length, (double)avgFct);
            return P86TaMonth(dataFct, dataFcs, avgFctArr);
        }
        /// <summary>
        /// Параметр P - оправд-ть аномалии месячной Та (стр.14,  "Наставление..., 1986, р.2, ч.VI, Москва, ГМИздат").
        /// </summary>
        /// <param name="dataFct">фактические данные</param>
        /// <param name="dataFcs">прогностические данные</param>
        /// <param name="avgFct">климат фактических данных</param>
        /// <returns></returns>
        static public Estimation P86TaMonth(double[] dataFct, double[] dataFcs, double[] avgFct)
        {
            int elemQ = 0;
            double dfct, dfcs;
            double q = 0;
            for (int i = 0; i < dataFct.Length; i++)
            {
                if (!double.IsNaN(dataFct[i]) && !double.IsNaN(dataFcs[i]) && !double.IsNaN(avgFct[i]))
                {
                    dfct = dataFct[i] - avgFct[i];
                    dfcs = dataFcs[i] - avgFct[i];
                    q += P86TaMonth(dfct, dfcs);
                    elemQ++;
                }
            }
            double value = double.NaN;
            if (elemQ > 0)
            {
                value = 1 * (double)q / elemQ;
            }
            return new Estimation(EnumMathVar.P86TAMONTH, value, elemQ);
        }
        /// <summary>
        /// Параметр P - оправд-ть аномалии месячной Та (стр.14,  "Наставление..., 1986, р.2, ч.VI, Москва, ГМИздат").
        /// </summary>
        /// <param name="dataFct">фактические данные</param>
        /// <param name="dataFcs">прогностические данные</param>
        /// <param name="avgFct">климат фактических данных</param>
        /// <returns></returns>
        static public Estimation PZHURTA(double[] dataFct, double[] dataFcs, double[] avgFct)
        {
            int elemQ = 0;
            double[] fctAnom = new double[dataFct.Length];
            double[] fcsAnom = new double[dataFct.Length];
            Array.Fill(fctAnom, double.NaN);
            Array.Fill(fcsAnom, double.NaN);
            for (int i = 0; i < dataFct.Length; i++)
            {
                if (!double.IsNaN(dataFct[i]) && !double.IsNaN(dataFcs[i]) && !double.IsNaN(avgFct[i]))
                {
                    fctAnom[i] = dataFct[i] - avgFct[i];
                    fcsAnom[i] = dataFcs[i] - avgFct[i];
                    elemQ++;
                }
            }
            double value = double.NaN;
            if (elemQ > 0)
            {
                double dfct = MathSupport.Avg(fctAnom)[0];
                double dfcs = MathSupport.Avg(fcsAnom)[0];
                value = P86TaMonth(dfct, dfcs);
            }
            return new Estimation(EnumMathVar.PZHURTA, value, elemQ);
        }
        static public Estimation PZHURRR(double[] dataFct, double[] dataFcs, double[] avgFct)
        {
            int elemQ = 0;
            double[] fctAnom = new double[dataFct.Length];
            double[] fcsAnom = new double[dataFct.Length];
            Array.Fill(fctAnom, double.NaN);
            Array.Fill(fcsAnom, double.NaN);
            for (int i = 0; i < dataFct.Length; i++)
            {
                if (!double.IsNaN(dataFct[i]) && !double.IsNaN(dataFcs[i]) && !double.IsNaN(avgFct[i]))
                {
                    fctAnom[i] = dataFct[i] / avgFct[i];
                    fcsAnom[i] = dataFcs[i] / avgFct[i];
                    elemQ++;
                }
            }
            double value = double.NaN;
            if (elemQ > 0)
            {
                double ft = MathSupport.Avg(fctAnom)[0];
                double fs = MathSupport.Avg(fcsAnom)[0];
                int Nft, Nfs;
                if (ft > 1.2)
                    Nft = 1;
                else
                    Nft = ((ft >= 0.8)) ? 2 : 3;
                if (fs > 1.2)
                    Nfs = 1;
                else
                    Nfs = ((fs >= 0.8)) ? 2 : 3;
                if (Math.Abs(Nft - Nfs) == 0)
                    value = 100;
                else
                    value = (Math.Abs(Nft - Nfs) == 1) ? 50 : 0;

            }
            return new Estimation(EnumMathVar.PZHURRR, value, elemQ);
        }
        public static double P86TaMonth(double dataFct, double dataFcs, double avgFct)
        {
            if (double.IsNaN(dataFct) || double.IsNaN(dataFcs) || double.IsNaN(avgFct))
                return double.NaN;
            return P86TaMonth(dataFct - avgFct, dataFcs - avgFct);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dFct">аномалия фактическая</param>
        /// <param name="dFcs">аномалия прогностическая</param>
        /// <returns></returns>
        private static double P86TaMonth(double dFct, double dFcs)
        {
            if (double.IsNaN(dFcs) || double.IsNaN(dFct))
                return double.NaN;
            double dT = Math.Abs(dFcs - dFct);
            if (Math.Abs(dFct) > 3)
            {
                if (dFct > 3)
                {
                    if (dFcs > 2)
                    {
                        return 100;
                    }
                    else
                    {
                        return (dFcs >= 1) ? 75 : 0;
                    }
                }
                else
                {
                    if (dFcs < -2)
                    {
                        return 100;
                    }
                    else
                    {
                        return (dFcs <= -1) ? 75 : 0;
                    }
                }
            }
            else
            {
                if (dT <= 1.0)
                {
                    return 100;
                }
                else if (dT <= 2)
                {
                    return 75;
                }
                else
                {
                    return (dT <= 3) ? 25 : 0;
                }
            }
        }
        /// <summary>
        /// Параметр Ro - оправд-ть аномалии сезонной Та по знаку (стр. 21,  "Наставление..., 1986, р.2, ч.VI, Москва, ГМИздат").
        /// </summary>
        /// <param name="dataFct">фактические данные</param>
        /// <param name="dataFcs">прогностические данные</param>
        /// <param name="avgFct">климат фактических данных</param>
        /// <returns></returns>
        static public Estimation Ro82TASeason(double[] dataFct, double[] dataFcs, double avgFct)
        {
            if (Double.IsNaN((double)avgFct))
            {
                return null;
            }
            double[] avgFctArr = Array.allocFill(dataFct.Length, (double)avgFct);
            return Ro82TASeason(dataFct, dataFcs, avgFctArr);
        }
        /// <summary>
        /// Параметр Ro - оправд-ть аномалии сезонной Та по знаку (стр. 21,  "Наставление..., 1986, р.2, ч.VI, Москва, ГМИздат").
        /// </summary>
        /// <param name="dataFct">фактические данные</param>
        /// <param name="dataFcs">прогностические данные</param>
        /// <param name="avgFct">климат фактических данных</param>
        /// <returns></returns>
        static public Estimation Ro82TASeason(double[] dataFct, double[] dataFcs, double[] avgFct)
        {
            int elemQ = 0;
            double dfct, dfcs;
            int P1 = 0, P2 = 0;
            for (int i = 0; i < dataFct.Length; i++)
            {
                if (!double.IsNaN(dataFct[i]) && !double.IsNaN(dataFcs[i]))
                {
                    dfct = dataFct[i] - avgFct[i];
                    dfcs = dataFcs[i] - avgFct[i];
                    if ((dfct * dfcs) > 0)
                    {
                        P1++;
                    }
                    else
                    {
                        if ((dfct == 0) || (dfcs == 0))
                        {
                            if ((dfct == 0) && (dfcs == 0))
                            {
                                P1++;
                            }
                        }
                        P2++;
                    }
                    elemQ++;
                }
            }
            double value = double.NaN;
            if (elemQ > 0)
            {
                value = (double)(P1 - P2) / elemQ;
            }
            return new Estimation(EnumMathVar.RO82TASEASON, value, elemQ);
        }
        /// <summary>
        /// Параметр Q - погрешность аномалии сезонной Та по величине (стр. 21,  "Наставление..., 1986, р.2, ч.VI, Москва, ГМИздат").
        /// </summary>
        /// <param name="dataFct">фактические данные</param>
        /// <param name="dataFcs">прогностические данные</param>
        /// <param name="stdFct">климатическое стандартное отклонение фактических данных</param>
        /// <returns></returns>
        static public Estimation Q82TaSeason(double[] dataFct, double[] dataFcs, double stdFct)
        {
            if (Double.IsNaN((double)stdFct))
            {
                return null;
            }
            double[] stdFctArr = Array.allocFill(dataFct.Length, (double)stdFct);
            return Q82TaSeason(dataFct, dataFcs, stdFctArr);
        }
        /// <summary>
        /// Параметр Q - погрешность аномалии сезонной Та по величине (стр. 21,  "Наставление..., 1986, р.2, ч.VI, Москва, ГМИздат").
        /// </summary>
        /// <param name="dataFct">фактические данные</param>
        /// <param name="dataFcs">прогностические данные</param>
        /// <param name="stdFct">климатическое стандартное отклонение фактических данных</param>
        /// <returns></returns>
        static public Estimation Q82TaSeason(double[] dataFct, double[] dataFcs, double[] stdFct)
        {
            int elemQ = 0;

            double S = 0;
            for (int i = 0; i < dataFct.Length; i++)
            {
                if (!double.IsNaN(dataFcs[i]) && !double.IsNaN(dataFct[i]))
                {
                    S += Math.Pow((dataFcs[i] - dataFct[i]) / stdFct[i], 2);
                    elemQ++;
                }
            }
            double value = double.NaN;
            if (elemQ > 0)
            {
                value = S / elemQ;
            }
            return new Estimation(EnumMathVar.Q82TASEASON, value, elemQ);
        }
        /// <summary>
        /// Параметр P - оправд-ть аномалии сезонной Та по знаку (стр. 21,  "Наставление..., 1986, р.2, ч.VI, Москва, ГМИздат").
        /// </summary>
        /// <param name="dataFct">фактические данные</param>
        /// <param name="dataFcs">прогностические данные</param>
        /// <param name="avgFct">климат фактических данных</param>
        /// <returns></returns>
        static public Estimation P82TaSeason(double[] dataFct, double[] dataFcs, double avgFct)
        {
            if (Double.IsNaN((double)avgFct))
            {
                return null;
            }
            double[] avgFctArr = Array.allocFill(dataFct.Length, (double)avgFct);
            return P82TaSeason(dataFct, dataFcs, avgFctArr);
        }
        /// <summary>
        /// Параметр P - оправд-ть аномалии сезонной Та по знаку (стр. 21,  "Наставление..., 1986, р.2, ч.VI, Москва, ГМИздат").
        /// </summary>
        /// <param name="dataFct">фактические данные</param>
        /// <param name="dataFcs">прогностические данные</param>
        /// <param name="avgFct">климат фактических данных</param>
        /// <returns></returns>
        static public Estimation P82TaSeason(double[] dataFct, double[] dataFcs, double[] avgFct)
        {
            Estimation Ro;
            int elemQ = 0;
            Ro = Ro82TASeason(dataFct, dataFcs, avgFct);
            double value = double.NaN;
            if (!double.IsNaN(Ro.Value))
            {
                value = 50 * (1 + Ro.Value);
                elemQ = Ro.ElemQ;
            }
            return new Estimation(EnumMathVar.P82TASEASON, value, elemQ);
        }
        /// <summary>
        /// Отношение оценки качества месячных прогнозов температуры (P86TAMONTH) к оценке климатического прогноза
        /// </summary>
        /// <param name="dataFct">фактические данные</param>
        /// <param name="dataFcs">прогностические данные</param>
        /// <param name="avgFct">климат фактических данных</param>
        /// <returns></returns>
        static public Estimation P86TA_P_Clm(double[] dataFct, double[] dataFcs, double avgFct)
        {
            if (Double.IsNaN((double)avgFct))
            {
                return null;
            }
            double[] avgFctArr = Array.allocFill(dataFct.Length, (double)avgFct);
            return P86TA_P_Clm(dataFct, dataFcs, avgFctArr);
        }
        /// <summary>
        /// Отношение оценки качества месячных прогнозов температуры (P86TAMONTH) к оценке климатического прогноза
        /// </summary>
        /// <param name="dataFct">фактические данные</param>
        /// <param name="dataFcs">прогностические данные</param>
        /// <param name="avgFct">климат фактических данных</param>
        /// <returns></returns>
        static public Estimation P86TA_P_Clm(double[] dataFct, double[] dataFcs, double[] avgFct)
        {
            int elemQ = 0;
            Estimation u, uo;
            u = P86TaMonth(dataFct, dataFcs, avgFct);
            double[] dataAvgFct = new double[dataFct.Length];
            for (int i = 0; i < dataFct.Length; i++)
            {
                dataAvgFct[i] = avgFct[i];
            }
            uo = P86TaMonth(dataFct, dataAvgFct, avgFct);
            double value = double.NaN;
            if (!double.IsNaN(u.Value) && !double.IsNaN(uo.Value))
            {
                if (uo.Value != 0)
                {
                    value = u.Value / uo.Value;
                    elemQ = u.ElemQ;
                }
            }
            return new Estimation(EnumMathVar.P86TA_P_CLM, value, elemQ);

        }
        /// <summary>
        /// Разность оценки качества месячных прогнозов температуры (P86TAMONTH) и оценки климатического прогноза
        /// </summary>
        /// <param name="dataFct">фактические данные</param>
        /// <param name="dataFcs">прогностические данные</param>
        /// <param name="avgFct">климат фактических данных</param>
        /// <returns></returns>
        static public Estimation P86TA_minus_P_Clm(double[] dataFct, double[] dataFcs, double avgFct)
        {
            if (Double.IsNaN((double)avgFct))
            {
                return null;
            }
            double[] avgFctArr = Array.allocFill(dataFct.Length, (double)avgFct);
            return P86TA_minus_P_Clm(dataFct, dataFcs, avgFctArr);
        }
        /// <summary>
        /// Разность оценки качества месячных прогнозов температуры (P86TAMONTH) и оценки климатического прогноза
        /// </summary>
        /// <param name="dataFct">фактические данные</param>
        /// <param name="dataFcs">прогностические данные</param>
        /// <param name="avgFct">климат фактических данных</param>
        /// <returns></returns>
        static public Estimation P86TA_minus_P_Clm(double[] dataFct, double[] dataFcs, double[] avgFct)
        {
            int elemQ = 0;
            Estimation u, uo;
            u = P86TaMonth(dataFct, dataFcs, avgFct);
            double[] dataAvgFct = new double[dataFct.Length];
            for (int i = 0; i < dataFct.Length; i++)
            {
                dataAvgFct[i] = avgFct[i];
            }
            uo = P86TaMonth(dataFct, dataAvgFct, avgFct);
            double value = double.NaN;
            if (!double.IsNaN(u.Value) && !double.IsNaN(uo.Value))
            {
                value = u.Value - uo.Value;
                elemQ = u.ElemQ;
            }
            return new Estimation(EnumMathVar.P86TA_minus_P_CLM, value, elemQ);

        }
        public static Estimation PsKQQQ(double[] dataA, double[] dataB, double sigma, EnumMathVar mathVar)
        {
            if (Double.IsNaN(sigma))
            {
                return null;
            }
            double[] est = PKQQQ(dataA, dataB, sigma);
            return new Estimation(mathVar, est[0], (int)est[1]);
        }
        private static double[] PKQQQ(double[] dataA, double[] dataB, double sigma)
        {
            int countAll = 0, count100 = 0;
            for (int i = 0; i < dataA.Length; i++)
            {
                if (!Double.IsNaN(dataA[i]) && !Double.IsNaN(dataB[i]))
                {
                    if (Math.Abs(dataA[i] - dataB[i]) <= sigma * 0.674)
                    {
                        count100++;
                    }
                    countAll++;
                }
            }
            if (countAll > 0)
            {
                return new double[] { ((double)count100 / countAll) * 100, countAll };
            }
            else return null;
        }
        /// <summary>
        /// Параметр P - показатель качества прогноза 
        /// месячной суммы осадков в классе ниже нормы (меньше 80%) 
        /// (Оценка «угадывания класса»).Если фактических данных в классе ниже нормы не было,
        /// то P3_-1=0;
        /// </summary>
        /// <param name="fct"> массив фактических данных</param>
        /// <param name="fcs">массив прогностических данных</param>
        /// <param name="avgFct">массив нормы (ср.значения) данных</param>
        /// <returns></returns>
        static Estimation PRR_Minus1(double[] fct, double[] fcs, double[] avgFct)
        {
            int elemQ = 0, q = 0, nn = 0;
            for (int i = 0; i < fct.Length; i++)
            {
                if (!Double.IsNaN(fct[i]) && !Double.IsNaN(fcs[i]))
                {
                    double ft = fct[i] / avgFct[i];
                    double fs = fcs[i] / avgFct[i];
                    if (ft < 0.8)
                    {
                        nn++;
                        if (fs < 0.8)
                            q++;
                    }
                    elemQ++;
                }
            }
            double value = double.NaN;
            if (elemQ > 0)
            {
                if (nn == 0)
                    value = 0;
                else
                    value = 100.0 * q / nn;
                return new Estimation(EnumMathVar.P3_Minus1, value, elemQ);
            }
            else return null;
        }
        /// <summary>
        /// Параметр P - показатель качества прогноза 
        /// месячной суммы осадков в классе нормы (>= 80% и 120%>=) 
        /// (Оценка «угадывания класса»).Если фактических данных в классе нормы не было,
        /// то P3_0=0;
        /// </summary>
        /// <param name="fct"> массив фактических данных</param>
        /// <param name="fcs">массив прогностических данных</param>
        /// <param name="avgFct">массив нормы (ср.значения) данных</param>
        /// <returns></returns>
        static Estimation PRR_0(double[] fct, double[] fcs, double[] avgFct)
        {
            int elemQ = 0, q = 0, nn = 0;
            for (int i = 0; i < fct.Length; i++)
            {
                if (!Double.IsNaN(fct[i]) && !Double.IsNaN(fcs[i]))
                {
                    double ft = fct[i] / avgFct[i];
                    double fs = fcs[i] / avgFct[i];
                    if ((ft >= 0.8) && (ft <= 1.2))
                    {
                        nn++;
                        if ((fs >= 0.8) && (fs <= 1.2))
                            q++;
                    }
                    elemQ++;
                }
            }
            double value = double.NaN;
            if (elemQ > 0)
            {
                if (nn == 0)
                    value = 0;
                else
                    value = 100.0 * q / nn;
                return new Estimation(EnumMathVar.P3_0, value, elemQ);
            }
            else return null;
        }
        /// <summary>
        /// Параметр P - показатель качества прогноза 
        /// месячной суммы осадков в классе выше нормы (больше 120%) 
        /// (Оценка «угадывания класса»).Если фактических данных в классе выше нормы не было,
        /// то P3_+1=0;
        /// </summary>
        /// <param name="fct"> массив фактических данных</param>
        /// <param name="fcs">массив прогностических данных</param>
        /// <param name="avgFct">массив нормы (ср.значения) данных</param>
        /// <returns></returns>
        static Estimation PRR_Plus1(double[] fct, double[] fcs, double[] avgFct)
        {
            int elemQ = 0, q = 0, nn = 0;
            for (int i = 0; i < fct.Length; i++)
            {
                if (!Double.IsNaN(fct[i]) && !Double.IsNaN(fcs[i]))
                {
                    double ft = fct[i] / avgFct[i];
                    double fs = fcs[i] / avgFct[i];
                    if (ft > 1.2)
                    {
                        nn++;
                        if (fs > 1.2)
                            q++;
                    }
                    elemQ++;
                }
            }
            double value = double.NaN;
            if (elemQ > 0)
            {
                if (nn == 0)
                    value = 0;
                else
                    value = 100.0 * q / nn;
                return new Estimation(EnumMathVar.P3_Plus1, value, elemQ);
            }
            else return null;
        }
        ///'S_SIGMA_U' - Отношение СКО прогнозов к СКО от нормы 
        ///РД РД 52.27.284-91, стр. 117, ф. 99,100
        ///n - длина ряда,
        ///fct - указательна одномерный массив фактических данных, 
        ///fcs - указательна одномерный массив прогностических данных.
        ///stdot - стандартное отклонение фактического ряда за многолетний период
        public static Estimation SSigmaU(double[] fct, double[] fcs, double std)
        {
            if (Double.IsNaN((double)std))
            {
                return null;
            }
            double sigma = (double)std;

            double s = 0;
            int elemQ = 0;
            for (int i = 0; i < fct.Length; i++)
            {
                if (!Double.IsNaN(fct[i]) && !Double.IsNaN(fcs[i]))
                {
                    s += Math.Pow((fct[i] - fcs[i]), 2);
                    elemQ++;
                }
            }
            if (elemQ > 1 && sigma > 0)
            {
                s = Math.Sqrt(s / (elemQ - 1));
                double value = s / sigma;
                return new Estimation(EnumMathVar.S_SIGMA_U, value, elemQ);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 'S_SIGMA_7' - Отношение СКО прогнозов к 
        /// СКО за период прогноза от нормы 
        /// РД РД 52.27.284-91, стр. 117, ф. 99,100
        /// </summary>
        /// <param name="dataFct"> массив фактических данных</param>
        /// <param name="dataFcs"> массив прогностических данных</param>
        /// <param name="avg">массив нормы (ср.значения) данных.</param>
        /// <returns></returns>
        public static Estimation SSigma7(double[] dataFct, double[] dataFcs, double[] avg)
        {
            double s = 0;
            double sigma = 0;
            int elemQ = 0;
            double value;

            for (int i = 0; i < dataFct.Length; i++)
            {
                if (!Double.IsNaN(dataFct[i]) && !Double.IsNaN(dataFcs[i]) && !double.IsNaN(avg[i]))
                {
                    s += Math.Pow(dataFct[i] - dataFcs[i], 2);
                    sigma += Math.Pow(dataFct[i] - avg[i], 2);
                    elemQ++;
                }
            }
            if (elemQ > 1 && sigma > 0)
            {
                s = Math.Sqrt(s / (elemQ - 1));
                sigma = Math.Sqrt(sigma / (elemQ - 1));
                value = s / sigma;
            }
            else
            {
                return null;
            }
            return new Estimation(EnumMathVar.S_SIGMA_7, value, elemQ);
        }
        public static Estimation SSigmaF(double[] dataFct, double[] dataFcs)
        {
            double s = 0;
            double sigma = 0;
            int elemQ = 0;
            double value;
            double fct_avg = dataFct.Average();
            for (int i = 0; i < dataFct.Length; i++)
            {
                if (!Double.IsNaN(dataFct[i]) && !Double.IsNaN(dataFcs[i]))
                {
                    s += Math.Pow(dataFct[i] - dataFcs[i], 2);
                    sigma += Math.Pow(dataFct[i] - fct_avg, 2);
                    elemQ++;
                }
            }
            if (elemQ > 1 && sigma > 0)
            {
                s = Math.Sqrt(s / (elemQ - 1));
                sigma = Math.Sqrt(sigma / (elemQ - 1));
                value = s / sigma;
            }
            else
            {
                return null;
            }
            return new Estimation(EnumMathVar.S_SIGMA_F, value, elemQ);
        }
        public static Estimation SDataFcs(double[] dataA, double[] dataB)
        {
            double value;
            int elemQ = 0;
            double dataFct_avg;
            double s = 0;
            dataFct_avg = MathSupport.Avg(dataA)[0];
            if (Double.IsNaN((double)dataFct_avg))
            {
                return null;
            }

            for (int i = 0; i < dataA.Length; i++)
            {
                if (!Double.IsNaN(dataA[i]) && !Double.IsNaN(dataB[i]))
                {
                    s += Math.Pow((double)(dataA[i] - dataB[i]), 2);
                    elemQ++;
                }
            }
            if (elemQ > 1)
            {
                value = Math.Sqrt(s / (elemQ - 1));
            }
            else return null;

            return new Estimation(EnumMathVar.S_FCS, value, elemQ);
        }
        /// <summary>
        /// Оценка Рv (%) для отклонений прогнозируемой скорости от фактической
        /// на V_krit м/с и менее (по «Методическими указаниями» 
        /// РД 52.27.284-91, п.1.2.3.3? Москва,1991, 119 c.)
        /// </summary>
        /// <param name="dataA"></param>
        /// <param name="dataB"></param>
        /// <param name="V_krit">максимальное отклонение для учета</param>
        /// <param name="mathVar"></param>
        /// <returns></returns>
        private static Estimation PWind(double[] dataA, double[] dataB, double V_krit, EnumMathVar mathVar)
        {
            int elemQ = 0, q = 0;
            for (int i = 0; i < dataA.Length; i++)
            {
                if (!double.IsNaN(dataA[i]) && !double.IsNaN(dataB[i]))
                {
                    if (Math.Abs(dataB[i] - dataA[i]) <= V_krit)
                    {
                        q++;
                    }
                    elemQ++;
                }

            }
            if (elemQ > 0)
                return new Estimation(mathVar, (double)q / elemQ * 100, elemQ);
            else
                return null;


        }

        /// <summary>
        /// Возвращает только те оценки, которые возможно было посчитать, если ни одной нельзя посчитать, то null
        /// </summary>
        /// <param name="fctData"></param>
        /// <param name="fcsData"></param>
        /// <param name="mathVar"></param>
        /// <returns></returns>
        public static List<Estimation> GetEstimations(double[] fctData, double[] fcsData, EnumMathVar[] mathVar)
        {
            List<Estimation> ret = new List<Estimation>();
            for (int i = 0; i < mathVar.Length; i++)
            {
                Estimation mv = GetEstimation(fctData, fcsData, mathVar[i]);
                if (mv != null)
                {
                    ret.Add(mv);
                }
            }
            return (ret.Count > 0 ? ret : null);
        }
        public static Estimation GetEstimation(double[] fctData, double[] fcsData, double[] fctAvg, double[] fctStd, EnumMathVar mathVar)
        {
            switch (mathVar)
            {
                case EnumMathVar.P86TAMONTH:
                    return P86TaMonth(fctData, fcsData, fctAvg);
                case EnumMathVar.RO82TASEASON:
                    return Ro82TASeason(fctData, fcsData, fctAvg);
                case EnumMathVar.Q82TASEASON:
                    return Q82TaSeason(fctData, fcsData, fctStd);
                case EnumMathVar.P82TASEASON:
                    return P82TaSeason(fctData, fcsData, fctAvg);
                case EnumMathVar.P86TAMONTH_cls:
                    return P86TAMonthCls(fctData, fcsData, fctAvg);
                case EnumMathVar.P86TA_minus_P_CLM:
                    return P86TA_minus_P_Clm(fctData, fcsData, fctAvg);
                case EnumMathVar.P86TA_P_CLM:
                    return P86TA_P_Clm(fctData, fcsData, fctAvg);
                case EnumMathVar.P82RRMONTH:
                    return P82RRMonth(fctData, fcsData, fctAvg);
                case EnumMathVar.P82RR_minus_P_CLM:
                    return P82RR_minus_P_Clm(fctData, fcsData, fctAvg);
                case EnumMathVar.P82RR_P_CLM:
                    return P82RR_P_Clm(fctData, fcsData, fctAvg);
                case EnumMathVar.PZHURTA:
                    return PZHURTA(fctData, fcsData, fctAvg);
                case EnumMathVar.PZHURRR:
                    return PZHURRR(fctData, fcsData, fctAvg);
                case EnumMathVar.S_SIGMA_U:
                    if (fctStd.Length < 1)
                        return null;
                    double std = fctStd[0];
                    foreach (var stdI in fctStd)
                    {
                        if (stdI != std)
                            return null;
                    }
                    return SSigmaU(fctData, fcsData, std);
                case EnumMathVar.S_SIGMA_7:
                    return SSigma7(fctData, fcsData, fctAvg);
                case EnumMathVar.P3_Minus1:
                    return PRR_Minus1(fctData, fcsData, fctAvg);
                case EnumMathVar.P3_0:
                    return PRR_0(fctData, fcsData, fctAvg);
                case EnumMathVar.P3_Plus1:
                    return PRR_Plus1(fctData, fcsData, fctAvg);
                default:
                    return GetEstimation(fctData, fcsData, mathVar);

            }

        }
        public static Estimation GetEstimation(double[] fctData, double[] fcsData, EnumMathVar mathVar)
        {
            switch (mathVar)
            {
                case EnumMathVar.R: return Corr(fctData, fcsData);
                case EnumMathVar.ABSDEV: return DevAbs(fctData, fcsData);
                case EnumMathVar.DEVAVG: return DevAvg(fctData, fcsData);
                case EnumMathVar.DEV2: return Dev2(fctData, fcsData);
                case EnumMathVar.ABSDEVMIN: return DevAbsMin(fctData, fcsData);
                case EnumMathVar.ABSDEVMAX: return DevAbsMax(fctData, fcsData);
                case EnumMathVar.PV2Pr: return PWind(fctData, fcsData, 2, EnumMathVar.PV2Pr);
                case EnumMathVar.PV5Pr: return PWind(fctData, fcsData, 5, EnumMathVar.PV5Pr);
                case EnumMathVar.PV10Pr: return PWind(fctData, fcsData, 10, EnumMathVar.PV10Pr);
                case EnumMathVar.SIGMA_WDIR_SIN: return SigmaWdirSin(fctData, fcsData);
                case EnumMathVar.SIGMA0_WDIR_RD: return SigmaWdirRD(fctData, fcsData, 0, 30.4999, mathVar);
                case EnumMathVar.SIGMA31_WDIR_RD: return SigmaWdirRD(fctData, fcsData, 30.5, 60.4999, mathVar);
                case EnumMathVar.SIGMA61_WDIR_RD: return SigmaWdirRD(fctData, fcsData, 60.5, 90.4999, mathVar);
                case EnumMathVar.SIGMA91_WDIR_RD: return SigmaWdirRD(fctData, fcsData, 90.5, 180, mathVar);
                case EnumMathVar.S_SIGMA_F: return SSigmaF(fctData, fcsData);
                default:
                    throw new Exception("Not supported MathVar = " + mathVar);
            }
        }

    }
}
