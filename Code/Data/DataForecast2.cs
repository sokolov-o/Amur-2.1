using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Amur.Data
{
    public class DataForecast2 : DataForecast
    {
        public double Value1 { get { return Value; } set { Value = value; } }
        public double Value2 { get; set; }

        public DataForecast2
(int catalogId, double lagFcs, DateTime dateFcs, DateTime dateIni, DateTime dateInsert, double value1, double value2)
            : base(catalogId, lagFcs, dateFcs, dateIni, value1, dateInsert)
        {
            Value2 = value2;
        }
    }
}
