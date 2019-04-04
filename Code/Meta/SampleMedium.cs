using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Amur.Meta
{
    [DataContract]
    public class SampleMedium: SOV.Common.DicItem
    {
        public SampleMedium(int id, string name, string description = null)
            : base(id, name, null, description)
        {
        }

        public static List<Common.DicItem> ToList<T1>(List<SampleMedium> coll)
        {
            List<Common.DicItem> ret = new List<Common.DicItem>();
            foreach (var item in coll)
            {
                ret.Add((Common.DicItem)item);
            }
            return ret;
        }
    }
}
