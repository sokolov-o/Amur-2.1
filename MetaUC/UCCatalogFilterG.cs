using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SOV.Amur.Meta;

namespace SOV.Amur.Meta
{
    /// <summary>
    /// Дерево ключей каталога данных с группами ключей- для установки фильтра и др. нужд.
    /// </summary>
    public partial class UCCatalogFilterG : UserControl
    {
        public UCCatalogFilterG()
        {
            InitializeComponent();
        }
        public UCCatalogFilterG(string connectionString)
        {
            InitializeComponent();
        }

        [DefaultValue(null)]
        TreeNode Node0Site { get; set; }

        public void Fill()
        {
            // SITE
            Node0Site = new TreeNode();
            Node0Site.Name = NODE_TEXT_SITES;
            Node0Site.Text = Node0Site.Name + " - 0";
            Node0Site.ImageIndex = Node0Site.SelectedImageIndex = 3;
            Node0Site.ContextMenuStrip = contextMenuStrip1;

            List<EntityGroup> sg = Meta.DataManager.GetInstance().EntityGroupRepository.SelectByEntityTableName("site");
            List<SiteType> siteTypes = Meta.DataManager.GetInstance().SiteTypeRepository.Select();

            foreach (var item in sg.OrderBy(x => x.Name))
            {
                TreeNode node1 = Node0Site.Nodes.Add(item.Name, item.Name + " - 0");
                node1.Tag = item;
                node1.ImageIndex = node1.SelectedImageIndex = 3;
                node1.ContextMenuStrip = contextMenuStrip2;

                foreach (Site site in item.Items.OrderBy(x => ((Site)x).Name))
                {
                    TreeNode node2 = node1.Nodes.Add("u", site.GetName(2, SiteTypeRepository.GetCash()));
                    node2.Tag = site.Id;
                    node2.ImageIndex = node2.SelectedImageIndex = 1;
                }
            }
            if (SiteNodeEnabled)
                tv.Nodes.Add(Node0Site);

            // VARIABLE
            Dictionary<EntityGroup, List<int[]>> eg = Meta.DataManager.GetInstance().EntityGroupRepository.SelectGroupsFK("variable");
            List<Variable> vars = Meta.DataManager.GetInstance().VariableRepository.Select(null);

            TreeNode node = tv.Nodes.Add(NODE_TEXT_VARIABLES, NODE_TEXT_VARIABLES + " - 0");
            node.ImageIndex = node.SelectedImageIndex = 2;
            node.ContextMenuStrip = contextMenuStrip1;

            foreach (var item in eg.OrderBy(x => x.Key.Name))
            {
                TreeNode node1 = node.Nodes.Add(item.Key.Name, item.Key.Name + " - 0");
                node1.Tag = item.Key;
                node1.ImageIndex = node1.SelectedImageIndex = 2;
                node1.ContextMenuStrip = contextMenuStrip2;

                //foreach (var var in vars.Where(x => item.Value.Exists(y => y[0] == x.Id)).OrderBy(x => x.Name))
                foreach (var varId in item.Value)
                {
                    Variable var = vars.Find(x => x.Id == varId[0]);
                    TreeNode node2 = node1.Nodes.Add("u", var.NameRus);
                    node2.Tag = var.Id;
                    node2.ImageIndex = node2.SelectedImageIndex = 1;
                }
            }

            // OFFSETTYPE
            node = tv.Nodes.Add(NODE_TEXT_OFFSET, NODE_TEXT_OFFSET + " - 0");
            node.ImageIndex = node.SelectedImageIndex = 4;
            node.ContextMenuStrip = contextMenuStrip1;

            eg = Meta.DataManager.GetInstance().EntityGroupRepository.SelectGroupsFK("offset_type");
            if (eg.Count > 0)
            {
                List<OffsetType> ofts = Meta.DataManager.GetInstance().OffsetTypeRepository.Select();

                foreach (var item1 in eg.OrderBy(x => x.Key.Name))
                {
                    TreeNode node1 = node.Nodes.Add(item1.Key.Name, item1.Key.Name + " - 0");
                    node1.Tag = item1.Key;
                    node1.ImageIndex = node1.SelectedImageIndex = 4;
                    node1.ContextMenuStrip = contextMenuStrip2;

                    foreach (var item2 in ofts.Where(x => item1.Value.Exists(y => y[0] == x.Id)).OrderBy(x => x.Name))
                    {
                        TreeNode node2 = node1.Nodes.Add("u", item2.Name);
                        node2.Tag = item2.Id;
                        node2.ImageIndex = node2.SelectedImageIndex = 1;
                    }
                }
            }
            // METHOD
            node = tv.Nodes.Add(NODE_TEXT_METHODS, NODE_TEXT_METHODS + " - 0");
            node.ImageIndex = node.SelectedImageIndex = 5;
            node.ContextMenuStrip = contextMenuStrip1;
            eg = Meta.DataManager.GetInstance().EntityGroupRepository.SelectGroupsFK("method");
            if (eg.Count > 0)
            {
                List<Method> methods = Meta.DataManager.GetInstance().MethodRepository.Select();

                foreach (var item1 in eg.OrderBy(x => x.Key.Name))
                {
                    TreeNode node1 = node.Nodes.Add(item1.Key.Name, item1.Key.Name + " - 0");
                    node1.Tag = item1.Key;
                    node1.ImageIndex = node1.SelectedImageIndex = 5;
                    node1.ContextMenuStrip = contextMenuStrip2;

                    foreach (var item2 in methods.Where(x => item1.Value.Exists(y => y[0] == x.Id)).OrderBy(x => x.Name))
                    {
                        TreeNode node2 = node1.Nodes.Add("u", item2.Name);
                        node2.Tag = item2.Id;
                        node2.ImageIndex = node2.SelectedImageIndex = 1;
                    }
                }
            }

            // SOURCE
            node = tv.Nodes.Add(NODE_TEXT_SOURCES, NODE_TEXT_SOURCES + " - 0");
            node.ImageIndex = node.SelectedImageIndex = 6;
            node.ContextMenuStrip = contextMenuStrip1;
            eg = Meta.DataManager.GetInstance().EntityGroupRepository.SelectGroupsFK("source");

            if (eg.Count > 0)
            {
                List<Social.LegalEntity> sources = Social.DataManager.GetInstance().LegalEntityRepository.SelectAll();

                foreach (var item1 in eg.OrderBy(x => x.Key.Name))
                {
                    TreeNode node1 = node.Nodes.Add(item1.Key.Name, item1.Key.Name + " - 0");
                    node1.Tag = item1.Key;
                    node1.ImageIndex = node1.SelectedImageIndex = 6;
                    node1.ContextMenuStrip = contextMenuStrip2;

                    foreach (var item2 in sources.Where(x => item1.Value.Exists(y => y[0] == x.Id)).OrderBy(x => x.NameRus))
                    {
                        TreeNode node2 = node1.Nodes.Add("u", item2.NameRus);
                        node2.Tag = item2.Id;
                        node2.ImageIndex = node2.SelectedImageIndex = 1;
                    }
                }
            }
        }

