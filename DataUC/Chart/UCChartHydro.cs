using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;
using SOV.Amur.Meta;
using SOV.Common;
using System.Drawing.Printing;

namespace SOV.Amur.Data
{
    /// <summary>
    /// Графики: x - дата, y - значения.
    /// </summary>
    public partial class UCChartHydro : UserControl
    {
        private struct dataStyles
        {
            public struct GageHeightLimit
            {
                public static Color[] colors = new Color[] { Color.LightGreen, Color.Yellow, Color.Red };
            }

            public class GageHeightLine
            {
                public static Color color = Color.Blue;
            }

            public class GageHeightDot
            {
                public Color color;
                public MarkerStyle style;
                public int size;
                public GageHeightDot(Color _color, MarkerStyle _style, int _size)
                {
                    color = _color;
                    style = _style;
                    size = _size;
                }
            };

            public static GageHeightDot DailyGageHeightDot = new GageHeightDot(Color.Blue, MarkerStyle.Circle, 3);
            public static GageHeightDot HourlyGageHeightDot = new GageHeightDot(Color.Blue, MarkerStyle.Square, 3);
            public static GageHeightDot ErrorDot = new GageHeightDot(Color.Red, MarkerStyle.Circle, 10);
            public static GageHeightDot NoAQCDot = new GageHeightDot(Color.Gray, MarkerStyle.Circle, 3);
            public static GageHeightDot DeletedDot = new GageHeightDot(Color.Black, MarkerStyle.Circle, 6);

            public struct Percip
            {
                public static Color[] colors = new Color[] { Color.Blue, Color.LightBlue, Color.DarkBlue, Color.DeepSkyBlue };
                public static MarkerStyle marker = MarkerStyle.None;
            };

            public struct WarningTitle
            {
                public static Color color = Color.Red;
                public static ChartHatchStyle backStyle = ChartHatchStyle.LightDownwardDiagonal;
            }
        };

        public class ChartOptions
        {
            /// <summary>
            /// Первые два сайта - ГП ? АГК, далее опорные по осадкам и др.
            /// </summary>
            public List<Site> Sites { get; set; }
            public List<Variable> Vars { get; set; }
            public DataFilter DataFilter { get; set; }

            public double? AxesMinGageHeight { get; set; }
            public double? AxesMaxPrecipitation { get; set; }
            /// <summary>
            /// UTC || LOC
            /// </summary>
            public EnumDateType TimeType { get; set; }

            /// <summary>
            /// Список из Climate объектов для каждого из 3х предельных значений уровня воды
            /// </summary>
            public List<Climate>[] gageHeightLimits { get; set; }
        };

        int userOrganisationId;

        public UCChartHydro(int _userOrganisationId, ChartOptions _chartOptions)
        {
            fillDelegateMethod = Fill;
            userOrganisationId = _userOrganisationId;
            chartOptions = _chartOptions;
            catalogs = Meta.DataManager.GetInstance().CatalogRepository.Select(chartOptions.DataFilter.CatalogFilter);
            InitializeComponent();
            Clear();

            chart.Titles[0].BackColor = Color.Transparent;
            chart.Titles[0].ShadowOffset = 0;
            chart.Titles[0].Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold);
        }

        private void deleteAllToolStripButton_Click(object sender, EventArgs e)
        {
            Clear();
        }
        /// <summary>
        /// Отображение панели инструментов элемента управления.
        /// </summary>
        public bool ToolbarVisible
        {
            get
            {
                return toolStrip1.Visible;
            }
            set
            {
                toolStrip1.Visible = value;
            }
        }
        /// <summary>
        /// Заголовок графика
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// Опции и настройки графика.
        /// </summary>
        public ChartOptions chartOptions { get; private set; }

        /// <summary>
        /// Первые два сайта - ГП ? АГК, далее опорные по осадкам и др.
        /// </summary>
        //List<Site> _sites;
        //List<Variable> _vars;
        //List<Catalog> _catalogs;
        List<DataValue> data;
        List<Catalog> catalogs;

        public void UpdateData(List<DataValue> _data = null)
        {
            data = _data != null ?
                _data :
                Data.DataManager.GetInstance().DataValueRepository.SelectA(chartOptions.DataFilter).OrderBy(x => x.DateUTC).ToList();
            Invoke(fillDelegateMethod);
        }

