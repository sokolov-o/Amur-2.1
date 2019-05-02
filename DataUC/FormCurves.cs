using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using SOV.Amur.Meta;
using System.Linq;

namespace SOV.Amur.Data
{
    public partial class FormCurves : Form
    {
        List<Curve> _allCurves = null;
        List<Catalog> _allCatalogs = null;

        DateTime[] _dateSAllPeriod = new DateTime[] { new DateTime(1963, 9, 7), DateTime.Now.AddYears(1) };

        public FormCurves()
        {
            InitializeComponent();

            // Initialize toolbar

            _allCurves = DataManager.GetInstance().CurveRepository.SelectAllCurvesNoSeries(_dateSAllPeriod[0], _dateSAllPeriod[1]);

            List<int> ids = _allCurves.Select(x => x.CatalogIdX)
                .Union(_allCurves.Select(x => x.CatalogIdY))
                .Distinct()
                .ToList();
            _allCatalogs = Meta.DataManager.GetInstance().CatalogRepository.Select(ids);

            ids = _allCatalogs.Select(x => x.VariableId).Distinct().ToList();
            Variable[] vars = Meta.DataManager.GetInstance().VariableRepository.Select(ids).OrderBy(x => x.NameRus).ToArray();

            varXCB.Items.AddRange(vars);
            varYCB.Items.AddRange(vars);
            varXCB.SelectedItem = vars.First(x => x.Id == (int)EnumVariable.GageHeightF);
            varYCB.SelectedItem = vars.First(x => x.Id == (int)EnumVariable.Discharge);

            dateSTextBox.Text = (new DateTime(DateTime.Today.Year - 2, 1, 1)).ToShortDateString();

            xyyxCB.Items.AddRange(new string[] { "X-Y", "Y-X" });
            xyyxCB.SelectedIndex = 0;

            AcceptProperties();
        }

        Variable VarX { get { return (Variable)varXCB.SelectedItem; } }
        Variable VarY { get { return (Variable)varYCB.SelectedItem; } }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            tv.Nodes.Clear();

            // GET SITES

            List<Catalog> catalogXs = _allCatalogs.Where(x => x.VariableId == VarX.Id).ToList();
            List<Catalog> catalogYs = _allCatalogs.Where(x => x.VariableId == VarY.Id).ToList();

            List<Curve> curves = _allCurves.Where(x =>
            catalogXs.Exists(y => y.Id == x.CatalogIdX) && catalogYs.Exists(y => y.Id == x.CatalogIdY)).ToList();
            if (curves.Count == 0)
            {
                TreeNode node = new TreeNode("Кривые для выбранных параметров отсутствуют в базе данных.");
                node.ForeColor = System.Drawing.Color.Red;
                tv.Nodes.Add(node);
                return;
            }

            List<Site> sites = Meta.DataManager.GetInstance().SiteRepository.Select(catalogXs.Select(x => x.SiteId).ToList());
            catalogXs = catalogXs
                .OrderBy(x => sites.First(y => y.Id == x.SiteId).GetName(2, SiteTypeRepository.GetCash()))
                .ToList();

            List<Curve.Seria> series = DataManager.GetInstance().CurveSeriaRepository.SelectSeries4CurveIds(
                curves.Select(x => x.Id).ToList(),
                null, // all seria type
                DateTime.Parse(dateSTextBox.Text), _dateSAllPeriod[1],
                false // headers only
            );

            // FILL TREE
            List<TreeNode> nodes = new List<TreeNode>();

            foreach (var curve in curves.OrderBy(x => sites.First(y => y.Id == catalogXs.Find(z => z.Id == x.CatalogIdX).SiteId).GetName(2, SiteTypeRepository.GetCash())))
            {
                Catalog catalog = _allCatalogs.First(x => x.Id == curve.CatalogIdX);

                // NODES 0 - SITES
                TreeNode nodeSite = new TreeNode();
                nodeSite.Text = sites.Find(x => x.Id == catalog.SiteId).GetName(2, SiteTypeRepository.GetCash());
                nodeSite.Tag = catalog;

                // NODES 1 - SITE SERIES (DATETIME'S)
                foreach (var seria in series.Where(x => x.CurveId == curve.Id).OrderByDescending(x => x.DateS))
                {
                    TreeNode nodeSeriaDate = new TreeNode();
                    nodeSeriaDate.Text = seria.DateS.ToShortDateString();
                    nodeSeriaDate.Tag = seria;
                    nodeSite.Nodes.Add(nodeSeriaDate);
                }
                nodes.Add(nodeSite);
            }
            tv.Nodes.AddRange(nodes.ToArray());
            infoLabel.Text = tv.Nodes.Count.ToString();

