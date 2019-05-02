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
    public class F51Report : List<F51Value>
    {
        private F51Report(int year, int month, int yearSClimate = 1900, int yearFClimate = 2000)
        {
            Year = year;
            Month = month;
            YearSClimate = yearSClimate;
            YearFClimate = yearFClimate;
        }

        public int Year { get; set; }
        public int Month { get; set; }
        public int YearSClimate { get; set; }
        public int YearFClimate { get; set; }
        static public F51Report Instance(F51Collection f51Coll)
        {
            F51Report ret = new F51Report(f51Coll.Year, f51Coll.Month, f51Coll.YearSClimate, f51Coll.YearFClimate);
            foreach (var f51Row in f51Coll)
            {
                int countDayInMonth = DateTime.DaysInMonth(f51Coll.Year, f51Coll.Month);
                for (int day = 1; day <= countDayInMonth; day++)
                {
                    DataValue dv = f51Row.DataValues[day - 1];
                    double? value = dv != null ? dv.Value : (double?)null;
                    int dec = (day >= 1 && day <= 10) ? 1 : (day >= 11 && day <= 20) ? 2 : 3;
                    DataValue dvD = f51Row.DataValues[30 + dec];
                    double? valueDec = dvD != null ? dvD.Value : (double?)null;
                    DataValue dvM = f51Row.DataValues[34];
                    double? valueM = dvM != null ? dvM.Value : (double?)null;
                    ret.Add(new F51Value(f51Row.Site.Code, f51Row.Site.GetName(0, SiteTypeRepository.GetCash()), f51Row.Notes, day, value, valueDec, valueM)
                    );
                }
            }
            return ret;
        }
    }

    public class F51Value
    {
        public string SiteCode { get; set; }
        public string SiteName { get; set; }
        public string Notes { get; set; }

        public int Day { get; set; }
        public int Decade { get; set; }
        private double _value;
        public double? Value
        {
            get { return double.IsNaN(_value) ? (double?)null : _value; }
            set { _value = (value != null) ? (double)value : double.NaN; }
        }
        private double _valueDecade;
        public double? ValueDecade
        {
            get { return double.IsNaN(_valueDecade) ? (double?)null : _valueDecade; }
            set { _valueDecade = (value != null) ? (double)value : double.NaN; }
        }
        private double _valueMonth;
        public double? ValueMonth
        {
            get { return double.IsNaN(_valueMonth) ? (double?)null : _valueMonth; }
            set { _valueMonth = (value != null) ? (double)value : double.NaN; }
        }
        public F51Value(string siteCode, string siteName, string notes, int day, double? value, double? valueDecade, double? valueMonth)
        {
            this.SiteCode = siteCode;
            this.SiteName = siteName;
            this.Notes = notes;
            this.Day = day;
            this.Value = value;
            this.ValueDecade = valueDecade;
            this.ValueMonth = valueMonth;
            this.Decade = (day >= 1 && day <= 10) ? 1 : (day >= 11 && day <= 20) ? 2 : 3;
        }
    }


    /// <summary>
    /// Подготовка данных для отчёта "Журнал осадков (месяц)" (Ф-51)
    /// </summary>
    public class F51Collection : List<F51Row>
    {
        private F51Collection(int year, int month, int yearSClimate = 1900, int yearFClimate = 2000)
        {
            Year = year;
            Month = month;
            YearSClimate = yearSClimate;
            YearFClimate = yearFClimate;
        }

        public int Year { get; set; }
        public int Month { get; set; }
        public int YearSClimate { get; set; }
        public int YearFClimate { get; set; }
        /// <summary>
        /// 
        /// Подготовка данных для отчёта "Журнал осадков (месяц)" (Ф-51)
        /// - считывание данных из БД;
        /// - расчёт необходимых параметров;
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="siteGroupId">Набор пунктов для формирования по ним отчёта.</param>
        /// <returns>Набор строк таблицы журнала осадков.</returns>
        static public F51Collection Instance(int siteGroupId, int year, int month, byte? flagAQC = null, int sourceId = 0)
        {
            // MISCEL

            F51Collection ret = new F51Collection(year, month);

            int offsetTypeId = 0;
            double offsetValue = 0;
            List<int> variables = new List<int>()
            {
                (int)EnumVariable.PrecipDay24F,
                (int)EnumVariable.PrecipDecade,
                (int)EnumVariable.PrecipMonth
            };

            // DATE PERIOD

            DateTime[] dateSF = DateTimeProcess.GetMonthDateSF(year, month);
            int monthDaysQ = DateTime.DaysInMonth(year, month);

            // SITES
            List<int[]> sg = Meta.DataManager.GetInstance().EntityGroupRepository.SelectEntities(siteGroupId);
            List<Site> sites = Meta.DataManager.GetInstance().SiteRepository.Select(sg.Select(x => x[0]).ToList());
            ret.AddSites(sites);

            // ДАННЫЕ: суточные, декадные и месячные осадки для всех постов за временной период отчёта

            List<DataValue> dvAll = Data.DataManager.GetInstance().DataValueRepository.SelectA(
                dateSF[0], dateSF[1], true,
                sites.Select(x => x.Id).ToList(), variables,
                new List<int> { offsetTypeId }, offsetValue, true, false,
                null, null, null);
            List<Catalog> catalogs = Meta.DataManager.GetInstance().CatalogRepository.Select(dvAll.Select(x => x.CatalogId).Distinct().ToList());

            // LOOP SITES, CREATE DATA STRUCTURE

            foreach (var row in ret)
            {
                // DAY
                IEnumerable<Catalog> catalog = catalogs.Where(x => x.SiteId == row.Site.Id && x.VariableId == (int)EnumVariable.PrecipDay24F);
                List<DataValue> dvSelected = dvAll.FindAll(x => catalog.Where(y => y.Id == x.Id).Count() > 0);
                foreach (DataValue dv in dvSelected)
                {
                    int i = F51Row.GetDataValuesIndex(EnumTime.Day, dv.DateLOC.Day);
                    if (row.DataValues[i] != null)
                        throw new Exception("Ошибка в алгоритме для суточных осадков.");

                    row.DataValues[i] = dv;
                }

                // DECADE
                catalog = catalogs.Where(x => x.SiteId == row.Site.Id && x.VariableId == (int)EnumVariable.PrecipDecade);
                dvSelected = dvAll.FindAll(x => catalog.Where(y => y.Id == x.Id).Count() > 0);
                foreach (DataValue dv in dvSelected)
                {
                    int decade = dv.DateLOC.Day == 1 ? 1 : dv.DateLOC.Day == 11 ? 2 : dv.DateLOC.Day == 21 ? 3 : -1;
                    int i = F51Row.GetDataValuesIndex(EnumTime.DecadeOfYear, decade);
                    if (row.DataValues[i] != null)
                        throw new Exception("Ошибка в алгоритме для декадных осадков.");

                    row.DataValues[i] = dv;
                }

                //MONTH
                catalog = catalogs.Where(x => x.SiteId == row.Site.Id && x.VariableId == (int)EnumVariable.PrecipMonth);
                dvSelected = dvAll.FindAll(x => catalog.Where(y => y.Id == x.Id).Count() > 0);

                foreach (DataValue dv in dvSelected)
                {
                    int i = F51Row.GetDataValuesIndex(EnumTime.Month, 0);
                    if (row.DataValues[i] != null)
                        throw new Exception("Ошибка в алгоритме для месячных осадков.");

                    row.DataValues[i] = dvSelected[0];
                }

            }
            return ret;
        }
        private void AddSites(List<Site> sites)
        {
            foreach (var site in sites)
            {
                Add(new F51Row(site));
            }
        }
    }

    /// <summary>
    /// Строка таблицы Ф-51.
    /// </summary>
    public class F51Row
    {
        /// <summary>
        /// Пункт
        /// </summary>
        public Site Site { get; set; }
        /// <summary>
        /// Примечания
        /// </summary>
        public string Notes { get; set; }
        /// <summary>
        /// 31+3+1 (сутки, декады, месяц)
        /// </summary>
        public List<DataValue> DataValues { get; set; }

        /// <summary>
        /// Позиция элемента данных заданного типа (time) в массиве DataValues.
        /// В массиве последовательно хранятся суточные, декадные и месячные данные.
        /// </summary>
        /// <param name="time">Тип временного интервала данных в массиве.</param>
        /// <param name="timeNum">Номер временного интервала указанного типа. Указывается декада года,
        /// но подразумевается - месяца.</param>
        /// <returns></returns>
        static public int GetDataValuesIndex(EnumTime time, int timeNum)
        {
            switch (time)
            {
                case EnumTime.Day:
                    return timeNum - 1;
                case EnumTime.DecadeOfYear: // декада месяца!
                    return 31 + timeNum - 1;
                case EnumTime.Month:
                    return 31 + 3;
                default:
                    return -1;
            }
        }

        public F51Row(Site site)
        {
            Site = site;
            Notes = String.Empty;

            DataValues = new List<DataValue>(new DataValue[31 + 3 + 1]);
        }
    }
}
