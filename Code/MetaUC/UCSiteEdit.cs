using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SOV.Common;

namespace SOV.Amur.Meta
{
    public partial class UCSiteEdit : UserControl
    {
        public UCSiteEdit()
        {
            InitializeComponent();
            ucEntityAttrValues.EntityName = "site";
        }

        public System.EventHandler ShowDataValueEventHandler
        {
            set
            {
                ucCatalogs.ShowDataValueEventHandler = value;
            }
        }
        public void Fill(int siteId)
        {
            ucSite.Site = DataManager.GetInstance().SiteRepository.Select(siteId);
            ucStationSites.Fill(siteId);
            FillGO(siteId);
        }
        private void FillGO(int siteId)
        {
            geoObjectsListBox.Clear();
            List<SiteGeoObject> sgo = DataManager.GetInstance().SiteGeoObjectRepository.SelectBySites(new List<int>(new int[] { siteId }));
            if (sgo.Count > 0)
                geoObjectsListBox.SetDataSource(
                    Enumerable.ToList<object>(DataManager.GetInstance().GeoObjectRepository.Select(sgo.Select(x => x.GeoObjectId).ToList()))
                    , "Name");
        }
        private void ucStationSites_UCCurrentRowChangedEvent(Site site)
        {
            try
            {
                this.ucEntityAttrValues.Clear();
                ucCatalogs.Clear();

                if (site != null)
                {
                    this.ucEntityAttrValues.Fill("site", site.Id, site.TypeId);
                    ucSiteXSites1.Fill(1, site.Id);
                    ucSiteXSites2.Fill(2, site.Id);
                    ucCatalogs.Fill(new CatalogFilter(new List<int>() { site.Id }, null, null, null, null, null));
                    ucSiteInstruments.Fill(site.Id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void Clear()
        {
            ucSite.Site = null;
            ucStationSites.Clear();
            geoObjectsListBox.Clear();
        }
        public Site Site
        {
            get
            {
                return ucSite.Site;
            }
            set
            {
                ucSite.Site = value;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (ucSite.Site.Id > 0)
                    DataManager.GetInstance().SiteRepository.Update(ucSite.Site);
                else
                {
                    //ucStation.Station.Id = DataManager.GetInstance().StationRepository.Insert(ucStation.Station);
                    int Id = DataManager.GetInstance().SiteRepository.Insert(ucSite.Site);
                    ucSite.Site = DataManager.GetInstance().SiteRepository.Select(Id);
                }
                ucEntityAttrValues.EntityId = ucSite.Site.Id;
                ucStationSites.ParentSiteId= ucSite.Site.Id;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            RaiseStationSavedEvent(ucSite.Site);
            EnableSites = true;
        }

        public bool EnableSites
        {
            get
            {
                return splitContainer1.Enabled;
            }
            set
            {
                splitContainer1.Enabled = value;
            }
        }
        #region EVENTS
        public delegate void UCStationSavedEventHandler(Site site);
        public event UCStationSavedEventHandler UCStationSavedEvent;
        protected virtual void RaiseStationSavedEvent(Site site)
        {
            if (UCStationSavedEvent != null)
            {
                UCStationSavedEvent(site);
            }
        }
        #endregion


        private void geoObjectsListBox_UCAddNewEvent()
        {
            FormSelectListItems frm = new FormSelectListItems("Выберите объект, к которому привязана станция",
                GeoObjectRepository.GetCash().OrderBy(x => x.Name).ToArray(), "Name");
            frm.StartPosition = FormStartPosition.CenterParent;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                foreach (var geoObject in frm.SelectedItems)
                {
                    DataManager.GetInstance().SiteGeoObjectRepository.Insert(((GeoObject)geoObject).Id, ucSite.Site.Id);
                }
                FillGO(ucSite.Site.Id);
            }
        }

        private void geoObjectsListBox_UCDeleteEvent(int id)
        {
            DataManager.GetInstance().SiteGeoObjectRepository.Delete(new SiteGeoObject(ucSite.Site.Id, id, 0));
            FillGO(ucSite.Site.Id);
        }
    }
}
