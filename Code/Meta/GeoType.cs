using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Amur.Meta
{
    [DataContract]
    public class GeoType
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string NameEng { get; set; }

        public GeoType(int id, string name, string nameEng, string description)
        {
            Id = id;
            Name = name;
            Description = description;
            NameEng = nameEng;
        }

        public static Common.DicItem ToDicItem(GeoType item)
        {
            return new Common.DicItem(item.Id, item.Name, item.NameEng, item.Description);
        }
        public static List<Common.DicItem> ToList<T1>(List<GeoType> coll)
        {
            List<Common.DicItem> ret = new List<Common.DicItem>();
            if (coll != null)
                foreach (var item in coll)
                {
                    ret.Add(ToDicItem(item));
                }
            return ret;
        }
    }
}
