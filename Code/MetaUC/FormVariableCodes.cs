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
    public partial class FormVariableCodes : Form
    {
        public FormVariableCodes()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormVariableCodes_Load(object sender, EventArgs e)
        {
            ucVariableCodes.Fill();
        }
    }
}
