using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Common;

namespace SOV.Amur.Meta
{
    /// <summary>
    /// Фильтр данных для графиков.
    /// </summary>
    public class ChartsFilter
    {
        public DateTimePeriod DateTimePeriod { get; set; }
        public int? SiteGroup { get; set; }

        public override string ToString()
        {
            return "DATETIMEPERIOD=" + DateTimePeriod.ToString('/') + ";SITEGROUP=" + SiteGroup.ToString();
        }

        static public ChartsFilter Parse(string df)
        {
            try
            {
                if (!string.IsNullOrEmpty(df))
                {
                    Dictionary<string, string> dic = StrVia.ToDictionary(df);
                    DateTimePeriod dtp;
                    string ss;

                    if (dic.TryGetValue("DATESF", out ss))
                    {
                        string[] s = dic["DATESF"].Split(',');
                        dtp = new DateTimePeriod(
                            DateTime.Parse(s[0]),
                            DateTime.Parse(s[1]),
                            DateTimePeriod.Type.Period,
                            7
                        );
                    }
                    else
                    {
                        dtp = DateTimePeriod.Parse(dic["DATETIMEPERIOD"], '/');
                    }
                    return new ChartsFilter(
                        dtp, 
                        dic.ContainsKey("SITEGROUP") ? StrVia.ParseInt(dic["SITEGROUP"]) : null
                    );
                }
                return new ChartsFilter();
            }
            catch 
            {
                return new ChartsFilter();
            }
        }

        public ChartsFilter()
        {
            this.DateTimePeriod = new DateTimePeriod();
            this.SiteGroup = null;
        }
        public ChartsFilter(DateTimePeriod dateTimePeriod, int? siteGroup)
        {
            this.DateTimePeriod = dateTimePeriod;
            this.SiteGroup = siteGroup;
        }
    }
}
