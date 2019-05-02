namespace SOV.Amur.Data
{
    partial class UCDataAQC
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
            this.dgv = new System.Windows.Forms.DataGridView();
            this.dgvIsRoleApplied = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvRoleDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvIsRoleApplied,
            this.dgvRoleDescription});
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(0, 0);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.Size = new System.Drawing.Size(271, 227);
            this.dgv.TabIndex = 0;
            // 
            // dgvIsRoleApplied
            // 
            this.dgvIsRoleApplied.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.dgvIsRoleApplied.HeaderText = "+";
            this.dgvIsRoleApplied.Name = "dgvIsRoleApplied";
            this.dgvIsRoleApplied.ReadOnly = true;
            this.dgvIsRoleApplied.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvIsRoleApplied.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dgvIsRoleApplied.Width = 38;
            // 
            // dgvRoleDescription
            // 
            this.dgvRoleDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvRoleDescription.HeaderText = "Правило АКК";
            this.dgvRoleDescription.Name = "dgvRoleDescription";
            this.dgvRoleDescription.ReadOnly = true;
            // 
            // UCDataAQC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgv);
            this.Name = "UCDataAQC";
            this.Size = new System.Drawing.Size(271, 227);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn aQCRoleDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvIsRoleApplied;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvRoleDescription;
    }
}
