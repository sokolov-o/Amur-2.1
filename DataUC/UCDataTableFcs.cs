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
    public partial class UCDataTableFcs : UserControl
    {
        public Sys.AttrValueCollection UserSettings { get; set; }
        public Common.User User { get; set; }

        public FormDataFilter FormDataFilter { get; set; }

        public enum View
        {
            Date_Variable_StationDate = 0,
            Date_Station_VariableDate = 1
        }
        public UCDataTableFcs()
        {
            _isFilled = true;

            InitializeComponent();

            TableView = UCDataTableFcs.View.Date_Variable_StationDate;

            _isFilled = false;
        }

        UCDataTableFcs.View TableView
        {
            get
            {
                return (UCDataTableFcs.View)tableViewToolStripComboBox.SelectedIndex;
            }
            set
            {
                tableViewToolStripComboBox.SelectedIndex = (int)value;
            }
        }

        Dictionary<int, List<DateTime>> _fcsCD;
        List<Catalog> _ctls;
        List<Site> _sites;
        List<Variable> _vars;
        List<DateTime> _dates;

        DataFilter DataFilter { get; set; }

        bool _isFilled = true;
        public void Fill(DataFilter df)
        {
            if (df == null) return;

            _isFilled = true;

            Clear();
            DataFilter = df;

            _fcsCD = Data.DataManager.GetInstance().DataForecastRepository.SelectGroupByCD((DateTime)df.DateTimePeriod.DateS, (DateTime)df.DateTimePeriod.DateF, IsDateFcs);

            _ctls = Meta.DataManager.GetInstance().CatalogRepository.Select(_fcsCD.Keys.ToList());
            _ctls = Catalog.FindAll(_ctls, df.CatalogFilter);

            _sites = Meta.DataManager.GetInstance().SiteRepository.Select(_ctls.Select(x => x.SiteId).Distinct().ToList());
            _vars = Meta.DataManager.GetInstance().VariableRepository.Select(_ctls.Select(x => x.VariableId).Distinct().ToList()).OrderBy(x => x.NameRus).ToList();

            _dates = new List<DateTime>();
            foreach (var item in _fcsCD)
            {
                foreach (var date in item.Value)
                {
                    if (!_dates.Exists(x => x == date))
                        _dates.Add(date);
                }
            }
            dateToolStripComboBox.Items.Clear();
            foreach (var date in _dates.OrderByDescending(x => x))
            {
                dateToolStripComboBox.Items.Add(date);
            }
            dateToolStripComboBox.SelectedIndex = dateToolStripComboBox.Items.Count > 0 ? 0 : -1;

            FillTreeView();

            _isFilled = false;
        }
        void FillTreeView()
        {
            tv.Nodes.Clear();

            switch (TableView)
            {
                case View.Date_Variable_StationDate:

                    foreach (var var in _vars.OrderBy(x => x.NameRus))
                    {
                        TreeNode tnV = new TreeNode(var.NameRus);
                        tnV.Tag = var;

                        tv.Nodes.Add(tnV);
                    }

                    break;
                case View.Date_Station_VariableDate:

                    foreach (var site in _sites.OrderBy(x => x.Name))
                    {
                        TreeNode tnV = new TreeNode(Meta.Site.GetName(site, 2, SiteTypeRepository.GetCash()));
                        tnV.Tag = site;

                        tv.Nodes.Add(tnV);
                    }

                    break;
                default:
                    throw new Exception("Неизвестный тип представления таблицы данных - " + TableView);
            }
        }
        public bool IsDateFcs
        {
            get
            {
                return dateUTCorLOCToolStripComboBox.SelectedIndex == 1;
            }
            set
            {
                dateUTCorLOCToolStripComboBox.SelectedIndex = value ? 1 : 0;
            }
        }
        static string DATE_FORMAT = "dd.MM.yyyy HH";

        private void isDateFcsToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_isFilled)
            {
                Fill(DataFilter);
            }
        }
        void Clear()
        {
            dgv.Rows.Clear();
            dgv.Columns.Clear();

            tv.Nodes.Clear();

            dateToolStripComboBox.Items.Clear();
            dateToolStripComboBox.Text = string.Empty;

            _fcsCD = null;
            _ctls = null;
            _sites = null;
            _vars = null;
            _dates = null;
        }
        public Sys.EntityInstanceValue DataFilterSAV { get; set; }

        private void filterToolStripButton_Click(object sender, EventArgs e)
        {
            if (FormDataFilter == null)
                FormDataFilter = new FormDataFilter(DataFilterSAV);

            if (FormDataFilter.ShowDialog() == DialogResult.OK)
            {
                Fill(FormDataFilter.DataFilter);
            }
        }

        private void tv_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            FillDataTable();
        }

        private void FillDataTable()
        {
            if (!Date.HasValue) return;
            DateTime date = (DateTime)Date;

            dgv.Rows.Clear();
            dgv.Columns.Clear();

            if (tv.SelectedNode == null) return;

            textBox1.Text = tv.SelectedNode.Text;

            switch (TableView)
            {
                case View.Date_Variable_StationDate:

                    List<Catalog> ctls = _ctls.FindAll(x => x.VariableId == ((Variable)tv.SelectedNode.Tag).Id);
                    List<DataForecast> data = Data.DataManager.GetInstance().
                        DataForecastRepository.SelectDataForecasts(ctls.Select(x => x.Id).ToList(), date, date, null, IsDateFcs);
                    List<DateTime> dates = data.Select(x => IsDateFcs ? x.DateIni : x.DateFcs).Distinct().OrderBy(x => x).ToList();

                    dgv.Columns.Add("siteName", "Пункт");
                    dgv.Columns[0].DefaultCellStyle.BackColor = Color.FromKnownColor(KnownColor.Info);
                    int iColData = 1;

                    foreach (var date1 in dates)
                    {
                        int i = dgv.Columns.Add(date1.ToString(DATE_FORMAT), date1.ToString(DATE_FORMAT));
                        dgv.Columns[i].Tag = date1;
                    }

                    foreach (var site in _sites)
                    {
                        DataGridViewRow row = dgv.Rows[dgv.Rows.Add()];
                        row.Cells[0].Value = Meta.Site.GetName(site, 0, SiteTypeRepository.GetCash());
                        row.Cells[0].Tag = site;

                        List<DataForecast> data1 = data.FindAll(x => ctls.Exists(y => y.SiteId == site.Id && y.Id == x.CatalogId));
                        bool isDateFcs = IsDateFcs;

                        for (int iCol = iColData; iCol < dgv.Columns.Count; iCol++)
                        {
                            List<DataForecast> dataCell = data1.FindAll(x =>
                                (isDateFcs ? x.DateIni : x.DateFcs) == (DateTime)dgv.Columns[iCol].Tag);
                            CellDataForecast.SetValues(row.Cells[iCol], dataCell);
                        }
                    }
                    dgv.Columns[0].Frozen = true;
                    dgv.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    break;
                case View.Date_Station_VariableDate:

                    ctls = _ctls.FindAll(x => x.SiteId == ((Site)tv.SelectedNode.Tag).Id);
                    data = Data.DataManager.GetInstance().
                        DataForecastRepository.SelectDataForecasts(ctls.Select(x => x.Id).ToList(), date, date, null, IsDateFcs);
                    dates = data.Select(x => IsDateFcs ? x.DateIni : x.DateFcs).Distinct().OrderBy(x => x).ToList();

                    dgv.Columns.Add("varName", "Переменная");
                    dgv.Columns[0].DefaultCellStyle.BackColor = Color.Aqua;
                    iColData = 1;

                    foreach (var date1 in dates)
                    {
                        int i = dgv.Columns.Add(date1.ToString(DATE_FORMAT), date1.ToString(DATE_FORMAT));
                        dgv.Columns[i].Tag = date1;
                    }

                    foreach (var var in _vars)
                    {
                        DataGridViewRow row = dgv.Rows[dgv.Rows.Add()];
                        row.Cells[0].Value = var.NameRus;
                        row.Cells[0].Tag = var;

                        List<DataForecast> data1 = data.FindAll(x => ctls.Exists(y => y.VariableId == var.Id && y.Id == x.CatalogId));
                        bool isDateFcs = IsDateFcs;

                        for (int iCol = iColData; iCol < dgv.Columns.Count; iCol++)
                        {
                            List<DataForecast> dataCell = data1.FindAll(x =>
                                (isDateFcs ? x.DateIni : x.DateFcs) == (DateTime)dgv.Columns[iCol].Tag);
                            CellDataForecast.SetValues(row.Cells[iCol], dataCell);
                        }
                    }
                    dgv.Columns[0].Frozen = true;
                    dgv.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    break;
                default:
                    throw new Exception("Неизвестный тип представления таблицы данных - " + TableView);
            }
        }
        DateTime? Date
        {
            get
            {
                return !string.IsNullOrEmpty(dateToolStripComboBox.Text) ? (DateTime?)DateTime.Parse(dateToolStripComboBox.Text) : null;
            }
        }

        private void tableViewToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isFilled) return;

            dgv.Rows.Clear();
            dgv.Columns.Clear();
            textBox1.Text = string.Empty;

            FillTreeView();
        }

        private void dateToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_isFilled)
            {
                FillDataTable();
            }
        }

        private void tv_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (!_isFilled)
            {
                FillDataTable();
            }
        }
    }
}
