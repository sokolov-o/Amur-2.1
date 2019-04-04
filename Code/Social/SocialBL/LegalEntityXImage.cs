using System.Runtime.Serialization;

namespace SOV.Social
{
    [DataContract]
    public class LegalEntityXImage
    {
        [DataMember]
        public int OrgId { get; set; }
        [DataMember]
        public int ImageId { get; set; }
    }
}
