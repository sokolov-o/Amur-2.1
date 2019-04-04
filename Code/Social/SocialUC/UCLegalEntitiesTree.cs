using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOV.Social
{
    public partial class UCLegalEntitiesTree : UserControl
    {
        public UCLegalEntitiesTree()
        {
            InitializeComponent();
        }

        internal DateTime? DateActual
        {
            get
            {
                DateTime dateActual;
                return string.IsNullOrEmpty(dateActualTextBox.Text) || !DateTime.TryParse(dateActualTextBox.Text, out dateActual) ? null : (DateTime?)dateActual;
            }
        }

        public void FillByType(Enums.LegalEntityType? leType)
        {
            tv.Nodes.Clear();

            if (!leType.HasValue || leType == Enums.LegalEntityType.Organization)
            {
                char ch = (char)Enums.ToStringLegalEntityType(Enums.LegalEntityType.Organization);
                TreeNode node = tv.Nodes.Add(ch.ToString(), "Организации/предприятия");
                node.Tag = Enums.LegalEntityType.Organization;
                List<LegalEntity> les = DataManager.GetInstance().LegalEntityRepository.SelectByType(ch).OrderBy(x => x.NameRusShort).ToList();
                AddRange(node, les,
                    mnuShowDivisionsToolStripMenuItem.Checked ? DataManager.GetInstance().DivisionRepository.Select() : null);
                node.Text += " (" + node.Nodes.Count + ")";
            }
            if (!leType.HasValue || leType == Enums.LegalEntityType.Person)
            {
                char ch = (char)Enums.ToStringLegalEntityType(Enums.LegalEntityType.Person);
                TreeNode node = tv.Nodes.Add(ch.ToString(), "Физические лица");
                node.Tag = Enums.LegalEntityType.Person;
                AddRange(node, DataManager.GetInstance().LegalEntityRepository.SelectByType(ch).OrderBy(x => x.NameRusShort).ToList(), null);
                node.Text += " (" + node.Nodes.Count + ")";
            }
            SetInfo();
        }
        public void FillByIds(List<int> leIds)
        {
            Clear();
            if (leIds != null)
            {
                tv.Nodes.AddRange(Common.ParentChild.GetTreeNodes(DataManager.GetInstance().LegalEntityRepository.Select(leIds).OrderBy(x => x.NameRusShort).ToList<Common.IParent>()).ToArray());
            }
            SetInfo();
        }
        void SetInfo()
        {
            infoLabel.Text = tv.Nodes.Count.ToString();
        }
        public void Clear()
        {
            tv.Nodes.Clear();
        }
        private void AddRange(TreeNode nodeParent, List<LegalEntity> les, List<Division> divs)
        {
            IEnumerable<LegalEntity> les1 = (nodeParent.Tag.GetType() == typeof(Enums.LegalEntityType))
                ? les.Where(x => !x.ParentId.HasValue)
                : les.Where(x => x.ParentId == ((LegalEntity)nodeParent.Tag).Id);

            foreach (var item in les1)
            {
                TreeNode node = new TreeNode();
                InitializeNode(node, item);
                nodeParent.Nodes.Add(node);

                AddRange(node, les, divs);
                AddRange(node, divs);
            }
        }
        private void AddRange(TreeNode nodeParent, List<Division> divs)
        {
            if (divs != null)
            {
                IEnumerable<Division> divs1 = null;
                if (nodeParent.Tag.GetType() == typeof(LegalEntity))
                    divs1 = divs.Where(x => x.ParentDivision == null && x.Employer.Id == ((LegalEntity)nodeParent.Tag).Id);
                else if (nodeParent.Tag.GetType() == typeof(Division))
                    divs1 = divs.Where(x => x.ParentDivision != null && x.ParentDivision.Id == ((Division)nodeParent.Tag).Id);
                if (divs1.Count() > 0)
                {
                    foreach (var item in divs1)
                    {
                        TreeNode node = new TreeNode();
                        InitializeNode(node, item);
                        nodeParent.Nodes.Add(node);

                        AddRange(node, divs);
                    }
                }
            }
        }
        static internal void InitializeNode(TreeNode node, LegalEntity item)
        {
            node.Text = item.NameRusShort;
            node.Tag = item;
            node.Name = item.Id.ToString();
        }
        static internal void InitializeNode(TreeNode node, Division item)
        {
            node.Text = item.NameRusShort;
            node.Tag = item;
            node.Name = item.Id.ToString();

            node.ImageIndex = 1;
            node.SelectedImageIndex = 1;
        }

        public delegate void UCRefreshEventHandler();
        public event UCRefreshEventHandler UCRefreshEvent;
        private void refreshButton_Click(object sender, EventArgs e)
        {
            if (UCRefreshEvent != null)
            {
                UCRefreshEvent();
            }
        }

        private void tv_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Collapse && e.Action != TreeViewAction.Expand)
            {
                if (tv.SelectedNode != null && tv.SelectedNode.Tag.GetType() != typeof(char))
                    RaiseSelectedNodeChangedEvent(tv.SelectedNode);
                else
                    RaiseSelectedNodeChangedEvent(null);
            }
        }

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

        public bool ShowToolbar
        {
            set
            {
                toolStrip1.Visible = value;
            }
            get
            {
                return toolStrip1.Visible;
            }
        }
        public bool ShowRefreshButton
        {
            set
            {
                refreshButton.Visible = value;
            }
            get
            {
                return refreshButton.Visible;
            }
        }
        public bool ShowAddNewButton
        {
            set
            {
                addNewLEButton.Visible = value;
            }
            get
            {
                return addNewLEButton.Visible;
            }
        }
        public bool ShowDeleteButton
        {
            set
            {
                deleteButton.Visible = value;
            }
            get
            {
                return deleteButton.Visible;
            }
        }
        public bool ShowDataActual
        {
            set
            {
                toolStripLabel1.Visible =
                dateActualTextBox.Visible = value;
            }
            get
            {
                return dateActualTextBox.Visible;
            }
        }
        public void RemoveNode(LegalEntity le)
        {
            tv.Controls.RemoveByKey(le.Id.ToString());
        }
        public LegalEntity LegalEntitySelected
        {
            set
            {
                tv.SelectedNode = null;
                if (value != null)
                {
                    TreeNode[] nodes = tv.Nodes.Find(value.Id.ToString(), true);
                    if (nodes != null && nodes.Length > 0)
                        tv.SelectedNode = nodes[0];
                }
                RaiseSelectedNodeChangedEvent(tv.SelectedNode);
            }
            get
            {
                if (tv.SelectedNode != null && tv.SelectedNode.Tag.GetType() != typeof(Enums.LegalEntityType))
                {
                    return (LegalEntity)tv.SelectedNode.Tag;
                }
                return null;
            }
        }

        public delegate void UCAddNewLEEventHandler();
        public event UCAddNewLEEventHandler UCAddNewLEEvent;
        protected virtual void RaiseAddNewLEEvent()
        {
            //tv.SelectedNode = null;
            if (UCAddNewLEEvent != null)
            {
                UCAddNewLEEvent();
            }
        }
        private void addNewLEButton_Click(object sender, EventArgs e)
        {
            RaiseAddNewLEEvent();
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

        private void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (LegalEntitySelected != null)
                {
                    DataManager.GetInstance().LegalEntityRepository.Delete(LegalEntitySelected.Id);
                    RemoveNode(LegalEntitySelected);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка в " + this, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        List<TreeNode> _FoundedNodes = null;
        int _iCurFoundedNode = -1;
        private void findNextButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(findPatternTextBox.Text))
            {
                if (_FoundedNodes == null)
                {
                    _FoundedNodes = new List<TreeNode>();
                    foreach (TreeNode node in tv.Nodes)
                    {
                        FindNodes(node);
                    }
                    _iCurFoundedNode = -1;
                    if (_FoundedNodes.Count > 0)
                        _iCurFoundedNode = 0;
                }

                if (_FoundedNodes.Count > 0)
                {
                    if (_iCurFoundedNode == _FoundedNodes.Count)
                    {
                        Console.Beep();
                        _iCurFoundedNode = 0;
                    }
                    tv.SelectedNode = _FoundedNodes[_iCurFoundedNode++];
                    return;
                }
                Console.Beep();
            }
        }

        private void FindNodes(TreeNode nodeRoot)
        {
            if (nodeRoot.Text.ToUpper().IndexOf(findPatternTextBox.Text.ToUpper()) >= 0)
                _FoundedNodes.Add(nodeRoot);
            foreach (TreeNode node in nodeRoot.Nodes)
            {
                FindNodes(node);
            }
        }

        private void findPatternTextBox_TextChanged(object sender, EventArgs e)
        {
            _FoundedNodes = null;
        }

        private void mnuShowDivisionsToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            MessageBox.Show("mnuShowDivisionsToolStripMenuItem_CheckedChanged - " + mnuShowDivisionsToolStripMenuItem.Checked);
        }
    }
}
