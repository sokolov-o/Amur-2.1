using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FERHRI.Common;

namespace FERHRI.Amur.Data
{
    public class DataFilter
    {
        DateTime _dateS, _dateF;
        public DateTime DateS { get { return _dateS; } set { _dateS = new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, 0); } }
        public DateTime DateF { get { return _dateF; } set { _dateF = new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, 0); } }

        public List<int> SiteIdList { get; set; }
        public List<int> VariableIdList { get; set; }

        public int? OffsetTypeId { get; set; }
        public double? OffsetValue { get; set; }
        public int? MethodId { get; set; }
        public int? SourceId { get; set; }
        public byte? FlagAQC { get; set; }


        public bool IsOneValue { get; set; }
        public bool IsSelectDeleted { get; set; }
        /// <summary>
        /// Выбирать/отображать данные ссылочного пункта?
        /// </summary>
        public bool IsRefSiteData { get; set; }

        public override string ToString()
        {
            return
                "DATESF=" + DateS.ToString() + "," + DateF.ToString()
                + ";SITEIDLIST=" + StrVia.ToString(SiteIdList)
                + ";VARIABLEIDLIST=" + StrVia.ToString(VariableIdList)
                + ";OFFSETTYPEID=" + ((OffsetTypeId.HasValue) ? OffsetTypeId.ToString() : "")
                + ";OFFSETVALUE=" + ((OffsetValue.HasValue) ? OffsetValue.ToString() : "")
                + ";METHODID=" + ((OffsetTypeId.HasValue) ? OffsetTypeId.ToString() : "")
                + ";SOURCEID=" + ((OffsetTypeId.HasValue) ? OffsetTypeId.ToString() : "")
                + ";FLAGAQC=" + ((FlagAQC.HasValue) ? FlagAQC.ToString() : "")
                + ";ISONEVALUE=" + IsOneValue
                + ";ISSELECTDELETED=" + IsSelectDeleted
                + ";ISREFSITEDATA=" + IsRefSiteData
            ;
        }
        static public DataFilter Parse(string df)
        {
            Dictionary<string, string> dic = StrVia.ToDictionary(df);
            string[] s = dic["DATESF"].Split(',');

            return
                new DataFilter(
                    DateTime.Parse(s[0]), DateTime.Parse(s[1]),
                    StrVia.ToListInt(dic["SITEIDLIST"]),
                    StrVia.ToListInt(dic["VARIABLEIDLIST"]),
                    string.IsNullOrEmpty(dic["OFFSETTYPEID"].Trim()) ? null : (int?)int.Parse(dic["OFFSETTYPEID"]),
                    string.IsNullOrEmpty(dic["OFFSETVALUE"].Trim()) ? null : (double?)double.Parse(dic["OFFSETVALUE"]),
                    string.IsNullOrEmpty(dic["METHODID"].Trim()) ? null : (int?)int.Parse(dic["METHODID"]),
                    string.IsNullOrEmpty(dic["SOURCEID"].Trim()) ? null : (int?)int.Parse(dic["SOURCEID"]),
                    string.IsNullOrEmpty(dic["FLAGAQC"].Trim()) ? null : (byte?)byte.Parse(dic["FLAGAQC"]),
                    (dic["ISONEVALUE"].ToUpper() == "TRUE") ? true : false,
                    (dic["ISSELECTDELETED"].ToUpper() == "TRUE") ? true : false,
                    (dic["ISREFSITEDATA"].ToUpper() == "TRUE") ? true : false
                );
        }

        public DataFilter
        (
            DateTime DateS,
            DateTime DateF,

            List<int> SiteIdList,
            List<int> VariableIdList,

            int? OffsetTypeId,
            double? OffsetValue,
            int? MethodId,
            int? SourceId,
            byte? FlagQCL,

            bool IsOneValue,
            bool IsSelectDeleted,
            bool IsRefSiteData
        )
        {
            this.DateS = DateS;
            this.DateF = DateF;

            this.SiteIdList = SiteIdList;
            this.VariableIdList = VariableIdList;

            this.OffsetTypeId = OffsetTypeId;
            this.OffsetValue = OffsetValue;
            this.MethodId = MethodId;
            this.SourceId = SourceId;
            this.FlagAQC = FlagQCL;

            this.IsOneValue = IsOneValue;
            this.IsSelectDeleted = IsSelectDeleted;
            this.IsRefSiteData = IsRefSiteData;
        }
    }
}
