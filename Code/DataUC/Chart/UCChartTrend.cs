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
using System.Windows.Forms.DataVisualization.Charting;

namespace SOV.Amur.Data.Chart
{
    public partial class UCChartTrend : UserControl
    {
        private class SiteAndVar
        {
            private int? varId;
            public int VarId
            {
                get { return varId.HasValue ? varId.Value : 0; }
                set { varId = value; }
            }

            private int? siteId { get; set; }
            public int SiteId
            {
                get { return siteId.HasValue ? siteId.Value : 0; }
                set { siteId = value; }
            }

            public bool IsInit() { return siteId.HasValue && varId.HasValue; }

            public SiteAndVar(int? _siteId = null, int? _varId = (int)EnumVariable.GageHeightF)
            {
                varId = _varId;
                siteId = _siteId;
            }
        }

        private DataFilter dataFilter;
        private Dictionary<int, int> chartHoursOffset = new Dictionary<int, int>();
        private List<SiteAndVar> siteAndVarArr = new List<SiteAndVar>();
        private List<DataValue> fullData = new List<DataValue>();
        private List<Catalog> catalogs = new List<Catalog>();
        private ToolTip toolTip = new ToolTip();

        public UCChartTrend()
        {
            InitializeComponent();

            offsetTypeComboBox.SelectedIndex = 0;
            timeTypeComboBox.Items.Add(EnumDateType.LOC);
            timeTypeComboBox.Items.Add(EnumDateType.UTC);
            timeTypeComboBox.SelectedIndex = 0;

            TimePeriod = new DateTimePeriod(new DateTime(2016, 8, 15), new DateTime(2016, 8, 25),
                                            DateTimePeriod.Type.Period, 0);
            //TimePeriod = new DateTimePeriod();
            chart.Series.Clear();

            siteAndVarArr.Add(new SiteAndVar());
            siteAndVarArr.Add(new SiteAndVar());
            UpdateAxisButtonsImg();
        }

        private DateTimePeriod timePeriod;
        public DateTimePeriod TimePeriod
        {
            get { return timePeriod; }
            private set
            {
                timePeriod = value;
                dateLabel.Text = value == null ? null : value.ToStringRus("dd-MM-yy");
            }
        }

        EnumDateType TimeType
        {
            get { return (EnumDateType)timeTypeComboBox.SelectedItem; }
        }

        private List<DataValue> SubData(List<Catalog> catalogs, int site)
        {
            List<Catalog> subCatalogs = catalogs.Where(x => x.SiteId == site).ToList();
            return fullData.Where(x =>
                subCatalogs.Exists(y => y.Id == x.CatalogId) &&
                x.Date(TimeType) >= TimePeriod.DateS &&
                x.Date(TimeType) <= TimePeriod.DateF
            ).ToList();
        }

        public void UpdateChart()
        {
            chart.Series.Clear();
            chart.ChartAreas.Clear();

            List<double> trendX = new List<double>();
            List<double> trendY = new List<double>();


            var newChartArea = new ChartArea("Def");
            newChartArea.AxisY.IsStartedFromZero = false;
            chart.ChartAreas.Add(newChartArea);

            Series seria = new Series("Points");
            seria.ChartArea = "Def";
            seria.XAxisType = AxisType.Primary;
            seria.YAxisType = AxisType.Primary;
            seria.ChartType = SeriesChartType.Point;
            seria.EmptyPointStyle.Color = Color.Transparent;
            seria.Color = Color.Blue;

            var xSiteId = siteAndVarArr[0].SiteId;
            var ySiteId = siteAndVarArr[1].SiteId;
            var data1 = SubData(catalogs, xSiteId);
            var data2 = SubData(catalogs, ySiteId);

            foreach (var dataElm1 in data1)
            {
                DateTime date1 = dataElm1.Date(TimeType).AddHours(chartHoursOffset[xSiteId]);
                var dataElm2 = data2.Find(x =>
                    x.Date(TimeType).AddHours(chartHoursOffset[ySiteId]) > date1.AddSeconds(-1) &&
                    x.Date(TimeType).AddHours(chartHoursOffset[ySiteId]) < date1.AddSeconds(1)
                );
                if (dataElm2 == null)
                    continue;
                DataPoint point = new DataPoint();
                trendX.Add(dataElm1.Value);
                trendY.Add(dataElm2.Value);
                point.SetValueXY(dataElm1.Value, dataElm2.Value);
                point.ToolTip = string.Format(
                    "Время наблюдения: {0}\nЗначение X: {1}\nЗначение Y: {2}",
                    date1.ToString("dd.MM.yy HH:mm"),
                    dataElm1.Value,
                    dataElm2.Value
                );
                point.MarkerSize = 5;
                point.Tag = date1.ToString("dd.MM.yy HH:mm");
                seria.Points.Add(point);
            }
            chart.Series.Add(seria);
            chart.Series["Points"].IsVisibleInLegend = false;
            chart.ChartAreas[0].AxisX.RoundAxisValues();
            chart.ChartAreas[0].AxisX.IsStartedFromZero = false;
            drawTrendLine(trendX, trendY);
        }

