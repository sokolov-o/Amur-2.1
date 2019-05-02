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
    public partial class UCSiteInstruments : UserControl
    {
        public UCSiteInstruments()
        {
            InitializeComponent();
        }

        Site SiteFilter;

        public void Fill(int siteId)
        {
            Clear();
            DataManager dm = DataManager.GetInstance();
            SiteFilter = dm.SiteRepository.Select(siteId);
            instrumentBindingSource.DataSource = dm.InstrumentRepository.Select().OrderBy(x => x.NameRus);
            var tmp = dm.SiteInstrumentRepository.SelectBySiteIds(new List<int>() { siteId })
                                        .OrderBy(x => x.DateS);
            if (tmp.Count() > 0)
                siteInstrumentBindingSource.DataSource = tmp;
            SetInfo();
        }

        void SetInfo()
        {
            infoLabel.Text = siteInstrumentBindingSource.Count.ToString();
        }

        internal void Clear()
        {
            instrumentBindingSource.Clear();
            siteInstrumentBindingSource.Clear();
            dgv.Rows.Clear();
        }

        private bool isNotNullAndTrue(object item)
        {
            return item != null && (bool)item;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (!IsHaveChanges())
                return;
            SiteInstrumentRepository repo = DataManager.GetInstance().SiteInstrumentRepository;
            for (int i = 0; i < dgv.Rows.Count; ++i)
            {
                SiteInstrument item = (SiteInstrument)dgv.Rows[i].DataBoundItem;
                if (isNotNullAndTrue(dgv.Rows[i].Cells["IsNew"].Value))
                    repo.Insert(item);
                else if (isNotNullAndTrue(dgv.Rows[i].Cells["IsDelete"].Value))
                    repo.Delete((int)dgv.Rows[i].Cells["Id"].Value);
                else if (isNotNullAndTrue(dgv.Rows[i].Cells["IsModify"].Value))
                    repo.Update(item);
            }
            MessageBox.Show("Изменения сохранены", "Таблица инструментов сайта");
            Fill(SiteFilter.Id);
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                var dateSF = new Common.DateSF(DateTime.Now, null);
                var tmp = new SiteInstrument(dateSF);
                tmp.SiteId = SiteFilter.Id;
                tmp.InstrumentId = ((Instrument)instrumentBindingSource[0]).Id;

                if (dgv.Rows.Count > 0)
                {
                    var value = dgv.Rows[dgv.Rows.Count - 1].Cells["dateFDGVC"].Value;
                    if (value != null && value.ToString() != "")
                        tmp.DateS = DateTime.Parse(value.ToString());

                    value = dgv.Rows[dgv.Rows.Count - 1].Cells["locationDescriptionDGVC"].Value;
                    if (value != null && value.ToString() != "")
                        tmp.LocationDescription = value.ToString();
                }

                siteInstrumentBindingSource.Add(tmp);
                dgv.Rows[dgv.Rows.Count - 1].Cells["IsNew"].Value = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dgv.Rows[e.RowIndex].Cells["IsModify"].Value = true;
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            List<int> rowsToDelete = new List<int>();
            for (var i = 0; i < dgv.SelectedCells.Count; ++i)
                rowsToDelete.Add(dgv.SelectedCells[i].RowIndex);
            foreach (var row in rowsToDelete.Distinct())
            {
                dgv.Rows[row].Cells["IsDelete"].Value = true;
                if (isNotNullAndTrue(dgv.Rows[row].Cells["IsNew"].Value))
                    dgv.Rows.RemoveAt(row);
                else
                {
                    dgv.CurrentCell = null;
                    dgv.Rows[row].Visible = false;
                }
            }
        }

        public bool IsHaveChanges()
        {
            for (int i = 0; i < dgv.Rows.Count; ++i)
                if (isNotNullAndTrue(dgv.Rows[i].Cells["IsNew"].Value) ||
                    isNotNullAndTrue(dgv.Rows[i].Cells["IsModify"].Value) ||
                    isNotNullAndTrue(dgv.Rows[i].Cells["IsDelete"].Value))
                    return true;
            return false;
        }

        public void SaveChanges()
        {
            saveButton_Click(null, null);
        }

        private void dataGrid_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < dgv.Rows.Count; ++i)
                if (isNotNullAndTrue(dgv.Rows[i].Cells["IsDelete"].Value))
                {
                    if (dgv.CurrentRow != null && dgv.CurrentRow.Index == i)
                        dgv.CurrentCell = null;
                    dgv.Rows[i].Visible = false;
                }
        }
    }
}
