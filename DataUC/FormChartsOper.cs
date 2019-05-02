using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FERHRI.Amur.Meta;
using FERHRI.Amur.Data;
using FERHRI.Common;

namespace FERHRI.Amur.Data
{
    public partial class FormChartsOper : Form
    {
        private Meta.ChartsFilter _chartsFilter;
        public Meta.ChartsFilter chartsFilter
        {
            get
            {
                return _chartsFilter;
            }
            set
            {
                DateTimePeriod = value.dateTimePeriod;
                _chartsFilter = value;
            }
        }
        private int userOrganisationId;

        void Clear()
        {
            tlp.Controls.Clear();
            tlp.RowCount = 0;
            tlp.ColumnCount = 0;

            for (int i = 0; i < tlp.RowStyles.Count; i++)
                tlp.RowStyles.RemoveAt(i);
            for (int i = 0; i < tlp.ColumnStyles.Count; i++)
                tlp.ColumnStyles.RemoveAt(i);
        }
        public int SiteGroupId
        {
            get
            {
                return siteGroupsCB.SelectedIndex < 0 ? -1 : ((SiteGroup)siteGroupsCB.SelectedItem).Id;
            }
            set
            {
                siteGroupsCB.SelectedIndex = -1;
                foreach (var item in siteGroupsCB.Items)
                {
                    SiteGroup sg = (SiteGroup)item;
                    if (sg.Id == value)
                    {
                        siteGroupsCB.SelectedItem = sg;
                        break;
                    }
                }
            }
        }

        private void CreateUCInCells()
        {
            Clear();

            if (siteGroupsCB.SelectedIndex >= 0)
            {
                tlp.RowCount = 1;
                tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
                tlp.ColumnCount = ColumnCount;
                for (int i = 0; i < tlp.ColumnCount; i++)
                {
                    tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                }
                tlp.ColumnStyles[0].SizeType = SizeType.Percent;

                Meta.DataManager mrep = Meta.DataManager.GetInstance();
                Data.DataManager drep = Data.DataManager.GetInstance();
                SiteGroup sg = mrep.SiteGroupRepository.SelectGroupFK(SiteGroupId);
                List<Station> stations = mrep.StationRepository.Select(sg.SiteList.Select(x => x.StationId).Distinct().ToList());

                // ЦИКЛ по всем пунктам группы
                for (int i = 0, iRow = 0, iCol = 0; i < sg.SiteList.Count; i++, iCol++)
                {
                    Site site = sg.SiteList[i];

                    // CREATE CHART OPTIONS
                    // SITES
                    List<Site> sites = mrep.SiteRepository.SelectRelated(site.StationId);
                    Site AHK_Or_Hydropost = sites.Find(
                        x => (new List<int> {(int)EnumStationType.HydroPost, (int)EnumStationType.AHK})
                            .Contains(x.SiteTypeId)
                    );
                    if (AHK_Or_Hydropost == null)
                    {
                        iCol--;
                        continue;
                    }
                    // VARIABLES
                    List<int> varIds = new List<int> {(int)EnumVariable.GageHeightF, (int)EnumVariable.PrecipDay24F};
                    List<Variable> vars = mrep.VariableRepository.Select(varIds);

                    // CATALOGS

                    DataFilter dataFilter = new DataFilter()
                    {
                        DateTimePeriod = this.DateTimePeriod,
                        CatalogFilter = new CatalogFilter()
                        {
                            Sites = sites.Select(x => x.Id).Distinct().ToList(),
                            Variables = varIds,
                            Methods = null,
                            Sources = null,
                            OffsetTypes = new List<int> { (int)EnumOffsetType.NoOffset },
                            OffsetValue = 0
                        },
                        FlagAQC = null,
                        IsActualValueOnly = true,
                        IsRefSiteData = false,
                        IsSelectDeleted = false,
                        IsDateLOC = this.TimeType == EnumTimeType.LOC
                    };

                    UCChartHydro.ChartOptions chartOptions = new UCChartHydro.ChartOptions()
                    {
                        Station = Meta.DataManager.GetInstance().StationRepository.Select(site.StationId),
                        Sites = sites,
                        Vars = vars,
                        DataFilter = dataFilter,
                        TimeType = this.TimeType,
                        AxesMaxPrecipitation = StrVia.ParseDouble(axesMaxPrecipitationTextBox.Text),
                        AxesMinGageHeight = StrVia.ParseDouble(axesMinGageHeightTextBox.Text),

                        gageHeightLimits = new List<Climate>[3] //Список значений пределов уровня воды {пойма, ня, оя}
                    };
                    
                    // Массив id типов данных {пойма, ня, оя}
                    int[] typesIds = new int[] { (int)EnumDataType.Poyma, (int)EnumDataType.NYa, (int)EnumDataType.OYa };
                    for (int k = 0; k < typesIds.Length; ++k)
                        chartOptions.gageHeightLimits[k] = drep.ClimateRepository.SelectClimateNearestMetaAndData(
                            DateTime.Now.Year, 2,
                            new List<int> { AHK_Or_Hydropost.Id },
                            new List<int> { (int)EnumVariable.GageHeightF }, 
                            (int)EnumOffsetType.NoOffset, 0, 
                            typesIds[k], (int)EnumTime.YearCommon
                        );

                    // CREATE UC & ADD 2 CELL

                    UCChartHydro uc = new UCChartHydro(userOrganisationId, chartOptions);
                    uc.ToolbarVisible = false;
                    uc.EnableAxesTitle = false;
                    uc.EnableLegendRevert();
                    uc.title = sg.SiteList[i].GetName(DicCash.Stations, DicCash.StationTypes, 2);
                    uc.Dock = DockStyle.Fill;

                    if (iCol == tlp.ColumnCount)
                    {
                        tlp.RowCount = tlp.RowCount + 1;
                        tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
                        iCol = 0;
                        iRow++;
                    }
                    tlp.Controls.Add(uc, iCol, iRow);
                }
                if (tlp.Controls.Count == 0)
                    MessageBox.Show("Нет данных");
            }
        }

