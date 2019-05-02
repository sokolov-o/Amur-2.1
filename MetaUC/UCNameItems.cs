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
    public partial class UCNameItems : UserControl
    {
        public UCNameItems()
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
        public void FillNew()
        {
            Fill(-1);
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
        public List<NameItem> GetNameItems()
        {
            List<NameItem> ret = new List<NameItem>();
            foreach (var item in nameItemBindingSource)
            {
                ret.Add((NameItem)item);
            }
            return ret;
        }
        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                dgv.EndEdit();
                nameItemBindingSource.EndEdit();
                NameItemRepository rep = DataManager.GetInstance().NameItemRepository;
                foreach (var item in GetNameItems())
                {
                    rep.InsertUpdate(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public bool UCShowToolStrip
        {
            set
            {
                tlp.RowStyles[0].Height = 0;
                if (value)
                    tlp.RowStyles[0].SizeType = SizeType.AutoSize;
                else
                    tlp.RowStyles[0].SizeType = SizeType.Absolute;
            }
            get
            {
                return tlp.RowStyles[0].SizeType == SizeType.AutoSize;
            }
        }

        private void UCNameItems_Leave(object sender, EventArgs e)
        {
            dgv.EndEdit();
            nameItemBindingSource.EndEdit();
        }
    }
}
