namespace SOV.Amur.Meta
{
    partial class UCSiteGeoObjectList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCSiteGeoObjectList));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.refreshToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveEditedButton = new System.Windows.Forms.ToolStripButton();
            this.editToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.editStationToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.showDataGrpSVYMButton = new System.Windows.Forms.ToolStripButton();
            this.siteGroupCombobox = new SOV.Amur.Meta.SiteGroupComboBox();
            this.infoToolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.findToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.findNextToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.isOneTableToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.geoObjectName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stationCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stationName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.siteTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuEditDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditSiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuClimateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuShowSiteDataGrpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToChartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(414, 261);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripButton,
            this.saveEditedButton,
            this.editToolStripButton,
            this.editStationToolStripButton,
            this.showDataGrpSVYMButton,
            this.siteGroupCombobox,
            this.infoToolStripLabel,
            this.findToolStripTextBox,
            this.findNextToolStripButton,
            this.toolStripSeparator1,
            this.isOneTableToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(414, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // refreshToolStripButton
            // 
            this.refreshToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.refreshToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("refreshToolStripButton.Image")));
            this.refreshToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.refreshToolStripButton.Name = "refreshToolStripButton";
            this.refreshToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.refreshToolStripButton.Text = "toolStripButton1";
            this.refreshToolStripButton.ToolTipText = "Обновить таблицу";
            this.refreshToolStripButton.Click += new System.EventHandler(this.RefreshToolStripButton_Click);
            // 
            // saveEditedButton
            // 
            this.saveEditedButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveEditedButton.Enabled = false;
            this.saveEditedButton.Image = global::SOV.Amur.Meta.Properties.Resources.SaveBlack;
            this.saveEditedButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveEditedButton.Name = "saveEditedButton";
            this.saveEditedButton.Size = new System.Drawing.Size(23, 22);
            this.saveEditedButton.Text = "toolStripButton1";
            this.saveEditedButton.Click += new System.EventHandler(this.saveEditedButton_Click);
            // 
            // editToolStripButton
            // 
            this.editToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.editToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("editToolStripButton.Image")));
            this.editToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editToolStripButton.Name = "editToolStripButton";
            this.editToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.editToolStripButton.Text = "toolStripButton1";
            this.editToolStripButton.ToolTipText = "Редактировать данные пункта";
            this.editToolStripButton.Click += new System.EventHandler(this.editToolStripButton_Click);
            // 
            // editStationToolStripButton
            // 
            this.editStationToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.editStationToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("editStationToolStripButton.Image")));
            this.editStationToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editStationToolStripButton.Name = "editStationToolStripButton";
            this.editStationToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.editStationToolStripButton.Text = "toolStripButton1";
            this.editStationToolStripButton.ToolTipText = "Редактировать пункт";
            this.editStationToolStripButton.Click += new System.EventHandler(this.editStationToolStripButton_Click);
            // 
            // showDataGrpSVYMButton
            // 
            this.showDataGrpSVYMButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.showDataGrpSVYMButton.Image = ((System.Drawing.Image)(resources.GetObject("showDataGrpSVYMButton.Image")));
            this.showDataGrpSVYMButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showDataGrpSVYMButton.Name = "showDataGrpSVYMButton";
            this.showDataGrpSVYMButton.Size = new System.Drawing.Size(23, 22);
            this.showDataGrpSVYMButton.Text = "Просмотр наличия данных для группы пунктов";
            this.showDataGrpSVYMButton.Click += new System.EventHandler(this.showDataGrpSVYMButton_Click);
            // 
            // siteGroupToolStripComboBox
            // 
            this.siteGroupCombobox.BackColor = System.Drawing.SystemColors.Info;
            this.siteGroupCombobox.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.siteGroupCombobox.Name = "siteGroupToolStripComboBox";
            this.siteGroupCombobox.SiteGroup = null;
            this.siteGroupCombobox.Size = new System.Drawing.Size(200, 25);
            this.siteGroupCombobox.SelectedIndexChanged += new System.EventHandler(this.siteGroupToolStripComboBox_SelectedIndexChanged);
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
            // 
            // findNextToolStripButton
            // 
            this.findNextToolStripButton.BackColor = System.Drawing.SystemColors.Info;
            this.findNextToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.findNextToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("findNextToolStripButton.Image")));
            this.findNextToolStripButton.ImageTransparentColor = System.Drawing.SystemColors.Info;
            this.findNextToolStripButton.Name = "findNextToolStripButton";
            this.findNextToolStripButton.Size = new System.Drawing.Size(23, 20);
            this.findNextToolStripButton.Text = "toolStripButton1";
            this.findNextToolStripButton.ToolTipText = "Найти следующее совпадение";
            this.findNextToolStripButton.Click += new System.EventHandler(this.findNextToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // isOneTableToolStripButton
            // 
            this.isOneTableToolStripButton.BackColor = System.Drawing.SystemColors.ControlDark;
            this.isOneTableToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.isOneTableToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.isOneTableToolStripButton.Name = "isOneTableToolStripButton";
            this.isOneTableToolStripButton.Size = new System.Drawing.Size(23, 19);
            this.isOneTableToolStripButton.Text = "2";
            this.isOneTableToolStripButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.isOneTableToolStripButton.ToolTipText = "Количество вкладок";
            this.isOneTableToolStripButton.Click += new System.EventHandler(this.isOneTableToolStripButton_Click);
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.geoObjectName,
            this.stationCode,
            this.stationName,
            this.siteTypeName});
            this.dgv.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(3, 28);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv.RowHeadersVisible = false;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgv.Size = new System.Drawing.Size(408, 230);
            this.dgv.TabIndex = 1;
            this.dgv.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellEndEdit);
            this.dgv.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellEnter);
            this.dgv.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_RowEnter);
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
            this.stationCode.Name = "siteCode";
            this.stationCode.ReadOnly = true;
            this.stationCode.Width = 70;
            // 
            // stationName
            // 
            this.stationName.HeaderText = "Пункт";
            this.stationName.Name = "siteName";
            this.stationName.ReadOnly = true;
            this.stationName.Width = 185;
            // 
            // siteTypeName
            // 
            this.siteTypeName.HeaderText = "Тип";
            this.siteTypeName.Name = "siteTypeName";
            this.siteTypeName.ReadOnly = true;
            this.siteTypeName.Width = 5;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEditDataToolStripMenuItem,
            this.mnuEditSiteToolStripMenuItem,
            this.mnuClimateToolStripMenuItem,
            this.mnuShowSiteDataGrpToolStripMenuItem,
            this.addToChartToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(245, 114);
            // 
            // mnuEditDataToolStripMenuItem
            // 
            this.mnuEditDataToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("mnuEditDataToolStripMenuItem.Image")));
            this.mnuEditDataToolStripMenuItem.Name = "mnuEditDataToolStripMenuItem";
            this.mnuEditDataToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.mnuEditDataToolStripMenuItem.Text = "Редактировать данные";
            this.mnuEditDataToolStripMenuItem.Click += new System.EventHandler(this.mnuEditDataToolStripMenuItem_Click);
            // 
            // mnuEditSiteToolStripMenuItem
            // 
            this.mnuEditSiteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("mnuEditSiteToolStripMenuItem.Image")));
            this.mnuEditSiteToolStripMenuItem.Name = "mnuEditSiteToolStripMenuItem";
            this.mnuEditSiteToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.mnuEditSiteToolStripMenuItem.Text = "Редактировать пункт";
            this.mnuEditSiteToolStripMenuItem.Click += new System.EventHandler(this.mnuEditSiteToolStripMenuItem_Click);
            // 
            // mnuClimateToolStripMenuItem
            // 
            this.mnuClimateToolStripMenuItem.Name = "mnuClimateToolStripMenuItem";
            this.mnuClimateToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.mnuClimateToolStripMenuItem.Text = "Климатические данные пункта";
            this.mnuClimateToolStripMenuItem.Click += new System.EventHandler(this.mnuClimateToolStripMenuItem_Click);
            // 
            // mnuShowSiteDataGrpToolStripMenuItem
            // 
            this.mnuShowSiteDataGrpToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("mnuShowSiteDataGrpToolStripMenuItem.Image")));
            this.mnuShowSiteDataGrpToolStripMenuItem.Name = "mnuShowSiteDataGrpToolStripMenuItem";
            this.mnuShowSiteDataGrpToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.mnuShowSiteDataGrpToolStripMenuItem.Text = "Наличие данных для пункта...";
            this.mnuShowSiteDataGrpToolStripMenuItem.Click += new System.EventHandler(this.mnuShowSiteDataGrpToolStripMenuItem_Click);
            // 
            // addToChartToolStripMenuItem
            // 
            this.addToChartToolStripMenuItem.Name = "addToChartToolStripMenuItem";
            this.addToChartToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.addToChartToolStripMenuItem.Text = "Добавить на график";
            this.addToChartToolStripMenuItem.Click += new System.EventHandler(this.addToChartToolStripMenuItem_Click);
            // 
            // UCSiteGeoObjectList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UCSiteGeoObjectList";
            this.Size = new System.Drawing.Size(414, 261);
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
        private Meta.SiteGroupComboBox siteGroupCombobox;
        private System.Windows.Forms.ToolStripLabel infoToolStripLabel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuEditDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton editToolStripButton;
        private System.Windows.Forms.ToolStripTextBox findToolStripTextBox;
        private System.Windows.Forms.ToolStripButton findNextToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuEditSiteToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton editStationToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem mnuClimateToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton refreshToolStripButton;
        private System.Windows.Forms.ToolStripButton isOneTableToolStripButton;
        private System.Windows.Forms.ToolStripButton showDataGrpSVYMButton;
        private System.Windows.Forms.ToolStripMenuItem mnuShowSiteDataGrpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToChartToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton saveEditedButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn geoObjectName;
        private System.Windows.Forms.DataGridViewTextBoxColumn stationCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn stationName;
        private System.Windows.Forms.DataGridViewTextBoxColumn siteTypeName;
    }
}
