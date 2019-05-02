using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Common;
using System.Runtime.Serialization;

namespace SOV.Amur.Meta
{
    [DataContract]
    public class SiteNew
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int? ParentId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public int SiteTypeId { get; set; }
        [DataMember]
        public int? OrgId { get; set; }
        [DataMember]
        public int? AddrRegionId { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public double? Lat { get; set; }
        [DataMember]
        public double? Lon { get; set; }


        public override string ToString() { return ToStringCodeRight(); }
        public string ToStringCodeRight() { return Code + " " + Name; }
        public string ToStringCodeLeft() { return Name + " " + Code; }
    }
}
