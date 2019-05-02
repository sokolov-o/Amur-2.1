using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Common
{
    /// <summary>
    /// Avg & Sum
    /// </summary>
    public class StatAS
    {
        public double Avg { get; set; }
        public double Sum { get; set; }

        public StatAS()
        {
            Avg = double.NaN;
            Sum = double.NaN;
        }
    }
    /// <summary>
    /// Avg & Sum & Min & Max
    /// </summary>
    public class StatASMM : StatAS
    {
        public double Min { get; set; }
        public double Max { get; set; }

        public StatASMM()
        {
            Min = double.NaN;
            Max = double.NaN;
        }
    }
    /// <summary>
    /// Param Avg & Sum
    /// </summary>
    public class ParamStatAS : StatAS
    {
        public IdNameRus Param { get; set; }
    }
}
