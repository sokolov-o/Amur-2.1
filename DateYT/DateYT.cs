using System;
using System.Collections.Generic;
using SOV.Amur.Meta;

namespace SOV.Amur
{
    public class DateYT : IComparable<DateYT>
    {
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        /// <summary>
        /// Год
        /// </summary>
        public int Year { get; set; }
        int _TimeNum;
        /// <summary>
        /// Номер временного интервала в пределах года
        /// </summary>
        public int TimeNum
        {
            get { return _TimeNum; }
            set
            {
                if (value > MaxTimeNum)
                    throw new Exception("(value > MaxTimeNum)");
                _TimeNum = value;
            }
        }
        EnumTime _TimePeriod;
        /// <summary>
        /// Тип временного интервала в пределах года
        /// </summary>
        public EnumTime TimePeriod
        {
            get { return _TimePeriod; }
            set
            {
                _TimePeriod = value;
                MaxTimeNum = Time.GetTimeNumMax((int)value);
            }
        }
        int _MaxTimeNum;
        /// <summary>
        /// Макс. кол. временных интервалов в пределах года
        /// </summary>
        public int MaxTimeNum
        {
            get { return _MaxTimeNum; }
            private set
            {
                if (value < 1)
                    throw new Exception("(MaxTimeNum < 1)");
                _MaxTimeNum = value;
            }
        }
        public DateYT(int year, int timeNum, EnumTime timePeriod)
        {
            Initialize(year, timeNum, timePeriod);
        }
        public DateYT()
        {
            Initialize(-1, -1, EnumTime.Unknown);
        }
        public DateYT(DateTime date, EnumTime timePeriod)
        {
            int[] ty = Time.GetTimeNumYear(date, (int)timePeriod, null);
            Initialize(ty[1], ty[0], timePeriod);
        }
        public void Initialize(int year, int timeNum, EnumTime timePeriod)
        {
            TimePeriod = timePeriod;
            Year = year;
            TimeNum = timeNum;
        }
        public DateYT(DateYT dytn)
        {
            Initialize(dytn.Year, dytn.TimeNum, dytn.TimePeriod);
        }
        /// <summary>
        /// Получить следующий временной YT-период.
        /// </summary>
        /// <returns></returns>
        public DateYT GetNext()
        {
            DateYT ret = new DateYT(Year, TimeNum, TimePeriod);

            if (ret.TimeNum + 1 > ret.MaxTimeNum)
            {
                ret.TimeNum = 1;
                ret.Year++;
            }
            else
            {
                ret.TimeNum++;
            }

            return ret;
        }
        public string Splitter = ";";
        public override string ToString()
        {
            return Year + Splitter + TimeNum + Splitter + TimePeriod;
        }
        static public DateYT Parse(string dateYT, char splitter = ';')
        {
            string[] s = dateYT.Split(splitter);
            return new DateYT(int.Parse(s[0]), int.Parse(s[1]), Enums.ParseTime(s[2]));
        }
        /// <summary>
        /// Начало периода.
        /// </summary>
        public DateTime DateS
        {
            get
            {
                return Time.GetDateSTimeNum(Year, TimeNum, (int)TimePeriod);
            }
        }
        /// <summary>
        /// Начало следующего периода.
        /// </summary>
        public DateTime DateSNext
        {
            get
            {
                return (new DateYT(this)).GetNext().DateS;
            }
        }
        /// <summary>
        ///  0 - равны
        /// -1 - this раньше 
        /// +1 - this позже
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(DateYT other)
        {
            if (other == null || (int)TimePeriod != (int)other.TimePeriod) return 1;

            return
                Year == other.Year && TimeNum == other.TimeNum ? 0 :
                Year < other.Year || (Year == other.Year && TimeNum < other.TimeNum) ? -1 :
                +1;
        }
        public static bool operator >(DateYT a, DateYT b)
        {
            return a.CompareTo(b) == 1;
        }
        public static bool operator <(DateYT a, DateYT b)
        {
            return a.CompareTo(b) == -1;
        }
        public static bool operator >=(DateYT a, DateYT b)
        {
            return a.CompareTo(b) >= 0;
        }
        public static bool operator <=(DateYT a, DateYT b)
        {
            return a.CompareTo(b) <= 0;
        }
        public double GetTotalSeconds()
        {
            return (DateSNext - DateS).TotalSeconds;
        }

        //public override int Compare(DateYT x, DateYT y)
        //{
        //    return
        //        x.Year == y.Year && x.TimeNum == y.TimeNum && x.TimePeriod == y.TimePeriod ? 0 :
        //        x.Year < y.Year || x.TimeNum < y.TimeNum ? -1 :
        //        +1;
        //}
    }
}
