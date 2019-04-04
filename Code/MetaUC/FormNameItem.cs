using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOV.Amur.Meta
{
    public partial class FormNameItem : Form
    {
        public FormNameItem(EnumFormView formView)
        {
            InitializeComponent();

            SetFormView(formView);
        }

        EnumFormView _formView;

        public void SetFormView(EnumFormView formView)
        {
            _formView = formView;
            switch (_formView)
            {
                case EnumFormView.AddNewSets:
                    Text = "Ввод новых наборов имён";
                    button1.Text = "Сохранить и ввести новый набор";
                    button2.Text = "Закрыть";
                    break;
                default:
                    throw new Exception("Неизвестный вид формы " + _formView);
            }
        }
        public List<int> NameSetIdProcessed = new List<int>();
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                NameItemRepository rep = DataManager.GetInstance().NameItemRepository;
                bool firstInsUpd = false;
                int id = -1;
                List<NameItem> items = ucNameItems.GetNameItems();

                foreach (var item in items)
                {
                    if (_formView == EnumFormView.AddNewSets && firstInsUpd)
                        item.NameSetId = id;

                    rep.InsertUpdate(item);
                    id = item.NameSetId;

                    if (!firstInsUpd)
                        NameSetIdProcessed.Add(id);
                    firstInsUpd = true;
                }
                ucNameItems.FillNew();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
