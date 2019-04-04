using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

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
        //[DataMember]
        //public int Id { get; set; }
        //[DataMember]
        //public string Name { get; set; }
        ///// <summary>
        ///// Типы сайтов (в строку через ,) для которых этот атрибут желателен.
        ///// </summary>
        //[DataMember]
        //public string MandatorySiteTypeIds { get; set; }

        //public SiteAttrType(int id, string name, string mandatorySiteTypeIds)
        //{
        //    Id = id;
        //    Name = name;
        //    MandatorySiteTypeIds = mandatorySiteTypeIds;
        //}

        //public static List<Common.DicItem> ToList<T1>(List<SiteAttrType> coll)
        //{
        //    List<Common.DicItem> ret = new List<Common.DicItem>();
        //    if (coll != null)
        //        foreach (var item in coll)
        //        {
        //            ret.Add(new Common.DicItem(item.Id, item.Name, item.MandatorySiteTypeIds));
        //        }
        //    return ret;
        //}
    }
}
