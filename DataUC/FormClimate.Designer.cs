namespace SOV.Amur.Data
{
    partial class FormClimate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormClimate));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ucMethods = new SOV.Amur.Meta.UCMethods();
            this.ucDataTable = new SOV.Amur.Data.UCDataTable();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ucMethods);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(355, 505);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Методы климатической обработки данных";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(826, 505);
            this.splitContainer1.SplitterDistance = 355;
            this.splitContainer1.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ucDataTable);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(467, 505);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Данные";
            // 
            // ucMethods
            // 
            this.ucMethods.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMethods.Location = new System.Drawing.Point(3, 16);
            this.ucMethods.Name = "ucMethods";
            this.ucMethods.Size = new System.Drawing.Size(349, 486);
            this.ucMethods.TabIndex = 0;
            this.ucMethods.UCToolbarVisible = true;
            this.ucMethods.UCCurrentMethodChangedEvent += new SOV.Amur.Meta.UCMethods.UCCurrentMethodChangedEventHandler(this.ucMethods_UCCurrentMethodChangedEvent);
            this.ucMethods.UCDataFilterChangedEvent += new SOV.Amur.Meta.UCMethods.UCDataFilterChangedEventHandler(this.ucMethods_UCDataFilterChangedEvent);
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
            this.ucDataTable.Location = new System.Drawing.Point(3, 16);
            this.ucDataTable.Name = "ucDataTable";
            this.ucDataTable.ShowChart = true;
            this.ucDataTable.ShowDataDetails = false;
            this.ucDataTable.ShowFilterButton = true;
            this.ucDataTable.Size = new System.Drawing.Size(461, 486);
            this.ucDataTable.TabIndex = 0;
            this.ucDataTable.TimeType = SOV.Amur.Meta.EnumDateType.UTC;
            this.ucDataTable.UserDirExportSAV = null;
            this.ucDataTable.UserOrganisationId = -1;
            // 
            // FormClimate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 505);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormClimate";
            this.Text = "FormClimate";
            this.groupBox1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private Meta.UCMethods ucMethods;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox2;
        private UCDataTable ucDataTable;
    }
}