using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FERHRI.Common;
using System.Runtime.Serialization;

namespace FERHRI.Amur.Meta
{
    [DataContract]
    public class MethodClimate
    {
        [DataMember]
        public int MethodId { get; set; }
        [DataMember]
        public int YearS { get; set; }
        [DataMember]
        public int YearF { get; set; }

        override public string ToString()
        {
            return YearS + "-" + YearF;
        }
    }
}
