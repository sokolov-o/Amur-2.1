using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using SOV.Amur.Meta;
using SOV.Common;
using Cursor = System.Windows.Forms.Cursor;

namespace SOV.Amur.Data.Chart
{
    public partial class UCChartJoint : UserControl
    {
        private class SeriasStyle
        {
            public ChartDashStyle style;
            public int width;
            public SeriasStyle(ChartDashStyle style, int width)
            {
                this.style = style;
                this.width = width;
            }
        };

        private static SeriasStyle ActiveSerias = new SeriasStyle(ChartDashStyle.Dot, 5);
        private static SeriasStyle FixedSerias = new SeriasStyle(ChartDashStyle.Solid, 5);
        private static SeriasStyle CommonSerias = new SeriasStyle(ChartDashStyle.Solid, 2);

        private List<Site> sitesGroupList = new List<Site>();
        private DataFilter dataFilter;
        private List<DataValue> fullData = new List<DataValue>();
        private List<Catalog> catalogs = new List<Catalog>();
        private Dictionary<int, int> chartHoursOffset = new Dictionary<int, int>();

        private int activeSite = -1;
        private int ActiveSite
        {
            get { return activeSite; }
            set
            {
                if (activeSite >= 0)
                {
                    chart.Series.First(x => (int)x.Tag == activeSite).BorderDashStyle = CommonSerias.style;
                    chart.Series.First(x => (int)x.Tag == activeSite).BorderWidth = CommonSerias.width;
                }
                activeSite = value;
                if (activeSite < 0)
                    return;
                activeSiteLabel.Text = SitePlusGeoObjName(value);
                chart.Series.First(x => (int)x.Tag == activeSite).BorderDashStyle = ActiveSerias.style;
                chart.Series.First(x => (int)x.Tag == activeSite).BorderWidth = ActiveSerias.width;
                foreach (var item in offsetSite.Items)
                    if (((DicItem)item).Id == value)
                        offsetSite.SelectedItem = item;
            }
        }
        private int fixedSite = -1;
        private int FixedSite
        {
            get { return fixedSite; }
            set
            {
                fixedSite = value;
                ActiveSite = -1;
                fixedSiteLabel.Text = SitePlusGeoObjName(value);
                chartHoursOffset[value] = 0;
                ResetSiteTitle(fixedSite);
                FillChart();
            }
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
        private Cursor cs = Cursor.Current;
        private double currPointXVal = -1;
        private Dictionary<string, DataPoint> seriesCurrPoint = new Dictionary<string, DataPoint>();

        private List<double> trendX = new List<double>(), trendY = new List<double>();

        public UCChartJoint()
        {
            InitializeComponent();
            siteGroupComboBox.Fill();
            offsetTypeComboBox.SelectedIndex = 0;
            timeTypeComboBox.Items.Add(EnumDateType.LOC);
            timeTypeComboBox.Items.Add(EnumDateType.UTC);
            timeTypeComboBox.SelectedIndex = 0;

            TimePeriod = new DateTimePeriod(new DateTime(2016, 8, 15), new DateTime(2016, 8, 25),
                DateTimePeriod.Type.Period, 0);
            chart.Series.Clear();
            actionRadioButton_CheckedChanged(null, null);
            ucVariablesList.Fill(Meta.DataManager.GetInstance().VariableRepository.Select());
            ucVariablesList.SelectedId = (int)EnumVariable.GageHeightF;
        }

        EnumDateType TimeType
        {
            get
            {
                return (EnumDateType)timeTypeComboBox.SelectedItem;
            }
        }

        private void siteGroupComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //var id = siteGroupComboBox.SelectedIndex < 0 ? -1 : ((SiteGroup)siteGroupComboBox.SelectedItem).Id;
            if (siteGroupComboBox.SiteGroup != null)
                sitesGroupList = siteGroupComboBox.GetGroupSites();
            refresh_Click(null, null);
            ActiveControl = toolStrip1;
        }

