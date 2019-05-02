using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SOV.Amur.Meta;

namespace SOV.Amur
{
    /// <summary>
    /// Сканирование временных интервалов. Пример использования:
    /// 
    ///     DateYTScanner dyts = new DateYTScanner(...)
    ///     do 
    ///     {
    ///         if(dyts.CurDYT == dateYT) { ... }
    ///     } while (dyts.moveNext());
    ///    
    /// </summary>
    public class DateYTScanner
    {

        public DateYT StartDTN { get; set; }
        public DateYT FinishDTN { get; set; }
        public EnumTime TimePeriod { get { return StartDTN.TimePeriod; } }
        List<int> TimeNumOnly { get; set; }

        public int MaxTimeNum { get { return StartDTN.MaxTimeNum; } }
        public DateYT CurDYT;

        public DateYTScanner(EnumTime timePeriod, int yearS, int yearF, int[] timeNumOnly = null)
        {
            if (yearS < 0 || yearF < 0 || yearS > yearF) throw new Exception("(yearS < 0 || yearF < 0 || yearS > yearF)");

            StartDTN = new DateYT(yearS, 1, timePeriod);
            FinishDTN = new DateYT(yearF, Time.GetTimeNumMax((int)timePeriod), timePeriod);
            TimeNumOnly = timeNumOnly == null ? null : new List<int>(timeNumOnly);

            MoveFirst();
        }
        //public DateYTScanner(EnumTime timePeriod, DateTime DateS, DateTime DateF, int[] timeNumOnly = null)
        //{
        //    if (DateS > DateF) throw new Exception("DateS>DateF");

        //    int[] ty = Time.GetTimeNumYear(DateS, (int)timePeriod, null);
        //    StartDTN = new DateYT(ty[1], ty[0], timePeriod);

        //    ty = Time.GetTimeNumYear(DateF, (int)timePeriod, null);
        //    FinishDTN = new DateYT(ty[1], ty[0], timePeriod);

        //    TimeNumOnly = timeNumOnly == null ? null : new List<int>(timeNumOnly);

        //    MoveFirst();
        //}
        public void MoveFirst()
        {
            CurDYT = new DateYT(StartDTN);
            if (!IsCurTimeNumNeed())
                MoveNext();
        }
        /// <summary>
        /// Вперёд двигаться нельзя?
        /// </summary>
        public bool EOF { get { return CurDYT == FinishDTN; } }
        /// <summary>
        /// Назад двигаться нельзя?
        /// </summary>
        public bool BOF { get { return CurDYT == StartDTN; } }

        public bool MoveNext()
        {
            if (!EOF)
            {
                CurDYT = CurDYT.GetNext();
                if (!IsCurTimeNumNeed())
                    MoveNext();
                return true;
            }
            return false;
        }
        /**
         * Is timenum need for scanninng?
         */
        private bool IsCurTimeNumNeed()
        {
            return TimeNumOnly == null || TimeNumOnly.Exists(x => x == CurDYT.TimeNum);
        }

        public int GetTimeNumQ()
        {
            DateYT cur = new DateYT(CurDYT);
            int ret = 0;

            MoveFirst();
            do { ret++; } while (MoveNext());

            CurDYT = new DateYT(cur);

            return ret;
        }
    }
}
