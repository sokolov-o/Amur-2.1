using System.Collections.Generic;
using System.Runtime.Serialization;
using SOV.Common;

namespace SOV.Social
{
    [DataContract]
    public class Org 
    {
        [DataMember]
        public int LegalEntityId { get; set; }
        /// <summary>
        /// Первое лицо организации
        /// </summary>
        [DataMember]
        public int? StaffIdFirstFace { get; set; }
    }
}
