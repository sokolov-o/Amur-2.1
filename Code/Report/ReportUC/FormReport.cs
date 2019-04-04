using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Microsoft.Reporting.WinForms;

namespace FERHRI.Amur.Report
{
    public partial class FormReport : Form
    {
        public FormReport(UserControl uc, int reportId )
        {
            InitializeComponent();
            this.Controls.Add(uc);
            uc.Dock = DockStyle.Fill;
            uc.Visible = true;

            Report rep = DataManager.GetInstance().ReportRepository.Select(reportId);
            Text = rep.NameFull;    
        }
    }
}
