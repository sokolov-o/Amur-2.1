using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Amur.Data;
using SOV.Amur.Meta;

namespace SOV.Amur.DataP
{
    public class Forecast
    {
        /// <summary>
        /// Агрегация за сутки.
        /// </summary>
        /// <param name="sitesId">Список пунктов.</param>
        /// <param name="varIni">Исходная переменная для агрегации.</param>
        /// <param name="methodId">Метод исходной переменной.</param>
        /// <param name="sourceId">Источник исходной переменной.</param>
        /// <param name="offsetTypeId">Тип смещения исходной переменной.</param>
        /// <param name="offsetValue">Смещение исходной переменной.</param>
        /// <param name="dateDayS">Начало периода агрегации.</param>
        /// <param name="dateDayF">Окончание периода агрегации.</param>
        /// <param name="siteUTCHourDayStart">Начало суток пункта во времени UTC.</param>
        /// <param name="siteUTCOffset">Пояс.</param>
        /// <param name="aggrDataTypeId">Тип агрегации переменной.</param>
        /// <returns></returns>
        public static Dictionary<DateTime, List<DataDV>> AggregateDay(List<int> sitesId, Variable varIni, int methodId, int sourceId, int offsetTypeId, double offsetValue,
            DateTime dateDayS, DateTime dateDayF, Dictionary<int, int> siteUTCHourDayStart, Dictionary<int, double> siteUTCOffset, int aggrDataTypeId)
        {
            // TEST: секунда
            if (varIni.TimeId != 100 && varIni.TimeSupport != 0)
                throw new Exception(string.Format("Не мгновенная (не секунда) переменная {0}", varIni.NameRus));
            // TEST: забл прогноза в часах
            MethodForecast methodFcs = Meta.DataManager.GetInstance().MethodForecastRepository.Select(methodId);
            //if (methodFcs.UnitFcsTime.Id != (int)Meta.EnumTime.Hour)
            //    throw new Exception("Заблаговременность прогноза указана не в часах " + methodFcs.UnitFcsTime.Id);

            #region READ DATA

            DateTime dateDaySRead = dateDayS.AddDays(-1), dateDayFRead = dateDayF.AddDays(+1);

            List<DataForecast> dfList; // catalogId = data_forecast_0_id = -siteId
            // Агрегация дефицита влажности?
            if (varIni.VariableTypeId == (int)EnumVariableType.VaporPressureDeficit)
            {
                // Рассчитать дефицит по данным Ta, Td из БД
                dfList = CalcVaporPressureDeficit(sitesId, methodId, sourceId, offsetTypeId, offsetValue, dateDaySRead, dateDayFRead);
            }
            else
            {
                Data.DataManager dmd = Data.DataManager.GetInstance();
                Meta.DataManager dmm = Meta.DataManager.GetInstance();
                // Получить записи каталогов для переменной и пунктов
                List<Catalog> catalogs = dmm.CatalogRepository.Select(
                    sitesId, new List<int> { varIni.Id },
                    new List<int> { methodId }, new List<int> { sourceId },
                    new List<int> { offsetTypeId }, offsetValue);
                if (catalogs.Count == 0)
                    throw new Exception("Не найдены записи каталога для прогностической переменной variableId = " + varIni.Id);
                // Выбрать прогнозы за период для записей кталога
                dfList = dmd.DataForecastRepository.SelectDataForecasts(catalogs.Select(x => x.Id).ToList(), dateDaySRead, dateDayFRead, null, true);
                foreach (var item in dfList)
                {
                    item.CatalogId = -catalogs.Find(x => x.Id == item.CatalogId).SiteId;
                }
            }
            #endregion READ DATA

            // LOOP BY DAY'S DATE
            Dictionary<DateTime, List<DataDV>> ret = new Dictionary<DateTime, List<DataDV>>();
            for (DateTime date = dateDayS.Date; date <= dateDayF; date = date.AddDays(1))
            {
                // LOOP SITES
                for (int iSite = 0; iSite < sitesId.Count; iSite++)
                {
                    double utcOffset = siteUTCOffset[sitesId[iSite]];
                    int utcHourDayStart = siteUTCHourDayStart[sitesId[iSite]];

                    DateTime dateSDayUTC = utcHourDayStart < 0 ? date.AddDays(-1).AddHours(-utcHourDayStart) : date.AddDays(0).AddHours(utcHourDayStart);

                    IEnumerable<DataForecast> dfsite = dfList.Where(x => x.CatalogId == -sitesId[iSite]);
                    if (dfsite.Count() == 0) continue;
                    //if (!methodFcs.IsDateFcsUTC)
                    //{
                    //    foreach (var item in dfsite)
                    //    {
                    //        item.DateFcs = item.DateFcs.AddHours(-utcOffset);
                    //    }
                    //}

                    foreach (var dateIni in dfsite.Select(x => x.DateFcs.AddHours(-x.LagFcs)).Distinct().OrderBy(x => x))
                    {
                        if (dateSDayUTC <= dateIni) continue;

                        IEnumerable<DataForecast> dfDay = dfsite.Where(x => x.DateFcs > dateSDayUTC && x.DateFcs <= dateSDayUTC.AddDays(1) && x.DateFcs.AddHours(-x.LagFcs) == dateIni);
                        if (dfDay.Count() >= methodFcs.MaxPerDayCount)
                        {
                            List<DataDV> adv = new List<DataDV>();
                            List<double> dayValues = dfDay.Select(x => x.Value).ToList();
                            adv.Add(new DataDV()
                            {
                                Date = DateTime.FromBinary(date.ToBinary()),
                                Value = aggrDataTypeId == 1 ? dayValues.Average() : dayValues.Sum(),
                                Param = new object[] { sitesId[iSite], dayValues.Count()/*Count*/, dfDay.Min(x => x.LagFcs)/*min fcsLag-hour*/, dfDay.Max(x => x.LagFcs)/*max fcsLag-hour*/ }
                            });

                            if (adv.Count > 0)
                            {
                                List<DataDV> dv = null;
                                if (ret.TryGetValue(dateIni, out dv))
                                    dv.AddRange(adv);
                                else
                                    ret.Add(dateIni, adv);
                            }
                        }
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// Рассчитать дефицит влажности по наблюдённым синхронным срочным значениям температуры воздуха и точки росы 
        /// для набора станций за период времени.
        /// </summary>
        /// <param name="sitesId">Набор станций</param>
        /// <param name="dateSLOC">Начало периода</param>
        /// <param name="dateFLOC">Окончание периода</param>
        /// <returns>Набор значений дефицита влажности у которых id=-1, catalogId=-siteId.</returns>
        public static List<DataForecast> CalcVaporPressureDeficit(List<int> sitesId, int methodId, int sourceId, int offsetId, double offsetValue, DateTime dateSFcs, DateTime dateFFcs)
        {
            Data.DataManager dmd = Data.DataManager.GetInstance();
            Meta.DataManager dmm = Meta.DataManager.GetInstance();

            // Выбрать переменные Ta и Td
            Variable varTa = dmm.VariableRepository.Select(
                (int)EnumVariableType.Temperature, (int)EnumTime.Second,
                (int)EnumUnit.DegreeCelsius, (int)EnumDataType.Continuous,
                (int)EnumGeneralCategory.Meteo, (int)EnumSampleMedium.Air,
                0, (int)EnumValueType.Forecast
            );
            Variable varTd = dmm.VariableRepository.Select(
                (int)EnumVariableType.TemperatureDewPoint, (int)EnumTime.Second,
                (int)EnumUnit.DegreeCelsius, (int)EnumDataType.Continuous,
                (int)EnumGeneralCategory.Meteo, (int)EnumSampleMedium.Air,
                0, (int)EnumValueType.Forecast
            );
            if (varTa == null || varTd == null)
                throw new Exception("Не найдены переменные для прогностических Ta и Td, которые необходимы для расчёта дефицита влажности.");

            // Получить записи каталогов для переменных Ta и Td
            List<Catalog> ctlTa = dmm.CatalogRepository.Select(
                sitesId, new List<int> { varTa.Id },
                new List<int> { methodId }, new List<int> { sourceId },
                new List<int> { offsetId }, offsetValue);
            List<Catalog> ctlTd = dmm.CatalogRepository.Select(
                sitesId, new List<int> { varTd.Id },
                new List<int> { methodId }, new List<int> { sourceId },
                new List<int> { offsetId }, offsetValue);
            if (((object)ctlTa) == (object)null || ((object)ctlTd) == (object)null)
                throw new Exception("Не найдены записи каталога для прогностических Ta и Td, которые необходимы для расчёта дефицита влажности.");

            List<DataForecast> ret = new List<DataForecast>();
            foreach (var catalogTa in ctlTa)
            {
                Catalog catalogTd = ctlTd.FirstOrDefault(x => x.SiteId == catalogTa.SiteId);
                if (((object)catalogTd) == (object)null) continue;

                // Выбрать прогнозы Ta, Td
                List<DataForecast2> df2 = Data.DataManager.GetInstance().DataForecastRepository.SelectDataForecasts2(catalogTa.Id, catalogTd.Id, dateSFcs, dateFFcs, null, true);

                // Расчитать дефицит по исходным прогнозам Ta и Td
                foreach (var item in df2)
                {
                    ret.Add(new DataForecast(-catalogTa.SiteId,
                        item.LagFcs, item.DateFcs, item.DateIni,
                        Common.Phisics.GetSaturatedVapourPressureDeficit(item.Value1, item.Value2), DateTime.Now)
                    );
                }
            }
            return ret;
        }
    }
}
