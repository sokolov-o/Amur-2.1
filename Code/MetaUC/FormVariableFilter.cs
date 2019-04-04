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
    public partial class FormVariableFilter : Form
    {
        public FormVariableFilter()
        {
            InitializeComponent();
        }
        VariableFilter _vfPrev = null;
        public VariableFilter VariableFilter
        {
            get
            {
                return ucVariableFilter1.VariableFilter;
            }
            set
            {
                _vfPrev = VariableFilter;
                ucVariableFilter1.VariableFilter = value;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.VariableFilter = _vfPrev;

            Close();
        }
    }
}
