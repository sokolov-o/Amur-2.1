using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Amur.Meta
{
    [DataContract]
    public class MeteoZone
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int UTCHourDayStart { get; set; }

        public MeteoZone(int id, string name, int utcHourDayStart)
        {
            Id = id;
            Name = name;
            UTCHourDayStart = utcHourDayStart;
        }
    }
}
