using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using SOV.Common;

namespace SOV.Geo
{

    /// <summary>
    ///Geo point.
    /// </summary>
    [DataContract]
    public class GeoPoint : IEquatable<GeoPoint>
    {
        /// <summary>
        /// WYSIWYG
        /// </summary>
        public const double GEO_MAXLONGITUDE_GRD = 360;
        public const double GEO_MAXLONGITUDE_MIN = 360 * 60;
        /// <summary>
        /// WYSIWYG
        /// </summary>
        public const double GEO_MINLONGITUDE_GRD = 0;
        /// <summary>
        /// WYSIWYG
        /// </summary>
        public const double GEO_MAXLATITUDE_GRD = 90;
        /// <summary>
        /// WYSIWYG
        /// </summary>
        public const double GEO_MINLATITUDE_GRD = -90;
        /// <summary>
        /// Инициализация экземпляра класса.
        /// </summary>
        public GeoPoint(double latGrd, double lonGrd)
        {
            init(latGrd, lonGrd);
        }
        /// <summary>
        /// Инициализация экземпляра класса.
        /// </summary>
        public GeoPoint(string lat, string lon)
        {
            init((GeoLLString.GetLatitude(lat)).ToGrad(), (GeoLLString.GetLongitude(lon)).ToGrad());
        }