        private void CheckNode(TreeNode node)
        {
            node.ImageIndex = node.SelectedImageIndex = 0;
            node.Name = "c";
            //RefreshCount(node.Parent);
            //RefreshCount(node.Parent.Parent);
        }
        private void UncheckNode(TreeNode node)
        {
            node.ImageIndex = node.SelectedImageIndex = 1;
            node.Name = "u";
            //RefreshCount(node.Parent);
            //RefreshCount(node.Parent.Parent);
        }
        private void mnuUncheckAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode[] nodes = tv.SelectedNode.Nodes.Find("c", true);
            foreach (var item in nodes)
            {
                UncheckNode(item);
            }
            RefreshCount(tv.SelectedNode);
        }
        private void RefreshCount(TreeNode node)
        {
            int count = node.Nodes.Find("c", true).Length;
            node.Text = node.Name + " - " + count;

            node.NodeFont = new Font(tv.Font, (count > 0) ? FontStyle.Bold : FontStyle.Regular);
        }

        private void mnuCheckAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tv.SelectedNode != null)
            {
                TreeNode[] nodes = tv.SelectedNode.Nodes.Find("u", true);
                foreach (var item in nodes)
                {
                    CheckNode(item);
                }
                RefreshCount(tv.SelectedNode);
                RefreshCount(tv.SelectedNode.Parent);
            }
        }

        private void mnuUncheckAllToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (tv.SelectedNode != null)
            {
                TreeNode[] nodes = tv.SelectedNode.Nodes.Find("c", true);
                foreach (var item in nodes)
                {
                    UncheckNode(item);
                }
                RefreshCount(tv.SelectedNode);
                RefreshCount(tv.SelectedNode.Parent);
            }
        }

        public List<int> SiteIdList
        {
            get
            {
                return SiteNodeEnabled ? GetCheckedId(NODE_TEXT_SITES) : null;
            }
            set
            {
                if (SiteNodeEnabled)
                    SetCheckedId(NODE_TEXT_SITES, value);
            }
        }

        public List<int> VariableIdList
        {
            get
            {
                return GetCheckedId(NODE_TEXT_VARIABLES);
            }
            set
            {
                SetCheckedId(NODE_TEXT_VARIABLES, value);
            }
        }

        public int? OffsetTypeId
        {
            get
            {
                List<int> id = GetCheckedId(NODE_TEXT_OFFSET);
                return id.Count > 0 ? (int?)id[0] : null;
            }
            set
            {
                List<int> idList = new List<int>();
                if (value.HasValue) idList.Add((int)value);

                SetCheckedId(NODE_TEXT_OFFSET, idList);
            }
        }

        public int? MethodId
        {
            get
            {
                List<int> id = GetCheckedId(NODE_TEXT_METHODS);
                return id.Count > 0 ? (int?)id[0] : null;
            }
            set
            {
                List<int> idList = new List<int>();
                if (value.HasValue) idList.Add((int)value);

                SetCheckedId(NODE_TEXT_METHODS, idList);
            }
        }


        public int? SourceId
        {
            get
            {
                List<int> id = GetCheckedId(NODE_TEXT_SOURCES);
                return id.Count > 0 ? (int?)id[0] : null;
            }
            set
            {
                List<int> idList = new List<int>();
                if (value.HasValue) idList.Add((int)value);

                SetCheckedId(NODE_TEXT_SOURCES, idList);
            }
        }
        public List<int> GetCheckedId(string rootName)
        {
            TreeNode[] nodes = tv.Nodes.Find(rootName, true);
            if (nodes.Length != 1)
                throw new Exception("Ошибка алгоритма. node.Length != 1");

            List<int> ret = new List<int>();
            nodes = nodes[0].Nodes.Find("c", true);
            if (nodes != null)
                foreach (var item in nodes)
                {
                    if (!ret.Exists(x => x == (int)item.Tag)) ret.Add(((int)item.Tag));
                }
            return ret;
        }
        public void SetCheckedId(string rootName, List<int> idList)
        {
            TreeNode[] nodes = tv.Nodes.Find(rootName, true);
            if (nodes.Length != 1)
                throw new Exception("Ошибка алгоритма. node.Length != 1");

            tv.SuspendLayout();
            List<int> idChecked = new List<int>();
            foreach (TreeNode item1 in nodes[0].Nodes)
            {
                foreach (TreeNode item2 in item1.Nodes)
                {
                    if (idList == null || !idList.Exists(x => x == (int)item2.Tag) || idChecked.Exists(x => x == (int)item2.Tag))
                    {
                        UncheckNode(item2);
                    }
                    else
                    {
                        CheckNode(item2);
                        idChecked.Add((int)item2.Tag);
                    }
                }
                RefreshCount(item1);
            }
            RefreshCount(nodes[0]);

            tv.ResumeLayout();
        }

        const string NODE_TEXT_SITES = "Пункты";
        const string NODE_TEXT_VARIABLES = "Переменные";
        const string NODE_TEXT_METHODS = "Методы";
        const string NODE_TEXT_SOURCES = "Источники";
        const string NODE_TEXT_OFFSET = "Смещение";

        bool _siteNodeEnabled = true;
        public bool SiteNodeEnabled
        {
            get { return _siteNodeEnabled; }
            set
            {
                if (Node0Site != null)
                {
                    if (value && !_siteNodeEnabled)
                    {
                        tv.Nodes.Insert(0, Node0Site);
                    }
                    else if (!value)
                    {
                        tv.Nodes.Remove(Node0Site);
                    }
                    _siteNodeEnabled = value;
                }
            }
        }
        List<TreeNode> _nodesFounded = new List<TreeNode>();
        TreeNode _nodeRoot = null;
        int _iNode;

        private void findNextToolStripButton_Click(object sender, EventArgs e)
        {
            if (tv.SelectedNode != null)
            {
                TreeNode node = (tv.SelectedNode.Nodes.Count > 0) ? tv.SelectedNode : tv.SelectedNode.Parent;
                if (node != _nodeRoot || _nodesFounded.Count == 0)
                {
                    _nodeRoot = node;
                    _nodesFounded.Clear();
                    Find(node, findToolStripTextBox.Text);
                    _iNode = -1;
                }
                if (_nodesFounded.Count > 0)
                {
                    _iNode = ((++_iNode > _nodesFounded.Count - 1) ? 0 : _iNode);
                    tv.SelectedNode = _nodesFounded[_iNode];
                }
            }
        }
        private void Find(TreeNode node, string nameLike)
        {
            foreach (TreeNode item in node.Nodes)
            {
                if (item.Text.ToUpper().IndexOf(nameLike.ToUpper()) >= 0)
                {
                    _nodesFounded.Add(item);
                }
                Find(item, nameLike);
            }
        }

        private void findToolStripTextBox_TextChanged(object sender, EventArgs e)
        {
            _nodesFounded.Clear();
        }

        private void tv_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode node = e.Node;
            if (node != null)
            {
                switch (node.ImageIndex)
                {
                    case 0:
                        UncheckNode(node);
                        RefreshCount(node.Parent);
                        RefreshCount(node.Parent.Parent);
                        break;
                    case 1:
                        CheckNode(node);
                        RefreshCount(node.Parent);
                        RefreshCount(node.Parent.Parent);
                        break;
                }
            }
        }
    }
}
