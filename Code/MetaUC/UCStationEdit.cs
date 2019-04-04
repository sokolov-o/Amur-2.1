using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SOV.Common;

namespace SOV.Amur.Meta
{
    public partial class UCStationEdit : UserControl
    {
        public UCStationEdit()
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
        public void Fill(int stationId)
        {
            ucStation.Station = DataManager.GetInstance().StationRepository.Select(stationId);
            ucStationSites.Fill(stationId);
            FillGO(stationId);
        }
        private void FillGO(int stationId)
        {
            geoObjectsListBox.Clear();
            Dictionary<Station, List<GeoObject>> sgo = DataManager.GetInstance().StationGeoObjectRepository.SelectWithFK(stationId);
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
            ucStation.Station = null;
            ucStationSites.Clear();
            geoObjectsListBox.Clear();
        }
        public Station Station
        {
            get
            {
                return ucStation.Station;
            }
            set
            {
                ucStation.Station = value;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (ucStation.Station.Id > 0)
                    DataManager.GetInstance().StationRepository.Update(ucStation.Station);
                else
                {
                    //ucStation.Station.Id = DataManager.GetInstance().StationRepository.Insert(ucStation.Station);
                    int Id = DataManager.GetInstance().StationRepository.Insert(ucStation.Station);
                    ucStation.Station = DataManager.GetInstance().StationRepository.Select(Id);
                }
                ucEntityAttrValues.EntityId = ucStation.Station.Id;
                ucStationSites.StationId = ucStation.Station.Id;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            RaiseStationSavedEvent(ucStation.Station);
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
                    DataManager.GetInstance().StationGeoObjectRepository.Insert(((GeoObject)geoObject).Id, ucStation.Station.Id);
                }
                FillGO(ucStation.Station.Id);
            }
        }

        private void geoObjectsListBox_UCDeleteEvent(int id)
        {
            DataManager.GetInstance().StationGeoObjectRepository.Delete(id, ucStation.Station.Id);
            FillGO(ucStation.Station.Id);
        }
    }
}
