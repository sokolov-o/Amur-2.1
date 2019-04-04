using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FERHRI.Amur.Meta
{
    [DataContract]
    public class ValueType : FERHRI.Common.DicItem
    {
        [DataMember]
        public string NameEng { get { return NameShort; } set { NameShort = value; } }

        public ValueType(int id, string name, string nameEng, string description = null)
            : base(id, name, nameEng, description)
        {
        }
        public static List<Common.DicItem> ToList<T1>(List<ValueType> coll)
        {
            List<Common.DicItem> ret = new List<Common.DicItem>();
            foreach (var item in coll)
            {
                ret.Add(new Common.DicItem(item.Id, item.Name, item.NameShort));
            }
            return ret;
        }
    }
}
