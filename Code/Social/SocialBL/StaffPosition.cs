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
    public class StaffPosition : IdNames
    {
        public StaffPosition(IdNames idNames) : base(idNames) {}
        public StaffPosition() : base() { }
    }
}
