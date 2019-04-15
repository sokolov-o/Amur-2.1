using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SOV.Amur.Data;
using SOV.Amur.Meta;
using SOV.Common;
using SOV.Social;
using Microsoft.Reporting.WinForms;

namespace SOV.Amur.Reports
{
    public class BaseReportHandler
    {
        public List<ReportParameter> parameters = new List<ReportParameter>();
        public List<ReportDataSource> dataSources = new List<ReportDataSource>();
        public Report reportObj;
        public virtual void initViewerSettings(ref ReportViewer viewer)
        {
            viewer.LocalReport.DataSources.Clear();
            viewer.LocalReport.ReportEmbeddedResource = "SOV.Amur.Report.Report" + reportObj.Id + ".rdlc";
            viewer.LocalReport.DisplayName = reportObj.NameFull;
            foreach (ReportDataSource dataSource in dataSources)
                viewer.LocalReport.DataSources.Add(dataSource);
            viewer.LocalReport.SetParameters(parameters);

            viewer.LocalReport.Refresh();
            viewer.RefreshReport();
            viewer.Dock = DockStyle.Fill;
        }
    }

    public class WaterObjReport : BaseReportHandler
    {
        public DataFilter dataFilter;

        public enum Type
        {
            Summer = 0,
            Winter = 1,
        }

        public class WaterObjDataItem
        {
            protected Site site;
            //Список всех сайтов: [0 - ГК, 1 - АГК, 2 - МС]
            protected Dictionary<EnumStationType, List<Site>> groupedSites = new Dictionary<EnumStationType, List<Site>>
            {
                { EnumStationType.HydroPost, null},
                { EnumStationType.AHK, null},
                { EnumStationType.MeteoStation, null}
            };
            protected List<DataValue> data;
            protected List<Catalog> catalogs;
            protected List<Climate> climate;
            protected EnumDateType timeType;
            public int? perennialGageHeight { get; protected set; }

            public WaterObjDataItem(Site site, DataFilter dataFilter, EnumVariable perennialVarId, EnumTime perennialTimeId)
            {
                Data.DataManager drep = Data.DataManager.GetInstance();
                Meta.DataManager mrep = Meta.DataManager.GetInstance();

                timeType = dataFilter.IsDateLOC ? EnumDateType.LOC : EnumDateType.UTC;
                this.site = mrep.SiteRepository.Select(site.Id);
                catalogs = mrep.CatalogRepository.Select(dataFilter.CatalogFilter);
                var allSites = mrep.SiteRepository.Select(dataFilter.CatalogFilter.Sites);
                foreach (var siteType in groupedSites.Keys.ToArray())
                    groupedSites[siteType] = allSites.Where(x => x.TypeId == (int)siteType).ToList();

                climate = drep.ClimateRepository.SelectClimateNearestMetaAndData(
                    DateTime.Now.Year, 2,
                    new List<int>() { site.Id },
                    new List<int>(new int[] { (int)perennialVarId }),
                    (int)EnumOffsetType.NoOffset, 0,
                    (int)EnumDataType.Average, (int)perennialTimeId
                );
                data = drep.DataValueRepository.SelectA(dataFilter).OrderBy(x => x.DateUTC).ToList();
                perennialGageHeight = null;
            }

            protected List<DataValue> SubData(ref List<DataValue> data, int varId, List<int> sites = null)
            {
                var subCatalogs = catalogs.Where(
                    x => x.VariableId == varId && (sites == null || sites.Contains(x.SiteId))
                ).ToList();
                return data.Where(x => subCatalogs.Exists(y => y.Id == x.CatalogId)).ToList();
            }

