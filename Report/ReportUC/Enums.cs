using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Amur.Reports
{
    /// <summary>
    /// Использовать в отчётах для указания на каких данных был создан отчёт или форма.
    /// </summary>
    public enum EnumDataFlag
    {
        /// <summary>
        /// Данные агрегированных значений из БД.
        /// </summary>
        AggregatedPeriod = 0,
        /// <summary>
        /// В БД нет агрегации значения.
        /// Данные рассчитаны (агрегированны) из БД по исходным, наблюдённым значениям "на лету".
        /// </summary>
        DerivedFromIni = 1
    }
}
