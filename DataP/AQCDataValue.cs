using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Amur.DataP
{
    /// <summary>
    /// Правила AQC для значения.
    /// </summary>
    [DataContract]
    public class AQCDataValue
    {
        [DataMember]
        public long DataValueId { get; set; }
        [DataMember]
        public int AQCRoleId { get; set; }
        [DataMember]
        public bool IsAQCApplied { get; set; }
    }
}
