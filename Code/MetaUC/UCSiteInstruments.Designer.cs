namespace SOV.Amur.Meta
{
    partial class UCSiteInstruments
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
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
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.instrumentBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.siteInstrumentBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.infoLabel = new System.Windows.Forms.ToolStripLabel();
            this.addButton = new System.Windows.Forms.ToolStripButton();
            this.deleteButton = new System.Windows.Forms.ToolStripButton();
            this.saveButton = new System.Windows.Forms.ToolStripButton();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsModify = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsNew = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsDelete = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.instrumentIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dateSDGVC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateFDGVC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.locationDescriptionDGVC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.instrumentBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.siteInstrumentBindingSource)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AutoGenerateColumns = false;
            this.dgv.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.IsModify,
            this.IsNew,
            this.IsDelete,
            this.instrumentIdDataGridViewTextBoxColumn,
            this.dateSDGVC,
            this.dateFDGVC,
            this.locationDescriptionDGVC,
            this.descriptionDataGridViewTextBoxColumn});
            this.dgv.DataSource = this.siteInstrumentBindingSource;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(1, 26);
            this.dgv.Margin = new System.Windows.Forms.Padding(1);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.Size = new System.Drawing.Size(624, 97);
            this.dgv.TabIndex = 0;
            this.dgv.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.dgv.Paint += new System.Windows.Forms.PaintEventHandler(this.dataGrid_Paint);
            // 
            // instrumentBindingSource
            // 
            this.instrumentBindingSource.DataSource = typeof(SOV.Amur.Meta.Instrument);
            // 
            // siteInstrumentBindingSource
            // 
            this.siteInstrumentBindingSource.DataSource = typeof(SOV.Amur.Meta.SiteInstrument);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dgv, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(626, 124);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(23, 22);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.infoLabel,
            this.addButton,
            this.deleteButton,
            this.saveButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip1.Size = new System.Drawing.Size(626, 25);
            this.toolStrip1.TabIndex = 1;
            // 
            // infoLabel
            // 
            this.infoLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(16, 22);
            this.infoLabel.Text = "...";
            // 
            // addButton
            // 
            this.addButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addButton.Image = global::SOV.Amur.Meta.Properties.Resources.action_add_16xLG1;
            this.addButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.addButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(23, 22);
            this.addButton.Text = "Добавить";
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deleteButton.Image = global::SOV.Amur.Meta.Properties.Resources.DeleteHS;
            this.deleteButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.deleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(23, 22);
            this.deleteButton.Text = "Удалить";
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveButton.Image = global::SOV.Amur.Meta.Properties.Resources.SaveBlack;
            this.saveButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(23, 22);
            this.saveButton.Text = "Сохранить";
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.Visible = false;
            // 
            // IsModify
            // 
            this.IsModify.HeaderText = "IsModify";
            this.IsModify.Name = "IsModify";
            this.IsModify.Visible = false;
            // 
            // IsNew
            // 
            this.IsNew.HeaderText = "IsNew";
            this.IsNew.IndeterminateValue = "";
            this.IsNew.Name = "IsNew";
            this.IsNew.Visible = false;
            // 
            // IsDelete
            // 
            this.IsDelete.HeaderText = "IsDelete";
            this.IsDelete.Name = "IsDelete";
            this.IsDelete.Visible = false;
            // 
            // instrumentIdDataGridViewTextBoxColumn
            // 
            this.instrumentIdDataGridViewTextBoxColumn.DataPropertyName = "InstrumentId";
            this.instrumentIdDataGridViewTextBoxColumn.DataSource = this.instrumentBindingSource;
            this.instrumentIdDataGridViewTextBoxColumn.DisplayMember = "NameRus";
            this.instrumentIdDataGridViewTextBoxColumn.HeaderText = "Инструмент";
            this.instrumentIdDataGridViewTextBoxColumn.Name = "instrumentIdDataGridViewTextBoxColumn";
            this.instrumentIdDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.instrumentIdDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.instrumentIdDataGridViewTextBoxColumn.ValueMember = "Id";
            this.instrumentIdDataGridViewTextBoxColumn.Width = 93;
            // 
            // dateSDGVC
            // 
            this.dateSDGVC.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dateSDGVC.DataPropertyName = "DateS";
            dataGridViewCellStyle1.Format = "dd.MM.yyyy";
            dataGridViewCellStyle1.NullValue = null;
            this.dateSDGVC.DefaultCellStyle = dataGridViewCellStyle1;
            this.dateSDGVC.HeaderText = "С даты";
            this.dateSDGVC.Name = "dateSDGVC";
            this.dateSDGVC.Width = 67;
            // 
            // dateFDGVC
            // 
            this.dateFDGVC.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dateFDGVC.DataPropertyName = "DateF";
            dataGridViewCellStyle2.Format = "dd.MM.yyyy";
            dataGridViewCellStyle2.NullValue = null;
            this.dateFDGVC.DefaultCellStyle = dataGridViewCellStyle2;
            this.dateFDGVC.HeaderText = "По дату";
            this.dateFDGVC.Name = "dateFDGVC";
            this.dateFDGVC.Width = 71;
            // 
            // locationDescriptionDGVC
            // 
            this.locationDescriptionDGVC.DataPropertyName = "LocationDescription";
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.locationDescriptionDGVC.DefaultCellStyle = dataGridViewCellStyle3;
            this.locationDescriptionDGVC.HeaderText = "Место установки";
            this.locationDescriptionDGVC.Name = "locationDescriptionDGVC";
            this.locationDescriptionDGVC.Width = 289;
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            this.descriptionDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            this.descriptionDataGridViewTextBoxColumn.HeaderText = "Примечание";
            this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            this.descriptionDataGridViewTextBoxColumn.Width = 95;
            // 
            // UCSiteInstruments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "UCSiteInstruments";
            this.Size = new System.Drawing.Size(626, 124);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.instrumentBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.siteInstrumentBindingSource)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource siteInstrumentBindingSource;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.BindingSource instrumentBindingSource;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel infoLabel;
        private System.Windows.Forms.ToolStripButton saveButton;
        private System.Windows.Forms.ToolStripButton addButton;
        private System.Windows.Forms.ToolStripButton deleteButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsModify;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsNew;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsDelete;
        private System.Windows.Forms.DataGridViewComboBoxColumn instrumentIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateSDGVC;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateFDGVC;
        private System.Windows.Forms.DataGridViewTextBoxColumn locationDescriptionDGVC;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
    }
}
