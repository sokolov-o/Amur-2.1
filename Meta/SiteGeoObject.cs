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

        public SiteGeoObject(int siteId, int geoObjectId, int orderBy)
        {
            SiteId = siteId;
            GeoObjectId = geoObjectId;
            OrderBy = orderBy;
        }
    }
}
