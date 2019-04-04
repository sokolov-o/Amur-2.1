namespace SOV.Social
{
    partial class UCStaffPositions
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
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameRus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameRusShort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameEng = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameEngShort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.staffPositionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffPositionBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AutoGenerateColumns = false;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.nameRus,
            this.nameRusShort,
            this.nameEng,
            this.nameEngShort});
            this.dgv.DataSource = this.staffPositionBindingSource;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(0, 0);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.Size = new System.Drawing.Size(601, 203);
            this.dgv.TabIndex = 0;
            this.dgv.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgv_MouseDoubleClick);
            // 
            // id
            // 
            this.id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.id.DataPropertyName = "Id";
            this.id.HeaderText = "Id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Width = 41;
            // 
            // nameRus
            // 
            this.nameRus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nameRus.DataPropertyName = "NameRus";
            this.nameRus.HeaderText = "Название (рус)";
            this.nameRus.Name = "nameRus";
            this.nameRus.ReadOnly = true;
            // 
            // nameRusShort
            // 
            this.nameRusShort.DataPropertyName = "NameRusShort";
            this.nameRusShort.HeaderText = "Кратко";
            this.nameRusShort.Name = "nameRusShort";
            this.nameRusShort.ReadOnly = true;
            // 
            // nameEng
            // 
            this.nameEng.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nameEng.DataPropertyName = "NameEng";
            this.nameEng.HeaderText = "Название (анг)";
            this.nameEng.Name = "nameEng";
            this.nameEng.ReadOnly = true;
            // 
            // nameEngShort
            // 
            this.nameEngShort.DataPropertyName = "NameEngShort";
            this.nameEngShort.HeaderText = "Кратко";
            this.nameEngShort.Name = "nameEngShort";
            this.nameEngShort.ReadOnly = true;
            // 
            // staffPositionBindingSource
            // 
            this.staffPositionBindingSource.DataSource = typeof(SOV.Social.StaffPosition);
            // 
            // UCStaffPositions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgv);
            this.Name = "UCStaffPositions";
            this.Size = new System.Drawing.Size(601, 203);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffPositionBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.BindingSource staffPositionBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameRus;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameRusShort;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameEng;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameEngShort;
    }
}
