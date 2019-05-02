using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SOV.Common
{
    public class StrVia
    {
        /// <summary>
        /// При невозможности parse возвращаются NaN.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static double[] ParseDoubleArray(string[] s)
        {
            double[] ret = new double[s.Length];
            for (int i = 0; i < ret.Length; i++)
            {
                ret[i] = ParseDouble(s[i]);
            }
            return ret;
        }
        /// <summary>
        /// Nullable int.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        static public int? ParseInt(string s)
        {
            return string.IsNullOrEmpty(s) ? null : (int?)int.Parse(s);
        }
        /// <summary>
        /// Возврат NaN при невозможности парсингаю
        /// </summary>
        /// <param name="s"></param>
        /// <returns>double ||NaN</returns>
        static public double ParseDouble(string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                double ret;
                if (double.TryParse(s.Replace(',', '.'), out ret)) return ret;
                if (double.TryParse(s.Replace('.', ','), out ret)) return ret;
            }
            return double.NaN;
        }

        static public string[] Parse(string s, int[] fieldLength)
        {
            string[] ret = new string[fieldLength.Length];
            for (int i = 0, j = 0; i < ret.Length; j += fieldLength[i++])
            {
                ret[i] = s.Substring(j, fieldLength[i]).Trim();
            }
            return ret;
        }
        static public string ToString(List<DateTime> list, string splitter = ",")
        {
            string ret = string.Empty;
            if (list != null && list.Count != 0)
            {
                ret = list[0].ToString();
                for (int i = 1; i < list.Count; i++)
                {
                    ret += splitter + list[i];
                }
            }
            return ret;
        }
        static public string ToString(Dictionary<string, string> dic, string splitter1 = "=", string splitter2 = ";")
        {
            string ret = string.Empty;
            for (int i = 0; i < dic.Count; i++)
            {
                ret += dic.ElementAt(i).Key + splitter1 + dic.ElementAt(i).Value + splitter2;
            }
            return ret;
        }
        /// <summary>
        /// Создать массив строк формата "sql in" из кодов (Id) коллекции.
        /// В каждой строке содержится не более sqlInMaxQ кодов.
        /// </summary>
        /// <param name="sqlInMaxQ">Кол. Id элементов коллекции, включаемых в одну строку. От этого зависит кол. djpdhfoftvs[ строк "sql in".</param>
        /// <returns></returns>
        static public List<string> ToStringSqlIn(List<int> id, int sqlInMaxQ)
        {
            List<string> ret = new List<string>();

            for (int i = 0; i < id.Count; )
            {
                int k = (i + sqlInMaxQ > id.Count) ? id.Count : i + sqlInMaxQ;
                string inId = string.Empty;

                for (int j = i; j < k; j++)
                {
                    inId += id[j] + ",";
                }
                i = k;
                if (!string.IsNullOrEmpty(inId))
                {
                    ret.Add(inId.Substring(0, inId.Length - 1));
                }
            }
            return ret;
        }

        static public string ToString(List<int> list, string splitter = ",")
        {
            string ret = string.Empty;
            if (list != null && list.Count != 0)
            {
                ret = list[0].ToString();
                for (int i = 1; i < list.Count; i++)
                {
                    ret += splitter + list[i];
                }
            }
            return ret;
        }
        static public int[] ToArray<T>(string str, char splitter = ';')
        {
            string[] s = str.Split(splitter);
            int[] ret = new int[s.Length];
            for (int i = 0; i < s.Length; i++)
            {
                ret[i] = int.Parse(s[i]);
            }
            return ret;
        }
        static public TimeSpan[] ToArrayTimeSpan(string str, char splitter = ';')
        {
            string[] s = str.Split(splitter);
            TimeSpan[] ret = new TimeSpan[s.Length];
            for (int i = 0; i < s.Length; i++)
            {
                ret[i] = TimeSpan.FromHours(Common.StrVia.ParseDouble(s[i]));
            }
            return ret;
        }
        static public double[] ToArrayDouble(string str, char splitter = ';')
        {
            string[] s = str.Split(splitter);
            double[] ret = new double[s.Length];
            for (int i = 0; i < s.Length; i++)
            {
                ret[i] = double.Parse(s[i]);
            }
            return ret;
        }
        static public string ToString(List<long> list, string splitter = ",")
        {
            string ret = string.Empty;
            if (list != null && list.Count != 0)
            {
                ret = list[0].ToString();
                for (int i = 1; i < list.Count; i++)
                {
                    ret += splitter + list[i];
                }
            }
            return ret;
        }
        /// <summary>
        /// ВСЁ TO_UPPER !
        /// </summary>
        /// <param name="strVia">Строка с разделителями.</param>
        /// <param name="dev1">Разделитель ;</param>
        /// <param name="dev2">Разделитель =</param>
        /// <returns></returns>
        static public Dictionary<string, string> ToDictionary(string strVia, char dev1 = ';', char dev2 = '=')
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            strVia = strVia.Trim();
            if (!string.IsNullOrEmpty(strVia))
            {
                foreach (var s in strVia.Split(dev1))
                {
                    if (s.Trim().IndexOf("//") == 0)
                        continue;

                    var sarr = s.Split(dev2);
                    if (sarr.Length > 2) throw new Exception("Incorrect strVia format: " + strVia);

                    string sarr0 = sarr[0].Trim();
                    if (sarr.Length > 0 && !string.IsNullOrEmpty(sarr0))
                        ret.Add(sarr0.ToUpper(), sarr.Length == 2 ? sarr[1].Trim() : null);
                }
            }
            return ret;
        }
        /// <summary>
        /// ВСЁ TO_UPPER !
        /// </summary>
        /// <param name="strVia">Строка с разделителями.</param>
        /// <param name="dev1">Разделитель ;</param>
        /// <param name="dev2">Разделитель =</param>
        /// <returns></returns>
        static public Dictionary<string, int?> ToDictionarySI(string strVia, char dev1 = ';', char dev2 = '=')
        {
            Dictionary<string, int?> ret = new Dictionary<string, int?>();
            strVia = strVia.Trim();
            if (!string.IsNullOrEmpty(strVia))
            {
                foreach (var s in strVia.Split(dev1))
                {
                    var ss = s.Split(dev2);
                    if (ss.Length > 2) throw new Exception("Incorrect strVia format: " + strVia);
                    if (ss.Length > 0)
                        ret.Add(ss[0].Trim().ToUpper(), ss.Length == 2 ? (int?)int.Parse(ss[1].Trim()) : null);
                }
            }
            return ret;
        }
        /// <summary>
        /// Парсинг строки состоящей из полей раздедённых dev.
        /// Количество полей д.б. кратно 2.
        /// </summary>
        /// <param name="strPairs">Строка, состоящая из полей раздедённых dev. Кол. полей кратно 2.</param>
        /// <param name="dev">Разделитель полей в строке.</param>
        /// <returns></returns>
        static public Dictionary<string, string> ToDictionaryPairs(string strPairs, char dev)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            strPairs = strPairs.Trim();
            if (!string.IsNullOrEmpty(strPairs))
            {
                string[] s = strPairs.Split(dev);
                for (int i = 0; i < s.Length; i += 2)
                {
                    ret.Add(s[i].Trim(), s[i + 1].Trim());
                }
            }
            return ret;
        }
        static public List<int> ToListInt(string s, string splitter = ",")
        {


            if (!string.IsNullOrEmpty(s.Trim()))
                return ToListInt(s.Split(splitter.ToCharArray()));
            return null;
        }
        static public List<int> ToListInt(string[] s)
        {
            if (s != null && s.Length > 0)
            {
                List<int> ret = new List<int>();
                foreach (var item in s)
                {
                    if (string.IsNullOrEmpty(item))
                        ret.Add(int.MinValue);
                    else
                        ret.Add(int.Parse(item));
                }
                return ret;
            }
            return null;
        }
        /// <summary>
        /// Получить значение поля из строки формата 1.
        /// Формат 1 строки: FIELDNAME1=FIELDVALUE1;FIELDNAME2=FIELDVALUE2;...
        ///		where dev1 = "=" and dev2 = ";".
        /// </summary>
        /// <param name="dev1">Первый разделитель "="</param>
        /// <param name="dev2">Второй разделитель ";"</param>
        /// <param name="valueName">Имя поле значение которого ищется.</param>
        /// <returns>Значение поля или null, если не найдено.</returns>
        static public string GetValue(string s_in, string valueName, char dev1 = '=', char dev2 = ';')
        {
            string[] split = s_in.Split(dev2);
            foreach (string s in split)
            {
                string[] ss = s.Split(dev1);
                if (ss[0].ToUpper().Trim().IndexOf(valueName.ToUpper().Trim()) >= 0)
                    return ss[1];
            }
            return null;
        }

        static public string ToString(double[] list, string splitter = ",")
        {
            string ret = string.Empty;
            if (list != null && list.Length != 0)
            {
                ret = list[0].ToString();
                for (int i = 1; i < list.Length; i++)
                {
                    ret += splitter + list[i];
                }
            }
            return ret;
        }
    }
}
