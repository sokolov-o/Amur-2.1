using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Amur.DataP
{
    public class DataStat
    {
        public double Avg { get; set; }
        public double Std { get; set; }
        public double Sum { get; set; }
        public double Sum2 { get; set; }

        public double Min { get; set; }
        public double Max { get; set; }
        public List<DateTime> MinDate { get; set; }
        public List<DateTime> MaxDate { get; set; }

        public int Count { get; set; }

        public DataStat()
        {
            Avg = Std = Sum = Sum2 = double.NaN;
            Count = -1;
        }
        public string Splitter = ";";
        public override string ToString()
        {
            return
                Count + Splitter +
                Avg + Splitter +
                Std + Splitter +
                Sum + Splitter +
                Sum2 + Splitter +
                Min + Splitter + string.Concat(MinDate.Select(x => x.ToString("yyyyMMdd HH:mm") + ',')).TrimEnd(new char[] { ',' }) + Splitter +
                Max + Splitter + string.Concat(MaxDate.Select(x => x.ToString("yyyyMMdd HH:mm") + ',')).TrimEnd(new char[] { ',' })
                ;
        }
    }

    public class DataYT : DataStat
    {
        public DateYT DateYT { get; set; }
        public Meta.EnumTime TimeIniData = Meta.EnumTime.Unknown;
        /// <summary>
        /// Group by the time for one catalog record data value list (without double.NaN)
        /// </summary>
        /// <param name="timeDst">Destination time period.</param>
        /// <param name="dvs">One catalog record data value list (without double.NaN).</param>
        /// <returns></returns>
        static public List<DataYT> GroupBy(Meta.EnumTime timeDst, List<Data.DataValueC> dvs)
        {
            // GET TIME SRC 
            List<Meta.Catalog> catalog = dvs.Select(x => x.Catalog).Distinct().ToList();
            if (catalog.Count != 1) throw new Exception("Catalog count = " + catalog.Count);
            Meta.Variable variable = Meta.DataManager.GetInstance().VariableRepository.Select(catalog[0].VariableId);
            Meta.EnumTime timeSrc = (Meta.EnumTime)variable.TimeId;

            var query = dvs.GroupBy(
                pet => (new DateYT(pet.DataValue.DateUTC, timeDst)).ToString(),
                pet => pet.DataValue,
                (dateYT, values) => new
                {
                    DataYT = new DataYT()
                    {
                        DateYT = DateYT.Parse(dateYT),
                        Count = values.Count(),
                        Avg = values.Select(x => x.Value).Average(),
                        Sum = values.Select(x => x.Value).Sum(),
                        Sum2 = values.Select(x => x.Value).Sum(x => x * x),
                        Min = values.Select(x => x.Value).Min(),
                        Max = values.Select(x => x.Value).Max(),
                        MinDate = values
                            .ToList()
                            .FindAll(x => x.Value == values.Select(y => y.Value).Min())
                            .Select(x => x.DateUTC)
                            .ToList(),
                        MaxDate = values
                            .ToList()
                            .FindAll(x => x.Value == values.Select(y => y.Value).Max())
                            .Select(x => x.DateUTC)
                            .ToList(),
                        TimeIniData = timeSrc
                    }
                });

            List<DataYT> ret = query.Select(x => x.DataYT).ToList();
            ret.ForEach(x => x.Std = x.Count < 2 ? double.NaN : SOV.Common.MathSupport.Stdev(x.Sum, x.Sum2, x.Count));
            return ret;
        }
        new public string Splitter = ";";
        public override string ToString()
        {
            base.Splitter = DateYT.Splitter = Splitter;

            return DateYT.ToString() + Splitter +
                base.ToString() + Splitter +
                TimeIniData
            ;
        }
        public static string ToString(List<DataYT> data, string div = ";")
        {
            string ret = "year;time_num;time_name;count;avg;std;sum;sum2;min;min_dates;max;max_dates;time_name_ini";
            for (int i = 0; i < data.Count; i++)
            {
                ret += "\n" + data[i].ToString();
            }

            return ret.Replace(";", div);
        }
    }
}
