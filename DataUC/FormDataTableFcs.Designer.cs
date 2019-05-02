namespace SOV.Amur.Data
{
    partial class FormDataTableFcs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDataTableFcs));
            this.ucDataFcsTable = new SOV.Amur.Data.UCDataTableFcs();
            this.SuspendLayout();
            // 
            // ucDataFcsTable
            // 
            this.ucDataFcsTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDataFcsTable.FormDataFilter = null;
            this.ucDataFcsTable.IsDateFcs = false;
            this.ucDataFcsTable.Location = new System.Drawing.Point(0, 0);
            this.ucDataFcsTable.Name = "ucDataFcsTable";
            this.ucDataFcsTable.Size = new System.Drawing.Size(826, 585);
            this.ucDataFcsTable.TabIndex = 0;
            // 
            // FormDataTableFcs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 585);
            this.Controls.Add(this.ucDataFcsTable);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormDataTableFcs";
            this.Text = "Журнал прогнозов";
            this.Load += new System.EventHandler(this.FormDataTableFcs_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UCDataTableFcs ucDataFcsTable;
    }
}