        public int ColumnCount
        {
            get
            {
                return int.Parse(columnCountTextBox.Text);
            }
            set
            {
                columnCountTextBox.Text = value < 1 ? "1" : value.ToString();
                CreateUCInCells();
            }
        }

        static System.Threading.Timer _timer;
        static bool _isBusy = false;
        private Cursor cs;

        public FormChartsOper(int _userOrganisationId, int siteGroupId = -1)
        {
            InitializeComponent();
            timeTypeComboBox.Items.Add(EnumTimeType.LOC);
            timeTypeComboBox.Items.Add(EnumTimeType.UTC);

            cs = this.Cursor;

            SiteGroupId = siteGroupId;
            ColumnCount = ColumnCount;
            _chartsFilter = new Meta.ChartsFilter();
            DateTimePeriod = new Common.DateTimePeriod();
            userOrganisationId = _userOrganisationId;
        }

        void SetTimer()
        {
            if (_timer != null) _timer.Dispose();
            _timer = new System.Threading.Timer(AsyncFill, null, 0, (int)(double.Parse(refreshPeriodMinutesTextBox.Text) * 60 * 1000));
        }

        private void FormChartsOper_Load(object sender, EventArgs e)
        {
            siteGroupsCB.Items.Clear();
            siteGroupsCB.Items.AddRange(Meta.DataManager.GetInstance().SiteGroupRepository.SelectGroups().ToArray());
            timeTypeComboBox.SelectedIndex = 0;
            splitContainer1.Panel2Collapsed = true;
            SetTimer();
        }

        EnumTimeType TimeType
        {
            get
            {
                return (EnumTimeType)timeTypeComboBox.SelectedItem;
            }
        }

        private void siteGroupsCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            CreateUCInCells();
            AsyncFill();
            this.Cursor = cs;
            ActiveControl = toolStrip1;
        }

        private void FillChart(UCChartHydro uc)
        {
            uc.UpdateData();
            System.Diagnostics.Trace.WriteLine(string.Format("{0}", uc.title));
        }

        private void AsyncFill(Object stateInfo = null)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            try
            {
                BackgroundWorker worker = new BackgroundWorker();
                foreach (UCChartHydro uc in tlp.Controls)
                    worker.DoWork += (obj, e) => FillChart(uc);
                worker.RunWorkerAsync();
            }
            finally
            {
                sw.Stop();
                System.Diagnostics.Trace.WriteLine(string.Format("### Time: {0} ###", sw.Elapsed));
            }
        }

        private void refreshToolStripButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                CreateUCInCells();
                SetTimer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                this.Cursor = cs;
            }
        }
        Common.DateTimePeriod _DateTimePeriod;
        public Common.DateTimePeriod DateTimePeriod
        {
            get { return _DateTimePeriod; }
            private set
            {
                _DateTimePeriod = value;
                labelDateTimePeriod.Text = value == null ? null : value.ToStringRus();
                chartsFilter.dateTimePeriod = value;
            }
        }

        private void dateTimePeriodButton_Click(object sender, EventArgs e)
        {
            Common.FormDateTimePeriod frm = new Common.FormDateTimePeriod();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.DateTimePeriod = this.DateTimePeriod;
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                DateTimePeriod = frm.DateTimePeriod;
            }
        }

        private void optionsToolStripButton_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel2Collapsed = !splitContainer1.Panel2Collapsed;
        }

        private void FormChartsOper_FormClosing(object sender, FormClosingEventArgs e)
        {
            _timer.Dispose();
        }
    }
}
