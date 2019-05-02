using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SOV.Amur.Meta;
using SOV.Amur.Data;
using SOV.Common;

namespace SOV.Amur.Data
{
    public partial class UCChartsOper : UserControl
    {
        private Meta.ChartsFilter _chartsFilter;
        public Meta.ChartsFilter chartsFilter
        {
            get
            {
                return new ChartsFilter(DateTimePeriod, SiteGroupId);
            }
            set
            {
                _chartsFilter = value;
                DateTimePeriod = _chartsFilter.DateTimePeriod;
                SiteGroupId = _chartsFilter.SiteGroup.HasValue ? _chartsFilter.SiteGroup.Value : -1;
                if (SiteGroupId != -1)
                {
                    List<int[]> idOrderBy = Meta.DataManager.GetInstance().EntityGroupRepository.SelectEntities(SiteGroupId);
                    List<Site> sitesInGroup = Meta.DataManager.GetInstance().SiteRepository.Select(idOrderBy.Select(x => x[0]).ToList());
                    SitesGroupList = sitesInGroup.OrderBy(x => idOrderBy.First(y => y[0] == x.Id)[1]).ToList();
                }
            }
        }
        private int userOrganisationId;

        void Clear()
        {
            tlp.Controls.Clear();
            tlp.RowCount = tlp.ColumnCount = 0;
            tlp.RowStyles.Clear();
            tlp.ColumnStyles.Clear();
        }

        public List<Site> SitesGroupList = new List<Site>();

        public int SiteGroupId
        {
            get { return siteGroupsCB.SiteGroup == null ? -1 : siteGroupsCB.SiteGroup.Id; }
            set { siteGroupsCB.SetSiteGroup(value); }
        }

        private void CreateUCInCells()
        {
            Clear();
            if (SitesGroupList.Count == 0)
                return;

            Meta.DataManager mrep = Meta.DataManager.GetInstance();
            Data.DataManager drep = Data.DataManager.GetInstance();
            //////List<Station> stations = mrep.StationRepository.Select(SitesGroupList.Select(x => x.StationId).Distinct().ToList());
            SitesGroupList.RemoveAll(x => SitesGroupList.FindIndex(y => y.Id == x.Id) != SitesGroupList.IndexOf(x));
            // ЦИКЛ по всем пунктам группы
            // TODO: неэффективный алгоритм чтения данных. Исправить. OSokolof@SOV.ru
            foreach (Site site in SitesGroupList)
            {
                // CREATE CHART OPTIONS
                // SITES
                List<Site> childSites = mrep.SiteRepository.SelectByParent(site.Id);
                Site AHK_Or_Hydropost = childSites.Find(
                    x => (new List<int> { (int)EnumStationType.HydroPost, (int)EnumStationType.AHK }).Contains(x.TypeId)
                );
                if (AHK_Or_Hydropost == null)
                    continue;
                // VARIABLES
                List<int> varIds = new List<int> { (int)EnumVariable.GageHeightF, (int)EnumVariable.PrecipDay24F };
                List<Variable> vars = mrep.VariableRepository.Select(varIds);

                // CATALOGS

                DataFilter dataFilter = new DataFilter()
                {
                    DateTimePeriod = this.DateTimePeriod,
                    CatalogFilter = new CatalogFilter()
                    {
                        Sites = childSites.Select(x => x.Id).Distinct().ToList(),
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
                    IsDateLOC = this.TimeType == EnumDateType.LOC
                };

                UCChartHydro.ChartOptions chartOptions = new UCChartHydro.ChartOptions()
                {
                    Sites = childSites,
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
                uc.title = site.GetName(2, Meta.SiteTypeRepository.GetCash());
                uc.Dock = DockStyle.Fill;
                uc.HandleDestroyed += uc_HandleDestroyed;

                tlp.Controls.Add(uc);
            }
            RefreshPanelControls();
            if (tlp.Controls.Count == 0)
                MessageBox.Show("Нет данных");
        }

        void uc_HandleDestroyed(object sender, EventArgs e)
        {
            tlp.Controls.Remove((Control)sender);
            RefreshPanelControls();
        }

        private void RefreshPanelControls()
        {
            tlp.RowCount = tlp.Controls.Count == 0 ? 1 : (int)Math.Floor(Math.Sqrt(tlp.Controls.Count - 1)) + 1;
            tlp.ColumnCount = (int)Math.Ceiling(tlp.Controls.Count / (double)tlp.RowCount);
            tlp.ColumnStyles.Clear();
            tlp.RowStyles.Clear();

            for (int i = 0; i < tlp.ColumnCount; ++i)
                tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            for (int i = 0; i < tlp.RowCount; ++i)
                tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));

            for (int i = 0, iRow = 0, iCol = 0; i < tlp.Controls.Count; ++i, ++iCol)
            {
                if (iCol == tlp.ColumnCount)
                {
                    iCol = 0;
                    ++iRow;
                }
                tlp.SetCellPosition(tlp.Controls[i], new TableLayoutPanelCellPosition(iCol, iRow));
            }
        }

