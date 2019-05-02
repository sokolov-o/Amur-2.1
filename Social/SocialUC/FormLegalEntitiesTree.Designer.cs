namespace SOV.Social
{
    partial class FormLegalEntitiesTree
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.saveButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tab = new System.Windows.Forms.TabControl();
            this.staffTabPage = new System.Windows.Forms.TabPage();
            this.orgInfoTabPage = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.staffEmployeeTabPage = new System.Windows.Forms.TabPage();
            this.loadImagesPage = new System.Windows.Forms.TabPage();
            this.imagesPage = new System.Windows.Forms.TabPage();
            this.deleteImgButton = new System.Windows.Forms.Button();
            this.infoTextBox = new System.Windows.Forms.TextBox();
            this.ucLegalEntitiesTree = new SOV.Social.UCLegalEntitiesTree();
            this.ucLegalEntity = new SOV.Social.UCLegalEntity();
            this.ucStaffs = new SOV.Social.UCStaffs();
            this.ucStaffEmployees1 = new SOV.Social.UCStaffEmployees();
            this.ucImageGalleryLoader = new SOV.Common.UCImageGalleryLoader();
            this.ucImageGallery = new SOV.Common.UCImageGallery();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tab.SuspendLayout();
            this.staffTabPage.SuspendLayout();
            this.orgInfoTabPage.SuspendLayout();
            this.staffEmployeeTabPage.SuspendLayout();
            this.loadImagesPage.SuspendLayout();
            this.imagesPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(36, 36);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(634, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuFileToolStripMenuItem
            // 
            this.mnuFileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileExitToolStripMenuItem});
            this.mnuFileToolStripMenuItem.Name = "mnuFileToolStripMenuItem";
            this.mnuFileToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.mnuFileToolStripMenuItem.Text = "Файл";
            // 
            // mnuFileExitToolStripMenuItem
            // 
            this.mnuFileExitToolStripMenuItem.Name = "mnuFileExitToolStripMenuItem";
            this.mnuFileExitToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.mnuFileExitToolStripMenuItem.Text = "Выход";
            this.mnuFileExitToolStripMenuItem.Click += new System.EventHandler(this.mnuFileExitToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer1.Panel2.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainer1.Size = new System.Drawing.Size(634, 473);
            this.splitContainer1.SplitterDistance = 209;
            this.splitContainer1.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ucLegalEntitiesTree);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(209, 473);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Субъекты права";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.saveButton, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tab, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.infoTextBox, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(421, 473);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(3, 269);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 1;
            this.saveButton.Text = "Сохранить";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // groupBox2
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox2, 2);
            this.groupBox2.Controls.Add(this.ucLegalEntity);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 29);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(415, 234);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Субъект";
            // 
            // tab
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.tab, 2);
            this.tab.Controls.Add(this.staffTabPage);
            this.tab.Controls.Add(this.orgInfoTabPage);
            this.tab.Controls.Add(this.staffEmployeeTabPage);
            this.tab.Controls.Add(this.loadImagesPage);
            this.tab.Controls.Add(this.imagesPage);
            this.tab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab.Location = new System.Drawing.Point(3, 298);
            this.tab.Name = "tab";
            this.tab.SelectedIndex = 0;
            this.tab.Size = new System.Drawing.Size(415, 172);
            this.tab.TabIndex = 3;
            // 
            // staffTabPage
            // 
            this.staffTabPage.Controls.Add(this.ucStaffs);
            this.staffTabPage.Location = new System.Drawing.Point(4, 22);
            this.staffTabPage.Name = "staffTabPage";
            this.staffTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.staffTabPage.Size = new System.Drawing.Size(407, 146);
            this.staffTabPage.TabIndex = 0;
            this.staffTabPage.Text = "Штатное расписание";
            this.staffTabPage.UseVisualStyleBackColor = true;
            // 
            // orgInfoTabPage
            // 
            this.orgInfoTabPage.Controls.Add(this.label1);
            this.orgInfoTabPage.Location = new System.Drawing.Point(4, 22);
            this.orgInfoTabPage.Name = "orgInfoTabPage";
            this.orgInfoTabPage.Size = new System.Drawing.Size(407, 146);
            this.orgInfoTabPage.TabIndex = 1;
            this.orgInfoTabPage.Text = "Сведения об организации";
            this.orgInfoTabPage.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Не реализовано";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // staffEmployeeTabPage
            // 
            this.staffEmployeeTabPage.Controls.Add(this.ucStaffEmployees1);
            this.staffEmployeeTabPage.Location = new System.Drawing.Point(4, 22);
            this.staffEmployeeTabPage.Margin = new System.Windows.Forms.Padding(1);
            this.staffEmployeeTabPage.Name = "staffEmployeeTabPage";
            this.staffEmployeeTabPage.Size = new System.Drawing.Size(407, 146);
            this.staffEmployeeTabPage.TabIndex = 2;
            this.staffEmployeeTabPage.Text = "Должности сотрудника";
            this.staffEmployeeTabPage.UseVisualStyleBackColor = true;
            // 
            // loadImagesPage
            // 
            this.loadImagesPage.Controls.Add(this.ucImageGalleryLoader);
            this.loadImagesPage.Location = new System.Drawing.Point(4, 22);
            this.loadImagesPage.Name = "loadImagesPage";
            this.loadImagesPage.Padding = new System.Windows.Forms.Padding(3);
            this.loadImagesPage.Size = new System.Drawing.Size(407, 146);
            this.loadImagesPage.TabIndex = 3;
            this.loadImagesPage.Text = "Загрузка изображений";
            this.loadImagesPage.UseVisualStyleBackColor = true;
            // 
            // imagesPage
            // 
            this.imagesPage.Controls.Add(this.deleteImgButton);
            this.imagesPage.Controls.Add(this.ucImageGallery);
            this.imagesPage.Location = new System.Drawing.Point(4, 22);
            this.imagesPage.Name = "imagesPage";
            this.imagesPage.Padding = new System.Windows.Forms.Padding(3);
            this.imagesPage.Size = new System.Drawing.Size(407, 146);
            this.imagesPage.TabIndex = 4;
            this.imagesPage.Text = "Изображения";
            this.imagesPage.UseVisualStyleBackColor = true;
            // 
            // deleteImgButton
            // 
            this.deleteImgButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.deleteImgButton.Location = new System.Drawing.Point(6, 115);
            this.deleteImgButton.Name = "deleteImgButton";
            this.deleteImgButton.Size = new System.Drawing.Size(75, 23);
            this.deleteImgButton.TabIndex = 1;
            this.deleteImgButton.Text = "Удалить";
            this.deleteImgButton.UseVisualStyleBackColor = true;
            this.deleteImgButton.Click += new System.EventHandler(this.deleteImgButton_Click);
            // 
            // infoTextBox
            // 
            this.infoTextBox.BackColor = System.Drawing.SystemColors.Info;
            this.tableLayoutPanel1.SetColumnSpan(this.infoTextBox, 2);
            this.infoTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoTextBox.Location = new System.Drawing.Point(3, 3);
            this.infoTextBox.Name = "infoTextBox";
            this.infoTextBox.Size = new System.Drawing.Size(415, 20);
            this.infoTextBox.TabIndex = 4;
            this.infoTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ucLegalEntitiesTree
            // 
            this.ucLegalEntitiesTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucLegalEntitiesTree.LegalEntitySelected = null;
            this.ucLegalEntitiesTree.Location = new System.Drawing.Point(3, 16);
            this.ucLegalEntitiesTree.Margin = new System.Windows.Forms.Padding(7);
            this.ucLegalEntitiesTree.Name = "ucLegalEntitiesTree";
            this.ucLegalEntitiesTree.ShowAddNewButton = true;
            this.ucLegalEntitiesTree.ShowDataActual = false;
            this.ucLegalEntitiesTree.ShowDeleteButton = true;
            this.ucLegalEntitiesTree.ShowRefreshButton = true;
            this.ucLegalEntitiesTree.ShowToolbar = true;
            this.ucLegalEntitiesTree.Size = new System.Drawing.Size(203, 454);
            this.ucLegalEntitiesTree.TabIndex = 0;
            this.ucLegalEntitiesTree.UCRefreshEvent += new SOV.Social.UCLegalEntitiesTree.UCRefreshEventHandler(this.ucLegalEntitiesTree_UCRefreshEvent);
            this.ucLegalEntitiesTree.UCSelectedNodeChangedEvent += new SOV.Social.UCLegalEntitiesTree.UCSelectedNodeChangedEventHandler(this.ucLegalEntitiesTree_UCSelectedNodeChangedEvent);
            this.ucLegalEntitiesTree.UCAddNewLEEvent += new SOV.Social.UCLegalEntitiesTree.UCAddNewLEEventHandler(this.ucLegalEntitiesTree_UCAddNewLEEvent);
            // 
            // ucLegalEntity
            // 
            this.ucLegalEntity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucLegalEntity.LegalEntity = null;
            this.ucLegalEntity.Location = new System.Drawing.Point(3, 16);
            this.ucLegalEntity.Margin = new System.Windows.Forms.Padding(7);
            this.ucLegalEntity.Name = "ucLegalEntity";
            this.ucLegalEntity.Size = new System.Drawing.Size(409, 215);
            this.ucLegalEntity.TabIndex = 0;
            // 
            // ucStaffs
            // 
            this.ucStaffs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucStaffs.Location = new System.Drawing.Point(3, 3);
            this.ucStaffs.Margin = new System.Windows.Forms.Padding(7);
            this.ucStaffs.Name = "ucStaffs";
            this.ucStaffs.Size = new System.Drawing.Size(401, 140);
            this.ucStaffs.TabIndex = 0;
            // 
            // ucStaffEmployees1
            // 
            this.ucStaffEmployees1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucStaffEmployees1.Location = new System.Drawing.Point(0, 0);
            this.ucStaffEmployees1.Name = "ucStaffEmployees1";
            this.ucStaffEmployees1.Size = new System.Drawing.Size(407, 146);
            this.ucStaffEmployees1.TabIndex = 0;
            // 
            // ucImageGalleryLoader
            // 
            this.ucImageGalleryLoader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucImageGalleryLoader.Location = new System.Drawing.Point(3, 3);
            this.ucImageGalleryLoader.Name = "ucImageGalleryLoader";
            this.ucImageGalleryLoader.Size = new System.Drawing.Size(401, 140);
            this.ucImageGalleryLoader.TabIndex = 0;
            // 
            // ucImageGallery
            // 
            this.ucImageGallery.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucImageGallery.Location = new System.Drawing.Point(3, 3);
            this.ucImageGallery.Name = "ucImageGallery";
            this.ucImageGallery.Size = new System.Drawing.Size(401, 106);
            this.ucImageGallery.TabIndex = 0;
            // 
            // FormLegalEntitiesTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 497);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormLegalEntitiesTree";
            this.Text = "Управление базой данных \"Субъекты права\"";
            this.Load += new System.EventHandler(this.FormLegalEntitiesTree_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.tab.ResumeLayout(false);
            this.staffTabPage.ResumeLayout(false);
            this.orgInfoTabPage.ResumeLayout(false);
            this.orgInfoTabPage.PerformLayout();
            this.staffEmployeeTabPage.ResumeLayout(false);
            this.loadImagesPage.ResumeLayout(false);
            this.imagesPage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UCLegalEntitiesTree ucLegalEntitiesTree;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExitToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private UCLegalEntity ucLegalEntity;
        private System.Windows.Forms.TabControl tab;
        private System.Windows.Forms.TabPage staffTabPage;
        private System.Windows.Forms.TabPage orgInfoTabPage;
        private UCStaffs ucStaffs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox infoTextBox;
        private System.Windows.Forms.TabPage staffEmployeeTabPage;
        private UCStaffEmployees ucStaffEmployees1;
        private System.Windows.Forms.TabPage loadImagesPage;
        private Common.UCImageGalleryLoader ucImageGalleryLoader;
        private System.Windows.Forms.TabPage imagesPage;
        private System.Windows.Forms.Button deleteImgButton;
        private Common.UCImageGallery ucImageGallery;
    }
}