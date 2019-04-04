using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SOV.Common;
using System.Runtime.Serialization;

namespace SOV.Geo
{
    /// <summary>
    /// Географический регион (трапеция на сфере).
    ///ВНИМАНИЕ: не учтены регионы с переходом через Гринвич.
    /// </summary>
    [DataContract]
    public class GeoRectangle
    {
        [DataMember]
        public int Id { get; set; }
        /// <summary>
        /// Северо-западный угол региона.
        /// </summary>
        [DataMember]
        public GeoPoint NorthWest;
        /// <summary>
        /// Юго-восточный угол региона.
        /// </summary>
        [DataMember]
        public GeoPoint SouthEast;
        /// <summary>
        /// Наименование региона.
        /// </summary>
        [DataMember]
        public string Name;
        /// <summary>
        /// Инициализация экземпляра класса.
        /// </summary>
        public GeoRectangle(GeoPoint centralPoint, double latGrdFromCenterPoint, double lonGrdFromCenterPoint, string name)
        {
            init(
                centralPoint.LatGrd - latGrdFromCenterPoint,
                centralPoint.LatGrd + latGrdFromCenterPoint,
                centralPoint.LonGrd - lonGrdFromCenterPoint,
                centralPoint.LonGrd + lonGrdFromCenterPoint,
                name);
        }
        /// <summary>
        /// Инициализация экземпляра класса.
        /// </summary>
        public GeoRectangle(GeoPoint north_west, GeoPoint south_east, string name)
        {
            init(south_east.LatGrd, north_west.LatGrd, north_west.LonGrd, south_east.LonGrd, name);
        }
        /// <summary>
        /// Инициализация экземпляра класса.
        /// </summary>
        public GeoRectangle(double south, double north, double west, double east, string name, int id = -1)
        {
            init(south, north, west, east, name);
            Id = id;
        }
        /// <summary>
        /// Инициализация экземпляра класса.
        /// </summary>
        public GeoRectangle(GeoRectangle gr)
        {
            init(gr.SouthEast.LatGrd, gr.NorthWest.LatGrd, gr.NorthWest.LonGrd, gr.SouthEast.LonGrd, gr.Name);
        }
        /// <summary>
        /// Получить значение широты южной границы региона (град).
        /// </summary>
        public double South { get { return SouthEast.LatGrd; } }
        /// <summary>
        /// Получить значение широты северной границы региона (град).
        /// </summary>
        public double North { get { return NorthWest.LatGrd; } }
        /// <summary>
        /// Получить значение долготы западной границы региона (град).
        /// </summary>
        public double West { get { return NorthWest.LonGrd; } }
        /// <summary>
        /// Получить значение долготы восточной границы региона (град).
        /// </summary>
        public double East { get { return SouthEast.LonGrd; } set { SouthEast.LonGrd = value; } }
        /// <summary>
        /// Инициализация экземпляра класса.
        /// </summary>
        /// <param name="s">Строковое представление региона с разделителем ;</param>
        private GeoRectangle(string s)
        {
            string[] ss = s.Split(new char[] { ';' });
            init(StrVia.ParseDouble(ss[1]), StrVia.ParseDouble(ss[0]), StrVia.ParseDouble(ss[2]), StrVia.ParseDouble(ss[3]), "");
        }

