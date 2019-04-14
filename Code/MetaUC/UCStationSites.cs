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
    public partial class UCStationSites : UserControl
    {
        public UCStationSites()
        {
            InitializeComponent();

            dgv.Columns.Add("id", "КОД");
            dgv.Columns.Add("code", "Код");
            dgv.Columns.Add("siteTypeName", "Тип");
            dgv.Columns.Add("description", "Примечание");
            dgv.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgv.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        public int ParentSiteId { get; set; }
        public void Clear()
        {
            dgv.Rows.Clear();
        }
        public void Fill(int parentSiteId)
        {
            Clear();

            List<Site> sites = DataManager.GetInstance().SiteRepository.SelectByParent(parentSiteId);
            ParentSiteId = parentSiteId;

            foreach (var item in sites)
            {
                DataGridViewRow row = dgv.Rows[dgv.Rows.Add()];
                row.Tag = item;

                row.Cells["id"].Value = item.Id;
                row.Cells["code"].Value = item.Code;
                row.Cells["siteTypeName"].Value = SiteTypeRepository.GetCash().Find(x => x.Id == item.TypeId).Name;
                row.Cells["description"].Value = item.Description;
            }
            RaiseCurrentRowChangedEvent(this.Site);
        }

        private void addNewToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                FormSite frm = new FormSite();
                frm.Site = new Site() { Id = -1, ParentId = ParentSiteId };
                frm.StartPosition = FormStartPosition.CenterParent;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    DataManager.GetInstance().SiteRepository.Insert(frm.Site);
                }
                Fill(ParentSiteId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #region EVENTS
        public delegate void UCCurrentRowChangedEventHandler(Site site);
        public event UCCurrentRowChangedEventHandler UCCurrentRowChangedEvent;
        protected virtual void RaiseCurrentRowChangedEvent(Site site)
        {
            if (UCCurrentRowChangedEvent != null)
            {
                UCCurrentRowChangedEvent(site);
            }
        }
        #endregion

        private void dgv_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            RaiseCurrentRowChangedEvent((Site)dgv.Rows[e.RowIndex].Tag);
        }

        private void deleteToolStripButton_Click(object sender, EventArgs e)
        {
            if (this.Site != null)
            {
                DataManager.GetInstance().SiteRepository.Delete(this.Site.Id);
                Fill(ParentSiteId);
            }
        }

        private void updateToolStripButton_Click(object sender, EventArgs e)
        {
            if (this.Site != null)
            {
                try
                {
                    FormSite frm = new FormSite(Site);
                    frm.StartPosition = FormStartPosition.CenterParent;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        DataManager.GetInstance().SiteRepository.Update(frm.Site);
                    }
                    Fill(ParentSiteId);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        Site Site
        {
            get
            {
                return (dgv.SelectedRows.Count != 1) ? null : (Site)dgv.SelectedRows[0].Tag;
            }
        }

        private void dgv_DoubleClick(object sender, EventArgs e)
        {
            updateToolStripButton_Click(null, null);
        }
    }
}
