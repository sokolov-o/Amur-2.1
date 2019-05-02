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
    public partial class FormNameSets : Form
    {
        bool _isSelect;
        public FormNameSets(bool isSelect)
        {
            InitializeComponent();

            _isSelect = isSelect;

            Text = _isSelect ? "Выбрать наименование" : "Просмотр и редактирование наименований";
            button1.Text = _isSelect ? "Выбрать" : "Закрыть";
            button2.Visible = _isSelect;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_isSelect)
                Hide();
            else
                Close();
        }

        public List<int> SelectedIds
        {
            get
            {
                return ucNameSets.SelectedIds;
            }
        }
    }
}
