using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOV.Amur.Data
{
    public partial class FormRichTextBox : Form
    {
        public FormRichTextBox()
        {
            InitializeComponent();
        }
        override public string Text
        {
            get
            {
                if (rtb == null)
                    return null;
                return rtb.Text;
            }
            set
            {
                rtb.Text = value;
            }
        }
        public string Caption
        {
            get
            {
                return this.Text;
            }
            set
            {
                this.Text = value;
            }
        }
        public void Fill(string text)
        {
            Text = text;
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
