using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Amur.DataP
{
    public class DerivedDayAttr
    {
        public DerivedDayAttr() { }
        public DerivedDayAttr(int id, string name, int methodId, int siteTypeId, int meteoZoneId, int variableId,
            int offsetTypeId, double offsetValue, int srcDateTypeId, int hourAddForStart, bool isIncludeF, int dstDateTypeId)
        {
            Id = id;
            Name = name;
            MethodId = methodId;
            SiteTypeId = siteTypeId;
            MeteoZoneId = meteoZoneId;
            VariableId = variableId;
            OffsetTypeId = offsetTypeId;
            OffsetValue = offsetValue;
            SrcDateTypeId = srcDateTypeId;
            HourAddForStart = hourAddForStart;
            IsIncludeF = isIncludeF;
            DstDateTypeId = dstDateTypeId;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int MethodId { get; set; }
        public int SiteTypeId { get; set; }
        public int MeteoZoneId { get; set; }
        public int VariableId { get; set; }
        public int OffsetTypeId { get; set; }
        public double OffsetValue { get; set; }
        public int SrcDateTypeId { get; set; }
        public int HourAddForStart { get; set; }
        public bool IsIncludeF { get; set; }
        public int DstDateTypeId { get; set; }
    }
}
