using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FERHRI.Common;

namespace FERHRI.Amur.Meta
{
    public static class SiteGroupControls
    {
        internal static List<SiteGroupComboBox> SiteGroupComboBoxList = new List<SiteGroupComboBox>();

        public static void GlobalReloadItems()
        {
            foreach (var item in SiteGroupComboBoxList)
                item.ReloadItems();
        }
    }

    public class SiteGroupComboBox : ToolStripComboBox
    {
        public SiteGroupComboBox() : base()
        {
            ReloadItems();
            SiteGroupControls.SiteGroupComboBoxList.Add(this);
        }
        List<SiteGroup> _specGroups = new List<SiteGroup>
        {
            new SiteGroup(0,0,"(_Все пункты)"),
            new SiteGroup(-((int)Meta.EnumStationType.HydroPost),0,"(_Все гидрологические посты)"),
            new SiteGroup(-((int)Meta.EnumStationType.MeteoStation),0,"(_Все метеорологические станции)"),
            new SiteGroup(-((int)Meta.EnumStationType.GeoObject),0,"(_Все гео-объекты)"),
            new SiteGroup(-((int)Meta.EnumStationType.MorePost),0,"(_Все морские посты)")
        };
        public void ReloadItems()
        {
            Items.Clear();
            List<SiteGroup> groups = Meta.DataManager.GetInstance().SiteGroupRepository.SelectGroups();
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
            SiteGroup siteGroup = this.SiteGroup;
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
                    return Meta.SiteRepository.GetCash().Where(x => x.SiteTypeId == -siteGroupId).ToList();
                else
                    return Meta.DataManager.GetInstance().SiteGroupRepository.SelectGroupFK((int)siteGroupId).SiteList;
            }
            return new List<Site>();
        }
        public void SetSiteGroup(int? id)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (((SiteGroup)Items[i]).Id == id)
                {
                    SelectedIndex = i;
                    return;
                }
            }
            SelectedIndex = -1;
        }
        public SiteGroup SiteGroup
        {
            get { return SelectedItem == null ? null : (SiteGroup)SelectedItem; }
            set
            {
                SelectedIndex = (value == null) ? -1 : Items.IndexOf(value);
            }
        }
    }
}