        public GeoRectangle(double[,] pointsLatLon)
        {
            if (pointsLatLon.GetLength(0) != 4) throw new Exception("pointsLatLon.GetLength() != 4");

            double south = double.MaxValue, north = double.MinValue, west = double.MaxValue, east = double.MinValue;
            for (int i = 0; i < 4; i++)
            {
                north = pointsLatLon[i, 0] > north ? pointsLatLon[i, 0] : north;
                south = pointsLatLon[i, 0] < south ? pointsLatLon[i, 0] : south;
                west = pointsLatLon[i, 1] < west ? pointsLatLon[i, 1] : west;
                east = pointsLatLon[i, 1] > east ? pointsLatLon[i, 1] : east;
            }
            NorthWest = new GeoPoint(north, west);
            SouthEast = new GeoPoint(south, east);
        }
        /// <summary>
        /// Пересекает/включает регион широту Гринвича?
        /// </summary>
        public bool IsIntersectGreenwich()
        {
            return (SouthEast.LonGrd < NorthWest.LonGrd) ? true : false;
        }
        /// <summary>
        /// Является ли регион точкой
        /// </summary>
        /// <returns></returns>
        public bool IsPoint()
        {
            if (North == South && West == East)
                return true;
            else
                return false;
        }
        /// <summary>
        /// Принадлежит точка региону?
        /// </summary>
        public bool IsPointBelong(GeoPoint geoPoint)
        {
            return IsPointBelong(geoPoint.LatGrd, geoPoint.LonGrd);
        }
        /// <summary>
        /// Принадлежит точка региону?
        /// </summary>
        public bool IsPointBelong(double lat, double lon)
        {
            if (lat >= South && lat <= North)
            {
                if (IsIntersectGreenwich())
                {
                    if (lon >= West && lon <= 360 || lon <= East && lon >= 0)
                        return true;
                }
                else
                {
                    if (lon >= West && lon <= East)
                        return true;
                }
            }
            return false;
        }
        public bool IsLatGrdBelong(double latGrd)
        {
            if (latGrd >= South && latGrd <= North) return true;
            return false;
        }
        public bool IsLonGrdBelong(double lonGrd)
        {
            if (IsIntersectGreenwich())
            {
                if (lonGrd >= West && lonGrd <= 360 || lonGrd <= East && lonGrd >= 0)
                    return true;
            }
            else
            {
                if (lonGrd >= West && lonGrd <= East)
                    return true;
            }
            return false;
        }
        private void init(double s, double n, double w, double e, string name)
        {
            if (w < 0) throw new Exception("(w < 0)");
            if (e < 0) throw new Exception("(e < 0)");
            if (s > n) throw new Exception("(south > north)");
            if (w > e) throw new Exception("(west > east)");

            NorthWest = new GeoPoint(n, w);
            SouthEast = new GeoPoint(s, e);
            this.Name = name;
        }
        /// <summary>
        /// Преобразование долготы [0:360] в интервал [-180:0:+180].
        /// </summary>
        public static double lon_180(double lon)
        {
            return (lon > 180) ? lon - 360 : lon;
        }

