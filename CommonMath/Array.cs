using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOV.Common
{
    /// <summary>
    /// Обработка массивов.
    /// </summary>
    public class Array
    {

        /// <summary>
        /// ВНИМАНИЕ! Порядок байт в массиве может быть изменён, после вызова данного метода.
        /// </summary>
        /// <param name="byteOrder">Порядок в b</param>
        public static double[] byteArrFloat2DoubleArr(byte[] b, EnumByteOrder byteOrder)
        {

            if (Math.IEEERemainder(b.Length, 4) != 0)
                throw new Exception("Math.IEEERemainder(b.Length, 4) != 0 : " + b.Length);

            if (System.BitConverter.IsLittleEndian && byteOrder != EnumByteOrder.LITTLE_ENDIAN)
            {
                byte bb;
                for (int i = 0; i < b.Length; i += 4)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        bb = b[i + j];
                        b[i + j] = b[i + 3 - j];
                        b[i + 3 - j] = bb;
                    }
                }
            }
            double[] ret = new double[b.Length / 4];
            for (int i = 0; i < ret.Length; i++)
            {
                //ret[i] = (double)System.BitConverter.ToSingle(b, i * 4);
                ret[i] = double.Parse(string.Format("{0:f7}", System.BitConverter.ToSingle(b, i * 4)));
                // ret[i] = IBM2Float(b, i * 4);
            }
            return ret;
        }
        public static byte[] toByteArrFloat(float[] data, EnumByteOrder byteOrder)
        {
            byte[] ret = new byte[data.Length * 4];
            for (int i = 0; i < data.Length; i++)
            {
                byte[] bt = BitConverter.GetBytes(data[i]);
                if ((System.BitConverter.IsLittleEndian && byteOrder == EnumByteOrder.LITTLE_ENDIAN) ||
                    (!System.BitConverter.IsLittleEndian && byteOrder == EnumByteOrder.BIG_ENDIAN))
                {
                    System.Array.Copy(bt, 0, ret, i * 4, 4);
                }
                else
                {
                    for (int j = 0; j < 4; j++)
                    {
                        System.Array.Copy(bt, 3 - j, ret, i * 4 + j, 1);
                    }
                }
            }
            return ret;
        }

        public static byte[] toByte(float[] s)
        {
            byte[] ret = new byte[s.Length * 4];
            for (int i = 0; i < s.Length; i++)
            {
                byte[] bt = BitConverter.GetBytes(s[i]);
                System.Array.Copy(bt, 0, ret, i * 4, 4);
            }
            return ret;
        }
        public static double[] ToDouble(string[] arr)
        {
            double[] ret = new double[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                ret[i] = double.Parse(arr[i]);
            }
            return ret;
        }
        public static int[] ToInt(string[] arr)
        {
            int[] ret = new int[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                ret[i] = int.Parse(arr[i]);
            }
            return ret;
        }

        public static string ToString(int[] arr)
        {
            return ToString(arr, ";");
        }
        public static string[] ToArrayString(int[] arr)
        {
            string[] ret = new string[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                ret[i] = arr[i].ToString();
            }
            return ret;
        }
        public static string ToString(int[] arr, string splitter)
        {
            string ret = arr[0].ToString();
            for (int i = 1; i < arr.Length; i++)
            {
                ret += splitter + arr[i];
            }
            return ret;
        }
        public static string ToString(string[] arr, string splitter)
        {
            string ret = arr[0].ToString();
            for (int i = 1; i < arr.Length; i++)
            {
                ret += splitter + arr[i];
            }
            return ret;
        }
        public static string ToString(object[] arr, string splitter)
        {
            string ret = arr[0].ToString();
            for (int i = 1; i < arr.Length; i++)
            {
                ret += splitter + arr[i];
            }
            return ret;
        }
        public static byte[] toByteFloatArr(double[] s)
        {
            byte[] ret = new byte[s.Length * 4];
            for (int i = 0; i < s.Length; i++)
            {
                byte[] bt = BitConverter.GetBytes((float)s[i]);
                System.Array.Copy(bt, 0, ret, i * 4, 4);
            }
            return ret;
        }
        public static float[] ToFloat(double[] s)
        {
            float[] ret = new float[s.Length];
            for (int i = 0; i < s.Length; i++)
            {
                ret[i] = (float)s[i];
            }
            return ret;
        }
        public static double[] ToFloat(decimal[] s)
        {
            double[] ret = new double[s.Length];
            for (int i = 0; i < s.Length; i++)
            {
                ret[i] = (double)s[i];
            }
            return ret;
        }
        public static double IBM2Float(byte[] ibm_, int i)
        {
            bool positive;
            int power;
            uint abspower;
            long mant;
            double value, exp;

            positive = (ibm_[i + 0] & 0x80) == 0;
            mant = ((long)ibm_[i + 1] << 16) + ((long)ibm_[i + 2] << 8) + (long)ibm_[i + 3];
            power = (int)(ibm_[i + 0] & 0x7f) - 64;
            abspower = (uint)((power > 0) ? power : -power);       /* calc exp */
            exp = 16.0;
            value = 1.0;
            while (abspower != 0)
            {
                if ((abspower & 1) != 0)
                {
                    value *= exp;
                }
                exp = exp * exp;
                abspower >>= 1;
            }

            if (power < 0) value = 1.0 / value;
            value = value * mant / 16777216.0;
            if (!positive) value = -value;
            return value;
        }
        public static int[] allocFill(int length, int value)
        {
            int[] a = new int[length];
            for (int i = 0; i < a.Length; i++) a[i] = value;
            return a;
        }
        public static double[,] AllocFill(int length1, int length2, double value)
        {
            double[,] ret = new double[length1, length2];
            for (int i = 0; i < length1; i++)
            {
                for (int j = 0; j < length2; j++)
                {
                    ret[i, j] = value;
                }
            }
            return ret;
        }
        public static bool[] allocFill(int length, bool value)
        {
            bool[] a = new bool[length];
            for (int i = 0; i < a.Length; i++) a[i] = value;
            return a;
        }
        public static double[] allocFill(int length, double value)
        {
            double[] a = new double[length];
            for (int i = 0; i < a.Length; i++) a[i] = value;
            return a;
        }
        public static string[] allocFill(int length, string value)
        {
            string[] a = new string[length];
            for (int i = 0; i < a.Length; i++) a[i] = value;
            return a;
        }
        public static float[] allocFill(int length, float value)
        {
            float[] a = new float[length];
            for (int i = 0; i < a.Length; i++) a[i] = value;
            return a;
        }
        public static DateTime[] allocFill(int length, DateTime value)
        {
            DateTime[] a = new DateTime[length];
            for (int i = 0; i < a.Length; i++) a[i] = value;
            return a;
        }
        public static double[][] allocFill(int length0, int length1, double value)
        {
            double[][] a = new double[length0][];
            if (length1 != 0)
            {
                for (int i = 0; i < a.Length; i++)
                {
                    a[i] = allocFill(length1, value);
                }
            }
            return a;
        }
        public static double[][][] allocFill(int length0, int length1, int length2, double value)
        {
            double[][][] a = new double[length0][][];

            for (int i = 0; i < a.Length; i++)
            {
                a[i] = allocFill(length1, length2, value);
            }
            return a;
        }
        public static int[][][] allocFill(int length0, int length1, int length2, int value)
        {
            int[][][] a = new int[length0][][];

            for (int i = 0; i < a.Length; i++)
            {
                a[i] = allocFill(length1, length2, value);
            }

            return a;
        }
        public static int[][] allocFill(int length0, int length1, int value)
        {
            int[][] a = new int[length0][];
            if (length1 != 0)
            {
                for (int i = 0; i < a.Length; i++)
                {
                    a[i] = allocFill(length1, value);
                }
            }
            return a;
        }
        /// <summary>Заполнение массива указанными значениями.</summary>
        public static double[] Fill(double[] a, double value)
        {
            for (int i = 0; i < a.Length; i++) a[i] = value;
            return a;

        }
        /// <summary>Заполнение массива указанными значениями.</summary>
        public static void Fill(float[] a, float value)
        {
            for (int i = 0; i < a.Length; i++) a[i] = value;
        }
        public static void Fill(float[,] a, int i1q, int i2q, float value)
        {
            int i, j;
            for (i = 0; i < i1q; i++)
                for (j = 0; j < i2q; j++)
                    a[i, j] = value;
        }
        /// <summary>
        /// Существует значение в массиве?
        /// </summary>
        /// <param name="store">Массив.</param>
        /// <param name="value">Значение для поиска первого вхождения в массив.</param>
        /// <returns>Есть или нет значение в массиве.</returns>
        public static bool HasValue(bool[] store, bool value)
        {
            for (int i = 0; i < store.Length; i++)
                if (store[i] == value)
                    return true;
            return false;
        }
        /// <summary>
        /// Заполнение массива указанными значениями, например, no_data_code.
        /// </summary>
        public static void Fill(int[] a, int value)
        {
            for (int i = 0; i < a.Length; i++) a[i] = value;
        }
        /// <summary>
        /// Копирование массива. 
        /// Dst[I,J], Src[K,L], I >= K, J >= L.
        /// </summary>
        /// <param name="Src"></param>
        /// <param name="Dst"></param>
        public static bool Copy(float[,] Src, float[,] Dst)
        {
            int k, l,
                I = Dst.GetUpperBound(0) - Dst.GetLowerBound(0),
                J = Dst.GetUpperBound(1) - Dst.GetLowerBound(1),
                K = Src.GetUpperBound(0) - Src.GetLowerBound(0),
                L = Src.GetUpperBound(1) - Src.GetLowerBound(1)
            ;
            if (I < K || J < L)
                return false;

            for (k = 0; k <= K; k++)
                for (l = 0; l <= L; l++)
                    Dst[k, l] = Src[k, l];
            return true;
        }
        public static bool Copy(string[,] Src, string[,] Dst)
        {
            int k, l,
                I = Dst.GetUpperBound(0) - Dst.GetLowerBound(0),
                J = Dst.GetUpperBound(1) - Dst.GetLowerBound(1),
                K = Src.GetUpperBound(0) - Src.GetLowerBound(0),
                L = Src.GetUpperBound(1) - Src.GetLowerBound(1)
            ;
            if (I < K || J < L)
                return false;

            for (k = 0; k <= K; k++)
                for (l = 0; l <= L; l++)
                    Dst[k, l] = Src[k, l];
            return true;
        }
        public static void Fill(int[,] a, int value)
        {
            for (int i = a.GetLowerBound(0); i <= a.GetUpperBound(0); i++)
                for (int j = a.GetLowerBound(1); j <= a.GetUpperBound(1); j++)
                    a[i, j] = value;
        }
        public static void Fill(float[,] a, float value)
        {
            for (int i = a.GetLowerBound(0); i <= a.GetUpperBound(0); i++)
                for (int j = a.GetLowerBound(1); j <= a.GetUpperBound(1); j++)
                    a[i, j] = value;
        }
        public static void Fill(double[,] a, double value)
        {
            for (int i = a.GetLowerBound(0); i <= a.GetUpperBound(0); i++)
                for (int j = a.GetLowerBound(1); j <= a.GetUpperBound(1); j++)
                    a[i, j] = value;
        }

        /// <summary>
        /// Определение первого индекса массива store со значением value или -1, если такового нет. 
        /// </summary>
        public static int IndexOf(int[] store, int value)
        {
            return IndexOf(store, 0, value);
        }
        public static int IndexOf(int[] store, int startIndex, int value)
        {
            for (int j = startIndex; j < store.Length; j++)
                if (store[j] == value)
                    return j;
            return -1;
        }

        /// <summary>
        /// Существует значение в массиве
        /// </summary>
        /// <param name="store">Массив.</param>
        /// <param name="value">Значение для поиска первого вхождения в массив.</param>
        /// <returns>Есть или нет значение в массиве.</returns>
        static public bool HasValue(int[] store, int value)
        {
            int i = IndexOf(store, value);
            return (i == -1) ? false : true;
        }
        static public bool HasDoubles(int[] store)
        {
            for (int i = 0; i < store.Length - 1; i++)
            {
                if (IndexOf(store, i + 1, store[i]) >= 0)
                    return true;
            }
            return false;
        }
        public static void multiply(double[] arr, double multiplier)
        {
            for (int i = 0; i < arr.Length; i++)
                arr[i] *= multiplier;
        }
        public static void multiply(double[][] arr, double multiplier)
        {
            for (int i = 0; i < arr.Length; i++)
                multiply(arr[i], multiplier);
        }
        /// <summary>
        /// Поменять местами последние элементы с первыми.
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static double[] ReArrange(double[] data)
        {
            double d;
            int j;
            for (int i = 0; i < data.Length / 2; i++)
            {
                j = data.Length - 1 - i;
                d = data[i];
                data[i] = data[j];
                data[j] = d;
            }
            return data;
        }
        /// <summary>
        /// разделить элементы массива на div.
        /// </summary>
        public static void Divide(double[] arr, double div)
        {
            for (int i = 0; i < arr.Length; i++)
                arr[i] /= div;
        }
        /// <summary>
        /// Выбрать уникальные значения из массива.
        /// </summary>
        public static double[] Distinct(double[] arr)
        {
            List<double> ret = new List<double>();

            for (int i = 0; i < arr.Length; i++)
            {
                if (ret.IndexOf(arr[i]) < 0)
                    ret.Add(arr[i]);
            }
            return ret.ToArray();
        }
        public static DateTime[] Copy(DateTime[] arr, int startIdx, int finishIdx)
        {
            DateTime[] ret = new DateTime[finishIdx - startIdx + 1];
            for (int i = startIdx; i <= finishIdx; i++)
                ret[i - startIdx] = arr[i];
            return ret;
        }
        public static bool IsEqual(double[] arr1, double[] arr2)
        {
            if (arr1.Length != arr2.Length)
                return false;
            for (int i = 0; i < arr1.Length; i++)
                if (arr1[i] != arr2[i])
                    return false;
            return true;
        }
        public static double[] Copy(double[] arr, int startIdx, int finishIdx)
        {
            double[] ret = new double[finishIdx - startIdx + 1];
            for (int i = startIdx; i <= finishIdx; i++)
                ret[i - startIdx] = arr[i];
            return ret;
        }
        public static List<object> Copy(List<object> arr, int startIdx, int finishIdx)
        {
            List<object> ret = new List<object>(finishIdx - startIdx + 1);
            for (int i = startIdx; i <= finishIdx; i++)
                ret.Add(arr[i]);
            return ret;
        }
        public static List<object> Copy(List<object> arr)
        {
            return Copy(arr, 0, arr.Count - 1);
        }
        public static DateTime[] Copy(DateTime[] arr, int startIdx)
        {
            return Copy(arr, startIdx, arr.Length - 1);
        }
        public static DateTime[] Copy(DateTime[] arr)
        {
            return Copy(arr, 0, arr.Length - 1);
        }
        public static double[] copy(double[] arr)
        {
            double[] ret = new double[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                ret[i] = arr[i];
            return ret;
        }
        /// <summary>
        /// Получить массив с данными из входного массива с указанными индексами.
        /// </summary>
        /// <param name="data">Исходный массив.</param>
        /// <param name="idxs">Индексы исходного массива.</param>
        /// <returns>Массив с данными из входного массива с указанными индексами.</returns>
        static public double[] Copy(double[] data, int[] idxs)
        {
            double[] newData = Array.allocFill(idxs.Length, double.NaN);
            for (int i = 0; i < idxs.Length; i++)
            {
                newData[i] = data[idxs[i]];
            }
            return newData;
        }
        public static double[][] copy(double[][] arr)
        {
            double[][] ret = new double[arr.Length][];
            for (int i = 0; i < arr.Length; i++)
                ret[i] = copy(arr[i]);
            return ret;
        }

        public static double[][] Plus(double[][] arr1, double[][] arr2)
        {
            double[][] ret = new double[arr1.Length][];
            for (int i = 0; i < ret.Length; i++)
                ret[i] = Plus(arr1[i], arr2[i]);
            return ret;
        }
        /// <summary>
        /// Сумма двух массивов.
        /// </summary>
        public static double[] Plus(double[] arr1, double[] arr2)
        {
            double[] ret = new double[arr1.Length];
            for (int i = 0; i < ret.Length; i++)
                ret[i] = arr1[i] + arr2[i];
            return ret;
        }
        /// <summary>
        /// Создает числовой массив со значениями элементов от valS до  valF (шаг 1)
        /// </summary>
        /// <param name="valS"></param>
        /// <param name="valF"></param>
        /// <returns></returns>
        public static int[] createArraySF(int valS, int valF)
        {
            int[] ret = new int[valF - valS + 1];
            for (int i = valS; i <= valF; i++)
            {
                ret[i - valS] = i;
            }
            return ret;
        }
        /// <summary>
        /// Исключить из массива элемент с указанным индексом.
        /// </summary>
        public static double[] Exclude(double[] arr, int i)
        {
            List<double> ret = new List<double>();
            for (int j = 0; j < arr.Length; j++)
                if (j != i)
                    ret.Add(arr[j]);
            return (ret.Count == 0) ? null : ret.ToArray();
        }
        //public static double[] Equals(List<object>)
        //{
        //    List<double> ret = new List<double>();
        //    for (int j = 0; j < arr.Length; j++)
        //        if (j != i)
        //            ret.Add(arr[j]);
        //    return (ret.Count == 0) ? null : ret.ToArray();
        //}

        public static bool HasNaN(double[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
                if (Double.IsNaN(arr[i]))
                    return true;
            return false;
        }
        /// <summary>
        /// Ищет заданное значение в массиве и возвращает индексы этих элементов массива. Null - если элемента в массиве нет
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int[] FindIndex(int[] array, int value)
        {
            List<int> ret = new List<int>();
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == value)
                    ret.Add(i);
            }
            return (ret.Count > 0) ? ret.ToArray() : null;
        }

        public static double[] Abs(double[] data)
        {
            double[] ret = new double[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                ret[i] = Math.Abs(data[i]);
            }
            return ret;
        }

        public static double Sum(double[] arr)
        {
            double ret = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                ret += arr[i];
            }
            return ret;
        }
    }
}
