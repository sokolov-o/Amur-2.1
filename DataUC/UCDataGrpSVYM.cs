using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SOV.Amur.DataP;
using SOV.Amur.Meta;
using System.Windows.Forms.DataVisualization.Charting;

namespace SOV.Amur.Data
{
    /// <summary>
    /// Отображение данных, сгрупированных по пункту, переменной, году и месяцу
    /// </summary>
    public partial class UCDataGrpSVYM : UserControl
    {
        public enum ViewType
        {
            ONE_SiteVariable_ALL_YearMonth = 1
        }

        Filter ThisFilter
        {
            get
            {
                return new Filter()
                {
                    Sites = (List<Site>)sitesCB.DataSource,
                    Variables = (List<Variable>)variablesCB.DataSource,
                    YearSUTC = int.Parse(yearSTB.Text),
                    YearFUTC = int.Parse(yearFTB.Text),
                    Monthes = Common.StrVia.ToListInt(monthesTB.Text)
                };
            }
            set
            {
                sitesCB.DataSource = value.Sites;
                variablesCB.DataSource = value.Variables;
                yearSTB.Text = value.YearSUTC.ToString();
                yearFTB.Text = value.YearFUTC.ToString();
                monthesTB.Text = Common.StrVia.ToString(value.Monthes);

                if (sitesCB.Items != null && sitesCB.Items.Count > 0) sitesCB.SelectedIndex = 0;
                if (variablesCB.Items != null && variablesCB.Items.Count > 0) variablesCB.SelectedIndex = 0;
            }
        }
        Variable CurVariable
        {
            get
            {
                return variablesCB.SelectedItem == null ? null : (Variable)variablesCB.SelectedItem;
            }
        }
        Site CurSite
        {
            get
            {
                return sitesCB.SelectedItem == null ? null : (Site)sitesCB.SelectedItem;
            }
        }
        public UCDataGrpSVYM()
        {
            InitializeComponent();
        }
        public void Fill(List<Site> sites, List<Variable> variables, int yearSUTC, int yearFUTC, List<int> monthes)
        {
            ThisFilter = new Filter() { Sites = sites, Variables = variables, YearSUTC = yearSUTC, YearFUTC = yearFUTC, Monthes = monthes };
            Fill();
        }
        List<DataGrpSVYM> _datas = null;

