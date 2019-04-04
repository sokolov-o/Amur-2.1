using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using SOV.Common;

namespace SOV.Social
{
    [DataContract]
    public class Staff : DateSF
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public Division Division { get; set; }
        [DataMember]
        public StaffPosition StaffPosition { get; set; }
        [DataMember]
        public bool ReadyForInsert { get { return Division != null && StaffPosition != null; } }
        [DataMember]
        public string NameFull
        {
            get
            {
                return Division.ToString() + " (" + StaffPosition.ToString() + ")";
            }
        }
    }
}
