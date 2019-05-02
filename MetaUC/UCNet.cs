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
    public partial class UCNet : UserControl
    {
        public UCNet()
        {
            InitializeComponent();
        }

        private void refreshToolStripButton_Click(object sender, EventArgs e)
        {
            Fill(tv.SelectedNode == null ? null : tv.SelectedNode.Tag);
        }

        public void Fill(object selected = null)
        {
            tv.Nodes.Clear();

            // ВОДНЫЕ ОБЪЕКТЫ И ИХ ПОСТЫ/СТАНЦИИ

            Dictionary<GeoObject, List<Site>> geoobXsites = Meta.DataManager.GetInstance().SiteGeoObjectRepository.SelectGeoobSites();

            foreach (KeyValuePair<GeoObject, List<Site>> gos in geoobXsites.Where(x => !x.Key.FallIntoId.HasValue).OrderBy(x => x.Key.OrderBy))
            {
                TreeNode node = new TreeNode(gos.Key.Name);
                node.NodeFont = new Font(TreeView.DefaultFont, FontStyle.Regular);
                node.ForeColor = Color.Blue;
                node.Tag = gos.Key;

                InsertChild(node, geoobXsites);

                tv.Nodes.Add(node);
            }

            // ПОСТЫ/СТАНЦИИ БЕЗ ВОДНЫХ ОБЪЕКТОВ
            List<Site> sites = Meta.DataManager.GetInstance().SiteRepository.SelectWithoutGeoObject();
            if (sites.Count > 0)
            {
                int i = tv.Nodes.Add(new TreeNode("ПОСТЫ/СТАНЦИИ БЕЗ ВОДНЫХ ОБЪЕКТОВ"));
                tv.Nodes[i].ForeColor = Color.Red;

                foreach (Site site in sites.OrderBy(x => x.Code))
                {
                    tv.Nodes[i].Nodes.Add(NewTreeNode(site));
                }
                tv.Nodes[i].Text += " - " + tv.Nodes[i].Nodes.Count + " шт.";
            }
        }
        TreeNode NewTreeNode(GeoObject geoob)
        {
            TreeNode ret = new TreeNode(geoob.Name);
            ret.NodeFont = new Font(TreeView.DefaultFont, FontStyle.Regular);
            ret.ForeColor = Color.Blue;
            ret.Tag = geoob;
            return ret;
        }
        TreeNode NewTreeNode(Site site)
        {
            TreeNode ret = new TreeNode();
            ret.Text = Meta.Site.GetName(site, 1, SiteTypeRepository.GetCash());
            if (site.TypeId != (int)EnumStationType.HydroPost && site.TypeId != (int)EnumStationType.MeteoStation)
                ret.NodeFont = new Font(TreeView.DefaultFont, FontStyle.Italic);
            ret.ForeColor = (site.TypeId == (int)EnumStationType.HydroPost) ? Color.Green : Color.Black;

            ret.Tag = new object[] { site };
            return ret;
        }
        public void InsertChild(TreeNode nodeParent, Dictionary<GeoObject, List<Site>> geoobXsites)
        {
            foreach (KeyValuePair<GeoObject, List<Site>> gxs in geoobXsites.Where(x => x.Key.FallIntoId == ((GeoObject)nodeParent.Tag).Id).OrderBy(x => x.Key.OrderBy))
            {
                TreeNode node = NewTreeNode(gxs.Key);

                InsertChild(node, geoobXsites);

                foreach (Site site in gxs.Value)
                {
                    node.Nodes.Add(NewTreeNode(site));
                }
                nodeParent.Nodes.Add(node);
            }
        }

        #region EVENTS
        public delegate void UCSelectedNodeChangedEventHandler(TreeNode node);
        public event UCSelectedNodeChangedEventHandler UCSelectedNodeChangedEvent;
        protected virtual void RaiseSelectedNodeChangedEvent(TreeNode node)
        {
            if (UCSelectedNodeChangedEvent != null)
            {
                UCSelectedNodeChangedEvent(node);
            }
            tv.Focus();
        }
        public delegate void UCEditDataEventHandler(TreeNode node);
        public event UCEditDataEventHandler UCEditDataEvent;
        protected virtual void RaiseEditDataEvent(TreeNode node)
        {
            if (UCEditDataEvent != null)
            {
                UCEditDataEvent(node);
            }
        }
        #endregion

        private void editDataToolStripButton_Click(object sender, EventArgs e)
        {
            RaiseEditDataEvent(tv.SelectedNode);
        }
        List<TreeNode> findedNode = null;
        int curFindedNode = -1;

        private void findToolStripButton_Click(object sender, EventArgs e)
        {
            if (findedNode == null)
            {
                findedNode = new List<TreeNode>();
                foreach (TreeNode node in tv.Nodes)
                {
                    FindNodes(node);
                }
            }
            if (findedNode.Count == 0)
            {
                tv.SelectedNode = null;
                tv.CollapseAll();
                return;
            }
            else if (curFindedNode == -1 || curFindedNode == findedNode.Count - 1)
            {
                curFindedNode = -1;
            }
            curFindedNode++;

            tv.SelectedNode = findedNode[curFindedNode];
            tv.SelectedNode.EnsureVisible();

            RaiseSelectedNodeChangedEvent(tv.SelectedNode);

            tv.Focus();
        }
        void FindNodes(TreeNode node)
        {
            if (node.Text.ToUpper().IndexOf(findToolStripTextBox.Text.ToUpper()) >= 0)
                findedNode.Add(node);
            foreach (TreeNode n in node.Nodes)
            {
                FindNodes(n);
            }
        }
        private void findToolStripTextBox_TextChanged(object sender, EventArgs e)
        {
            findedNode = null;
            curFindedNode = -1;
        }

        private void tv_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Collapse && e.Action != TreeViewAction.Expand)
            {
                if (tv.SelectedNode != null)
                    RaiseSelectedNodeChangedEvent(tv.SelectedNode);
            }
        }
    }
}
