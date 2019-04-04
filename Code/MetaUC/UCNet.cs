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

            Dictionary<GeoObject, List<Station>> goxs = Meta.DataManager.GetInstance().StationGeoObjectRepository.SelectGeoObjectsXStations();
            Dictionary<Station, List<Site>> sxs = Meta.DataManager.GetInstance().StationRepository.SelectStationXSites();

            foreach (KeyValuePair<GeoObject, List<Station>> gos in goxs.Where(x => !x.Key.FallIntoId.HasValue).OrderBy(x => x.Key.Order))
            {
                TreeNode node = new TreeNode(gos.Key.Name);
                node.NodeFont = new Font(TreeView.DefaultFont, FontStyle.Regular);
                node.ForeColor = Color.Blue;
                node.Tag = gos.Key;

                InsertChild(node, goxs, sxs);

                tv.Nodes.Add(node);
            }

            // ПОСТЫ/СТАНЦИИ БЕЗ ВОДНЫХ ОБЪЕКТОВ
            List<Station> stations = Meta.DataManager.GetInstance().StationRepository.SelectWithoutGeoObject();
            if (stations.Count > 0)
            {
                int i = tv.Nodes.Add(new TreeNode("ПОСТЫ/СТАНЦИИ БЕЗ ВОДНЫХ ОБЪЕКТОВ"));
                tv.Nodes[i].ForeColor = Color.Red;

                foreach (Station station in stations.OrderBy(x => x.Code))
                {
                    tv.Nodes[i].Nodes.Add(NewTreeNode(station, null));
                }
                tv.Nodes[i].Text += " - " + tv.Nodes[i].Nodes.Count + " шт.";
            }
        }
        TreeNode NewTreeNode(GeoObject go)
        {
            TreeNode ret = new TreeNode(go.Name);
            ret.NodeFont = new Font(TreeView.DefaultFont, FontStyle.Regular);
            ret.ForeColor = Color.Blue;
            ret.Tag = go;
            return ret;
        }
        TreeNode NewTreeNode(Station station, Site site)
        {
            TreeNode ret = new TreeNode();
            if (site == null)
            {
                ret.Text = station.ToString();
            }
            else
            {
                ret.Text = Meta.Site.GetName(site, StationRepository.GetCash(), SiteTypeRepository.GetCash(), 1);
                if (site.SiteTypeId != (int)EnumStationType.HydroPost && site.SiteTypeId != (int)EnumStationType.MeteoStation)
                    ret.NodeFont = new Font(TreeView.DefaultFont, FontStyle.Italic);
            }
            ret.ForeColor = (station.TypeId == (int)EnumStationType.HydroPost) ? Color.Green : Color.Black;

            ret.Tag = new object[] { station, site };
            return ret;
        }
        public void InsertChild(TreeNode nodeParent, Dictionary<GeoObject, List<Station>> gos, Dictionary<Station, List<Site>> ss)
        {
            foreach (KeyValuePair<GeoObject, List<Station>> go in gos.Where(x => x.Key.FallIntoId == ((GeoObject)nodeParent.Tag).Id).OrderBy(x => x.Key.Order))
            {
                TreeNode node = NewTreeNode(go.Key);

                InsertChild(node, gos, ss);

                foreach (Station station in go.Value)
                {
                    foreach (Site site in ss.First(x => x.Key.Id == station.Id).Value)
                    {
                        node.Nodes.Add(NewTreeNode(station, site));
                    }
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
