using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SOV.Amur.Meta;
using SOV.Common;

namespace SOV.Amur.Meta
{
    public partial class UCCatalogs : UserControl
    {
        public UCCatalogs()
        {
            InitializeComponent();

            UCShowOrderbyButtons = false;

            //ucCatalogFilter.Fill(SiteRepository.GetCash(), VariableRepository.GetCash(),
            //    MethodRepository.GetCash(), Social.LegalEntityRepository.GetCash(),
            //    OffsetTypeRepository.GetCash());
        }
        public void Fill(CatalogFilter ctlFilter)
        {
            Fill(Meta.DataManager.GetInstance().CatalogRepository.Select(ctlFilter));
        }
        public void Fill(List<Catalog> ctls)
        {
            ucCatalog.Catalog = null;

            // FILL DGV
            dgv.Rows.Clear();
            foreach (var ctl in ctls)
            {
                DataGridViewRow row = dgv.Rows[dgv.Rows.Add()];
                row.Tag = ctl;

                row.Cells[0].Value = Meta.Site.GetName(SiteRepository.GetCash().Find(y => y.Id == ctl.SiteId), 2, SiteTypeRepository.GetCash());
                row.Cells[1].Value = VariableRepository.GetCash().Find(x => x.Id == ctl.VariableId).NameRus;
                row.Cells[2].Value = OffsetTypeRepository.GetCash().Find(x => x.Id == ctl.OffsetTypeId).Name;
                row.Cells[3].Value = ctl.OffsetValue;
                row.Cells[4].Value = MethodRepository.GetCash().Find(x => x.Id == ctl.MethodId).Name;
                row.Cells[5].Value = Social.LegalEntityRepository.GetCash().Find(x => x.Id == ctl.SourceId).NameRus;
                row.Cells[6].Value = ctl.Id;
            }

            ucCatalog.Catalog = dgv.SelectedRows.Count > 0 ? (Catalog)dgv.SelectedRows[0].Tag : null;

            infoLabel.Text = dgv.Rows.Count.ToString();
        }
        private void SaveButton_Click(object sender, EventArgs e)
        {
            Catalog ctl = ucCatalog.Catalog;
            if ((object)ctl == null)
            {
                MessageBox.Show("Запись каталога не произведена - ошибка в форме каталога. Проверьте, определены ли все поля записи.");
                return;
            }
            if (ctl.Id < 0)
                Meta.DataManager.GetInstance().CatalogRepository.Insert(ctl);
            else
                Meta.DataManager.GetInstance().CatalogRepository.Update(ctl);
        }

        private void newButton_Click(object sender, EventArgs e)
        {
            ucCatalog.Catalog = null;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Catalog ctl = ucCatalog.Catalog;
            ctl.Id = -1;
            ucCatalog.Catalog = ctl;
        }
        public delegate void UCSaveFilterUserSettingsEventHandler(CatalogFilter cf);
        public event UCSaveFilterUserSettingsEventHandler UCSaveFilterUserSettingsEvent;
        protected virtual void RaiseSaveFilterUserSettingsEvent(CatalogFilter cf)
        {
            if (UCSaveFilterUserSettingsEvent != null)
            {
                UCSaveFilterUserSettingsEvent(cf);
            }
        }

        public CatalogFilter CatalogFilter { set { ucCatalogFilter.CatalogFilter = value; } }

        public List<Catalog> GetCatalogs()
        {
            List<Catalog> ret = new List<Catalog>();
            foreach (DataGridViewRow row in dgv.Rows)
            {
                ret.Add((Catalog)row.Tag);
            }
            return ret;
        }

        public List<Catalog> GetSelectedCatalogs()
        {
            List<Catalog> ret = new List<Catalog>();
            foreach (DataGridViewRow row in dgv.SelectedRows)
            {
                ret.Add((Catalog)row.Tag);
            }
            return ret;
        }
        /// <summary>
        /// Null, если не выбрана ни одна запись, либо выбрано более одной.
        /// </summary>
        public Catalog CurCatalog
        {
            get
            {
                List<Catalog> ret = SelectedCatalogs;
                return ret.Count == 1 ? ret[0] : null;
            }
        }
        /// <summary>
        /// Записи каталога, выбранные в таблице.
        /// </summary>
        public List<Catalog> SelectedCatalogs
        {
            get
            {
                List<Catalog> ret = new List<Catalog>();
                foreach (DataGridViewRow item in dgv.SelectedRows)
                    ret.Add((Catalog)item.Tag);
                return ret;
            }
        }

        public bool UCCatalogFilterVisible
        {
            get
            {
                return !splitContainer1.Panel1Collapsed;
            }
            set
            {
                splitContainer1.Panel1Collapsed = !value;
                //splitContainer2.Panel1.ClientSize = new Size(splitContainer2.ClientSize.Width / 2, splitContainer2.Panel1.ClientSize.Height);
            }
        }
        public bool UCCatalogVisible
        {
            get
            {
                return !splitContainer2.Panel2Collapsed;
            }
            set
            {
                splitContainer2.Panel2Collapsed = !value;
            }
        }
        public bool UCDeleteCatalogButtonVisible
        {
            get
            {
                return deleteCatalogButton.Visible;
            }
            set
            {
                deleteCatalogButton.Visible = value;
            }
        }
        public bool UCAddCatalogButtonVisible
        {
            get
            {
                return addCatalogButton.Visible;
            }
            set
            {
                addCatalogButton.Visible = value;
            }
        }
        public bool UCShowOrderbyButtons
        {
            get
            {
                return tableLayoutPanel3.ColumnStyles[1].SizeType == SizeType.AutoSize;
            }
            set
            {
                if (value)
                {
                    tableLayoutPanel3.ColumnStyles[1].SizeType = SizeType.AutoSize;
                    saveCatalogOrderbyButton.Visible = true;
                }
                else
                {
                    tableLayoutPanel3.ColumnStyles[1].SizeType = SizeType.Absolute;
                    tableLayoutPanel3.ColumnStyles[1].Width = 0;
                    saveCatalogOrderbyButton.Visible = false;
                }
            }
        }
        public string UCCatalogsText
        {
            get
            {
                return groupBox2.Text;
            }
            set
            {
                groupBox2.Text = value;
            }
        }
        private void deleteCatalogToolStripButton_Click(object sender, EventArgs e)
        {
            RaiseUCDeleteButtonClickEvent(GetSelectedCatalogs().Select(x => x.Id).ToList());
        }
        public delegate void UCDeleteButtonClickEventHandler(List<int> catalogsId);
        public event UCDeleteButtonClickEventHandler UCDeleteButtonClick;
        protected virtual void RaiseUCDeleteButtonClickEvent(List<int> catalogsId)
        {
            if (UCDeleteButtonClick != null)
            {
                UCDeleteButtonClick(catalogsId);
            }
        }

