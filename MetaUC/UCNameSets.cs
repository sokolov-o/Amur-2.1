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
    public partial class UCNameSets : UserControl
    {
        public UCNameSets()
        {
            InitializeComponent();

            langComboBox.SelectedIndex = 0;
        }
        int _nameTypeId = 1;

        private void findLikeButton_Click(object sender, EventArgs e)
        {
            existsListBox.Clear();
            if (!string.IsNullOrEmpty(likeTextBox.Text))
            {
                List<NameItem> items = DataManager.GetInstance().NameItemRepository.SelectLike("%" + likeTextBox.Text + "%", langComboBox.SelectedIndex, _nameTypeId);
                Fill(items);
            }
            Console.Beep();
        }
        private void Fill(List<NameItem> items)
        {
            existsListBox.SetDataSource(items.Select(x => new IdName() { Id = x.NameSetId, Name = x.Name }).OrderBy(x => x.Name).ToList());
        }
        public int? CurrentNameSetId
        {
            get
            {
                object ret = existsListBox.CurrentId; //nameSetBindingSource.Current;
                return (ret != null) ? (int?)ret : null;
            }
        }
        public List<int> SelectedIds
        {
            get
            {
                return existsListBox.GetSelectedItemsId(); //nameSetBindingSource.Current;
            }
        }
        FormNameItem _FormNameItem;
        FormNameItem FormNameItem
        {
            get
            {
                if (_FormNameItem == null)
                {
                    _FormNameItem = new FormNameItem(EnumFormView.AddNewSets);
                    _FormNameItem.FormBorderStyle = FormBorderStyle.FixedDialog;
                    _FormNameItem.StartPosition = FormStartPosition.CenterScreen;
                }
                return _FormNameItem;
            }
        }
        private void addNewnameSetButton_Click(object sender, EventArgs e)
        {
            try
            {
                FormNameItem.Show();

                List<NameItem> items = DataManager.GetInstance().NameItemRepository.Select(FormNameItem.NameSetIdProcessed);
                Fill(items);

            }
            finally
            {
                FormNameItem.NameSetIdProcessed = new List<int>();
            }
        }

        private void addNewFromSearchBoxButton_Click(object sender, EventArgs e)
        {
            List<NameItem> items = DataManager.GetInstance().NameItemRepository.SelectLike(likeTextBox.Text, langComboBox.SelectedIndex, _nameTypeId);
            if (items.Count > 0)
            {
                if (MessageBox.Show("В базе данных существуют " + items.Count + "записей с наименованием " + likeTextBox.Text + "\n"
                    + "Создать новый набор наименований с этим содержанием?",
                    "Вы уверены?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
            }

            NameItem newItem = new NameItem()
            {
                Id = -1,
                NameSetId = -1,
                LangId = langComboBox.SelectedIndex,
                Name = likeTextBox.Text,
                NameTypeId = _nameTypeId
            };
            DataManager.GetInstance().NameItemRepository.Insert(newItem);
            Add(newItem, true);

            Fill(items);
        }

        private void Add(NameItem newItem, bool isCurrent)
        {
            existsListBox.AddRange(new List<object> { new IdName() { Id = newItem.Id, Name = newItem.Name } });
            existsListBox.SetSelectedItemById(newItem.Id);
        }

        private void existsListBox_UCSelectedItemChanged()
        {
            ucNameItem.Clear();
            if (CurrentNameSetId.HasValue)
                ucNameItem.Fill((int)CurrentNameSetId);
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            UCShowNameItems = !UCShowNameItems;
        }

        public bool UCShowNameItems
        {
            set
            {
                splitContainer1.Panel2Collapsed = !value;
            }
            get
            {
                return !splitContainer1.Panel2Collapsed;
            }
        }
    }
}
