using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SOV.Amur.Meta;
using SOV.Common;

namespace SOV.Amur.Reports
{
    public partial class UCF50DGV : UserControl
    {

        public UCF50DGV(F50Collection f50)
        {
            InitializeComponent();

            Report rep = DataManager.GetInstance().ReportRepository.Select(50);
            rptCaption.Text = rep.NameFull
                + " " + f50.Year + ", " + DateTimeProcess.MonthNameRus[f50.Month - 1]
                + ((f50.ReportTimeUnit == EnumTime.DecadeOfYear)
                ? ", декада " + f50.MonthDecade : "");

            dgv.DataSource = f50;
        }

        private void UCF50DGV_Load(object sender, EventArgs e)
        {
            // PAINT DGV

            int iColMax = dgv.Columns["gageMax"].Index;
            int iColPoyma = dgv.Columns["gagePoyma"].Index;
            int iColNya = dgv.Columns["gageNya"].Index;
            int iColOya = dgv.Columns["gageOya"].Index;
            foreach (DataGridViewRow item in dgv.Rows)
            {
                double? valueMax = (double?)item.Cells[iColMax].Value;
                if (!valueMax.HasValue) continue;

                DataGridViewCell[] cells = new DataGridViewCell[] { item.Cells[iColPoyma], item.Cells[iColNya], item.Cells[iColOya] };

                for (int i = 0; i < cells.Length; i++)
                {
                    if (cells[i].Value != null && valueMax > (double)cells[i].Value)
                    {
                        cells[i].Style.ForeColor = Color.Red;
                        cells[i].ToolTipText = "Превышение...";
                    }
                }
                dgv.Columns["gageAvg"].AutoSizeMode =
                dgv.Columns["gageMin"].AutoSizeMode =
                dgv.Columns["gageMax"].AutoSizeMode =
                dgv.Columns["gageClm"].AutoSizeMode =
                dgv.Columns["precipitation"].AutoSizeMode =
                dgv.Columns["GageAnomaly"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            }
        }
    }
}
