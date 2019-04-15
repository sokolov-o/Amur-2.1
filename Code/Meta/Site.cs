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
        /// <param name="siteTypes">Показывать краткое наименование типа пункта в его имени, если не null.</param>
        /// <returns></returns>
        static public string GetName(Site site, int codeSide, List<SiteType> siteTypes = null)
        {
            string ret = site.Name + (siteTypes != null ? " (" + siteTypes.First(x => x.Id == site.TypeId).NameShort + ")" : "");
            return (codeSide == 1 ? site.Code + " " + ret : codeSide == 2 ? ret + " " + site.Code : ret);
        }
        /// <summary>
        /// Получить составное наименование пункта (с наименованием, индексом и типом пункта).
        /// </summary>
        /// <param name="codeSide">Расположение кода пункта: 0 - без кода, 1 - слева от наименования, 2 - справа</param>
        /// <param name="siteTypes">Показывать краткое наименование типа пункта в его имени, если не null.</param>
        /// <returns></returns>
        public string GetName(int codeSide, List<SiteType> siteTypes = null)
        {
            return GetName(this, codeSide, siteTypes);
        }
        public static List<DicItem> ToDicItemList(List<Site> sites, int codeSide, List<SiteType> siteTypes, Common.DicItem parent = null)
        {
            List<DicItem> ret = null;
            if (sites != null)
            {
                ret = new List<DicItem>();
                foreach (var site in sites)
                {
                    ret.Add(site.ToDicItem(codeSide, siteTypes, parent));
                }
            }
            return ret;
        }

        public DicItem ToDicItem(int codeSide, List<SiteType> siteTypes, Common.DicItem parent = null)
        {
            DicItem ret = new DicItem(Id, GetName(this, codeSide, siteTypes));
            ret.ParentDicItem = parent;
            ret.Entity = this;
            return ret;
        }
    }
}
