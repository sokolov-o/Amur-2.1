using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Common;

namespace SOV.Social
{
    public class AddrType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameShort { get; set; }

        public static List<Common.DicItem> ToList<T1>(List<AddrType> items)
        {
            List<Common.DicItem> ret = new List<Common.DicItem>();
            if (items != null)
                foreach (var item in items)
                {
                    ret.Add(new Common.DicItem(item.Id, item.Name));
                }
            return ret;
        }

    }
}
