using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FERHRI.Common;

namespace FERHRI.Amur.Meta
{
    public partial class UCCategoryDefinition : UserControl
    {
        public UCCategoryDefinition()
        {
            InitializeComponent();
        }

        public int Language { get; set; }

        public void Fill(int? selectedItemId = null)
        {
            List<CategoryDefinition> categoryDefs = DataManager.GetInstance().CategoryDefinitionRepository.SelectByCategoryId();
            List<NameItem> names = DataManager.GetInstance().NameItemRepository.SelectByNameSetId
                (
                categoryDefs.Select(x => x.CategoryId)
                .Union(categoryDefs.Select(y => y.CategoryItemId))
                .Distinct()
                .ToList()
                );
            List<IdName> idNames = categoryDefs
                .Select(x => new IdName() { Id = x.CategoryId, Name = NameItem.GetName(names, x.CategoryId, Language, 1) })
                .ToList();
            ucCategoryList.SetDataSource(idNames);
        }

        private void ucCategoryList_UCSelectedItemChanged()
        {
            categoryItemLocalizedBindingSource.Clear();
            object item = ucCategoryList.GetSelectedItem();
            if (item != null)
            {
                List<CategoryDefinition> categoryDefs = DataManager.GetInstance().CategoryDefinitionRepository.SelectByCategoryId(((IdName)item).Id);
                List<NameItem> nameItems = DataManager.GetInstance().NameItemRepository.SelectByNameSetId(categoryDefs.Select(y => y.CategoryItemId).ToList());
                List<CategoryItemLocalized> categItems = categoryDefs.Select(x => new CategoryItemLocalized()
                {
                    CategoryDefinition = x,
                    ItemName = NameItem.GetName(nameItems, x.CategoryItemId, Language, 1),
                    ItemNameShort = NameItem.GetName(nameItems, x.CategoryItemId, Language, 2),
                    ItemDescription = NameItem.GetName(nameItems, x.CategoryItemId, Language, 3)
                }).ToList();

                categoryItemLocalizedBindingSource.DataSource = categItems;
            }
        }

        private void categoryItemLocalizedBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            ucNameItem.Clear();
            if (categoryItemLocalizedBindingSource.Current != null)
            {
                int id = ((CategoryItemLocalized)categoryItemLocalizedBindingSource.Current).CategoryDefinition.CategoryItemId;
                ucNameItem.Fill(id);
            }
        }

        private void addNewItemButton_Click(object sender, EventArgs e)
        {
            
        }
    }
}
