using System;
using System.Collections.Generic;
using System.Linq;
using TestService.AmurServiceRWReference;

namespace MyNamespace
{
    public class StationExt : Station
    {
        // Коды типов атрибутов пункта.
        // Для разных баз данных могут различаться.
        // Указаны коды БД Амур.ДВНИГМИ
        public static int _LAT_SITEATTR_TYPE_ID = 1000;
        public static int _LON_SITEATTR_TYPE_ID = 1001;
        public static int _UTCOFFSET_SITEATTR_TYPE_ID = 1003;

        StationExt(Station station)
            : base(station)
        {
        }

        public List<Site> Sites { get; set; }
        public List<EntityAttrValue> SiteAttributes { get; set; }

        public Site MainSite
        {
            get
            {
                return Sites.FirstOrDefault(x => x.SiteTypeId == this.TypeId);
            }
        }
        string GetMainStationAttribute(int attrId)
        {
            Site site = MainSite;
            if (site != null)
                return GetSiteAttribute(site.Id, attrId);
            return null;
        }
        public string GetSiteAttribute(int siteId, int attrId)
        {
            EntityAttrValue attr = SiteAttributes.FirstOrDefault(x => x.EntityId == siteId && x.AttrTypeId == attrId);
            if (attr != null)
                return attr.Value;
            return null;
        }
        public double? Latitude
        {
            get
            {
                string ret = GetMainStationAttribute(_LAT_SITEATTR_TYPE_ID);
                if (ret != null)
                    return double.Parse(ret);
                return null;
            }
        }
        public double? Longitude
        {
            get
            {
                string ret = GetMainStationAttribute(_LON_SITEATTR_TYPE_ID);
                if (ret != null)
                    return double.Parse(ret);
                return null;
            }
        }
        public double? UTCOffset
        {
            get
            {
                string ret = GetMainStationAttribute(_UTCOFFSET_SITEATTR_TYPE_ID);
                if (ret != null)
                    return double.Parse(ret);
                return null;
            }
        }

        static public StationExt GetStation(ServiceClient svc, long hSvc, string stationIndex, DateTime siteAttrDateActual)
        {
            Station station = svc.GetStationByIndex(hSvc, stationIndex);
            if (station == null) return null;

            StationExt ret = new StationExt(svc.GetStationByIndex(hSvc, stationIndex));

            Site[] sites = svc.GetSitesByStation(hSvc, ret.Id, null);
            if (sites == null || sites.Length == 0)
                ret.Sites = new List<Site>();
            else
                ret.Sites = sites.ToList();

            for (int i = 0; i < ret.Sites.Count; i++)
            {
                EntityAttrValue[] siteAttributes = svc.GetSitesAttrValues(hSvc, ret.Sites.Select(x => x.Id).ToArray(), null, siteAttrDateActual);
                if (siteAttributes == null || siteAttributes.Length == 0)
                    ret.SiteAttributes = new List<EntityAttrValue>();
                else
                    ret.SiteAttributes = siteAttributes.ToList();
            }
            return ret;
        }
    }
}
namespace TestService.AmurServiceRWReference
{
    public partial class Station
    {
        public Station() { }
        public Station(Station station)
        {
            Id = station.Id;
            Code = station.Code;
            Name = station.Name;
            NameEng = station.NameEng;
            TypeId = station.TypeId;
            AddrRegionId = station.AddrRegionId;
            OrgId = station.OrgId;
        }
    }
}