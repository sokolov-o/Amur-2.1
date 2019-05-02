using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SOV.Common;

namespace SOV.Amur.Meta
{
    public static class SiteGroupControls
    {
        internal static List<SiteGroupComboBox> SiteGroupComboBoxList = new List<SiteGroupComboBox>();

        public static void GlobalReloadItems()
        {
            foreach (var item in SiteGroupComboBoxList)
                item.Fill();
        }
    }

    public class SiteGroupComboBox : ToolStripComboBox
    {
        public SiteGroupComboBox() : base()
        {
            SiteGroupControls.SiteGroupComboBoxList.Add(this);
        }
        const string TAB_NAME = "site";
        List<EntityGroup> _specGroups = new List<EntityGroup>
        {
            new EntityGroup(0,"(Все пункты)",TAB_NAME),
            new EntityGroup(-((int)Meta.EnumStationType.HydroPost),"(Все гидрологические посты)",TAB_NAME),
            new EntityGroup(-((int)Meta.EnumStationType.MeteoStation),"(Все метеорологические станции)",TAB_NAME),
            new EntityGroup(-((int)Meta.EnumStationType.GeoObject),"(Все гео-объекты)",TAB_NAME),
            new EntityGroup(-((int)Meta.EnumStationType.MorePost),"(Все морские посты)",TAB_NAME)
        };

        public bool AllowSpecialGroups { get; set; }

        public void Fill()
        {
            Items.Clear();
            List<EntityGroup> groups = Meta.DataManager.GetInstance().EntityGroupRepository.SelectByEntityTableName(TAB_NAME);
            if (AllowSpecialGroups)
                groups.AddRange(_specGroups);
            Items.AddRange(groups.OrderBy(x => x.Name).ToArray());
        }

        protected override void Dispose(bool disposing)
        {
            SiteGroupControls.SiteGroupComboBoxList.Remove(this);
            base.Dispose(disposing);
        }

        public List<Site> GetGroupSites()
        {
            EntityGroup siteGroup = this.SiteGroup;
            if (siteGroup != null)
                return GetGroupSites(siteGroup.Id);
            return new List<Site>();
        }
        static public List<Site> GetGroupSites(int? siteGroupId)
        {
            if (siteGroupId.HasValue)
            {
                if (siteGroupId == 0)
                    return Meta.SiteRepository.GetCash();
                else if (siteGroupId < 0)
                    return Meta.SiteRepository.GetCash().Where(x => x.TypeId == -siteGroupId).OrderBy(x => x.Name).ToList();
                else
                {
                    List<int[]> idOrder = Meta.DataManager.GetInstance().EntityGroupRepository.SelectEntities((int)siteGroupId);
                    List<Site> sites = DataManager.GetInstance().SiteRepository.Select(idOrder.Select(x => x[0]).ToList());
                    sites = sites.OrderBy(x => idOrder.First(y => y[0] == x.Id)[1]).ToList();
                    return sites;
                }
            }
            return new List<Site>();
        }
        public void SetSiteGroup(int? id)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (((EntityGroup)Items[i]).Id == id)
                {
                    SelectedIndex = i;
                    return;
                }
            }
            SelectedIndex = -1;
        }
        public EntityGroup SiteGroup
        {
            get { return SelectedItem == null ? null : (EntityGroup)SelectedItem; }
            set
            {
                SelectedIndex = (value == null) ? -1 : Items.IndexOf(value);
            }
        }
    }
}
