using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FERHRI.Social
{
    public partial class UCAddrs : UserControl
    {
        public UCAddrs()
        {
            InitializeComponent();

            //RootTreeIds = new List<int>();
        }
        //public List<int> RootTreeIds { get; set; }

        public int? AddrTypeIdIn { get; set; }
        public int? AddrTypeIdNotIn { get; set; }

        public void Fill(int? selectedAddrId)
        {
            tv.Nodes.Clear();
            List<Addr> Addrs = DataManager.GetInstance().AddrRepository.Select()
                .Where(x => (!AddrTypeIdIn.HasValue || x.TypeId == AddrTypeIdIn)
                    && (!AddrTypeIdNotIn.HasValue || x.TypeId != AddrTypeIdNotIn))
                .ToList();
            // READ
            //if (RootTreeIds.Count == 0)
            //{
            //    _Addrs = DataManager.GetInstance().AddrRepository.Select();
            //    Addr.FillChilds(_Addrs.FindAll(x => x.ParentId == null), _Addrs);
            //}
            //else
            //{
            //    _Addrs = DataManager.GetInstance().AddrRepository.Select(RootTreeIds, true);
            //}
            //_Addrs = parents.OrderBy(x => x.Name).ToList();

            // FILL
            List<Addr> ars = Addrs.FindAll(x => x.ParentId == null);
            Addr.FillChilds(ars, Addrs);
            foreach (Addr arParent in ars)
            {
                TreeNode node = GetTreeNode(arParent);
                AddChilds(node);
                tv.Nodes.Add(node);
            }

            if (selectedAddrId.HasValue)
                SetSelectedAddr((int)selectedAddrId);
        }

        void AddChilds(TreeNode node)
        {
            Addr ar = (Addr)node.Tag;
            if (ar.Childs == null) return;

            foreach (var item in ar.Childs)
            {
                TreeNode nodeChild = GetTreeNode(item);
                AddChilds(nodeChild);
                node.Nodes.Add(nodeChild);
            }
        }
        TreeNode GetTreeNode(Addr ar)
        {
            TreeNode node = new TreeNode();
            node.Text = ar.ToString();
            node.Name = ar.Id.ToString();
            node.Tag = ar;
            return node;
        }
        private void refreshButton_Click(object sender, EventArgs e)
        {
            Addr ar = GetSelectedAddr();
            Fill(ar == null ? null : (int?)ar.Id);
            addNewButton.Enabled = true;
            RaiseSelectedItemChanged();
        }
        public delegate void UCSelectedItemChangedEventHandler(Addr ar);
        public event UCSelectedItemChangedEventHandler UCSelectedItemChanged;
        protected virtual void RaiseSelectedItemChanged()
        {
            if (UCSelectedItemChanged != null)
            {
                UCSelectedItemChanged(GetSelectedAddr());
            }
        }
        public delegate void UCAddNewItemEventHandler();
        public event UCAddNewItemEventHandler UCAddNewItem;
        protected virtual void RaiseAddNewItem()
        {
            if (UCAddNewItem != null)
            {
                UCAddNewItem();
            }
        }

        private void tv_AfterSelect(object sender, TreeViewEventArgs e)
        {
            tv.SelectedNode = e.Node;
            RaiseSelectedItemChanged();
        }

        public void SetSelectedAddr(int AddrId)
        {
            TreeNode[] nodes = tv.Nodes.Find(AddrId.ToString(), true);
            tv.SelectedNode = (nodes.Length > 0) ? nodes[0] : null;
            RaiseSelectedItemChanged();

        }
        public Addr GetSelectedAddr()
        {
            if (tv.SelectedNode == null)
                return null;
            return (Addr)tv.SelectedNode.Tag;
        }

        private void addNewButton_Click(object sender, EventArgs e)
        {
            RaiseAddNewItem();
        }
    }
}
