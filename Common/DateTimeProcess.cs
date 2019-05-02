using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Viaware.Sakura.Meta;

namespace SOV.Common
{
    public class DateTimeProcess
    {
        //       static public string DATE_FORMAT_DMYHM = "dd.MM.yyyy HH:mm";
        //       static public string DATE_FORMAT_YMDHMS = "yyyyMMdd HH:mm:ss";

        static public string[] MonthNameRus = new string[] { "январь", "февраль", "март", "апрель", "май", "июнь", "июль", "август", "сентябрь", "октябрь", "ноябрь", "декабрь", };

        //       public static int GetYear(DateTime date, Enums.TimePeriod timePeriod)
        //       {
        //           switch (timePeriod)
        //           {
        //               case Enums.TimePeriod.SEASON:
        //               case Enums.TimePeriod.PERIOD_D_M:
        //               case Enums.TimePeriod.SEASON_ES2:
        //               case Enums.TimePeriod.SEASONJFMR:
        //                   if (date.Month == 12)
        //                   {
        //                       return date.Year + 1;
        //                   } break;
        //               case Enums.TimePeriod.PERIOD_A_L:
        //                   if (date.Month >= 8)
        //                       return date.Year + 1;
        //                   break;
        //               case Enums.TimePeriod.ZIMA:
        //               case Enums.TimePeriod.GYDRO_YEAR:
        //               case Enums.TimePeriod.GYDRO_SEASON:
        //               case Enums.TimePeriod.GYDRO_ICE:
        //               case Enums.TimePeriod.GYDRO_NO_ICE:
        //               case Enums.TimePeriod.PERIOD_N_A:
        //                   if (date.Month == 12 || date.Month == 11)
        //                   {
        //                       return date.Year + 1;
        //                   } break;
        //           }
        //           return date.Year;
        //       }
        //       /// <summary>
        //       /// В году.
        //       /// </summary>
        //       /// <param name="timePeriod"></param>
        //       /// <returns></returns>
        //       public static int GetTimeNumMax(Enums.TimePeriod timePeriod)
        //       {
        //           switch (timePeriod)
        //           {
        //               case Enums.TimePeriod.MINUTE: return 60;
        //               case Enums.TimePeriod.MONTH: return 12;
        //               case Enums.TimePeriod.HOUR: return 24;
        //               case Enums.TimePeriod.PENTADE: return 72;
        //               case Enums.TimePeriod.PENTADE1:
        //               case Enums.TimePeriod.DECADE1:
        //               case Enums.TimePeriod.DAY366:
        //                   return 366;
        //               case Enums.TimePeriod.DECADE: return 36;
        //               case Enums.TimePeriod.SEASON: return 4;
        //               case Enums.TimePeriod.SEASON_ES2: return 6;
        //               case Enums.TimePeriod.SEASON_JFM: return 4;

        //               case Enums.TimePeriod.PERIOD_D_M:
        //               case Enums.TimePeriod.PERIOD_M_J:
        //               case Enums.TimePeriod.YEAR:
        //               case Enums.TimePeriod.PERIOD_N_A:
        //               case Enums.TimePeriod.PERIOD_J_S:
        //               case Enums.TimePeriod.PERIOD_A_L:
        //               case Enums.TimePeriod.ZIMA:
        //               case Enums.TimePeriod.PER_AP_OK:
        //                   return 1;

        //               case Enums.TimePeriod.PER_J_MR: return 2;
        //               case Enums.TimePeriod.YEAR_HALF: return 2;
        //           }
        //           return -1;
        //       }
        //       public static int GetTimeNum(DateTime date, Enums.TimePeriod timePeriod)
        //       {
        //           switch (timePeriod)
        //           {

