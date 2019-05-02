using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOV.Amur.Meta
{
    public partial class UCStations : UserControl
    {
        public bool MultySelect
        {
            get { return dgv.MultiSelect; }
            set
            {
                selectionInfoToolStripLabel.Visible = value;
                selectionCounterToolStripLabel.Visible = value;
                dgv.MultiSelect = value;
            }
        }

        public bool VisibleComplateButton
        {
            get { return complateSelectionToolStripButton.Visible; }
            set { complateSelectionToolStripButton.Visible = value; }
        }

        public bool VisibleSiteGroups
        {
            get { return siteGroupToolStripComboBox.Visible; }
            set { siteGroupToolStripComboBox.Visible = value; }
        }

        public bool VisibleAddNewButton
        {
            get { return addNewToolStripButton.Visible; }
            set { addNewToolStripButton.Visible = value; }
        }

        public bool VisibleEditStationButton
        {
            get { return editStationToolStripButton.Visible; }
            set { editStationToolStripButton.Visible = value; }
        }

        public bool EnableMenuStrip
        {
            get { return contextMenuStrip1.Enabled; }
            set { contextMenuStrip1.Enabled = value; }
        }

        public bool VisibleNoSiteButton
        {
            get { return noSitesToolStripButton.Visible; }
            set { noSitesToolStripButton.Visible = value; }
        }

        public Size SerchSiteInputSize
        {
            get { return findToolStripTextBox.Size; }
            set { findToolStripTextBox.Size = value; }
        }

        public UCStations()
        {
            InitializeComponent();
            MultySelect = false;
            dgv.SortCompare += new DataGridViewSortCompareEventHandler(this.dataGridView_SortCompare);
        }

        bool _isFilled = false;
        public void FillSiteGroup(int? siteGroupId)
        {
            Cursor cs = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            _isFilled = true;
            try
            {
                dgv.Columns["stationName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
                dgv.Columns["stationTypeName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
                dgv.Rows.Clear();
                if (siteGroupId.HasValue)
                {
                    FillStations(DataManager.GetInstance().SiteRepository.Select(
                        SiteGroupComboBox.GetGroupSites(siteGroupId).Select(x => x.Id).ToList())
                    );
                }
            }
            finally
            {
                this.Cursor = cs;
                _isFilled = false;
                RaiseSelectedStationChangedEvent();
            }
        }
        public void FillStations(List<Site> sites)
        {
            Cursor cs = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            _isFilled = true;
            try
            {
                dgv.Columns["siteName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
                dgv.Columns["siteTypeName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
                dgv.Rows.Clear();

                // GeoObjects, Stations, Types
                List<SiteType> sts = DataManager.GetInstance().SiteTypeRepository.Select();
                List<SiteGeoObject> sgos = DataManager.GetInstance().SiteGeoObjectRepository.SelectBySites(sites.Select(x => x.Id).ToList());
                if (sgos.Count > 0)
                {
                    List<GeoObject> gos = DataManager.GetInstance().GeoObjectRepository.Select(sgos.Select(x => x.GeoObjectId).Distinct().ToList());

                    // FILL DGV - GO WITH SITES
                    foreach (var go in gos.OrderBy(x => x.OrderBy))
                    {
                        foreach (var sgo in sgos.Where(x => x.GeoObjectId == go.Id).OrderBy(x => x.OrderBy))
                        {
                            Site site = sites.Find(x => x.Id == sgo.SiteId);
                            SiteType sType = sts.Find(x => x.Id == site.TypeId);

                            DataGridViewRow row = dgv.Rows[dgv.Rows.Add()];
                            row.Tag = new object[] { site, go };

                            row.Cells["id"].Value = site.Id;
                            row.Cells["geoObjectName"].Value = go.Name;
                            row.Cells["siteName"].Value = site.Name;
                            row.Cells["siteCode"].Value = site.Code;
                            row.Cells["siteTypeName"].Value = sType.NameShort;
                        }
                    }
                }

                // FILL DGV - SITES WITHOUT GO
                List<Site> stnNoGOList = sites.Where(x => !sgos.Exists(y => y.SiteId == x.Id)).ToList();
                foreach (Site station in stnNoGOList.OrderBy(x => x.Name))
                {
                    DataGridViewRow row = dgv.Rows[dgv.Rows.Add()];
                    row.Tag = new object[] { station, null };

                    row.Cells["id"].Value = station.Id;
                    row.Cells["geoObjectName"].Value = string.Empty;
                    row.Cells["siteName"].Value = station.Name;
                    row.Cells["siteCode"].Value = station.Code;
                    row.Cells["siteTypeName"].Value = sts.Find(x => x.Id == station.TypeId).NameShort;
                }

                dgv.Columns["siteName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgv.Columns["siteTypeName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;

                infoToolStripLabel.Text = dgv.Rows.Count.ToString();
            }
            finally
            {
                this.Cursor = cs;
                _isFilled = false;
                RaiseSelectedStationChangedEvent();
            }
        }

        public Site Site
        {
            get
            {
                return dgv.SelectedRows.Count == 0 ? null : (Site)((object[])dgv.SelectedRows[0].Tag)[0];
            }
        }
        GeoObject GeoObject
        {
            get
            {
                return dgv.SelectedRows.Count == 0 ? null : (GeoObject)((object[])dgv.SelectedRows[0].Tag)[1];
            }
        }

        private void siteGroupToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillSiteGroup((SiteGroup == null) ? null : (int?)SiteGroup.Id);
        }

        public int? SiteGroupId
        {
            get
            {
                return siteGroupToolStripComboBox.SelectedItem == null ? (int?)null : ((EntityGroup)siteGroupToolStripComboBox.SelectedItem).Id;
            }
            set
            {
                SiteGroup = value.HasValue ? new EntityGroup(value.Value, "", "site") : null;
            }
        }

        public EntityGroup SiteGroup
        {
            get
            {
                return siteGroupToolStripComboBox.SelectedItem == null ? null : siteGroupToolStripComboBox.SelectedItem as EntityGroup;
            }
            set
            {
                siteGroupToolStripComboBox.SelectedItem = null;
                dgv.Rows.Clear();

                if (value != null)
                {
                    foreach (var item in siteGroupToolStripComboBox.Items)
                    {
                        if (((EntityGroup)item).Id == value.Id)
                            siteGroupToolStripComboBox.SelectedItem = item;
                    }
                    //if (SiteGroup != null)
                    //    Fill(SiteGroup.Id);
                }
            }
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

        #region EVENTS
        public delegate void UCSelectedStationChangedEventHandler(Site site);
        public event UCSelectedStationChangedEventHandler UCSelectedStationChangedEvent;
        protected virtual void RaiseSelectedStationChangedEvent()
        {
            if (UCSelectedStationChangedEvent != null)
            {
                UCSelectedStationChangedEvent(Site);
            }
        }
        public delegate void UCEditStationEventHandler(int stationId);
        public event UCEditStationEventHandler UCEditStationEvent;
        protected virtual void RaiseEditStationEvent()
        {
            if (UCEditStationEvent != null && Site != null)
            {
                UCEditStationEvent(Site.Id);
            }
        }
        public delegate void UCNewStationEventHandler();
        public event UCNewStationEventHandler UCNewStationEvent;
        protected virtual void RaiseNewStationEvent()
        {
            if (UCNewStationEvent != null)
            {
                UCNewStationEvent();
            }
        }
        #endregion EVENTS

        private void refreshToolStripButton_Click(object sender, EventArgs e)
        {
            DataManager.ClearCashs();
            FillSiteGroup((SiteGroup == null) ? null : (int?)SiteGroup.Id);
        }

        private void dgv_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (!_isFilled)
                RaiseSelectedStationChangedEvent();
        }

        private void editStationToolStripButton_Click(object sender, EventArgs e)
        {
            RaiseEditStationEvent();
        }

        private void mnuEditSiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RaiseEditStationEvent();
        }

        private void noSitesToolStripButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Deprecated button...");
        }

        private void addNewToolStripButton_Click(object sender, EventArgs e)
        {
            RaiseNewStationEvent();
        }

        public delegate void OnComplateSelectionEventHandler(UCStations sender);
        public event OnComplateSelectionEventHandler OnComplateSelectionEvent = null;

        private void complateSelection(object sender, EventArgs e)
        {
            if (OnComplateSelectionEvent != null)
                OnComplateSelectionEvent(this);
        }

        private void dataGridView_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            e.SortResult = 0;
            if (dgv.Rows[e.RowIndex1].Selected && !dgv.Rows[e.RowIndex2].Selected)
                e.SortResult = dgv.SortOrder == SortOrder.Ascending ? -1 : 1;
            if (!dgv.Rows[e.RowIndex1].Selected && dgv.Rows[e.RowIndex2].Selected)
                e.SortResult = dgv.SortOrder == SortOrder.Ascending ? 1 : -1;
            if (e.SortResult == 0)
                e.SortResult = System.String.Compare(e.CellValue1.ToString(), e.CellValue2.ToString());
            e.Handled = true;
        }

        public List<Site> SelectedSites()
        {
            if (dgv.SelectedRows.Count == 0)
                return null;
            List<int> ids = new List<int>();
            for (int i = 0; i < dgv.SelectedRows.Count; ++i)
                ids.Add((int)dgv.SelectedRows[i].Cells["id"].Value);
            return siteGroupToolStripComboBox.GetGroupSites().FindAll(x => ids.Contains(x.Id));
        }

        public void SetSelectedSites(List<int> sites)
        {
            if (sites == null)
                return;
            dgv.ClearSelection();
            foreach (var site in sites)
                foreach (DataGridViewRow row in dgv.Rows)
                    if (site == (int)row.Cells["id"].Value)
                        row.Selected = true;
        }

        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            selectionCounterToolStripLabel.Text = dgv.SelectedRows.Count.ToString();
        }

        private void findToolStripTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                findNextToolStripButton_Click(null, null);
        }
    }
}
