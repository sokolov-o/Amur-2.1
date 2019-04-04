using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Amur.DataP
{
    public class DataGrpSVYM
    {
        public int SiteId { get; set; }
        public int VariableId { get; set; }

        public int Year { get; set; }
        public int Month { get; set; }
        public int Count { get; set; }

        public DateTime MinDateTimeUTC { get; set; }
        public DateTime MaxDateTimeUTC { get; set; }

        public Double AvgValue { get; set; }
        public Double SumValue { get; set; }
        public Double MinValue { get; set; }
        public Double MaxValue { get; set; }

        public override string ToString()
        {
            return "Код пункта = " + SiteId
                + "\nКод переменной = " + VariableId
                + "\nГод = " + Year
                + "\nМесяц = " + Month
                + "\nКол. = " + Count
                + "\nМин. дата = " + MinDateTimeUTC
                + "\nМакс. дата = " + MaxDateTimeUTC
                +"\n------"
                + "\nСреднее = " + AvgValue
                + "\nСумма = " + SumValue
                + "\nМин = " + MinValue
                + "\nМакс = " + MaxValue
                ;
        }
    }
}
