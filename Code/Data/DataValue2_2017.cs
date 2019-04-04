using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Amur.Data
{
    public class DataValue2_2017
    {
        public int CatalogId1 { get; set; }
        public int CatalogId2 { get; set; }
        public double Value1 { get; set; }
        public double Value2 { get; set; }
        public int DateTypeId { get; set; }
        public DateTime Date { get; set; }
        public float UTCOffset1 { get; set; }
        public float UTCOffset2 { get; set; }
        public byte FlagAQC { get; set; }
    }
}
