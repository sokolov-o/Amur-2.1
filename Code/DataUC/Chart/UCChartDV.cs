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
using SOV.Amur.Meta;

namespace SOV.Amur.Data
{
    /// <summary>
    /// Графики: x - дата, y - значения.
    /// </summary>
    public partial class UCChartDV : UserControl
    {
        public UCChartDV()
        {
            InitializeComponent();

            Clear();
            chart.ChartAreas[0].AxisX.Title = "Дата";
        }

        private void deleteAllToolStripButton_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void AddSeria(string seriaName, List<DataValue> dvs, EnumDateType useTimeType, object seriaTag,
            SeriesChartType chartType = SeriesChartType.Line, AxisType xAxisType = AxisType.Primary, AxisType yAxisType = AxisType.Primary)
        {
            try
            {
                // CREATE SERIA
                Series seria = new Series(seriaName);
                seria.Tag = new object[] { seriaTag, dvs };

                // SERIA DESIGN
                seria.XAxisType = xAxisType;
                seria.YAxisType = yAxisType;
                seria.ChartType = chartType;

                // CHART DESIGN
                if (seria.YAxisType == AxisType.Primary && string.IsNullOrEmpty(chart.ChartAreas[0].AxisY.Title))
                    chart.ChartAreas[0].AxisY.Title = seriaName;
                if (seria.YAxisType == AxisType.Secondary && string.IsNullOrEmpty(chart.ChartAreas[0].AxisY2.Title))
                    chart.ChartAreas[0].AxisY2.Title = seriaName;

                // DATA BIND
                string dateFieldName = useTimeType == EnumDateType.UTC ? "DateUTC" : "DateLOC";
                seria.Points.DataBind(dvs, dateFieldName, "Value", "Tooltip=" + dateFieldName + "{dd.MM.yyyy HH}");
                seria.MarkerStyle = MarkerStyle.Circle;

                // SHOW SERIA
                chart.Series.Add(seria);
                chart.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public SeriesCollection GetSeriesCollection()
        {
            return chart.Series;
        }
        public void Clear()
        {
            chart.Series.Clear();
        }

    }
}
