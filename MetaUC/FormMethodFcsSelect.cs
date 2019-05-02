using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SOV.Social;

namespace SOV.Amur.Meta
{
    public partial class FormMethodFcsSelect : Form
    {
        public FormMethodFcsSelect()
        {
            InitializeComponent();
        }

        public void Fill(List<MethodForecast> methods, List<SOV.Social.LegalEntity> sources)
        {
            methodComboBox.Items.AddRange(methods.ToArray());
            sourceComboBox.Items.AddRange(sources.ToArray());
        }

        public MethodForecast MethodForecast
        {
            get
            {
                return (MethodForecast)methodComboBox.SelectedItem;
            }
        }
        public LegalEntity Source
        {
            get
            {
                return (LegalEntity)sourceComboBox.SelectedItem;
            }
        }

        public List<double> FcsLags
        {
            get
            {
                List<double> ret = new List<double>();
                foreach (var item in fcsLagListBox.SelectedItems)
                {
                    if (item == "(Все)")
                        return null;
                    ret.Add(double.Parse((string)item));
                }
                return ret;
            }
        }

        public bool IsAllFcsLags
        {
            get
            {
                return FcsLags == null;
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

        private void methodComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            fcsLagListBox.Items.Clear();
            fcsLagListBox.Items.Add("(Все)");

            double[] leadTimes = ((MethodForecast)methodComboBox.SelectedItem).LeadTimes;
            if (leadTimes != null)
            {
                foreach (var item in leadTimes)
                {
                    fcsLagListBox.Items.Add(item.ToString());
                }
            }
        }
    }
}
