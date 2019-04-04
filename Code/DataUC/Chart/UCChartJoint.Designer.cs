namespace SOV.Amur.Data.Chart
{
    partial class UCChartJoint
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.siteGroupComboBox = new SOV.Amur.Meta.SiteGroupComboBox();
            this.addSiteButton = new System.Windows.Forms.ToolStripButton();
            this.refreshButton = new System.Windows.Forms.ToolStripButton();
            this.dateLabel = new System.Windows.Forms.ToolStripLabel();
            this.timeTypeComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.dateButton = new System.Windows.Forms.ToolStripButton();
            this.viewSplitButton = new System.Windows.Forms.ToolStripSplitButton();
            this.jointMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.separateMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trendToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.controlsPanel = new System.Windows.Forms.Panel();
            this.infoPanel = new System.Windows.Forms.Panel();
            this.dateInfolabel = new System.Windows.Forms.Label();
            this.infoLabel = new System.Windows.Forms.Label();
            this.infoDataPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.offsetPanel = new System.Windows.Forms.Panel();
            this.offsetSite = new System.Windows.Forms.ComboBox();
            this.offsetLabel = new System.Windows.Forms.Label();
            this.offsetTypeComboBox = new System.Windows.Forms.ComboBox();
            this.offsetCountUpDown = new System.Windows.Forms.NumericUpDown();
            this.incButton = new System.Windows.Forms.Button();
            this.decButton = new System.Windows.Forms.Button();
            this.chooseActionPanel = new System.Windows.Forms.Panel();
            this.twoPointsActionRadioButton = new System.Windows.Forms.RadioButton();
            this.chooseActionLabel = new System.Windows.Forms.Label();
            this.pointActionRadioButton = new System.Windows.Forms.RadioButton();
            this.plotActionRadioButton = new System.Windows.Forms.RadioButton();
            this.activeSiteLabel = new System.Windows.Forms.Label();
            this.fixedSiteLabel = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.varToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ucVariablesList = new SOV.Amur.Meta.UCVariablesList();
            this.siteLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.controlsPanel.SuspendLayout();
            this.infoPanel.SuspendLayout();
            this.offsetPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.offsetCountUpDown)).BeginInit();
            this.chooseActionPanel.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart
            // 
            this.chart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea3.Name = "ChartArea1";
            this.chart.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chart.Legends.Add(legend3);
            this.chart.Location = new System.Drawing.Point(3, 25);
            this.chart.Name = "chart";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.chart.Series.Add(series3);
            this.chart.Size = new System.Drawing.Size(474, 352);
            this.chart.TabIndex = 0;
            this.chart.Text = "chart1";
            this.chart.MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart_MouseUp);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.siteGroupComboBox,
            this.addSiteButton,
            this.refreshButton,
            this.dateLabel,
            this.timeTypeComboBox,
            this.dateButton,
            this.viewSplitButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(577, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // siteGroupComboBox
            // 
            this.siteGroupComboBox.Name = "siteGroupComboBox";
            this.siteGroupComboBox.SiteGroup = null;
            this.siteGroupComboBox.Size = new System.Drawing.Size(121, 25);
            this.siteGroupComboBox.SelectedIndexChanged += new System.EventHandler(this.siteGroupComboBox_SelectedIndexChanged);
            // 
            // addSiteButton
            // 
            this.addSiteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addSiteButton.Image = global::SOV.Amur.Data.Properties.Resources.action_add_16xLG;
            this.addSiteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addSiteButton.Name = "addSiteButton";
            this.addSiteButton.Size = new System.Drawing.Size(23, 22);
            this.addSiteButton.Text = "Добавить";
            this.addSiteButton.Click += new System.EventHandler(this.addSite_Click);
            // 
            // refreshButton
            // 
            this.refreshButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.refreshButton.Image = global::SOV.Amur.Data.Properties.Resources.refresh_16xLG;
            this.refreshButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(23, 22);
            this.refreshButton.Text = "Обновить";
            this.refreshButton.Click += new System.EventHandler(this.refresh_Click);
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
            this.dateButton.Click += new System.EventHandler(this.dateButton_Click);
            // 
            // viewSplitButton
            // 
            this.viewSplitButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.viewSplitButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.jointMenuItem,
            this.separateMenuItem,
            this.trendToolStripMenuItem});
            this.viewSplitButton.Image = global::SOV.Amur.Data.Properties.Resources.Property_501;
            this.viewSplitButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.viewSplitButton.Name = "viewSplitButton";
            this.viewSplitButton.Size = new System.Drawing.Size(32, 22);
            this.viewSplitButton.Text = "toolStripSplitButton1";
            // 
            // jointMenuItem
            // 
            this.jointMenuItem.Name = "jointMenuItem";
            this.jointMenuItem.Size = new System.Drawing.Size(144, 22);
            this.jointMenuItem.Text = "Совместный";
            this.jointMenuItem.Click += new System.EventHandler(this.jointMenuItem_Click);
            // 
            // separateMenuItem
            // 
            this.separateMenuItem.Enabled = false;
            this.separateMenuItem.Name = "separateMenuItem";
            this.separateMenuItem.Size = new System.Drawing.Size(144, 22);
            this.separateMenuItem.Text = "Раздельный";
            this.separateMenuItem.Click += new System.EventHandler(this.separateMenuItem_Click);
            // 
            // trendToolStripMenuItem
            // 
            this.trendToolStripMenuItem.Name = "trendToolStripMenuItem";
            this.trendToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.trendToolStripMenuItem.Text = "Тренд";
            this.trendToolStripMenuItem.Click += new System.EventHandler(this.trendToolStripMenuItem_Click);
            // 
            // controlsPanel
            // 
            this.controlsPanel.Controls.Add(this.infoPanel);
            this.controlsPanel.Controls.Add(this.offsetPanel);
            this.controlsPanel.Controls.Add(this.chooseActionPanel);
            this.controlsPanel.Controls.Add(this.activeSiteLabel);
            this.controlsPanel.Controls.Add(this.fixedSiteLabel);
            this.controlsPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.controlsPanel.Location = new System.Drawing.Point(476, 25);
            this.controlsPanel.Name = "controlsPanel";
            this.controlsPanel.Size = new System.Drawing.Size(101, 352);
            this.controlsPanel.TabIndex = 2;
            // 
            // infoPanel
            // 
            this.infoPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.infoPanel.Controls.Add(this.dateInfolabel);
            this.infoPanel.Controls.Add(this.infoLabel);
            this.infoPanel.Controls.Add(this.infoDataPanel);
            this.infoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoPanel.Location = new System.Drawing.Point(0, 224);
            this.infoPanel.Name = "infoPanel";
            this.infoPanel.Size = new System.Drawing.Size(101, 128);
            this.infoPanel.TabIndex = 12;
            // 
            // dateInfolabel
            // 
            this.dateInfolabel.AutoSize = true;
            this.dateInfolabel.Location = new System.Drawing.Point(5, 19);
            this.dateInfolabel.Name = "dateInfolabel";
            this.dateInfolabel.Size = new System.Drawing.Size(0, 13);
            this.dateInfolabel.TabIndex = 13;
            // 
            // infoLabel
            // 
            this.infoLabel.AutoSize = true;
            this.infoLabel.Location = new System.Drawing.Point(5, 5);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(70, 13);
            this.infoLabel.TabIndex = 12;
            this.infoLabel.Text = "Разность на";
            // 
            // infoDataPanel
            // 
            this.infoDataPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.infoDataPanel.AutoScroll = true;
            this.infoDataPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.infoDataPanel.Location = new System.Drawing.Point(-2, 41);
            this.infoDataPanel.Name = "infoDataPanel";
            this.infoDataPanel.Size = new System.Drawing.Size(101, 84);
            this.infoDataPanel.TabIndex = 11;
            // 
            // offsetPanel
            // 
            this.offsetPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.offsetPanel.Controls.Add(this.siteLabel);
            this.offsetPanel.Controls.Add(this.offsetSite);
            this.offsetPanel.Controls.Add(this.offsetLabel);
            this.offsetPanel.Controls.Add(this.offsetTypeComboBox);
            this.offsetPanel.Controls.Add(this.offsetCountUpDown);
            this.offsetPanel.Controls.Add(this.incButton);
            this.offsetPanel.Controls.Add(this.decButton);
            this.offsetPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.offsetPanel.Location = new System.Drawing.Point(0, 100);
            this.offsetPanel.Name = "offsetPanel";
            this.offsetPanel.Size = new System.Drawing.Size(101, 124);
            this.offsetPanel.TabIndex = 6;
            // 
            // offsetSite
            // 
            this.offsetSite.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.offsetSite.FormattingEnabled = true;
            this.offsetSite.Location = new System.Drawing.Point(6, 19);
            this.offsetSite.Name = "offsetSite";
            this.offsetSite.Size = new System.Drawing.Size(88, 21);
            this.offsetSite.TabIndex = 7;
            this.offsetSite.SelectedIndexChanged += new System.EventHandler(this.offsetSite_SelectedIndexChanged);
            // 
            // offsetLabel
            // 
            this.offsetLabel.AutoSize = true;
            this.offsetLabel.Location = new System.Drawing.Point(5, 47);
            this.offsetLabel.Name = "offsetLabel";
            this.offsetLabel.Size = new System.Drawing.Size(61, 13);
            this.offsetLabel.TabIndex = 6;
            this.offsetLabel.Text = "Смещение";
            // 
            // offsetTypeComboBox
            // 
            this.offsetTypeComboBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.offsetTypeComboBox.FormattingEnabled = true;
            this.offsetTypeComboBox.Items.AddRange(new object[] {
            "Час",
            "День"});
            this.offsetTypeComboBox.Location = new System.Drawing.Point(6, 65);
            this.offsetTypeComboBox.Name = "offsetTypeComboBox";
            this.offsetTypeComboBox.Size = new System.Drawing.Size(88, 21);
            this.offsetTypeComboBox.TabIndex = 4;
            // 
            // offsetCountUpDown
            // 
            this.offsetCountUpDown.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.offsetCountUpDown.Location = new System.Drawing.Point(34, 93);
            this.offsetCountUpDown.Name = "offsetCountUpDown";
            this.offsetCountUpDown.Size = new System.Drawing.Size(32, 20);
            this.offsetCountUpDown.TabIndex = 5;
            this.offsetCountUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // incButton
            // 
            this.incButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.incButton.Location = new System.Drawing.Point(69, 93);
            this.incButton.Name = "incButton";
            this.incButton.Size = new System.Drawing.Size(25, 20);
            this.incButton.TabIndex = 0;
            this.incButton.Tag = "1";
            this.incButton.Text = ">";
            this.incButton.UseVisualStyleBackColor = true;
            this.incButton.Click += new System.EventHandler(this.ChangeOffsetClick);
            // 
            // decButton
            // 
            this.decButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.decButton.Location = new System.Drawing.Point(5, 93);
            this.decButton.Name = "decButton";
            this.decButton.Size = new System.Drawing.Size(25, 20);
            this.decButton.TabIndex = 1;
            this.decButton.Tag = "-1";
            this.decButton.Text = "<";
            this.decButton.UseVisualStyleBackColor = true;
            this.decButton.Click += new System.EventHandler(this.ChangeOffsetClick);
            // 
            // chooseActionPanel
            // 
            this.chooseActionPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.chooseActionPanel.Controls.Add(this.twoPointsActionRadioButton);
            this.chooseActionPanel.Controls.Add(this.chooseActionLabel);
            this.chooseActionPanel.Controls.Add(this.pointActionRadioButton);
            this.chooseActionPanel.Controls.Add(this.plotActionRadioButton);
            this.chooseActionPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.chooseActionPanel.Location = new System.Drawing.Point(0, 0);
            this.chooseActionPanel.Name = "chooseActionPanel";
            this.chooseActionPanel.Size = new System.Drawing.Size(101, 100);
            this.chooseActionPanel.TabIndex = 10;
            // 
            // twoPointsActionRadioButton
            // 
            this.twoPointsActionRadioButton.AutoSize = true;
            this.twoPointsActionRadioButton.Enabled = false;
            this.twoPointsActionRadioButton.Location = new System.Drawing.Point(6, 76);
            this.twoPointsActionRadioButton.Name = "twoPointsActionRadioButton";
            this.twoPointsActionRadioButton.Size = new System.Drawing.Size(77, 17);
            this.twoPointsActionRadioButton.TabIndex = 11;
            this.twoPointsActionRadioButton.Text = "Две точки";
            this.twoPointsActionRadioButton.UseVisualStyleBackColor = true;
            this.twoPointsActionRadioButton.CheckedChanged += new System.EventHandler(this.actionRadioButton_CheckedChanged);
            // 
            // chooseActionLabel
            // 
            this.chooseActionLabel.AutoSize = true;
            this.chooseActionLabel.Location = new System.Drawing.Point(3, 5);
            this.chooseActionLabel.Name = "chooseActionLabel";
            this.chooseActionLabel.Size = new System.Drawing.Size(57, 26);
            this.chooseActionLabel.TabIndex = 10;
            this.chooseActionLabel.Text = "Активный\r\nобъект";
            // 
            // pointActionRadioButton
            // 
            this.pointActionRadioButton.AutoSize = true;
            this.pointActionRadioButton.Checked = true;
            this.pointActionRadioButton.Location = new System.Drawing.Point(6, 34);
            this.pointActionRadioButton.Name = "pointActionRadioButton";
            this.pointActionRadioButton.Size = new System.Drawing.Size(55, 17);
            this.pointActionRadioButton.TabIndex = 8;
            this.pointActionRadioButton.TabStop = true;
            this.pointActionRadioButton.Text = "Точка";
            this.pointActionRadioButton.UseVisualStyleBackColor = true;
            this.pointActionRadioButton.CheckedChanged += new System.EventHandler(this.actionRadioButton_CheckedChanged);
            // 
            // plotActionRadioButton
            // 
            this.plotActionRadioButton.AutoSize = true;
            this.plotActionRadioButton.Location = new System.Drawing.Point(6, 54);
            this.plotActionRadioButton.Name = "plotActionRadioButton";
            this.plotActionRadioButton.Size = new System.Drawing.Size(63, 17);
            this.plotActionRadioButton.TabIndex = 9;
            this.plotActionRadioButton.Text = "График";
            this.plotActionRadioButton.UseVisualStyleBackColor = true;
            this.plotActionRadioButton.CheckedChanged += new System.EventHandler(this.actionRadioButton_CheckedChanged);
            // 
            // activeSiteLabel
            // 
            this.activeSiteLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.activeSiteLabel.AutoSize = true;
            this.activeSiteLabel.Location = new System.Drawing.Point(110, 172);
            this.activeSiteLabel.Name = "activeSiteLabel";
            this.activeSiteLabel.Size = new System.Drawing.Size(0, 13);
            this.activeSiteLabel.TabIndex = 3;
            // 
            // fixedSiteLabel
            // 
            this.fixedSiteLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.fixedSiteLabel.AutoSize = true;
            this.fixedSiteLabel.Location = new System.Drawing.Point(151, 20);
            this.fixedSiteLabel.Name = "fixedSiteLabel";
            this.fixedSiteLabel.Size = new System.Drawing.Size(0, 13);
            this.fixedSiteLabel.TabIndex = 2;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.refreshToolStripMenuItem,
            this.varToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(144, 70);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.addToolStripMenuItem.Text = "Добавить";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addSite_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.refreshToolStripMenuItem.Text = "Обновить";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refresh_Click);
            // 
            // varToolStripMenuItem
            // 
            this.varToolStripMenuItem.Name = "varToolStripMenuItem";
            this.varToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.varToolStripMenuItem.Text = "Переменная";
            this.varToolStripMenuItem.Click += new System.EventHandler(this.varToolStripMenuItem_Click);
            // 
            // ucVariablesList
            // 
            this.ucVariablesList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucVariablesList.Location = new System.Drawing.Point(0, 25);
            this.ucVariablesList.Name = "ucVariablesList";
            this.ucVariablesList.ShowFilterButton = true;
            this.ucVariablesList.ShowToolBox = true;
            this.ucVariablesList.Size = new System.Drawing.Size(194, 354);
            this.ucVariablesList.TabIndex = 3;
            this.ucVariablesList.Visible = false;
            // 
            // siteLabel
            // 
            this.siteLabel.AutoSize = true;
            this.siteLabel.Location = new System.Drawing.Point(5, 3);
            this.siteLabel.Name = "siteLabel";
            this.siteLabel.Size = new System.Drawing.Size(31, 13);
            this.siteLabel.TabIndex = 8;
            this.siteLabel.Text = "Сайт";
            // 
            // UCChartJoint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ucVariablesList);
            this.Controls.Add(this.controlsPanel);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.chart);
            this.Name = "UCChartJoint";
            this.Size = new System.Drawing.Size(577, 377);
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.controlsPanel.ResumeLayout(false);
            this.controlsPanel.PerformLayout();
            this.infoPanel.ResumeLayout(false);
            this.infoPanel.PerformLayout();
            this.offsetPanel.ResumeLayout(false);
            this.offsetPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.offsetCountUpDown)).EndInit();
            this.chooseActionPanel.ResumeLayout(false);
            this.chooseActionPanel.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private Meta.SiteGroupComboBox siteGroupComboBox;
        private System.Windows.Forms.ToolStripButton addSiteButton;
        private System.Windows.Forms.ToolStripButton refreshButton;
        private System.Windows.Forms.ToolStripLabel dateLabel;
        private System.Windows.Forms.ToolStripButton dateButton;
        private System.Windows.Forms.ToolStripComboBox timeTypeComboBox;
        private System.Windows.Forms.Panel controlsPanel;
        private System.Windows.Forms.Button decButton;
        private System.Windows.Forms.Button incButton;
        private System.Windows.Forms.Label fixedSiteLabel;
        private System.Windows.Forms.Label activeSiteLabel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripSplitButton viewSplitButton;
        private System.Windows.Forms.ToolStripMenuItem jointMenuItem;
        private System.Windows.Forms.ToolStripMenuItem separateMenuItem;
        private System.Windows.Forms.NumericUpDown offsetCountUpDown;
        private System.Windows.Forms.ComboBox offsetTypeComboBox;
        private System.Windows.Forms.Panel offsetPanel;
        private System.Windows.Forms.Label offsetLabel;
        private System.Windows.Forms.RadioButton plotActionRadioButton;
        private System.Windows.Forms.RadioButton pointActionRadioButton;
        private System.Windows.Forms.Panel chooseActionPanel;
        private System.Windows.Forms.Label chooseActionLabel;
        private System.Windows.Forms.FlowLayoutPanel infoDataPanel;
        private System.Windows.Forms.Panel infoPanel;
        private System.Windows.Forms.Label infoLabel;
        private System.Windows.Forms.Label dateInfolabel;
        private System.Windows.Forms.ToolStripMenuItem varToolStripMenuItem;
        private Meta.UCVariablesList ucVariablesList;
        private System.Windows.Forms.RadioButton twoPointsActionRadioButton;
        private System.Windows.Forms.ToolStripMenuItem trendToolStripMenuItem;
        private System.Windows.Forms.ComboBox offsetSite;
        private System.Windows.Forms.Label siteLabel;
    }
}