            public string geoObj
            {
                get
                {
                    Meta.DataManager mrep = Meta.DataManager.GetInstance();
                    var geoObjId = mrep.SiteGeoObjectRepository.SelectBySites(new List<int>() { site.Id });
                    return geoObjId.Count == 0 ? "" : mrep.GeoObjectRepository.Select(geoObjId[0].GeoObjectId).Name;
                }
            }
            public string stationName
            {
                get { return site.Code + " " + site.Name; }
            }
            public double? avgPrecip
            {
                get
                {
                    foreach (var sites in groupedSites.Values)
                    {
                        var tmp = SubData(ref data, (int)EnumVariable.PrecipDay24F, sites.Select(x => x.Id).ToList());
                        if (tmp.Count != 0)
                            return tmp.Average(x => x.Value);
                    }
                    return null;
                }
            }
            public double? avgDischarge
            {
                get
                {
                    List<DataValue> tmp = SubData(ref data, (int)EnumVariable.Discharge);
                    if (tmp.Count == 0)
                        return null;
                    return tmp.Average(x => x.Value);
                }
            }
            protected List<DataValue> gageHeight()
            {
                return SubData(
                    ref data,
                    (int)EnumVariable.GageHeightF,
                    groupedSites[EnumStationType.HydroPost].Select(x => x.Id).ToList()
                );
            }
        }

        public class WaterObjDailyDataItem : WaterObjDataItem
        {
            private DataValue trendData;
            private double? prevDayGageHeight;

            public WaterObjDailyDataItem(Site site, DataFilter dataFilter)
                : base(site, dataFilter, EnumVariable.GageHeightAvgDecade, EnumTime.DecadeOfYear)
            {
                Data.DataManager drep = Data.DataManager.GetInstance();

                trendData = drep.DataValueRepository.SelectTrendStart(
                    dataFilter.DateTimePeriod.DateS.Value,
                    (int)EnumVariable.GageHeightF,
                    site.Id
                );

                var gageHeightLimit = drep.ClimateRepository.SelectClimateNearestMetaAndData(
                    DateTime.Now.Year, 2,
                    new List<int> { base.site.Id },
                    new List<int> { (int)EnumVariable.GageHeightF },
                    (int)EnumOffsetType.NoOffset, 0,
                    (int)EnumDataType.Poyma, (int)EnumTime.YearCommon
                );

                DataFilter prevDataFilter = new DataFilter()
                {
                    DateTimePeriod = new DateTimePeriod(
                        dataFilter.DateTimePeriod.DateS.Value.AddDays(-1),
                        null,
                        dataFilter.DateTimePeriod.PeriodType,
                        0
                    ),
                    CatalogFilter = new CatalogFilter()
                    {
                        Methods = null,
                        Sources = null,
                        OffsetTypes = new List<int>(new int[] { (int)EnumOffsetType.NoOffset }),
                        OffsetValue = 0,
                        Variables = new List<int> { (int)EnumVariable.GageHeightF },
                        Sites = dataFilter.CatalogFilter.Sites
                    },

                };
                var tmp = drep.DataValueRepository.SelectA(prevDataFilter);
                if (tmp.Count != 0)
                    prevDayGageHeight = tmp.Find(x => x.Date(timeType) == tmp.Min(y => y.Date(timeType))).Value;

                poima = null;
                if (gageHeightLimit.Count != 0)
                    poima = (int)gageHeightLimit[0].Data[1];
                if (climate.Count != 0)
                    perennialGageHeight = (int)climate[0].Data[(short)dataFilter.DateTimePeriod.DateS.Value.Month];
            }

            public int? poima { get; private set; }

            public string trendStartDate
            {
                get
                {
                    if (trendData == null)
                        return "-";
                    return trendData.Date(timeType).ToString("dd.MM");
                }
            }

            public double? dailyGageHeight
            {
                get
                {
                    List<DataValue> tmp = gageHeight();
                    if (tmp.Count == 0)
                        return null;
                    return tmp.Find(x => x.Date(timeType) == tmp.Min(y => y.Date(timeType))).Value;
                }
            }

            public double? diffTrendGageHeight
            {
                get
                {
                    if (trendData == null || !dailyGageHeight.HasValue)
                        return null;
                    return dailyGageHeight.Value - trendData.Value;
                }
            }

