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
    public partial class UCVariablesList : UserControl
    {
        public enum ROWS_VIEW
        {
            ALL,
            MINIMUM
        }

        private ROWS_VIEW rowsViewMode = ROWS_VIEW.ALL;
        public ROWS_VIEW RowsViewMode {
            get { return rowsViewMode; }
            set
            {
                rowsViewMode = value;
                foreach (DataGridViewColumn col in dgv.Columns)
                    col.Visible = false;
                switch (rowsViewMode)
                {
                    case ROWS_VIEW.ALL:
                        foreach (DataGridViewColumn col in dgv.Columns)
                        {
                            col.Visible = true;
                            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
                        }
                        break;
                    case ROWS_VIEW.MINIMUM:
                        dgv.Columns["id"].Visible = true;
                        dgv.Columns["nameRus"].Visible = true;
                        break;
                }
            }
        }

        public UCVariablesList()
        {
            InitializeComponent();

            dgv.SortCompare += new DataGridViewSortCompareEventHandler(this.dataGridView_SortCompare);
        }

        public void Fill(List<Variable> vc)
        {
            if (!DesignMode)
            {
                dgv.Rows.Clear();
                foreach (var var in vc)
                {
                    DataGridViewRow row = dgv.Rows[dgv.Rows.Add()];
                    row.Tag = var;

                    row.Cells["id"].Value = var.Id;
                    row.Cells["nameRus"].Value = var.NameRus;
                    row.Cells["unitsTimeName"].Value = UnitRepository.GetCash().Find(x => x.Id == var.TimeId).Name;
                    row.Cells["timeSupport"].Value = var.TimeSupport;
                    row.Cells["unitsName"].Value = UnitRepository.GetCash().Find(x => x.Id == var.UnitId).Name;
                    row.Cells["valueTypeName"].Value = ValueTypeRepository.GetCash().Find(pet => pet.Id == var.ValueTypeId).Name;
                    row.Cells["dataTypeName"].Value = DataTypeRepository.GetCash().Find(pet => pet.Id == var.DataTypeId).Name;
                    row.Cells["variableName"].Value = VariableTypeRepository.GetCash().Find(pet => pet.Id == var.VariableTypeId).Name;
                    row.Cells["generalCategory"].Value = GeneralCategoryRepository.GetCash().Find(pet => pet.Id == var.GeneralCategoryId).Name;
                    row.Cells["sampleMedium"].Value = SampleMediumRepository.GetCash().Find(pet => pet.Id == var.SampleMediumId).Name;
                    row.Cells["name_eng"].Value = var.NameEng;
                    row.Cells["code_no_data"].Value = var.CodeNoData;
                    row.Cells["code_err_data"].Value = var.CodeErrData;
                }
                infoLabel.Text = dgv.Rows.Count.ToString();

                dgv.Columns["nameRus"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
                RaiseUCVariableChangedEvent(SelectedVariable);
            }
        }
        public bool ShowToolBox
        {
            set
            {
                toolStrip1.Visible = value;
            }
            get
            {
                return toolStrip1.Visible;
            }
        }
        public bool ShowFilterButton
        {
            set
            {
                filterButton.Visible = value;
            }
            get
            {
                return filterButton.Visible;
            }
        }
        private void mnuShowToolBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowToolBox = !ShowToolBox;
        }
        #region EVENTS
        public delegate void UCVariableChangedEventHandler(Variable var);
        public event UCVariableChangedEventHandler UCVariableChangedEvent;
        protected virtual void RaiseUCVariableChangedEvent(Variable var)
        {
            if (UCVariableChangedEvent != null)
            {
                UCVariableChangedEvent(var);
            }
        }
        #endregion

        private void dgv_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            RaiseUCVariableChangedEvent(SelectedVariable);
        }

        public int SelectedId
        {
            set
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (((Variable)row.Tag).Id == value)
                    {
                        dgv.ClearSelection();
                        row.Selected = true;
                        RaiseUCVariableChangedEvent(SelectedVariable);
                    }
                }
            }
        }
        public Variable SelectedVariable
        {
            get
            {
                return dgv.SelectedRows.Count > 0 ? (Variable)dgv.SelectedRows[0].Tag : null;
            }
        }
        FormVariableFilter FormVariableFilter { get; set; }
        private void filterButton_Click(object sender, EventArgs e)
        {
            if (FormVariableFilter == null)
            {
                FormVariableFilter = new FormVariableFilter();
                FormVariableFilter.StartPosition = FormStartPosition.CenterScreen;
            }
            if (FormVariableFilter.ShowDialog() == DialogResult.OK)
            {
                VariableFilter vf = FormVariableFilter.VariableFilter;
                Fill(DataManager.GetInstance().VariableRepository.Select(
                    vf.VariableTypeIds,
                    vf.TimeIds, vf.UnitIds,
                    vf.DataTypeIds,
                    vf.GeneralCategoryIds,
                    vf.SampleMediumIds,
                    vf.TimeSupports,
                    vf.ValueTypeIds));
            }
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
    }
}
