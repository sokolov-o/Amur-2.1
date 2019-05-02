using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOV.Amur.Reports
{
    public partial class FormF50ReportFilter : Form
    {
        public FormF50ReportFilter()
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
        public int SiteGroup { get { return ucF50ReportFilter1.SiteGroup; } }
        public byte? FlagAQC { get { return ucF50ReportFilter1.FlagAQC; } }
        public int Year { get { return ucF50ReportFilter1.Year; } }
        public int Month { get { return ucF50ReportFilter1.Month; } }
        public int? DecadeOfMonth { get { return ucF50ReportFilter1.DecadeOfMonth; } }

        public int TimeUnit
        {
            get { return ucF50ReportFilter1.TimeUnit; }
            set
            {
                ucF50ReportFilter1.TimeUnit = value;
            }
        }

        public bool ShowTimeUnitControl
        {
            get
            {
                return ucF50ReportFilter1.ShowTimeUnitControl;
            }
            set
            {
                ucF50ReportFilter1.ShowTimeUnitControl = value;
            }
        }
    }
}
