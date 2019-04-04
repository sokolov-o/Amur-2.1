namespace SOV.Amur.Sys
{
    partial class FormSys
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSys));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.entityesListBox = new System.Windows.Forms.ListBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.instancesListBox = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvValues = new System.Windows.Forms.DataGridView();
            this.EntityAttr = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.attrBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.valueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.attrValueBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.entityLogToolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.dateSToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.dateFToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.refreshToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.showParentsOnlyToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.filterLikeToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.filterLikeToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.dgvLog = new System.Windows.Forms.DataGridView();
            this.isUrgentDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.messageDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.logBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvValues)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.attrBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.attrValueBindingSource)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(822, 561);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(814, 535);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Свойства системных объектов";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
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
            this.splitContainer1.Size = new System.Drawing.Size(808, 529);
            this.splitContainer1.SplitterDistance = 152;
            this.splitContainer1.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.entityesListBox);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(152, 529);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Объекты";
            // 
            // entityesListBox
            // 
            this.entityesListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.entityesListBox.FormattingEnabled = true;
            this.entityesListBox.Location = new System.Drawing.Point(3, 16);
            this.entityesListBox.Name = "entityesListBox";
            this.entityesListBox.Size = new System.Drawing.Size(146, 510);
            this.entityesListBox.TabIndex = 1;
            this.entityesListBox.SelectedIndexChanged += new System.EventHandler(this.entityesListBox_SelectedIndexChanged);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer2.Size = new System.Drawing.Size(652, 529);
            this.splitContainer2.SplitterDistance = 171;
            this.splitContainer2.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.instancesListBox);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(171, 529);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Экземпляры объектов";
            // 
            // instancesListBox
            // 
            this.instancesListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.instancesListBox.FormattingEnabled = true;
            this.instancesListBox.Location = new System.Drawing.Point(3, 16);
            this.instancesListBox.Name = "instancesListBox";
            this.instancesListBox.Size = new System.Drawing.Size(165, 510);
            this.instancesListBox.TabIndex = 0;
            this.instancesListBox.SelectedIndexChanged += new System.EventHandler(this.instancesListBox_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgvValues);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(477, 529);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Свойства экземпляров";
            // 
            // dgvValues
            // 
            this.dgvValues.AutoGenerateColumns = false;
            this.dgvValues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvValues.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EntityAttr,
            this.valueDataGridViewTextBoxColumn});
            this.dgvValues.DataSource = this.attrValueBindingSource;
            this.dgvValues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvValues.Location = new System.Drawing.Point(3, 16);
            this.dgvValues.Name = "dgvValues";
            this.dgvValues.RowHeadersVisible = false;
            this.dgvValues.Size = new System.Drawing.Size(471, 510);
            this.dgvValues.TabIndex = 3;
            // 
            // EntityAttr
            // 
            this.EntityAttr.DataPropertyName = "AttrId";
            this.EntityAttr.DataSource = this.attrBindingSource;
            this.EntityAttr.DisplayMember = "Name";
            this.EntityAttr.HeaderText = "Свойство, атрибут";
            this.EntityAttr.Name = "EntityAttr";
            this.EntityAttr.ValueMember = "Id";
            this.EntityAttr.Width = 200;
            // 
            // attrBindingSource
            // 
            this.attrBindingSource.DataSource = typeof(SOV.Amur.Sys.EntityAttr);
            // 
            // valueDataGridViewTextBoxColumn
            // 
            this.valueDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.valueDataGridViewTextBoxColumn.DataPropertyName = "Value";
            this.valueDataGridViewTextBoxColumn.HeaderText = "Значение";
            this.valueDataGridViewTextBoxColumn.Name = "valueDataGridViewTextBoxColumn";
            // 
            // attrValueBindingSource
            // 
            this.attrValueBindingSource.DataSource = typeof(SOV.Amur.Sys.EntityInstanceValue);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(814, 535);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Журнал событий";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgvLog, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(808, 529);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel4,
            this.entityLogToolStripComboBox,
            this.toolStripLabel2,
            this.dateSToolStripTextBox,
            this.toolStripLabel1,
            this.dateFToolStripTextBox,
            this.refreshToolStripButton,
            this.showParentsOnlyToolStripButton,
            this.toolStripLabel3,
            this.filterLikeToolStripTextBox,
            this.filterLikeToolStripButton,
            this.toolStripSeparator1,
            this.deleteToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(808, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(123, 22);
            this.toolStripLabel4.Text = "Сообщения объекта:";
            // 
            // entityLogToolStripComboBox
            // 
            this.entityLogToolStripComboBox.Name = "entityLogToolStripComboBox";
            this.entityLogToolStripComboBox.Size = new System.Drawing.Size(121, 25);
            this.entityLogToolStripComboBox.TextChanged += new System.EventHandler(this.entityLogToolStripComboBox_TextChanged);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(52, 22);
            this.toolStripLabel2.Text = "Период:";
            // 
            // dateSToolStripTextBox
            // 
            this.dateSToolStripTextBox.Name = "dateSToolStripTextBox";
            this.dateSToolStripTextBox.Size = new System.Drawing.Size(100, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(12, 22);
            this.toolStripLabel1.Text = "-";
            // 
            // dateFToolStripTextBox
            // 
            this.dateFToolStripTextBox.Name = "dateFToolStripTextBox";
            this.dateFToolStripTextBox.Size = new System.Drawing.Size(100, 25);
            // 
            // refreshToolStripButton
            // 
            this.refreshToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.refreshToolStripButton.Image = global::SOV.Amur.Sys.Properties.Resources.refresh_16xMD;
            this.refreshToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.refreshToolStripButton.Name = "refreshToolStripButton";
            this.refreshToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.refreshToolStripButton.Text = "toolStripButton1";
            this.refreshToolStripButton.ToolTipText = "Обновить таблицу";
            this.refreshToolStripButton.Click += new System.EventHandler(this.refreshToolStripButton_Click);
            // 
            // showParentsOnlyToolStripButton
            // 
            this.showParentsOnlyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.showParentsOnlyToolStripButton.Image = global::SOV.Amur.Sys.Properties.Resources.PrimaryKeyHS;
            this.showParentsOnlyToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showParentsOnlyToolStripButton.Name = "showParentsOnlyToolStripButton";
            this.showParentsOnlyToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.showParentsOnlyToolStripButton.Text = "toolStripButton1";
            this.showParentsOnlyToolStripButton.ToolTipText = "Показать только строки-родители или все";
            this.showParentsOnlyToolStripButton.Click += new System.EventHandler(this.showParentsOnlyToolStripButton_Click);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(44, 22);
            this.toolStripLabel3.Text = "Найти:";
            // 
            // filterLikeToolStripTextBox
            // 
            this.filterLikeToolStripTextBox.Name = "filterLikeToolStripTextBox";
            this.filterLikeToolStripTextBox.Size = new System.Drawing.Size(100, 25);
            // 
            // filterLikeToolStripButton
            // 
            this.filterLikeToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.filterLikeToolStripButton.Image = global::SOV.Amur.Sys.Properties.Resources.filter_16xLG;
            this.filterLikeToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.filterLikeToolStripButton.Name = "filterLikeToolStripButton";
            this.filterLikeToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.filterLikeToolStripButton.Text = "toolStripButton1";
            this.filterLikeToolStripButton.Click += new System.EventHandler(this.filterLikeToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // deleteToolStripButton
            // 
            this.deleteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deleteToolStripButton.Image = global::SOV.Amur.Sys.Properties.Resources.DeleteHS;
            this.deleteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteToolStripButton.Name = "deleteToolStripButton";
            this.deleteToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.deleteToolStripButton.Text = "toolStripButton1";
            this.deleteToolStripButton.ToolTipText = "Удалить строки";
            this.deleteToolStripButton.Click += new System.EventHandler(this.deleteToolStripButton_Click);
            // 
            // dgvLog
            // 
            this.dgvLog.AllowUserToAddRows = false;
            this.dgvLog.AllowUserToDeleteRows = false;
            this.dgvLog.AutoGenerateColumns = false;
            this.dgvLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.isUrgentDataGridViewCheckBoxColumn,
            this.messageDataGridViewTextBoxColumn,
            this.dateDataGridViewTextBoxColumn});
            this.dgvLog.DataSource = this.logBindingSource;
            this.dgvLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLog.Location = new System.Drawing.Point(3, 28);
            this.dgvLog.Name = "dgvLog";
            this.dgvLog.ReadOnly = true;
            this.dgvLog.RowHeadersVisible = false;
            this.dgvLog.Size = new System.Drawing.Size(802, 498);
            this.dgvLog.TabIndex = 0;
            // 
            // isUrgentDataGridViewCheckBoxColumn
            // 
            this.isUrgentDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.isUrgentDataGridViewCheckBoxColumn.DataPropertyName = "IsUrgent";
            this.isUrgentDataGridViewCheckBoxColumn.HeaderText = "Критичное сообщение?";
            this.isUrgentDataGridViewCheckBoxColumn.Name = "isUrgentDataGridViewCheckBoxColumn";
            this.isUrgentDataGridViewCheckBoxColumn.ReadOnly = true;
            this.isUrgentDataGridViewCheckBoxColumn.Width = 5;
            // 
            // messageDataGridViewTextBoxColumn
            // 
            this.messageDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.messageDataGridViewTextBoxColumn.DataPropertyName = "Message";
            this.messageDataGridViewTextBoxColumn.HeaderText = "Сообщение";
            this.messageDataGridViewTextBoxColumn.Name = "messageDataGridViewTextBoxColumn";
            this.messageDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dateDataGridViewTextBoxColumn
            // 
            this.dateDataGridViewTextBoxColumn.DataPropertyName = "Date";
            this.dateDataGridViewTextBoxColumn.HeaderText = "Дата";
            this.dateDataGridViewTextBoxColumn.Name = "dateDataGridViewTextBoxColumn";
            this.dateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // logBindingSource
            // 
            this.logBindingSource.DataSource = typeof(SOV.Amur.Sys.Log);
            // 
            // FormSys
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 561);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormSys";
            this.Text = "FormSys";
            this.Load += new System.EventHandler(this.FormSys_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvValues)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.attrBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.attrValueBindingSource)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListBox instancesListBox;
        private System.Windows.Forms.ListBox entityesListBox;
        private System.Windows.Forms.DataGridView dgvLog;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripTextBox dateSToolStripTextBox;
        private System.Windows.Forms.ToolStripTextBox dateFToolStripTextBox;
        private System.Windows.Forms.ToolStripTextBox filterLikeToolStripTextBox;
        private System.Windows.Forms.DataGridView dgvValues;
        private System.Windows.Forms.ToolStripButton refreshToolStripButton;
        private System.Windows.Forms.ToolStripButton deleteToolStripButton;
        private System.Windows.Forms.ToolStripButton showParentsOnlyToolStripButton;
        private System.Windows.Forms.ToolStripButton filterLikeToolStripButton;
        private System.Windows.Forms.BindingSource logBindingSource;
        private System.Windows.Forms.BindingSource attrValueBindingSource;
        private System.Windows.Forms.BindingSource attrBindingSource;
        private System.Windows.Forms.ToolStripComboBox entityLogToolStripComboBox;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridViewComboBoxColumn EntityAttr;
        private System.Windows.Forms.DataGridViewTextBoxColumn valueDataGridViewTextBoxColumn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isUrgentDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn messageDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateDataGridViewTextBoxColumn;
    }
}