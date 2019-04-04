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
    public partial class UCDicTree : UserControl
    {
        List<Common.DicItem> _viewTypes = new List<DicItem>();

        public User User { get; set; }
        public UCDicTree()
        {
            InitializeComponent();
        }
        private void refreshButton_Click(object sender, EventArgs e)
        {
            //Fill(_dictree, CurDicItem);
            RaiseRefresh();
        }
        public void AddRange(List<DicItem> dictree)
        {
            AddRange(null, dictree);
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

        void AddRange(TreeNode nodeParent, List<DicItem> dictree)
        {
            if (dictree == null) return;

            foreach (var dici in dictree)//.OrderBy(x => x.Name))
            {
                TreeNode node = new TreeNode(dici.Name);
                node.Name = dici.Key;
                node.Tag = dici;
                if (tv.ImageList != null)
                {
                    node.ImageIndex = tv.ImageList.Images.IndexOfKey(dici.Entity.GetType().ToString());
                    node.SelectedImageIndex = node.ImageIndex;
                }

                if (nodeParent == null) tv.Nodes.Add(node);
                else nodeParent.Nodes.Add(node);

                AddRange(node, dici.Childs);
            }
        }
        public DicItem SelectedDicItem
        {
            get
            {
                if (tv.SelectedNode != null)
                    return (DicItem)tv.SelectedNode.Tag;
                return null;
            }
            set
            {
                tv.SelectedNode = null;
                if (value != null && tv.Nodes != null)
                {
                    TreeNode node = GetNode(value);
                    if (node != null)
                        tv.SelectedNode = node;
                }
            }
        }
        TreeNode GetNode(DicItem dici)
        {
            TreeNode[] nodes = tv.Nodes.Find(dici.Key, true);
            if (nodes.Length > 1)
                throw new Exception("(nodes.Length > 1)");
            return (nodes.Length == 1) ? nodes[0] : null;

        }
        private void tv_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Collapse && e.Action != TreeViewAction.Expand)
            {
                if (tv.SelectedNode != null)
                {
                    int i = ((DicItem)tv.SelectedNode.Tag).Level;
                    if (i > 0 && ContextMenuStrip4Types != null && ContextMenuStrip4Types.Count > i)
                        tv.ContextMenuStrip = ContextMenuStrip4Types[i];
                    
                    RaiseSelectedItemChanged(SelectedDicItem);
                }
            }
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
        public delegate void UCAddItemEventHandler(DicItem diciCurrent);
        public event UCAddItemEventHandler UCAddNewItem;
        protected virtual void RaiseAddItem(DicItem diciCurrent)
        {
            if (UCAddNewItem != null)
            {
                UCAddNewItem(diciCurrent);
            }
            tv.Focus();
        }
        public delegate void UCDeleteItemEventHandler(DicItem dici);
        public event UCDeleteItemEventHandler UCDeleteItem;
        protected virtual void RaiseDeleteItem(DicItem dici)
        {
            if (UCDeleteItem != null)
            {
                UCDeleteItem(dici);
            }
            tv.Focus();
        }
        public delegate void UCCloneItemEventHandler(DicItem dici);
        public event UCCloneItemEventHandler UCCloneItem;
        protected virtual void RaiseCloneItem(DicItem dici)
        {
            if (UCCloneItem != null)
            {
                UCCloneItem(dici);
            }
            tv.Focus();
        }
        #endregion

        public void Clear()
        {
            tv.Nodes.Clear();
            //RaiseSelectedItemChanged(null);
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
            RaiseAddItem(SelectedDicItem);
        }

        private void deleteItemButton_Click(object sender, EventArgs e)
        {
            RaiseDeleteItem(SelectedDicItem);
        }

        private void cloneItemButton_Click(object sender, EventArgs e)
        {
            RaiseCloneItem(SelectedDicItem);
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

        public void Remove(DicItem dici)
        {
            TreeNode node = GetNode(dici);
            if (node != null)
                tv.Nodes.Remove(node);
        }

        public delegate void UCEditItemEventHandler(DicItem diciCurrent);
        public event UCEditItemEventHandler UCEditNewItem;
        protected virtual void RaiseEditItem(DicItem diciCurrent)
        {
            if (UCEditNewItem != null)
            {
                UCEditNewItem(diciCurrent);
            }
            tv.Focus();
        }
        private void editButton_Click(object sender, EventArgs e)
        {
            RaiseEditItem(SelectedDicItem);
        }
    }
}
