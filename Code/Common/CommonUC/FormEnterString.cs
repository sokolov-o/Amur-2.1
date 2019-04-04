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
    public partial class FormEnterString : Form
    {
        FormEnterString()
        {
            InitializeComponent();
        }
        static public string Show(string caption, string defaultText = null)
        {
            FormEnterString frm = new FormEnterString();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Text = caption;
            frm.textBox1.Text = defaultText;
            frm.textBox1.Focus();
            frm.textBox1.SelectAll();

            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                return frm.textBox1.Text;
            return null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
