using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Common;

namespace SOV.Amur.Meta
{
    /// <summary>
    /// Фильтр отбора данных из источника.
    /// </summary>
    public class DataFilter
    {
        public DateTimePeriod DateTimePeriod { get; set; }
        //DateTime _dateS, _dateF;
        //public DateTime DateS
        //{
        //    get { return _dateS; }
        //    set { _dateS = DateTime.FromBinary(value.ToBinary()); }
        //}
        //public DateTime DateF
        //{
        //    get { return _dateF; }
        //    set { _dateF = DateTime.FromBinary(value.ToBinary()); }
        //}
        public byte? FlagAQC { get; set; }
        public bool IsActualValueOnly { get; set; }
        public bool IsSelectDeleted { get; set; }
        public bool IsDateLOC { get; set; }
        public int DateTypeId { get; set; }
        /// <summary>
        /// Выбирать/отображать данные ссылочного пункта?
        /// </summary>
        public bool IsRefSiteData { get; set; }

        public CatalogFilter CatalogFilter { get; set; }
        ///// <summary>
        ///// Альтернативное использование фильтра для записей каталога:
        ///// Либо CatalogFilter, либо Catalogs должны быть null.
        ///// Если это не так, могут быть проблемы.
        ///// </summary>
        //public List<Catalog> Catalogs { get; set; }

        public override string ToString()
        {
            return
                  "DATETIMEPERIOD=" + DateTimePeriod.ToString('/')
                + ";FLAGAQC=" + ((FlagAQC.HasValue) ? FlagAQC.ToString() : "")
                + ";ISONEVALUE=" + IsActualValueOnly
                + ";ISSELECTDELETED=" + IsSelectDeleted
                + ";ISREFSITEDATA=" + IsRefSiteData
                + ";IS_DATE_LOC=" + IsDateLOC

                + ";" + this.CatalogFilter.ToString()
            ;
        }
        static public DataFilter Parse(string df)
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
                    return
                        new DataFilter(
                            dtp,
                            string.IsNullOrEmpty(dic["FLAGAQC"].Trim()) ? null : (byte?)byte.Parse(dic["FLAGAQC"]),
                            (dic["ISONEVALUE"].ToUpper() == "TRUE") ? true : false,
                            (dic["ISSELECTDELETED"].ToUpper() == "TRUE") ? true : false,
                            (dic["ISREFSITEDATA"].ToUpper() == "TRUE") ? true : false,
                            (dic["IS_DATE_LOC"].ToUpper() == "TRUE") ? true : false,

                            CatalogFilter.Parse(df)
                        );
                }
                return new DataFilter();
            }
            catch 
            {
                return new DataFilter();
            }
        }

        public DataFilter()
        {
            DateTimePeriod = new DateTimePeriod();

            this.FlagAQC = null;
            this.IsActualValueOnly = true;
            this.IsSelectDeleted = false;
            this.IsRefSiteData = false;

            this.CatalogFilter = new CatalogFilter();
        }
        public DataFilter
        (
            DateTimePeriod DateTimePeriod,

            byte? FlagQCL,
            bool IsOneValue,
            bool IsSelectDeleted,
            bool IsRefSiteData,
            bool IsDateLOC,

            CatalogFilter CatalogFilter
        )
        {
            this.DateTimePeriod = DateTimePeriod;

            this.FlagAQC = FlagQCL;
            this.IsActualValueOnly = IsOneValue;
            this.IsSelectDeleted = IsSelectDeleted;
            this.IsRefSiteData = IsRefSiteData;
            this.IsDateLOC = IsDateLOC;

            this.CatalogFilter = CatalogFilter;
        }
    }
}
