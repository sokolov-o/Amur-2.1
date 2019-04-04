using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace SOV.Common.TableIUD
{
    public partial class UCGridTableViewer<T> : UserControl, TableViewer<T> where T : class
    {
        public List<TableField> Fields { get; set; }
        public event TableViewerMouseEvent OnViewerMouseUp;
        public event TableViewerDragEvent OnViewerDragDrop;

        public UCGridTableViewer(List<TableField> _fields)
        {
            InitializeComponent();
            Fields = _fields;
            foreach (var field in Fields)
                using (var col = new DataGridViewColumn())
                {
                    if (!field.inView)
                        continue;
                    col.HeaderText = field.title;
                    col.Name = field.db;
                    if (field.type == TableFieldUCType.ImageInput || field.type == TableFieldUCType.ImageGallery)
                        col.CellTemplate = new DataGridViewImageCell();
                    else
                        col.CellTemplate = new DataGridViewTextBoxCell();

                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    col.SortMode = DataGridViewColumnSortMode.Automatic;
                    dataGrid.Columns.Add(col);
                }
            dataGrid.Sort(dataGrid.Columns[0], ListSortDirection.Ascending);
        }

        public T SelectedItem()
        {
            return (T)dataGrid.SelectedRows[0].Tag;
        }

        public void RefreshData<ElmT>(List<ElmT> data) where ElmT : class
        {
            int selected = Int32.MaxValue;
            if (dataGrid.SelectedRows.Count > 0)
                selected = dataGrid.SelectedRows[0].Index;
            dataGrid.Rows.Clear();
            foreach (var dataElm in data)
            {
                dataGrid.Rows.Add(Fields.Where(x => x.inView).Select(x => x.ObjView<ElmT>(dataElm)).ToArray());
                dataGrid.Rows[dataGrid.RowCount - 1].Tag = dataElm;
            }
            if (dataGrid.SortOrder != SortOrder.None)
                dataGrid.Sort(
                    dataGrid.SortedColumn,
                    dataGrid.SortOrder == SortOrder.Ascending ? ListSortDirection.Ascending : ListSortDirection.Descending
                );
            if (dataGrid.RowCount > selected)
                dataGrid.Rows[selected].Selected = true;
        }

        private void dataGrid_MouseUp(object sender, MouseEventArgs e)
        {
            if (OnViewerMouseUp != null) OnViewerMouseUp(sender, e);
        }

        private void dataGrid_DragDrop(object sender, DragEventArgs e)
        {
            if (OnViewerDragDrop != null) OnViewerDragDrop(sender, e);
        }
    }
}