            public double? diffPrevDayGageHeight
            {
                get
                {
                    if (!prevDayGageHeight.HasValue || !dailyGageHeight.HasValue)
                        return null;
                    return dailyGageHeight.Value - prevDayGageHeight.Value;
                }
            }

            public List<double?> forecast
            {
                get { return null; }
            }
        }

        public class WaterObjDecadeDataItem : WaterObjDataItem
        {
            protected List<DataValue> prevDecadeData;
            protected List<VariableCode> varCodes;

            public WaterObjDecadeDataItem(Site site, DataFilter dataFilter)
                : base(site, dataFilter, EnumVariable.GageHeightAvgDecade, EnumTime.DecadeOfYear)
            {
                Data.DataManager drep = Data.DataManager.GetInstance();
                DataFilter prevDataFilter = new DataFilter()
                {
                    DateTimePeriod = new DateTimePeriod(
                        dataFilter.DateTimePeriod.DateS.Value.AddDays(-10),
                        null,
                        dataFilter.DateTimePeriod.PeriodType,
                        0
                    ),
                    CatalogFilter = dataFilter.CatalogFilter,
                    FlagAQC = dataFilter.FlagAQC,
                    IsActualValueOnly = dataFilter.IsActualValueOnly,
                    IsRefSiteData = dataFilter.IsRefSiteData,
                    IsSelectDeleted = dataFilter.IsSelectDeleted
                };
                prevDecadeData = drep.DataValueRepository.SelectA(prevDataFilter).OrderBy(x => x.DateUTC).ToList();
                short decadeNumber = DateTimeProcess.GetDecadeNumber(dataFilter.DateTimePeriod);
                if (climate.Count != 0)
                    perennialGageHeight = (int)climate[0].Data[decadeNumber];
                varCodes = Meta.DataManager.GetInstance()
                    .VariableCodeRepository.Select((int)EnumVariable.SnowDepthIce);
            }
            public double? avgGageHeight
            {
                get
                {
                    List<DataValue> tmp = gageHeight();
                    if (tmp.Count == 0)
                        return null;
                    return tmp.Average(x => x.Value);
                }
            }
            public double? maxGageHeight
            {
                get
                {
                    List<DataValue> tmp = gageHeight();
                    if (tmp.Count == 0)
                        return null;
                    return tmp.Max(x => x.Value);
                }
            }
            public double? minGageHeight
            {
                get
                {
                    List<DataValue> tmp = gageHeight();
                    if (tmp.Count == 0)
                        return null;
                    return tmp.Min(x => x.Value);
                }
            }
            public double? prevDecadeGageHeight
            {
                get
                {
                    List<DataValue> tmp = SubData(ref prevDecadeData, (int)EnumVariable.GageHeightF);
                    if (tmp.Count == 0)
                        return null;
                    return tmp.Average(x => x.Value);
                }
            }
            public double? diffPrevDecadeGageHeight
            {
                get
                {
                    if (!avgGageHeight.HasValue || !prevDecadeGageHeight.HasValue)
                        return null;
                    return avgGageHeight - prevDecadeGageHeight;
                }
            }
            public double? diffPerennialGageHeight
            {
                get
                {
                    if (!avgGageHeight.HasValue || !perennialGageHeight.HasValue)
                        return null;
                    return avgGageHeight - perennialGageHeight;
                }
            }

            public double? avgIceDepth
            {
                get
                {
                    foreach (var sites in groupedSites.Values)
                    {
                        var tmp = SubData(ref data, (int)EnumVariable.IceDepthF, sites.Select(x => x.Id).ToList());
                        if (tmp.Count != 0)
                            return tmp.Average(x => x.Value);
                    }
                    return null;
                }
            }

            public string snowDepthIce
            {
                get
                {
                    var tmp = SubData(ref data, (int)EnumVariable.SnowDepthIce);
                    return tmp != null && tmp.Count > 0 ? varCodes.Find(x => x.Code == tmp[0].Value).Name : "";
                }
            }
        }

        EntityGroup siteGroup;
        EnumDateType timeType;
        Type type;

