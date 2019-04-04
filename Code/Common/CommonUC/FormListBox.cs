using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOV.Common
{
    public partial class FormListBox : Form
    {
        public FormListBox(string formCaption, object[] dataSourceIdNames, string dataPropertyName, bool showNULLButton)
        {
            InitializeComponent();

            Text = formCaption;

            lb.HideTollbarControls();
            lb.ShowToolbar
                = lb.ShowFindItemToolbarButton
                = lb.ShowSelectAllToolbarButton
                = lb.ShowSelectedOnly
                = lb.ShowSelectedOnlyToolbarButton
                = lb.ShowUnselectAllToolbarButton
                = lb.MultiSelect
                = true;

            lb.SetDataSource(dataSourceIdNames.ToList(), dataPropertyName);
            ShowNULLButton = showNULLButton;
        }
        public bool IsNullPressed = false;
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            IsNullPressed = true;
            Close();
        }

        private void ucListBox_UCDoubleClick()
        {
            button1_Click(null, null);
        }

        public bool ShowNULLButton
        {
            set
            {
                button3.Visible = value;
            }
        }
        public List<object> GetSelectedItems()
        {
            return lb.GetSelectedItems();
        }
        public object GetSelectedItem()
        {
            return lb.GetSelectedItem();
        }
    }
}