            AcceptProperties();
        }
        private void tv_AfterSelect(object sender, TreeViewEventArgs e)
        {
            NodeChanged(e.Node);
        }
        private void NodeChanged(TreeNode node)
        {
            if (node == null) return;

            chart.Series.Clear();
            curveDescriptionTextBox.Text = seriaDescriptionTextBox.Text = null;
            List<Curve.Seria> curveSeries = new List<Curve.Seria>();

            // CURVE DATETIME NODE SELECTED
            if (node.Tag.GetType() == typeof(Curve.Seria))
            {
                curveSeries = DataManager.GetInstance().CurveSeriaRepository.SelectSeries(
                    new List<int>() { ((Curve.Seria)node.Tag).Id },
                    false
                );

                pointsBindingSource.DataSource = curveSeries[0].Points.OrderBy(x => x.X);
                coefsBindingSource.DataSource = curveSeries[0].Coefs != null ? curveSeries[0].Coefs.OrderBy(x => x.Month).ToList() : curveSeries[0].Coefs;
                splitContainer2.Panel1Collapsed = splitContainer3.Panel1Collapsed = false;

                seriaDescriptionTextBox.Text = curveSeries[0].Description;
                curveDescriptionTextBox.Text = _allCurves.Find(x => x.Id == ((Curve.Seria)node.Tag).CurveId).Description;

                AcceptProperties();
            }

            // SITE NODE SELECTED
            if (node.Tag.GetType() == typeof(Catalog))
            {
                curveSeries = DataManager.GetInstance().CurveSeriaRepository.SelectSeries4CurveIds(
                    _allCurves.Where(x => x.CatalogIdX == ((Catalog)node.Tag).Id).Select(x => x.Id).ToList(),
                    null, // all seria type
                    DateTime.Parse(dateSTextBox.Text), _dateSAllPeriod[1],
                    false // all seria data
                );

                pointsBindingSource.DataSource = null;
                coefsBindingSource.DataSource = null;
                splitContainer2.Panel1Collapsed = splitContainer3.Panel1Collapsed = true;

                splitContainer5.Panel2Collapsed = true;

                curveDescriptionTextBox.Text = _allCurves.Find(x => x.CatalogIdX == ((Catalog)node.Tag).Id).Description;
            }

            // ADD CHART SERIES

            chart.Legends[0].Docking = Docking.Bottom;
            chart.ChartAreas[0].AxisX.Title = xyyxCB.SelectedItem.ToString() == "x-y" ? VarX.NameRus : VarY.NameRus;
            chart.ChartAreas[0].AxisY.Title = xyyxCB.SelectedItem.ToString() == "x-y" ? VarY.NameRus : VarX.NameRus;

            foreach (var curveSeria in curveSeries)
            {
                Curve.Seria.Point.AcceptXYFormat("F" + digitsAfterCommaTextBox, curveSeria.Points);

                Series chartSeria = new Series();
                chartSeria.ChartType = SeriesChartType.Point;
                chartSeria.LegendText = (node.Parent != null ? node.Parent.Text + " - " : "") + curveSeria.DateS.ToShortDateString();

                string[] xy = xyyxCB.SelectedItem.ToString().Split('-');
                chartSeria.Points.DataBind(curveSeria.Points, xy[0], xy[1], "Tooltip=Name{}");

                chart.Series.Add(chartSeria);
            }
        }

        private void xyyxCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            NodeChanged(tv.SelectedNode);
        }
        FormCurveProperties _formCurveProperties = new FormCurveProperties();
        private void propertiesButton_Click(object sender, EventArgs e)
        {
            _formCurveProperties.StartPosition = FormStartPosition.CenterScreen;
            if (_formCurveProperties.ShowDialog() == DialogResult.OK)
            {
                AcceptProperties();
            }
        }

        private void AcceptProperties()
        {
            splitContainer4.Panel2Collapsed = !_formCurveProperties.ShowCurveDescription;
            splitContainer5.Panel2Collapsed = !_formCurveProperties.ShowSeriaDescription;
        }
    }
}
