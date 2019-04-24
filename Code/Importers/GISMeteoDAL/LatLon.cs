using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.GISMeteo
{
    internal struct LatLon
    {
        public Int32 coord;

        internal LatLon(Int32 coord)
        {
            this.coord = coord;
        }
        public short Lon
        {
            get
            {
                return (short)Convert.ToDecimal((coord >> 16) / 60.0);
            }
        }
        public Decimal Lat
        {
            get
            {
                return (short)Convert.ToDecimal((coord & 0xffff) / 60.0);
            }
        }
    }
}
