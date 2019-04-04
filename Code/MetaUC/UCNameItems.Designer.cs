namespace SOV.Amur.Meta
{
    partial class UCNameItems
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
            this.dgv = new System.Windows.Forms.DataGridView();
            this.tlp = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.langBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.nameTypeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.nameItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.saveButton = new System.Windows.Forms.ToolStripButton();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.langIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.nameTypeIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.tlp.SuspendLayout();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.langBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nameTypeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nameItemBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AutoGenerateColumns = false;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn,
            this.langIdDataGridViewTextBoxColumn,
            this.nameTypeIdDataGridViewTextBoxColumn});
            this.dgv.DataSource = this.nameItemBindingSource;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(3, 28);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.Size = new System.Drawing.Size(407, 262);
            this.dgv.TabIndex = 0;
            // 
            // tlp
            // 
            this.tlp.ColumnCount = 1;
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp.Controls.Add(this.dgv, 0, 1);
            this.tlp.Controls.Add(this.toolStrip, 0, 0);
            this.tlp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp.Location = new System.Drawing.Point(0, 0);
            this.tlp.Name = "tlp";
            this.tlp.RowCount = 2;
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp.Size = new System.Drawing.Size(413, 293);
            this.tlp.TabIndex = 1;
            // 
            // toolStrip
            // 
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveButton});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(413, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip1";
            // 
            // langBindingSource
            // 
            this.langBindingSource.DataSource = typeof(SOV.Common.IdName);
            // 
            // nameTypeBindingSource
            // 
            this.nameTypeBindingSource.DataSource = typeof(SOV.Common.IdName);
            // 
            // nameItemBindingSource
            // 
            this.nameItemBindingSource.DataSource = typeof(SOV.Amur.Meta.NameItem);
            this.nameItemBindingSource.AddingNew += new System.ComponentModel.AddingNewEventHandler(this.nameItemBindingSource_AddingNew);
            // 
            // saveButton
            // 
            this.saveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveButton.Image = global::SOV.Amur.Meta.Properties.Resources.SaveBlack;
            this.saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(23, 22);
            this.saveButton.Text = "toolStripButton1";
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Наименование";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            // 
            // langIdDataGridViewTextBoxColumn
            // 
            this.langIdDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.langIdDataGridViewTextBoxColumn.DataPropertyName = "LangId";
            this.langIdDataGridViewTextBoxColumn.DataSource = this.langBindingSource;
            this.langIdDataGridViewTextBoxColumn.DisplayMember = "Name";
            this.langIdDataGridViewTextBoxColumn.HeaderText = "Язык";
            this.langIdDataGridViewTextBoxColumn.Name = "langIdDataGridViewTextBoxColumn";
            this.langIdDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.langIdDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.langIdDataGridViewTextBoxColumn.ValueMember = "Id";
            this.langIdDataGridViewTextBoxColumn.Width = 60;
            // 
            // nameTypeIdDataGridViewTextBoxColumn
            // 
            this.nameTypeIdDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.nameTypeIdDataGridViewTextBoxColumn.DataPropertyName = "NameTypeId";
            this.nameTypeIdDataGridViewTextBoxColumn.DataSource = this.nameTypeBindingSource;
            this.nameTypeIdDataGridViewTextBoxColumn.DisplayMember = "Name";
            this.nameTypeIdDataGridViewTextBoxColumn.HeaderText = "Тип";
            this.nameTypeIdDataGridViewTextBoxColumn.Name = "nameTypeIdDataGridViewTextBoxColumn";
            this.nameTypeIdDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.nameTypeIdDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.nameTypeIdDataGridViewTextBoxColumn.ValueMember = "Id";
            // 
            // UCNameItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlp);
            this.Name = "UCNameItems";
            this.Size = new System.Drawing.Size(413, 293);
            this.Leave += new System.EventHandler(this.UCNameItems_Leave);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.tlp.ResumeLayout(false);
            this.tlp.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.langBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nameTypeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nameItemBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.TableLayoutPanel tlp;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.BindingSource nameItemBindingSource;
        private System.Windows.Forms.ToolStripButton saveButton;
        private System.Windows.Forms.BindingSource langBindingSource;
        private System.Windows.Forms.BindingSource nameTypeBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn langIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn nameTypeIdDataGridViewTextBoxColumn;
    }
}
