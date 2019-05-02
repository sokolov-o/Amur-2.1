using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Amur.Data;
using SOV.Amur.Meta;
using System.Diagnostics;

namespace SOV.Amur.DataP
{
    public class Observations
    {
        static string DATE_FORMAT = "yyyyMMdd HH:mm";

        /// <summary>
        /// Суточная агрегация данных срочных наблюдений.
        /// </summary>
        /// <param name="catalogFilter">Фильтр данных</param>
        /// <param name="dateDayS">Дата начала агрегации.</param>
        /// <param name="dateDayF">Дата окончания агрегации.</param>
        /// <param name="siteHourDayStartUTC">Сроки (часы, ВСВ) начала суток в пунктах.</param>
        /// <returns>
        /// Набор осреднённых за сутки значений, у которых 
        ///     Param[0] = (int)siteId - код пункта;
        ///     Param[1] = (int)count - количество исх. значений, участвующих в осреднении.
        /// </returns>
        public static List<DataDV> AggregateDay(List<int> sitesId, Variable var, int methodId, int sourceId, int offsetTypeId, double offsetValue,
            DateTime dateDayS, DateTime dateDayF, Dictionary<int, int> siteHourDayStartUTC, int aggrDataTypeId)
        {
            // TEST: секунда
            if (var.TimeId != 100 && var.TimeSupport != 0)
                throw new Exception(string.Format("Не мгновенная (не секунда) переменная {0}", var.NameRus));

            #region READ DATA

            Debug.WriteLine("Чтение данных за период {0} - {1} ...", dateDayS.ToString(DATE_FORMAT), dateDayF.ToString(DATE_FORMAT));

            DateTime dateDaySRead = dateDayS.AddDays(-1), dateDayFRead = dateDayF.AddDays(+1);

            List<DataValue> dvsReaded;
            // Агрегация дефицита влажности?
            if (var.VariableTypeId == (int)EnumVariableType.VaporPressureDeficit)
            {
                // Рассчитать дефицит по данным Ta, Td из БД
                dvsReaded = CalcVaporPressureDeficit(sitesId, dateDaySRead, dateDayFRead);
            }
            else
            {
                // Выбрать из БД
                dvsReaded = Data.DataManager.GetInstance().DataValueRepository.SelectA(
                    dateDaySRead, dateDayFRead, true,
                    sitesId, new List<int> { var.Id },
                    new List<int> { offsetTypeId }, offsetValue,
                    true, false,
                    new List<int> { methodId }, new List<int> { sourceId }, 1);
                // Темп и темп. точки росы со значениями 99.9 - удалить.
                if (var.VariableTypeId == 269 || var.VariableTypeId == 270 || var.VariableTypeId == 84)
                {
                    int q = dvsReaded.RemoveAll(x => x.Value > 99);
                }
                // Осадки
                if (var.VariableTypeId == (int)Meta.EnumVariableType.Precipitation)
                {
                    foreach (var item in dvsReaded.Where(x => x.Value == 990))
                    {
                        item.Value = 0;
                    }
                }
            }
            List<Catalog> catalogs = Meta.DataManager.GetInstance().CatalogRepository.Select(
                sitesId, new List<int> { var.Id },
                new List<int> { methodId }, new List<int> { sourceId },
                new List<int> { offsetTypeId }, offsetValue);

            #endregion READ DATA

            // LOOP BY DAY'S DATE
            List<DataDV> ret = new List<DataDV>();
            for (DateTime date = dateDayS.Date; date <= dateDayF; date = date.Date.AddDays(1))
            {
                Debug.Write("Агрегация за {0}", date.ToString(DATE_FORMAT));

                // LOOP SITES
                int countValuesAggr = 0;
                for (int iSite = 0; iSite < sitesId.Count; iSite++)
                {
                    Debug.WriteLine("  Код пункта {0} ({1} из {2})", sitesId[iSite], iSite + 1, sitesId.Count);

                    int utcHourDayStart;
                    if (!siteHourDayStartUTC.TryGetValue(sitesId[iSite], out utcHourDayStart))
                        throw new Exception("Отсутствует час начала суток (ВСВ) для пункта с кодом " + sitesId[iSite]);
                    DateTime dateUTCSDay = ((utcHourDayStart < 0) ? date.AddDays(-1).AddHours(Math.Abs(utcHourDayStart)) : date.AddHours(utcHourDayStart));
                    DateTime dateUTCFDay = dateUTCSDay.AddDays(1);
                    dateUTCSDay = dateUTCSDay.AddSeconds(1);

                    int id;
                    if (var.VariableTypeId == (int)EnumVariableType.VaporPressureDeficit)
                        id = -sitesId[iSite];
                    else
                    {
                        Catalog catalog = catalogs.FirstOrDefault(x => x.SiteId == sitesId[iSite] && x.VariableId == var.Id && x.OffsetTypeId == offsetTypeId && x.MethodId == methodId && x.SourceId == sourceId && x.OffsetValue == offsetValue);
                        if (((object)catalog) == (object)null) throw new Exception(string.Format("Отсутствует запись каталога данных для пункта с кодом {0} и переменной {1}", sitesId[iSite], var));
                        id = catalog.Id;
                    }
                    IEnumerable<DataValue> dvss = dvsReaded.Where(x =>
                        x.CatalogId == id
                        && x.DateUTC >= dateUTCSDay
                        && x.DateUTC <= dateUTCFDay
                        );
                    if (dvss.Count() == 0)
                    {
                        Debug.WriteLine(" - нет данных.");
                        continue;
                    }

                    List<DataDV> dv = DataDV.ToList(dvss, true); ;

                    ret.Add(new DataDV()
                    {
                        Date = date,
                        Value = aggrDataTypeId == 1 ? dv.Average(x => x.Value) : dv.Sum(x => x.Value),
                        Param = new object[] { sitesId[iSite], dv.Count() }
                    });
                    countValuesAggr++;

                    Debug.WriteLine(" - успешно. {0} значений в агрегациию", dv.Count());
                }
            }
            return ret;
        }
        /// <summary>
        /// Рассчитать дефицит влажности по наблюдённым синхронным срочным значениям температуры воздуха и точки росы 
        /// для набора станций за период времени."
        /// </summary>
        /// <param name="sitesId">Набор станций</param>
        /// <param name="dateSLOC">Начало периода</param>
        /// <param name="dateFLOC">Окончание периода</param>
        /// <returns>Набор значений дефицита влажности
        /// у которых id=-1, catalogId=-siteId.</returns>
        public static List<DataValue> CalcVaporPressureDeficit(List<int> sitesId, DateTime dateSLOC, DateTime dateFLOC)
        {
            Data.DataManager dmd = Data.DataManager.GetInstance();
            Meta.DataManager dmm = Meta.DataManager.GetInstance();

            // Выбрать переменные Ta и Td
            Variable varTa = dmm.VariableRepository.Select(
                (int)EnumVariableType.Temperature, (int)EnumTime.Second,
                (int)EnumUnit.DegreeCelsius, (int)EnumDataType.Continuous,
                (int)EnumGeneralCategory.Meteo, (int)EnumSampleMedium.Air,
                0, (int)EnumValueType.FieldObservation
            );
            Variable varTd = dmm.VariableRepository.Select(
                (int)EnumVariableType.TemperatureDewPoint, (int)EnumTime.Second,
                (int)EnumUnit.DegreeCelsius, (int)EnumDataType.Continuous,
                (int)EnumGeneralCategory.Meteo, (int)EnumSampleMedium.Air,
                0, (int)EnumValueType.FieldObservation
            );
            if (varTa == null || varTd == null)
                throw new Exception("Не найдены переменные для наблюдённых Ta и Td, которые необходимы для расчёта дефицита влажности.");

            // Получить записи каталогов для переменных Ta и Td
            List<Catalog> ctlTa = dmm.CatalogRepository.Select(
                sitesId, new List<int> { varTa.Id },
                new List<int> { (int)EnumMethod.ObservationInSitu }, new List<int> { 0/*sourceId*/},
            new List<int> { (int)EnumOffsetType.NoOffset }, 0/*offsetValue*/);
            List<Catalog> ctlTd = dmm.CatalogRepository.Select(
                sitesId, new List<int> { varTd.Id },
                 new List<int> { (int)EnumMethod.ObservationInSitu }, new List<int> { 0/*sourceId*/},
                 new List<int> { (int)EnumOffsetType.NoOffset }, 0/*offsetValue*/);
            if (((object)ctlTa) == (object)null || ((object)ctlTd) == (object)null)
                throw new Exception("Не найдены записи каталога для наблюдённых Ta и Td, которые необходимы для расчёта дефицита влажности.");

            List<DataValue> ret = new List<DataValue>();
            foreach (var catalogTa in ctlTa)
            {
                Catalog catalogTd = ctlTd.FirstOrDefault(x => x.SiteId == catalogTa.SiteId);
                if (((object)catalogTd) == (object)null) continue;

                List<DataValue2> dvTaTd = dmd.DataValueRepository.SelectDataValueB2(dateSLOC, dateFLOC, catalogTa.Id, catalogTd.Id, new short[] { 0, 1 });
                dvTaTd.RemoveAll(x => x.Value1 > 99 || x.Value2 > 99); // Ошибки в даннных

                foreach (var item in dvTaTd)
                {
                    ret.Add(new DataValue(-1, -catalogTa.SiteId,
                        Common.Phisics.GetSaturatedVapourPressureDeficit(item.Value1, item.Value2),
                        item.DateLOC, item.DateUTC, item.FlagAQC, (float)(item.DateLOC - item.DateUTC).TotalHours));
                }
            }
            return ret;
        }
        /// <summary>
        /// Повторяемость суток с туманом.
        /// </summary>
        /// <param name="sitesId"></param>
        /// <param name="dateSLOC">Первые сутки (время не учитывается)</param>
        /// <param name="dateFLOC">Последние сутки (время не учитывается)</param>
        /// <param name="varIdWW">Переменная Погода в срок</param>
        /// <param name="varIdW1W2">Переменная Погода прошедшая</param>
        /// <returns></returns>
        public static double[] CalcProbabFogDay(List<int> siteIds, DateTime dateSLOC, DateTime dateFLOC, int varIdWW = 51, int varIdW1W2 = 1057)
        {
            // GET CATALOG IDS for weather codes

            List<Catalog> ctls = Meta.DataManager.GetInstance().CatalogRepository.Select(
                siteIds, new List<int> { varIdWW, varIdW1W2 },
                new List<int> { (int)EnumMethod.ObservationInSitu }, null,
                new List<int> { (int)EnumOffsetType.NoOffset }, 0);
            if (ctls.Count > 2) throw new Exception("Algorithmic error (ctls.Count > 2). OSokolov@SOV.ru");
            if (ctls.Count < 2) return null;
            int ctlWW = ctls.Find(x => x.VariableId == varIdWW).Id;
            int ctlW1W2 = ctls.Find(x => x.VariableId == varIdW1W2).Id;

            // READ DATA
            List<DataValue> dvs = Data.DataManager.GetInstance().DataValueRepository.SelectA(
                dateSLOC,
                dateFLOC.Date.AddDays(1).AddMilliseconds(-1),
                ctls.Select(x => x.Id).ToList(),
                true);

            // CALC FOG PROBAB
            double[/*суток всего;суток с туманом;повторяемость*/] ret = new double[3];
            DateTime date = dateSLOC.Date;
            while (date <= dateFLOC)
            {
                ret[0]++;
                List<DataValue> dvs1 = dvs.FindAll(x => x.DateLOC.Date.Ticks == date.Ticks);
                if (dvs1.Count > 0)
                {
                    if (dvs1.Exists(x =>
                        (x.CatalogId == ctlWW && (x.Value == 11 || x.Value == 12 || x.Value == 28 || (x.Value >= 40 && x.Value <= 49)))
                        ||
                        (x.CatalogId == ctlW1W2 && x.Value == 4 && x.DateLOC.Hour != 0)
                        ))
                    {
                        ret[1]++;
                    }
                }
                date = date.AddDays(1);
            }
            if (ret[0] > 0) ret[2] = ret[1] / ret[0] * 100;
            return ret;
        }
    }
}