        static public string Splitter = "x";
        /// <summary>
        /// Парсинг строкового представления гео-точки с разделителем "х"
        /// </summary>
        /// <param name="geoPointToString">Строка.</param>
        /// <param name="isMinute">ture, если в строке представлены координаты в минутах. В противном случае - в градусах.</param>
        /// <returns>Гео-точку.</returns>
        static public GeoPoint Parse(string geoPointToString, bool isMinute)
        {
            string[] s = geoPointToString.Split(Splitter[0]);
            if (isMinute)
            {
                return new GeoPoint(int.Parse(s[0]), int.Parse(s[1]));
            }
            else
            {
                return new GeoPoint(float.Parse(s[0]), float.Parse(s[1]));
            }
        }
        /// <summary>
        /// Строка в градусах
        /// </summary>
        /// <param name="geoPointToString"></param>
        /// <returns></returns>
        static public GeoPoint Parse(string geoPointToString)
        {
            return Parse(geoPointToString, false);
        }
        /// <summary>
        /// Перевод градусов в минуты.
        /// </summary>
        /// <param name="grd">Градусы.</param>
        /// <returns>Минуты.</returns>
        public static double Grd2Min(double grd)
        {
            return grd * 60;
        }
        /// <summary>
        /// Возвращает строку, представляющую текущий экземпляр класса.
        /// </summary>
        /// <returns>Строка, представляющая текущий экземпляр класса.</returns>
        public override string ToString()
        {
            return (double)LatGrd + Splitter + (double)LonGrd;
        }
        public string ToString(string doubleFormat)
        {
            return string.Format("{0:" + doubleFormat + "}{1}{2:" + doubleFormat + "}", LatGrd, Splitter, LonGrd);
        }
        /// <summary>
        /// Возвращает строку, представляющую текущий экземпляр класса в минутах.
        /// </summary>
        /// <returns>Строка, представляющая текущий экземпляр класса.</returns>
        public string ToStringMin()
        {
            return LatMin + Splitter + LonMin;
        }
        /// <summary>
        /// Возвращает или задаёт широту точки в градусах.
        /// </summary>
        [DataMember]
        public double LatGrd { get; set; }
        /// <summary>
        /// Возвращает или задаёт долготу точки в градусах.
        /// </summary>
        [DataMember]
        public double LonGrd { get; set; }
        /// <summary>
        /// Возвращает или задаёт широту точки в минутах.
        /// </summary>
        public double LatMin
        {
            get
            {
                return Grd2Min(LatGrd);
            }
            set
            {
                LatGrd = (float)(value / 60);
            }
        }
        /// <summary>
        /// Возвращает целые градусы широты точки.
        /// </summary>
        public int LatPartGradInt
        {
            get
            {
                return (int)LatGrd;
            }
        }
        /// <summary>
        /// Возвращает целые градусы долготы точки.
        /// </summary>
        public int LonPartGradInt
        {
            get
            {
                return (int)LonGrd;
            }
        }
        /// <summary>
        /// Возвращает минуты широты точки.
        /// </summary>
        public double LatPartMinFloat
        {
            get
            {
                return (LatGrd - LatPartGradInt) * 60f;
            }
        }
        /// <summary>
        /// Широта в радианах.
        /// </summary>
        public double LatRaians
        {
            get
            {
                return Vector.grad2Radians(LatGrd);
            }
        }
        /// <summary>
        /// Долгота в радианах.
        /// </summary>
        public double LonRadians
        {
            get
            {
                return Vector.grad2Radians(LonGrd);
            }
        }
        /// <summary>
        /// Возвращает минуты долготы точки.
        /// </summary>
        public double LonPartMinFloat
        {
            get
            {
                return (LonGrd - LonPartGradInt) * 60f;
            }
        }
        /// <summary>
        /// Возвращает целые минуты широты точки.
        /// </summary>
        public int LatPartMinInt
        {
            get
            {
                return (int)LatPartMinFloat;
            }
        }
        /// <summary>
        /// Возвращает целые минуты долготы точки.
        /// </summary>
        public int LonPartMinInt
        {
            get
            {
                return (int)LonPartMinFloat;
            }
        }
        /// <summary>
        /// Возвращает или задаёт долготу точки в минутах.
        /// </summary>
        public double LonMin
        {
            get
            {
                return Grd2Min(LonGrd);
            }
            set
            {
                LonGrd = value / 60;
            }
        }
        static public bool Test(double lat, double lon)
        {
            if (lon <= GEO_MAXLONGITUDE_GRD && lon >= GEO_MINLONGITUDE_GRD && lat <= GEO_MAXLATITUDE_GRD && lat >= GEO_MINLATITUDE_GRD)
                return true;
            return false;
        }
        private void init(double lat, double lon)
        {
            if (!Test(lat, lon))
                throw new Exception("Point is wrong: " + lat + ";" + lon);
            this.LatGrd = lat;
            this.LonGrd = lon;

        }
        /// <summary>
        /// Преобразование частей гео-координаты в доли градусов.
        /// </summary>
        /// <param name="grd">Часть - градусы.</param>
        /// <param name="min">Часть - минуты.</param>
        /// <param name="sec">Часть - секунды.</param>
        /// <returns>Градусы.</returns>
        public static double toDouble(int grd, int? min, int? sec)
        {
            return (double)grd + ((min == null) ? 0 : ((double)min) / 60) + ((sec == null) ? 0 : ((double)sec) / 3600);
        }

        public double getDistMin(GeoPoint gp)
        {

            double dx = Math.Abs(this.LonMin - gp.LonMin);
            if (dx > 180) dx = 360 - dx;
            double dy = Math.Abs(this.LatMin - gp.LatMin);
            return Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2));
        }

        public bool Equals(GeoPoint other)
        {
            //Check whether the compared object is null. 
            if (Object.ReferenceEquals(other, null)) return false;

            //Check whether the compared object references the same data. 
            if (Object.ReferenceEquals(this, other)) return true;
            return LatGrd.Equals(other.LatGrd) && LonGrd.Equals(other.LonGrd);
        }
        public override int GetHashCode()
        {

            //Get hash code for the Name field if it is not null. 
            int hashGeoPointsLatGrd = LatGrd.GetHashCode();

            //Get hash code for the Code field. 
            int hashGeoPointsLonGrd = LonGrd.GetHashCode();

            //Calculate the hash code for the product. 
            return hashGeoPointsLatGrd ^ hashGeoPointsLonGrd;
        }
    }
}
