using System.Collections.Generic;
using System.Runtime.Serialization;
using SOV.Common;

namespace SOV.Social
{
    [DataContract]
    public class Org 
    {
        /// <summary>
        /// Код организации (legal_entity.id).
        /// </summary>
        [DataMember]
        public int Id { get; set; }
        /// <summary>
        /// Код первого лица организации (legal_entity.id).
        /// </summary>
        [DataMember]
        public int? StaffIdFirstFace { get; set; }

        //////// Ниже члены класса Филиппа... оказалось из Reports ((
        //////public int ReportId { get; set; }
        //////public int? ImageId { get; set; }
        //////public string Param { get; set; }
        //////public string OrgView { get; set; }
        //////public string ReportView { get; set; }
        //////public byte[] Image { get; set; }
    }
}
