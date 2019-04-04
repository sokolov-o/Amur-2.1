using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Common;

namespace SOV.Geo
{
    /// <summary>
    /// Условие (тип) ближайшей точки.
    /// </summary>
    public enum EnumPointNearestType
    {
        /// <summary>
        /// Не интерполировать, а выбрать значение ближайшей точки
        /// </summary>
        Nearest = 0,
        /// <summary>
        /// 
        /// </summary>
        Nearest2East = 1,
        /// <summary>
        /// 
        /// </summary>
        Nearest2West = 2,
        /// <summary>
        /// 
        /// </summary>
        Nearest2South = 3,
        /// <summary>
        /// 
        /// </summary>
        Nearest2North = 4,
        /// <summary>
        /// Линейная интерполяция
        /// </summary>
        Interpolate = 5
    }

    public enum EnumProjection : long
    {
        LATLON = 0, //	Latitude/Longitude Grid also called Equidistant Cylindrical or Plate Carree projection grid
        MERCATOR = 1, //Mercator Projection Grid 
        GNOMONIC = 2, //	Gnomonic Projection Grid 
        LAMBERT = 3, //	Lambert Conformal, secant or tangent, conical or bipolar (normal or oblique) Projection Grid
        GAUSS = 4, //	Gaussian Latitude/Longitude Grid
        POLAR = 5, //	Polar Stereographic Projection Grid
        LAMBERT_OBL = 13, //Oblique Lambert conformal, secant or tangent, conical or bipolar, projection 
    }
    /// <summary>
    /// Тип расчёта расстояний на сфере.
    /// </summary>
    public enum EnumDistanceType
    {
        /// <summary>
        /// Плоскость.
        /// </summary>
        Plane = 1,
        /// <summary>
        /// Для больших расстояний на сфере?
        /// </summary>
        TheoremCos = 2,
        /// <summary>
        /// По формуле гаверсинусов - для НЕбольших расстояний на сфере?
        /// </summary>
        TheoremHaverSin = 3
    }

    public class Geo
    {
        /// <summary>
        /// Радиус Земли (м).
        /// </summary>
        public const double R = 6372795.0;
        static public double SphereDistanceG(double lon1grad, double lon2grad, double lat1grad, double lat2grad, EnumDistanceType distanceType)
        {
            return SphereDistance(Vector.grad2Radians(lon1grad), Vector.grad2Radians(lon2grad), Vector.grad2Radians(lat1grad), Vector.grad2Radians(lat2grad), distanceType);
        }
        /// <summary>
        /// Расчёт расстояния (m) на сфере с радиусом Земли по заданным (в радианах) координатам двух точек.
        /// Для DistanceType.Plane считаем (тупо) корень из суммы квадратов того, что подали в метод.
        /// </summary>
        /// <param name="lon1rad">радианы</param>
        /// <param name="lon2rad">радианы</param>
        /// <param name="lat1rad">радианы</param>
        /// <param name="lat2rad">радианы</param>
        /// <param name="distanceType">Формула расчёта расстояния.</param>
        /// <returns>Расстояние (m) или double.NaN, если неизвестен DistanceType.</returns>
        static public double SphereDistance(double lon1rad, double lon2rad, double lat1rad, double lat2rad, EnumDistanceType distanceType)
        {
            double dx = lon2rad - lon1rad;
            double dy = lat2rad - lat1rad;
            //if (dx == 0 && dy == 0)
            //    return 0;

            switch (distanceType)
            {
                case EnumDistanceType.Plane:
                    return Vector.uv2Module(dx, dy);
                case EnumDistanceType.TheoremCos:
                    return Geo.R * Math.Acos(Math.Sin(lat1rad) * Math.Sin(lat2rad) + Math.Cos(lat1rad) * Math.Cos(lat2rad) * Math.Cos(dx));
                case EnumDistanceType.TheoremHaverSin:
                    return Geo.R * 2 * Math.Asin(
                        Math.Sqrt(Math.Pow((Math.Sin(dy)) / 2.0, 2) + Math.Cos(lat1rad) * Math.Cos(lat2rad) * Math.Pow(Math.Sin(dx / 2.0), 2))
                    );
                default:
                    return double.NaN;

            }
        }
        ///// <summary>
        ///// Средневзвешенная линейная интерполяция в заданную координату между точками. Веса - расстояния от заданной до соответствующей точки.
        ///// </summary>
        ///// <param name="lonrad">Долгота искомой точки (рад).</param>
        ///// <param name="latrad">Широта искомой точки (рад).</param>
        ///// <param name="values">Значения в точках (рад).</param>
        ///// <param name="lonsrad">Долготы точек (рад).</param>
        ///// <param name="latsrad">Широты точек (рад).</param>
        ///// <param name="distanceType">Тип расчёта расстояния, необходимого для весов.</param>
        ///// <returns>Интерполированное значение.</returns>
        //static public double Interpolate(double lonrad, double latrad, double[] values, double[] lonsrad, double[] latsrad, EnumDistanceType distanceType)
        //{
        //    double ret = 0, weightSum = 0;

        //    for (int i = 0; i < values.Length; i++)
        //    {
        //        if (lonrad == lonsrad[i] && latrad == latsrad[i])
        //            return values[i];

        //        double w = SphereDistance(lonrad, lonsrad[i], latrad, latsrad[i], distanceType);
        //        //w = w * w;
        //        ret += (values[i] / w);
        //        weightSum += (1 / w);
        //    }
        //    return ret / weightSum;
        //}
    }
}