        //               case Enums.TimePeriod.YEAR:
        //               case Enums.TimePeriod.PERIOD_A_L:
        //                   return 1;
        //               case Enums.TimePeriod.PENTADE1:
        //               case Enums.TimePeriod.DECADE1:
        //               case Enums.TimePeriod.DAY366:
        //                   return date.DayOfYear + ((date.Month > 2 && !DateTime.IsLeapYear(date.Year)) ? 1 : 0);
        //               case Enums.TimePeriod.PENTADE:
        //                   int pent = (date.Day - 1) / 5;
        //                   return (date.Month - 1) * 6 + ((pent > 5) ? 5 : pent) + 1;
        //               case Enums.TimePeriod.DECADE:
        //                   int day = date.Day;
        //                   return (date.Month - 1) * 3 + ((day <= 10) ? 1 : (day <= 20) ? 2 : 3);
        //               case Enums.TimePeriod.MONTH:
        //                   return date.Month;
        //               case Enums.TimePeriod.SEASON_ES2:
        //                   switch (date.Month)
        //                   {
        //                       case 12:
        //                       case 1: return 1;
        //                       case 2:
        //                       case 3: return 2;
        //                       case 4:
        //                       case 5: return 3;
        //                       case 6:
        //                       case 7: return 4;
        //                       case 8:
        //                       case 9: return 5;
        //                       case 10:
        //                       case 11: return 6;
        //                   }
        //                   break;
        //               case Enums.TimePeriod.SEASON:
        //                   switch (date.Month)
        //                   {
        //                       case 12:
        //                       case 1:
        //                       case 2: return 1;
        //                       case 3:
        //                       case 4:
        //                       case 5: return 2;
        //                       case 6:
        //                       case 7:
        //                       case 8: return 3;
        //                       case 9:
        //                       case 10:
        //                       case 11: return 4;
        //                   }
        //                   break;
        //               case Enums.TimePeriod.ZIMA:
        //                   if (date.Month <= 3 || date.Month >= 11)
        //                       return 1;
        //                   break;
        //               case Enums.TimePeriod.PER_J_MR:
        //                   return (date.Month >= 1 && date.Month <= 3) ? 1 : 2;
        //               case Enums.TimePeriod.PER_AP_OK:
        //                   if (date.Month >= 4 && date.Month <= 10)
        //                       return 1;
        //                   break;

        //           }
        //           return -1;
        //       }
        //       /// <summary>
        //       ///Сдвиг текущей даты.
        //       ///</summary>
        //       ///<param name="ttype">Тип временного интервала сдвижки.</param>
        //       ///<param name="time_in">Номера временных интервалов типа ttime_add, включаемых в выборку при сдвигах move_next.</param>
        //       ///<param name="addQ">Количество временных интервалов типа ttime_add, добавляемых при сдвигах move_next.</param>
        //       static public DateTime Add(DateTime d, Enums.TimePeriod timePeriod, int addQ)
        //       {
        //           switch (timePeriod)
        //           {
        //               case Enums.TimePeriod.SECOND: return d.AddSeconds(addQ);
        //               case Enums.TimePeriod.HOUR: return d.AddHours(addQ);
        //               case Enums.TimePeriod.HOUR4: return d.AddHours(6 * addQ);
        //               case Enums.TimePeriod.HOUR8: return d.AddHours(3 * addQ);
        //               case Enums.TimePeriod.HOUR2: return d.AddHours(12 * addQ);
        //               case Enums.TimePeriod.DAY: return d.AddDays(addQ);
        //               case Enums.TimePeriod.MONTH: return d.AddMonths(addQ);
        //               case Enums.TimePeriod.YEAR: return d.AddYears(addQ);
        //               case Enums.TimePeriod.PENTADE:
        //               case Enums.TimePeriod.DECADE:
        //               case Enums.TimePeriod.DAY366:
        //               case Enums.TimePeriod.SEASON_ES2:
        //               case Enums.TimePeriod.SEASON:
        //               case Enums.TimePeriod.PER_J_MR:
        //                   int year = GetYear(d, timePeriod);
        //                   int timeNumMax = GetTimeNumMax(timePeriod);
        //                   if (timeNumMax == -1)
        //                       throw new Exception("Unknown timeNumMax for timeId=" + timePeriod);
        //                   int timeNum = GetTimeNum(d, timePeriod);
        //                   if (timeNum == -1)
        //                       throw new Exception("Unknown timeNum for timeId=" + timePeriod + " and date = " + d);

