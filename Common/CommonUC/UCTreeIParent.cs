using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOV.Common
{
    /// <summary>
    /// Using:
    ///     Clear();
    ///     AddRange(...);
    ///     SetSelectedNode(...);
    /// </summary>
    public partial class UCTreeIParent : UserControl
    {
        public UCTreeIParent()
        {
            InitializeComponent();
        }
        private void refreshButton_Click(object sender, EventArgs e)
        {
            RaiseRefresh();
        }
        /// <summary>
        /// Добавление корневых узлов из массива и к ним веткок из items. 
        /// 
        /// Интерфейс IParent используется, но не обязателен, можно и без него.
        /// Наличие такого интерфейса проверяется у первого ключа словаря (предполагается, что остальные ключи тоже имеют такой интерфейс).
        /// 
        /// 2. В обязательном порядке интерфейс IParent используется для items.
        /// 
        /// </summary>
        /// <param name="roots_items">Корни дерева (IClass) и соответствующие им ветки IParent.</param>
        public void AddRange(Dictionary<object, object[]> roots_items, Enums.TreeView treeView, Color? textColor)
        {
            // ADD ROOTS
            object[] items = roots_items.Keys.OrderBy(x => x.ToString()).ToArray();
            tv.Nodes.AddRange(GetTreeNodes(items, treeView, textColor));

            // ADD CHILDS
            foreach (TreeNode node in tv.Nodes)
            {
                node.Nodes.AddRange(GetTreeNodes(node, roots_items, treeView, null));
            }
            infoLabel.Text = tv.Nodes.Count.ToString();
        }

        //>>>>>>>>>>>>>>>>>>>>>>>>>>   IdNameTree   >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public void AddRange(List<IdNameTree> items, Color? textColor = null)
        {
            foreach (var item in items)
            {
                Add(item, null);
            }
            infoLabel.Text = tv.Nodes.Count.ToString();
        }
        private void Add(IdNameTree item, TreeNode nodeParent)
        {
            TreeNode node = new TreeNode();

            node.Text = item.ToString();
            node.Name = item.Id.ToString();
            node.Tag = item;

            if (tv.ImageList != null)
            {
                node.ImageIndex = tv.ImageList.Images.IndexOfKey(item.Entity.GetType().ToString());
                node.SelectedImageIndex = node.ImageIndex;
            }

            if (item.Childs != null)
            {
                foreach (var item1 in item.Childs)
                {
                    Add(item1, node);
                }
            }

            TreeNodeCollection nodes = (nodeParent == null) ? tv.Nodes : nodeParent.Nodes;
            nodes.Add(node);
        }
        public void RemoveNode(int id, Type nodeTagTypeOf)
        {
            TreeNode node = FindNode(id, nodeTagTypeOf);
            if (node != null)
            {
                tv.Nodes.Remove(node);
                return;
            }
            Console.Beep(10000, 2000);
        }
        public TreeNode FindNode(int id, Type nodeTagTypeOf)
        {
            IEnumerable<TreeNode> nodes = tv.Nodes.Find(id.ToString(), true).Where(x => ((IdName)x.Tag).Entity.GetType() == nodeTagTypeOf);
            if (nodes.Count() == 1)
                return nodes.ElementAt(0);
            throw new Exception("В дереве есть более одного (" + nodes.Count() + ") узла с ключем " + id + " типа " + nodeTagTypeOf + ".");
        }
        //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        TreeNode[] GetTreeNodes(TreeNode nodeParent, Dictionary<object, object[]> roots_items, Enums.TreeView treeView, Color? textColor)
        {
            List<TreeNode> ret = new List<TreeNode>();
            object[] items = roots_items[nodeParent.Tag];

            if (items != null && items.Length > 0)
            {
                if (treeView == Enums.TreeView.Tree
                    && items[0].GetType().GetInterfaces().FirstOrDefault(x => x == typeof(IParent)) != null)
                {
                    foreach (var item in IdClass.GetRoots(items.ToList()).OrderBy(x => x.ToString()))
                    {
                        TreeNode node = GetTreeNode(item);
                        if (textColor.HasValue)
                            node.ForeColor = (Color)textColor;
                        ret.Add(node);

                        node.Nodes.AddRange(GetTreeNodes(((IParent)item).GetId(), items, textColor));
                    }
                }
                else
                {
                    foreach (var item in items.OrderBy(x => x.ToString()))
                    {
                        TreeNode node = GetTreeNode(item);
                        ret.Add(node);
                    }
                }
            }

            foreach (TreeNode node in nodeParent.Nodes)
            {
                node.Nodes.AddRange(GetTreeNodes(node, roots_items, treeView, textColor));
            }

            return ret.ToArray();
        }
        /// <summary>
        /// Добавить к дереву узлы интерфейса IParent.
        /// Определяются корневые узлы и к ним строятся ветки.
        /// </summary>
        /// <param name="items">Узлы дерева: корневые и нет. IParent interface.</param>
        TreeNode[] GetTreeNodes(object[] items, Enums.TreeView treeView, Color? textColor)
        {
            List<TreeNode> ret = new List<TreeNode>();
            if (items != null)
            {
                if (treeView == Enums.TreeView.Tree
                    && items[0].GetType().GetInterfaces().FirstOrDefault(x => x == typeof(IParent)) != null)
                {
                    foreach (var item in IdClass.GetRoots(items.ToList()).OrderBy(x => x.ToString()))
                    {
                        TreeNode node = GetTreeNode(item);
                        if (textColor.HasValue)
                            node.ForeColor = (Color)textColor;
                        ret.Add(node);

                        node.Nodes.AddRange(GetTreeNodes(((IParent)item).GetId(), items, textColor));
                    }
                }
                else
                {
                    foreach (var item in items.OrderBy(x => x.ToString()))
                    {
                        TreeNode node = GetTreeNode(item);
                        if (textColor.HasValue)
                            node.ForeColor = (Color)textColor;
                        ret.Add(node);
                    }
                }
            }
            return ret.ToArray();
        }
        /// <summary>
        /// Добавить к дереву узлы интерфейса IParent.
        /// Определяются корневые узлы и к ним строятся ветки.
        /// </summary>
        /// <param name="items">Узлы дерева: корневые и нет. IParent interface.</param>
        public void AddRange(object[] items)
        {
            if (items != null)
            {
                foreach (var item in IdClass.GetRoots(items.ToList()).OrderBy(x => x.ToString()))
                {
                    TreeNode node = GetTreeNode(item);
                    node.Nodes.AddRange(GetTreeNodes(((IParent)item).GetId(), items, null));
                    tv.Nodes.Add(node);
                }
            }
        }
        private TreeNode[] GetTreeNodes(int parentId, object[] iparentItems, Color? textColor)
        {
            List<TreeNode> ret = new List<TreeNode>();

            foreach (var item in iparentItems.Where(x => ((IParent)x).GetParentId() == parentId).OrderBy(x => x.ToString()))
            {
                TreeNode node = GetTreeNode(item);
                if (textColor.HasValue) node.ForeColor = (Color)textColor;
                ret.Add(node);
                node.Nodes.AddRange(GetTreeNodes(((IParent)item).GetId(), iparentItems, textColor));
            }

            return ret.ToArray();
        }
        /// <summary>
        /// Создать узел дерева.
        /// </summary>
        /// <param name="item">IDClass обязателен. IParent может быть.</param>
        /// <returns></returns>
        private TreeNode GetTreeNode(object item)
        {
            TreeNode node = new TreeNode();
            SetTreeNodeValue(node, item);
            return node;
        }
        private void SetTreeNodeValue(TreeNode node, object item)
        {
            bool isIParent = item.GetType().GetInterfaces().Where(x => x == typeof(IParent)).Count() == 1;
            node.Text = isIParent ? ((IParent)item).GetName() : ToString();
            node.Name = isIParent ? ((IParent)item).GetId().ToString() : ((IdClass)item).Id.ToString();
            node.Tag = item;
            node.ToolTipText = node.Name;

            if (tv.ImageList != null)
            {
                node.ImageIndex = tv.ImageList.Images.IndexOfKey(item.GetType().ToString());
                node.SelectedImageIndex = node.ImageIndex;
            }
        }

        /// <summary>
        /// Set treeview image list.
        /// </summary>
        /// <param name="imageList">Key is name of the dic entity Type = dici.Entity.GetType().ToString()</param>
        public void SetImageList4Types(ImageList imageList)
        {
            tv.ImageList = imageList;
        }

        public List<ContextMenuStrip> ContextMenuStrip4Types { get; set; }

        public object SelectedItem
        {
            get
            {
                if (tv.SelectedNode != null)
                    return tv.SelectedNode.Tag;
                return null;
            }
            //set
            //{
            //    tv.SelectedNode = null;
            //    if (value != null && tv.Nodes != null)
            //    {
            //        TreeNode node = GetNode(value);
            //        if (node != null)
            //            tv.SelectedNode = node;
            //    }
            //}
        }
        public TreeNode SelectedNode
        {
            get
            {
                return tv.SelectedNode;
            }
        }
        private void tv_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Collapse && e.Action != TreeViewAction.Expand)
            {
                if (tv.SelectedNode != null)
                {
                    //int i = ((DicItem)tv.SelectedNode.Tag).Level;
                    //if (i > 0 && ContextMenuStrip4Types != null && ContextMenuStrip4Types.Count > i)
                    //    tv.ContextMenuStrip = ContextMenuStrip4Types[i];

                    RaiseSelectedItemChanged(SelectedItem);
                }
            }
        }
        #region EVENTS
        public delegate void UCSelectedItemChangedEventHandler(object dici);
        public event UCSelectedItemChangedEventHandler UCSelectedItemChanged;
        protected virtual void RaiseSelectedItemChanged(object item)
        {
            if (UCSelectedItemChanged != null)
            {
                UCSelectedItemChanged(item);
            }
            tv.Focus();
        }
        public delegate void UCRefreshEventHandler();
        public event UCRefreshEventHandler UCRefresh;
        protected virtual void RaiseRefresh()
        {
            if (UCRefresh != null)
            {
                UCRefresh();
            }
            tv.Focus();
        }
        public delegate void UCAddItemEventHandler(object item);
        public event UCAddItemEventHandler UCAddNewItem;
        protected virtual void RaiseAddItem(object item)
        {
            if (UCAddNewItem != null)
            {
                UCAddNewItem(item);
            }
            tv.Focus();
        }
        public delegate void UCDeleteItemEventHandler(object item);
        public event UCDeleteItemEventHandler UCDeleteItem;
        protected virtual void RaiseDeleteItem(object item)
        {
            if (UCDeleteItem != null)
            {
                UCDeleteItem(item);
            }
            tv.Focus();
        }
        public delegate void UCCloneItemEventHandler(object dici);
        public event UCCloneItemEventHandler UCCloneItem;
        protected virtual void RaiseCloneItem(object item)
        {
            if (UCCloneItem != null)
            {
                UCCloneItem(item);
            }
            tv.Focus();
        }
        #endregion

        public void SetSelectedNode(int id, Type nodeTagTypeOf)
        {
            TreeNode node = FindNode(id, nodeTagTypeOf);
            tv.SelectedNode = (node == null) ? null : node;
        }
        public void SetSelectedNodeValue(object value)
        {
            if (tv.SelectedNode != null)
            {
                SetTreeNodeValue(tv.SelectedNode, value);
            }
        }

        public void Clear()
        {
            tv.Nodes.Clear();
        }

        private void tv_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            tv.ContextMenuStrip = null;

            if (e.Node != null)
            {
                tv.SelectedNode = e.Node;
            }
        }
        public bool ShowRefreshButton
        {
            set { refreshButton.Visible = value; }
            get { return refreshButton.Visible; }
        }
        public bool ShowDeleteButton
        {
            set { deleteItemButton.Visible = value; }
            get { return deleteItemButton.Visible; }
        }
        public bool ShowAddButton
        {
            set { addItemButton.Visible = value; }
            get { return addItemButton.Visible; }
        }
        public bool ShowEditButton
        {
            set { editButton.Visible = value; }
            get { return editButton.Visible; }
        }
        public bool ShowFindTextBox
        {
            set { findTextBox.Visible = value; }
            get { return findTextBox.Visible; }
        }
        public void HideAllToolStripButtons()
        {
            ShowAddButton =
            ShowCloneButton =
            ShowDeleteButton =
            ShowEditButton =
            ShowFindTextBox =
            ShowRefreshButton =
            false;
        }
        public bool ShowToolStrip
        {
            set { toolStrip.Visible = value; }
            get { return toolStrip.Visible; }
        }
        public bool ShowCloneButton
        {
            set { cloneItemButton.Visible = value; }
            get { return cloneItemButton.Visible; }
        }
        private void addNewButton_Click(object sender, EventArgs e)
        {
            RaiseAddItem(SelectedItem);
        }

        private void deleteItemButton_Click(object sender, EventArgs e)
        {
            RaiseDeleteItem(SelectedItem);
        }

        private void cloneItemButton_Click(object sender, EventArgs e)
        {
            RaiseCloneItem(SelectedItem);
        }

        public delegate void UCNodesOrderChangedEventHandler();
        public event UCNodesOrderChangedEventHandler UCNodesOrderChangedEvent;
        protected virtual void RaiseNodesOrderChangedEvent()
        {
            if (UCNodesOrderChangedEvent != null)
            {
                UCNodesOrderChangedEvent();
            }
        }

        public delegate void UCEditItemEventHandler(object item);
        public event UCEditItemEventHandler UCEditNewItem;
        protected virtual void RaiseEditItem(object item)
        {
            if (UCEditNewItem != null)
            {
                UCEditNewItem(item);
            }
            tv.Focus();
        }
        private void editButton_Click(object sender, EventArgs e)
        {
            RaiseEditItem(SelectedItem);
        }
        public delegate void UCDoubleClickEventHandler();
        public event UCDoubleClickEventHandler UCDoubleClickEvent;
        private void tv_DoubleClick(object sender, EventArgs e)
        {
            if (UCDoubleClickEvent != null)
            {
                UCDoubleClickEvent();
            }
        }

        List<TreeNode> _nodesFinded = null;
        int _nodesFindedIdx = -1;
        string _textFinded = null;
        private void findTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                // FIND NODES
                if (_textFinded != findTextBox.Text)
                {
                    _nodesFinded = new List<TreeNode>();
                    foreach (TreeNode node in tv.Nodes)
                    {
                        _nodesFinded.AddRange(GetNodesByText(node, findTextBox.Text));
                    }
                    _nodesFindedIdx = -1;
                    _textFinded = findTextBox.Text;
                }

                // SCAN NODES
                tv.Focus();
                if (++_nodesFindedIdx == _nodesFinded.Count)
                {
                    Console.Beep();
                    _nodesFindedIdx = 0;
                }
                tv.SelectedNode = _nodesFinded[_nodesFindedIdx];

                e.Handled = true;
            }
        }

        private List<TreeNode> GetNodesByText(TreeNode node, string text)
        {
            List<TreeNode> ret = new List<TreeNode>();
            if (node.Text.ToUpper().IndexOf(text.ToUpper()) >= 0)
                ret.Add(node);
            foreach (TreeNode item in node.Nodes)
            {
                ret.AddRange(GetNodesByText(item, text));
            }
            return ret;
        }

        private void tv_KeyPress(object sender, KeyPressEventArgs e)
        {
            findTextBox_KeyPress(sender, e);
        }
        /// <summary>
        /// Получить корневой для выбранного узла дерева элемент (объект).
        /// </summary>
        /// <returns>Корневой элемент или null, если не выбран узел дерева.</returns>
        public object GetRootItem()
        {
            TreeNode node = tv.SelectedNode;
            if (node != null)
            {
                while (node.Parent != null)
                {
                    node = node.Parent;
                }
                return node.Tag;
            }
            return null;
        }
        /// <summary>
        /// Получить для выбранного узла дерева первого родителя заданного типа.
        /// </summary>
        /// <returns>Первый родитель заданного типа для выбранного узла.</returns>
        public object GetParentItem(Type type)
        {
            TreeNode node = tv.SelectedNode;
            if (node != null)
            {
                while (node.Parent != null)
                {
                    if (((IdNameTree)node.Parent.Tag).Entity.GetType() == type)
                        return node.Parent.Tag;
                    node = node.Parent;
                }
            }
            return null;
        }
    }
}
