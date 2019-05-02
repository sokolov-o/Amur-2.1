using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SOV.Common
{
    public partial class FormSelectListItems : Form
    {
        public FormSelectListItems(string caption, object[] items, string dataPropertyName)
        {
            InitializeComponent();

            Text = caption;
            ucTable.SetDataSource(items.ToList(), dataPropertyName);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        public List<object> SelectedItems
        {
            get
            {
                return ucTable.GetSelectedItems();
            }
        }
        public List<int> SelectedItemsId
        {
            get
            {
                return ucTable.GetSelectedItemsId();
            }
        }

        private void ucDicList_UCDoubleClick()
        {
            button1_Click(null, null);
        }
        public bool AreNullPressed = false;
        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            AreNullPressed = true;
            Close();
        }

        private void FormDicSelect_Load(object sender, EventArgs e)
        {
            AreNullPressed = false;
        }
    }
}