        public WaterObjReport(EntityGroup siteGroup, DataFilter dataFilter, Report rep, Org org, Type type,
                                string viewText, string dangerText1, string dangerText2, DateTime dateS,
                                string author1, string author2, string pos1, string pos2)
        {
            this.siteGroup = siteGroup;
            this.dataFilter = dataFilter;
            this.reportObj = rep;
            ////this.timeType = timeType;
            this.type = type;
            Dictionary<DateTimePeriod.Type, string> dic = new Dictionary<DateTimePeriod.Type, string>() {
                {DateTimePeriod.Type.FstDecade, "первую"},
                {DateTimePeriod.Type.SndDecade, "вторую"},
                {DateTimePeriod.Type.ThdDecade, "третью"}
            };
            DateTimePeriod.Type periodType = dataFilter.DateTimePeriod.PeriodType;
            string datePeriod = (
                periodType == DateTimePeriod.Type.Day
                    ? dateS.ToString("dd.MM.yyyy")
                    : dic[periodType] + " декаду " + dateS.ToString("MMMM yyyy")
            );

            Social.DataManager socialDM = Social.DataManager.GetInstance();
            string imgLogo = org.ImgId.HasValue ? Convert.ToBase64String(socialDM.ImageRepository.Select(org.ImgId.Value).Img) : "";
            LegalEntity entity = socialDM.LegalEntityRepository.Select(org.OrgId);
            parameters.AddRange(new ReportParameter[] {
                new ReportParameter("orgFullName", org.Param ?? ""),
                new ReportParameter("orgPhone", entity.Phones ?? ""),
                new ReportParameter("orgEmail", entity.Email ?? ""),
                new ReportParameter("orgAddress",  entity.AddrAdd),
                new ReportParameter("orgLogoFile", imgLogo),
                new ReportParameter("datePeriod", datePeriod),
                new ReportParameter("danger1Text", dangerText1),
                new ReportParameter("danger2Text", dangerText2),
                new ReportParameter("author1", author1),
                new ReportParameter("author2", author2),
                new ReportParameter("pos1", pos1),
                new ReportParameter("pos2", pos2),
                new ReportParameter("viewText", viewText),
                new ReportParameter("dataTableId",
                    (periodType == DateTimePeriod.Type.Day ? 0 : (type == Type.Summer ? 1 : 2)).ToString()
                ),
                new ReportParameter("dateS", dateS.ToString("dd.MM.yyyy"))
            });
        }

        public override void initViewerSettings(ref ReportViewer viewer)
        {
            viewer.LocalReport.EnableExternalImages = true;

            List<int> varIds = new List<int> {
                (int)EnumVariable.GageHeightF,
                (int)EnumVariable.PrecipDay24F,
                (int)EnumVariable.Discharge,
                (int)EnumVariable.SnowDepthIce,
                (int)EnumVariable.IceDepthF,
            };

            List<WaterObjDataItem> waterObjData = new List<WaterObjDataItem>();

            for (int i = 0; i < siteGroup.Items.Count; ++i)
            {
                Site site = ((Site)siteGroup.Items[i]);

                List<Site> sites =  Meta.DataManager.GetInstance().SiteRepository.SelectByParent((int)site.Id);
                if (sites.Count > 0)
                {
                    dataFilter.CatalogFilter.Variables = varIds;
                    dataFilter.CatalogFilter.Sites = sites.Select(x => x.Id).Distinct().ToList();

                    waterObjData.Add(
                        dataFilter.DateTimePeriod.PeriodType == DateTimePeriod.Type.Day ?
                        (WaterObjDataItem)new WaterObjDailyDataItem(site, dataFilter) :
                        (WaterObjDataItem)new WaterObjDecadeDataItem(site, dataFilter)
                        );
                }
            }
            dataSources.Add(new ReportDataSource("DecadeDataItem", waterObjData));
            dataSources.Add(new ReportDataSource("DailyDataItem", waterObjData));
            base.initViewerSettings(ref viewer);
        }
    }

    public partial class UCReport : UserControl
    {

