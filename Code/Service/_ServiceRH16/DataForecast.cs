using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FERHRI.Amur.ServiceRH16
{
    public class DataForecast : DataValue
    {
        /// <summary>
        /// Минимальная заблаговременность прогноза на дату (час).
        /// например, для агрегированных прогностических значений.
        /// </summary>
        public double MinLagFcs { get; set; }
        /// <summary>
        /// Минимальная заблаговременность прогноза на дату (час).
        /// например, для агрегированных прогностических значений.
        /// </summary>
        public double MaxLagFcs { get; set; }
    }
}