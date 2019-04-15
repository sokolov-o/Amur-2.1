using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Amur.Data;
using SOV.Amur.Meta;
using SOV.Common;

namespace SOV.Amur.Reports
{
    public class DiffReportItem
    {
        public string PostName { get { return Site.GetName(_sitePost, 1, SiteTypeRepository.GetCash()); } }
        private Site _sitePost { get; set; }
        private Variable _var { get; set; }
        public int ParamId { get { return _var.Id; } }
        public string ParamName { get { return _var.NameRus; } }
        public DateTime Date { get; set; }
        private double _valueGP { get; set; }
        private double _valueAGH { get; set; }
        public double? ValueGP { get { return double.IsNaN(_valueGP) ? (double?)null : _valueGP; } }
        public double? ValueAGH { get { return double.IsNaN(_valueAGH) ? (double?)null : _valueAGH; } }
        public double? ValueDiff
        {
            get
            {
                if (double.IsNaN(_valueGP) || double.IsNaN(_valueAGH))
                    return null;
                if (_valueGP == 990)
                    return 0 - _valueAGH;
                else
                    return _valueGP - _valueAGH;
            }

        }

        public DiffReportItem(Site post, Variable var, DateTime date, double valueGP, double valueAGH)
        {
            this._sitePost = post;
            this.Date = date;
            this._valueGP = valueGP;
            this._valueAGH = valueAGH;
            this._var = var;
        }
    }
    public class DiffReport : List<DiffReportItem>
    {


        public DiffReport() { }

        public DiffReport(List<DataValue> dataPost, List<DataValue> dataAGH, Variable variablePost, Site sitePost, Site siteChild, List<DateTime> dateVec)
        {

            foreach (DateTime date in dateVec)
            {
                if (dataPost == null)
                    dataPost = new List<DataValue>();
                if (dataAGH == null)
                    dataAGH = new List<DataValue>();
                var pDV = dataPost.Where(t => t.DateLOC == date).ToArray();
                var aghDV = dataAGH.Where(t => t.DateLOC == date).ToArray();
                double valuePost = (pDV.Length == 1) ? pDV[0].Value : double.NaN;
                double valueAGH = (aghDV.Length == 1) ? aghDV[0].Value : double.NaN;
                Add(new DiffReportItem(sitePost, variablePost, date, valuePost, valueAGH));
            }

        }

    }

    public class JournalReportItem
    {
        private Site _site { get; set; }
        private Variable _variable { get; set; }
        public DateTime Date { get; set; }
        public string ParamName { get { return _variable.NameRus; } }
        public int ParamId { get { return _variable.Id; } }
        public string SiteCode { get { return _site.Code; } }
        public int SiteType { get { return _site.TypeId; } }
        public double? Value { get; set; }
        public bool? IsBad { get; set; }
        public int? CountInDay
        {
            get
            {
                return (_variable.TimeId == 100) ? 24 : (int?)null;
            }
        }


        public JournalReportItem(Variable var, Site site, double value, bool? isBad, DateTime date)
        {
            Value = double.IsNaN(value) ? (double?)null : value;
            IsBad = isBad;
            Date = date;
            _site = site;
            _variable = var;
        }
    }
    public class JournalReport : List<JournalReportItem>
    {
        public JournalReport() { }
        public JournalReport(List<DataValue> dataValues, Dictionary<int, Catalog> ctlDic, Variable var, Site site, List<DateTime> dateD)
        {
            foreach (DateTime d in dateD)
            {
                JournalReport jrI = new JournalReport();
                var data = (dataValues != null) ?
                    dataValues.Where(t => t.DateLOC.Date == d).ToArray() : new DataValue[0];
                if (data.Length == 0)

                    Add(new JournalReportItem(var, site, double.NaN, null, d.Date));
                else
                {
                    foreach (DataValue dv in data)
                    {
                        Catalog ctl;
                        if (ctlDic.TryGetValue(dv.CatalogId, out ctl))
                        {
                            if (ctl.VariableId == var.Id && ctl.SiteId == site.Id)
                            {
                                jrI.Add(new JournalReportItem(var, site, dv.Value, dv.FlagAQC == 2 ? true : false, dv.DateLOC));
                            }
                        }
                    }
                }
                if (jrI.Count == 0)
                    Add(new JournalReportItem(var, site, double.NaN, null, d.Date));
                else
                    AddRange(jrI);
            }
        }
    }


