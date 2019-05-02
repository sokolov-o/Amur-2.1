using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using SOV.Common;

namespace SOV.Amur.Meta
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
                DicItem di = new Common.DicItem(item.Id, item.Name, item.NameShort);
                di.Entity = item;
                ret.Add(di);
            }
            return ret;
        }
    }
}
