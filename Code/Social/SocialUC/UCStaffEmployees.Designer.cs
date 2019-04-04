namespace SOV.Social
{
    partial class UCStaffEmployees
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.refreshButton = new System.Windows.Forms.ToolStripButton();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewComboBoxColumn1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.infoLabel = new System.Windows.Forms.ToolStripLabel();
            this.staffBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.employeeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.staffEmployeeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.staffDGVC = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.editStaffDGVC = new System.Windows.Forms.DataGridViewButtonColumn();
            this.employeeDGVC = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.editEmployeeDGVC = new System.Windows.Forms.DataGridViewButtonColumn();
            this.percent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateSDGVC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateFDGVC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.employeeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffEmployeeBindingSource)).BeginInit();
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
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(7);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1458, 361);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(36, 36);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshButton,
            this.infoLabel});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(1458, 43);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // refreshButton
            // 
            this.refreshButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.refreshButton.Image = global::SOV.Social.Properties.Resources.refresh_16xLG;
            this.refreshButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(40, 40);
            this.refreshButton.Text = "Обновить таблицу";
            // 
            // dgv
            // 
            this.dgv.AutoGenerateColumns = false;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.staffDGVC,
            this.editStaffDGVC,
            this.employeeDGVC,
            this.editEmployeeDGVC,
            this.percent,
            this.dateSDGVC,
            this.dateFDGVC});
            this.dgv.DataSource = this.staffEmployeeBindingSource;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(7, 50);
            this.dgv.Margin = new System.Windows.Forms.Padding(7);
            this.dgv.Name = "dgv";
            this.dgv.Size = new System.Drawing.Size(1444, 304);
            this.dgv.TabIndex = 1;
            this.dgv.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellClick);
            this.dgv.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellEndEdit);
            this.dgv.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgv_UserDeletingRow);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Employee";
            this.dataGridViewTextBoxColumn1.HeaderText = "Работник";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewComboBoxColumn1
            // 
            this.dataGridViewComboBoxColumn1.DataPropertyName = "StaffPosition";
            this.dataGridViewComboBoxColumn1.HeaderText = "Должность";
            this.dataGridViewComboBoxColumn1.Name = "dataGridViewComboBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Employer";
            this.dataGridViewTextBoxColumn2.HeaderText = "Работодатель";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // infoLabel
            // 
            this.infoLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(35, 40);
            this.infoLabel.Text = "...";
            // 
            // staffBindingSource
            // 
            this.staffBindingSource.DataSource = typeof(SOV.Social.Staff);
            // 
            // employeeBindingSource
            // 
            this.employeeBindingSource.DataSource = typeof(SOV.Social.LegalEntity);
            // 
            // staffEmployeeBindingSource
            // 
            this.staffEmployeeBindingSource.DataSource = typeof(SOV.Social.StaffEmployee);
            this.staffEmployeeBindingSource.AddingNew += new System.ComponentModel.AddingNewEventHandler(this.StaffBindingSource_AddingNew);
            // 
            // staffDGVC
            // 
            this.staffDGVC.DataPropertyName = "StaffId";
            this.staffDGVC.DataSource = this.staffBindingSource;
            this.staffDGVC.DisplayMember = "NameFull";
            this.staffDGVC.HeaderText = "Подразделение и должность";
            this.staffDGVC.Name = "staffDGVC";
            this.staffDGVC.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.staffDGVC.ValueMember = "Id";
            this.staffDGVC.Width = 686;
            // 
            // editStaffDGVC
            // 
            this.editStaffDGVC.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.editStaffDGVC.HeaderText = "...";
            this.editStaffDGVC.Name = "editStaffDGVC";
            this.editStaffDGVC.Text = "...";
            this.editStaffDGVC.Width = 37;
            // 
            // employeeDGVC
            // 
            this.employeeDGVC.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.employeeDGVC.DataPropertyName = "EmployeeId";
            this.employeeDGVC.DataSource = this.employeeBindingSource;
            this.employeeDGVC.DisplayMember = "NameRusShort";
            this.employeeDGVC.HeaderText = "ФИО";
            this.employeeDGVC.Name = "employeeDGVC";
            this.employeeDGVC.ReadOnly = true;
            this.employeeDGVC.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.employeeDGVC.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.employeeDGVC.ValueMember = "Id";
            // 
            // editEmployeeDGVC
            // 
            this.editEmployeeDGVC.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.editEmployeeDGVC.HeaderText = "...";
            this.editEmployeeDGVC.Name = "editEmployeeDGVC";
            this.editEmployeeDGVC.Text = "...";
            this.editEmployeeDGVC.Width = 37;
            // 
            // percent
            // 
            this.percent.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.percent.DataPropertyName = "Percent";
            this.percent.HeaderText = "%";
            this.percent.Name = "percent";
            this.percent.Width = 84;
            // 
            // dateSDGVC
            // 
            this.dateSDGVC.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dateSDGVC.DataPropertyName = "DateS";
            this.dateSDGVC.HeaderText = "Начало";
            this.dateSDGVC.Name = "dateSDGVC";
            this.dateSDGVC.Width = 148;
            // 
            // dateFDGVC
            // 
            this.dateFDGVC.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dateFDGVC.DataPropertyName = "DateF";
            this.dateFDGVC.HeaderText = "Окончание";
            this.dateFDGVC.Name = "dateFDGVC";
            this.dateFDGVC.Width = 193;
            // 
            // UCStaffEmployees
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(7);
            this.Name = "UCStaffEmployees";
            this.Size = new System.Drawing.Size(1458, 361);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.employeeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffEmployeeBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.ToolStripButton refreshButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.BindingSource staffEmployeeBindingSource;
        private System.Windows.Forms.BindingSource staffBindingSource;
        private System.Windows.Forms.ToolStripLabel infoLabel;
        private System.Windows.Forms.BindingSource employeeBindingSource;
        private System.Windows.Forms.DataGridViewComboBoxColumn staffDGVC;
        private System.Windows.Forms.DataGridViewButtonColumn editStaffDGVC;
        private System.Windows.Forms.DataGridViewComboBoxColumn employeeDGVC;
        private System.Windows.Forms.DataGridViewButtonColumn editEmployeeDGVC;
        private System.Windows.Forms.DataGridViewTextBoxColumn percent;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateSDGVC;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateFDGVC;
    }
}
