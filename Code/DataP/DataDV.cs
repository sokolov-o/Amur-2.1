using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace SOV.Amur.DataP
{
    [DataContract]
    public class DataDV
    {
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public double Value { get; set; }
        /// <summary>
        /// Контекстный параметр значения.
        /// Например, количество значений при агрегации.
        /// </summary>
        [DataMember]
        public object[] Param { get; set; }

        public DataDV() { Value = double.NaN; Date = DateTime.MinValue; }

        static public List<DataDV> ToList(List<Data.DataForecast> df)
        {
            List<DataDV> ret = new List<DataDV>();
            foreach (var item in df)
            {
                ret.Add(new DataDV() { Date = item.DateFcs, Value = item.Value });
            }
            return ret;
        }
        static public List<DataDV> ToList(IEnumerable<Data.DataForecast> df)
        {
            List<DataDV> ret = new List<DataDV>();
            foreach (var item in df)
            {
                ret.Add(new DataDV() { Date = item.DateFcs, Value = item.Value });
            }
            return ret;
        }
        static public List<DataDV> ToList(IEnumerable<Data.DataValue> dvs, bool isDateUTC)
        {
            List<DataDV> ret = new List<DataDV>();
            foreach (var item in dvs)
            {
                ret.Add(new DataDV() { Date = isDateUTC ? item.DateUTC : item.DateLOC, Value = item.Value });
            }
            return ret;
        }

    }
}
