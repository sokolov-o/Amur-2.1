namespace SOV.Amur.Meta
{
    partial class FormStations
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
            SOV.Amur.Meta.Site station1 = new SOV.Amur.Meta.Site();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStations));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ucStations = new SOV.Amur.Meta.UCStations();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ucStationEdit = new SOV.Amur.Meta.UCSiteEdit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
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
            this.splitContainer1.Size = new System.Drawing.Size(1224, 587);
            this.splitContainer1.SplitterDistance = 451;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ucStations);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(451, 587);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Журнал станций";
            // 
            // ucStations
            // 
            this.ucStations.Cursor = System.Windows.Forms.Cursors.Default;
            this.ucStations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucStations.EnableMenuStrip = true;
            this.ucStations.Location = new System.Drawing.Point(3, 16);
            this.ucStations.MultySelect = false;
            this.ucStations.Name = "ucStations";
            this.ucStations.SerchSiteInputSize = new System.Drawing.Size(50, 25);
            this.ucStations.SiteGroup = null;
            this.ucStations.SiteGroupId = null;
            this.ucStations.Size = new System.Drawing.Size(445, 568);
            this.ucStations.TabIndex = 0;
            this.ucStations.VisibleAddNewButton = true;
            this.ucStations.VisibleComplateButton = false;
            this.ucStations.VisibleEditStationButton = true;
            this.ucStations.VisibleNoSiteButton = true;
            this.ucStations.VisibleSiteGroups = true;
            this.ucStations.UCSelectedStationChangedEvent += new SOV.Amur.Meta.UCStations.UCSelectedStationChangedEventHandler(this.ucStations_UCSelectedStationChangedEvent);
            this.ucStations.UCEditStationEvent += new SOV.Amur.Meta.UCStations.UCEditStationEventHandler(this.ucStations_UCEditStationEvent);
            this.ucStations.UCNewStationEvent += new SOV.Amur.Meta.UCStations.UCNewStationEventHandler(this.ucStations_UCNewStationEvent);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ucStationEdit);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(769, 587);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Станция";
            // 
            // ucStationEdit
            // 
            this.ucStationEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucStationEdit.EnableSites = true;
            this.ucStationEdit.Location = new System.Drawing.Point(3, 16);
            this.ucStationEdit.Name = "ucStationEdit";
            this.ucStationEdit.Size = new System.Drawing.Size(763, 568);
            station1.AddrRegionId = null;
            station1.Code = "";
            station1.Id = -1;
            station1.Name = "";
            station1.OrgId = null;
            station1.TypeId = -2147483648;
            station1.AddrRegionId = null;
            this.ucStationEdit.Site = station1;
            this.ucStationEdit.TabIndex = 0;
            // 
            // FormStations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1224, 587);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormStations";
            this.Text = "Станции";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private UCStations ucStations;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private UCSiteEdit ucStationEdit;
    }
}