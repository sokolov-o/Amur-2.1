using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERHRI.Amur.DataP
{
    /// <summary>
    /// Агрегация данных: осреднение, суммирование и др.
    /// </summary>
    public class Aggregate
    {
        /// <summary>
        /// Суточная агрегация.
        /// </summary>
        /// <param name="dataTypeAggr">Тип агрегации. Реализовано суммирование и осреднение.</param>
        /// <param name="di">Массив значений для агрегации.</param>
        /// <param name="dateS">Дата-сутки начала агрегирования.</param>
        /// /// <param name="dateА">Дата-сутки окончания агрегирования.</param>
        /// /// <param name="dateS">Дата-сутки окончания агрегирования.</param>
        /// <param name="daySShift">Начало суток: сдвиг от суток агрегирования.</param>
        /// <param name="hourS">Час начала суток.</param>
        /// <param name="hourSIncluded">Час начала суток всключён в сутки агрегирования?</param>
        /// <param name="dayFShift">Окончание суток: сдвиг от суток агрегирования.</param>
        /// <param name="hourF">Час окончания суток.</param>
        /// <param name="hourFIncluded">Час окончания суток всключён в сутки агрегирования?</param>
        /// <returns>Список агрегированных значений. Для случая отсутствия значений в сутках, значению присваивается double.NaN.</returns>
        static public List<DataItem> Dayly(Meta.EnumDataType dataTypeAggr, List<DataItem> dis, DateTime dateS, DateTime dateF, int daySShift, int hourS, bool hourSIncluded, int dayFShift, int hourF, bool hourFIncluded)
        {
            List<DataItem> ret = new List<DataItem>();

            // Расчётные сутки
            DateTime curDay = new DateTime(dateS.Year, dateS.Month, dateS.Day);
            // Начало расчётных суток
            DateTime date0 = curDay.AddDays(daySShift);
            date0 = new DateTime(date0.Year, date0.Month, hourS);
            date0 = (hourSIncluded) ? date0.AddSeconds(-1) : date0.AddSeconds(+1);
            // Окончание расчётных суток
            DateTime date1 = curDay.AddDays(dayFShift);
            date1 = new DateTime(date0.Year, date0.Month, hourF);
            date1 = (hourFIncluded) ? date1.AddSeconds(+1) : date1.AddSeconds(-1);

            // ЦИКЛ по расчётным суткам
            while (curDay <= dateF)
            {
                DataItem di = new DataItem() { Id=-1, Date = DateTime.FromBinary(curDay.ToBinary()), Value = 0, DataItemsRelated = new List<DataItem>() };
                
                // ЦИКЛ по значениям, относящимся к расчётным суткам
                foreach (var item in dis.Where(x => x.Date >= date0 && x.Date <= date1))
                {
                    di.Value += item.Value;
                    di.DataItemsRelated.Add(item);
                }
                
                // Коррекция расчётного значения
                if (di.DataItemsRelated.Count == 0)
                {
                    di.Value = double.NaN;
                }
                else
                {
                    if (dataTypeAggr == Meta.EnumDataType.Cumulative)
                        di.Value /= di.DataItemsRelated.Count;
                }
                
                // Переход на следующие сутки
                curDay = curDay.AddDays(1);
                date0 = date0.AddDays(1);
                date1 = date1.AddDays(1);
            }
            return ret;
        }
    }
}