        static char[] GEOREGS_SPLITTER = new char[] { '/' };
        /// <summary>
        /// Возвращает строку, представляющую текущий экземпляр класса. Разделитель / Строку можно использовать для создания нового экземпляра класса.
        /// </summary>
        /// <returns>Строка, представляющая текущий экземпляр класса.</returns>
        static public string ToString(GeoRectangle[] gr)
        {
            string ret = "";
            for (int i = 0; i < gr.Length; i++)
            {
                ret += ((i == 0) ? "" : new String(GEOREGS_SPLITTER)) + gr[i].ToString();
            }
            return ret;
        }
        /// <summary>
        /// Получить экземпляры класса из строки, содержит инф-ию о нескольких регионах 
        /// с разделителем регинов GEOREGS_SPLITTER.
        /// </summary>
        /// <param name="geoRegs">Строковое представление регионов.</param>
        /// <returns></returns>
        static public GeoRectangle[] ParseGeoRegions(string geoRegs)
        {
            string[] s = geoRegs.Split(GEOREGS_SPLITTER);
            GeoRectangle[] ret = new GeoRectangle[s.Length];
            for (int i = 0; i < ret.Length; i++)
            {
                ret[i] = new GeoRectangle(s[i]);
            }
            return ret;
        }
        static public GeoRectangle Parse(string sgeoReg)
        {
            return new GeoRectangle(sgeoReg);
        }
        /// <summary>
        /// Возвращает строку, представляющую текущий экземпляр класса. Разделитель / Строку можно использовать для создания нового экземпляра класса.
        /// </summary>
        /// <returns>Строка, представляющая текущий экземпляр класса.</returns>
        public override string ToString()
        {
            return (NorthWest.LatGrd + ";" + SouthEast.LatGrd + ";" + NorthWest.LonGrd + ";" + SouthEast.LonGrd).Replace(",", ".");
        }
        /// <summary>
        /// Определяет вложенность регионов.
        /// </summary>
        /// <param name="g1">Первый регион.</param>
        /// <param name="g2">Второй регион.</param>
        /// <returns>true, если первый регион вложен или равен второму.</returns>
        public static bool operator <=(GeoRectangle g1, GeoRectangle g2)
        {
            if (g1 < g2 || g1 == g2) return true;
            return false;
        }
        /// <summary>
        /// Определяет вложенность регионов.
        /// </summary>
        /// <param name="g1">Первый регион.</param>
        /// <param name="g2">Второй регион.</param>
        /// <returns>true, если второй регион вложен или равен первому.</returns>
        public static bool operator >=(GeoRectangle g1, GeoRectangle g2)
        {
            if (g1 > g2 || g1 == g2) return true;
            return false;
        }
        /// <summary>
        /// Определяет вложенность регионов.
        /// </summary>
        /// <param name="g1">Первый регион.</param>
        /// <param name="g2">Второй регион.</param>
        /// <returns>true, если первый регион вложен во второй.</returns>
        public static bool operator <(GeoRectangle g1, GeoRectangle g2)
        {
            if (g1 == g2) return false;
            if (
                   g1.North <= g2.North && g1.South >= g2.South
                && g1.West >= g2.West && g1.East <= g2.East
                )
                return true;
            return false;
        }
        /// <summary>
        /// Определяет вложенность регионов.
        /// </summary>
        /// <param name="g1">Первый регион.</param>
        /// <param name="g2">Второй регион.</param>
        /// <returns>true, если второй регион вложен в первый.</returns>
        public static bool operator >(GeoRectangle g1, GeoRectangle g2)
        {
            if (g1 == g2) return false;
            if (
                   g1.North >= g2.North && g1.South <= g2.South
                && g1.West <= g2.West && g1.East >= g2.East
                )
                return true;
            return false;
        }
        /// <summary>
        /// Определяет равенство границ регионов
        /// Без проверки совпадения названия региона.
        /// </summary>
        public static bool operator ==(GeoRectangle g1, GeoRectangle g2)
        {
            try
            {
                if (g1.NorthWest.LatGrd == g2.NorthWest.LatGrd && g1.NorthWest.LonGrd == g2.NorthWest.LonGrd
                    && g1.SouthEast.LatGrd == g2.SouthEast.LatGrd && g1.SouthEast.LonGrd == g2.SouthEast.LonGrd)
                    return true;
            }
            catch
            {
                return false; // g2 || g1 is null
            }
            return false;
        }
        /// <summary>
        /// Определяет не равенство границ регионов
        /// Без проверки совпадения названия региона.
        /// </summary>
        public static bool operator !=(GeoRectangle g1, GeoRectangle g2)
        {
            return !(g1 == g2);
        }
        /// <summary>
        /// return true;
        /// </summary>
        /// <param name="o">Object to compare.</param>
        /// <returns>true</returns>
        public override bool Equals(object o)
        {
            return true;
        }
        /// <summary>
        /// return 0;
        /// </summary>
        /// <returns>0</returns>
        public override int GetHashCode()
        {
            return 0;
        }

        /// <summary>
        /// Возвращает SQL-предложение where (без самого слова where) для отбора региона из базы данных. Предложение заключённое в скобки.
        /// </summary>
        /// <param name="isIncludeBounds">Признак равенства границ искомого региона региону this.</param>
        /// <returns>SQL-предложение where (без самого слова where).</returns>
        public string getSqlWhere(bool isIncludeBounds)
        {
            string s =
                "("
                    + " lat < " + NorthWest.LatGrd
                    + " and lat > " + SouthEast.LatGrd
                    + " and lon < " + SouthEast.LonGrd
                    + " and lon > " + NorthWest.LonGrd
                + ")";
            return (isIncludeBounds) ? s.Replace("<", "<=").Replace(">", ">=") : s;
        }
        public double getLonBeltMin()
        {
            if (IsIntersectGreenwich())
            {
                throw new Exception("isIntersectGreenwich()");
            }
            return GeoPoint.Grd2Min(SouthEast.LonGrd - NorthWest.LonGrd);
        }

        public double getLatBeltMin()
        {
            return GeoPoint.Grd2Min(NorthWest.LatGrd - SouthEast.LatGrd);
        }
    }
}
