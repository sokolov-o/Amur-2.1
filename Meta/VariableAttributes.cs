using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Amur.Meta
{
    /// <summary>
    /// Атрибуты переменной, использующиеся при её отображении и проч.
    /// </summary>
    public class VariableAttributes
    {
        public int VariableId { get; set; }
        public double AxisMin { get; set; }
        public double AxisMax { get; set; }
        public double AxisStep { get; set; }
        /// <summary>
        /// В качестве разделителя разрядов здесь используется точка.
        /// </summary>
        public string ValueFormat { get; set; }

        public double AcceptValueFormat(double data)
        {
            if (!string.IsNullOrEmpty(ValueFormat) && !double.IsNaN(data))
            {
                string sFormat = "{0:" + ValueFormat + "}";
                return double.Parse(string.Format(sFormat, data));
            }
            return data;
        }
    }
}
