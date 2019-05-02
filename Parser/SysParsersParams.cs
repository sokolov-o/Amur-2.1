using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Amur.Parser
{
    public class SysParsersParams
    {
        public int CodeFormId { get; set; }

        public string VariableName { get; set; }
        public int SysParsersParamsSetId { get; set; }
        public int IntVariableId { get; set; }
        public int IntOffsetId { get; set; }
        public int OffsetUnitsId { get; set; }
        public string OffsetDescription { get; set; }

        public string ExtParam { get; set; }
        public int ExtLevelId { get; set; }
        
        public double Multiplier { get; set; }
        public double VarErrorDataValue { get; set; }
        public double VarNoDataValue { get; set; }
        
    }
}
