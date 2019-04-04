using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Amur.Meta
{
    public class _DELME_SiteGroup : EntityGroup
    {
        /// <summary>
        /// Сайты группы.
        /// null - не инициализирован.
        /// Count = 0 - нет сайтов в группе.
        /// </summary>
        public List<Site> SiteList { get; set; }
        /// <summary>
        /// Станции, которым принадлежат сайты. Станции не повторяются.
        /// 
        /// null - не инициализирован.
        /// </summary>
        public List<_DELME_Station> StationList { get; set; }
        private static string tabName = "site";
        public _DELME_SiteGroup(int id, int orderBy, string name)
            : base(id, name, tabName)
        {
            SiteList = null;
            StationList = null;
        }

        public _DELME_SiteGroup(EntityGroup item)
            : base(item.Id, item.Name, item.TabName)
        {
        }

        public static List<Common.DicItem> ToListDicItem(List<_DELME_SiteGroup> items)
        {
            List<Common.DicItem> ret = new List<Common.DicItem>();
            ////foreach (var item in items)
            ////{
            ////    ret.Add(ToDicItem(item));
            ////}
            return ret;
        }
        public override string ToString()
        {
            return Name + " (" + Id + ")";
        }
    }
}
