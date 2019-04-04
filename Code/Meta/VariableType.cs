using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Amur.Meta
{
    [DataContract]
    public class VariableType : SOV.Common.DicItem
    {
        [DataMember]
        public string NameEng { get; set; }

        public VariableType(int id, string name, string nameShort, string nameEng, string description = null)
            : base(id, name, nameShort, description)
        {
            NameEng = nameEng;
        }
        public static List<Common.DicItem> ToList<T1>(List<VariableType> coll)
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
