using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.GISMeteo
{
    public class StationGM
    {
        public int StationIndex { get; set; }

        //public FERHRI.Amur.Meta.Site Site { get; set; }
        public int SiteIdAmur { get; set; }

        public string Name { get; set; }

        public short Lon { get; set; }

        public short Lat { get; set; }

        public int Height { get; set; }

        public string Sign { get; set; }

        public float? UTCOffset { get; set; }

        override public string ToString()
        {
            return StationIndex + " " + Name;
        }
    }
}
