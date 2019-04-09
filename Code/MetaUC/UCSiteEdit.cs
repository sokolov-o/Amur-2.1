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
            ucStation.Site = DataManager.GetInstance().SiteRepository.Select(siteId);
            ucStationSites.Fill(siteId);
            FillGO(siteId);
        }
        private void FillGO(int siteId)
        {
            geoObjectsListBox.Clear();
            Dictionary<Site, List<GeoObject>> sgo = DataManager.GetInstance().SiteGeoObjectRepository.SelectBySites (siteId);
            if (sgo.Count > 0)
                geoObjectsListBox.SetDataSource(sgo.ElementAt(0).Value.ToArray<object>().ToList(), "Name");
        }
        private void ucStationSites_UCCurrentRowChangedEvent(Site site)
        {
            try
            {
                this.ucEntityAttrValues.Clear();
                ucCatalogs.Clear();

                if (site != null)
                {
                    this.ucEntityAttrValues.Fill("site", site.Id, site.SiteTypeId);
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
            ucStation.Site = null;
            ucStationSites.Clear();
            geoObjectsListBox.Clear();
        }
        public Station Station
        {
            get
            {
                return ucStation.Site;
            }
            set
            {
                ucStation.Site = value;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (ucStation.Site.Id > 0)
                    DataManager.GetInstance().StationRepository.Update(ucStation.Site);
                else
                {
                    //ucStation.Station.Id = DataManager.GetInstance().StationRepository.Insert(ucStation.Station);
                    int Id = DataManager.GetInstance().StationRepository.Insert(ucStation.Site);
                    ucStation.Site = DataManager.GetInstance().StationRepository.Select(Id);
                }
                ucEntityAttrValues.EntityId = ucStation.Site.Id;
                ucStationSites.StationId = ucStation.Site.Id;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            RaiseStationSavedEvent(ucStation.Site);
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
        public delegate void UCStationSavedEventHandler(Station station);
        public event UCStationSavedEventHandler UCStationSavedEvent;
        protected virtual void RaiseStationSavedEvent(Station station)
        {
            if (UCStationSavedEvent != null)
            {
                UCStationSavedEvent(station);
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
                    DataManager.GetInstance().StationGeoObjectRepository.Insert(((GeoObject)geoObject).Id, ucStation.Site.Id);
                }
                FillGO(ucStation.Site.Id);
            }
        }

        private void geoObjectsListBox_UCDeleteEvent(int id)
        {
            DataManager.GetInstance().StationGeoObjectRepository.Delete(id, ucStation.Site.Id);
            FillGO(ucStation.Site.Id);
        }
    }
}
