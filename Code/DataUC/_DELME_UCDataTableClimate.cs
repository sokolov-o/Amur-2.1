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
    public partial class _DELME_UCDataTableClimate : UserControl
    {
        static public List<string> ViewType = new List<string>() {
            "Одна переменная",
            "Одна станция/пункт",
            "Один временной интервал"
        };
        public _DELME_UCDataTableClimate()
        {
            InitializeComponent();

            tableViewCB.DataSource = ViewType;
        }

        List<Site> _sites;
        List<Climate> _data;
        List<Variable> _variables;
        List<DataType> _datatypes;
        List<Unit> _times;

        public void Fill(List<int> sitesId, int[] yearSF)
        {
            _sites = Meta.DataManager.GetInstance().SiteRepository.Select(sitesId).OrderBy(x => x.GetName(2, SiteTypeRepository.GetCash())).ToList();
            _data = Data.DataManager.GetInstance().ClimateRepository.SelectClimateMetaAndData(sitesId, null, null, null, null, null,
                yearSF[0], yearSF[1]);
            _variables = Meta.DataManager.GetInstance().VariableRepository.Select(_data.Select(x => x.VariableId).Distinct().ToList()).OrderBy(x => x.NameRus).ToList();
            _datatypes = Meta.DataManager.GetInstance().DataTypeRepository.Select(_data.Select(x => x.DataTypeId).Distinct().ToList()).OrderBy(x => x.Name).ToList();
            _times = Meta.DataManager.GetInstance().UnitRepository.Select(_data.Select(x => x.TimeId).Distinct().ToList()).OrderBy(x => x.Name).ToList();
            // TODO: Реализовать считывание мес. начала гидросезонов
            _times.Remove(_times.FirstOrDefault(x => x.Id == (int)EnumTime.HydroSeason));

            timePeriodCB.Items.AddRange(_times.ToArray());
            timePeriodCB.SelectedIndex = 0;

            Fill();
        }
        void Fill()
        {
            dgv.Rows.Clear();
            dgv.Columns.Clear();
            viewFixCB.Items.Clear();

            switch (tableViewCB.SelectedIndex)
            {
                #region Отдельная переменная
                case 0:
                    viewFixLabel.Text = "Переменная";
                    viewFixCB.Items.AddRange(_variables.ToArray());

                    DataGridViewColumn col = dgv.Columns[dgv.Columns.Add("Пункт", "Пункт")];
                    col.DefaultCellStyle.BackColor = Color.AliceBlue;
                    col = dgv.Columns[dgv.Columns.Add("Тип", "Тип")];
                    col.DefaultCellStyle.BackColor = Color.LightSkyBlue;

                    for (int i = 0; i < Time.GetTimeNumMax(CurTimePeriodId); i++)
                    {
                        col = dgv.Columns[dgv.Columns.Add((i + 1).ToString(), (i + 1).ToString())];
                        col.Tag = i + 1;
                    }

                    foreach (var site in _sites)
                    {
                        DataGridViewRow row = dgv.Rows[dgv.Rows.Add()];
                        row.Cells[0].Value = site.GetName(2, SiteTypeRepository.GetCash());

                        foreach (var dataType in _datatypes)
                        {
                            row = dgv.Rows[dgv.Rows.Add()];
                            row.Cells[1].Value = dataType.Name;
                            row.Tag = new object[] { site, dataType };

                            for (int i = 2; i < dgv.Columns.Count; i++)
                            {
                                //TODO: Continue here OSokolov@20161230
                                //row.Cells[i].Value = GetDataValue(
                                //    site.Id, 
                                //    ((Variable)viewFixCB.SelectedItem).Id,

                                //     );
                            }
                        }
                    }
                    break;
                #endregion

                #region Климат отдельного пункта
                case 1:
                    break;
                #endregion

                #region Климат отдельного временного интервала
                case 2:
                    break;
                #endregion

                default: throw new Exception("Неизвестный вид формы представления данных " + tableViewCB.SelectedIndex + ".");
            }
        }

        private double GetDataValue(int SiteId, int VariableId, int OffsetTypeId, double OffsetValue, int DataTypeId, int TimeId, short TimeNum)
        {
            List<Climate> ret = _data.FindAll(x =>
                x.SiteId == SiteId
                && x.VariableId == VariableId
                && x.OffsetTypeId == OffsetTypeId
                && x.OffsetValue == OffsetValue
                && x.DataTypeId == DataTypeId
                && x.TimeId == TimeId
            );
            if (ret == null) return double.NaN;
            if (ret.Count > 1) throw new Exception("(ret.Count > 1)"); ;

            return ret[0].Data[TimeNum];
        }
        int CurTimePeriodId
        {
            get
            {
                return ((Unit)timePeriodCB.SelectedItem).Id;
            }
        }
        private void viewFixCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fill();
        }

        private void timePeriodCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fill();
        }

        private void tableViewCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fill();
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel1Collapsed = !splitContainer1.Panel1Collapsed;
        }

        private void UCDataTableClimate_Load(object sender, EventArgs e)
        {
            tableViewCB.Items.AddRange(Enum.GetNames(typeof(View)).ToArray<object>());
        }
        //public void Fill1(int siteId, int[] yearSF)
        //{
        //    List<Climate> clm = Data.DataManager.GetInstance().ClimateRepository.SelectClimateMetaAndData(
        //        new List<int>(new int[] { siteId }), null, null, null, null, null, yearSF[0], yearSF[1]);
        //    List<Variable> clmVariables = Meta.DataManager.GetInstance().VariableRepository.Select(clm.Select(x => x.VariableId).Distinct().ToList());

        //    int iColData = dgv.Columns.Count;

        //    #region CREATE COLUMNS
        //    List<Variable> clmVars = Meta.DataManager.GetInstance().VariableRepository.Select(clm.Select(x => x.VariableId).Distinct().ToList());

        //    foreach (Variable clmVar in clmVars)
        //    {
        //        foreach (int clmDataTypeId in clm.Where(x => x.VariableId == clmVar.Id).Select(x => x.DataTypeId).Distinct())
        //        {
        //            dgv.Columns
        //            [
        //                dgv.Columns.Add
        //                (
        //                    clmVar.Id.ToString(),
        //                    MetaDataTypeRepository.GetCash().Find(x => x.Id == clmDataTypeId).NameShort + " - " + clmVar.Name
        //                )
        //            ].Tag = new object[] { clmVar, clmDataTypeId };

        //        }
        //    }
        //    #endregion CREATE COLUMNS

        //    #region CREATE ROWS
        //    DataGridViewRow row = dgv.Rows[dgv.Rows.Add()];
        //    row.Cells[_iColTime].Value = "Год";
        //    row.Cells[_iColTime].Tag = new object[] { EnumTime.YearCommon, 1 };
        //    row.DefaultCellStyle.BackColor = Color.AliceBlue;

        //    for (int i = 1; i <= 12; i++)
        //    {
        //        row = dgv.Rows[dgv.Rows.Add()];

        //        row.Cells[_iColTime].Value = "Месяц " + i;
        //        row.Cells[_iColTime].Tag = new object[] { EnumTime.Month, i };
        //        row.DefaultCellStyle.BackColor = Color.AliceBlue;

        //        for (int j = 1; j <= 3; j++)
        //        {
        //            row = dgv.Rows[dgv.Rows.Add()];
        //            row.Cells[_iColTime].Value = "  Декада " + j;
        //            row.Cells[_iColTime].Tag = new object[] { EnumTime.DecadeOfYear, i, j };
        //        }
        //        for (int j = 1; j <= 6; j++)
        //        {
        //            row = dgv.Rows[dgv.Rows.Add()];
        //            row.Cells[_iColTime].Value = "    Пентада " + j;
        //            row.Cells[_iColTime].Tag = new object[] { EnumTime.PentadeOfYear, i, j };
        //        }
        //    }
        //    #endregion CREATE ROWS

        //    // FILL DATA CELLS
        //    foreach (DataGridViewRow row_ in dgv.Rows)
        //    {
        //        object[] tag = (object[])row_.Cells[_iColTime].Tag;

        //        EnumTime et = (EnumTime)tag[0];
        //        int timeNum, month;
        //        if (et == EnumTime.DecadeOfYear || et == EnumTime.PentadeOfYear)
        //        {
        //            timeNum = (int)tag[2];
        //            month = (int)tag[1];
        //        }
        //        else
        //        {
        //            timeNum = (int)tag[1];
        //            month = -1;
        //        }

        //        for (int i = iColData; i < row_.Cells.Count; i++)
        //        {
        //            DataGridViewCell cell = row_.Cells[i];

        //            Variable var = (Variable)((object[])cell.OwningColumn.Tag)[0];
        //            if (var.TimeId != (int)et) continue;
        //            EnumDataType clmDataType = (EnumDataType)((object[])cell.OwningColumn.Tag)[1];

        //            //List<Variable> vars = clmVariables.FindAll(x =>
        //            //    x.VariableTypeId == (int)varType
        //            //    && x.DataTypeId == (int)varDataType
        //            //    && x.TimeId == (int)et
        //            //    );
        //            //Variable var = null;
        //            //if (vars.Count() > 0)
        //            //{
        //            //    if (vars.Count() > 1)
        //            //        throw new Exception("(vars.Count() > 1)");
        //            //    var = vars[0];
        //            //}
        //            //if (var == null) continue;

        //            List<Climate> clmm = clm.FindAll(x => x.VariableId == var.Id && x.DataTypeId == (int)clmDataType);
        //            if (clmm.Count() > 0)
        //            {
        //                if (clmm.Count() > 1)
        //                    throw new Exception("(clmm.Count() > 1)");
        //                double value;
        //                if (clmm[0].Data.TryGetValue((month == -1) ? (short)timeNum : (short)Common.DateTimeProcess.GetDecadeYearByDecadeMonth(month, timeNum), out value))
        //                    cell.Value = value;
        //            }
        //        }
        //    }
        //}
    }
}
