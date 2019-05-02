using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Amur.Meta;
using SOV.Common;

namespace SOV.Amur.DataP
{
    public static class Enums
    {
        public enum TypeTask
        {
            DAY = 1,
            UNIT_TIME = 2,
            SECOND = 3
        }
    }
    public class DerivedTask
    {
        public DerivedTask() { }

        public DerivedTask(int id, int methodDerId, int stationTypeId, int siteTypeId, int[] variableSrcId, int unitTimeId, int offsetTypeId, double offsetValue,
            int? methodId, int? sourceId, int timeSupport, int[] dataTypeId, string param, bool isSchedule, bool isFcsData)
        {
            Id = id; MethodDerId = methodDerId; SiteTypeId = siteTypeId; VariableSrcId = variableSrcId;
            UnitTimeId = unitTimeId; OffsetTypeId = offsetTypeId; OffsetValue = offsetValue;
            MethodSrc = methodId; SourceSrc = sourceId; TimeSupport = timeSupport; DataTypeId = dataTypeId; Param = param;
            IsSchedule = isSchedule; IsFcsData = isFcsData;

            int dstDateTypeId;
            int srcDateTypeId;


            if (!int.TryParse(StrVia.GetValue(param, "@dateTypeDstId"), out dstDateTypeId))
                dstDateTypeId = -1;
            if (!int.TryParse(StrVia.GetValue(param, "@dateTypeSrcId"), out srcDateTypeId))
                srcDateTypeId = -1;

            if (unitTimeId == (int)EnumTime.Day)
            {
                TypeTask = Enums.TypeTask.DAY;
                if (VariableSrcId.Length != 1)
                    throw new Exception("Error in DerivedTaskId=" + id + "! VariableStr is not 1");
                if (!MethodSrc.HasValue)
                    throw new Exception("Error in DerivedTaskId=" + id + "! MethodSetId is null");
                if (!SourceSrc.HasValue)
                    throw new Exception("Error in DerivedTaskId=" + id + "! SourceSetId is null");
                if (dstDateTypeId == -1)
                    throw new Exception("Error in DerivedTaskId=" + id + "! No attr in params @dateTypeDstId");
                else
                    DateTypeId = dstDateTypeId;
                DayRun = 1;
            }
            else



                if (unitTimeId == (int)EnumTime.Second)
                {
                    TypeTask = Enums.TypeTask.SECOND;
                    DayRun = 1;
                    if (srcDateTypeId == -1)
                        throw new Exception("Error in DerivedTaskId=" + id + "! No attr in params @srcDateTypeId");
                    else
                        DateTypeId = srcDateTypeId;
                }
                else
                {
                    TypeTask = Enums.TypeTask.UNIT_TIME;
                    if (srcDateTypeId == -1)
                        throw new Exception("Error in DerivedTaskId=" + id + "! No attr in params @srcDateTypeId");
                    else
                        DateTypeId = srcDateTypeId;
                    int dayRun;
                    if (!int.TryParse(StrVia.GetValue(param, "@dayRun"), out dayRun))
                    {
                        DayRun = 1;
                    }
                    else
                        DayRun = dayRun;
                }
        }

        public Enums.TypeTask TypeTask { get; private set; }
        public DateTime GetDateShift(DateTime date, List<int[]> seasonMonthes = null)
        {
            DateTime ret = date;
            int dayShift;
            if (int.TryParse(StrVia.GetValue(Param, "@dayShift"), out dayShift))
            {
                ret = ret.AddDays(-1 * dayShift);
            }
            return ret;
        }
        public bool IsSchedule { get; set; }
        public bool IsFcsData { get; set; }
        public int? GetSiteGroupId()
        {
            int ret;
            if (int.TryParse(StrVia.GetValue(Param, "@siteGroupId"), out ret))
            {
                return ret;
            }
            else
                return null;
        }
        public int? GetSiteId()
        {
            int ret;
            if (int.TryParse(StrVia.GetValue(Param, "@siteId"), out ret))
            {
                return ret;
            }
            else
                return null;
        }
        public int DayRun { get; set; }
        public bool IsRun(DateTime date, List<int[]> seasonMonthes = null)
        {
            bool ret = true;
            if (TypeTask == Enums.TypeTask.UNIT_TIME)
            {
                DateTime dateTimeNumS = Time.GetDateSTimeNum(date, UnitTimeId, seasonMonthes);
                if (date > dateTimeNumS.AddDays(DayRun - 1))
                    ret = false;
            }
            return ret;
        }
        private string Param { get; set; }
        public int Id { get; set; }

        public int SiteTypeId { get; set; }

        //////public int StationTypeId { get; set; }

        public int[] VariableSrcId { get; set; }

        public int MethodDerId { get; set; }

        public int UnitTimeId { get; set; }

        public int DateTypeId { get; set; }

        public int OffsetTypeId { get; set; }

        public double OffsetValue { get; set; }

        private int _methodSrc;
        public int? MethodSrc
        {
            get { return (this._methodSrc < 0) ? (int?)null : this._methodSrc; }
            set { this._methodSrc = (value != null) ? (int)value : -1; }
        }
        private int _sourceSrc;
        public int? SourceSrc
        {
            get { return (this._sourceSrc < 0) ? (int?)null : this._sourceSrc; }
            set { this._sourceSrc = (value != null) ? (int)value : -1; }
        }
        public int TimeSupport { get; set; }
        public int[] DataTypeId { get; set; }

    }
}