        private void addCatalogButton_Click(object sender, EventArgs e)
        {
            RaiseUCAddButtonClickEvent();
        }
        public delegate void UCAddButtonClickEventHandler();
        public event UCAddButtonClickEventHandler UCAddButtonClickEvent;
        protected virtual void RaiseUCAddButtonClickEvent()
        {
            if (UCAddButtonClickEvent != null)
            {
                UCAddButtonClickEvent();
            }
        }

        public void Clear()
        {
            dgv.Rows.Clear();
            ucCatalog.Clear();
        }
        public void SetModeSelect()
        {
            UCDeleteCatalogButtonVisible = false;
            UCAddCatalogButtonVisible = false;
            UCCatalogVisible = false;
        }
        public void SetModeEdit()
        {
            UCDeleteCatalogButtonVisible = true;
            UCAddCatalogButtonVisible = true;
            UCCatalogVisible = true;
        }

        private void dgv_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            ucCatalog.Clear();
            if (CatalogDetailsVisible)
            {
                ucCatalog.Catalog = dgv.SelectedRows.Count > 0 ? (Catalog)dgv.SelectedRows[0].Tag : null;
                RaiseUCSelectedCatalogChangedEvent(CurCatalog);
            }
        }

        public delegate void UCSelectedCatalogChangedEventHandler(Catalog catalog);
        public event UCSelectedCatalogChangedEventHandler UCSelectedCatalogChanged;
        protected virtual void RaiseUCSelectedCatalogChangedEvent(Catalog catalog)
        {
            if (UCSelectedCatalogChanged != null)
            {
                UCSelectedCatalogChanged(catalog);
            }
        }

        private void ucCatalogFilter_UCFilterButtonClickEvent()
        {
            // SELECT CATALOGS
            CatalogFilter f = ucCatalogFilter.CatalogFilter;
            List<Catalog> ctls = Meta.DataManager.GetInstance().CatalogRepository.Select(
                f.Sites,
                f.Variables,
                f.Methods,
                f.Sources,
                f.OffsetTypes,
                f.OffsetValue
            );

            Fill(ctls);
        }

        private void upButton_Click(object sender, EventArgs e)
        {
            updownButtonClick(true);
        }

        private void downButton_Click(object sender, EventArgs e)
        {
            updownButtonClick(false);
        }
        private void updownButtonClick(bool isUp)
        {
            if (dgv.SelectedRows.Count == 1 && dgv.SelectedRows[0].Index != (isUp ? 0 : dgv.RowCount - 1))
            {
                int i = dgv.SelectedRows[0].Index;
                DataGridViewRow row = dgv.Rows[i];

                dgv.Rows.Remove(row);
                dgv.Rows.Insert(i + (isUp ? -1 : +1), row);
                if (dgv.SelectedRows.Count == 1)
                    dgv.SelectedRows[0].Selected = false;
                row.Selected = true;
                row.Visible = true;
            }
        }

        public delegate void UCSaveCatalogOrderbyEventHandler(List<Catalog> ctls);
        public event UCSaveCatalogOrderbyEventHandler UCSaveCatalogOrderby;
        protected virtual void RaiseSaveCatalogOrderby(List<Catalog> ctls)
        {
            if (UCSaveCatalogOrderby != null)
            {
                UCSaveCatalogOrderby(ctls);
            }
        }
        private void saveCatalogOrderbyButton_Click(object sender, EventArgs e)
        {
            RaiseSaveCatalogOrderby(GetCatalogs());
        }

        private void showDataButton_Click(object sender, EventArgs e)
        {
            if (ShowDataValueEventHandler == null)
                Console.Beep();
            else
                ShowDataValueEventHandler.Invoke(this, new CatalogEventArgs() { Catalogs = SelectedCatalogs });
        }
        public System.EventHandler ShowDataValueEventHandler { get; set; }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            CatalogDetailsVisible = !CatalogDetailsVisible;
        }
        bool CatalogDetailsVisible
        {
            get
            {
                return !splitContainer2.Panel2Collapsed;
            }
            set
            {
                splitContainer2.Panel2Collapsed = !splitContainer2.Panel2Collapsed;
            }
        }

        private void UCCatalogs_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                ucCatalogFilter.Fill(SiteRepository.GetCash(), VariableRepository.GetCash(), MethodRepository.GetCash(), Social.LegalEntityRepository.GetCash(), OffsetTypeRepository.GetCash());
            }
        }
    }

    public class CatalogEventArgs : EventArgs
    {
        public List<Catalog> Catalogs;
    }
}
