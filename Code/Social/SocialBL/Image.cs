using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using FERHRI.Common;

namespace FERHRI.Social
{
    [DataContract]
    public class Image
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public byte[] Img { get; set; }
    }
}