        private void addSite_Click(object sender, EventArgs e)
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
                if (sitesGroupList.Find(x => x.Id == site.Id) == null) sitesGroupList.Add(site);
            sender.Parent.Dispose();
            refresh_Click(null, null);
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

        private void CreateChartArea(string name)
        {
            var newChartArea = new ChartArea(name);
            newChartArea.AxisY.IsStartedFromZero = false;
            newChartArea.AxisX.Minimum = TimePeriod.DateS.Value.ToOADate();
            newChartArea.AxisX.Maximum = TimePeriod.DateF.Value.ToOADate();
            newChartArea.CursorX.IsUserEnabled = true;
            newChartArea.CursorX.IntervalType = DateTimeIntervalType.Hours;
            newChartArea.CursorX.Interval = 1;
            newChartArea.CursorX.IsUserSelectionEnabled = true;
            //newChartArea.AxisX.MaximumAutoSize = 90;
            newChartArea.AxisX.LabelStyle.Format = "dd-MM-yy";
            //newChartArea.AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
            newChartArea.AxisX.IntervalType = DateTimeIntervalType.Days;
            newChartArea.AxisX.Interval = 1;
            //newChartArea.AxisY.

            newChartArea.AxisX.ScaleView.Zoomable = false;
            newChartArea.AxisX.ScrollBar.IsPositionedInside = false;
            newChartArea.AxisY.ScaleView.Zoomable = false;
            newChartArea.AxisY.ScrollBar.IsPositionedInside = false;

            chart.ChartAreas.Add(newChartArea);
        }

        private void FillChart()
        {
            bool isSeparete = !separateMenuItem.Enabled;
            List<Color> validColors = chart.Series.Count > 0 ?
                chart.Series.Select(x => x.Color).ToList() :
                new List<Color>()
                {
                    Color.Blue, Color.BlueViolet, Color.Brown, Color.Chocolate, Color.Crimson, Color.DarkBlue,
                    Color.DarkGreen, Color.DarkCyan, Color.DarkGoldenrod, Color.DarkMagenta, Color.DarkSlateGray,
                    Color.OliveDrab, Color.DarkRed, Color.DarkGray, Color.DarkKhaki, Color.DarkSlateBlue, Color.DarkOliveGreen
                };
            List<string> names = chart.Series.Count > 0 ? chart.Series.Select(x => x.Name).ToList() : null;
            int counter = 0;
            seriesCurrPoint.Clear();
            chart.Series.Clear();
            chart.ChartAreas.Clear();
            chart.Legends[0].CustomItems.Clear();

            if (!isSeparete)
                CreateChartArea("Def");

            foreach (var site in sitesGroupList)
            {
                var data = SubData(catalogs, site.Id);
                if (data.Count == 0)
                    continue;
                Series seria = new Series(names != null ? names[counter] : SitePlusGeoObjName(site.Id));
                seria.ChartArea = isSeparete ? seria.Name : "Def";
                seria.XAxisType = AxisType.Primary;
                seria.YAxisType = AxisType.Primary;
                seria.ChartType = SeriesChartType.Line;
                seria.EmptyPointStyle.Color = Color.Transparent;
                seria.Color = counter < validColors.Count ? validColors[counter++] : Color.Black;

                SeriasStyle style = CommonSerias;
                if (site.Id == ActiveSite)
                    style = ActiveSerias;
                if (site.Id == FixedSite)
                    style = FixedSerias;

                seria.BorderWidth = style.width;
                seria.BorderDashStyle = style.style;
                seria.Tag = site.Id;

                DateTime itrDate = TimePeriod.DateS.Value;
                for (int i = 0; i < data.Count; ++i)
                {
                    DataPoint point = new DataPoint();
                    DateTime date = data[i].Date(TimeType);
                    bool isEmpty = (date - itrDate).TotalDays >= 1;
                    itrDate = itrDate.AddDays((int)(date - itrDate).TotalDays + 1);
                    point.SetValueXY(date.AddHours(chartHoursOffset[site.Id]), data[i].Value);
                    point.IsEmpty = isEmpty;
                    point.SetCustomProperty("id", data[i].Id.ToString());
                    point.ToolTip = string.Format("Время наблюдения: {0}\nЗначение: {1}", date.ToString("dd.MM.yy HH:mm"), data[i].Value);

                    point.MarkerSize = 2;
                    point.MarkerStyle = isEmpty ? MarkerStyle.None : MarkerStyle.Circle;
                    seria.Points.Add(point);
                }
                // SHOW SERIA
                if (isSeparete)
                    CreateChartArea(seria.Name);
                //if (site != sitesGroupList[sitesGroupList.Count - 1])
                //seria.ChartType = SeriesChartType.Stock;
                seria.XValueType = ChartValueType.DateTime;
                chart.Series.Add(seria);
                seriesCurrPoint.Add(seria.Name, null);
            }
            //chart.ChartAreas[0].AlignWithChartArea = chart.ChartAreas[1].Name;
            for (int i = 0; i < chart.ChartAreas.Count - 1; ++i)
            {
                chart.ChartAreas[i].AlignWithChartArea = chart.ChartAreas[chart.ChartAreas.Count - 1].Name;
                chart.ChartAreas[i].AxisX.LabelStyle.Enabled = false;
            }
            MarkPoints();
            InitInfoBlock();
        }

