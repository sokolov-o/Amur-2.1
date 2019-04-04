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
    public partial class UCNameItem : UserControl
    {
        public UCNameItem()
        {
            InitializeComponent();

            langBindingSource.DataSource = new List<IdName>() { new IdName() { Id = 0, Name = "rus" }, new IdName() { Id = 1, Name = "eng" } };
            nameTypeBindingSource.DataSource = new List<IdName>() { new IdName() { Id = 1, Name = "Полное" }, new IdName() { Id = 2, Name = "Кратко" }, new IdName() { Id = 3, Name = "Описание" } };
        }
        int _nameSetId;
        public void Fill(int nameSetId)
        {
            Clear();
            _nameSetId = nameSetId;
            List<NameItem> names = DataManager.GetInstance().NameItemRepository.SelectByNameSetId(nameSetId);
            nameItemBindingSource.DataSource = names;
        }
        public void Clear()
        {
            nameItemBindingSource.Clear();
        }

        private void nameItemBindingSource_AddingNew(object sender, AddingNewEventArgs e)
        {
            NameItem item = new NameItem()
            {
                LangId = 0,
                NameTypeId = 1,
                NameSetId = _nameSetId
            };
            e.NewObject = item;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                NameItemRepository rep = DataManager.GetInstance().NameItemRepository;
                foreach (var item in nameItemBindingSource)
                {
                    rep.InsertUpdate((NameItem)item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
