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
    public partial class FormOptionsUCDataEdit : Form
    {
        public FormOptionsUCDataEdit()
        {
            InitializeComponent();

            Options = new OptionsDataEdit(false, false, true, false, false, false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        public OptionsDataEdit Options
        {
            get
            {
                return new OptionsDataEdit(
                    showDataDetailsCheckBox.Checked,
                    onlyRedCheckBox.Checked,
                    showChartCheckBox.Checked,
                    showAQCCheckBox.Checked,
                    showDerivedCheckBox.Checked,
                    showVarCodeTextCheckBox.Checked
                    );
            }
            set
            {
                showDataDetailsCheckBox.Checked = value.ShowDataDetails;
                onlyRedCheckBox.Checked = value.OnlyRedValues;
                showChartCheckBox.Checked = value.ShowChart;
                showAQCCheckBox.Checked = value.ShowAQC;
                showDerivedCheckBox.Checked = value.ShowDerived;
            }
        }

        private void showDataDetailsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = showDataDetailsCheckBox.Checked;
        }
    }
}