    public class GP25Header
    {
        //        public GP25Header(Site site, SiteAttributeCollection siteAttributeCollection, WaterObjectCollection woc, List<ClimateInfo> climateInfoList, CriteriaCollection criteriaCollection, DateTime beginDate, DateTime endDate)
        public GP25Header(Site site, List<EntityAttrValue> siteAttributeCollection, List<GeoObject> woc, List<Climate> clm, DateTime beginDate, DateTime endDate)
        {
            if (beginDate.AddDays(1).ToLocalTime().Month == endDate.AddDays(-1).ToLocalTime().Month)
            {
                this.Period = beginDate.AddDays(1).ToLocalTime().ToString("MMMM yyyy", System.Globalization.CultureInfo.CurrentUICulture);
                if (!string.IsNullOrEmpty(this.Period))
                {
                    this.Period = char.ToUpper(this.Period[0]) + this.Period.Substring(1);
                }
            }
            else
            {
                this.Period = string.Format("{0:dd.MM.yyyy} - {1:dd.MM.yyyy}", beginDate.ToLocalTime(), endDate.ToLocalTime());
            }
            int stationIndex = 0;
            if (Int32.TryParse(site.Code, out stationIndex))
            {
                this.StationIndex = stationIndex;
            }
            this.StationName = site.Name;
            if (woc != null && woc.Count > 0)
            {
                for (int w = 0; w < woc.Count; w++)
                {
                    this.WaterObject += woc[w].Name;
                    if (w != woc.Count - 1)
                        this.WaterObject += ";";
                }
            }
            try
            {
                double val;
                foreach (var clmI in clm)
                {
                    if (clmI.VariableId == (int)EnumVariable.GageHeightAvgYear && clmI.DataTypeId == (int)EnumDataType.Average
                        && clmI.Data != null)
                    {
                        this.H = clmI.Data.TryGetValue(1, out val) ? val : default(double?);
                    }
                    if (clmI.VariableId == (int)EnumVariable.DischargeAvgMonth && clmI.DataTypeId == (int)EnumDataType.Average
                        && clmI.Data != null)
                    {
                        this.Q = clmI.Data.TryGetValue(1, out val) ? val : default(double?);
                    }
                    if (clmI.VariableId == (int)EnumVariable.WMonth && clmI.DataTypeId == (int)EnumDataType.Average
                        && clmI.Data != null)
                    {
                        this.W = clmI.Data.TryGetValue(1, out val) ? val : default(double?);
                    }
                    if (clmI.VariableId == (int)EnumVariable.GageHeightF && clmI.DataTypeId == (int)EnumDataType.Poyma
                        && clmI.Data != null)// Выход на пойму
                    {
                        this.FloodPlain = clmI.Data.TryGetValue(1, out val) ? val : default(double?);
                    }
                    if (clmI.VariableId == (int)EnumVariable.GageHeightF && clmI.DataTypeId == (int)EnumDataType.NYa
                            && clmI.Data != null)// Неблагоприятное явление
                    {
                        this.NegativePhenomena = clmI.Data.TryGetValue(1, out val) ? val : default(double?);
                    }
                    if (clmI.VariableId == (int)EnumVariable.GageHeightF && clmI.DataTypeId == (int)EnumDataType.OYa
                            && clmI.Data != null)// Опасное явление
                    {
                        this.DangerousPhenomena = clmI.Data.TryGetValue(1, out val) ? val : default(double?);
                    }
                }

            }
            catch { }

            string decimalSeparator = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            try
            {
                var eav = EntityAttrValue.GetEntityAttrValue(siteAttributeCollection, site.Id, (int)EnumSiteAttrType.ZeroMarkChart, beginDate);
                this.ZeroMark = Convert.ToSingle(eav.Value.Replace(".", decimalSeparator).Replace(",", decimalSeparator)); // отметка "0" графика
            }
            catch { }
        }
        public string Period { get; set; } // период

