using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace FERHRI.Amur.ServiceRH16
{
    public class DataValue
    {
        /// <summary>
        /// Код станции.
        /// </summary>
        [DefaultValue(-1)]
        public int StationId { get; set; }
        /// <summary>
        /// Код переменной.
        /// </summary>
        [DefaultValue(-1)]
        public int VariableId { get; set; }
        /// <summary>
        /// Дата наблюдения или дата прогноза
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Значение
        /// </summary>
        [DefaultValue(double.NaN)]
        public double Value { get; set; }
        /// <summary>
        /// Для агрегированных значений - количество исходных значений в агрегации.
        /// -1, если не определено или не доступно.
        /// </summary>
        [DefaultValue(-1)]
        public int Count { get; set; }
    }
}