        public void TrendChart()
        {
            seriesCurrPoint.Clear();
            chart.Series.Clear();
            chart.ChartAreas.Clear();

            trendX.Clear();
            trendY.Clear();

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
            SeriasStyle style = CommonSerias;

            var site1 = sitesGroupList[0];
            var site2 = sitesGroupList[1];
            var data1 = SubData(catalogs, site1.Id);
            var data2 = SubData(catalogs, site2.Id);

            foreach (var dataElm1 in data1)
            {
                DateTime date1 = dataElm1.Date(TimeType).AddHours(chartHoursOffset[site1.Id]);
                var dataElm2 = data2.Find(x =>
                    x.Date(TimeType).AddHours(chartHoursOffset[site2.Id]) > date1.AddSeconds(-1) &&
                    x.Date(TimeType).AddHours(chartHoursOffset[site2.Id]) < date1.AddSeconds(1)
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
                seria.Points.Add(point);
            }
            chart.Series.Add(seria);
            chart.Series["Points"].IsVisibleInLegend = false;
            drawTrendLine();
        }

        private void drawTrendLine()
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
            addLegentItem("Коэффициенты", true);
            addLegentItem("a = " + Math.Round(a, 4));
            addLegentItem("b = " + Math.Round(b, 4));
            addLegentItem("R^2 = " + Math.Round(Math.Pow(r, 2), 4));
            addLegentItem("");
            addLegentItem("Смещение", true);
            addLegentItem(SitePlusGeoObjName(sitesGroupList[0].Id) + " " + SiteNameOffsetSuffix(sitesGroupList[0].Id));
            addLegentItem(SitePlusGeoObjName(sitesGroupList[1].Id) + " " + SiteNameOffsetSuffix(sitesGroupList[1].Id));
        }

        private void addLegentItem(string text, bool isTitle = false)
        {
            LegendItem newItem = new LegendItem();
            newItem.Cells.Add(LegendCellType.Text, text, isTitle ? ContentAlignment.MiddleCenter : ContentAlignment.MiddleLeft);
            if (isTitle)
                newItem.Cells[0].Font = new Font(FontFamily.GenericSerif, 10, FontStyle.Bold);
            chart.Legends[0].CustomItems.Add(newItem);
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            if (ucVariablesList.SelectedVariable == null)
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
                    Sites = sitesGroupList.Select(x => x.Id).Distinct().ToList(),
                    Variables = new List<int> { ucVariablesList.SelectedVariable.Id },
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
            offsetSite.Items.Clear();
            foreach (var site in sitesGroupList)
            {
                chartHoursOffset[site.Id] = 0;
                offsetSite.Items.Add(new DicItem(site.Id, SitePlusGeoObjName(site.Id)));
                if (site.Id == ActiveSite)
                    offsetSite.SelectedIndex = offsetSite.Items.Count - 1;
            }
            chart.Series.Clear();
            FillChart();
        }

