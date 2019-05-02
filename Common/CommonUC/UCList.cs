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
    /// <summary>
    /// IdClass items as datasource for datagridview.
    /// </summary>
    public partial class UCList : UserControl
    {
        public UCList()
        {
            InitializeComponent();
        }

        public void Clear()
        {
            itemsBindingSource.Clear();
            UpdateInfoText();
            RaiseSelectedItemChanged();
        }
        /// <summary>
        /// 
        /// IdClasss data source.
        /// 
        /// </summary>
        /// <param name="items"></param>
        public void SetDataSource(List<object> items, string dataPropertyName)
        {
            Clear();
            itemsBindingSource.DataSource = items;
            this.name.DataPropertyName = dataPropertyName;

            infoToolStripLabel.Text = items.Count.ToString();

            RaiseSelectedItemChanged();
        }
        public void SetDataSource(List<IdName> items)
        {
            SetDataSource(items.ToList<object>(), "Name");
        }
        public void AddRange(List<object> items)
        {
            if (itemsBindingSource.DataSource == null)
                throw new Exception("Предварительно необходим вызов метода SetDataSource.");

            itemsBindingSource.EndEdit();
            dgv.EndEdit();
            dgv.SuspendLayout();
            foreach (var item in items)
            {
                itemsBindingSource.Add(item);
            }
            dgv.ResumeLayout();
        }
        public void Remove(List<object> items)
        {
            foreach (var item in items)
            {
                itemsBindingSource.Remove(item);
            }
        }
        public void RemoveById(List<int> ids)
        {
            List<object> os = new List<object>();
            foreach (var item in itemsBindingSource)
            {
                if (ids.Exists(x => x == ((IdClass)item).Id))
                    os.Add(item);
            }
            os.ForEach(x => itemsBindingSource.Remove(x));
        }

        public List<object> GetDataSource()
        {
            List<object> ret = new List<object>();
            foreach (var item in itemsBindingSource)
            {
                ret.Add(item);
            }
            return ret;
        }
        /// <summary>
        /// Получить набор всех выбранных элементов в списке или null.
        /// </summary>
        /// <returns>null is IsAllSelected or selected items if not.</returns>
        public List<object> GetSelectedItems()
        {
            //List<object> ret = null;
            //if (!AreAllItemsSelected)
            //{
            List<object> ret = new List<object>();

            foreach (DataGridViewRow row in dgv.SelectedRows)
            {
                ret.Add(row.DataBoundItem);
            }
            //}
            return ret;
        }
        /// <summary>
        /// Возвращает null, если не выбран элемент или выбрано несколько.
        /// </summary>
        /// <returns></returns>
        public object GetSelectedItem()
        {
            List<object> ret = GetSelectedItems();
            return ret == null || ret.Count != 1 ? null : ret[0];
        }
        public List<int> GetSelectedItemsId()
        {
            return GetSelectedItems().Select(x => ((IdClass)x).Id).ToList();
        }
        public void UnselectAll()
        {
            SetSelectedItemsById(new List<int>());
        }
        public void SetSelectedItemsById(List<int> ids)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (ids.Exists(x => x == ((IdClass)row.DataBoundItem).Id))
                    row.Selected = true;
                else
                    row.Selected = false;
            }
            UpdateInfoText();
            RaiseSelectedItemChanged();
        }
        public void SetSelectedItemById(int id_)
        {
            // не работает itemsBindingSource.Position = itemsBindingSource.Find("Id", id_);
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                if (id_ == ((IdClass)dgv.Rows[i].DataBoundItem).Id)
                {
                    dgv.Rows[i].Selected = true;
                    dgv.FirstDisplayedScrollingRowIndex = i;
                }
                else
                    dgv.Rows[i].Selected = false;
            }
            UpdateInfoText();
            RaiseSelectedItemChanged();
        }

        public int Count
        {
            get
            {
                return itemsBindingSource.Count;
            }
        }
        //bool _isAllSelected = false;
        //public bool AreAllItemsSelected
        //{
        //    get
        //    {
        //        return _isAllSelected;
        //    }
        //    set
        //    {
        //        dgv.Enabled = !value;
        //        selectAllToolStripButton.BackColor = value ? Color.Red : SystemColors.Control;

        //        _isAllSelected = value;
        //        RaiseSelectedItemChanged();
        //    }
        //}
        private void selectAllButton_Click(object sender, EventArgs e)
        {
            dgv.SuspendLayout();
            foreach (DataGridViewRow item in dgv.Rows)
            {
                item.Selected = true;
            }
            dgv.ResumeLayout();
            //AreAllItemsSelected = !AreAllItemsSelected;
        }
        void SetLayoutColumnsOn(bool value)
        {
            if (value)
            {
                dgv.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgv.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            else
            {
                dgv.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
                dgv.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
            }
        }
        private void unselectAllButton_Click(object sender, EventArgs e)
        {
            SetSelectedItemsById(new List<int>());
        }

        public void UpdateInfoText()
        {
            infoToolStripLabel.Text = dgv.SelectedRows.Count + "/" + Count;
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
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (value)
                        row.Visible = dgv.SelectedRows.Contains(row);
                    else
                        row.Visible = true;
                }
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
            if (!string.IsNullOrEmpty(findToolStripTextBox.Text) && itemsBindingSource.Count > 0)
            {
                int iSel = (dgv.FirstDisplayedScrollingRowIndex + 1 == itemsBindingSource.Count) ? 0 : dgv.FirstDisplayedScrollingRowIndex + 1;

                for (int i = iSel; i < dgv.Rows.Count; i++)
                {
                    if (dgv.Rows[i].Cells[0].Value.ToString().ToUpper().IndexOf(findToolStripTextBox.Text.ToUpper()) > -1)
                    {
                        dgv.FirstDisplayedScrollingRowIndex = i;
                        return;
                    }
                }
                Console.Beep();
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

        private void upButton_Click(object sender, EventArgs e)
        {
            MoveItem(-1);
        }

        private void downButton_Click(object sender, EventArgs e)
        {
            MoveItem(+1);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updown">-1 up, +1 down</param>
        private void MoveItem(int updown)
        {
            if (dgv.SelectedRows.Count == 1 && ((updown == -1 && dgv.SelectedRows[0].Index != 0) || (updown == +1 && dgv.SelectedRows[0].Index != Count - 1)))
            {
                int i = dgv.SelectedRows[0].Index;
                object o = itemsBindingSource[i];

                itemsBindingSource.RemoveAt(i);
                itemsBindingSource.Insert(i + updown, o);

                dgv.Rows[i + updown].Selected = true;

                RaiseUCItemOrderChangedEvent();
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
                findNextToolStripButton.Visible = findToolStripTextBox.Visible = value;
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

        public bool MultiSelect
        {
            get
            {
                return dgv.MultiSelect;
            }
            set
            {
                dgv.MultiSelect = value;
            }
        }

        private void showOrderToolStripButton_Click(object sender, EventArgs e)
        {
            ShowOrderControls = !ShowOrderControls;
        }

        #region EVENTS
        public delegate void UCSelectedItemChangedEventHandler();
        public event UCSelectedItemChangedEventHandler UCSelectedItemChanged;
        protected virtual void RaiseSelectedItemChanged()
        {
            if (UCSelectedItemChanged != null)
            {
                UCSelectedItemChanged();
            }
        }
        public delegate void UCItemOrderChangedEventHandler();
        public event UCItemOrderChangedEventHandler UCItemOrderChangedEvent;
        protected virtual void RaiseUCItemOrderChangedEvent()
        {
            if (UCItemOrderChangedEvent != null)
            {
                UCItemOrderChangedEvent();
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
        public delegate void UCUpdateEventHandler();
        public event UCUpdateEventHandler UCUpdateEvent;
        protected virtual void RaiseUCUpdateEvent()
        {
            if (UCUpdateEvent != null)
            {
                UCUpdateEvent();
            }
        }
        public delegate void UCSaveEventHandler();
        public event UCSaveEventHandler UCSaveEvent;
        protected virtual void RaiseUCSaveEvent()
        {
            if (UCSaveEvent != null)
            {
                UCSaveEvent();
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
            if (itemsBindingSource.Current != null)
                RaiseUCUpdateEvent();
        }

        private void deleteToolStripButton_Click(object sender, EventArgs e)
        {
            if (CurrentId != null)
                if (MessageBox.Show("Вы уверены, что хотите удалить выбранную строку таблицы?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    RaiseUCDeleteEvent((int)CurrentId);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            RaiseUCSaveEvent();
        }
        /// <summary>
        /// Id текущего (выбранного) элемента. выбрано может быть более одного элемента.
        /// </summary>
        public int? CurrentId
        {
            get
            {
                return (itemsBindingSource.Current != null) ? (int?)((IdClass)itemsBindingSource.Current).Id : null;
            }
        }

        private void dgv_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            RaiseSelectedItemChanged();
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
