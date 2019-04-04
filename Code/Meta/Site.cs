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
    public class Site : IdNameParent
    {
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public int TypeId { get; set; }
        [DataMember]
        public int? OrgId { get; set; }
        [DataMember]
        public int? AddrRegionId { get; set; }
        [DataMember]
        public string Description { get; set; }

        public override string ToString()
        {
            return Name + " " + Code + " (" + TypeId + ")";
        }
        /// <summary>
        /// Получить составное наименование пункта (с наименованием, индексом и типом пункта).
        /// </summary>
        /// <param name="site">Пункт.</param>
        /// <param name="codeSide">Расположение кода пункта: 0 - без кода, 1 - слева от наименования, 2 - справа</param>
        /// <param name="showTypeNameShort">Показывать краткое наименование типа пункта в его имени?</param>
        /// <returns></returns>
        static public string GetName(Site site, int codeSide, bool showTypeNameShort, List<SiteType> siteTypes)
        {
            string ret = site.Name + (showTypeNameShort ? " (" + siteTypes.First(x => x.Id == site.TypeId).NameShort + ")" : "");
            return (codeSide == 1 ? site.Code + " " + ret : codeSide == 2 ? ret + " " + site.Code : ret);
        }
        public string GetName(int codeSide, bool showTypeNameShort, List<SiteType> siteTypes)
        {
            return GetName(this, codeSide, showTypeNameShort, siteTypes);
        }
        public static List<DicItem> ToDicItemList(List<Site> sites, int codeSide, bool showTypeNameShort, List<SiteType> siteTypes, Common.DicItem parent = null)
        {
            List<DicItem> ret = null;
            if (sites != null)
            {
                ret = new List<DicItem>();
                foreach (var site in sites)
                {
                    ret.Add(site.ToDicItem(codeSide, showTypeNameShort, siteTypes, parent));
                }
            }
            return ret;
        }

        public DicItem ToDicItem(int codeSide, bool showTypeNameShort, List<SiteType> siteTypes, Common.DicItem parent = null)
        {
            DicItem ret = new DicItem(Id, GetName(this, codeSide, showTypeNameShort, siteTypes));
            ret.ParentDicItem = parent;
            ret.Entity = this;
            return ret;
        }
    }
}