        delegate void FillDelegate();

        private FillDelegate fillDelegateMethod;

        public void Fill()
        {
            try
            {
                Clear();

                double maxGageHeight = double.MinValue,
                       minGageHeight = double.MaxValue,
                       maxPercip = double.MinValue;
                Site hydroPost = chartOptions.Sites.Where(x => x.TypeId == (int)EnumStationType.HydroPost).FirstOrDefault();
                List<DataValue> dailyGageHeight = SubData(hydroPost, (int)EnumVariable.GageHeightF);
                AddSeriaGageHeight(ref dailyGageHeight, true, ref maxGageHeight, ref minGageHeight);

                Site ahk = chartOptions.Sites.Where(x => x.TypeId == (int)EnumStationType.AHK).FirstOrDefault();
                List<DataValue> hourlyGageHeight = SubData(ahk, (int)EnumVariable.GageHeightF);
                AddSeriaGageHeight(ref hourlyGageHeight, false, ref maxGageHeight, ref minGageHeight);

                AddSeriaPrecip(chartOptions.Sites, ref maxPercip);

                #region Заголовок
                //chart.Legends[0].Docking = Docking.Bottom;
                List<SiteGeoObject> geoObjId = Meta.DataManager.GetInstance().SiteGeoObjectRepository.SelectBySites(
                                                    new List<int>() { hydroPost.Id }
                                                );
                string geoObj = geoObjId.Count == 0 ? "" : Meta.DataManager.GetInstance().GeoObjectRepository.Select(
                                                                geoObjId[0].GeoObjectId
                                                        ).Name;
                geoObj = (chartOptions.Sites.Count > 0 ? chartOptions.Sites[0].Code : "НЕТ ПУНКТА!") + " " + geoObj;

                string titleData = (dailyGageHeight.Count == 0 ? "" : ("ГП: " + Math.Round(dailyGageHeight.Last().Value).ToString() + " cм")) +
                                    (hourlyGageHeight.Count == 0 ? "" : (" АГК: " + Math.Round(hourlyGageHeight.Last().Value).ToString() + " cм"));
                chart.Titles[0].Text = geoObj + " — " + title.Replace(", ГП", "") + (titleData == "" ? "" : " (" + titleData + ")");
                #endregion

                #region Настройка шкал графика
                foreach (Series seria in chart.Series)
                    seria.XValueType = ChartValueType.DateTime;

                bool hasGageHeight = (maxGageHeight - double.MinValue) >= 1e-10,
                     hasPercip = (maxPercip - double.MinValue) >= 1e-10;
                double percipInterval = 10,
                       gageHeightInterval = 50,
                       percipScale = hasGageHeight ? 3 : 1,
                       gageHeightScale = hasPercip ? 1.0 / 3.0 : 0;

                // Ось осадков
                if (hasPercip)
                {
                    double axe = ((int)((maxPercip * percipScale) / percipInterval) + 1) * percipInterval;
                    axe = double.IsNaN(chartOptions.AxesMaxPrecipitation.Value) ? axe : chartOptions.AxesMaxPrecipitation.Value;
                    chart.ChartAreas[0].Axes[3].Minimum = 0;
                    chart.ChartAreas[0].Axes[3].Maximum = (int)axe;
                }
                // Ось уровня
                if (hasGageHeight)
                {
                    double? minAxe = chartOptions.AxesMinGageHeight;
                    chart.ChartAreas[0].Axes[1].Minimum = !double.IsNaN(minAxe.Value) ? (double)minAxe : minGageHeight;
                    chart.ChartAreas[0].Axes[1].Maximum = maxGageHeight + (maxGageHeight - chart.ChartAreas[0].Axes[1].Minimum) * gageHeightScale;
                    // Нормирование границ графика
                    double tmpMax = chart.ChartAreas[0].Axes[1].Maximum,
                            tmpMin = chart.ChartAreas[0].Axes[1].Minimum;
                    chart.ChartAreas[0].Axes[1].Minimum = ((int)(tmpMin / gageHeightInterval) - (tmpMin < 0 ? 1 : 0)) * gageHeightInterval;
                    chart.ChartAreas[0].Axes[1].Maximum = ((int)(tmpMax / gageHeightInterval) + (tmpMax < 0 ? 0 : 1)) * gageHeightInterval;
                }
                #endregion

                AddGageHeightLimits(maxGageHeight);

                chart.ChartAreas[0].Axes[0].LabelStyle.Format = "dd-MM-yy";
                chart.ChartAreas[0].Axes[3].MajorGrid.Enabled = false;
                chart.ChartAreas[0].Axes[0].Tag = "Дата";
                chart.ChartAreas[0].Axes[1].Tag = "Уровень воды";
                chart.ChartAreas[0].Axes[3].Tag = "Осадки";
                //chart.ChartAreas[0].Axes[0].LabelAutoFitStyle = LabelAutoFitStyles.LabelsAngleStep30;

                SetZoom(true);
                chart.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Добавить на чарт области, ограниченные передельными зачениями уровня воды
        /// </summary>
        private void AddGageHeightLimits(double maxGageHeight)
        {
            List<StripLine> slines = new List<StripLine>();
            foreach (List<Climate> limit in chartOptions.gageHeightLimits)
            {
                if (limit.Count == 0)
                    continue;

                double val = limit[0].Data[1];

                StripLine sline = new StripLine();
                sline.IntervalOffset = val;
                sline.Text = val.ToString();
                sline.TextLineAlignment = StringAlignment.Far;
                sline.Interval = 0.0;
                sline.Font = new Font(sline.Font, FontStyle.Bold);
                slines.Add(sline);
            }
            if (slines.Count == 0)
                return;
            StripLine maxSline = new StripLine();
            maxSline.IntervalOffset = Math.Max(slines.Last().IntervalOffset, (int)chart.ChartAreas[0].Axes[1].Maximum) + 10;
            slines.Add(maxSline);

            for (int i = 0; i < slines.Count - 1; ++i)
            {
                // Проверка уровня на выход за НЯ/ОЯ
                if (maxGageHeight >= slines[i + 1].IntervalOffset)
                {
                    chart.Titles[0].BackHatchStyle = dataStyles.WarningTitle.backStyle;
                    chart.Titles[0].BackColor = dataStyles.WarningTitle.color;
                }
                slines[i].StripWidth = slines[i + 1].IntervalOffset - slines[i].IntervalOffset;
                slines[i].BackColor = dataStyles.GageHeightLimit.colors[i];
                chart.ChartAreas[0].AxisY.StripLines.Add(slines[i]);
            }

            maxSline.Dispose();
        }

        /// <summary>
        /// Срез данных по сайту и переменной
        /// </summary>
        private List<DataValue> SubData(Site site, int varId)
        {
            if (site == null)
                return new List<DataValue>();
            List<Catalog> subCatalogs = catalogs.Where(x => x.VariableId == varId && x.SiteId == site.Id).ToList();
            return data.Where(x => subCatalogs.Exists(y => y.Id == x.CatalogId)).ToList();
        }

        /// <summary>
        /// Переключение zoom чарта
        /// </summary>
        private void SetZoom(bool enable)
        {
            chart.ChartAreas[0].CursorX.IsUserEnabled = enable;
            chart.ChartAreas[0].CursorX.IsUserSelectionEnabled = enable;
            chart.ChartAreas[0].AxisX.ScaleView.Zoomable = enable;
            chart.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = enable;

            chart.ChartAreas[0].CursorY.IsUserEnabled = enable;
            chart.ChartAreas[0].CursorY.IsUserSelectionEnabled = enable;
            chart.ChartAreas[0].AxisY.ScaleView.Zoomable = enable;
            chart.ChartAreas[0].AxisY.ScrollBar.IsPositionedInside = enable;
        }

        private void AddSeriaPrecip(List<Site> sites, ref double max)
        {
            Variable var = chartOptions.Vars.FirstOrDefault(x => x.Id == (int)EnumVariable.PrecipDay24F);
            for (int j = 0, k = 0; j < sites.Count; j++)
            {
                if (sites[j] == null) continue;

                List<DataValue> datas = SubData(sites[j], var.Id);

                if (datas.Count == 0) continue;

                Series seria = new Series(var.NameRus + " (" + sites[j].Code + ")");
                seria.Tag = var;
                seria.XAxisType = AxisType.Primary;
                seria.YAxisType = AxisType.Secondary;
                seria.ChartType = SeriesChartType.Column;

                seria.MarkerStyle = dataStyles.Percip.marker;
                seria.Color = dataStyles.Percip.colors[k++];
                chart.ChartAreas[0].Axes[3].IsReversed = true;
                chart.ChartAreas[0].Axes[3].Tag = var.NameRus;

                // DATA BIND
                for (int i = 0; i < datas.Count; ++i)
                {
                    DataPoint point = new DataPoint();
                    DateTime date = datas[i].Date(chartOptions.TimeType);
                    point.SetValueXY(date, datas[i].Value);
                    point.ToolTip = string.Format(
                        "Пункт: {2}\nВремя наблюдения: {0}\nЗначение: {1}",
                        date.ToString("dd.MM.yy HH:mm"),
                        datas[i].Value,
                        sites[j].GetName(2, SiteTypeRepository.GetCash())
                    );
                    seria.Points.Add(point);

                    max = Math.Max(max, datas[i].Value);
                }
                // seria["PixelPointWidth"] = "10";
                chart.Series.Add(seria);
            }
        }

        private void AddSeriaGageHeight(ref List<DataValue> datas, bool daily, ref double max, ref double min)
        {
            Variable var = chartOptions.Vars.FirstOrDefault(x => x.Id == (int)EnumVariable.GageHeightF);

            if (datas.Count == 0) return;

            // CREATE SERIA

            Dictionary<byte, dataStyles.GageHeightDot> stylesByAQC = new Dictionary<byte, dataStyles.GageHeightDot>
            {
                {(byte)EnumFlagAQC.NoAQC, dataStyles.NoAQCDot},
                {(byte)EnumFlagAQC.Success, daily ? dataStyles.DailyGageHeightDot : dataStyles.HourlyGageHeightDot},
                {(byte)EnumFlagAQC.Error, dataStyles.ErrorDot},
                {(byte)EnumFlagAQC.Deleted, dataStyles.DeletedDot},
                {(byte)EnumFlagAQC.Approved, daily ? dataStyles.DailyGageHeightDot : dataStyles.HourlyGageHeightDot},
            };

            Series seria = new Series("Уровень воды, " + (daily ? "дневной" : "часовой"));
            seria.Tag = var;
            seria.XAxisType = AxisType.Primary;
            seria.YAxisType = AxisType.Primary;
            seria.ChartType = SeriesChartType.Line;
            seria.EmptyPointStyle.Color = Color.Transparent;
            seria.Color = dataStyles.GageHeightLine.color;

            chart.ChartAreas[0].Axes[1].Tag = var.NameRus;

            // DATA BIND
            DateTime itrDate = chartOptions.DataFilter.DateTimePeriod.DateS.Value;
            for (int i = 0; i < datas.Count; ++i)
            {
                DataPoint point = new DataPoint();
                DateTime date = datas[i].Date(chartOptions.TimeType);
                bool isEmpty = (daily ? (date - itrDate).TotalDays : (date - itrDate).TotalHours) >= 1;
                itrDate = daily ? itrDate.AddDays((int)(date - itrDate).TotalDays + 1) : itrDate.AddHours((int)(date - itrDate).TotalHours + 1);
                point.SetValueXY(date, datas[i].Value);
                point.IsEmpty = isEmpty;
                point.SetCustomProperty("id", datas[i].Id.ToString());
                point.ToolTip = string.Format("Время наблюдения: {0}\nЗначение: {1}", date.ToString("dd.MM.yy HH:mm"), datas[i].Value);

                dataStyles.GageHeightDot style;
                style = stylesByAQC.TryGetValue(datas[i].FlagAQC, out style) ? style : stylesByAQC[(byte)EnumFlagAQC.Deleted];
                point.MarkerColor = style.color;
                point.MarkerSize = style.size;
                point.MarkerStyle = isEmpty ? MarkerStyle.None : style.style;
                seria.Points.Add(point);

                max = Math.Max(max, datas[i].Value);
                min = Math.Min(min, datas[i].Value);
            }
            // SHOW SERIA
            chart.Series.Add(seria);
        }

        private void SetAxesTitles()
        {
            foreach (var item in chart.ChartAreas[0].Axes)
            {
                item.Title = _EnableAxesTitle ? (item.Tag != null ? item.Tag.ToString() : "НЕТ ПОДПИСИ") : string.Empty;
            }

        }

        public SeriesCollection GetSeriesCollection()
        {
            return chart.Series;
        }
        public void Clear()
        {
            chart.Series.Clear();
            chart.ChartAreas.Clear();
            chart.ChartAreas.Add(new System.Windows.Forms.DataVisualization.Charting.ChartArea());
        }

        private void refreshToolStripButton_Click(object sender, EventArgs e)
        {
            RaiseRefreshEvent(chartOptions);
        }

        public delegate void UCRefreshEventHandler(ChartOptions chartOptions);
        public event UCRefreshEventHandler UCRefreshEvent;
        protected virtual void RaiseRefreshEvent(ChartOptions chartOptions)
        {
            if (UCRefreshEvent != null)
            {
                UCRefreshEvent(chartOptions);
            }
        }

        private void legendEnabledToolStripButton_Click(object sender, EventArgs e)
        {
            EnableLegendRevert();
        }

        bool _EnableAxesTitle = true;
        /// <summary>
        /// Отображение подписей осей графика.
        /// </summary>
        public bool EnableAxesTitle
        {
            get { return _EnableAxesTitle; }
            set
            {
                _EnableAxesTitle = value;
                SetAxesTitles();
            }
        }
        public void EnableLegendRevert()
        {
            foreach (var item in chart.Legends)
            {
                item.Enabled = !item.Enabled;
            }
        }
        private void axesTitleEnableToolStripButton_Click(object sender, EventArgs e)
        {
            EnableAxesTitle = !EnableAxesTitle;
        }

        private void showPointLabelsToolStripButton_Click(object sender, EventArgs e)
        {
            EnablePointLabels = !EnablePointLabels;
        }
        bool _EnablePointLabels;
        public bool EnablePointLabels
        {
            get { return _EnablePointLabels; }
            set
            {
                _EnablePointLabels = value;
                foreach (var item in chart.Series)
                {
                    item.IsValueShownAsLabel = value;
                }
            }
        }

        private void chart_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //ZoomToggle(false);
        }

        private void chart_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                HitTestResult result = chart.HitTest(e.X, e.Y);
                if (result.ChartElementType == ChartElementType.DataPoint && result.Series.Name.Contains("Уровень воды"))
                {
                    DataPoint point = result.Series.Points[result.PointIndex];
                    if (point.IsEmpty)
                        return;
                    int id = Int32.Parse(result.Series.Points[result.PointIndex].GetCustomProperty("id"));
                    DataValue dataValue = data.First(x => x.Id == id);

                    new FormSingleUC(new UCDataValue(userOrganisationId, dataValue), "Редактирование данных").Show();
                }
                else
                    contextMenuStrip1.Show(this, new System.Drawing.Point(e.X, e.Y));
            }
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "png files (*.png)|*.png";
            sfd.FilterIndex = 2;
            sfd.RestoreDirectory = true;
            sfd.FileName = "Graph.png";
            if (sfd.ShowDialog() == DialogResult.OK)
                chart.SaveImage(sfd.FileName, ChartImageFormat.Png);
        }

        private void clipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                this.chart.SaveImage(ms, ChartImageFormat.Bmp);
                Bitmap bm = new Bitmap(ms);
                Clipboard.SetImage(bm);
            }
        }

        private void unFoldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UCChartHydro uc = new UCChartHydro(userOrganisationId, chartOptions);
            uc.title = title;
            uc.ToolbarVisible = false;
            uc.EnableAxesTitle = false;
            uc.EnableLegendRevert();
            uc.contextMenuStrip1.Items.Find("unFoldToolStripMenuItem", false)[0].Enabled = false;
            uc.contextMenuStrip1.Items.Find("deleteToolStripMenuItem", false)[0].Enabled = false;
            new FormSingleUC(uc, title).Show();
            uc.UpdateData(data);
        }

        private void resetZoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chart.ChartAreas[0].AxisX.ScaleView.ZoomReset(0);
            chart.ChartAreas[0].AxisY.ScaleView.ZoomReset(0);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateData();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Drawing.Printing.PrintDocument pd = new System.Drawing.Printing.PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
            pd.Print();
        }

        private void pd_PrintPage(object o, PrintPageEventArgs e)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                this.chart.SaveImage(ms, ChartImageFormat.Bmp);
                System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                e.Graphics.DrawImage(img, new System.Drawing.Point(100, 100));
            }
        }
    }
}