        //                   int yearCountAdd = (int)Math.Truncate((double)addQ / timeNumMax);
        //                   year += yearCountAdd;
        //                   addQ = addQ % timeNumMax;
        //                   timeNum += addQ;
        //                   if (timeNum < 1)
        //                   {
        //                       year--;
        //                       timeNum += timeNumMax;
        //                   }
        //                   else if (timeNum > timeNumMax)
        //                   {
        //                       year++;
        //                       timeNum -= timeNumMax;
        //                   }
        //                   return GetDateTimeStart(year, timeNum, timePeriod);

        //               default: throw new SystemException("UNKNOWN TimePeriod");
        //           }
        //       }
        //       /**
        //* Получить дату начала временного периода указанного года.
        //* @param year год, для которого требуется дата начала периода
        //* @param timeId тип временного периода
        //* @param timeNum номер временного периода
        //* @return Дата начала временного периода указанного года.
        //*/
        //       public static DateTime GetDateTimeStart(int year, int timeNum, Enums.TimePeriod timeId)
        //       {
        //           if (timeNum < 1 || timeNum > GetTimeNumMax(timeId)) throw new Exception("WRONG input timeNum: " + timeNum + ", " + timeId);

        //           switch (timeId)
        //           {
        //               case Enums.TimePeriod.DAYOFYEAR:
        //                   return (new DateTime(year, 1, 1)).AddDays(timeNum - 1);
        //               case Enums.TimePeriod.PENTADE1:
        //               case Enums.TimePeriod.DECADE1:
        //               case Enums.TimePeriod.DAY366:
        //                   if (!DateTime.IsLeapYear(year) && timeNum == 60)
        //                       throw new Exception("WRONG input timeNum = 60 and not LeapYear= " + year + ", " + timeId);
        //                   else
        //                       return (new DateTime(year, 1, 1)).AddDays(timeNum - ((timeNum > 60 && !DateTime.IsLeapYear(year)) ? 2 : 1));
        //               case Enums.TimePeriod.PENTADE:
        //                   int month = timeNum / 6;
        //                   if (timeNum % 6 != 0)
        //                   {
        //                       month++;
        //                   }
        //                   int day = 26;
        //                   switch (timeNum % 6)
        //                   {
        //                       case 1:
        //                           day = 1;
        //                           break;
        //                       case 2:
        //                           day = 6;
        //                           break;
        //                       case 3:
        //                           day = 11;
        //                           break;
        //                       case 4:
        //                           day = 16;
        //                           break;
        //                       case 5:
        //                           day = 21;
        //                           break;
        //                   }
        //                   return new DateTime(year, month, day, 0, 0, 0, 0);

        //               case Enums.TimePeriod.DECADE:
        //                   int rem, rem1;
        //                   Math.DivRem(timeNum, 3, out  rem);
        //                   Math.DivRem(timeNum - 1, 3, out  rem1);
        //                   return new DateTime(
        //                       year,
        //                       (int)(timeNum / 3) + ((rem == 0) ? 0 : 1),
        //                       (rem1 == 0) ? 1 : (rem == 0) ? 21 : 11,
        //                       0, 0, 0, 0);
        //               case Enums.TimePeriod.MONTH:
        //                   return new DateTime(year, timeNum, 1, 0, 0, 0, 0);
        //               case Enums.TimePeriod.SEASON_ES2:
        //                   if (timeNum == 1)
        //                   {
        //                       return new DateTime(year - 1, 12, 1, 0, 0, 0, 0);
        //                   }
        //                   else
        //                   {
        //                       return new DateTime(year, (timeNum - 1) * 2, 1, 0, 0, 0, 0);
        //                   }
        //               case Enums.TimePeriod.PERIOD_N_A:
        //                   return new DateTime(year, 4, 1, 0, 0, 0, 0);
        //               case Enums.TimePeriod.SEASON:
        //                   if (timeNum == 1)
        //                   {
        //                       return new DateTime(year - 1, 12, 1, 0, 0, 0, 0);
        //                   }
        //                   else
        //                   {
        //                       return new DateTime(year, (timeNum - 1) * 3, 1, 0, 0, 0, 0);
        //                   }
        //               case Enums.TimePeriod.YEAR:
        //                   return new DateTime(year, 1, 1);
        //               case Enums.TimePeriod.ZIMA:
        //                   return new DateTime(year - 1, 11, 1, 0, 0, 0, 0);
        //               case Enums.TimePeriod.PER_AP_OK:
        //                   return new DateTime(year, 4, 1, 0, 0, 0, 0);
        //               case Enums.TimePeriod.PERIOD_A_L:
        //                   return new DateTime(year - 1, 8, 1, 0, 0, 0, 0);
        //               case Enums.TimePeriod.PER_J_MR:
        //                   if (timeNum == 1)
        //                       return new DateTime(year, 1, 1, 0, 0, 0, 0);
        //                   else //timenum=2
        //                       return new DateTime(year, 4, 1, 0, 0, 0, 0);