        private void dateButton_Click(object sender, EventArgs e)
        {
            FormDateTimePeriod frm = new Common.FormDateTimePeriod();
            frm.DateTimePeriod = TimePeriod;
            frm.StartPosition = FormStartPosition.CenterParent;
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TimePeriod = frm.DateTimePeriod;
                this.Cursor = Cursors.WaitCursor;
                refresh_Click(null, null);
                this.Cursor = cs;
            }
        }

        private void ResetSiteTitle(int siteId)
        {
            var series = chart.Series.First(x => (int)x.Tag == siteId);
            series.Name = new Regex("\\s[\\+-][\\d]+\\s[дч]\\.").Replace(series.Name, "");
        }

        private string SiteNameOffsetSuffix(int siteId)
        {
            int days = (chartHoursOffset[siteId] / 24);
            int hours = (chartHoursOffset[siteId] % 24);
            return (days != 0 ? string.Format(" {0}{1} д.", days > 0 ? "+" : "", days) : "") +
                    (hours != 0 ? string.Format(" {0}{1} ч.", hours > 0 ? "+" : "", hours) : "");

        }

        private void ChangeOffsetClick(object sender, EventArgs e)
        {
            if (ActiveSite == -1)
                return;
            int scale = 1;
            switch (offsetTypeComboBox.SelectedIndex)
            {
                case 0: scale = 1; break;
                case 1: scale = 24; break;
            }
            chartHoursOffset[activeSite] += int.Parse((string)((Button)sender).Tag) * (int)offsetCountUpDown.Value * scale;
            if (!trendToolStripMenuItem.Enabled)
                TrendChart();
            else
            {
                ResetSiteTitle(activeSite);
                chart.Series.First(x => (int)x.Tag == activeSite).Name += SiteNameOffsetSuffix(activeSite);
                FillChart();
            }
        }

        private void MarkPoint(DataPoint point, bool clear = false)
        {
            if (point == null)
                return;
            point.Label = clear ? "" : point.YValues[0].ToString();
            point.MarkerSize = clear ? 1 : 8;
        }

        private void MarkPoints(bool clear = false)
        {
            if (currPointXVal > 0)
                foreach (var seria in chart.Series)
                {
                    var points = seria.Points.Where(x => x.XValue == currPointXVal).ToList();
                    if (points.Count == 0)
                        continue;
                    MarkPoint(points[0], clear);
                    chart.ChartAreas[seria.ChartArea].CursorX.SetCursorPosition(currPointXVal);
                }
            foreach (var point in seriesCurrPoint.Values)
                MarkPoint(point);
        }

        private void InitInfoBlock()
        {
            infoDataPanel.Controls.Clear();
            if (FixedSite < 0 || currPointXVal < 0)
                return;
            var fixedSeria = chart.Series.First(x => (int)x.Tag == FixedSite);
            var fixedPoints = fixedSeria.Points.Where(x => x.XValue == currPointXVal).ToList();
            foreach (var seria in chart.Series.Where(x => x.Tag != fixedSeria.Tag))
            {
                var tmpPoints = seria.Points.Where(x => x.XValue == currPointXVal).ToList();
                if (tmpPoints.Count() == 0 || fixedPoints.Count() == 0)
                    continue;
                var tmpLabel = new Label();
                tmpLabel.Text = seria.Name + ": " + (fixedPoints[0].YValues[0] - tmpPoints[0].YValues[0]).ToString() + "\n";
                infoDataPanel.Controls.Add(tmpLabel);
            }
        }

        private void chart_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                HitTestResult result = chart.HitTest(e.X, e.Y);
                if (!trendToolStripMenuItem.Enabled)
                {
                    MarkPoints(true);
                    currPointXVal = -1;
                    if (result.ChartElementType != ChartElementType.DataPoint)
                        return;
                    var point = (DataPoint)result.Object;
                    currPointXVal = point.XValue;
                    MarkPoints();
                    return;
                }

