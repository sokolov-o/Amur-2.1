namespace SOV.Amur.Data.Chart
{
    partial class UCChartTrend
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCChartTrend));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.XAxisButton = new System.Windows.Forms.ToolStripButton();
            this.swapAsixButton = new System.Windows.Forms.ToolStripButton();
            this.YAxisButton = new System.Windows.Forms.ToolStripButton();
            this.refreshButton = new System.Windows.Forms.ToolStripButton();
            this.dateLabel = new System.Windows.Forms.ToolStripLabel();
            this.timeTypeComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.dateButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.offsetTypeComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.leftOffsetButton = new System.Windows.Forms.ToolStripButton();
            this.offsetTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.rightOffsetButton = new System.Windows.Forms.ToolStripButton();
            this.offsetCaptionLabel = new System.Windows.Forms.ToolStripLabel();
            this.offsetValueLabel = new System.Windows.Forms.ToolStripLabel();
            this.offsetCamcelButton = new System.Windows.Forms.ToolStripButton();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.XAxisButton,
            this.swapAsixButton,
            this.YAxisButton,
            this.refreshButton,
            this.dateLabel,
            this.timeTypeComboBox,
            this.dateButton,
            this.toolStripSeparator1,
            this.offsetTypeComboBox,
            this.leftOffsetButton,
            this.offsetTextBox,
            this.rightOffsetButton,
            this.offsetCamcelButton,
            this.offsetCaptionLabel,
            this.offsetValueLabel});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(684, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // XAxisButton
            // 
            this.XAxisButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.XAxisButton.Image = global::SOV.Amur.Data.Properties.Resources.X_letter;
            this.XAxisButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.XAxisButton.Name = "XAxisButton";
            this.XAxisButton.Size = new System.Drawing.Size(23, 22);
            this.XAxisButton.Tag = "0";
            this.XAxisButton.Text = "Добавить";
            this.XAxisButton.ToolTipText = "Ось X";
            this.XAxisButton.Click += new System.EventHandler(this.AxisButton_Click);
            // 
            // swapAsixButton
            // 
            this.swapAsixButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.swapAsixButton.Image = global::SOV.Amur.Data.Properties.Resources.two_side_arrow;
            this.swapAsixButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.swapAsixButton.Name = "swapAsixButton";
            this.swapAsixButton.Size = new System.Drawing.Size(23, 22);
            this.swapAsixButton.Text = "Поменять оси";
            this.swapAsixButton.ToolTipText = "Поменять оси";
            this.swapAsixButton.Click += new System.EventHandler(this.SwapAsixButton_Click);
            // 
            // YAxisButton
            // 
            this.YAxisButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.YAxisButton.Image = global::SOV.Amur.Data.Properties.Resources.Y_letter;
            this.YAxisButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.YAxisButton.Name = "YAxisButton";
            this.YAxisButton.Size = new System.Drawing.Size(23, 22);
            this.YAxisButton.Tag = "1";
            this.YAxisButton.Text = "Добавить";
            this.YAxisButton.ToolTipText = "Ось Y";
            this.YAxisButton.Click += new System.EventHandler(this.AxisButton_Click);
            // 
            // refreshButton
            // 
            this.refreshButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.refreshButton.Image = global::SOV.Amur.Data.Properties.Resources.refresh_16xLG;
            this.refreshButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(23, 22);
            this.refreshButton.Text = "Обновить";
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // dateLabel
            // 
            this.dateLabel.Name = "dateLabel";
            this.dateLabel.Size = new System.Drawing.Size(0, 22);
            // 
            // timeTypeComboBox
            // 
            this.timeTypeComboBox.Name = "timeTypeComboBox";
            this.timeTypeComboBox.Size = new System.Drawing.Size(75, 25);
            // 
            // dateButton
            // 
            this.dateButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.dateButton.Image = global::SOV.Amur.Data.Properties.Resources.calendar_16xLG;
            this.dateButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.dateButton.Name = "dateButton";
            this.dateButton.Size = new System.Drawing.Size(23, 22);
            this.dateButton.Text = "toolStripButton1";
            this.dateButton.ToolTipText = "Временной диапозон";
            this.dateButton.Click += new System.EventHandler(this.dateButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // offsetTypeComboBox
            // 
            this.offsetTypeComboBox.Items.AddRange(new object[] {
            "Час",
            "День",
            "Неделя"});
            this.offsetTypeComboBox.Margin = new System.Windows.Forms.Padding(5, 0, 1, 0);
            this.offsetTypeComboBox.Name = "offsetTypeComboBox";
            this.offsetTypeComboBox.Size = new System.Drawing.Size(90, 25);
            this.offsetTypeComboBox.ToolTipText = "Единица смещения";
            // 
            // leftOffsetButton
            // 
            this.leftOffsetButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.leftOffsetButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.leftOffsetButton.Name = "leftOffsetButton";
            this.leftOffsetButton.Size = new System.Drawing.Size(23, 22);
            this.leftOffsetButton.Tag = "-1";
            this.leftOffsetButton.Text = "<";
            this.leftOffsetButton.ToolTipText = "Отрицательное смещение";
            this.leftOffsetButton.Click += new System.EventHandler(this.changeOffsetClick);
            // 
            // offsetTextBox
            // 
            this.offsetTextBox.Name = "offsetTextBox";
            this.offsetTextBox.Size = new System.Drawing.Size(30, 25);
            this.offsetTextBox.ToolTipText = "Величина смещения";
            // 
            // rightOffsetButton
            // 
            this.rightOffsetButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.rightOffsetButton.Image = ((System.Drawing.Image)(resources.GetObject("rightOffsetButton.Image")));
            this.rightOffsetButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.rightOffsetButton.Name = "rightOffsetButton";
            this.rightOffsetButton.Size = new System.Drawing.Size(23, 22);
            this.rightOffsetButton.Tag = "1";
            this.rightOffsetButton.Text = ">";
            this.rightOffsetButton.ToolTipText = "Положительное смещение";
            this.rightOffsetButton.Click += new System.EventHandler(this.changeOffsetClick);
            // 
            // offsetCaptionLabel
            // 
            this.offsetCaptionLabel.Name = "offsetCaptionLabel";
            this.offsetCaptionLabel.Size = new System.Drawing.Size(73, 22);
            this.offsetCaptionLabel.Text = "Смещение: ";
            // 
            // offsetValueLabel
            // 
            this.offsetValueLabel.Name = "offsetValueLabel";
            this.offsetValueLabel.Size = new System.Drawing.Size(26, 22);
            this.offsetValueLabel.Text = "0 ч.";
            // 
            // offsetCamcelButton
            // 
            this.offsetCamcelButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.offsetCamcelButton.Image = global::SOV.Amur.Data.Properties.Resources.DeleteHS;
            this.offsetCamcelButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.offsetCamcelButton.Name = "offsetCamcelButton";
            this.offsetCamcelButton.Size = new System.Drawing.Size(23, 22);
            this.offsetCamcelButton.Text = "toolStripButton1";
            this.offsetCamcelButton.ToolTipText = "Сбросить смещение";
            this.offsetCamcelButton.Click += new System.EventHandler(this.offsetCamcelButton_Click);
            // 
            // chart
            // 
            this.chart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.chart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart.Legends.Add(legend1);
            this.chart.Location = new System.Drawing.Point(0, 28);
            this.chart.Name = "chart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart.Series.Add(series1);
            this.chart.Size = new System.Drawing.Size(684, 321);
            this.chart.TabIndex = 3;
            this.chart.Text = "chart1";
            this.chart.MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart_MouseUp);
            // 
            // UCChartTrend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chart);
            this.Controls.Add(this.toolStrip1);
            this.Name = "UCChartTrend";
            this.Size = new System.Drawing.Size(684, 349);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton YAxisButton;
        private System.Windows.Forms.ToolStripButton refreshButton;
        private System.Windows.Forms.ToolStripLabel dateLabel;
        private System.Windows.Forms.ToolStripComboBox timeTypeComboBox;
        private System.Windows.Forms.ToolStripButton dateButton;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.ToolStripButton XAxisButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel offsetCaptionLabel;
        private System.Windows.Forms.ToolStripLabel offsetValueLabel;
        private System.Windows.Forms.ToolStripComboBox offsetTypeComboBox;
        private System.Windows.Forms.ToolStripButton leftOffsetButton;
        private System.Windows.Forms.ToolStripTextBox offsetTextBox;
        private System.Windows.Forms.ToolStripButton rightOffsetButton;
        private System.Windows.Forms.ToolStripButton swapAsixButton;
        private System.Windows.Forms.ToolStripButton offsetCamcelButton;
    }
}
