namespace SOV.Amur.Data
{
    partial class FormDataTable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDataTable));
            this.ucDataTable = new SOV.Amur.Data.UCDataTable();
            this.SuspendLayout();
            // 
            // ucDataTable
            // 
            this.ucDataTable.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ucDataTable.CurDateTime = null;
            this.ucDataTable.CurSiteId = null;
            this.ucDataTable.Cursor = System.Windows.Forms.Cursors.Default;
            this.ucDataTable.CurVariableId = null;
            this.ucDataTable.CurViewType = SOV.Amur.Data.UCDataTable.ViewType.Date_RStations_CVariables;
            this.ucDataTable.DataFilter = null;
            this.ucDataTable.DataFilterEnabled = true;
            this.ucDataTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDataTable.Location = new System.Drawing.Point(0, 0);
            this.ucDataTable.Name = "ucDataTable";
            this.ucDataTable.ShowChart = true;
            this.ucDataTable.ShowDataDetails = false;
            this.ucDataTable.ShowFilterButton = true;
            this.ucDataTable.Size = new System.Drawing.Size(620, 404);
            this.ucDataTable.TabIndex = 0;
            this.ucDataTable.TimeType = SOV.Amur.Meta.EnumDateType.UTC;
            this.ucDataTable.UserDirExportSAV = null;
            this.ucDataTable.UserOrganisationId = -1;
            // 
            // FormDataTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 404);
            this.Controls.Add(this.ucDataTable);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormDataTable";
            this.Text = "Таблица данных (форма 2)";
            this.ResumeLayout(false);

        }

        #endregion

        private UCDataTable ucDataTable;
    }
}