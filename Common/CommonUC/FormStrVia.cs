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
    public partial class FormStrVia : Form
    {
        public FormStrVia(string caption, string nameColumnHeader, string valueColumnHeader)
        {
            InitializeComponent();

            Text = caption;
            ucStrVia.UCNameColumnHeader = nameColumnHeader;
            ucStrVia.UCValueColumnHeader = valueColumnHeader;
        }
        public bool NameColumnReadOnly
        {
            set
            {
                ucStrVia.UCNameColumnReadOnly = value;
            }
        }
        public bool ValueColumnReadOnly
        {
            set
            {
                ucStrVia.UCValueColumnReadOnly = value;
            }
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
