using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace SOV.Amur.Meta
{
    /// <summary>
    /// Тип отношения между пунктами.
    /// </summary>
    [DataContract]
    public class SiteXSite
    {
        [DataMember]
        public int SiteId1 { get; set; }
        [DataMember]
        public int SiteId2 { get; set; }
        /// <summary>
        /// Тип отношения первого пункта ко второму.
        /// </summary>
        [DataMember]
        public int RelationTypeId { get; set; }
    }
}