        private int _repId;
        private Report _report;
        public Report Report { get { return _report; } }
        private DataFilter _dataFilter;
        private string _user;
        private object _reportDataSet;

        public UCReport(BaseReportHandler handler)
        {
            InitializeComponent();
            handler.initViewerSettings(ref reportViewer);
        }

        public UCReport(int reportId, DataFilter dataFilter, string user)
        {
            InitializeComponent();
            this._repId = reportId;
            this._dataFilter = dataFilter;
            this._user = user;
        }
        public UCReport(int reportId, DataFilter dataFilter, string user, object repDataSet)
        {
            InitializeComponent();
            this._repId = reportId;
            this._dataFilter = dataFilter;
            this._user = user;
            this._reportDataSet = repDataSet;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            switch (_repId)
            {
                case (int)EnumReport.AHC10:
                case (int)EnumReport.AHC11:
                    OnLoadJournal();
                    break;
                case (int)EnumReport.AHC12:
                    OnLoadDiffRR();
                    break;
                case (int)EnumReport.GP25:
                    OnLoadGP();
                    break;
                default:
                    throw new Exception("UNKNOWN ReportId=" + _repId);
            }
            //this.Controls.Add(this._reportViewer);
        }

        private void OnLoadGP()
        {
            DateTime dateS = (DateTime)_dataFilter.DateTimePeriod.DateS;
            DateTime dateF = (DateTime)_dataFilter.DateTimePeriod.DateF;
            int siteId = _dataFilter.CatalogFilter.Sites[0];

            Meta.DataManager dmm = Meta.DataManager.GetInstance();
            Data.DataManager dmd = Data.DataManager.GetInstance();

            Site site = dmm.SiteRepository.Select(siteId);

            List<SiteGeoObject> sgos = dmm.SiteGeoObjectRepository.SelectBySites(new List<int> { site.Id });
            List<GeoObject> geoObjList = sgos.Count == 0 ? null : dmm.GeoObjectRepository.Select(sgos.Select(x => x.GeoObjectId).ToList());
            int offsetTypeId = 0;
            int offsetValue = 0;


            List<Climate> clm = dmd.ClimateRepository.SelectClimateNearestMetaAndData(dateS.Year, 1900, new List<int>() { site.Id },
                    new List<int>() { (int)EnumVariable.WMonth, (int)EnumVariable.DischargeAvgMonth,
                        (int)EnumVariable.GageHeightF, (int)EnumVariable.GageHeightAvgYear }, offsetTypeId, offsetValue, null, 107);
            List<EntityAttrValue> sac = dmm.EntityAttrRepository.SelectAttrValues("site", site.Id);// Загрузка атрибутов
            List<int> varList = new List<int> { (int)EnumVariable.GageHeightF, (int)EnumVariable.GageHeightShiftDay,
                (int)EnumVariable.PrecipDay24F, (int)EnumVariable.TempWaterF,
                (int)EnumVariable.TempAirObs, (int)EnumVariable.Discharge,
                (int)EnumVariable.IceDepthF, (int)EnumVariable.SnowDepthIce,
                (int)EnumVariable.IcePhenom};
            List<DataValue> dvs = dmd.DataValueRepository.SelectA(dateS.AddDays(-1), dateF, true,
                    new List<int>() { site.Id }, varList, new List<int>() { offsetTypeId }, offsetValue, true);
            List<Catalog> ctlList = dmm.CatalogRepository.Select(dvs.Select(x => x.CatalogId).Distinct().ToList());
            List<DataValueCatalog> dvc = DataValueCatalog.GetList(ctlList, dvs);

            GP25Header gp25Header = new GP25Header(site, sac, geoObjList, clm, dateS, dateF);
            List<VariableCode> categoryList = dmm.VariableCodeRepository.Select(
                new List<int>() { (int)EnumVariable.IcePhenom, (int)EnumVariable.SnowDepthIce });//Ледовые явления, Толщина снега на льду

            GP25Report gp25Data = new GP25Report(site, dvc, dateS.AddDays(-1), dateF, categoryList);
            gp25Data.LoadReport(); // Формирования структуры отчета

            reportViewer.LocalReport.DataSources.Clear();
            reportViewer.LocalReport.ReportEmbeddedResource = "SOV.Amur.Report.ReportGP25.rdlc";
            GP25ReportItem firstItem = gp25Data.FirstOrDefault(); // предыдущий день

            if (gp25Data.Count > 1)
            {
                gp25Data.RemoveAt(0); // удаляем предыдущий день
            }

            reportViewer.LocalReport.SetParameters(new ReportParameter[] {
                new ReportParameter("rpPrevDate", firstItem.LocalDate.ToString()),
                new ReportParameter("rpPrevLevelAt8", firstItem.LevelAt8.HasValue ? firstItem.LevelAt8.Value.ToString("#0") : " "),
                new ReportParameter("rpPrevLevelAt20", firstItem.LevelAt20.HasValue ? firstItem.LevelAt20.Value.ToString("#0") : " "),
                new ReportParameter("rpPrevLevelChange", firstItem.LevelChange.HasValue ? firstItem.LevelChange.Value.ToString("#0") : " "),
                new ReportParameter("rpPrevTWater", firstItem.TWater.HasValue ? firstItem.TWater.Value.ToString("#0.0") : " "),
                new ReportParameter("rpPrevTAir", firstItem.TAir.HasValue ? firstItem.TAir.Value.ToString("#0") : " "),
                new ReportParameter("rpPrevPrecipitation", firstItem.TotalPrecipitation.HasValue ? firstItem.TotalPrecipitation.Value.ToString("#0") : " "),
                new ReportParameter("rpPrevIceDepth", firstItem.IceDepth.HasValue ? firstItem.IceDepth.Value.ToString("#0") : " "),
                new ReportParameter("rpPrevSnowDepth", firstItem.SnowDepth),
                new ReportParameter("rpPrevIcePhenomena", firstItem.IcePhenomena),
                new ReportParameter("rpPrevDischarge", firstItem.Discharge.HasValue ? firstItem.Discharge.Value.ToString("#0") : " "),
                new ReportParameter("rpPrevComment", firstItem.Comment),
                new ReportParameter("rpValueCharacterIdAt8", firstItem.ValueCharacterIdAt8.ToString()),
                new ReportParameter("rpValueCharacterIdAt20", firstItem.ValueCharacterIdAt20.ToString())
            });


            Microsoft.Reporting.WinForms.ReportDataSource gp25HeaderDataSet =
                new Microsoft.Reporting.WinForms.ReportDataSource("GP25HeaderDataSet", new List<GP25Header> { gp25Header });
            reportViewer.LocalReport.DataSources.Add(gp25HeaderDataSet);


            Microsoft.Reporting.WinForms.ReportDataSource gp25DataSet = new Microsoft.Reporting.WinForms.ReportDataSource("GP25DataSet", gp25Data);
            reportViewer.LocalReport.DataSources.Add(gp25DataSet);



            //CustomMessageClass MessageClass = new CustomMessageClass();
            //reportViewer.Messages = MessageClass;
            reportViewer.LocalReport.Refresh();
            reportViewer.RefreshReport();
            //this._gp25ReportViewer = new GP25ReportViewer(gp25Header, gp25Report);
            //this.Controls.Add(this._gp25ReportViewer);
        }

