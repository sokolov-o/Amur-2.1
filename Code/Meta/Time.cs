using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Amur.Meta
{
    public class Time
    {
        /// <summary>
        /// Словарь названий временных интервалов различных типов.
        /// </summary>
        static Dictionary<EnumDateType, string[/*date period name (0 - rus;1 - eng)*/]> DATE_PERIOD_NAME = new Dictionary<EnumDateType, string[]>()
        {
            {EnumDateType.Meteo, new string[]{"метео-дата","meteo-daу"} },
            {EnumDateType.MeteoDay, new string[]{"метео-сутки","meteo-day"} },
            {EnumDateType.MeteoDayNight, new string[]{"день","ночь","day","night"} }
        };

        /// <summary>
        /// Начало метео-суток во времени UTC для указанного метео-времени.
        /// </summary>
        public static DateTime GetUTCStart4DateMeteo(DateTime dateTimeMeteo, int UTCHour4MeteoDayStart)
        {
            return dateTimeMeteo.Date.AddDays(-1).AddHours(UTCHour4MeteoDayStart).AddSeconds(1);
        }

        public static string GetDatePeriodName(EnumDateType dateType, EnumLanguage lang)
        {
            string[] names = DATE_PERIOD_NAME[dateType];
            if (names.Length != 2)
                throw new NotImplementedException();

            return names[lang == EnumLanguage.Rus ? 0 : 1];
        }
        /// <summary>
        /// Преобразование data.DateFcs к требуемому типу.
        /// </summary>
        /// <param name="iniDateUTC">Дата-время ВСВ.</param>
        /// <param name="dstDateType">Тип интервала, к которому нужно привести вх. дату-время.</param>
        /// <param name="meteoZone">Метео-зона, к которой относится преобразование.</param>
        static public object[/*DateTime converted;Date period name*/] ToDate(DateTime iniDateUTC, EnumDateType dstDateType, MeteoZone meteoZone, EnumLanguage lang)
        {
            DateTime dstDate;
            string dstDateName;
            double dateAdd;

            switch (dstDateType)
            {
                // Метео-сутки
                case EnumDateType.MeteoDay:
                    if (meteoZone.UTCHourDayStart < 0)
                    {
                        dateAdd = (iniDateUTC.Hour > Math.Abs(meteoZone.UTCHourDayStart)) ? 1 : 0;
                    }
                    else
                        throw new Exception("Неизвестный для обработки тип метео-зоны: " + meteoZone.Name);

                    dstDate = iniDateUTC.AddDays(dateAdd).Date;
                    dstDateName = GetDatePeriodName(dstDateType, lang);
                    break;
                default:
                    throw new Exception(string.Format("Неизвестный тип временного интервала: {0} = {1} ", dstDateType, (int)dstDateType));
            }
            return new object[] { dstDate, dstDateName };
        }
        /// <summary>
        /// Добавить интервалы указанного типа к дате-времени.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="time"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static DateTime Add(DateTime date, EnumTime time, double count)
        {
            switch (time)
            {
                case EnumTime.Hour: return date.AddHours(count);
                case EnumTime.Day: return date.AddDays(count);
                default:
                    throw new Exception("Неизвестный код временного интервала (unitId) = " + (int)time);
            }
        }
        public static long GetMilliSecondsCount(int unitId)
        {
            switch (unitId)
            {
                case (int)EnumTime.Hour: return (long)60 * 60 * 1000;
                case (int)EnumTime.Day: return GetMilliSecondsCount((int)EnumTime.Hour) * 24;
                case (int)EnumTime.Month: return GetMilliSecondsCount((int)EnumTime.Day) * 30;
                case (int)EnumTime.YearCommon: return GetMilliSecondsCount((int)EnumTime.Month) * 12;
                case (int)EnumTime.DecadeOfYear: return GetMilliSecondsCount((int)EnumTime.Day) * 10;
                case (int)EnumTime.PentadeOfYear: return GetMilliSecondsCount((int)EnumTime.Day) * 5;
                case (int)EnumTime.HydroSeason: return GetMilliSecondsCount((int)EnumTime.Day) * 30 * 3;
                default:
                    throw new Exception("Неизвестный код временного интервала (unit) = " + unitId);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="seasonMonthes"></param>
        /// <returns>int[/*timeNum;year*/] - год начала сезона.</returns>
        public static int[/*timeNum;year*/] GetTimeNumHydroSeason(DateTime date, List<int[/*месяц начала;месяц окончания*/]> seasonMonthes)
        {
            int month = date.Month;
            int year = date.Year;
            int i;
            for (i = 0; i < seasonMonthes.Count; i++)
            {
                if (seasonMonthes[i][0] <= seasonMonthes[i][1])
                {
                    if (month >= seasonMonthes[i][0] && month <= seasonMonthes[i][1]) break;
                }
                else
                {
                    if (month >= seasonMonthes[i][0] && month <= 12) break;
                    if (month >= 1 && month <= seasonMonthes[i][1]) { year--; break; }
                }
            }
            if (i == seasonMonthes.Count)
                return null;
            return new int[] { i + 1, year };
        }

        /// <summary>
        /// Номер временного интервала заданного типа в пределах года и год этого интервала.
        /// SOV@2003.10 - 2015.06
        /// </summary>
        /// <param name="date"></param>
        /// <param name="timeId"></param>
        /// <param name="isDateLOC"></param>
        /// <returns>int[/*timeNum;year*/]</returns>
        public static int[/*timeNum;year*/] GetTimeNumYear(DateTime date, int timeId, List<int[/*месяц начала;месяц окончания*/]> seasonMonthes)
        {
            int i, tn;
            switch (timeId)
            {
                case (int)EnumTime.YearCommon: return new int[] { 1, date.Year };
                case (int)EnumTime.HydroSeason: return GetTimeNumHydroSeason(date, seasonMonthes);
                case (int)EnumTime.Month: return new int[] { date.Month, date.Year };
                case (int)EnumTime.DecadeOfYear:
                    i = (date.Day - 1) / 10 + 1;
                    i = i > 3 ? i - 1 : i;
                    tn = (date.Month - 1) * 3 + i;
                    return new int[] { tn, date.Year };
                case (int)EnumTime.PentadeOfYear:
                    i = (date.Day - 1) / 5 + 1;
                    i = i > 6 ? i - 1 : i;
                    tn = (date.Month - 1) * 6 + i;
                    return new int[] { tn, date.Year };
            }
            throw new Exception("Неизвестный тип временного интервала: " + timeId);
        }

        /// <summary>
        /// Получает дату начала временного периода по заданному типу и дате
        /// </summary>
        /// <param name="date">Дата</param>
        /// <param name="unitsIdTime">Тип временного интервала.</param>
        /// <param name="seasonMonthes"></param>
        /// <param name="isDay820">Признак "местных" суток: c 8 часов утра местного времени.</param>
        /// <returns></returns>
        public static DateTime GetDateSTimeNum(DateTime date, int unitsIdTime, List<int[]> seasonMonthes)
        {
            int[] tn = GetTimeNumYear(date, unitsIdTime, seasonMonthes);
            int[] seasonsMonthStart = (seasonMonthes != null) ? GetSeasonsMonthStart(seasonMonthes) : null;
            return Time.GetDateSTimeNum(tn[1], tn[0], unitsIdTime, seasonsMonthStart);
        }
        /// <summary>
        /// Количество временных интервалов заданного типа в пределах года.
        /// </summary>
        /// <param name="unitsIdTime"></param>
        /// <returns>Количество временных интервалов заданного типа в пределах года.</returns>
        public static int GetTimeNumMax(int unitsIdTime, int[] hydroSeazonMonthStart = null)
        {
            switch (unitsIdTime)
            {
                case (int)EnumTime.Day: return 366;
                case (int)EnumTime.DayHalf: return 366 * 2;
                case (int)EnumTime.DecadeOfYear: return 12 * 3;
                case (int)EnumTime.Month: return 12;
                case (int)EnumTime.PentadeOfYear: return 12 * 6;
                case (int)EnumTime.YearCommon: return 1;
                case (int)EnumTime.HydroSeason: return hydroSeazonMonthStart.Length;
                case (int)EnumTime.Unknown: return -1;
                default: throw new Exception("switch(unitsIdTime) : " + unitsIdTime);
            }
        }
        /// <summary>
        /// 
        /// SOV@2003.10 - 2015.06
        /// </summary>
        /// <param name="year">Год.</param>
        /// <param name="timeNum">Номер временного интервала заданного типа в пределах года.</param>
        /// <param name="timeId">Тип временного интервала.</param>
        /// <returns></returns>
        public static DateTime GetDateSTimeNum(int year, int timeNum, int timeId, int[] hydroSeazonMonthStart = null)
        {
            if (timeNum < 1 || timeNum > GetTimeNumMax(timeId, hydroSeazonMonthStart))
                throw new Exception("timeNum < 1 or timeNum > GetTimeNumMax(unitsIdTime)");

            DateTime ret = new DateTime(year, 1, 1);
            int month, day;

            switch (timeId)
            {
                case (int)EnumTime.HydroSeason:
                    return new DateTime(year, hydroSeazonMonthStart[timeNum - 1], 1);
                case (int)EnumTime.YearCommon:
                    break;
                case 104: // СУТКИ ГОДА
                    if (!DateTime.IsLeapYear(year) && timeNum == 60)
                        throw new Exception("(IsLeapYear(year) = 0 and timeNum = 60)");

                    timeNum = timeNum + ((!DateTime.IsLeapYear(year) && @timeNum > 59) ? -1 : 0);
                    ret = ret.AddDays(timeNum - 1);
                    break;
                case (int)EnumTime.Month://Месяц
                    ret = ret.AddMonths(timeNum - 1);
                    break;
                case 322: // ПОЛУСУТКИ
                    if (!DateTime.IsLeapYear(year) && (timeNum == 119 || timeNum == 120))
                        throw new Exception("(IsLeapYear(year) = 0 && (timeNum ==119||timeNum == 120))");

                    timeNum = timeNum + ((!DateTime.IsLeapYear(year) && @timeNum > 59 * 2) ? -2 : 0);
                    int dayofyear = ((timeNum % 2 != 0) ? timeNum + 1 : timeNum) / 2;
                    ret = ret.AddDays(dayofyear - 1);
                    if (timeNum % 2 == 0) ret = ret.AddHours(12);
                    break;
                case 318: // Декада года с выравниванием на месяц
                    month = (int)((timeNum - 1) / 3) + 1;
                    day = (timeNum - 1) % 3 == 0 ? 1 : timeNum % 3 == 0 ? 21 : 11;
                    ret = new DateTime(year, month, day);
                    break;
                case 321: // Пентада года с выравниванием на месяц
                    month = (int)((timeNum - 1) / 6) + 1;
                    switch (timeNum % 6)
                    {
                        case 1: day = 1; break;
                        case 2: day = 6; break;
                        case 3: day = 11; break;
                        case 4: day = 16; break;
                        case 5: day = 21; break;
                        case 0: day = 26; break;
                        default: throw new Exception("Алгоритмическая ошибка с пентадой.");
                    }
                    ret = new DateTime(year, month, day);
                    break;
                default:
                    throw new Exception("switch(unitsIdTime) : " + timeId);
            }
            return ret;
        }
        private static int[] GetSeasonsMonthStart(List<int[]> seasonMonthes)
        {
            List<int> seasonsMonthStart = new List<int>();
            foreach (var m in seasonMonthes)
            {
                if (m != null && m.Length > 0)
                    seasonsMonthStart.Add(m[0]);
            }
            return seasonsMonthStart.ToArray();
        }
        public static int[] ParseSeasonsMonthesStart(string seasonMonthesStart)
        {
            if (seasonMonthesStart[seasonMonthesStart.Length - 1] == ';')
                seasonMonthesStart = seasonMonthesStart.Substring(0, seasonMonthesStart.Length - 1);
            return Common.StrVia.ToArray<int>(seasonMonthesStart).ToArray();
        }
        public static DateTime GetDateSNextTimeNum(DateTime date, int timeId, List<int[]> seasonMonthes)
        {
            int[] tn = Time.GetTimeNumYear(date, timeId, seasonMonthes);
            int[] seasonsMonthStart = (seasonMonthes != null) ? GetSeasonsMonthStart(seasonMonthes) : null;
            int tnMax = Time.GetTimeNumMax(timeId);
            if (tn[0] > tnMax)
                throw new Exception("tn[0]>tnMax");
            if (tn[0] == tnMax)
            {
                tn[0] = 1;
                tn[1]++;
            }
            else
                tn[0]++;
            return Time.GetDateSTimeNum(tn[1], tn[0], timeId, seasonsMonthStart);
        }


    }
}
