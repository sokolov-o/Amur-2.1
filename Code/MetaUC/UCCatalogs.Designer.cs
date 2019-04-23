namespace SOV.Amur.Meta
{
    partial class UCCatalogs
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
            SOV.Amur.Meta.CatalogFilter catalogFilter1 = new SOV.Amur.Meta.CatalogFilter();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCCatalogs));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ucCatalogFilter = new SOV.Amur.Meta.UCCatalogFilter();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.downButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.siteIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.variableIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.offsetTypeIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.offsetValueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.methodIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sourceIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.infoLabel = new System.Windows.Forms.ToolStripLabel();
            this.settingsButton = new System.Windows.Forms.ToolStripButton();
            this.addCatalogButton = new System.Windows.Forms.ToolStripButton();
            this.deleteCatalogButton = new System.Windows.Forms.ToolStripButton();
            this.saveCatalogOrderbyButton = new System.Windows.Forms.ToolStripButton();
            this.showDataButton = new System.Windows.Forms.ToolStripButton();
            this.upButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.newButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.ucCatalog = new SOV.Amur.Meta.UCCatalog();
            this.catalogBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.catalogBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 383F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(812, 383);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(806, 377);
            this.splitContainer1.SplitterDistance = 246;
            this.splitContainer1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ucCatalogFilter);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(244, 375);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Фильтр  каталога";
            // 
            // ucCatalogFilter
            // 
            catalogFilter1.Catalogs = null;
            catalogFilter1.Methods = null;
            catalogFilter1.OffsetTypes = null;
            catalogFilter1.OffsetValue = null;
            catalogFilter1.Sites = null;
            catalogFilter1.Sources = null;
            catalogFilter1.Variables = null;
            this.ucCatalogFilter.CatalogFilter = catalogFilter1;
            this.ucCatalogFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucCatalogFilter.Location = new System.Drawing.Point(3, 16);
            this.ucCatalogFilter.Margin = new System.Windows.Forms.Padding(7);
            this.ucCatalogFilter.Name = "ucCatalogFilter";
            this.ucCatalogFilter.ShowToolStrip = true;
            this.ucCatalogFilter.Size = new System.Drawing.Size(238, 356);
            this.ucCatalogFilter.TabIndex = 0;
            this.ucCatalogFilter.UCFilterButtonClickEvent += new SOV.Amur.Meta.UCCatalogFilter.UCFilterButtonClickEventHandler(this.ucCatalogFilter_UCFilterButtonClickEvent);
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tableLayoutPanel3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.button1);
            this.splitContainer2.Panel2.Controls.Add(this.newButton);
            this.splitContainer2.Panel2.Controls.Add(this.saveButton);
            this.splitContainer2.Panel2.Controls.Add(this.ucCatalog);
            this.splitContainer2.Size = new System.Drawing.Size(556, 377);
            this.splitContainer2.SplitterDistance = 269;
            this.splitContainer2.TabIndex = 3;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.downButton, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.upButton, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(267, 375);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // downButton
            // 
            this.downButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.downButton.Image = ((System.Drawing.Image)(resources.GetObject("downButton.Image")));
            this.downButton.Location = new System.Drawing.Point(234, 190);
            this.downButton.Name = "downButton";
            this.downButton.Size = new System.Drawing.Size(30, 30);
            this.downButton.TabIndex = 5;
            this.downButton.UseVisualStyleBackColor = true;
            this.downButton.Click += new System.EventHandler(this.downButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.tableLayoutPanel3.SetRowSpan(this.groupBox2, 2);
            this.groupBox2.Size = new System.Drawing.Size(225, 369);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Записи каталога данных";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.dgv, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(219, 350);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.siteIdDataGridViewTextBoxColumn,
            this.variableIdDataGridViewTextBoxColumn,
            this.offsetTypeIdDataGridViewTextBoxColumn,
            this.offsetValueDataGridViewTextBoxColumn,
            this.methodIdDataGridViewTextBoxColumn,
            this.sourceIdDataGridViewTextBoxColumn,
            this.id});
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(3, 28);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(213, 319);
            this.dgv.TabIndex = 0;
            this.dgv.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_RowEnter);
            // 
            // siteIdDataGridViewTextBoxColumn
            // 
            this.siteIdDataGridViewTextBoxColumn.HeaderText = "Пункт";
            this.siteIdDataGridViewTextBoxColumn.Name = "siteIdDataGridViewTextBoxColumn";
            this.siteIdDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // variableIdDataGridViewTextBoxColumn
            // 
            this.variableIdDataGridViewTextBoxColumn.HeaderText = "Переменная";
            this.variableIdDataGridViewTextBoxColumn.Name = "variableIdDataGridViewTextBoxColumn";
            this.variableIdDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // offsetTypeIdDataGridViewTextBoxColumn
            // 
            this.offsetTypeIdDataGridViewTextBoxColumn.HeaderText = "Смещение";
            this.offsetTypeIdDataGridViewTextBoxColumn.Name = "offsetTypeIdDataGridViewTextBoxColumn";
            this.offsetTypeIdDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // offsetValueDataGridViewTextBoxColumn
            // 
            this.offsetValueDataGridViewTextBoxColumn.HeaderText = "Значение";
            this.offsetValueDataGridViewTextBoxColumn.Name = "offsetValueDataGridViewTextBoxColumn";
            this.offsetValueDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // methodIdDataGridViewTextBoxColumn
            // 
            this.methodIdDataGridViewTextBoxColumn.HeaderText = "Метод";
            this.methodIdDataGridViewTextBoxColumn.Name = "methodIdDataGridViewTextBoxColumn";
            this.methodIdDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sourceIdDataGridViewTextBoxColumn
            // 
            this.sourceIdDataGridViewTextBoxColumn.HeaderText = "Источник";
            this.sourceIdDataGridViewTextBoxColumn.Name = "sourceIdDataGridViewTextBoxColumn";
            this.sourceIdDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // id
            // 
            this.id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.id.HeaderText = "КОД";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Width = 56;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(36, 36);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.infoLabel,
            this.settingsButton,
            this.addCatalogButton,
            this.deleteCatalogButton,
            this.saveCatalogOrderbyButton,
            this.showDataButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(219, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // infoLabel
            // 
            this.infoLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(12, 22);
            this.infoLabel.Text = "-";
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
            this.settingsButton.Text = "settingsButton";
            this.settingsButton.ToolTipText = "Скрыть/показать подробности текущей записи";
            this.settingsButton.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // addCatalogButton
            // 
            this.addCatalogButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addCatalogButton.Image = global::SOV.Amur.Meta.Properties.Resources.action_add_16xLG1;
            this.addCatalogButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.addCatalogButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addCatalogButton.Name = "addCatalogButton";
            this.addCatalogButton.Size = new System.Drawing.Size(23, 22);
            this.addCatalogButton.Text = "Добавить запись каталога";
            this.addCatalogButton.ToolTipText = "Добавить запись каталога";
            this.addCatalogButton.Click += new System.EventHandler(this.addCatalogButton_Click);
            // 
            // deleteCatalogButton
            // 
            this.deleteCatalogButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deleteCatalogButton.Image = global::SOV.Amur.Meta.Properties.Resources.DeleteHS;
            this.deleteCatalogButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.deleteCatalogButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteCatalogButton.Name = "deleteCatalogButton";
            this.deleteCatalogButton.Size = new System.Drawing.Size(23, 22);
            this.deleteCatalogButton.Text = "toolStripButton1";
            this.deleteCatalogButton.ToolTipText = "Удалить выбранные записи каталога данных";
            this.deleteCatalogButton.Click += new System.EventHandler(this.deleteCatalogToolStripButton_Click);
            // 
            // saveCatalogOrderbyButton
            // 
            this.saveCatalogOrderbyButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveCatalogOrderbyButton.Image = global::SOV.Amur.Meta.Properties.Resources.SaveBlack;
            this.saveCatalogOrderbyButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.saveCatalogOrderbyButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveCatalogOrderbyButton.Name = "saveCatalogOrderbyButton";
            this.saveCatalogOrderbyButton.Size = new System.Drawing.Size(23, 22);
            this.saveCatalogOrderbyButton.Text = "toolStripButton1";
            this.saveCatalogOrderbyButton.ToolTipText = "Сохранить порядок строк";
            this.saveCatalogOrderbyButton.Click += new System.EventHandler(this.saveCatalogOrderbyButton_Click);
            // 
            // showDataButton
            // 
            this.showDataButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.showDataButton.Image = global::SOV.Amur.Meta.Properties.Resources.Edit;
            this.showDataButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.showDataButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showDataButton.Name = "showDataButton";
            this.showDataButton.Size = new System.Drawing.Size(23, 22);
            this.showDataButton.Text = "Show data";
            this.showDataButton.ToolTipText = "Просмотр данных выбранной записи каталога";
            this.showDataButton.Click += new System.EventHandler(this.showDataButton_Click);
            // 
            // upButton
            // 
            this.upButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.upButton.Image = ((System.Drawing.Image)(resources.GetObject("upButton.Image")));
            this.upButton.Location = new System.Drawing.Point(234, 154);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(30, 30);
            this.upButton.TabIndex = 4;
            this.upButton.UseVisualStyleBackColor = true;
            this.upButton.Click += new System.EventHandler(this.upButton_Click);
            // 
            // button1
            // 
            this.button1.Image = global::SOV.Amur.Meta.Properties.Resources.action_add_16xLG1;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(3, 229);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(166, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Создать на основе";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // newButton
            // 
            this.newButton.Image = global::SOV.Amur.Meta.Properties.Resources.action_add_16xLG;
            this.newButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.newButton.Location = new System.Drawing.Point(3, 200);
            this.newButton.Name = "newButton";
            this.newButton.Size = new System.Drawing.Size(75, 23);
            this.newButton.TabIndex = 2;
            this.newButton.Text = "Создать";
            this.newButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.newButton.UseVisualStyleBackColor = true;
            this.newButton.Click += new System.EventHandler(this.newButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Image = global::SOV.Amur.Meta.Properties.Resources.SaveBlack;
            this.saveButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.saveButton.Location = new System.Drawing.Point(84, 200);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(85, 23);
            this.saveButton.TabIndex = 1;
            this.saveButton.Text = "Сохранить";
            this.saveButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // ucCatalog
            // 
            this.ucCatalog.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ucCatalog.Catalog = null;
            this.ucCatalog.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucCatalog.Location = new System.Drawing.Point(0, 0);
            this.ucCatalog.Margin = new System.Windows.Forms.Padding(7);
            this.ucCatalog.Name = "ucCatalog";
            this.ucCatalog.Size = new System.Drawing.Size(281, 194);
            this.ucCatalog.TabIndex = 0;
            // 
            // catalogBindingSource
            // 
            this.catalogBindingSource.DataSource = typeof(SOV.Amur.Meta.Catalog);
            // 
            // UCCatalogs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UCCatalogs";
            this.Size = new System.Drawing.Size(812, 383);
            this.Load += new System.EventHandler(this.UCCatalogs_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.catalogBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        //private Meta.UCCatalogFilterG ucCatalogKeyTree1;
        private UCCatalogFilter ucCatalogFilter;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.BindingSource catalogBindingSource;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel infoLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button newButton;
        private System.Windows.Forms.Button saveButton;
        private UCCatalog ucCatalog;
        private System.Windows.Forms.ToolStripButton deleteCatalogButton;
        private System.Windows.Forms.ToolStripButton addCatalogButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button upButton;
        private System.Windows.Forms.Button downButton;
        private System.Windows.Forms.ToolStripButton saveCatalogOrderbyButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn siteIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn variableIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn offsetTypeIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn offsetValueDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn methodIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sourceIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.ToolStripButton showDataButton;
        private System.Windows.Forms.ToolStripButton settingsButton;
    }
}
