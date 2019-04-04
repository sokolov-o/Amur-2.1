using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERHRI.Amur.Meta
{
    public class StationAddrRegion
    {
        public int StationId { get; set; }
        public int AddrRegionId { get; set; }

        public StationAddrRegion(int stationId, int addrRegionId)
        {
            StationId = stationId;
            AddrRegionId = addrRegionId;
        }
    }
}
