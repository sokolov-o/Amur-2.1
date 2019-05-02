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
    /// <summary>
    /// 
    /// Подготовка данных для отчёта "Сводная таблица по ГП за период" (Ф-50)
    /// 
    /// - считывание данных из БД;
    /// - расчёт необходимых параметров;
    /// 
    /// В отчёте приводятся стат. параметры уровня на ГП за период с отклонением от нормы, 
    /// с кол. дней и периодами затопления поймы и др.
    /// 
    /// OSokolov@SOV.ru, 201511-201601
    /// </summary>
    public class F50Collection : List<F50Row>
    {

        private F50Collection()
        {
            YearSClimate = 1900;
            YearFClimate = 2000;
        }
        public int YearSClimate { get; set; }
        public int YearFClimate { get; set; }
        /// <summary>
        /// 
        /// Формирование экземпляра отчёта Ф-50 для заданного периода времени.
        /// - считывание данных из БД;
        /// - расчёт необходимых параметров;
        /// 
        /// За основу берётся декадный климат уровня и по нему, при необходимости, "на лету" рассчитывается месячный климат.
        /// 
        /// </summary>
        /// <param name="monthDecade">Декада месяца [1;3]</param>
        /// <returns></returns>
        /// <param name="monthDecade">Декада месяца [1;3]</param>
        /// <param name="siteGroupId">Набор пунктов отчёта, либо все ГП, если null.</param>
        /// <returns></returns>
        static public F50Collection Instance(int siteGroupId, int year, int month, int? monthDecade = null, byte? flagAQC = null)
        {
            F50Collection ret = new F50Collection();
            ret.Year = year;
            ret.Month = month;
            ret.MonthDecade = monthDecade;

            int offsetTypeId = 0;
            double offsetValue = 0;

            DataValueRepository repdv = Data.DataManager.GetInstance().DataValueRepository;
            ClimateRepository repclm = Data.DataManager.GetInstance().ClimateRepository;
            //CriteriaRepository repcrit = MetaData.DataManager.Instance.CriteriaRepository;

            #region Формирование списка необходимых переменных и границ временного периода

            int[] variables; // gage avg, min, max, precipitetion
            DateTime[] dateSF;
            int[] decadeOfYearSF; // Нужно для осреднения декадного климата в пределах месяца
            List<VariableCode> varcodes = Meta.DataManager.GetInstance().VariableCodeRepository.Select((int)EnumVariable.IcePhenom);
            switch (ret.ReportTimeUnit)
            {
                case EnumTime.Month:
                    variables = new int[] {
                        (int)EnumVariable.GageHeightAvgMonth,
                        (int)EnumVariable.GageHeightMinMonth,
                        (int)EnumVariable.GageHeightMaxMonth,
                        (int)EnumVariable.PrecipMonth,
                        (int)EnumVariable.GageHeightF,
                        (int)EnumVariable.PrecipDay24F,
                        (int)EnumVariable.IcePhenom,
                        (int)EnumVariable.GageHeightAvgDay};
                    dateSF = DateTimeProcess.GetMonthDateSF(year, month);
                    decadeOfYearSF = new int[] {
                        DateTimeProcess.GetDecadeYearByDecadeMonth(month, 1),
                        DateTimeProcess.GetDecadeYearByDecadeMonth(month, 3) };
                    break;
                case EnumTime.DecadeOfYear:
                    variables = new int[] {
                        (int)EnumVariable.GageHeightAvgDecade,
                        (int)EnumVariable.GageHeightMinDecade,
                        (int)EnumVariable.GageHeightMaxDecade,
                        (int)EnumVariable.PrecipDecade,
                        (int)EnumVariable.GageHeightF,
                        (int)EnumVariable.PrecipDay24F,
                        (int)EnumVariable.IcePhenom,
                        (int)EnumVariable.GageHeightAvgDay};
                    dateSF = DateTimeProcess.GetMonthDecadeDateSF(year, month, (int)monthDecade);
                    decadeOfYearSF = new int[] {
                        DateTimeProcess.GetDecadeYearByDecadeMonth(month, (int)monthDecade),
                        DateTimeProcess.GetDecadeYearByDecadeMonth(month, (int)monthDecade) };
                    break;
                default:
                    throw new Exception("Неизвестный тип временного интервала для выполнения алгоритма отчёта Ф-50.");
            }

            #endregion Формирование списка необходимых переменных

            #region Формирование списка постов

            List<int[]> groupItems = Meta.DataManager.GetInstance().EntityGroupRepository.SelectEntities(siteGroupId);
            List<SiteGeoObject> sgos =
                 Meta.DataManager.GetInstance().SiteGeoObjectRepository.SelectBySites(groupItems.Select(x => x[0]).ToList());

            ret.AddRange(SiteGeoObjectRepository.ToDictionarySiteGeoobs(sgos));

            #endregion Формирование списка постов

            // Все данные для всех постов за временной период отчёта
            List<DataValue> dvAll = repdv.SelectA(dateSF[0], dateSF[1], true,
                ret.GetSiteIdList(), new List<int>(variables),
                new List<int> { offsetTypeId }, offsetValue,
                true, false, null, null, flagAQC);
            List<Catalog> dvCatalogs = Meta.DataManager.GetInstance().CatalogRepository.Select(
                dvAll.Select(x => x.CatalogId).Distinct().ToList());

            #region Обработка данных
            foreach (F50Row row in ret)
            {
                #region ВЫБОРКА КЛИМАТА УРОВНЯ, ПОЙМЫ

                // КЛИМАТ

                List<Climate> climate = repclm.SelectClimateMetaAndData(
                    new List<int>(new int[] { row.Site.Id }), new List<int>(new int[] { (int)EnumVariable.GageHeightAvgDecade }), offsetTypeId, offsetValue,
                    (int)EnumDataType.Average, (int)EnumTime.DecadeOfYear, ret.YearSClimate, ret.YearFClimate);

                if (climate.Count == 1)
                {
                    double[] avg = climate[0].Avg(decadeOfYearSF[0], decadeOfYearSF[1]);
                    row.GageClm = avg == null ? null : (double?)avg[0];
                }
                else if (climate.Count > 1)
                    throw new Exception("(climate.Count > 0)");

                // ПОЙМА, НЯ, ОЯ
                climate = repclm.SelectClimateMetaAndData(
                    new List<int>(new int[] { row.Site.Id }), new List<int>(new int[] { (int)EnumVariable.GageHeightF }),
                    offsetTypeId, offsetValue,
                    null, (int)EnumTime.YearCommon, ret.YearSClimate, ret.YearFClimate);

                List<Climate> climate1 = climate.FindAll(x => x.DataTypeId == (int)EnumDataType.Poyma);
                if (climate1.Count == 1)
                {
                    row.GagePoyma = climate1[0].Data[1];
                }
                else if (climate1.Count > 1)
                    throw new Exception("(climate1.Count > 0)");

                climate1 = climate.FindAll(x => x.DataTypeId == (int)EnumDataType.NYa);
                if (climate1.Count == 1)
                {
                    row.GageNya = climate1[0].Data[1];
                }
                else if (climate1.Count > 1)
                    throw new Exception("(climate1.Count > 0)");

                climate1 = climate.FindAll(x => x.DataTypeId == (int)EnumDataType.OYa);
                if (climate1.Count == 1)
                {
                    row.GageOya = climate1[0].Data[1];
                }
                else if (climate1.Count > 1)
                    throw new Exception("(climate1.Count > 0)");

                #endregion КЛИМАТ

                #region УРОВЕНЬ

                // Все суточные данные за период - по ним проводится агрегация "на лету", если нет данных за декаду, месяц.
                List<DataValue> dvGageHeightDay = GetDataValues(dvAll, dvCatalogs, row.Site.Id, (int)EnumVariable.GageHeightAvgDay);

                // Все исходные данные за период (по ним определяем периоды выходов на пойму и могут пригодиться, если нет агрегированных).
                List<DataValue> dvGageHeightF = GetDataValues(dvAll, dvCatalogs, row.Site.Id, (int)EnumVariable.GageHeightF);

                // СРЕДНИЕ ВЕЛИЧИНЫ
                List<DataValue> dv = GetDataValues(dvAll, dvCatalogs, row.Site.Id, variables[0]);
                if (dv.Count > 1) throw new Exception("Ошибка алгоритма 1: (dv.Count > 1)");
                if (dv.Count > 0)
                    row.GageAvg = dv[0].Value;
                else
                // Если нет среднего - выбираем из данных
                {
                    dv = (dvGageHeightDay.Count > 0) ? dvGageHeightDay : (dvGageHeightF.Count > 0) ? dvGageHeightF : null;
                    if (dv != null)
                    {
                        row.GageAvg = dv.Select(pet => pet.Value).Average();
                        row.GageAvgFlag = EnumDataFlag.DerivedFromIni;
                    }
                }

                // МИНИМАЛЬНЫЕ ВЕЛИЧИНЫ
                dv = GetDataValues(dvAll, dvCatalogs, row.Site.Id, variables[1]);
                if (dv.Count > 1) throw new Exception("Ошибка алгоритма 2: (dv.Count > 1)");
                if (dv.Count() > 0)
                    row.GageMin = dv.ElementAt(0).Value;
                else
                // Если нет абс. минимумов - выбираем из наблюдённых или ср. суточных
                {
                    dv = (dvGageHeightF.Count > 0) ? dvGageHeightF : (dvGageHeightDay.Count > 0) ? dvGageHeightDay : null;
                    if (dv != null)
                    {
                        row.GageMin = dv.Select(pet => pet.Value).Min();
                        row.GageMinFlag = EnumDataFlag.DerivedFromIni;
                    }
                }
                // Даты минимумов
                foreach (var item in dvGageHeightF)
                {
                    if (item.Value == row.GageMin)
                        row.DatesGageMin.Add(item.DateLOC);
                }

                // МАКСИМАЛЬНЫЕ ВЕЛИЧИНЫ
                dv = GetDataValues(dvAll, dvCatalogs, row.Site.Id, variables[2]);
                if (dv.Count > 1) throw new Exception("Ошибка алгоритма 2: (dv.Count > 1)");
                if (dv.Count > 0)
                    row.GageMax = dv[0].Value;
                else
                // Если нет абс. минимумов - выбираем из данных
                {
                    dv = (dvGageHeightF.Count > 0) ? dvGageHeightF : (dvGageHeightDay.Count > 0) ? dvGageHeightDay : null;
                    if (dv != null)
                    {
                        row.GageMax = dv.Select(pet => pet.Value).Max();
                        row.GageMaxFlag = EnumDataFlag.DerivedFromIni;
                    }
                }
                // Даты максимумов
                foreach (var item in dvGageHeightF)
                {
                    if (item.Value == row.GageMax)
                        row.DatesGageMax.Add(item.DateLOC);
                }

                // КОЛ СУТОК
                row.CountDays = dvGageHeightDay.Count();
                //dvAll.Where(
                //x => x.SiteId == row.Site.SiteId && x.VariableId == (int)EnumVariable.GageHeightAvgDay).Count();
                #endregion УРОВЕНЬ

                #region ВЫХОД НА ПОЙМУ
                List<DateTime?[]> datePoyma = new List<DateTime?[]>();
                if (row.GagePoyma.HasValue && dvGageHeightF.Count > 0)
                {
                    row.DatesGagePoyma = dvGageHeightF.Where(x => x.Value >= row.GagePoyma).Select(x => x.DateLOC).OrderBy(x => x).ToList();
                }
                #endregion ВЫХОД НА ПОЙМУ

                #region ОСАДКИ
                dv = GetDataValues(dvAll, dvCatalogs, row.Site.Id, variables[3]);
                if (dv.Count > 1) throw new Exception("Ошибка алгоритма 3: (dv.Count > 1)");
                if (dv.Count > 0)
                    row.Precipitation = dv[0].Value;
                else
                // Если нет суммы - выбираем из исх. данных
                {
                    dv = GetDataValues(dvAll, dvCatalogs, row.Site.Id, (int)EnumVariable.PrecipDay24F);
                    if (dv.Count > 0)
                    {
                        row.Precipitation = dv.Where(x => x.Value != 990).Select(pet => pet.Value).Sum();
                        row.PrecipitationFlag = EnumDataFlag.DerivedFromIni;
                    }
                }
                #endregion ОСАДКИ

                #region ЛЕДОВЫЕ ЯВЛЕНИЯ

                Dictionary<DateTime, List<int>> phenoms = new Dictionary<DateTime, List<int>>();
                List<int> unqPhenoms = new List<int>();

                // Значения -> коды явлений за даты
                foreach (var item in GetDataValues(dvAll, dvCatalogs, row.Site.Id, (int)EnumVariable.IcePhenom).OrderBy(x => x.DateLOC))
                {
                    List<int> ph = VariableCode.Parse(item.Value), ph1;
                    unqPhenoms.AddRange(ph.Where(x => !unqPhenoms.Exists(y => y == x)));

                    if (phenoms.TryGetValue(item.DateLOC, out ph1))
                        ph1.AddRange(ph);
                    else
                        phenoms.Add(item.DateLOC, ph);
                }

                // Коды явлений -> строка
                string notes = string.Empty;
                foreach (var item in unqPhenoms)
                {
                    // Интенсивность явления? КН-15
                    // Так как может быть несколько дней подряд с явлением разной интенсивности,
                    // то здесь интенсивность не обрабатываем. Пока не обрабатываем...
                    if (item < 11) continue;

                    List<DateTime> dates = new List<DateTime>();
                    foreach (var kvp in phenoms)
                    {
                        if (!kvp.Value.Exists(x => x == item)) continue;
                        if (!dates.Exists(x => x == kvp.Key))
                            dates.Add(kvp.Key);
                    }
                    dates.OrderBy(x => x);

                    VariableCode varCode = varcodes.Find(x => x.Code == item);

                    //notes += ((cat == null) ? item + "*" : cat.Description);
                    //notes += " (";
                    bool isDateSequence = false;
                    int i;

                    for (i = 0; i < dates.Count; i++)
                    {
                        if (i != 0)
                        {
                            if (dates[i].Date == dates[i - 1].Date) continue;

                            if (dates[i].Date == dates[i - 1].Date.AddDays(1))
                            {
                                isDateSequence = true;
                                continue;
                            }
                        }
                        //if (notes[notes.Length - 1] == '(')
                        if (notes.Length == 0 || notes[notes.Length - 1] == ';')
                            notes += dates[i].Day;
                        else if (isDateSequence)
                        {
                            notes += "-" + dates[i - 1].Day + "," + dates[i].Day;
                            isDateSequence = false;
                        }
                        else
                            notes += "," + dates[i].Day;
                    }

                    i = dates.Count - 1;
                    //if (notes[notes.Length - 1] == '(')
                    if (notes[notes.Length - 1] == ';')
                        notes += dates[i].Day;
                    else if (isDateSequence)
                    {
                        notes += "-" + dates[i].Day;
                    }

                    //notes += "); ";
                    notes += " " + ((varCode == null) ? item + "?явление?" : varCode.Description) + ";";
                }
                row.Notes = notes;

                #endregion ЛЕДОВЫЕ ЯВЛЕНИЯ
            }
            #endregion Обработка данных

            return ret;
        }
        /// <summary>
        /// Get data ordered by dateLOC.
        /// </summary>
        /// <param name="dvAll">Список всех значений.</param>
        /// <param name="dvCatalogs">Все каталоги в списке значений.</param>
        /// <param name="siteId">Сайт, для которого отбираются значения.</param>
        /// <param name="variableId">Параметр, значения которого отбираются.</param>
        /// <returns></returns>
        private static List<DataValue> GetDataValues(List<DataValue> dvAll, List<Catalog> dvCatalogs, int siteId, int variableId)
        {
            List<Catalog> catalog = dvCatalogs.FindAll(x => x.SiteId == siteId && x.VariableId == variableId);
            if (catalog.Count > 1)
                throw new Exception("Ошибка алгоритма: (catalog.Count > 1); siteId=" + siteId + "; variableId=" + variableId);

            return (catalog.Count == 0) ? new List<DataValue>() : dvAll.Where(x => x.CatalogId == catalog[0].Id).OrderBy(x => x.DateLOC).ToList();

        }
        private List<int> GetSiteIdList()
        {
            return this.Select(x => x.Site.Id).ToList();
        }
        /// <summary>
        /// Учитывается (добавляется) только первый гео-объект (река).
        /// </summary>
        /// <param name="sites"></param>
        private void AddRange(Dictionary<Site, List<GeoObject>> sites)
        {
            foreach (var item in sites)
            {
                Add(new F50Row(item.Key, item.Value.Count > 0 ? item.Value[0] : null));
            }
        }

        public EnumTime ReportTimeUnit
        {
            get
            {
                return MonthDecade.HasValue ? EnumTime.DecadeOfYear : EnumTime.Month;
            }
        }
        public int Year { get; set; }
        public int Month { get; set; }
        int? _monthDecade = int.MinValue;
        public int? MonthDecade
        {
            get
            {
                if (_monthDecade == int.MinValue) throw new Exception("Декада месяца не проинициализирована.");
                return _monthDecade;
            }
            set
            {
                if (value.HasValue && (value < 1 || value > 3)) throw new Exception("Некорректная декада месяца:" + value);
                _monthDecade = value;
            }
        }
    }
    /// <summary>
    /// Названия столбцов таблицы Ф-50.
    /// </summary>
    public class F50RowHeader
    {
        public string WaterObject { get; set; }
        public string SiteName { get; set; }
        public string GageAvg { get; set; }
        public string GageMax { get; set; }
        public string GageMin { get; set; }
        public string GageClm { get; set; }
        public string GagePoyma { get; set; }
        public string GageNya { get; set; }
        public string GageOya { get; set; }
        public string GageAnomaly { get; set; }
        public string Precipitation { get; set; }
        public string DatesGageMax { get; set; }
        public string DatesGageMin { get; set; }
        public string DatesGagePoyma { get; set; }
        public string CountDays { get; set; }
        public string DataFlag { get; set; }
        public string Notes { get; set; }

        public F50RowHeader()
        {
            WaterObject = "Водный объект";
            SiteName = "Пункт";
            GageAvg = "Уровень средн.";
            GageMax = "Уровень макс.";
            GageMin = "Уровень мин.";
            GageClm = "Норма";
            GagePoyma = "Пойма";
            GageNya = "НЯ";
            GageOya = "ОЯ";
            GageAnomaly = "Аномалия уровня";
            Precipitation = "Осадки, мм";
            DatesGageMax = "Дата макс.";
            DatesGageMin = "Дата мин.";
            DatesGagePoyma = "Даты выхода воды на пойму";
            CountDays = "Дней";
            DataFlag = "ДФ";
            Notes = "Примечания";
        }
        //double? _gageMin = null;
        //public double? GageMin { get { return _gageMin; } set { _gageMin = value == null ? value : Math.Round((double)value); } }
        //public EnumDataFlag GageMinFlag { get; set; }
        //double? _gageMax = null;
        //public double? GageMax { get { return _gageMax; } set { _gageMax = value == null ? value : Math.Round((double)value); } }
        //public EnumDataFlag GageMaxFlag { get; set; }
        //double? _gageClm = null;
        //public double? GageClm { get { return _gageClm; } set { _gageClm = value == null ? value : Math.Round((double)value); } }
        //double? _precipitation = null;
        //public double? Precipitation { get { return _precipitation; } set { _precipitation = value == null ? value : Math.Round((double)value, 1); } }
        //public EnumDataFlag PrecipitationFlag { get; set; }

        //public string SiteName { get { return Site.Name; } }
        //public string WaterObjectName { get { return (WaterObject == null) ? null : WaterObject.Name; } }
        //public double? GageAnomaly { get { return GageAvg == null || GageClm == null ? null : (double?)Math.Round((double)GageAvg - (double)GageClm); } }

        ///// <summary>
        ///// Кол. суток с данными
        ///// </summary>
        //public int CountDays { get; set; }

        //public List<DateTime> DatesGageMax { get; set; }
        //public List<DateTime> DatesGageMin { get; set; }
        //public List<DateTime> DatesGagePoyma { get; set; }

        //public F50Row(Site site, WaterObject wo)
        //{
        //    Site = site;
        //    WaterObject = wo;
        //    Notes = String.Empty;

        //    GageAvg = GageMin = GageMax = GageClm = null;
        //    GageAvgFlag = GageMinFlag = GageMaxFlag = PrecipitationFlag = EnumDataFlag.AggregatedPeriod;

        //    CountDays = 0;

        //    DatesGageMax = new List<DateTime>();
        //    DatesGageMin = new List<DateTime>();
        //    DatesGagePoyma = new List<DateTime>();
        //}

        //public double? GagePoyma { get; set; }
        //public string DataFlag
        //{
        //    get
        //    {
        //        string ret;
        //        ret =
        //        (
        //            (GageAvgFlag != EnumDataFlag.AggregatedPeriod
        //            || GageMinFlag != EnumDataFlag.AggregatedPeriod
        //            || GageMaxFlag != EnumDataFlag.AggregatedPeriod) ? "У" : "")

        //            + ((PrecipitationFlag != EnumDataFlag.AggregatedPeriod ? "O" : "")
        //        );
        //        return ret;
        //    }
        //}
        //public string DatesGageMaxString
        //{
        //    get
        //    {
        //        return DateTimeProcess.ToStringDayly(DatesGageMax);
        //    }
        //}
        //public string DatesGageMinString
        //{
        //    get
        //    {
        //        return DateTimeProcess.ToStringDayly(DatesGageMin);
        //    }
        //}
        //public string DatesGagePoymaString
        //{
        //    get
        //    {
        //        return DateTimeProcess.ToStringDayly(DatesGagePoyma);
        //    }
        //}

        //public double? GageNya { get; set; }

        //public double? GageOya { get; set; }
    }
    /// <summary>
    /// Строка таблицы Ф-50.
    /// </summary>
    public class F50Row
    {
        public static string[] header = new string[] { "названия" };
        public Site Site { get; set; }
        public GeoObject GeoObject { get; set; }
        public string Notes { get; set; }

        double? _gageAvg = null;
        public double? GageAvg { get { return _gageAvg; } set { _gageAvg = value == null ? value : Math.Round((double)value); } }
        public EnumDataFlag GageAvgFlag { get; set; }
        double? _gageMin = null;
        public double? GageMin { get { return _gageMin; } set { _gageMin = value == null ? value : Math.Round((double)value); } }
        public EnumDataFlag GageMinFlag { get; set; }
        double? _gageMax = null;
        public double? GageMax { get { return _gageMax; } set { _gageMax = value == null ? value : Math.Round((double)value); } }
        public EnumDataFlag GageMaxFlag { get; set; }
        double? _gageClm = null;
        public double? GageClm { get { return _gageClm; } set { _gageClm = value == null ? value : Math.Round((double)value); } }
        double? _precipitation = null;
        public double? Precipitation { get { return _precipitation; } set { _precipitation = value == null ? value : Math.Round((double)value, 1); } }
        public EnumDataFlag PrecipitationFlag { get; set; }

        public string SiteName
        {
            get
            {
                //DicItem dicStation = MetaDicCash.GetDicItem(typeof(Station), this.Site.StationId);
                //DicItem dicSiteType = MetaDicCash.GetDicItem(typeof(SiteType), this.Site.SiteTypeId);

                return Site.GetName(2, SiteTypeRepository.GetCash());
            }
        }
        public string WaterObjectName { get { return (GeoObject == null) ? null : GeoObject.Name; } }
        public double? GageAnomaly { get { return GageAvg == null || GageClm == null ? null : (double?)Math.Round((double)GageAvg - (double)GageClm); } }

        /// <summary>
        /// Кол. суток с данными
        /// </summary>
        public int CountDays { get; set; }

        public List<DateTime> DatesGageMax { get; set; }
        public List<DateTime> DatesGageMin { get; set; }
        public List<DateTime> DatesGagePoyma { get; set; }

        public F50Row(Site site, GeoObject wo)
        {
            Site = site;
            GeoObject = wo;
            Notes = String.Empty;

            GageAvg = GageMin = GageMax = GageClm = null;
            GageAvgFlag = GageMinFlag = GageMaxFlag = PrecipitationFlag = EnumDataFlag.AggregatedPeriod;

            CountDays = 0;

            DatesGageMax = new List<DateTime>();
            DatesGageMin = new List<DateTime>();
            DatesGagePoyma = new List<DateTime>();
        }

        public double? GagePoyma { get; set; }
        public string DataFlag
        {
            get
            {
                string ret;
                ret =
                (
                    (GageAvgFlag != EnumDataFlag.AggregatedPeriod
                    || GageMinFlag != EnumDataFlag.AggregatedPeriod
                    || GageMaxFlag != EnumDataFlag.AggregatedPeriod) ? "У" : "")

                    + ((PrecipitationFlag != EnumDataFlag.AggregatedPeriod ? "O" : "")
                );
                return ret;
            }
        }
        public string DatesGageMaxString
        {
            get
            {
                return DateTimeProcess.ToStringDayly(DatesGageMax);
            }
        }
        public string DatesGageMinString
        {
            get
            {
                return DateTimeProcess.ToStringDayly(DatesGageMin);
            }
        }
        public string DatesGagePoymaString
        {
            get
            {
                return DateTimeProcess.ToStringDayly(DatesGagePoyma);
            }
        }

        public double? GageNya { get; set; }

        public double? GageOya { get; set; }
    }
}
