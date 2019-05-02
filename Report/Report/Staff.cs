using System;
using System.Runtime.Serialization;

namespace FERHRI.Amur.Report
{
    /// <summary>
    ///  Сотрудники
    /// </summary>
    [DataContract]
    public class Staff
    {
        public Staff(int id, int personId, int personPosId, int orgId)
        {
            this.id = id;
            this.personId = personId;
            this.personPosId = personPosId;
            this.orgId = orgId;
        }
        /// <summary>
        /// Идентифкиатор
        /// </summary>
        [DataMember]
        public int id { get; set; }
        /// <summary>
        /// Идентификатор человека в таблице report.person
        /// </summary>
        [DataMember]
        public int personId { get; set; }
        /// <summary>
        /// Идентификатор должности в таблице report.person_pos
        /// </summary>
        [DataMember]
        public int personPosId { get; set; }
        /// <summary>
        /// Идентификатор организации в таблице report.organisation
        /// </summary>
        [DataMember]
        public int orgId { get; set; }
    }
}