        public string WaterObject { get; set; } // река, озеро, водохранилище

        public string StationName { get; set; } // Водомерный пост Название

        public int? StationIndex { get; set; } // Водомерный пост Индекс

        public double? ZeroMark { get; set; } // Отметка "0" графика

        public double? FloodPlain { get; set; } // Выход на пойму

        public double? NegativePhenomena { get; set; } // Неблагоприятное явление

        public double? DangerousPhenomena { get; set; } // Опасное явление

        public double? H { get; set; }

        public double? Q { get; set; }

        public double? W { get; set; }

    }
    public class GP25ReportItem
    {
        public DateTime LocalDate { get; set; }

        public double? LevelAt20 { get; set; }

        public int ValueCharacterIdAt20 { get; set; }

        public double? LevelAt8 { get; set; }

        public int ValueCharacterIdAt8 { get; set; }

        public double? LevelChange { get; set; }

        public double? TotalPrecipitation { get; set; }

        public double? TWater { get; set; }

        public double? TAir { get; set; }

        public double? IceDepth { get; set; }

        public string SnowDepth { get; set; }

        public string IcePhenomena { get; set; }

        public double? Discharge { get; set; }

        public string Comment { get; set; }

    }

    public class GP25Report : List<GP25ReportItem>
    {

        private Site Site { get; set; }

        private List<DataValueCatalog> DataValueCollection { get; set; }

        public DateTime LocalBeginDate { get; set; }

        public DateTime LocalEndDate { get; set; }

        public List<VariableCode> CategoryList { get; set; }

        public GP25Report(Site site, List<DataValueCatalog> dvc, DateTime localBeginDate, DateTime localEndDate, List<VariableCode> categoryList)
        {
            this.Site = site;
            this.DataValueCollection = dvc;
            this.LocalBeginDate = localBeginDate;
            this.LocalEndDate = localEndDate;
            this.CategoryList = categoryList;
        }

