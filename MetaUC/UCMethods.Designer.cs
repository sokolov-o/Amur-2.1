namespace SOV.Amur.Meta
{
    partial class UCMethods
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
            SOV.Amur.Meta.Method method1 = new SOV.Amur.Meta.Method();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCMethods));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.viewTypeComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvMethods = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MethodName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.methodBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ucMethod = new SOV.Amur.Meta.UCMethod();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.showHideMethodDetailsToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.dataFilterToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.idDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.parentIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sourceLegalEntityIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.orderDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.methodOutputStoreParametersDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.methodForecastDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMethods)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.methodBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.toolStrip, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(647, 473);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // toolStrip
            // 
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewTypeComboBox});
            this.toolStrip.Location = new System.Drawing.Point(49, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip.Size = new System.Drawing.Size(598, 25);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip1";
            // 
            // viewTypeComboBox
            // 
            this.viewTypeComboBox.Name = "viewTypeComboBox";
            this.viewTypeComboBox.Size = new System.Drawing.Size(150, 25);
            this.viewTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.viewTypeComboBox_SelectedIndexChanged);
            // 
            // splitContainer1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.splitContainer1, 2);
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 28);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvMethods);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(641, 442);
            this.splitContainer1.SplitterDistance = 276;
            this.splitContainer1.TabIndex = 1;
            // 
            // dgvMethods
            // 
            this.dgvMethods.AllowUserToAddRows = false;
            this.dgvMethods.AllowUserToDeleteRows = false;
            this.dgvMethods.AutoGenerateColumns = false;
            this.dgvMethods.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMethods.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.MethodName});
            this.dgvMethods.DataSource = this.methodBindingSource;
            this.dgvMethods.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMethods.Location = new System.Drawing.Point(0, 0);
            this.dgvMethods.Name = "dgvMethods";
            this.dgvMethods.ReadOnly = true;
            this.dgvMethods.RowHeadersVisible = false;
            this.dgvMethods.Size = new System.Drawing.Size(276, 442);
            this.dgvMethods.TabIndex = 0;
            this.dgvMethods.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMethods_CellEnter);
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn.HeaderText = "Id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.ReadOnly = true;
            this.idDataGridViewTextBoxColumn.Width = 41;
            // 
            // MethodName
            // 
            this.MethodName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.MethodName.DataPropertyName = "Name";
            this.MethodName.HeaderText = "Метод";
            this.MethodName.Name = "MethodName";
            this.MethodName.ReadOnly = true;
            // 
            // methodBindingSource
            // 
            this.methodBindingSource.DataSource = typeof(SOV.Amur.Meta.Method);
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer2.Size = new System.Drawing.Size(308, 443);
            this.splitContainer2.SplitterDistance = 288;
            this.splitContainer2.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ucMethod);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(304, 284);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Метод";
            // 
            // ucMethod
            // 
            this.ucMethod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMethod.Location = new System.Drawing.Point(3, 16);
            method1.Description = "";
            method1.Entity = null;
            method1.Id = -1;
            method1.MethodOutputStoreParameters = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("method1.MethodOutputStoreParameters")));
            method1.Name = "";
            method1.Order = ((short)(32767));
            method1.ParentId = null;
            method1.SourceLegalEntityId = null;
            this.ucMethod.Method = method1;
            this.ucMethod.Name = "ucMethod";
            this.ucMethod.Size = new System.Drawing.Size(298, 265);
            this.ucMethod.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showHideMethodDetailsToolStripButton,
            this.dataFilterToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(49, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // showHideMethodDetailsToolStripButton
            // 
            this.showHideMethodDetailsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.showHideMethodDetailsToolStripButton.Image = global::SOV.Amur.Meta.Properties.Resources.Property_501;
            this.showHideMethodDetailsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showHideMethodDetailsToolStripButton.Name = "showHideMethodDetailsToolStripButton";
            this.showHideMethodDetailsToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.showHideMethodDetailsToolStripButton.Text = "toolStripButton1";
            this.showHideMethodDetailsToolStripButton.ToolTipText = "Показать/скрыть детали метода";
            this.showHideMethodDetailsToolStripButton.Click += new System.EventHandler(this.showHideMethodDetailsToolStripButton_Click);
            // 
            // dataFilterToolStripButton
            // 
            this.dataFilterToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.dataFilterToolStripButton.Image = global::SOV.Amur.Meta.Properties.Resources.filter_16xLG;
            this.dataFilterToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.dataFilterToolStripButton.Name = "dataFilterToolStripButton";
            this.dataFilterToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.dataFilterToolStripButton.Text = "toolStripButton1";
            this.dataFilterToolStripButton.ToolTipText = "Установка фильтра климатических данных";
            this.dataFilterToolStripButton.Click += new System.EventHandler(this.dataFilterToolStripButton_Click);
            // 
            // idDataGridViewTextBoxColumn1
            // 
            this.idDataGridViewTextBoxColumn1.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn1.HeaderText = "Id";
            this.idDataGridViewTextBoxColumn1.Name = "idDataGridViewTextBoxColumn1";
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            this.descriptionDataGridViewTextBoxColumn.HeaderText = "Description";
            this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            // 
            // parentIdDataGridViewTextBoxColumn
            // 
            this.parentIdDataGridViewTextBoxColumn.DataPropertyName = "ParentId";
            this.parentIdDataGridViewTextBoxColumn.HeaderText = "ParentId";
            this.parentIdDataGridViewTextBoxColumn.Name = "parentIdDataGridViewTextBoxColumn";
            // 
            // sourceLegalEntityIdDataGridViewTextBoxColumn
            // 
            this.sourceLegalEntityIdDataGridViewTextBoxColumn.DataPropertyName = "SourceLegalEntityId";
            this.sourceLegalEntityIdDataGridViewTextBoxColumn.HeaderText = "SourceLegalEntityId";
            this.sourceLegalEntityIdDataGridViewTextBoxColumn.Name = "sourceLegalEntityIdDataGridViewTextBoxColumn";
            // 
            // orderDataGridViewTextBoxColumn
            // 
            this.orderDataGridViewTextBoxColumn.DataPropertyName = "Order";
            this.orderDataGridViewTextBoxColumn.HeaderText = "Order";
            this.orderDataGridViewTextBoxColumn.Name = "orderDataGridViewTextBoxColumn";
            // 
            // methodOutputStoreParametersDataGridViewTextBoxColumn
            // 
            this.methodOutputStoreParametersDataGridViewTextBoxColumn.DataPropertyName = "MethodOutputStoreParameters";
            this.methodOutputStoreParametersDataGridViewTextBoxColumn.HeaderText = "MethodOutputStoreParameters";
            this.methodOutputStoreParametersDataGridViewTextBoxColumn.Name = "methodOutputStoreParametersDataGridViewTextBoxColumn";
            // 
            // methodForecastDataGridViewTextBoxColumn
            // 
            this.methodForecastDataGridViewTextBoxColumn.DataPropertyName = "MethodForecast";
            this.methodForecastDataGridViewTextBoxColumn.HeaderText = "MethodForecast";
            this.methodForecastDataGridViewTextBoxColumn.Name = "methodForecastDataGridViewTextBoxColumn";
            // 
            // UCMethods
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UCMethods";
            this.Size = new System.Drawing.Size(647, 473);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMethods)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.methodBindingSource)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripComboBox viewTypeComboBox;
        private System.Windows.Forms.BindingSource methodBindingSource;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvMethods;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn MethodName;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox1;
        private UCMethod ucMethod;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton showHideMethodDetailsToolStripButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn parentIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sourceLegalEntityIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn orderDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn methodOutputStoreParametersDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn methodForecastDataGridViewTextBoxColumn;
        private System.Windows.Forms.ToolStripButton dataFilterToolStripButton;
    }
}
