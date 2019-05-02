using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Amur.Meta
{
    [DataContract]
    public class DataType : SOV.Common.DicItem
    {
        public DataType(int id, string name, string nameShort, string description = null)
            : base(id, name, nameShort, description)
        {
        }
        public static List<Common.DicItem> ToList<T1>(List<DataType> coll)
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