        private void drawTrendLine(List<double> trendX, List<double> trendY)
        {
            double Sx = 0, Sy = 0, Sxy = 0, Sxx = 0, _Sx = 0, _Sy = 0, _Sxy = 0;
            int n = trendX.Count;
            if (n == 0)
                return;
            for (var i = 0; i < n; i++)
            {
                Sx += trendX[i];
                Sy += trendY[i];
                Sxy += trendX[i] * trendY[i];
                Sxx += trendX[i] * trendX[i];
            }
            Sx /= n;
            Sy /= n;
            Sxy /= n;
            Sxx /= n;
            double a = (Sx * Sy - Sxy) / (Sx * Sx - Sxx);
            double b = (Sxy - a * Sxx) / Sx;
            for (var i = 0; i < n; i++)
            {
                _Sx += Math.Pow(trendX[i] - Sx, 2);
                _Sy += Math.Pow(trendY[i] - Sy, 2);
                _Sxy += (trendX[i] - Sx) * (trendY[i] - Sy);
            }
            double r = _Sxy / (Math.Sqrt(_Sx) * Math.Sqrt(_Sy));
            Series seria = new Series("Trend Line");
            seria.ChartArea = "Def";
            seria.XAxisType = AxisType.Primary;
            seria.YAxisType = AxisType.Primary;
            seria.ChartType = SeriesChartType.Line;
            seria.Color = Color.Red;

            seria.Points.Add(new DataPoint(trendX.Min(), trendX.Min() * a + b));
            seria.Points.Add(new DataPoint(trendX.Max(), trendX.Max() * a + b));

            chart.Series.Add(seria);
            chart.Series["Trend Line"].IsVisibleInLegend = false;
            chart.Legends[0].CustomItems.Clear();
            addLegentItem("Параметры", true);
            addLegentItem("X: " + SitePlusGeoObjName(siteAndVarArr[0].SiteId));
            addLegentItem("Y: " + SitePlusGeoObjName(siteAndVarArr[1].SiteId));
            addLegentItem("");
            addLegentItem("a = " + Math.Round(a, 4));
            addLegentItem("b = " + Math.Round(b, 4));
            addLegentItem("R^2 = " + Math.Round(Math.Pow(r, 2), 4));
        }

        private void addLegentItem(string text, bool isTitle = false)
        {
            LegendItem newItem = new LegendItem();
            newItem.Cells.Add(LegendCellType.Text, text, isTitle ? ContentAlignment.MiddleCenter : ContentAlignment.MiddleLeft);
            if (isTitle)
                newItem.Cells[0].Font = new Font(FontFamily.GenericSerif, 10, FontStyle.Bold);
            chart.Legends[0].CustomItems.Add(newItem);
        }

        private string offsetSuffix(int siteId)
        {
            int days = (chartHoursOffset[siteId] / 24);
            int hours = (chartHoursOffset[siteId] % 24);
            return (days != 0 ? string.Format(" {0}{1} д.", days > 0 ? "+" : "", days) : "") +
                    (hours != 0 ? string.Format(" {0}{1} ч.", hours > 0 ? "+" : "", hours) : "");

        }

        private string SitePlusGeoObjName(int siteId)
        {
            List<SiteGeoObject> geoObjId = Meta.DataManager.GetInstance().SiteGeoObjectRepository.SelectBySites(new List<int>() { siteId });
            string geoObjName = geoObjId.Count == 0 ? "" : Meta.GeoObjectRepository.GetCash()
                .Find(x => x.Id == geoObjId[0].GeoObjectId)
                .Name;
            Site site = SiteRepository.GetCash().Find(x => x.Id == siteId);
            return site.Code + " " + geoObjName + " " + site.Name;
        }

        private void UpdateAxisButtonsImg()
        {
            XAxisButton.Image = siteAndVarArr[0].IsInit() ? Properties.Resources.X_letter : Properties.Resources.X_letter_attention;
            YAxisButton.Image = siteAndVarArr[1].IsInit() ? Properties.Resources.Y_letter : Properties.Resources.Y_letter_attention;
        }

