namespace SOV.Amur.Meta
{
    partial class UCCatalogFilter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCCatalogFilter));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuUncheckAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuCheckAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuUncheckAllToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tc = new System.Windows.Forms.TabControl();
            this._tpSite = new System.Windows.Forms.TabPage();
            this.sitesFilter = new SOV.Amur.Meta.UCCatalogFilter0();
            this._tpVariable = new System.Windows.Forms.TabPage();
            this.varsFilter = new SOV.Amur.Meta.UCCatalogFilter0();
            this._tpOffset = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.offsetsFilter = new SOV.Amur.Meta.UCCatalogFilter0();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.offsetValueTextBox = new System.Windows.Forms.TextBox();
            this._tpMethod = new System.Windows.Forms.TabPage();
            this.methodsFilter = new SOV.Amur.Meta.UCCatalogFilter0();
            this._tpSource = new System.Windows.Forms.TabPage();
            this.sourcesFilter = new SOV.Amur.Meta.UCCatalogFilter0();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.refreshAllCashButton = new System.Windows.Forms.ToolStripButton();
            this.filterButton = new System.Windows.Forms.ToolStripButton();
            this.saveFilterButton = new System.Windows.Forms.ToolStripButton();
            this.settingsButton = new System.Windows.Forms.ToolStripButton();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tc.SuspendLayout();
            this._tpSite.SuspendLayout();
            this._tpVariable.SuspendLayout();
            this._tpOffset.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this._tpMethod.SuspendLayout();
            this._tpSource.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "checked");
            this.imageList1.Images.SetKeyName(1, "unchecked");
            this.imageList1.Images.SetKeyName(2, "variableGroup");
            this.imageList1.Images.SetKeyName(3, "siteGroup");
            this.imageList1.Images.SetKeyName(4, "offset");
            this.imageList1.Images.SetKeyName(5, "method");
            this.imageList1.Images.SetKeyName(6, "NavForward.png");
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(36, 36);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuUncheckAllToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(166, 26);
            // 
            // mnuUncheckAllToolStripMenuItem
            // 
            this.mnuUncheckAllToolStripMenuItem.Name = "mnuUncheckAllToolStripMenuItem";
            this.mnuUncheckAllToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.mnuUncheckAllToolStripMenuItem.Text = "Очистить выбор";
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(36, 36);
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCheckAllToolStripMenuItem,
            this.mnuUncheckAllToolStripMenuItem1});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(166, 48);
            // 
            // mnuCheckAllToolStripMenuItem
            // 
            this.mnuCheckAllToolStripMenuItem.Name = "mnuCheckAllToolStripMenuItem";
            this.mnuCheckAllToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.mnuCheckAllToolStripMenuItem.Text = "Выбрать все";
            // 
            // mnuUncheckAllToolStripMenuItem1
            // 
            this.mnuUncheckAllToolStripMenuItem1.Name = "mnuUncheckAllToolStripMenuItem1";
            this.mnuUncheckAllToolStripMenuItem1.Size = new System.Drawing.Size(165, 22);
            this.mnuUncheckAllToolStripMenuItem1.Text = "Очистить выбор";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tc, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(351, 280);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // tc
            // 
            this.tc.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tc.Controls.Add(this._tpSite);
            this.tc.Controls.Add(this._tpVariable);
            this.tc.Controls.Add(this._tpOffset);
            this.tc.Controls.Add(this._tpMethod);
            this.tc.Controls.Add(this._tpSource);
            this.tc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tc.Location = new System.Drawing.Point(3, 28);
            this.tc.Multiline = true;
            this.tc.Name = "tc";
            this.tc.SelectedIndex = 0;
            this.tc.Size = new System.Drawing.Size(345, 249);
            this.tc.TabIndex = 2;
            // 
            // _tpSite
            // 
            this._tpSite.Controls.Add(this.sitesFilter);
            this._tpSite.Location = new System.Drawing.Point(4, 25);
            this._tpSite.Name = "_tpSite";
            this._tpSite.Padding = new System.Windows.Forms.Padding(3);
            this._tpSite.Size = new System.Drawing.Size(337, 220);
            this._tpSite.TabIndex = 0;
            this._tpSite.Text = "Пункты";
            this._tpSite.UseVisualStyleBackColor = true;
            // 
            // sitesFilter
            // 
            this.sitesFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sitesFilter.Location = new System.Drawing.Point(3, 3);
            this.sitesFilter.Margin = new System.Windows.Forms.Padding(7);
            this.sitesFilter.Name = "sitesFilter";
            this.sitesFilter.Size = new System.Drawing.Size(331, 214);
            this.sitesFilter.TabIndex = 0;
            this.sitesFilter.UCDicItemCheckedEvent += new SOV.Amur.Meta.UCCatalogFilter0.UCDicItemCheckedEventHandler(this.sitesFilter_UCDicItemCheckedEvent);
            this.sitesFilter.UCGroupChangedEvent += new SOV.Amur.Meta.UCCatalogFilter0.UCGroupChangedEventHandler(this.sitesFilter_UCGroupChangedEvent);
            // 
            // _tpVariable
            // 
            this._tpVariable.Controls.Add(this.varsFilter);
            this._tpVariable.Location = new System.Drawing.Point(4, 25);
            this._tpVariable.Name = "_tpVariable";
            this._tpVariable.Padding = new System.Windows.Forms.Padding(3);
            this._tpVariable.Size = new System.Drawing.Size(337, 220);
            this._tpVariable.TabIndex = 1;
            this._tpVariable.Text = "Переменные";
            this._tpVariable.UseVisualStyleBackColor = true;
            // 
            // varsFilter
            // 
            this.varsFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.varsFilter.Location = new System.Drawing.Point(3, 3);
            this.varsFilter.Margin = new System.Windows.Forms.Padding(7);
            this.varsFilter.Name = "varsFilter";
            this.varsFilter.Size = new System.Drawing.Size(331, 214);
            this.varsFilter.TabIndex = 1;
            // 
            // _tpOffset
            // 
            this._tpOffset.Controls.Add(this.tableLayoutPanel2);
            this._tpOffset.Location = new System.Drawing.Point(4, 25);
            this._tpOffset.Name = "_tpOffset";
            this._tpOffset.Size = new System.Drawing.Size(337, 220);
            this._tpOffset.TabIndex = 2;
            this._tpOffset.Text = "Смещения";
            this._tpOffset.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.offsetsFilter, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(337, 220);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // offsetsFilter
            // 
            this.offsetsFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.offsetsFilter.Location = new System.Drawing.Point(7, 7);
            this.offsetsFilter.Margin = new System.Windows.Forms.Padding(7);
            this.offsetsFilter.Name = "offsetsFilter";
            this.offsetsFilter.Size = new System.Drawing.Size(323, 173);
            this.offsetsFilter.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.4433F));
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.offsetValueTextBox, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 190);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(331, 27);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Значение смещения";
            // 
            // offsetValueTextBox
            // 
            this.offsetValueTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.offsetValueTextBox.Location = new System.Drawing.Point(120, 3);
            this.offsetValueTextBox.Name = "offsetValueTextBox";
            this.offsetValueTextBox.Size = new System.Drawing.Size(208, 20);
            this.offsetValueTextBox.TabIndex = 1;
            // 
            // _tpMethod
            // 
            this._tpMethod.Controls.Add(this.methodsFilter);
            this._tpMethod.Location = new System.Drawing.Point(4, 25);
            this._tpMethod.Name = "_tpMethod";
            this._tpMethod.Size = new System.Drawing.Size(337, 220);
            this._tpMethod.TabIndex = 3;
            this._tpMethod.Text = "Методы";
            this._tpMethod.UseVisualStyleBackColor = true;
            // 
            // methodsFilter
            // 
            this.methodsFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.methodsFilter.Location = new System.Drawing.Point(0, 0);
            this.methodsFilter.Margin = new System.Windows.Forms.Padding(7);
            this.methodsFilter.Name = "methodsFilter";
            this.methodsFilter.Size = new System.Drawing.Size(337, 220);
            this.methodsFilter.TabIndex = 1;
            // 
            // _tpSource
            // 
            this._tpSource.Controls.Add(this.sourcesFilter);
            this._tpSource.Location = new System.Drawing.Point(4, 25);
            this._tpSource.Name = "_tpSource";
            this._tpSource.Size = new System.Drawing.Size(337, 220);
            this._tpSource.TabIndex = 4;
            this._tpSource.Text = "Источники";
            this._tpSource.UseVisualStyleBackColor = true;
            // 
            // sourcesFilter
            // 
            this.sourcesFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sourcesFilter.Location = new System.Drawing.Point(0, 0);
            this.sourcesFilter.Margin = new System.Windows.Forms.Padding(7);
            this.sourcesFilter.Name = "sourcesFilter";
            this.sourcesFilter.Size = new System.Drawing.Size(337, 220);
            this.sourcesFilter.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(36, 36);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshAllCashButton,
            this.filterButton,
            this.saveFilterButton,
            this.settingsButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(351, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // refreshAllCashButton
            // 
            this.refreshAllCashButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.refreshAllCashButton.Image = global::SOV.Amur.Meta.Properties.Resources.refresh_16xLG;
            this.refreshAllCashButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.refreshAllCashButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.refreshAllCashButton.Name = "refreshAllCashButton";
            this.refreshAllCashButton.Size = new System.Drawing.Size(23, 22);
            this.refreshAllCashButton.Text = "toolStripButton1";
            this.refreshAllCashButton.ToolTipText = "Обновить все справочники";
            this.refreshAllCashButton.Click += new System.EventHandler(this.refreshAllCashButton_Click);
            // 
            // filterButton
            // 
            this.filterButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.filterButton.Image = global::SOV.Amur.Meta.Properties.Resources.FilteredObject_13400_16x_MD;
            this.filterButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.filterButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.filterButton.Name = "filterButton";
            this.filterButton.Size = new System.Drawing.Size(23, 22);
            this.filterButton.Text = "Применить фильтр";
            this.filterButton.Click += new System.EventHandler(this.filterButton_Click);
            // 
            // saveFilterButton
            // 
            this.saveFilterButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveFilterButton.Image = global::SOV.Amur.Meta.Properties.Resources.SaveBlack;
            this.saveFilterButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.saveFilterButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveFilterButton.Name = "saveFilterButton";
            this.saveFilterButton.Size = new System.Drawing.Size(23, 22);
            this.saveFilterButton.Text = "Сохранить фильтр в настройках пользователя";
            this.saveFilterButton.Click += new System.EventHandler(this.saveFilterButton_Click);
            // 
            // settingsButton
            // 
            this.settingsButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.settingsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.settingsButton.Image = global::SOV.Amur.Meta.Properties.Resources.Property_501;
            this.settingsButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.settingsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(23, 22);
            this.settingsButton.Text = "Настройки";
            this.settingsButton.Click += new System.EventHandler(this.settingsButton_Click);
            // 
            // UCCatalogFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UCCatalogFilter";
            this.Size = new System.Drawing.Size(351, 280);
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tc.ResumeLayout(false);
            this._tpSite.ResumeLayout(false);
            this._tpVariable.ResumeLayout(false);
            this._tpOffset.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this._tpMethod.ResumeLayout(false);
            this._tpSource.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuUncheckAllToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem mnuCheckAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuUncheckAllToolStripMenuItem1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl tc;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton filterButton;
        private System.Windows.Forms.ToolStripButton saveFilterButton;
        private UCCatalogFilter0 sitesFilter;
        private UCCatalogFilter0 varsFilter;
        private UCCatalogFilter0 offsetsFilter;
        private UCCatalogFilter0 methodsFilter;
        private UCCatalogFilter0 sourcesFilter;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox offsetValueTextBox;
        private System.Windows.Forms.ToolStripButton settingsButton;
        private System.Windows.Forms.ToolStripButton refreshAllCashButton;
    }
}
