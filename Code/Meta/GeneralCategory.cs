using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FERHRI.Amur.Meta
{
    [DataContract]
    public class GeneralCategory: FERHRI.Common.DicItem
    {
        public GeneralCategory(int id, string name, string nameShort, string description = null)
            : base(id, name, nameShort, description)
        {
        }
        public static List<Common.DicItem> ToList<T1>(List<GeneralCategory> coll)
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
