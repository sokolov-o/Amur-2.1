using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Common;
using System.Runtime.Serialization;

namespace SOV.Amur.Meta
{
    [DataContract]
    public class OffsetType : IdName
    {
        [DataMember]
        public int UnitId { get; set; }

        public OffsetType(int id, string name, int unitId)
        {
            Id = id;
            Name = name;
            UnitId = unitId;
        }

        public static List<Common.DicItem> ToList<T1>(List<OffsetType> items)
        {
            List<DicItem> ret = new List<DicItem>();
            foreach (var item in items)
            {
                ret.Add(new DicItem(item.Id, item.Name));
            }
            return ret;
        }
        override public string ToString()
        {
            return Name + "/" + Id;
        }

    }
}
