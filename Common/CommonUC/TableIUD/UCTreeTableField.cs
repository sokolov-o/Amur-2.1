using System.Collections.Generic;
using System.Windows.Forms;

namespace SOV.Common.TableIUD
{
    public partial class UCTreeTableField : UCTableField
    {
        public UCTreeTableField(TreeTableField field, object currVal) : base(field)
        {
            InitializeComponent();
            label.Text = field.title;
            this.ucInput = tree;
            FillTree(tree.Nodes, field.tree);
            if (currVal == null)
                return;
            var selectedNode = tree.Nodes.Find(currVal.ToString(), true);
            if (selectedNode.Length > 0)
                tree.SelectedNode = selectedNode[0];
        }

        private void FillTree(TreeNodeCollection node, List<DicItem> items)
        {
            foreach (var item in items)
            {
                node.Add(item.Id.ToString(), item.Name);
                FillTree(node[node.Count - 1].Nodes, item.Childs);
            }
        }

        private void tree_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void tree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (onChange != null) onChange(this);
        }

        public override KeyValuePair<string, object> NameAndVal()
        {
            return new KeyValuePair<string, object>(Field.db, tree.SelectedNode == null ? null : tree.SelectedNode.Name);
        }
    }
}