        //               default:
        //                   throw new Exception("UNKNOWN timeId=" + timeId);
        //           }
        //       }

        //       /// <summary>
        //       /// Даты начала и окончания декады месяца. Окончание на 1 сек раньше начала следующей декады.
        //       /// </summary>
        //       /// <param name="year">Год.</param>
        //       /// <param name="month">Месяц.</param>
        //       /// <param name="monthDecade">Номер декады в месяце.</param>
        //       /// <returns></returns>

        static public DateTime[] GetMonthDecadeDateSF(int year, int month, int monthDecade)
        {
            DateTime[] dateSF = GetMonthDateSF(year, month);

            if (monthDecade < 3)
                dateSF[1] = dateSF[0].AddDays((int)monthDecade * 10).AddSeconds(-1);
            dateSF[0] = dateSF[0].AddDays((int)(monthDecade - 1) * 10);

            return dateSF;
        }

        /// <summary>
        /// Даты начала и окончания месяца. Окончание на 1 сек раньше начала следующего месяца.
        /// </summary>
        /// <param name="year">Год.</param>
        /// <param name="month">Месяц.</param>
        /// <returns></returns>
        static public DateTime[] GetMonthDateSF(int year, int month)
        {
            DateTime dateS = new DateTime(year, month, 1), dateF = dateS.AddMonths(1).AddSeconds(-1); ;
            return new DateTime[] { dateS, dateF };
        }
        //       /// <summary>
        //       /// 
        //       /// </summary>
        //       /// <param name="date"></param>
        //       /// <param name="seasonMonthes"></param>
        //       /// <returns>int[/*timeNum;year*/] - год начала сезона.</returns>
        //       public static int[/*timeNum;year*/] GetTimeNumHydroSeason(DateTime date, List<int[/*месяц начала;месяц окончания*/]> seasonMonthes)
        //       {
        //           int month = date.Month;
        //           int year = date.Year;
        //           int i;
        //           for (i = 0; i < seasonMonthes.Count; i++)
        //           {
        //               if (seasonMonthes[i][0] <= seasonMonthes[i][1])
        //               {
        //                   if (month >= seasonMonthes[i][0] && month <= seasonMonthes[i][1]) break;
        //               }
        //               else
        //               {
        //                   if (month >= seasonMonthes[i][0] && month <= 12) break;
        //                   if (month >= 1 && month <= seasonMonthes[i][1]) { year--; break; }
        //               }
        //           }
        //           if (i == seasonMonthes.Count)
        //               return null;
        //           return new int[] { i + 1, year };
        //       }

        /// <summary>
        /// Получить номер декады года по номеру декады в месяце.
        /// </summary>
        /// <param name="month">Месяц.</param>
        /// <param name="monthDecade">Номер декады в месяце.</param>
        /// <returns></returns>
        static public int GetDecadeYearByDecadeMonth(int month, int monthDecade)
        {
            return (month - 1) * 3 + monthDecade;
        }

