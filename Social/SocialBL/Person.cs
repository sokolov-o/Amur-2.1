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
    public class Person
    {
        [DataMember]
        public int LegalEntityId { get; set; }
        [DataMember]
        public char? Sex { get; set; }

        public static List<DicItem> SexTypes = new List<DicItem>()
        {
            new DicItem(0, "Муж.", 'm'), new DicItem(1, "Жен.", 'f')
        };
    }
}