                if (plotActionRadioButton.Checked)
                {
                    if (result.ChartElementType != ChartElementType.DataPoint)
                    {
                        ActiveSite = -1;
                        return;
                    }
                    if (FixedSite == (int)result.Series.Tag)
                        return;
                    if (ActiveSite == (int)result.Series.Tag)
                        FixedSite = ActiveSite;
                    else
                        ActiveSite = (int)result.Series.Tag;
                }
                if (pointActionRadioButton.Checked)
                {
                    MarkPoints(true);
                    currPointXVal = -1;
                    if (result.ChartElementType != ChartElementType.DataPoint)
                        return;
                    var point = (DataPoint)result.Object;
                    dateInfolabel.Text = DateTime.FromOADate(point.XValue).ToString("dd.MM.yyyy hh:mm");
                    currPointXVal = point.XValue;
                    MarkPoints();
                    InitInfoBlock();
                }
                if (twoPointsActionRadioButton.Checked)
                {
                    MarkPoint(seriesCurrPoint[result.Series.Name], true);
                    //currPointXVal = -1;
                    if (result.ChartElementType != ChartElementType.DataPoint)
                        return;
                    var point = (DataPoint)result.Object;
                    seriesCurrPoint[result.Series.Name] = point;
                    MarkPoints();
                    //InitInfoBlock();
                }
            }
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(this, new System.Drawing.Point(e.X, e.Y));
        }

        private string SitePlusGeoObjName(int siteId)
        {
            List<SiteGeoObject> geoObjId = Meta.DataManager.GetInstance().SiteGeoObjectRepository.SelectBySites(new List<int>() { siteId });
            string geoObjName = geoObjId.Count == 0 ? "" : Meta.GeoObjectRepository.GetCash()
                .Find(x => x.Id == geoObjId[0].GeoObjectId)
                .Name;
            return SiteRepository.GetCash().Find(x => x.Id == siteId).Code + " " + geoObjName;
        }

        private void jointMenuItem_Click(object sender, EventArgs e)
        {
            trendToolStripMenuItem.Enabled = separateMenuItem.Enabled = true;
            jointMenuItem.Enabled = false;
            pointActionRadioButton.Enabled = true;
            FillChart();
        }

        private void separateMenuItem_Click(object sender, EventArgs e)
        {
            trendToolStripMenuItem.Enabled = jointMenuItem.Enabled = true;
            separateMenuItem.Enabled = false;
            pointActionRadioButton.Enabled = true;
            FillChart();
        }


        private void trendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sitesGroupList.Count != 2)
            {
                MessageBox.Show("Количество выбранных сайтов не ровно 2");
                return;
            }
            jointMenuItem.Enabled = separateMenuItem.Enabled = true;
            trendToolStripMenuItem.Enabled = false;
            plotActionRadioButton.Checked = true;
            pointActionRadioButton.Enabled = false;
            TrendChart();
        }

        private void actionRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            offsetPanel.Enabled = plotActionRadioButton.Checked;
            foreach (var chartArea in chart.ChartAreas)
            {
                chartArea.CursorX.IsUserEnabled = pointActionRadioButton.Checked;
                chartArea.CursorX.IsUserSelectionEnabled = pointActionRadioButton.Checked;
            }
            if (!pointActionRadioButton.Checked && twoPointsActionRadioButton.Checked)
            {
                MarkPoints(true);
                currPointXVal = -1;
                seriesCurrPoint.Clear();
                foreach (var seria in chart.Series)
                    seriesCurrPoint.Add(seria.Name, null);
                foreach (var chartArea in chart.ChartAreas)
                    chartArea.CursorX.SetCursorPosition(0);
            }
            if (pointActionRadioButton.Checked && !twoPointsActionRadioButton.Checked)
            {
                foreach (var point in seriesCurrPoint.Values)
                    MarkPoint(point, true);
                seriesCurrPoint.Clear();
            }
        }

        private void offsetSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            activeSite = ((DicItem)offsetSite.SelectedItem).Id;
        }

        private void varToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ucVariablesList.Visible = !ucVariablesList.Visible;
        }
    }
}