        private void OnLoadDiffRR()
        {
            ReportRepository rep = DataManager.GetInstance().ReportRepository;
            DataValueRepository dvR = SOV.Amur.Data.DataManager.GetInstance().DataValueRepository;
            SiteRepository siteR = SOV.Amur.Meta.DataManager.GetInstance().SiteRepository;
            VariableRepository varR = SOV.Amur.Meta.DataManager.GetInstance().VariableRepository;
            Report _report = rep.Select(_repId);
            if (_dataFilter.DateTimePeriod.DateS == null || _dataFilter.DateTimePeriod.DateF == null || _dataFilter.CatalogFilter.Variables.Count != 1)
                throw new Exception("Не заданы даты или кол-во параметров не равно 1!");

            Variable variablePost = varR.Select(_dataFilter.CatalogFilter.Variables[0]);
            var variableAGH = variablePost.Clone();
            variableAGH.ValueTypeId = 1;
            variableAGH.Id = -1;
            var vcAGH = varR.Select(variableAGH.VariableTypeId, variableAGH.TimeId, variableAGH.UnitId, variableAGH.DataTypeId, variableAGH.GeneralCategoryId,
                variableAGH.SampleMediumId, variableAGH.TimeSupport, variableAGH.ValueTypeId);
            if (vcAGH != null)
                variableAGH = vcAGH;
            else
                throw new Exception("Нельзя определить параметр для АГК! ");
            DateTime dateS = ((DateTime)_dataFilter.DateTimePeriod.DateS).Date.AddHours(8);
            DateTime dateF = ((DateTime)_dataFilter.DateTimePeriod.DateF).Date.AddDays(1).AddMilliseconds(-1);
            List<DateTime> dateVec = new List<DateTime>();
            for (DateTime d = dateS; d <= dateF; d = d.AddDays(1))
            {
                dateVec.Add(d);
            }

            DiffReport dr = new DiffReport();
            foreach (int siteId in _dataFilter.CatalogFilter.Sites)
            {
                Site sitePost = siteR.Select(siteId);
                if (sitePost == null || sitePost.TypeId != (int)Meta.EnumStationType.HydroPost)
                    continue;
                //Data.DataValueCollection dataPost = dmi.DataValues.GetDataValuesLocal(sitePost.SiteId, dateS, dateF,
                //    variablePost.Id, _dataFilter.OffsetTypeId, _dataFilter.OffsetValue);

                List<DataValue> dataPost = dvR.SelectA(dateS, dateF, true,
                    new List<int>() { sitePost.Id }, new List<int>() { variablePost.Id },
                    _dataFilter.CatalogFilter.OffsetTypes, _dataFilter.CatalogFilter.OffsetValue, true);



                List<Site> siteAGK = siteR.SelectByParent(sitePost.Id)
                    .FindAll(x => x.TypeId == (int)EnumStationType.AHK);
                if (siteAGK.Count == 1)
                {
                    List<DataValue> dataAGH = dvR.SelectA(dateS, dateF, true, new List<int>() { siteAGK[0].Id }, new List<int>() { variableAGH.Id },
                        _dataFilter.CatalogFilter.OffsetTypes, _dataFilter.CatalogFilter.OffsetValue, true);
                    dr.AddRange(new DiffReport(dataPost, dataAGH, variablePost, sitePost, siteAGK[0], dateVec));
                }
                else
                {
                    //if (siteAGK.Count > 1)
                    //{
                    //    MessageBox.Show("Для станции есть несколько АГК " + siteAGK.Count);
                    //}
                }
            }

            reportViewer.LocalReport.DataSources.Clear();
            reportViewer.LocalReport.ReportEmbeddedResource = "SOV.Amur.Report.Report" + _repId + ".rdlc";
            var grpP = dr.GroupBy(t => t.ParamId);
            if (grpP.Count() > 1)
                throw new Exception("Параметров >1");

            int paramId = grpP.First().Key;
            reportViewer.LocalReport.DisplayName = _report.Name;

            reportViewer.LocalReport.SetParameters(new ReportParameter[] {
                new ReportParameter("paramId", paramId.ToString()),
                new ReportParameter("userName",_user),
                new ReportParameter("reportNameFull",_report.NameFull)
            });


            Microsoft.Reporting.WinForms.ReportDataSource repDataSet =
               new Microsoft.Reporting.WinForms.ReportDataSource("DiffDataSet", dr);
            reportViewer.LocalReport.DataSources.Add(repDataSet);

            reportViewer.LocalReport.SubreportProcessing +=
                    new SubreportProcessingEventHandler(DiffDS_SubreportProcessing);
            _reportDataSet = dr;
            //CustomMessageClass MessageClass = new CustomMessageClass();
            //reportViewer.Messages = MessageClass;
            reportViewer.LocalReport.Refresh();
            reportViewer.RefreshReport();
            reportViewer.Dock = DockStyle.Fill;
        }
        void DiffDS_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            //Передаем источник данных в отчет
            e.DataSources.Add(new ReportDataSource("DiffDataSet", _reportDataSet));
        }
        private void OnLoadJournal()
        {
            Data.DataManager dmd = Data.DataManager.GetInstance();
            Meta.DataManager dmm = Meta.DataManager.GetInstance();
            DataManager dmr = DataManager.GetInstance();
            _report = dmr.ReportRepository.Select(_repId);
            if (_dataFilter.DateTimePeriod.DateS == null || _dataFilter.DateTimePeriod.DateF == null ||
                _dataFilter.CatalogFilter.Variables.Count != 1)
                throw new Exception("Не заданы даты или кол-во параметров не равно 1!");

            var variable = dmm.VariableRepository.Select(_dataFilter.CatalogFilter.Variables[0]);
            DateTime dateS = (DateTime)_dataFilter.DateTimePeriod.DateS;
            DateTime dateF = ((DateTime)_dataFilter.DateTimePeriod.DateF).Date.AddDays(1).AddMilliseconds(-1);
            List<DateTime> dateVec = new List<DateTime>();
            for (DateTime d = dateS; d <= dateF; d = d.AddDays(1))
            {
                dateVec.Add(d.Date);
            }

            JournalReport jr = new JournalReport();
            List<DataValue> dataCol = dmd.DataValueRepository.SelectA(dateS, dateF, true, _dataFilter.CatalogFilter.Sites, new List<int>() { variable.Id }
                    , _dataFilter.CatalogFilter.OffsetTypes, _dataFilter.CatalogFilter.OffsetValue, true);

            Dictionary<int, Catalog> ctlDic = dmm.CatalogRepository.Select(_dataFilter.CatalogFilter.Sites, new List<int>() { variable.Id },
                null, null, _dataFilter.CatalogFilter.OffsetTypes, _dataFilter.CatalogFilter.OffsetValue).ToDictionary(t => t.Id);
            foreach (int siteId in _dataFilter.CatalogFilter.Sites)
            {
                Site site = dmm.SiteRepository.Select(siteId);
                if (site != null)
                    jr.AddRange(new JournalReport(dataCol, ctlDic, variable, site, dateVec));
            }

            reportViewer.LocalReport.DataSources.Clear();
            reportViewer.LocalReport.ReportEmbeddedResource = "SOV.Amur.Report.Report" + _repId + ".rdlc";
            var grpP = jr.GroupBy(t => t.ParamId);
            if (grpP.Count() > 1)
                throw new Exception("Параметров >1");

            int paramId = grpP.First().Key;
            reportViewer.LocalReport.DisplayName = _report.NameFull;

            reportViewer.LocalReport.SetParameters(new ReportParameter[] {
                new ReportParameter("paramId", paramId.ToString()),
                new ReportParameter("userName",_user),
                new ReportParameter("reportNameFull",_report.NameFull)
            });


            Microsoft.Reporting.WinForms.ReportDataSource journalDataSet =
               new Microsoft.Reporting.WinForms.ReportDataSource("JournalDataSet", jr);
            _reportDataSet = jr;
            reportViewer.LocalReport.DataSources.Add(journalDataSet);

            if (_repId == (int)EnumReport.AHC11)
                reportViewer.LocalReport.SubreportProcessing +=
                        new SubreportProcessingEventHandler(JournalDS_SubreportProcessing);

            //CustomMessageClass MessageClass = new CustomMessageClass();
            //reportViewer.Messages = MessageClass;
            reportViewer.LocalReport.Refresh();
            reportViewer.RefreshReport();
            reportViewer.Dock = DockStyle.Fill;


        }
        void JournalDS_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            //Передаем источник данных в отчет
            e.DataSources.Add(new ReportDataSource("JournalDataSet", _reportDataSet));
        }
    }
}
