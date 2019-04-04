using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SOV.Common.TableIUD
{
    public partial class UCTreeTableViewer<T> : UserControl, TableViewer<T> where T : class
    {
        public List<TableField> Fields { get; set; }
        public event TableViewerMouseEvent OnViewerMouseUp;
        public event TableViewerDragEvent OnViewerDragDrop;

        public UCTreeTableViewer(List<TableField> _fields)
        {
            InitializeComponent();
            Fields = _fields;
        }

        public T SelectedItem()
        {
            return tree.SelectedNode != null ? (T)tree.SelectedNode.Tag : null;
        }

        public void RefreshData<ElmT>(List<ElmT> data) where ElmT : class
        {
            var selectedName = tree.SelectedNode != null ? tree.SelectedNode.Name : "";
            tree.Nodes.Clear();
            FillTree(tree.Nodes, data.Select(x => x as DicItem).ToList());
            var selectedNode = tree.Nodes.Find(selectedName, true);
            if (selectedNode.Length > 0)
                tree.SelectedNode = selectedNode[0];
        }

        private void FillTree(TreeNodeCollection node, List<DicItem> items)
        {
            foreach (var item in items)
            {
                node.Add(item.Id.ToString(), item.Name);
                node[node.Count - 1].Tag = item.Entity;
                FillTree(node[node.Count - 1].Nodes, item.Childs);
            }
        }

        private void dataGrid_MouseUp(object sender, MouseEventArgs e)
        {
            if (OnViewerMouseUp != null) OnViewerMouseUp(sender, e);
        }

        private void tree_DragDrop(object sender, DragEventArgs e)
        {
            if (OnViewerDragDrop != null) OnViewerDragDrop(sender, e);
        }

        private void tree_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void tree_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }
    }
}