        public int ColumnCount
        {
            get { return int.Parse(columnCountTextBox.Text); }
            set { columnCountTextBox.Text = value < 1 ? "1" : value.ToString(); }
        }

        static System.Threading.Timer _timer;
        static bool _isBusy = false;
        private Cursor cs;

        public UCChartsOper(int userOrganisationId, ChartsFilter chartsFilter)
        {
            InitializeComponent();
            Clear();
            cs = this.Cursor;
            timeTypeComboBox.Items.Add(EnumDateType.LOC);
            timeTypeComboBox.Items.Add(EnumDateType.UTC);
            timeTypeComboBox.SelectedIndex = 0;

            //siteGroupsCB.Items.AddRange(Meta.DataManager.GetInstance().SiteGroupRepository.SelectGroups().ToArray());
            this.userOrganisationId = userOrganisationId;
            this.chartsFilter = chartsFilter;
            //Инициализация событий после присвоение значений из chartsFilter
            this.siteGroupsCB.SelectedIndexChanged += new System.EventHandler(this.siteGroupsCB_SelectedIndexChanged);
            this.dateTimePeriodButton.Click += new System.EventHandler(this.dateTimePeriodButton_Click);
        }

        void SetTimer()
        {
            if (_timer != null) _timer.Dispose();
            _timer = new System.Threading.Timer(AsyncFill, null, 0, (int)(double.Parse(refreshPeriodMinutesTextBox.Text) * 60 * 1000));
        }

        EnumDateType TimeType
        {
            get
            {
                return (EnumDateType)timeTypeComboBox.SelectedItem;
            }
        }

        private void siteGroupsCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (siteGroupsCB.SelectedIndex != -1)
                SitesGroupList = siteGroupsCB.GetGroupSites();
            refreshToolStripButton_Click(null, null);
            ActiveControl = toolStrip1;
        }

        private void FillChart(UCChartHydro uc)
        {
            uc.UpdateData();
            System.Diagnostics.Trace.WriteLine(string.Format("{0}", uc.title));
        }

        private void AsyncFill(Object stateInfo = null)
        {
            foreach (UCChartHydro uc in tlp.Controls)
            {
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += (obj, e) => FillChart(uc);
                worker.RunWorkerAsync();
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
                this.Cursor = Cursors.WaitCursor;
                refreshToolStripButton_Click(null, null);
                this.Cursor = cs;
            }
        }

        private void optionsToolStripButton_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel2Collapsed = !splitContainer1.Panel2Collapsed;
        }

        public void OnShow()
        {
            splitContainer1.Panel2Collapsed = true;
            refreshToolStripButton_Click(null, null);
        }

        private void addSitesToolStrip_Click(object sender, EventArgs e)
        {
            var uc = new UCStations();
            uc.VisibleAddNewButton = false;
            uc.VisibleEditStationButton = false;
            uc.EnableMenuStrip = false;
            uc.VisibleNoSiteButton = false;
            uc.MultySelect = true;
            uc.OnComplateSelectionEvent += AddSitesByUCStations;
            new FormSingleUC(uc, "Добавить пункты на график", 500, 400).Show();
        }

        private void AddSitesByUCStations(UCStations sender)
        {
            foreach (var site in sender.SelectedSites())
                SitesGroupList.Add(site);
            sender.Parent.Dispose();
            OnShow();
        }
    }
}
