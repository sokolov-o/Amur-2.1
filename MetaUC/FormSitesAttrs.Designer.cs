namespace SOV.Amur.Meta
{
    partial class FormSitesAttrs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSitesAttrs));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.refreshSitesButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.dateActualTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.siteAttrsDropDown = new System.Windows.Forms.ToolStripDropDownButton();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.eavGroupBox = new System.Windows.Forms.GroupBox();
            this.saveEAVButton = new System.Windows.Forms.Button();
            this.ucSites = new SOV.Amur.Meta.UCSiteGeoObjectList();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.eavGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.button1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(416, 454);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshSitesButton,
            this.toolStripLabel1,
            this.dateActualTextBox,
            this.siteAttrsDropDown});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(416, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // refreshSitesButton
            // 
            this.refreshSitesButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.refreshSitesButton.Image = ((System.Drawing.Image)(resources.GetObject("refreshSitesButton.Image")));
            this.refreshSitesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.refreshSitesButton.Name = "refreshSitesButton";
            this.refreshSitesButton.Size = new System.Drawing.Size(23, 22);
            this.refreshSitesButton.Text = "Обновить атрибуты пунктов";
            this.refreshSitesButton.Click += new System.EventHandler(this.refreshSitesButton_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(171, 22);
            this.toolStripLabel1.Text = "Дата актуальности атрибутов:";
            // 
            // dateActualTextBox
            // 
            this.dateActualTextBox.Name = "dateActualTextBox";
            this.dateActualTextBox.Size = new System.Drawing.Size(70, 25);
            // 
            // siteAttrsDropDown
            // 
            this.siteAttrsDropDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.siteAttrsDropDown.Image = ((System.Drawing.Image)(resources.GetObject("siteAttrsDropDown.Image")));
            this.siteAttrsDropDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.siteAttrsDropDown.Name = "siteAttrsDropDown";
            this.siteAttrsDropDown.Size = new System.Drawing.Size(114, 22);
            this.siteAttrsDropDown.Text = "Атрибуты сайтов";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 428);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Закрыть";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ucSites);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(410, 394);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Пункты";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(671, 456);
            this.splitContainer1.SplitterDistance = 418;
            this.splitContainer1.TabIndex = 2;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.eavGroupBox);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.saveEAVButton);
            this.splitContainer2.Size = new System.Drawing.Size(247, 454);
            this.splitContainer2.SplitterDistance = 234;
            this.splitContainer2.TabIndex = 2;
            // 
            // eavGroupBox
            // 
            this.eavGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eavGroupBox.Location = new System.Drawing.Point(0, 0);
            this.eavGroupBox.Name = "eavGroupBox";
            this.eavGroupBox.Size = new System.Drawing.Size(247, 234);
            this.eavGroupBox.TabIndex = 0;
            this.eavGroupBox.TabStop = false;
            this.eavGroupBox.Text = "Название пункта!!!";
            // 
            // saveEAVButton
            // 
            this.saveEAVButton.Location = new System.Drawing.Point(3, 3);
            this.saveEAVButton.Name = "saveEAVButton";
            this.saveEAVButton.Size = new System.Drawing.Size(75, 23);
            this.saveEAVButton.TabIndex = 1;
            this.saveEAVButton.Text = "Сохранить";
            this.saveEAVButton.UseVisualStyleBackColor = true;
            this.saveEAVButton.Click += new System.EventHandler(this.saveEAVButton_Click);
            // 
            // ucSites
            // 
            this.ucSites.DefaultNewSiteAttrDateTime = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.ucSites.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSites.Location = new System.Drawing.Point(3, 16);
            this.ucSites.Name = "ucSites";
            this.ucSites.ShowEditDataButton = false;
            this.ucSites.ShowEditStationButton = false;
            this.ucSites.ShowGroupDataButton = false;
            this.ucSites.ShowIsOneTableButton = false;
            this.ucSites.ShowRefreshButton = false;
            this.ucSites.SiteAttrDateActual = new System.DateTime(2017, 10, 8, 0, 0, 0, 0);
            this.ucSites.SiteAttrTypes = null;
            this.ucSites.SiteGroupId = null;
            this.ucSites.Size = new System.Drawing.Size(404, 375);
            this.ucSites.TabIndex = 0;
            this.ucSites.UCEntityAttrValueChangedEvent += new SOV.Amur.Meta.UCSiteGeoObjectList.UCEntityAttrValueChangedEventHandler(this.ucSites_UCEntityAttrValueChangedEvent);
            this.ucSites.Load += new System.EventHandler(this.ucSiteObjects_Load);
            // 
            // FormSitesAttrs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 456);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FormSitesAttrs";
            this.Text = "Редактирование атрибутов пункта";
            this.Load += new System.EventHandler(this.FormSitesAttrs_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.eavGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UCSiteGeoObjectList ucSites;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox dateActualTextBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStripDropDownButton siteAttrsDropDown;
        private System.Windows.Forms.ToolStripButton refreshSitesButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox eavGroupBox;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button saveEAVButton;
    }
}