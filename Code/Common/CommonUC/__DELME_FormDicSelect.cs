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
    public partial class __DELME_FormDicSelect : Form
    {
        public __DELME_FormDicSelect(string caption, List<DicItem> dics)
        {
            InitializeComponent();

            Text = caption;
            if (dics != null)
                ucDicList.AddRange(dics);

            ucDicList.ShowToolbar = false;
            ucDicList.ShowCheckBox = false;
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

        public List<int> CheckedId
        {
            get
            {
                return ucDicList.CheckedId;
            }
        }
        public DicItem CurrentDicItem
        {
            get
            {
                return ucDicList.CurrentDicItem;
            }
        }
        public List<int> SelectedId
        {
            get
            {
                return ucDicList.CheckedId;
            }
        }

        private void ucDicList_UCDoubleClick()
        {
            button1_Click(null, null);
        }
        public bool IsNullPressed = false;
        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            IsNullPressed = true;
            Close();
        }

        private void FormDicSelect_Load(object sender, EventArgs e)
        {
            IsNullPressed = false;
        }
    }
}
