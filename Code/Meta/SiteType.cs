using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using FERHRI.Common;

namespace FERHRI.Amur.Meta
{
    [DataContract]
    public class SiteType : IdName
    {
        [DataMember]
        public string NameShort { get; set; }

        public SiteType(int id, string name, string nameShort)
        {
            Id = id;
            Name = name;
            NameShort = nameShort;
        }

        public int GetId()
        {
            return Id;
        }

        public static List<Common.DicItem> ToList<T1>(List<SiteType> coll)
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