        UCDataGrpSVYM.ViewType CurViewType
        {
            get
            {
                return (UCDataGrpSVYM.ViewType)Enum.Parse(typeof(UCDataGrpSVYM.ViewType), viewTypeCB.SelectedItem.ToString());
            }
        }
        List<VariableAttributes> _variableAttrs;
        /// <summary>
        /// - CLEAR TABLE
        /// - READ DATA
        /// - FILL OPTIONS SITE & VARIABLES
        /// - FILL TABLE
        /// - DRAW CHART
        /// </summary>
        void Fill()
        {
            // CLEAR TABLE
            dgv.Rows.Clear();
            dgv.Columns.Clear();
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            // READ DATA
            if (viewTypeCB.SelectedItem != null)
            {
                if (_datas == null)
                {
                    Filter filter = ThisFilter;
                    _datas = DataP.DataManager.GetInstance().DataValueProcessRepository.SelectDataGrpSVYM(
                        filter.Sites == null ? null : filter.Sites.Select(x => x.Id).ToList(),
                        filter.Variables == null ? null : filter.Variables.Select(x => x.Id).ToList(),
                        filter.YearSUTC, filter.YearFUTC, filter.Monthes);

                    if (filter.Sites == null)
                    {
                        List<Site> sites = Meta.DataManager.GetInstance().SiteRepository.Select(_datas.Select(x => x.SiteId).ToList());
                        sitesCB.DataSource = Meta.Site.ToDicItemList(sites, 1, SiteTypeRepository.GetCash());
                    }
                    if (filter.Variables == null)
                    {
                        List<Variable> vars = Meta.DataManager.GetInstance().VariableRepository.Select(_datas.Select(x => x.VariableId).ToList()).OrderBy(x => x.NameRus).ToList();
                        variablesCB.DataSource = vars;
                        _variableAttrs = Meta.DataManager.GetInstance().VariableAttributesRepository
                            .Select(vars.Select(x => x.Id).ToList());
                    }
                }

                switch (CurViewType)
                {
                    case UCDataGrpSVYM.ViewType.ONE_SiteVariable_ALL_YearMonth:
                        if (CurSite == null || CurVariable == null)
                        {
                            MessageBox.Show("Выберите пункт и переменную.");
                            return;
                        }
                        Filter filter = ThisFilter;
                        // CREATE COLUMNS
                        dgv.Columns.Add("year", "Год");
                        dgv.Columns[0].DefaultCellStyle.BackColor = Color.LightYellow;
                        foreach (var item in filter.Monthes)
                        {
                            dgv.Columns.Add("month" + item, item.ToString());
                        }
                        // CREATE ROWS
                        for (int i = filter.YearSUTC; i <= filter.YearFUTC; i++)
                        {
                            DataGridViewRow row = dgv.Rows[dgv.Rows.Add()];
                            row.Cells[0].Value = i;
                            for (int j = 0; j < filter.Monthes.Count; j++)
                            {
                                DataGrpSVYM data = _datas.FirstOrDefault(x =>
                                    x.SiteId == CurSite.Id
                                    && x.VariableId == CurVariable.Id
                                    && x.Year == i
                                    && x.Month == j + 1
                                    );
                                if (data != null)
                                {
                                    row.Cells[j + 1].Value = GetDataValueByCellValueType(data); ;
                                    row.Cells[j + 1].ToolTipText = data.ToString();
                                    row.Cells[j + 1].Tag = data;
                                }
                            }
                        }
                        // DRAW CHART
                        DrawChart();
                        // END
                        infoLabel.Text = CurSite.GetName(1, SiteTypeRepository.GetCash()) +
                            ", " + CurVariable.NameRus + " (" + (string)cellValueTypeCB.SelectedItem +
                            ")";
                        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                        break;
                    default:
                        throw new Exception("Неизвестный тип формы представления таблицы данных - " + viewTypeCB.SelectedItem.ToString());
                }
            }
        }
        double GetDataValueByCellValueType(DataGrpSVYM data)
        {
            double ret = double.NaN;
            VariableAttributes va = CurVariable == null ? null : _variableAttrs.Find(x => x.VariableId == CurVariable.Id);
            string format = va == null ? null : va.ValueFormat;

            switch ((string)cellValueTypeCB.SelectedItem)
            {
                case "Количество": ret = data.Count; break;
                case "Среднее": ret = double.Parse(format == null ? data.AvgValue.ToString() : string.Format(format, data.AvgValue)); break;
                case "Сумма": ret = data.SumValue; break;
                case "Минимум": ret = data.MinValue; break;
                case "Максимум": ret = data.MaxValue; break;
            }
            return ret;
        }
        private void DrawChart()
        {
            chart.Series.Clear();
            chart.ChartAreas.RemoveAt(0);
            if (!showGrapgCHK.Checked) return;

            chart.ChartAreas.Add("1");
            switch (CurViewType)
            {
                case ViewType.ONE_SiteVariable_ALL_YearMonth:

                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        List<DataPoint> dps = new List<DataPoint>();
                        string seriaName = "unknown";
                        Series seria = new Series();

                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            DataPoint dp = new DataPoint();
                            if (cell.OwningColumn.Index == 0)
                            {
                                seriaName = cell.Value.ToString();
                            }
                            else
                            {
                                dp.XValue = int.Parse(cell.OwningColumn.HeaderText);

                                if (cell.Tag != null)
                                {
                                    dp.YValues[0] = GetDataValueByCellValueType(((DataGrpSVYM)cell.Tag));
                                    dp.Label = dp.YValues[0].ToString();
                                }
                                else
                                {
                                    dp.YValues[0] = double.NaN;
                                }
                            }
                            chart.ChartAreas[0].AxisY.Title = (string)cellValueTypeCB.SelectedItem;
                            dp.ToolTip = seriaName + " " + dp.XValue + " " + dp.YValues[0];
                            dp.LabelBorderColor = Color.Black;

                            seria.Points.Add(dp);
                        }
                        seria.Name = seriaName;
                        chart.Series.Add(seria);
                    }
                    break;
            }
        }

        private void UCDataGrpSVYM_Load(object sender, EventArgs e)
        {
            viewTypeCB.Items.AddRange(Enum.GetNames(typeof(UCDataGrpSVYM.ViewType)));
            viewTypeCB.SelectedIndex = 0;

            splitContainer1.Panel2Collapsed = true;
            cellValueTypeCB.SelectedIndex = 0;
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            Fill();
        }

        public class Filter
        {
            public Filter() { }

            public List<Meta.Site> Sites;
            public List<Meta.Variable> Variables;
            public int YearSUTC;
            public int YearFUTC;
            public List<int> _monthes;
            public List<int> Monthes
            {
                get
                {
                    return _monthes;
                }
                set
                {
                    _monthes = value != null && value.Count != 0 ? value : new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
                }
            }
        }

        private void optionsButton_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel2Collapsed = !splitContainer1.Panel2Collapsed;
        }

        private void acceptOptionsButton_Click(object sender, EventArgs e)
        {
            _datas = null;
            refreshButton_Click(null, null);
        }

        private void showGrapgCHK_CheckedChanged(object sender, EventArgs e)
        {
            splitContainer2.Panel2Collapsed = !showGrapgCHK.Checked;
        }
    }
}
