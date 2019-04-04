using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Common
{
    public class DateTimePeriod
    {
        public enum Type
        {
            Period = 0, YearMonth = 1, DaysBeforeCurDate = 2, Day = 3, FstDecade = 4, SndDecade = 5, ThdDecade = 6, Years = 7
        }

        public DateTimePeriod()
        {
            PeriodType = Type.Period;
            DaysBeforeDateNow = 7;
            SetDateSF(DateTime.Today, DateTime.Today);
        }
        public DateTimePeriod(Type periodType, int daysBeforeDateNow)
        {
            PeriodType = periodType;
            DaysBeforeDateNow = daysBeforeDateNow;
            SetDateSF(null, null);
        }
        public DateTimePeriod(DateTime? dateS, DateTime? dateF, Type periodType, int daysBeforeDateNow)
        {
            PeriodType = periodType;
            DaysBeforeDateNow = daysBeforeDateNow;
            SetDateSF(dateS, dateF);
        }
        public int DaysBeforeDateNow { get; set; }

        public void SetDateSF(DateTime? dateS, DateTime? dateF)
        {
            if (dateS.HasValue && dateF.HasValue && dateS > dateF && PeriodType!= Type.YearMonth)
                throw new Exception("Некорректный временной период: " + dateS + " - " + dateF);

            _dateS = dateS;
            _dateF = dateF;
        }
        DateTime? _dateS;
        public DateTime? DateS
        {
            get
            {
                switch (PeriodType)
                {
                    case DateTimePeriod.Type.Period: 
                        return _dateS;
                    case DateTimePeriod.Type.YearMonth: 
                        return new DateTime(((DateTime)_dateS).Year, ((DateTime)_dateF).Month, 1);
                    case DateTimePeriod.Type.DaysBeforeCurDate: 
                        return DateTime.Now.AddDays(-DaysBeforeDateNow);
                    case DateTimePeriod.Type.Day: 
                        return new DateTime(((DateTime)_dateS).Year, ((DateTime)_dateS).Month, ((DateTime)_dateS).Day);
                    case DateTimePeriod.Type.FstDecade: 
                        return new DateTime(((DateTime)_dateS).Year, ((DateTime)_dateS).Month, 1);
                    case DateTimePeriod.Type.SndDecade: 
                        return new DateTime(((DateTime)_dateS).Year, ((DateTime)_dateS).Month, 11);
                    case DateTimePeriod.Type.ThdDecade: 
                        return new DateTime(((DateTime)_dateS).Year, ((DateTime)_dateS).Month, 21);
                    default:
                        throw new Exception("Неизвестный тип временного периода.");
                }
            }
        }
        DateTime? _dateF;
        public DateTime? DateF
        {
            get
            {
                switch (PeriodType)
                {
                    case DateTimePeriod.Type.Period:
                        return _dateF;
                    case DateTimePeriod.Type.YearMonth:
                        return ((DateTime)DateS).AddMonths(1).AddSeconds(-1);
                    case DateTimePeriod.Type.DaysBeforeCurDate:
                        return DateTime.Now;
                    case DateTimePeriod.Type.Day:
                        return ((DateTime)DateS).AddDays(1).AddSeconds(-1);
                    case DateTimePeriod.Type.FstDecade:
                        return ((DateTime)DateS).AddDays(10).AddSeconds(-1);
                    case DateTimePeriod.Type.SndDecade:
                        return ((DateTime)DateS).AddDays(10).AddSeconds(-1);
                    case DateTimePeriod.Type.ThdDecade:
                        return new DateTime(((DateTime)DateS).Year, ((DateTime)DateS).Month, 1).AddMonths(1).AddSeconds(-1);;
                    default:
                        throw new Exception("Неизвестный тип временного периода.");
                }
            }
        }

        public Type PeriodType { get; set; }

        public char Splitter = '/';

        public override string ToString()
        {
            return ToString(Splitter);
        }
        public string ToString(char splitter)
        {
            return
                (DateS.HasValue ? ((DateTime)DateS).ToString() : "NULL") + splitter
                + (DateF.HasValue ? ((DateTime)DateF).ToString() : "NULL") + splitter
                + (int)PeriodType + splitter
                + DaysBeforeDateNow
            ;
        }
        public string ToStringRus(string format = null)
        {
            switch (PeriodType)
            {
                case DateTimePeriod.Type.Period:
                    return (DateS.HasValue ? ((DateTime)DateS).ToString(format) : "NULL") + " - "
                        + (DateF.HasValue ? ((DateTime)DateF).ToString(format) : "NULL");
                case DateTimePeriod.Type.YearMonth: return "Год.месяц " + ((DateTime)DateS).Year + "." + ((DateTime)DateS).Month;
                case DateTimePeriod.Type.DaysBeforeCurDate: return DaysBeforeDateNow + " сут. от текущей даты.";
                default:
                    return ToString();
            }
        }
        static public DateTimePeriod Parse(string dump, char splitter = '/')
        {
            try
            {
                string[] s = dump.Split(splitter);
                return new DateTimePeriod(
                    s[0] == "NULL" ? null : (DateTime?)DateTime.Parse(s[0]),
                    s[1] == "NULL" ? null : (DateTime?)DateTime.Parse(s[1]),
                    (Type)int.Parse(s[2]), int.Parse(s[3])
                    );

            }
            catch
            {
                return null;
            }
        }
    }
}
