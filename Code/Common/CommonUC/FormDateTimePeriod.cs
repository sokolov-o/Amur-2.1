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
    public partial class FormDateTimePeriod : Form
    {
        public FormDateTimePeriod()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
        public DateTimePeriod DateTimePeriod
        {
            get
            {
                return uc.DateTimePeriod;
            }
            set
            {
                uc.DateTimePeriod = value;
            }
        }
    }
}
