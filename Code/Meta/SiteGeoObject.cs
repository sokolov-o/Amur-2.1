using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Amur.Meta
{
    public class SiteGeoObject
    {
        public int SiteId { get; set; }
        public int GeoObjectId { get; set; }
        public int OrderBy { get; set; }

        public SiteGeoObject(int stationId, int geoObjectId, int order)
        {
            SiteId = stationId;
            GeoObjectId = geoObjectId;
            OrderBy = order;
        }
    }
}
