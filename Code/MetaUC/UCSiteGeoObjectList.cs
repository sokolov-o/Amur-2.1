﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FERHRI.Amur.Meta
{
    public partial class UCSiteGeoObjectList : UserControl
    {
        public UCSiteGeoObjectList()
        {
            InitializeComponent();

            SiteAttrDateActual = DateTime.Today;
            _dgvBaseColumnsCount = dgv.ColumnCount;

            DefaultNewSiteAttrDateTime = new DateTime(1900, 1, 1);
        }
        bool _isFilled = false;
        public void Fill(int? siteGroupId)
        {
            Cursor cs = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            _isFilled = true;
            try
            {
                dgv.Columns["stationName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
                dgv.Columns["siteTypeName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
                dgv.Rows.Clear();
                if (siteGroupId.HasValue)
                {
                    DataManager dm = DataManager.GetInstance();
                    if (dm == null)
                        return;

                    // GET GROUP Sites & Stations
                    List<Site> sites = SiteGroupComboBox.GetGroupSites(siteGroupId);
                    List<Station> stations = dm.StationRepository.Select(sites.Select(x => x.StationId).ToList());
                    List<StationType> sts = dm.StationTypeRepository.Select();

                    // Station X GeoObject -> Geo objects
                    List<StationGeoObject> sgos = dm.StationGeoObjectRepository.SelectByStations(stations.Select(x => x.Id).ToList());
                    List<int> sgosId = sgos.Select(x => x.GeoObjectId).Distinct().ToList();
                    List<GeoObject> gos = (sgosId.Count == 0) ? new List<GeoObject>() : dm.GeoObjectRepository.Select(sgos.Select(x => x.GeoObjectId).Distinct().ToList());

                    // FILL DGV - SITES WITH GO
                    foreach (var go in gos.OrderBy(x => x.Order))
                    {
                        foreach (var sgo in sgos.Where(x => x.GeoObjectId == go.Id).OrderBy(x => x.Order))
                        {
                            Station station = stations.Find(x => x.Id == sgo.StationId);

                            foreach (var site in sites.Where(x => x.StationId == sgo.StationId))
                            {
                                //StationType siteType = sts.Find(x => x.Id == site.SiteTypeId);
                                AddDGVRow(site, station, go, sts.Find(x => x.Id == site.SiteTypeId));

                                //DataGridViewRow row = dgv.Rows[dgv.Rows.Add()];
                                //row.Tag = new RowTag() { Site = site, Station = station, GeoObject = go, SiteType = siteType };

                                //row.Cells["geoObjectName"].Value = go.Name;
                                //row.Cells["stationName"].Value = site.SiteCode + " " + station.Name;
                                //row.Cells["stationCode"].Value = station.Code;
                                //row.Cells["siteTypeName"].Value = siteType.NameShort;
                            }
                        }
                    }
                    // FILL DGV - SITES WITHOUT GO
                    List<Station> stnNoGOList = stations.Where(x => !sgos.Exists(y => y.StationId == x.Id)).ToList();
                    foreach (Station station in stnNoGOList.OrderBy(x => x.Name))
                    {
                        foreach (var site in sites.Where(x => x.StationId == station.Id))
                        {
                            //StationType siteType = sts.Find(x => x.Id == site.SiteTypeId);
                            AddDGVRow(site, station, null, sts.Find(x => x.Id == site.SiteTypeId));

                            //DataGridViewRow row = dgv.Rows[dgv.Rows.Add()];
                            //row.Tag = new RowTag() { Site = site, Station = station, GeoObject = null, SiteType = siteType };//

                            //row.Cells["geoObjectName"].Value = string.Empty;
                            //row.Cells["stationName"].Value = site.SiteCode + " " + station.Name;
                            //row.Cells["stationCode"].Value = station.Code;
                            //row.Cells["siteTypeName"].Value = siteType.NameShort;
                        }
                    }

                    FillSiteAttributes(sites);

                    dgv.Columns["stationName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dgv.Columns["siteTypeName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;

                    infoToolStripLabel.Text = dgv.Rows.Count.ToString();
                }
            }
            finally
            {
                this.Cursor = cs;
                _isFilled = false;
                RaiseSelectedSiteChangedEvent();
            }
        }
        void AddDGVRow(Site site, Station station, GeoObject go, StationType siteType)
        {
            DataGridViewRow row = dgv.Rows[dgv.Rows.Add()];
            row.Tag = new RowTag() { Site = site, Station = station, GeoObject = null, SiteType = siteType };//

            row.Cells["geoObjectName"].Value = go == null ? string.Empty : go.Name;
            row.Cells["stationName"].Value = station.Name + " (" + site.SiteCode + ")";
            row.Cells["stationCode"].Value = station.Code;
            row.Cells["siteTypeName"].Value = siteType.NameShort;
        }
        private void FillSiteAttributes(List<Site> sites)
        {
            int columnCount = dgv.ColumnCount;
            for (int i = _dgvBaseColumnsCount; i < columnCount; i++)
            {
                dgv.Columns.RemoveAt(i);
                columnCount--;
            }
            if (SiteAttrTypes != null && SiteAttrTypes.Count > 0)
            {
                List<EntityAttrValue> savs = DataManager.GetInstance().EntityAttrRepository.SelectAttrValuesActual("site",
                    sites.Select(x => x.Id).ToList(),
                    SiteAttrTypes.Select(x => x.Id).ToList(),
                    SiteAttrDateActual);

                foreach (var item in SiteAttrTypes)
                {
                    DataGridViewColumn column = dgv.Columns[dgv.Columns.Add("siteAttr" + item.Id, item.Name)];
                    column.Tag = item;
                    //column.CellTemplate.ReadOnly = false;

                    List<EntityAttrValue> savt = savs.FindAll(x => x.AttrTypeId == item.Id);
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        Site site = ((RowTag)row.Tag).Site;
                        DataGridViewCell cell = row.Cells[column.Index];
                        //cell.ReadOnly = false;

                        EntityAttrValue sav = savt.FirstOrDefault(x => x.EntityId == site.Id);
                        if (sav != null)
                        {
                            cell.Tag = sav;
                            cell.Value = sav.Value;
                            cell.ToolTipText = "Дата актуальности: " + sav.DateS;
                        }
                    }
                }

            }
        }

        Station Station
        {
            get
            {
                return dgv.SelectedRows.Count == 0 ? null : ((RowTag)dgv.SelectedRows[0].Tag).Station;
            }
        }
        GeoObject GeoObject
        {
            get
            {
                return dgv.SelectedRows.Count == 0 ? null : ((RowTag)dgv.SelectedRows[0].Tag).GeoObject;
            }
        }
        StationType SiteType
        {
            get
            {
                return dgv.SelectedRows.Count == 0 ? null : ((RowTag)dgv.SelectedRows[0].Tag).SiteType;
            }
        }

        private void siteGroupToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fill(SiteGroupId);
        }

        public int? SiteGroupId
        {
            get { return siteGroupToolStripComboBox.SiteGroup == null ? null : (int?)siteGroupToolStripComboBox.SiteGroup.Id; }
            set
            {
                dgv.Rows.Clear();
                siteGroupToolStripComboBox.SetSiteGroup(value);
            }
        }

        private void editToolStripButton_Click(object sender, EventArgs e)
        {
            RaiseEditDataEvent();
        }

        private void findNextToolStripButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(findToolStripTextBox.Text) && dgv.Rows.Count > 0)
            {
                int iStart = 0;
                if (dgv.SelectedRows.Count > 0)
                {
                    iStart = dgv.SelectedRows[0].Index + 1; dgv.SelectedRows[0].Selected = false;
                }
                if (iStart > dgv.Rows.Count)
                {
                    iStart = 0; Console.Beep();
                }

                for (int i = iStart; i < dgv.Rows.Count; i++)
                {
                    foreach (DataGridViewCell item in dgv.Rows[i].Cells)
                    {
                        if (item.Value.ToString().ToUpper().IndexOf(findToolStripTextBox.Text.ToUpper()) >= 0)
                        {
                            dgv.Rows[item.RowIndex].Selected = true;
                            dgv.FirstDisplayedScrollingRowIndex = item.RowIndex;
                            return;
                        }
                    }
                }
                Console.Beep();
            }
        }

        private void mnuEditSiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RaiseEditSiteEvent();
        }
        private void mnuEditDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RaiseEditDataEvent();
        }

        private void editStationToolStripButton_Click(object sender, EventArgs e)
        {
            RaiseEditSiteEvent();
        }

        private void mnuClimateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RaiseShowClimateEvent();
        }

        private void refreshToolStripButton_Click(object sender, EventArgs e)
        {
            Meta.DataManager.ClearCashs();
            Fill(SiteGroupId);
        }

        private void isOneTableToolStripButton_Click(object sender, EventArgs e)
        {
            IsOneDataTable = !IsOneDataTable;
        }
        public bool IsOneDataTable
        {
            get
            {
                return isOneTableToolStripButton.Text == "1";
            }
            private set
            {
                if (value)
                {
                    isOneTableToolStripButton.Text = "1";
                    isOneTableToolStripButton.ToolTipText = "Одна вкладка для всех пунктов";
                }
                else
                {
                    isOneTableToolStripButton.Text = "2";
                    isOneTableToolStripButton.ToolTipText = "Отдельная вкладка для каждого пункта";
                }
            }
        }

        private void dgv_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (!_isFilled)
                RaiseSelectedSiteChangedEvent();
        }

        public void SetCurrentSite(int siteId)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                Site site = ((RowTag)row.Tag).Site;
                if (site.Id == siteId)
                {
                    row.Selected = true;
                    dgv.FirstDisplayedScrollingRowIndex = row.Index;
                }
            }
        }
        public DataGridViewSelectionMode UCDGVSelectionMode
        {
            get
            {
                return dgv.SelectionMode;
            }
            set
            {
                dgv.SelectionMode = value;
            }
        }
        /// <summary>
        /// Selected site.
        /// </summary>
        public Site Site
        {
            get
            {
                return dgv.SelectedRows.Count == 0 ? null : ((RowTag)dgv.SelectedRows[0].Tag).Site;
            }
        }

        /// <summary>
        /// Show current Site group data grp.
        /// </summary>
        private void showDataGrpSVYMButton_Click(object sender, EventArgs e)
        {
            RaiseShowDataGrpEvent();
        }

        #region EVENTS
        public delegate void UCSelectedSiteChangedEventHandler(Site site);
        public event UCSelectedSiteChangedEventHandler UCSelectedSiteChangedEvent;
        protected virtual void RaiseSelectedSiteChangedEvent()
        {
            if (UCSelectedSiteChangedEvent != null)
            {
                UCSelectedSiteChangedEvent(Site);
            }
        }
        public delegate void UCEditDataEventHandler(Site site, Station station, StationType siteType);
        public event UCEditDataEventHandler UCEditDataEvent;
        protected virtual void RaiseEditDataEvent()
        {
            if (UCEditDataEvent != null)
            {
                UCEditDataEvent(Site, Station, SiteType);
            }
        }
        public delegate void UCEditSiteEventHandler(int siteId);
        public event UCEditSiteEventHandler UCEditSiteEvent;
        protected virtual void RaiseEditSiteEvent()
        {
            if (UCEditSiteEvent != null)
            {
                UCEditSiteEvent(Site.Id);
            }
        }
        public delegate void UCShowClimateEventHandler(Site site);
        public event UCShowClimateEventHandler UCShowClimateEvent;
        protected virtual void RaiseShowClimateEvent()
        {
            if (UCShowClimateEvent != null)
            {
                UCShowClimateEvent(Site);
            }
        }
        public delegate void UCShowDataGrpEventHandler(int siteGroupId);
        public event UCShowDataGrpEventHandler UCShowDataGrpEvent;
        protected virtual void RaiseShowDataGrpEvent()
        {
            if (UCShowDataGrpEvent != null)
            {
                if (SiteGroupId.HasValue) UCShowDataGrpEvent(SiteGroupId.Value);
            }
        }
        public delegate void UCShowSiteDataGrpEventHandler(int siteId);
        public event UCShowSiteDataGrpEventHandler UCShowSiteDataGrpEvent;
        protected virtual void RaiseShowSiteDataGrpEvent()
        {
            if (UCShowSiteDataGrpEvent != null)
            {
                UCShowSiteDataGrpEvent(Site.Id);
            }
        }
        public delegate void UCAddSiteChartEventHandler(int siteId);
        public event UCAddSiteChartEventHandler UCAddSiteChartEvent;
        protected virtual void RaiseAddSiteChartEvent()
        {
            if (UCAddSiteChartEvent != null)
            {
                UCAddSiteChartEvent(Site.Id);
            }
        }
        #endregion EVENTS

        private void mnuShowSiteDataGrpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RaiseShowSiteDataGrpEvent();
        }

        private void addToChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RaiseAddSiteChartEvent();
        }
        public bool ShowEditDataButton
        {
            get { return editToolStripButton.Visible; }
            set { editToolStripButton.Visible = value; }
        }
        public bool ShowEditStationButton
        {
            get { return editStationToolStripButton.Visible; }
            set { editStationToolStripButton.Visible = value; }
        }
        public bool ShowIsOneTableButton
        {
            get { return isOneTableToolStripButton.Visible; }
            set
            {
                isOneTableToolStripButton.Visible = value;
                toolStripSeparator1.Visible = value;
            }
        }
        public bool ShowGroupDataButton
        {
            get { return showDataGrpSVYMButton.Visible; }
            set { showDataGrpSVYMButton.Visible = value; }
        }
        public bool ShowRefreshButton
        {
            get { return refreshToolStripButton.Visible; }
            set
            {
                refreshToolStripButton.Visible = value;
            }
        }

        public DateTime DefaultNewSiteAttrDateTime { get; set; }
        public List<SiteAttrType> SiteAttrTypes { get; set; }
        public DateTime SiteAttrDateActual { get; set; }
        static int _dgvBaseColumnsCount;

        class RowTag
        {
            public Site Site { get; set; }
            public Station Station { get; set; }
            public GeoObject GeoObject { get; set; }
            public StationType SiteType { get; set; }

            public string SiteName
            {
                get
                {
                    return Meta.Site.GetName(Station, SiteType, Site.SiteCode, 2) + " (" + Site.Id + ")";
                }
            }
        }

        public List<EntityAttrValue> EAVEdited = new List<EntityAttrValue>();

        private void dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
            EntityAttrValue eav = (EntityAttrValue)cell.Tag;
            if (eav == null && cell.Value != null && !string.IsNullOrEmpty(cell.Value.ToString()))
            {
                eav = new EntityAttrValue(
                    ((RowTag)dgv.Rows[cell.RowIndex].Tag).Site.Id,
                    ((SiteAttrType)dgv.Columns[cell.ColumnIndex].Tag).Id,
                    DefaultNewSiteAttrDateTime,
                    cell.Value.ToString()
                );
                cell.Tag = eav;
            }
            else
            {
                string value = cell.Value == null ? null : cell.Value.ToString().Trim();
                if (eav.Value == value)
                    return;
                eav.Value = value;
            }

            EAVEdited.Add(eav);
            saveEditedButton.Enabled = true;
        }

        private void saveEditedButton_Click(object sender, EventArgs e)
        {
            EntityAttrRepository rep = DataManager.GetInstance().EntityAttrRepository;
            foreach (var item in EAVEdited)
            {
                if (string.IsNullOrEmpty(item.Value))
                    rep.DeleteValue("site", item.EntityId, item.AttrTypeId, (DateTime)item.DateS);
                else
                    rep.InsertUpdateValue("site", item.EntityId, item.AttrTypeId, (DateTime)item.DateS, item.Value);
            }
            EAVEdited.Clear();
            saveEditedButton.Enabled = false;
        }
        public delegate void UCEntityAttrValueChangedEventHandler(string siteName, EntityAttrValue eav);
        public event UCEntityAttrValueChangedEventHandler UCEntityAttrValueChangedEvent;
        protected virtual void RaiseEntityAttrValueChangedEvent()
        {
            if (UCEntityAttrValueChangedEvent != null)
            {
                // PREPARE EntityAttrValue 
                EntityAttrValue eav = CurrentEntityAttrValue;
                if (eav == null && dgv.SelectedCells.Count == 1)
                {
                    DataGridViewColumn column = dgv.Columns[dgv.SelectedCells[0].ColumnIndex];
                    if (column.Tag != null && column.Tag.GetType() == typeof(SiteAttrType))
                    {
                        eav = new EntityAttrValue(
                            CurrentRowTag.Site.Id,
                            ((SiteAttrType)dgv.Columns[dgv.SelectedCells[0].ColumnIndex].Tag).Id,
                            DefaultNewSiteAttrDateTime, null);
                    }
                }

                // RAISE
                UCEntityAttrValueChangedEvent(CurrentRowTag == null ? null : CurrentRowTag.SiteName, eav);
            }
        }
        RowTag CurrentRowTag
        {
            get
            {
                DataGridViewRow row = null;
                if (dgv.SelectedRows.Count == 1) row = dgv.SelectedRows[0];
                if (dgv.SelectedCells.Count == 1) row = dgv.Rows[dgv.SelectedCells[0].RowIndex];

                if (row != null && row.Tag != null)
                {
                    return (RowTag)row.Tag;
                }
                return null;
            }
        }
        /// <summary>
        /// Current cell EntityAttrValue, or null if cell not type of EntityAttrValue.
        /// </summary>
        EntityAttrValue CurrentEntityAttrValue
        {
            get
            {
                if (dgv.SelectedCells.Count == 1)
                {
                    DataGridViewCell cell = dgv.SelectedCells[0];
                    if (cell.Tag != null && cell.Tag.GetType() == typeof(EntityAttrValue))
                    {
                        return (EntityAttrValue)cell.Tag;
                    }
                }
                return null;
            }
        }
        private void dgv_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            RaiseEntityAttrValueChangedEvent();
        }
    }
}