        //       /// <summary>
        //       /// Список дат -> в строку. Выделяются непрерывные суточные периоды.
        //       /// Даты д.б. отсортированы по возрастанию.
        //       /// </summary>
        public static string ToStringDayly(List<DateTime> dateList, char div1 = '-', char div2 = ';')
        {
            if (dateList.Count == 0)
                return null;

            DateTime datePrev = dateList[0].Date;
            string ret = datePrev.Day.ToString();

            for (int i = 1; i < dateList.Count; i++)
            {
                DateTime dateCur = dateList[i].Date;
                if (datePrev == dateCur || datePrev.AddDays(1) == dateCur)
                {
                    if (ret[ret.Length - 1] != div1) ret += div1;
                }
                else
                {
                    ret += ((ret[ret.Length - 1] == div1) ? datePrev.Day.ToString() : "") + div2 + dateCur.Day;
                }
                datePrev = dateCur;
            }
            ret += (datePrev == dateList[0].Date) ? "" : ((ret[ret.Length - 1] == div1) ? "" : div2.ToString()) + datePrev.Day;
            return ret;
        }
        public static DateTime Parse(string sdate, string format)
        {
            DateTime ret = new DateTime();
            if (!TryParse(sdate, format, out ret))
                throw new Exception("Не распознана дата: " + sdate + " формат " + format);
            return ret;
        }
        static public bool TryParse(string sdate, string format, out DateTime date)
        {
            date = DateTime.MinValue;
            int i;
            string hh;
            try
            {
                string[] s, ss;

                switch (format)
                {
                    case "yyyyMMddHH":
                        date = new DateTime(
                            int.Parse(sdate.Substring(0, 4)),
                            int.Parse(sdate.Substring(4, 2)),
                            int.Parse(sdate.Substring(6, 2)),
                            int.Parse(sdate.Substring(8, 2)),
                            0, 0);
                        break;
                    case "yyyyMMdd HH:mm:ss":
                    case "yyyyMMdd HH:mm":
                    case "yyyyMMdd":
                        s = sdate.Split(' ');
                        date = new DateTime(
                            int.Parse(s[0].Substring(0, 4)),
                            int.Parse(s[0].Substring(4, 2)),
                            int.Parse(s[0].Substring(6, 2)));
                        if (s.Length == 2)
                        {
                            s = s[1].Split(':');
                            date = date.AddHours(int.Parse(s[0]));
                            date = date.AddMinutes(int.Parse(s[1]));
                            if (s.Length == 3)
                                date = date.AddSeconds(int.Parse(s[2]));
                        }
                        break;
                    case "dd.MM.yyyy HH:mm":
                    case "dd.MM.yyyy":
                        s = sdate.Split(' ');
                        ss = s[0].Split('.');
                        date = new DateTime(
                            int.Parse(ss[2]),
                            int.Parse(ss[1]),
                            int.Parse(ss[0]));
                        if (s.Length == 2)
                        {
                            s = s[1].Split(':');
                            date = date.AddHours(int.Parse(s[0]));
                            date = date.AddMinutes(int.Parse(s[1]));
                        }
                        break;
                    case "HH XXX dd.MM.yyyy":
                        i = sdate.IndexOf(' ');
                        hh = sdate.Substring(0, i);
                        i = sdate.IndexOf(' ', i + 1);

                        ss = sdate.Substring(i, sdate.Length - i).Split('.');
                        date = new DateTime(
                            int.Parse(ss[2]),
                            int.Parse(ss[1]),
                            int.Parse(ss[0]));
                        date = date.AddHours(int.Parse(hh));
                        break;
                    default:
                        return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Порядковый номер декады
        /// </summary>
        public static short GetDecadeNumber(DateTimePeriod period)
        {
            DateTime dateS = period.DateS.HasValue ? period.DateS.Value : new DateTime();
            return period.PeriodType == DateTimePeriod.Type.Day
                ? (short)1
                : (short)(((dateS.Month - 1) * 3) + period.PeriodType - DateTimePeriod.Type.FstDecade + 1);
        }
    }
}
