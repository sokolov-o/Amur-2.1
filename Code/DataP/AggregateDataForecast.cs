using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FERHRI.Amur.Data;

namespace FERHRI.Amur.DataP
{
    public class AggregateDataForecast
    {
        static public Dictionary<double/*fcsLag*/, List<DataItem>> Dayly
            (Meta.EnumDataType dataTypeAggr, int catalogId, DateTime dateS, DateTime dateF, int daySShift, int hourS, bool hourSIncluded, int dayFShift, int hourF, bool hourFIncluded)
        {
            Dictionary<double, List<DataItem>> ret = new Dictionary<double, List<DataItem>>();
            
            // READ forecast data for aggregation
            List<DataForecast> dfs = DataForecastRepository.Instance.Select(dateS.AddDays(-1), dateF.AddDays(+1), catalogId, 000, true);

            // LOOP by forecast lags
            foreach (var fcsLag in dfs.Select(x => x.LagFcs).Distinct())
            {
                // CONVERT DataForecast 2 DataItem class
                List<DataItem> dis = new List<DataItem>();
                foreach (var dfss in dfs.Where(x => x.LagFcs == fcsLag))
                {
                    dis.Add(new DataItem() { Id = dfss.Id, Date = dfss.DateFcs, Value = dfss.Value });
                }
                // AGGREGATE dayly
                ret.Add(fcsLag, Aggregate.Dayly(dataTypeAggr, dis, dateS, dateF, daySShift, hourS, hourSIncluded, dayFShift, hourF, hourFIncluded));
            }
            return ret;
        }
    }
}
