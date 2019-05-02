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

namespace FERHRI.Amur.Data
{
    public enum ChartViewType
    {
        Double_DateTime = 1
    }
    public partial class UCChart : UserControl
    {
        public ChartViewType ChartViewType { get; set; }

        public UCChart()
        {
            InitializeComponent();
            Clear();
        }

        private void deleteAllToolStripButton_Click(object sender, EventArgs e)
        {
            Clear();
        }
        bool SetChartViewType(ChartViewType cvt)
        {
            if (chart.Series.Count == 0)
            {
                ChartViewType = cvt;
                chart.ChartAreas[0].AxisX.Title = "Дата";
                chart.ChartAreas[0].AxisY.Title = "Значение";
            }
            else if (cvt != ChartViewType)
            {
                MessageBox.Show("Указанный тип графика не соответствует типу контейнера графиков.");
                return false;
            }
            return true;
        }
        public void AddSeria(string seriaName, List<DateTime> x, List<double> y, List<DataValue> dvs)
        {
            if (!SetChartViewType(ChartViewType.Double_DateTime)) return;

            try
            {
                chart.Series.Add(seriaName);
                var seria = chart.Series[chart.Series.Count - 1];
                seria.Tag = dvs;

                seria.XAxisType = AxisType.Primary;
                seria.XValueType = ChartValueType.DateTime;
                seria.YAxisType = AxisType.Primary;
                seria.YValueType = ChartValueType.Double;
                seria.ChartType = SeriesChartType.Line;

                for (int i = 0; i < x.Count; i++)
                {
                    seria.Points.DataBindXY(x, "это X", y, "это Y");
                    //seria.Points.Add(new DataPoint(x[i].ToBinary(), y[i]));
                    //seria.Points[i].Tag = dvs[i];
                }
                chart.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Clear()
        {
            chart.Series.Clear();
        }

    }
}