        /*** Events ***/

        private void SwapAsixButton_Click(object sender, EventArgs e)
        {
            SiteAndVar tmp = siteAndVarArr[0];
            siteAndVarArr[0] = siteAndVarArr[1];
            siteAndVarArr[1] = tmp;

            UpdateChart();
        }

        private void AxisButton_Click(object sender, EventArgs e)
        {
            var form = new FormSiteAndVarSelection();
            int index = Int32.Parse((string)((ToolStripButton)sender).Tag);

            form.OnComplateSelectionEvent += onSiteAndVarSelect;
            form.Show();
            form.SiteId = siteAndVarArr[index].SiteId;
            form.VarId = siteAndVarArr[index].VarId;
            form.Tag = ((ToolStripButton)sender).Tag;
        }

        private void onSiteAndVarSelect(FormSiteAndVarSelection form)
        {
            int index = Int32.Parse((string)form.Tag);
            siteAndVarArr[index].SiteId = form.SiteId.Value;
            siteAndVarArr[index].VarId = form.VarId.Value;
            UpdateAxisButtonsImg();
            refreshButton_Click(null, null);
        }

        private void dateButton_Click(object sender, EventArgs e)
        {
            FormDateTimePeriod frm = new Common.FormDateTimePeriod();
            frm.DateTimePeriod = TimePeriod;
            frm.StartPosition = FormStartPosition.CenterParent;
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TimePeriod = frm.DateTimePeriod;
                var cs = this.Cursor;
                this.Cursor = Cursors.WaitCursor;
                refreshButton_Click(null, null);
                this.Cursor = cs;
            }
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            if (!siteAndVarArr[0].IsInit() || !siteAndVarArr[1].IsInit())
                return;
            dataFilter = new DataFilter()
            {
                DateTimePeriod = new DateTimePeriod(
                    TimePeriod.DateS.Value.AddMonths(-1),
                    TimePeriod.DateF.Value.AddMonths(1),
                    TimePeriod.PeriodType,
                    TimePeriod.DaysBeforeDateNow
                ),
                CatalogFilter = new CatalogFilter()
                {
                    Sites = new List<int> { siteAndVarArr[0].SiteId, siteAndVarArr[1].SiteId },
                    Variables = new List<int> { siteAndVarArr[0].VarId, siteAndVarArr[1].VarId },
                    Methods = null,
                    Sources = null,
                    OffsetTypes = new List<int> { (int)EnumOffsetType.NoOffset },
                    OffsetValue = 0
                },
                FlagAQC = null,
                IsActualValueOnly = true,
                IsRefSiteData = false,
                IsSelectDeleted = false,
                IsDateLOC = TimeType == EnumDateType.LOC
            };
            catalogs = Meta.DataManager.GetInstance().CatalogRepository.Select(dataFilter.CatalogFilter);
            fullData = Data.DataManager.GetInstance().DataValueRepository.SelectA(dataFilter).OrderBy(x => x.DateUTC).ToList();
            chartHoursOffset.Clear();
            foreach (var siteVar in siteAndVarArr)
                chartHoursOffset[siteVar.SiteId] = 0;
            chart.Series.Clear();
            UpdateChart();
        }

        private void changeOffsetClick(object sender, EventArgs e)
        {
            if (!siteAndVarArr[0].IsInit())
                return;
            int scale = 1;
            switch (offsetTypeComboBox.SelectedIndex)
            {
                case 0: scale = 1; break;
                case 1: scale = 24; break;
            }
            int offsetDir = int.Parse((string)((ToolStripButton)sender).Tag);
            chartHoursOffset[siteAndVarArr[0].SiteId] += offsetDir * int.Parse(offsetTextBox.Text) * scale;
            offsetValueLabel.Text = offsetSuffix(siteAndVarArr[0].SiteId);
            UpdateChart();
        }

        private void offsetCamcelButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < chartHoursOffset.Count; ++i)
                chartHoursOffset[i] = 0;
            offsetValueLabel.Text = "0 ч.";
            UpdateChart();
        }

        private void chart_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                toolTip.RemoveAll();
                HitTestResult result = chart.HitTest(e.X, e.Y);
                var pos = e.Location;
                if (result.ChartElementType != ChartElementType.DataPoint)
                    return;
                var point = (DataPoint)result.Object;
                toolTip.Show(
                    string.Format(
                        "Время наблюдения: {0}\nЗначение X: {1}\nЗначение Y: {2}",
                        (string)point.Tag,
                        point.XValue,
                        point.YValues[0]
                    ),
                    chart, pos.X, pos.Y - 15
                );
            }
        }
    }
}
