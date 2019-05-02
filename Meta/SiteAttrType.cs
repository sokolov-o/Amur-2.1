using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using SOV.Amur.Meta;

namespace SOV.Amur.Meta
{
    [DataContract]
    public class SiteAttrType : EntityAttrType
    {
        public SiteAttrType(int id, string name, string mandatoryIds)
            : base(id, name, mandatoryIds)
        { }
        public SiteAttrType(int id, string name, List<int> mandatoryIds)
            : base(id, name, mandatoryIds)
        {
        }
        static public List<EntityAttrType> ToEntityAttrTypeList(List<SiteAttrType> sat)
        {
            return sat.Select(x => (EntityAttrType)x).ToList();
        }
    }
}
