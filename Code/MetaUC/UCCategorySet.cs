using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SOV.Common;

namespace SOV.Amur.Meta
{
    public partial class UCCategorySet : UserControl
    {
        public UCCategorySet()
        {
            InitializeComponent();
        }

        public int Language { get; set; }

        public void Fill(int? selectedCategorySetId = null)
        {
            List<CategorySet> categoryItems = DataManager.GetInstance().CategorySetRepository.Select();
            List<NameItem> names = DataManager.GetInstance().NameItemRepository.SelectByNameSetId(categoryItems.Select(x => x.Id).ToList());
            List<IdName> idNames = categoryItems
                .Select(x => new IdName() { Id = x.Id, Name = NameItem.GetName(names, x.Id, Language, 1) })
                .ToList();
            ucCategorySetList.SetDataSource(idNames);
        }
        private int? CurrentCategorySetId
        {
            get
            {
                object item = ucCategorySetList.GetSelectedItem();
                return (item != null) ? (int?)((IdName)item).Id : null;
            }
        }
        private CategoryItem CurrentCategoryItem
        {
            get
            {
                object item = categoryItemBindingSource.Current;
                return (item != null) ? (CategoryItem)item : null;
            }
        }
        private void ucCategorySetList_UCSelectedItemChanged()
        {
            categoryItemBindingSource.Clear();
            object item = ucCategorySetList.GetSelectedItem();
            if (item != null)
            {
                List<CategoryItem> categoryDefs = DataManager.GetInstance().CategoryItemRepository.SelectByCategorySetId(((IdName)item).Id);
                List<NameItem> nameItems = DataManager.GetInstance().NameItemRepository.SelectByNameSetId
                    (categoryDefs.Select(y => y.CategoryItemNameSetId).ToList());
                List<CategoryItemLocalized> categItems = categoryDefs
                    .Select(x => new CategoryItemLocalized(x, Language, nameItems))
                    .OrderBy(x => x.OrderBy)
                    .ToList();

                categoryItemBindingSource.DataSource = categItems;
            }
        }
        private void categoryItemLocalizedBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            ucNameItem.Clear();
            if (CurrentCategoryItem != null)
                ucNameItem.Fill((int)CurrentCategoryItem.CategoryItemNameSetId);
        }
        FormNameSets _formNameSetsIUS;
        FormNameSets FormNameSetsIUS
        {
            get
            {
                if (_formNameSetsIUS == null)
                {
                    _formNameSetsIUS = new FormNameSets(true);
                    _formNameSetsIUS.StartPosition = FormStartPosition.CenterScreen;
                }
                return _formNameSetsIUS;
            }
        }
        private void addNewItemButton_Click(object sender, EventArgs e)
        {
            if (CurrentCategorySetId != null)
            {
                if (FormNameSetsIUS.ShowDialog() == DialogResult.OK)
                {
                    CategoryItemRepository rep = DataManager.GetInstance().CategoryItemRepository;

                    // REMOVE ITEMS ALREADY EXISTING IN SET
                    List<CategoryItem> exists = rep.SelectByCategorySetId((int)CurrentCategorySetId);
                    List<int> forAdd = new List<int>(FormNameSetsIUS.SelectedIds);
                    forAdd.RemoveAll(x => exists.Exists(y => y.CategoryItemNameSetId == x));

                    foreach (int i in forAdd)
                    {
                        List<CategoryItemLocalized> items = ((List<CategoryItemLocalized>)categoryItemBindingSource.DataSource);
                        int code = items.Count == 0 ? 1 : items.Max(x => x.Code) + 1;
                        int orderBy = items.Count == 0 ? 1 : items.Max(x => x.OrderBy) + 1;

                        int id = rep.Insert(new CategoryItem()
                        {
                            CategorySetId = (int)CurrentCategorySetId,
                            CategoryItemNameSetId = i,
                            Code = code,
                            OrderBy = orderBy
                        });
                        ucCategorySetList_UCSelectedItemChanged();
                    }
                }
                return;
            }
            Console.Beep();
        }

        private void deleteItemButton_Click(object sender, EventArgs e)
        {
            if (CurrentCategoryItem != null)
            {
                DataManager.GetInstance().CategoryItemRepository.Delete((int)CurrentCategoryItem.Id);
                ucCategorySetList_UCSelectedItemChanged();
            }
        }

        private void upButton_Click(object sender, EventArgs e)
        {
            categoryItemBindingSource.EndEdit();
            dgvItems.EndEdit();
            dgvItems.SuspendLayout();
            int i = categoryItemBindingSource.Position;
            if (i != 0)
            {
                CategoryItem item = CurrentCategoryItem;
                if (item != null)
                {
                    categoryItemBindingSource.Remove(item);
                    categoryItemBindingSource.Insert(i - 1, item);
                    categoryItemBindingSource.Position = categoryItemBindingSource.IndexOf(item);
                }
            }
            dgvItems.ResumeLayout();
            dgvItems.Refresh();
        }

        private void downButton_Click(object sender, EventArgs e)
        {
            categoryItemBindingSource.EndEdit();
            dgvItems.EndEdit();
            dgvItems.SuspendLayout();
            int i = categoryItemBindingSource.Position;
            if (i != categoryItemBindingSource.Count - 1)
            {
                CategoryItem item = (CategoryItem)categoryItemBindingSource[i + 1];
                if (item != null)
                {
                    categoryItemBindingSource.Remove(item);
                    categoryItemBindingSource.Insert(i, item);
                }
            }
            dgvItems.ResumeLayout();
            dgvItems.Refresh();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            CategoryItemRepository rep = DataManager.GetInstance().CategoryItemRepository;
            int orderBy = ((List<CategoryItemLocalized>)categoryItemBindingSource.DataSource).Max(x => x.OrderBy);

            for (int i = 0; i < categoryItemBindingSource.Count; i++)
            {
                CategoryItem item = (CategoryItem)categoryItemBindingSource[i];
                item.OrderBy = ++orderBy + i;
                rep.Update(item);
            }
        }

        private void dgvItems_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            categoryItemBindingSource.DataSource = ((List<CategoryItemLocalized>)categoryItemBindingSource.DataSource).OrderBy(x => x.ItemName).ToList();
        }

        private void ucCategorySetList_UCAddNewEvent()
        {
            FormNameItem frm = new FormNameItem(EnumFormView.AddNewSet);
            frm.StartPosition = FormStartPosition.CenterScreen;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                //List<CategoryItemLocalized> items = ((List<CategoryItemLocalized>)categoryItemBindingSource.DataSource);
                //int code = items.Count == 0 ? 1 : items.Max(x => x.Code) + 1;
                //int orderBy = items.Count == 0 ? 1 : items.Max(x => x.OrderBy) + 1;

                DataManager.GetInstance().CategorySetRepository.Insert(new CategorySet()
                {
                    Id = (int)FormNameSetsIUS.SelectedIds[0]
                });
                Fill((int)FormNameSetsIUS.SelectedIds[0]);
            }
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            UCShowNameItems = !UCShowNameItems;
        }

        public bool UCShowNameItems
        {
            set
            {
                splitContainer2.Panel2Collapsed = !value;
            }
            get
            {
                return !splitContainer2.Panel2Collapsed;
            }
        }
    }
}
