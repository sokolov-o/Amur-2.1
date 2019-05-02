namespace SOV.Amur.Meta
{
    partial class UCStations
    {
        /// <summary> 
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Обязательный метод для поддержки конструктора - не изменяйте 
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCStations));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.refreshToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.noSitesToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.siteGroupToolStripComboBox = new SOV.Amur.Meta.SiteGroupComboBox();
            this.infoToolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.findToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.findNextToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.addNewToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.editStationToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.selectionInfoToolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.selectionCounterToolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.complateSelectionToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.geoObjectName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stationCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stationName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stationTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuEditSiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgv, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(560, 278);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripButton,
            this.noSitesToolStripButton,
            this.siteGroupToolStripComboBox,
            this.infoToolStripLabel,
            this.findToolStripTextBox,
            this.findNextToolStripButton,
            this.toolStripSeparator1,
            this.addNewToolStripButton,
            this.editStationToolStripButton,
            this.selectionInfoToolStripLabel,
            this.selectionCounterToolStripLabel,
            this.complateSelectionToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(560, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // refreshToolStripButton
            // 
            this.refreshToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.refreshToolStripButton.Image = global::SOV.Amur.Meta.Properties.Resources.refresh_16xLG;
            this.refreshToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.refreshToolStripButton.Name = "refreshToolStripButton";
            this.refreshToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.refreshToolStripButton.Text = "toolStripButton1";
            this.refreshToolStripButton.ToolTipText = "Обновить таблицу";
            this.refreshToolStripButton.Click += new System.EventHandler(this.refreshToolStripButton_Click);
            // 
            // noSitesToolStripButton
            // 
            this.noSitesToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.noSitesToolStripButton.Image = global::SOV.Amur.Meta.Properties.Resources.house_16xMD;
            this.noSitesToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.noSitesToolStripButton.Name = "noSitesToolStripButton";
            this.noSitesToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.noSitesToolStripButton.Text = "Станции без группы";
            this.noSitesToolStripButton.ToolTipText = "Станции без наблюдательных пунктов";
            this.noSitesToolStripButton.Click += new System.EventHandler(this.noSitesToolStripButton_Click);
            // 
            // siteGroupToolStripComboBox
            // 
            this.siteGroupToolStripComboBox.BackColor = System.Drawing.SystemColors.Info;
            this.siteGroupToolStripComboBox.Name = "siteGroupToolStripComboBox";
            this.siteGroupToolStripComboBox.SiteGroup = null;
            this.siteGroupToolStripComboBox.Size = new System.Drawing.Size(200, 25);
            this.siteGroupToolStripComboBox.SelectedIndexChanged += new System.EventHandler(this.siteGroupToolStripComboBox_SelectedIndexChanged);
            // 
            // infoToolStripLabel
            // 
            this.infoToolStripLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.infoToolStripLabel.BackColor = System.Drawing.SystemColors.Info;
            this.infoToolStripLabel.Name = "infoToolStripLabel";
            this.infoToolStripLabel.Size = new System.Drawing.Size(10, 22);
            this.infoToolStripLabel.Text = ".";
            // 
            // findToolStripTextBox
            // 
            this.findToolStripTextBox.BackColor = System.Drawing.SystemColors.Info;
            this.findToolStripTextBox.Margin = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.findToolStripTextBox.Name = "findToolStripTextBox";
            this.findToolStripTextBox.Size = new System.Drawing.Size(50, 25);
            this.findToolStripTextBox.ToolTipText = "Поиск - введите часть искомого названия и нажмите кнопку ПОИСК справа";
            this.findToolStripTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.findToolStripTextBox_KeyUp);
            // 
            // findNextToolStripButton
            // 
            this.findNextToolStripButton.BackColor = System.Drawing.SystemColors.Info;
            this.findNextToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.findNextToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("findNextToolStripButton.Image")));
            this.findNextToolStripButton.ImageTransparentColor = System.Drawing.SystemColors.Info;
            this.findNextToolStripButton.Name = "findNextToolStripButton";
            this.findNextToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.findNextToolStripButton.Text = "Поиск";
            this.findNextToolStripButton.ToolTipText = "Найти следующее совпадение";
            this.findNextToolStripButton.Click += new System.EventHandler(this.findNextToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // addNewToolStripButton
            // 
            this.addNewToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addNewToolStripButton.Image = global::SOV.Amur.Meta.Properties.Resources.action_add_16xLG;
            this.addNewToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addNewToolStripButton.Name = "addNewToolStripButton";
            this.addNewToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.addNewToolStripButton.Text = "toolStripButton1";
            this.addNewToolStripButton.ToolTipText = "Новая станция";
            this.addNewToolStripButton.Click += new System.EventHandler(this.addNewToolStripButton_Click);
            // 
            // editStationToolStripButton
            // 
            this.editStationToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.editStationToolStripButton.Image = global::SOV.Amur.Meta.Properties.Resources.Home_5699;
            this.editStationToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editStationToolStripButton.Name = "editStationToolStripButton";
            this.editStationToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.editStationToolStripButton.Text = "toolStripButton1";
            this.editStationToolStripButton.ToolTipText = "Редактировать станцию";
            this.editStationToolStripButton.Click += new System.EventHandler(this.editStationToolStripButton_Click);
            // 
            // selectionInfoToolStripLabel
            // 
            this.selectionInfoToolStripLabel.Name = "selectionInfoToolStripLabel";
            this.selectionInfoToolStripLabel.Size = new System.Drawing.Size(60, 22);
            this.selectionInfoToolStripLabel.Text = "Выбрано:";
            // 
            // selectionCounterToolStripLabel
            // 
            this.selectionCounterToolStripLabel.Name = "selectionCounterToolStripLabel";
            this.selectionCounterToolStripLabel.Size = new System.Drawing.Size(12, 22);
            this.selectionCounterToolStripLabel.Text = "-";
            // 
            // complateSelectionToolStripButton
            // 
            this.complateSelectionToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.complateSelectionToolStripButton.Image = global::SOV.Amur.Meta.Properties.Resources.SaveBlack;
            this.complateSelectionToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.complateSelectionToolStripButton.Name = "complateSelectionToolStripButton";
            this.complateSelectionToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.complateSelectionToolStripButton.Text = "toolStripButton1";
            this.complateSelectionToolStripButton.Click += new System.EventHandler(this.complateSelection);
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.geoObjectName,
            this.stationCode,
            this.stationName,
            this.stationTypeName});
            this.dgv.ContextMenuStrip = this.contextMenuStrip1;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(3, 28);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(554, 247);
            this.dgv.TabIndex = 1;
            this.dgv.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_RowEnter);
            this.dgv.SelectionChanged += new System.EventHandler(this.dgv_SelectionChanged);
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // geoObjectName
            // 
            this.geoObjectName.HeaderText = "Водный объект";
            this.geoObjectName.Name = "geoObjectName";
            this.geoObjectName.ReadOnly = true;
            // 
            // stationCode
            // 
            this.stationCode.HeaderText = "Индекс";
            this.stationCode.Name = "stationCode";
            this.stationCode.ReadOnly = true;
            this.stationCode.Width = 70;
            // 
            // stationName
            // 
            this.stationName.HeaderText = "Пост, станция";
            this.stationName.Name = "stationName";
            this.stationName.ReadOnly = true;
            this.stationName.Width = 185;
            // 
            // stationTypeName
            // 
            this.stationTypeName.HeaderText = "Тип";
            this.stationTypeName.Name = "stationTypeName";
            this.stationTypeName.ReadOnly = true;
            this.stationTypeName.Width = 5;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEditSiteToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(189, 26);
            // 
            // mnuEditSiteToolStripMenuItem
            // 
            this.mnuEditSiteToolStripMenuItem.Image = global::SOV.Amur.Meta.Properties.Resources.Home_5699;
            this.mnuEditSiteToolStripMenuItem.Name = "mnuEditSiteToolStripMenuItem";
            this.mnuEditSiteToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.mnuEditSiteToolStripMenuItem.Text = "Редактировать пункт";
            this.mnuEditSiteToolStripMenuItem.Click += new System.EventHandler(this.mnuEditSiteToolStripMenuItem_Click);
            // 
            // UCStations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UCStations";
            this.Size = new System.Drawing.Size(560, 278);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.DataGridView dgv;
        private Meta.SiteGroupComboBox siteGroupToolStripComboBox;
        private System.Windows.Forms.ToolStripLabel infoToolStripLabel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripTextBox findToolStripTextBox;
        private System.Windows.Forms.ToolStripButton findNextToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuEditSiteToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton refreshToolStripButton;
        private System.Windows.Forms.ToolStripButton editStationToolStripButton;
        private System.Windows.Forms.ToolStripButton noSitesToolStripButton;
        private System.Windows.Forms.ToolStripButton addNewToolStripButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn geoObjectName;
        private System.Windows.Forms.DataGridViewTextBoxColumn stationCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn stationName;
        private System.Windows.Forms.DataGridViewTextBoxColumn stationTypeName;
        private System.Windows.Forms.ToolStripLabel selectionInfoToolStripLabel;
        private System.Windows.Forms.ToolStripLabel selectionCounterToolStripLabel;
        private System.Windows.Forms.ToolStripButton complateSelectionToolStripButton;
    }
}
