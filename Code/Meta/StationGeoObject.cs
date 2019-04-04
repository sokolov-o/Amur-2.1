using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Amur.Meta
{
    public class StationGeoObject
    {
        public int StationId { get; set; }
        public int GeoObjectId { get; set; }
        public int Order { get; set; }

        public StationGeoObject(int stationId, int geoObjectId, int order)
        {
            StationId = stationId;
            GeoObjectId = geoObjectId;
            Order = order;
        }
    }
}
