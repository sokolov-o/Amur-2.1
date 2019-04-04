using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Amur.Meta
{
    public class SiteAddrRegion
    {
        public int StationId { get; set; }
        public int AddrRegionId { get; set; }

        public SiteAddrRegion(int stationId, int addrRegionId)
        {
            StationId = stationId;
            AddrRegionId = addrRegionId;
        }
    }
}
