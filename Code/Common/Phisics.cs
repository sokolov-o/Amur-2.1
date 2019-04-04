using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Common
{
    public partial class Phisics
    {
        /// <summary>
        /// Абсолютный ноль по шкале Цельсия.
        /// </summary>
        public static double AbsZeroInCelsius = -273.15;
        /// <summary>
        /// Давление водяного пара в воздухе при заданной температуре (e, гПа).
        /// http://www.vaisala.com/Vaisala%20Documents/Application%20notes/Humidity_Conversion_Formulas_B210973EN-F.pdf
        /// 
        /// Формула 3, стрю 4
        /// 
        /// </summary>
        /// <param name="TCelsius">Degree Celsius</param>
        /// <returns>hPa</returns>
        [Obsolete("Использовать необходимый метод GetSaturatedVapourPressureWater или GetSaturatedVapourPressureIce")]
        public static double GetSaturatedVapourPressure(double TCelsius)
        {
            return GetSaturatedVapourPressureWater(TCelsius);
        }
        /// <summary>
        /// Давление водяного пара в воздухе при заданной температуре > 0 грд. Цельсия  (e, гПа) 
        /// http://www.vaisala.com/Vaisala%20Documents/Application%20notes/Humidity_Conversion_Formulas_B210973EN-F.pdf
        /// Формула 3, стр. 4
        /// </summary>
        /// <param name="TCelsius">Degree Celsius</param>
        /// <returns>hPa</returns>
        public static double GetSaturatedVapourPressureWater(double TCelsius)
        {
            double Tkelv = TCelsius - AbsZeroInCelsius;
            double Pc = 220640;
            double Tc = 647.096;
            double theta = (1 - Tkelv / Tc);
            double a1 = -7.85951783, a2 = 1.84408259, a3 = -11.7866497, a4 = 22.6807411, a5 = -15.9618719, a6 = 1.80122502;

            double e = 0;

            e = Pc * Math.Exp(Tc / Tkelv * (a1 * theta + a2 * Math.Pow(theta, 1.5) + a3 * Math.Pow(theta, 3) + a4 * Math.Pow(theta, 3.5) + a5 * Math.Pow(theta, 4) + a6 * Math.Pow(theta, 7.5)));

            return e;
        }

        /// <summary>
        /// Давление водяного пара в воздухе надо льдом при заданной температуре меньше 0 грд. Цельсия  (e, гПа) 
        /// http://www.vaisala.com/Vaisala%20Documents/Application%20notes/Humidity_Conversion_Formulas_B210973EN-F.pdf
        /// Формула 5, стр. 5
        /// </summary>
        /// <param name="TCelsius">Degree Celsius</param>
        /// <returns>hPa</returns>
        public static double GetSaturatedVapourPressureIce(double TCelsius)
        {
            if (TCelsius > 0)
                return double.NaN;
            double Tkelv = TCelsius - AbsZeroInCelsius;
            double Pn = 6.11657;
            double Tn = 273.16;
            double theta = Tkelv / Tn;
            double a0 = -13.928169, a1 = 34.707823;

            double e = 0;

            e = Pn * Math.Exp(a0 * (1 - Math.Pow(theta, -1.5)) + a1 * (1 - Math.Pow(theta, -1.25)));

            return e;
        }

        /// <summary>
        /// Дефицит упругости
        /// </summary>
        /// <param name="TaCelsius">Темп. воздуха.</param>
        /// <param name="TdCelsius">Темп. точки росы.</param>
        /// <returns></returns>
        public static double GetSaturatedVapourPressureDeficit(double TaCelsius, double TdCelsius)
        {
            return GetSaturatedVapourPressureWater(TaCelsius) - GetSaturatedVapourPressureWater(TdCelsius);
        }

        /// <summary>
        /// Относительная влажность
        /// </summary>
        /// <param name="TaCelsius">Темп. воздуха.</param>
        /// <param name="TdCelsius">Темп. точки росы.</param>
        /// <returns></returns>
        public static double GetRelativeHumidity(double TaCelsius, double TdCelsius)
        {
            return 100 * Math.Round(GetSaturatedVapourPressureWater(TdCelsius), 2) / Math.Round(GetSaturatedVapourPressureWater(TaCelsius), 2);
        }
    }
}