        public void LoadReport()
        {
            //IEnumerable<string> localDates = this.DataValueCollection.Select(d => d.Date.ToShortDateString()).Distinct();
            for (DateTime cd = LocalBeginDate.Date; cd <= LocalEndDate.Date; cd = cd.AddDays(1))
            {
                DataValueCatalog needDV = null;
                IEnumerable<DataValueCatalog> DateDV = this.DataValueCollection.Where(d => d.DataValue.DateLOC.Date == cd.Date);
                GP25ReportItem item = new GP25ReportItem();
                //item.LocalDate = cd.Date;
                //needDV = DateDV.Where(d => d.Date == cd.AddHours(8) && d.VariableId == 2).FirstOrDefault(); // Уровень за 08
                //item.LevelAt8 = needDV != null ? needDV.Value : default(float?);
                //item.ValueCharacterIdAt8 = needDV != null ? needDV.ValueCharacterId : default(int);


                //needDV = DateDV.Where(d => d.Date == cd.AddHours(20) && d.VariableId == 2).FirstOrDefault(); // Уровень за 20
                //item.LevelAt20 = needDV != null ? needDV.Value : default(float?);
                //item.ValueCharacterIdAt20 = needDV != null ? needDV.ValueCharacterId : default(int);

                //needDV = DateDV.Where(d => d.Date == cd.AddHours(8) && d.VariableId == 28).FirstOrDefault(); // Изменение уровня воды за сутки
                //if (needDV == null) // расчет изменения уровня за сутки
                //{
                //    item.LevelChange = null;
                //    if (item.LevelAt8.HasValue)
                //    {
                //        GP25ReportItem prevItem = this.LastOrDefault();
                //        if (prevItem != null && prevItem.LocalDate.Date == cd.AddDays(-1).Date && prevItem.LevelAt8.HasValue)
                //        {
                //            item.LevelChange = Convert.ToSingle(Math.Round(item.LevelAt8.Value, 0) - Math.Round(prevItem.LevelAt8.Value, 0));
                //        }
                //    }
                //}
                //else
                //{
                //    item.LevelChange = needDV.Value;
                //}

                //needDV = DateDV.Where(d => d.Date == cd.AddHours(8) && d.VariableId == 23).FirstOrDefault(); // Сумма осадков за сутки
                //item.TotalPrecipitation = needDV != null ? needDV.Value : default(float?);

                //if (item.TotalPrecipitation.HasValue && item.TotalPrecipitation == 990)
                //{
                //    item.TotalPrecipitation = 0.001f;
                //}

                //needDV = DateDV.Where(d => d.Date == cd.AddHours(8) && d.VariableId == 13).FirstOrDefault(); // Температура воды
                //item.TWater = needDV != null ? needDV.Value : default(float?);

                //needDV = DateDV.Where(d => d.Date == cd.AddHours(8) && d.VariableId == 5).FirstOrDefault(); // Температура воздуха
                //item.TAir = needDV != null ? needDV.Value : default(float?);

                //needDV = DateDV.Where(d => d.Date == cd.AddHours(8) && d.VariableId == 14 && d.MethodId == 2).FirstOrDefault(); // Расход воды расчитанный
                //item.Discharge = needDV != null ? needDV.Value : default(float?);

                //needDV = DateDV.Where(d => d.Date == cd.AddHours(8) && d.VariableId == 14 && d.MethodId == 0).FirstOrDefault(); // Расход воды измеренный
                //if (needDV != null)
                //{
                //    item.Comment += string.Format("ИР: {0:#0.0} ", needDV.Value, 1);
                //}

                //this.Add(item);

                item.LocalDate = cd.Date;
                needDV = DateDV.Where(d => d.DataValue.DateLOC == cd.AddHours(8)
                                        && d.Catalog.VariableId == (int)EnumVariable.GageHeightF).FirstOrDefault(); // Уровень за 08
                item.LevelAt8 = needDV != null ? needDV.DataValue.Value : default(double?);

                needDV = DateDV.Where(d => d.DataValue.DateLOC == cd.AddHours(20)
                                        && d.Catalog.VariableId == (int)EnumVariable.GageHeightF).FirstOrDefault(); // Уровень за 20
                item.LevelAt20 = needDV != null ? needDV.DataValue.Value : default(double?);

                needDV = DateDV.Where(d => d.DataValue.DateLOC == cd.AddHours(8)
                                    && d.Catalog.VariableId == (int)EnumVariable.GageHeightShiftDay).FirstOrDefault(); // Изменение уровня воды за сутки
                if (needDV == null) // расчет изменения уровня за сутки
                {
                    item.LevelChange = null;
                    if (item.LevelAt8.HasValue)
                    {
                        GP25ReportItem prevItem = this.LastOrDefault();
                        if (prevItem != null && prevItem.LocalDate.Date == cd.AddDays(-1).Date && prevItem.LevelAt8.HasValue)
                        {
                            item.LevelChange = Convert.ToSingle(Math.Round(item.LevelAt8.Value, 0) - Math.Round(prevItem.LevelAt8.Value, 0));
                        }
                    }
                }
                else
                {
                    item.LevelChange = needDV.DataValue.Value;
                }

                needDV = DateDV.Where(d => d.DataValue.DateLOC == cd.AddHours(8)
                                && d.Catalog.VariableId == (int)EnumVariable.PrecipDay24F).FirstOrDefault(); // Сумма осадков за сутки
                item.TotalPrecipitation = needDV != null ? needDV.DataValue.Value : default(double?);

                if (item.TotalPrecipitation.HasValue && item.TotalPrecipitation == 990)
                {
                    item.TotalPrecipitation = 0.001f;
                }

                needDV = DateDV.Where(d => d.DataValue.DateLOC == cd.AddHours(8)
                                && d.Catalog.VariableId == (int)EnumVariable.TempWaterF).FirstOrDefault(); // Температура воды
                item.TWater = needDV != null ? needDV.DataValue.Value : default(double?);

                needDV = DateDV.Where(d => d.DataValue.DateLOC == cd.AddHours(8)
                                && d.Catalog.VariableId == (int)EnumVariable.TempAirObs).FirstOrDefault(); // Температура воздуха
                item.TAir = needDV != null ? needDV.DataValue.Value : default(double?);

                needDV = DateDV.Where(d => d.DataValue.DateLOC == cd.AddHours(8) && d.Catalog.VariableId == (int)EnumVariable.Discharge
                                    && d.Catalog.MethodId == (int)EnumMethod.InterpCurve).FirstOrDefault(); // Расход воды расчитанный
                item.Discharge = needDV != null ? needDV.DataValue.Value : default(double?);

                //needDV = DateDV.Where(d => d.Date == cd.AddHours(8) && d.VariableId == 14 && d.MethodId == 0).FirstOrDefault(); // Расход воды измеренный
                //if (needDV != null)
                //{
                //    item.Comment += string.Format("{0:#0.0} ", needDV.Value, 1);
                //}

                needDV = DateDV.Where(d => d.DataValue.DateLOC == cd.AddHours(8)
                            && d.Catalog.VariableId == (int)EnumVariable.IceDepthF
                            && d.Catalog.MethodId == (int)EnumMethod.ObservationInSitu).FirstOrDefault(); // Толщина льда
                if (needDV != null)
                {
                    item.IceDepth = needDV.DataValue.Value;
                }

                needDV = DateDV.Where(d => d.DataValue.DateLOC == cd.AddHours(8)
                    && d.Catalog.VariableId == (int)EnumVariable.SnowDepthIce
                    && d.Catalog.MethodId == (int)EnumMethod.ObservationInSitu).FirstOrDefault(); // Толщина снега на льду
                if (needDV != null)
                {
                    VariableCode category = this.CategoryList.Where(c => c.VariableId == (int)EnumVariable.SnowDepthIce
                        && c.Code == (int)needDV.DataValue.Value).FirstOrDefault();
                    if (category != null)
                    {
                        item.SnowDepth = category.Description;
                    }
                }
                var icePhenIntensity = new List<int>() { 12, 13, 14, 16, 19, 39, 48, 49, 64 };
                IEnumerable<DataValueCatalog> IcePhenList = DateDV.Where(d => d.DataValue.DateLOC == cd.AddHours(8)
                    && d.Catalog.VariableId == (int)EnumVariable.IcePhenom && d.Catalog.MethodId == (int)EnumMethod.ObservationInSitu); // Ледовые явления
                foreach (DataValueCatalog dv in IcePhenList)
                {
                    int pref = Convert.ToInt32(dv.DataValue.Value) / 100;
                    int postfix = Convert.ToInt32(dv.DataValue.Value) % 100;
                    VariableCode categoryPref = this.CategoryList.Where(c => c.VariableId == (int)EnumVariable.IcePhenom && c.Code == pref).FirstOrDefault();
                    if (categoryPref != null)
                    {

                        if (icePhenIntensity.Contains(pref))//categoryPref.IsOption)
                        {
                            item.IcePhenomena += string.Format("{0} {1}; ", categoryPref.NameShort, postfix);
                        }
                        else
                        {
                            item.IcePhenomena += string.Format("{0}; ", categoryPref.NameShort);
                            if (pref != postfix)
                            {
                                VariableCode categoryPostfix = this.CategoryList.Where(c => c.VariableId == (int)EnumVariable.IcePhenom
                                    && c.Code == postfix).FirstOrDefault();
                                if (categoryPostfix != null)
                                {
                                    item.IcePhenomena += string.Format("{0}; ", categoryPostfix.NameShort);
                                }
                            }
                        }
                    }
                }

                this.Add(item);
            }
        }

    }

}
