using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace SOV.Common
{
    public partial class UCDicListBox : UserControl
    {
        public UCDicListBox()
        {
            InitializeComponent();
            //ShowAddNewToolbarButton = ShowDeleteToolbarButton = ShowUpdateToolbarButton = false;
        }

        public void Clear()
        {
            if (dgv.Rows.Count > 0)
            {
                dgv.Rows.Clear();
                UpdateInfoText();
                //RaiseSelectedItemChanged(null);
            }
        }

        public void AddRange(List<DicItem> dic)
        {
            LayoutColumns = false;
            for (int i = 0; i < dic.Count; i++)
            {
                Add(dic[i]);
            }
            LayoutColumns = true;
        }
        public void Fill(List<DicItem> dic)
        {
            AddRange(dic);
            RaiseSelectedItemChanged(CurrentDicItem);
        }
        public void AddRange(Dictionary<int, string> dic)
        {
            LayoutColumns = false;
            foreach (var kvp in dic)
            {
                Add(new DicItem() { Id = kvp.Key, Name = kvp.Value });
            }
            LayoutColumns = true;
        }
        public int Add(DicItem dic, bool isChecked = false)
        {
            int i = dgv.Rows.Add(new object[] { "0", dic.Name, dic.Id });
            dgv.Rows[i].Tag = dic;
            if (isChecked)
                Check(dgv.Rows[i]);
            UpdateInfoText();
            return i;
        }
        public List<DicItem> GetDicCopy()
        {
            List<DicItem> ret = new List<DicItem>();
            foreach (DataGridViewRow item in dgv.Rows)
            {
                ret.Add(new DicItem((int)item.Cells["id"].Value, item.Cells["name"].Value as string));
            }
            return ret;
        }
        public List<DicItem> GetDicis()
        {
            List<DicItem> ret = new List<DicItem>();
            foreach (DataGridViewRow item in dgv.Rows)
            {
                ret.Add((DicItem)item.Tag);
            }
            return ret;
        }
        public List<DicItem> CheckedDicItems
        {
            get
            {
                List<DicItem> ret = new List<DicItem>();
                List<int> checkedId = CheckedId;

                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    DicItem dici = (DicItem)dgv.Rows[i].Tag;
                    if (checkedId.Exists(x => x == dici.Id))
                        ret.Add(dici);
                }
                return ret;
            }
        }
        public List<int> CheckedId
        {
            get
            {
                List<int> ret = new List<int>();
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    if (IsChecked(dgv.Rows[i]))
                        ret.Add((int)dgv.Rows[i].Cells[2].Value);
                }
                return ret;
            }
            set
            {
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dgv.Rows[i].Cells[0];

                    if (value.IndexOf((int)dgv.Rows[i].Cells[2].Value) >= 0)
                        cell.Value = cell.TrueValue;
                    else
                        cell.Value = cell.FalseValue;
                }
                UpdateInfoText();
            }
        }
        public int SelectedRowId
        {
            set
            {
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    dgv.Rows[i].Selected = false;

                    DicItem di = (DicItem)dgv.Rows[i].Tag;
                    if (di.Id == value)
                    {
                        dgv.Rows[i].Selected = true;
                        return;
                    }
                }
            }
            get
            {
                if (dgv.SelectedRows.Count > 0)
                    return ((DicItem)dgv.SelectedRows[0].Tag).Id;
                return -1;
            }
        }
        private void Check(DataGridViewRow row)
        {
            ((DataGridViewCheckBoxCell)row.Cells[0]).Value = ((DataGridViewCheckBoxCell)row.Cells[0]).TrueValue;
        }
        public void Check(int id)
        {
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                if (((DicItem)dgv.Rows[i].Tag).Id == id)
                {
                    Check(dgv.Rows[i]);
                    return;
                }
            }
        }
        public List<int> UncheckedId
        {
            get
            {
                List<int> ret = new List<int>();
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    if (!IsChecked(dgv.Rows[i]))
                        ret.Add((int)dgv.Rows[i].Cells[2].Value);
                }
                return ret;
            }
        }
        public int Count
        {
            get
            {
                return dgv.RowCount;
            }
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            SelectAll();
        }
        public void SelectAll()
        {
            LayoutColumns = false;
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                dgv.Rows[i].Cells[0].Value = ((DataGridViewCheckBoxColumn)dgv.Columns[0]).TrueValue;
            }
            UpdateInfoText();
            LayoutColumns = true;
        }
        bool LayoutColumns
        {
            set
            {
                if (value)
                {
                    dgv.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dgv.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dgv.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                else
                {
                    dgv.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
                    dgv.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
                    dgv.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
                }
            }
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            UnselectAll();
        }
        public void UnselectAll()
        {
            LayoutColumns = false;
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                dgv.Rows[i].Cells[0].Value = ((DataGridViewCheckBoxColumn)dgv.Columns[0]).FalseValue;
            }
            UpdateInfoText();
            LayoutColumns = true;
        }

        public void UpdateInfoText()
        {
            infoToolStripLabel.Text = CheckedId.Count + "/" + Count;
        }

        private void showSelectedToolStripButton_Click(object sender, EventArgs e)
        {
            ShowSelectedOnly = !ShowSelectedOnly;
        }
        bool _ShowSelectedOnly = false;
        public bool ShowSelectedOnly
        {
            get { return _ShowSelectedOnly; }
            set
            {
                DataGridViewRow[] rows = new DataGridViewRow[dgv.Rows.Count];
                dgv.Rows.CopyTo(rows, 0);
                dgv.Rows.Clear();

                for (int i = 0; i < rows.Length; i++)
                {
                    DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)rows[i].Cells[0];
                    rows[i].Visible = value || cell.Value == cell.TrueValue ? true : false;
                }
                dgv.Rows.AddRange(rows);
                //selectAllToolStripButton.Visible = value;
                //unselectAllToolStripButton.Enabled = !value;
                showSelectedToolStripButton.ToolTipText = (value) ? "Показать все строки" : "Показать только выбранные строки";
                _ShowSelectedOnly = value;
            }
        }

        private void findToolStripTextBox_TextChanged(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count > 0)
                dgv.SelectedRows[0].Selected = false;
        }

        private void findNextToolStripButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(findToolStripTextBox.Text) && dgv.Rows.Count > 0)
            {
                int iStart = 0;
                if (dgv.SelectedRows.Count > 0) { iStart = dgv.SelectedRows[0].Index + 1; dgv.SelectedRows[0].Selected = false; }
                if (iStart > dgv.Rows.Count) iStart = 0;

                for (int i = iStart; i < dgv.Rows.Count; i++)
                {
                    if (((DataGridViewTextBoxCell)dgv.Rows[i].Cells[1]).Value.ToString().ToUpper().IndexOf(findToolStripTextBox.Text.ToUpper()) >= 0)
                    {
                        dgv.Rows[i].Selected = true;
                        dgv.FirstDisplayedScrollingRowIndex = dgv.SelectedRows[0].Index;
                        break;
                    }
                }
            }
        }

        public bool ShowToolbar
        {
            get
            {
                return toolStrip1.Visible;
            }
            set
            {
                toolStrip1.Visible = value;
            }
        }
        public bool ShowCheckBox
        {
            get
            {
                return dgv.Columns["checkbox"].Visible;
            }
            set
            {
                dgv.Columns["checkbox"].Visible = value;
            }
        }
        public bool ShowColumnHeaders
        {
            get
            {
                return dgv.ColumnHeadersVisible;
            }
            set
            {
                dgv.ColumnHeadersVisible = value;
            }
        }
        public bool ShowId
        {
            get
            {
                return dgv.Columns["id"].Visible;
            }
            set
            {
                dgv.Columns["id"].Visible = value;
            }
        }
        public bool ColumnsHeadersVisible
        {
            get
            {
                return dgv.ColumnHeadersVisible;
            }
            set
            {
                dgv.ColumnHeadersVisible = value;
            }
        }

        public int IndexOf(int id)
        {
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                if (((DicItem)dgv.Rows[i].Tag).Id == id)
                    return i;
            }
            return -1;
        }

        public void Remove(List<int> dicId)
        {
            dgv.SuspendLayout();
            for (int i = 0; i < dgv.Rows.Count; )
            {
                DataGridViewRow row = dgv.Rows[i];
                if (dicId.Exists(pet => pet == ((DicItem)row.Tag).Id))
                {
                    dgv.Rows.Remove(row);
                    continue;
                }
                i++;
            }
            dgv.ResumeLayout();
            UpdateInfoText();
        }

        string _dicItemName;
        public string DicItemName
        {
            get
            {
                return _dicItemName;
            }
            set
            {
                dgv.Columns["name"].HeaderText = value == null ? "Название" : value;
                _dicItemName = value;
            }
        }

        private void upButton_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count == 1 && dgv.SelectedRows[0].Index != 0)
            {
                int i = dgv.SelectedRows[0].Index;
                DataGridViewRow row = dgv.Rows[i];
                dgv.Rows.Remove(row);
                dgv.Rows.Insert(i - 1, row);

                row.Selected = true;

                RaiseDicItemOrderChangedEvent();
            }
        }

        private void downButton_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count == 1 && dgv.SelectedRows[0].Index != Count - 1)
            {
                int i = dgv.SelectedRows[0].Index;
                DataGridViewRow row = dgv.Rows[i];
                dgv.Rows.Remove(row);
                dgv.Rows.Insert(i + 1, row);

                row.Selected = true;

                RaiseDicItemOrderChangedEvent();
            }
        }
        public bool ShowDeleteToolbarButton
        {
            get
            {
                return deleteToolStripButton.Visible;
            }
            set
            {
                deleteToolStripButton.Visible = value;
            }
        }
        public bool ShowSelectAllToolbarButton
        {
            get
            {
                return selectAllToolStripButton.Visible;
            }
            set
            {
                selectAllToolStripButton.Visible = value;
            }
        }
        public bool ShowOrderToolbarButton
        {
            get
            {
                return showOrderToolStripButton.Visible;
            }
            set
            {
                showOrderToolStripButton.Visible = toolStripSeparator2.Visible = value;
            }
        }
        public bool ShowFindItemToolbarButton
        {
            get
            {
                return findNextToolStripButton.Visible;
            }
            set
            {
                findNextToolStripButton.Visible = findToolStripTextBox.Visible = toolStripSeparator1.Visible = value;
            }
        }
        public bool ShowSelectedOnlyToolbarButton
        {
            get
            {
                return showSelectedToolStripButton.Visible;
            }
            set
            {
                showSelectedToolStripButton.Visible = value;
            }
        }
        public bool ShowUnselectAllToolbarButton
        {
            get
            {
                return unselectAllToolStripButton.Visible;
            }
            set
            {
                unselectAllToolStripButton.Visible = value;
            }
        }
        public bool ShowAddNewToolbarButton
        {
            get
            {
                return addNewToolStripButton.Visible;
            }
            set
            {
                addNewToolStripButton.Visible = value;
            }
        }
        public bool ShowSaveToolbarButton
        {
            get
            {
                return saveToolStripButton.Visible;
            }
            set
            {
                saveToolStripButton.Visible = value;
            }
        }
        public bool ShowUpdateToolbarButton
        {
            get
            {
                return updateToolStripButton.Visible;
            }
            set
            {
                updateToolStripButton.Visible = value;
            }
        }
        public bool ShowOrderControls
        {
            get
            {
                return tableLayoutPanel1.ColumnStyles[1].SizeType == SizeType.AutoSize;
            }
            set
            {
                if (value)
                    tableLayoutPanel1.ColumnStyles[1].SizeType = SizeType.AutoSize;
                else
                    tableLayoutPanel1.ColumnStyles[1].SizeType = SizeType.Absolute;
            }
        }
        /// <summary>
        /// От первой строки к последней.
        /// </summary>
        public List<DicItem> DicItemList
        {
            get
            {
                List<DicItem> ret = new List<DicItem>();
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    ret.Add((DicItem)dgv.Rows[i].Tag);
                }
                return ret;
            }
        }

        public bool AllowMultiSelect
        {
            get
            {
                return ShowCheckBox;
            }
            set
            {
                ShowCheckBox = value;
            }
        }

        private void showOrderToolStripButton_Click(object sender, EventArgs e)
        {
            ShowOrderControls = !ShowOrderControls;
        }

        #region EVENTS
        public delegate void UCSelectedItemChangedEventHandler(DicItem dici);
        public event UCSelectedItemChangedEventHandler UCSelectedItemChanged;
        protected virtual void RaiseSelectedItemChanged(DicItem dici)
        {
            if (UCSelectedItemChanged != null)
            {
                UCSelectedItemChanged(dici);
            }
        }
        public delegate void DicItemOrderChangedEventHandler();
        public event DicItemOrderChangedEventHandler DicItemOrderChangedEvent;
        protected virtual void RaiseDicItemOrderChangedEvent()
        {
            if (DicItemOrderChangedEvent != null)
            {
                DicItemOrderChangedEvent();
            }
        }
        public delegate void UCAddNewEventHandler();
        public event UCAddNewEventHandler UCAddNewEvent;
        protected virtual void RaiseUCAddNewEvent()
        {
            if (UCAddNewEvent != null)
            {
                UCAddNewEvent();
            }
        }
        public delegate void UCDeleteEventHandler(int id);
        public event UCDeleteEventHandler UCDeleteEvent;
        protected virtual void RaiseUCDeleteEvent(int id)
        {
            if (UCDeleteEvent != null)
            {
                UCDeleteEvent(id);
            }
        }
        public delegate void UCUpdateEventHandler(int id);
        public event UCUpdateEventHandler UCUpdateEvent;
        protected virtual void RaiseUCUpdateEvent(int id)
        {
            if (UCUpdateEvent != null)
            {
                UCUpdateEvent(id);
            }
        }
        public delegate void UCSaveEventHandler();
        public event UCSaveEventHandler UCSaveEvent;
        protected virtual void RaiseUCSaveEvent(int id)
        {
            if (UCSaveEvent != null)
            {
                UCSaveEvent();
            }
        }
        public delegate void UCItemCheckedEventHandler(List<int> checkedId);
        public event UCItemCheckedEventHandler UCItemCheckedEvent;
        protected virtual void RaiseUCItemCheckedEvent()
        {
            if (UCItemCheckedEvent != null)
            {
                UCItemCheckedEvent(CheckedId);
            }
        }
        #endregion


        private void mnuShowToolbarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowToolbar = !ShowToolbar;
        }

        private void addNewToolStripButton_Click(object sender, EventArgs e)
        {
            RaiseUCAddNewEvent();
        }

        private void updateToolStripButton_Click(object sender, EventArgs e)
        {
            RaiseUCUpdateEvent((int)CurrentDicItemId);
        }

        private void deleteToolStripButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить выбранную строку таблицы?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                RaiseUCDeleteEvent((int)CurrentDicItemId);
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            RaiseUCSaveEvent((int)CurrentDicItemId);
        }
        private void dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.Columns[e.ColumnIndex].Name == "checkbox")
            {
                UpdateInfoText();
                RaiseUCItemCheckedEvent();
            }
        }
        bool IsChecked(DataGridViewRow row)
        {
            DataGridViewCheckBoxCell cellCheck = (DataGridViewCheckBoxCell)row.Cells[0];
            return (cellCheck.Value == cellCheck.TrueValue);
        }
        public int? CurrentDicItemId
        {
            get
            {
                if (dgv.SelectedRows.Count == 1)
                    return ((DicItem)dgv.SelectedRows[0].Tag).Id;
                return null;
            }
            set
            {
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    dgv.Rows[i].Selected = false;
                    if (value == null && value == ((DicItem)dgv.Rows[i].Tag).Id)
                        dgv.Rows[i].Selected = true;
                }
            }
        }
        public DicItem CurrentDicItem
        {
            get
            {
                if (dgv.SelectedRows.Count == 1)
                    return (DicItem)dgv.SelectedRows[0].Tag;
                return null;
            }
        }

        private void dgv_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            RaiseSelectedItemChanged(CurrentDicItem);
        }
        public void HideTollbarControls()
        {
            ShowAddNewToolbarButton =
            ShowColumnHeaders =
            ShowDeleteToolbarButton =
            ShowFindItemToolbarButton =
            ShowId =
            ShowOrderControls =
            ShowOrderToolbarButton =
            ShowSaveToolbarButton =
            ShowCheckBox =
            ShowSelectAllToolbarButton =
            ShowToolbar =
            ShowUnselectAllToolbarButton =
            ShowUpdateToolbarButton =
            ShowSelectedOnly =

            false;
        }

        public delegate void UCDoubleClickEventHandler();
        public event UCDoubleClickEventHandler UCDoubleClick;
        private void dgv_DoubleClick(object sender, EventArgs e)
        {
            if (UCDoubleClick != null)
            {
                UCDoubleClick();
            }
        }
    }
}
