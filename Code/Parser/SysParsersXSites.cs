using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Amur.Parser
{
    public class SysParsersXSites
    {
        public int Id { get; set; }

        public int SysObjId { get; set; }
        public int SysParsersParamsSetId { get; set; }
        public bool isActual { get; set; }

        public int SiteId { get; set; }
        public int SiteTypeId { get; set; }
        public string SiteCode { get; set; }
        public string SiteName { get; set; }
        public string ExtSiteId { get; set; }
        
    }
}
