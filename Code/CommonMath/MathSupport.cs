using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOV.Common
{
    /// <summary>
    /// Математика и статистика.
    /// </summary>
    public static class MathSupport
    {
        /// <summary>
        /// Скользящее среднее
        /// </summary>
        /// <param name="windowElemQ"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        static public double[] AvgSlide(int windowElemQ, double[] data)
        {
            double[] ret = new double[data.Length - windowElemQ + 1];
            double sum = 0;

            for (int i = 0; i < windowElemQ; i++)
            {
                sum += data[i];
            }

            for (int i = 0; i < data.Length - windowElemQ; i++)
            {
                ret[i] = sum / windowElemQ;
                sum -= data[i];
                sum += data[i + windowElemQ];
            }
            ret[ret.Length - 1] = sum / windowElemQ;

            return ret;
        }

        public static int IndexOfMinValue(int[] arr)
        {
            int ret = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] < arr[ret])
                {
                    ret = i;
                }
            }
            return ret;
        }

        public static double[] MinMax(double[] array)
        {
            int j = 1 / array.Length; // throw if null || vx.Length = 0

            double[] ret = new double[] { double.MaxValue, double.MinValue };
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] < ret[0]) ret[0] = array[i];
                if (array[i] > ret[1]) ret[1] = array[i];
            }
            return ret;
        }
        /// <summary>
        /// Интерполяция массива data В-сплайном, построенным по четырем узлам по формуле
        /// y(t)[j]=((1-t)^3)/6*x[j-1]+(3*t^3-6*t^2+4)/6*x[j]+(-3t^3+3*t^2+3*t+1)/6*x[j+1]+t^3/6*x[j+2]
        /// t принадлежит [0,1]
        /// интерполяция производится для всех значений i, для которых data[i] == double.NaN
        /// 
        /// Ported form java (eqtt) by sov.
        /// 
        /// </summary>
        /// <param name="data">Массив с дырками типа double.NaN.</param>
        /// <returns>Массив без дырок (проинтерполированный).</returns>
        public static double[] InterpolateBSpline(double[] data)
        {
            int n = data.Length;
            double[] tempArray = new double[n + 2];
            int m = n + 2;
            tempArray[0] = (data[0] - data[1]) + data[0];
            for (int i = 1; i <= n; i++)
            {
                tempArray[i] = data[i - 1];
            }
            tempArray[n + 1] = (data[n - 1] - data[n - 2]) + data[n - 1];
            for (int i = 0; i < m - 1; i++)
            {
                if (tempArray[i] == double.NaN)
                {
                    int j1 = i;
                    while (double.IsNaN(tempArray[j1]))
                    {
                        j1--;
                    }
                    int j2 = j1 - 1;
                    while (double.IsNaN(tempArray[j2]))
                    {
                        j2--;
                    }
                    int k1 = i;
                    while (double.IsNaN(tempArray[k1]))
                    {
                        k1++;
                    }
                    int k2 = k1 + 1;
                    while (double.IsNaN(tempArray[k2]))
                    {
                        k2++;
                    }
                    int mp = k1 - j1 - 1;
                    double step = 1.0 / ((double)mp * 10);
                    int k = 0;
                    int nk = j1;
                    double t = 0;

                    while (t <= 1)
                    {
                        double cjm1 = Math.Pow(1 - t, 3) / 6.0;
                        double cj = (3 * t * t * t - 6 * t * t + 4) / 6.0;
                        double cjp1 = (-3 * t * t * t + 3 * t * t + 3 * t + 1) / 6.0;
                        double cjp2 = t * t * t / 6.0;
                        double yt = tempArray[j2] * cjm1 + tempArray[j1] * cj + tempArray[k1] * cjp1 + tempArray[k2] * cjp2;
                        if (k % 10 == 0 && k != 0)
                        {
                            nk++;
                        }
                        if (t != 0 && t != 1 && k % 10 == 0)
                        {
                            tempArray[nk] = yt;
                        }
                        t += step;
                        k++;
                    }
                }
            }
            double[] res = new double[n];
            for (int i = 1; i < m - 1; i++)
            {
                res[i - 1] = tempArray[i];
            }
            return res;
        }
        /// <summary>
        ///  Линейная интерполяция
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <param name="y1"></param>
        /// <param name="y2"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double InterpolateLine(double x1, double x2, double y1, double y2, double x)
        {
            if (x < x1 || x > x2)
                throw new Exception("(x < x1 || x > x2) : " + x + "/" + x1 + "/" + x2);
            return ((x - x1) / (x2 - x1)) * (y2 - y1) + y1;
        }
        /// <summary>
        /// ВНИМАНИЕ: x1.Kind = x2.Kind
        /// </summary>
        /// <returns>new object[] { List DateTime date, List double data }</returns>
        public static Object InterpolateLine(DateTime x1, DateTime x2, double y1, double y2, ulong xStepSeconds)
        {
            DateTime xCur = (DateTime.FromBinary(x1.ToBinary())).AddSeconds(xStepSeconds);
            long x1b = x1.Ticks, x2b = x2.Ticks;

            List<DateTime> x = new List<DateTime>();
            List<double> y = new List<double>();
            while (xCur < x2)
            {
                x.Add(DateTime.FromBinary(xCur.ToBinary()));
                y.Add(InterpolateLine(x1b, x2b, y1, y2, xCur.Ticks));

                xCur = xCur.AddSeconds(xStepSeconds);
            }
            if (x.Count == 0)
                return null;
            return new object[] { x, y };
        }

        public static double[/*valueInterp,elemQ*/] interpolateLine(double[] dist, double[] values)
        {
            if (dist.Length != values.Length) { throw new Exception("dist.Length!=values.Length"); }
            double sumDist = 0;
            double sumValueW = 0;
            int elemQ = 0;
            for (int i = 0; i < dist.Length; i++)
            {
                if (!double.IsNaN(values[i]) && !double.IsNaN(dist[i]))
                {
                    if (dist[i] == 0)
                    {
                        sumValueW = values[i];
                        sumDist = 1;
                        elemQ = 1;
                        break;
                    }
                    sumDist += 1 / dist[i];
                    sumValueW += values[i] / dist[i];
                    elemQ++;
                }
            }
            if (elemQ > 0)
            {
                return new double[] { sumValueW / sumDist, elemQ };
            }
            else { return new double[] { double.NaN, elemQ }; }
        }
        /// <summary>
        /// 50% Квартиль ряда.
        /// </summary>
        /// <param name="data">ранжированный массив</param>
        public static double Cvartile50(double[] data)
        {
            int[] i = Cvartile50Idx(data.Length);
            return 0.5 * (data[i[0]] + data[i[1]]);
        }
        /// <summary>
        /// 25% Квартиль ряда.
        /// </summary>
        /// <param name="data">ранжированный массив</param>
        public static double Cvartile25(double[] data)
        {
            int[] i = Cvartile50Idx(data.Length / 2);
            return 0.5 * (data[i[0]] + data[i[1]]);
        }
        /// <summary>
        /// 75% Квартиль ряда.
        /// </summary>
        /// <param name="data">ранжированный массив</param>
        public static double Cvartile75(double[] data)
        {
            int[] i = Cvartile50Idx(data.Length / 2);
            return 0.5 * (data[data.Length - i[0]] + data.Length - data[i[1]]);
        }
        private static int[] Cvartile50Idx(int dataLength)
        {
            int i = dataLength / 2;
            if (i * 2 == dataLength)
            {
                return new int[] { i, i + 1 };
            }
            else
            {
                i = (dataLength + 1) / 2;
                return new int[] { i, i };
            }
        }
        /// <summary>
        ///                         0    1      2    3    4
        /// return new double[] { sum, sum2, elemQ, min, max };
        /// </summary>
        /// <param name="data"></param>
        /// <returns>return new double[] { sum, sum2, elemQ, min, max };</returns>
        public static double[] Sum(double[] data)
        {
            if (data == null)
                return null;

            double sum = 0, sum2 = 0;
            double min = double.MaxValue, max = double.MinValue;
            int elemQ = 0;

            for (int i = 0; i < data.Length; i++)
            {
                if (!double.IsNaN(data[i]))
                {
                    if (min > data[i]) min = data[i];
                    if (max < data[i]) max = data[i];
                    sum += data[i];
                    sum2 += (data[i] * data[i]);
                    elemQ++;

                }
            }
            return (elemQ == 0) ? new double[] { double.NaN, double.NaN, 0, double.NaN, double.NaN } : new double[] { sum, sum2, elemQ, min, max };

        }
        /// <summary>
        /// Allow NaN. dataA.Length is primary.
        /// </summary>
        /// <param name="dataA"></param>
        /// <param name="dataB"></param>
        /// <returns></returns>
        public static double[/*sumA sum2A sumB sum2B sumAB elemQ*/] Sum(double[] dataA, double[] dataB)
        {
            double sumAB = 0;
            double[] sum = new double[] { 0, 0 };
            double[] sum2 = new double[] { 0, 0 };
            int elemQ = 0;

            for (int i = 0; i < dataA.Length; i++)
            {
                if (!double.IsNaN(dataA[i]) && !double.IsNaN(dataB[i]))
                {
                    sum[0] += dataA[i];
                    sum[1] += dataB[i];
                    sum2[0] += (dataA[i] * dataA[i]);
                    sum2[1] += (dataB[i] * dataB[i]);
                    sumAB += (dataA[i] * dataB[i]);
                    elemQ++;
                }
            }
            return new double[/*sumA sum2A sumB sum2B sumAB elemQ*/] { sum[0], sum2[0], sum[1], sum2[1], sumAB, elemQ };
        }
        /// <summary>
        /// Сумма квадратов разностей элементов двух рядов.
        /// </summary>
        /// <returns>sumAmB2;elemQ*</returns>
        public static double[/*sumAmB2;elemQ*/] SumAmB2(double[] dataA, double[] dataB)
        {
            double sumAmB2 = 0;
            int elemQ = 0;

            for (int i = 0; i < dataA.Length; i++)
            {
                if (!double.IsNaN(dataA[i]) && !double.IsNaN(dataB[i]))
                {
                    sumAmB2 += Math.Pow(dataA[i] - dataB[i], 2);
                    elemQ++;
                }
            }
            if (elemQ == 0)
                return null;

            return new double[/*sumAmB2;elemQ*/] { sumAmB2, elemQ };
        }

        /// <summary>
        /// Correlation coefficent at the 5% and 1% levels of significance for various degrees of freedom.
        /// W.J. Emery, R.E. Thompson Data analysis methods in phisical oceanography. Pergamon, 1998, 634 p.
        /// </summary>
        /// <param name="DegreesOfFreedom"></param>
        /// <param name="SignificanceLevel">Only 5% released (sov@200704)</param>
        /// <returns></returns>
        static public double CorrSignificance(int DegreesOfFreedom, float SignificanceLevel)
        {
            double[,] f = new double[,]  
			{
				// DegreesOfFreedom
				{1    ,2    ,3    ,4    ,5    ,6    ,7    ,8    ,9    ,10   ,11   ,12   ,13   ,14   ,15   ,16   ,17   ,18   ,19   ,20   ,21   ,22   ,23   ,24   ,25   ,26   ,27   ,28   ,29   ,30   ,35   ,40   ,45   ,50   ,60   ,70   ,80   ,90   ,100  ,125  ,150  ,200  ,300  ,400  ,500  ,1000 },
				// 5% level of significance
				{0.997,0.950,0.878,0.811,0.754,0.707,0.666,0.632,0.602,0.576,0.553,0.532,0.514,0.497,0.482,0.468,0.456,0.444,0.433,0.423,0.413,0.404,0.396,0.388,0.381,0.374,0.367,0.361,0.355,0.349,0.325,0.304,0.288,0.273,0.250,0.232,0.217,0.205,0.195,0.174,0.159,0.138,0.113,0.098,0.088,0.062}
			};

            int i, j;
            if (SignificanceLevel == 5)
                j = 1;
            else
                j = 0;

            if (j > 0)
            {
                for (i = f.GetLowerBound(1) + 1; i <= f.GetUpperBound(1); i++)
                {
                    if (DegreesOfFreedom < f[0, i])
                        return f[j, i - 1];
                }
                return f[j, i - 1];
            }
            return float.MaxValue;
        }
        static public int LO(uint ui) { return (short)ui; }
        static public int HI(uint ui) { return (short)((ui >> 16) & 0xFFFF); }
        static public byte[] LO2HI(byte[] barr)
        {
            byte b = barr[3];
            barr[3] = barr[0];
            barr[0] = b;
            b = barr[2];
            barr[2] = barr[1];
            barr[1] = b;
            return barr;
        }
        static public int Byte2Int(byte[] b, int byte_num)
        {
            int i;
            i = (int)b[byte_num];
            i += (((int)b[byte_num + 1] << 8) & 0xFFFF);
            i += (((int)b[byte_num + 2] << 16) & 0xFFFF);
            i += (((int)b[byte_num + 3] << 24) & 0xFFFF);
            return i;
        }
        static public byte[] Int2Byte(Int32 value, byte[] b, int iByteStart)
        {
            byte[] bb = Int2Byte(value);

            for (int i = 0; i < 4; i++)
            {
                b[iByteStart + i] = bb[i];
            }

            return b;
        }
        static public byte[] Int2Byte(Int32 value)
        {
            byte[] b = new byte[4];

            b[0] = (byte)value;
            b[1] = (byte)((value >> 8) & 0xFFFF);
            b[2] = (byte)((value >> 16) & 0xFFFF);
            b[3] = (byte)((value >> 24) & 0xFFFF);

            return b;
        }
        public static bool IsKrat(float f1, float f2)
        {
            return ((int)(f1 / f2) * f2 == (f1)) ? true : false;
        }
        public static bool IsKrat(double f1, double f2)
        {
            return ((int)(f1 / f2) * f2 == (f1)) ? true : false;
        }

        /// <summary>
        /// Стандартное отклонение.
        /// </summary>
        public static double Stdev(double sum, double sum2, int elem_q)
        {
            if (elem_q > 1)
            {
                return System.Math.Sqrt((sum2 * elem_q - sum * sum) / (elem_q * elem_q)); //((DWORD)n*(n-1))
            }
            return double.NaN;
        }
        /// <summary>
        /// Коэффициент корреляции.
        /// </summary>
        public static double Corr(double sumA, double sumB, double sumAB, double sum2A, double sum2B, int elem_q)
        {
            if (elem_q > 1)
            {
                double dcov = Cov(sumA, sumB, sumAB, elem_q);
                double stdev1 = Stdev(sumA, sum2A, elem_q);
                double stdev2 = Stdev(sumB, sum2B, elem_q);

                return Corr(dcov, stdev1, stdev2);
            }
            return double.MaxValue;
        }
        /// <summary>
        /// Коэффициент корреляции.
        /// </summary>
        /// <param name="cov"></param>
        /// <param name="std1"></param>
        /// <param name="std2"></param>
        /// <returns></returns>
        public static double Corr(double cov, double std1, double std2)
        {
            return cov / (std1 * std2);
        }
        /// <summary>
        /// Евклидова метрика
        /// </summary>
        /// <param name="dataA"></param>
        /// <param name="dataB"></param>
        /// <returns></returns>
        public static double[/*euclid;elemQ*/] Euclid2(double[] dataA, double[] dataB)
        {
            double[] sums = SumAmB2(dataA, dataB);
            if ((int)sums[1] < 1)
                return null;

            sums[0] = Math.Sqrt(sums[0]);
            return sums;

        }
        /// <summary>
        /// Коэффициент корреляции
        /// </summary>
        /// <param name="dataA"></param>
        /// <param name="dataB"></param>    
        /// <returns>[0] - corr; [1] - count</returns>
        public static double[/*crr;elemQ*/] Corr(double[] dataA, double[] dataB)
        {
            double[] sums = Sum(dataA, dataB);
            int elemQ = (int)sums[5];
            if (elemQ > 1)
            {
                double[] ret = new double[2];
                ret[0] = Corr(sums[0], sums[2], sums[4], sums[1], sums[3], elemQ);
                ret[1] = elemQ;
                return ret;
            }
            return null;
        }
        /// <summary>
        /// Array avg.
        /// </summary>
        /// <returns>double[avg;elemQ]</returns>
        public static double[] Avg(double[] d)
        {
            double[] summ = Sum(d);
            return new double[] { (summ[2] > 0) ? summ[0] /= summ[2] : double.NaN, summ[2] };
        }

        /// <summary>
        /// Ковариация.
        /// </summary>
        public static double Cov(double sumA, double sumB, double sumAB, int elem_q)
        {
            if (elem_q > 1)
            {

                return (sumAB - (sumA * sumB) / elem_q) / elem_q;
            }
            return double.MaxValue;
        }

        /// <summary>
        /// Удовлетворяет ли значение метрики пределам? 
        /// Проверки нет, если IsNaN(metric_lim).
        /// </summary>
        static public bool IsBetterMetric(double metric, double metric_lim, string metric_order)
        {
            switch (metric_order)
            {
                case "desc":
                    if (metric <= metric_lim) return false;
                    break;
                case "asc":
                    if (metric >= metric_lim) return false;
                    break;
                default:
                    throw new Exception("switch (metric_order)");
            }
            return true;
        }
        /// <summary>
        /// Успешность прогноза знака величины
        /// </summary>
        public static double est_ro(double fct, double fcs, double avg)
        {
            fct -= avg;
            fcs -= avg;
            if (fct != fcs && fct * fcs == 0) return 0;
            if (Math.Sign(fct) == Math.Sign(fcs)) return 1;
            return -1;
        }

        /// <summary>
        /// Стандартное отклонение. от заданного среднего. (1/(n-1))
        /// </summary>
        public static double StdevNm1(double[] data, double avg)
        {
            int n = data.Length;
            if (n > 1)
            {
                double sum = 0;
                for (int i = 0; i < n; i++)
                {
                    sum += Math.Pow(data[i] - avg, 2);
                }
                return Math.Sqrt(sum / (n - 1));
            }
            return double.MaxValue;
        }
        /// <summary>
        /// Расчитывает средневзвешенное значение 
        /// </summary>
        /// <param name="data">значения</param>
        /// <param name="weight">веса</param>
        /// <returns></returns>
        public static double[] CalcAvgWeight(double[] data, double[] weight)
        {
            if (data.Length != weight.Length)
                throw new Exception("ERROR! data.Length != weight.Length");
            double weightAll = 0, dataSumAll = 0;
            int countDataOk = 0;
            for (int i = 0; i < data.Length; i++)
            {
                if (!double.IsNaN(data[i]) && !double.IsNaN(weight[i]))
                {
                    countDataOk++;
                    weightAll += weight[i];
                    dataSumAll += data[i] * weight[i];
                }
            }
            if (countDataOk > 0)
            {
                if (weightAll != 0)
                {
                    double ret = dataSumAll / weightAll;
                    return new double[] { ret, countDataOk };
                }
                else
                    return new double[] { double.NaN, countDataOk };
            }
            else
                return new double[] { double.NaN, 0 };
        }
    }
}

