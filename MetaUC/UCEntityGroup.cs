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
    public partial class UCEntityGroup : UserControl
    {
        public UCEntityGroup()
        {
            InitializeComponent();
        }

        private void UCEntityGroup_Load(object sender, EventArgs e)
        {
            List<Entity> entityes = Meta.DataManager.GetInstance().EntityRepository.SelectEntityes();

            TreeNode node;
            foreach (var entity in entityes.OrderBy(x => x.NameRus))
            {
                node = new TreeNode(entity.NameRus);
                node.Tag = entity;
                node.ImageIndex = 0;
                tv.Nodes.Add(node);

                RefreshChild(node);
            }
        }

        Entity Entity
        {
            get
            {
                return tv.SelectedNode.Tag.GetType() == typeof(Entity) ? (Entity)tv.SelectedNode.Tag : (Entity)tv.SelectedNode.Parent.Tag;
            }
        }
        EntityGroup Group
        {
            get
            {
                return tv.SelectedNode.Tag.GetType() == typeof(EntityGroup) ? (EntityGroup)tv.SelectedNode.Tag : null;
            }
        }
        Dictionary<string, List<Common.IdName>> _entityItems = new Dictionary<string, List<Common.IdName>>();
        private void FillGroupItems(EntityGroup eg)
        {
            Cursor cs = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                groupItemsList.Clear();

                if (eg == null) return;

                List<int[]> groupItemsId = Meta.DataManager.GetInstance().EntityGroupRepository.SelectEntities(eg.Id);

                // Получить все элементы сущности (в виде списка элементов словаря),
                // если они не были получены ранее.
                List<Common.IdName> egItems = null;
                if (!_entityItems.TryGetValue(eg.TabName, out egItems))
                {
                    Dictionary<int, string> entityItems = Meta.DataManager.GetInstance().EntityRepository.SelectEntityItemsAll(eg.TabName);
                    egItems = new List<Common.IdName>(entityItems.Select(x => new Common.IdName() { Id = x.Key, Name = x.Value }));
                    _entityItems.Add(eg.TabName, egItems);
                }

                // Упорядочить элементы группы
                List<Common.IdName> groupItems = new List<Common.IdName>();
                foreach (var item in groupItemsId.OrderBy(x => x[1]))
                {
                    Common.IdName di = egItems.FirstOrDefault(x => x.Id == item[0]);
                    if (di != null)
                        groupItems.Add(di);
                    else
                        MessageBox.Show("\" В группе \"" + eg.Name + "\""
                            + " присутствует/указан элемент с кодом id = " + item[0]
                            + ", который отсутствует в справочнике \"" + eg.TabName,
                            "ВНИМАНИЕ!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                groupItemsList.SetDataSource(groupItems);
                FillFreeItemsList(eg.TabName, null);
            }
            finally
            {
                this.Cursor = cs;
            }
        }
        /// <summary>
        /// Заполнение списка элементов сущности не находящихся в группе
        /// с учётом возможного для них фильтра (по кодам элеменов).
        /// </summary>
        /// <param name="freeItemsIdIncluded">Все, если null.</param>
        void FillFreeItemsList(string TabName, List<int> freeItemsIdIncluded)
        {
            List<Common.IdName> egItems = _entityItems[TabName];
            List<Common.IdName> groupItems = groupItemsList.GetDataSource().Select(x => (Common.IdName)x).ToList();
            // TODO: перепроверить алгоритм
            List<Common.IdName> disFree = 
                egItems.Where(x => !groupItems.Exists(y => y.Id == x.Id)
                && (freeItemsIdIncluded == null || freeItemsIdIncluded.Exists(y => y == x.Id))).OrderBy(x => x.Name).ToList();
            freeItemsList.SetDataSource(disFree);
        }
        /// <summary>
        /// Отобразить свободные элементы с указанными кодами.
        /// Отобразить все свободные (не в принадлежащие группе) элементы, если null.
        /// </summary>
        public List<int> FreeItemsIdFilter
        {
            set
            {
            }
        }
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            groupItemsList.Clear();

            if (e.Node.Tag.GetType() == typeof(EntityGroup))
            {
                FillGroupItems((EntityGroup)e.Node.Tag);
                RaiseSelectedEntityGroupChanged((EntityGroup)e.Node.Tag);
            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Entity != null)
            {
                string s = Common.FormEnterString.Show("Введите название новой группы для элементов справочника");
                if (s != null)
                {
                    int id = Meta.DataManager.GetInstance().EntityGroupRepository.InsertGroup(s, this.Entity.NameEng);
                    RefreshChild(tv.SelectedNode.Tag.GetType() == typeof(Entity) ? tv.SelectedNode : tv.SelectedNode.Parent);
                }
            }
        }

        private void RefreshChild(TreeNode entityNode)
        {
            entityNode.Nodes.Clear();
            List<EntityGroup> groups = Meta.DataManager.GetInstance().EntityGroupRepository.SelectByEntityTableName(((Entity)entityNode.Tag).NameEng);

            foreach (var group in groups.OrderBy(x => x.Name))
            {
                TreeNode node = new TreeNode(group.Name);
                node.Tag = group;
                node.ImageIndex = 1;
                node.ContextMenuStrip = groupContextMenuStrip;

                entityNode.Nodes.Add(node);
            }
            tv.SelectedNode = entityNode;
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Entity != null && this.Group != null)
            {
                string s = Common.FormEnterString.Show("Уверены, что хотите удалить группу элементов справочника (да/нет)?", "нет");
                if (s != null && s == "да")
                {
                    Meta.DataManager.GetInstance().EntityGroupRepository.DeleteGroup(this.Group.Id);
                    RefreshChild(tv.SelectedNode.Tag.GetType() == typeof(Entity) ? tv.SelectedNode : tv.SelectedNode.Parent);
                }
            }
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Group != null)
            {
                string s = Common.FormEnterString.Show("Введите новое название группы", Group.Name);
                if (s != null)
                {
                    Meta.DataManager.GetInstance().EntityGroupRepository.UpdateGroup(Group.Id, s);
                    RefreshChild(tv.SelectedNode.Tag.GetType() == typeof(Entity) ? tv.SelectedNode : tv.SelectedNode.Parent);
                }
            }
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshChild(tv.SelectedNode.Tag.GetType() == typeof(Entity) ? tv.SelectedNode : tv.SelectedNode.Parent);
        }

        private void insertGroupItemButton_Click(object sender, EventArgs e)
        {
            List<object> items = freeItemsList.GetSelectedItems();
            if (items.Count == 0)
            {
                MessageBox.Show("Для добавления элементов выберите их в правом списке.");
                return;
            }
            groupItemsList.AddRange(items);
            freeItemsList.Remove(items);
        }

        private void deleteGroupItemButton_Click(object sender, EventArgs e)
        {
            List<object> items = groupItemsList.GetSelectedItems();
            if (items.Count == 0)
            {
                MessageBox.Show("Для удаления элементов выберите их в левом списке.");
                return;
            }
            freeItemsList.AddRange(items);
            groupItemsList.Remove(items);
        }

        private void saveGroupItemsBtton_Click(object sender, EventArgs e)
        {
            if (Group != null)
            {
                EntityGroupRepository rep = Meta.DataManager.GetInstance().EntityGroupRepository;

                List<int> ids = groupItemsList.GetDataSource().Select(x => ((Common.IdClass)x).Id).ToList();
                rep.UpdateGroupItems(Group.Id, ids);

                rep.UpdateGroup(Group.Id, Group.Name);

                MessageBox.Show("Сохранено " + ids.Count + " элементов группы \"" + Group.Name + "\"");
            }
        }
        EntityFilterId freeItemsFilterId = new EntityFilterId();
        private void freeItemsFilterButton_Click(object sender, EventArgs e)
        {
            try
            {
                FillFreeItemsList(Entity.NameEng, freeItemsFilterId.GetFilteredId(Entity.NameEng));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public delegate void UCSelectedEntityGroupChangedEventHandler(EntityGroup eg);
        public event UCSelectedEntityGroupChangedEventHandler UCSelectedItemChanged;
        protected virtual void RaiseSelectedEntityGroupChanged(EntityGroup eg)
        {
            if (UCSelectedItemChanged != null)
            {
                UCSelectedItemChanged(eg);
            }
        }

    }
}
