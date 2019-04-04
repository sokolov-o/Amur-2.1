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

namespace SOV.Amur.Data
{
    public partial class UCDataTableClimate2Site : UserControl
    {
        public UCDataTableClimate2Site()
        {
            InitializeComponent();

            mnuYearToolStripMenuItem.Tag = EnumTime.YearCommon;
            mnuMonthToolStripMenuItem.Tag = EnumTime.Month;
            mnuDecadeToolStripMenuItem.Tag = EnumTime.DecadeOfYear;
            mnuPentadeToolStripMenuItem.Tag = EnumTime.PentadeOfYear;
        }
        int _iColTime = 0;
        void AddColumns(List<Climate> clm)
        {
            DataGridViewColumn col;
            List<Variable> clmVars = Meta.DataManager.GetInstance().VariableRepository.Select(clm.Select(x => x.VariableId).Distinct().ToList());

            foreach (Variable clmVar in clmVars)
            {
                foreach (int clmDataTypeId in clm.Where(x => x.VariableId == clmVar.Id).Select(x => x.DataTypeId).Distinct())
                {
                    col = dgv.Columns[dgv.Columns.Add(
                        clmVar.Id.ToString(),
                        DataTypeRepository.GetCash().Find(x => x.Id == clmDataTypeId).NameShort + " - " + clmVar.NameRus)];
                    col.Tag = new object[] { clmVar, clmDataTypeId };

                }
            }
        }
        public void Fill(int siteId, int[] yearSF)
        {
            List<Climate> clm = Data.DataManager.GetInstance().ClimateRepository.SelectClimateMetaAndData(
                new List<int>(new int[] { siteId }), null, null, null, null, null, yearSF[0], yearSF[1]);
            List<Variable> clmVariables = Meta.DataManager.GetInstance().VariableRepository.Select(clm.Select(x => x.VariableId).Distinct().ToList());

            int iColData = dgv.Columns.Count;

            #region CREATE COLUMNS
            DataGridViewColumn col;
            List<Variable> clmVars = Meta.DataManager.GetInstance().VariableRepository.Select(clm.Select(x => x.VariableId).Distinct().ToList());

            foreach (Variable clmVar in clmVars)
            {
                foreach (int clmDataTypeId in clm.Where(x => x.VariableId == clmVar.Id).Select(x => x.DataTypeId).Distinct())
                {
                    col = dgv.Columns[dgv.Columns.Add(
                        clmVar.Id.ToString(),
                        DataTypeRepository.GetCash().Find(x => x.Id == clmDataTypeId).NameShort + " - " + clmVar.NameRus)];
                    col.Tag = new object[] { clmVar, clmDataTypeId };

                }
            }
            #endregion CREATE COLUMNS

            #region CREATE ROWS
            DataGridViewRow row = dgv.Rows[dgv.Rows.Add()];
            row.Cells[_iColTime].Value = "Год";
            row.Cells[_iColTime].Tag = new object[] { EnumTime.YearCommon, 1 };
            row.DefaultCellStyle.BackColor = Color.AliceBlue;

            for (int i = 1; i <= 12; i++)
            {
                row = dgv.Rows[dgv.Rows.Add()];

                row.Cells[_iColTime].Value = "Месяц " + i;
                row.Cells[_iColTime].Tag = new object[] { EnumTime.Month, i };
                row.DefaultCellStyle.BackColor = Color.AliceBlue;

                for (int j = 1; j <= 3; j++)
                {
                    row = dgv.Rows[dgv.Rows.Add()];
                    row.Cells[_iColTime].Value = "  Декада " + j;
                    row.Cells[_iColTime].Tag = new object[] { EnumTime.DecadeOfYear, i, j };
                }
                for (int j = 1; j <= 6; j++)
                {
                    row = dgv.Rows[dgv.Rows.Add()];
                    row.Cells[_iColTime].Value = "    Пентада " + j;
                    row.Cells[_iColTime].Tag = new object[] { EnumTime.PentadeOfYear, i, j };
                }
            }
            #endregion CREATE ROWS

            // FILL DATA CELLS
            foreach (DataGridViewRow row_ in dgv.Rows)
            {
                object[] tag = (object[])row_.Cells[_iColTime].Tag;

                EnumTime et = (EnumTime)tag[0];
                int timeNum, month;
                if (et == EnumTime.DecadeOfYear || et == EnumTime.PentadeOfYear)
                {
                    timeNum = (int)tag[2];
                    month = (int)tag[1];
                }
                else
                {
                    timeNum = (int)tag[1];
                    month = -1;
                }

                for (int i = iColData; i < row_.Cells.Count; i++)
                {
                    DataGridViewCell cell = row_.Cells[i];

                    Variable var = (Variable)((object[])cell.OwningColumn.Tag)[0];
                    if (var.TimeId != (int)et) continue;
                    EnumDataType clmDataType = (EnumDataType)((object[])cell.OwningColumn.Tag)[1];

                    //List<Variable> vars = clmVariables.FindAll(x =>
                    //    x.VariableTypeId == (int)varType
                    //    && x.DataTypeId == (int)varDataType
                    //    && x.TimeId == (int)et
                    //    );
                    //Variable var = null;
                    //if (vars.Count() > 0)
                    //{
                    //    if (vars.Count() > 1)
                    //        throw new Exception("(vars.Count() > 1)");
                    //    var = vars[0];
                    //}
                    //if (var == null) continue;

                    List<Climate> clmm = clm.FindAll(x => x.VariableId == var.Id && x.DataTypeId == (int)clmDataType);
                    if (clmm.Count() > 0)
                    {
                        if (clmm.Count() > 1)
                            throw new Exception("(clmm.Count() > 1)");
                        double value;
                        if (clmm[0].Data.TryGetValue(
                            (month == -1) ? (short)timeNum : (short)Common.DateTimeProcess.GetDecadeYearByDecadeMonth(month, timeNum),
                            out value))
                            cell.Value = value;
                    }
                }
            }
        }
        void ShowRows(ToolStripMenuItem tsmenu)
        {
            bool visible;
            if (tsmenu.DisplayStyle == ToolStripItemDisplayStyle.ImageAndText)
            {
                tsmenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
                visible = false;
            }
            else
            {
                tsmenu.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                visible = true;
            }

            EnumTime et = (EnumTime)tsmenu.Tag;
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (GetRowTime(row) == et)
                    row.Visible = visible;
            }
        }
        private void mnuYearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowRows(mnuYearToolStripMenuItem);
        }
        EnumTime GetRowTime(DataGridViewRow row)
        {
            return (EnumTime)((object[])row.Cells[_iColTime].Tag)[0];
        }

        private void mnuMonthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowRows(mnuMonthToolStripMenuItem);
        }

        private void mnuDecadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowRows(mnuDecadeToolStripMenuItem);
        }

        private void mnuPentadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowRows(mnuPentadeToolStripMenuItem);
        }
    }
}
