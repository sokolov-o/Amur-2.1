using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Common;
using System.Runtime.Serialization;

namespace SOV.Amur.Meta
{
    [DataContract]
    public class MethodForecast
    {
        [DataMember]
        public Method Method { get; set; }
        [DataMember]
        public double[] LeadTimes { get; set; }
        [DataMember]
        public int LeadTimeUnitId { get; set; }
        /// <summary>
        /// Часы (UTC) суток в которые выпускается прогноз.
        /// </summary>
        [DataMember]
        public double[] DateIniHoursUTC { get; set; }
        /// <summary>
        /// Атрибуты метода
        /// </summary>
        [DataMember]
        public Dictionary<string, string> Attr { get; set; }

        public int? GetAttrInt(string attrName)
        {
            string str = null;
            if (Attr.TryGetValue(attrName.ToUpper(), out str))
                return int.Parse(str);
            return null;
        }

        public MethodForecast() { }
        public MethodForecast(Method method, string leadTimes, int leadTimeUnitId, string dateIniHoursUTC = null, Dictionary<string, string> attr = null)
        {
            Method = method;
            LeadTimes = string.IsNullOrEmpty(leadTimes) ? null : Common.StrVia.ToArrayDouble(leadTimes);
            LeadTimeUnitId = leadTimeUnitId;
            DateIniHoursUTC = string.IsNullOrEmpty(dateIniHoursUTC) ? null : Common.StrVia.ToArrayDouble(dateIniHoursUTC);
            Attr = attr;
        }
        override public string ToString()
        {
            return Method.ToString();
        }
        /// <summary>
        /// Кол. прогнозов в сутки. Определяется по заблаговременностям первых суток прогноза.
        /// </summary>
        /// <returns>-1, если определить невозможно: если алгоритму неизвестен тип временного интервала или не заданы заблаговременности.</returns>
        public int MaxPerDayCount
        {
            get
            {
                switch (LeadTimeUnitId)
                {
                    case (int)EnumTime.Hour: return LeadTimes == null ? -1 : LeadTimes.Where(x => x >= 0 && x < 24).Count();
                    default: return -1;
                }
            }
        }
    }
}
