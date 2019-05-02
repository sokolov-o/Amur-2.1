namespace SOV.Social
{
    partial class UCLegalEntitiesTree
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCLegalEntitiesTree));
            this.tv = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tlp = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.refreshButton = new System.Windows.Forms.ToolStripButton();
            this.addNewLEButton = new System.Windows.Forms.ToolStripButton();
            this.deleteButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.dateActualTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.findPatternTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.findNextButton = new System.Windows.Forms.ToolStripButton();
            this.infoLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.mnuShowDivisionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tlp.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tv
            // 
            this.tv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv.ImageIndex = 0;
            this.tv.ImageList = this.imageList1;
            this.tv.Location = new System.Drawing.Point(3, 28);
            this.tv.Name = "tv";
            this.tv.SelectedImageIndex = 0;
            this.tv.Size = new System.Drawing.Size(426, 152);
            this.tv.TabIndex = 0;
            this.tv.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterSelect);
            this.tv.DoubleClick += new System.EventHandler(this.tv_DoubleClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "3(three)columns_9714.png");
            this.imageList1.Images.SetKeyName(1, "poolball.png");
            // 
            // tlp
            // 
            this.tlp.ColumnCount = 1;
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp.Controls.Add(this.tv, 0, 1);
            this.tlp.Controls.Add(this.toolStrip1, 0, 0);
            this.tlp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp.Location = new System.Drawing.Point(0, 0);
            this.tlp.Name = "tlp";
            this.tlp.RowCount = 2;
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp.Size = new System.Drawing.Size(432, 183);
            this.tlp.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(36, 36);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshButton,
            this.addNewLEButton,
            this.deleteButton,
            this.toolStripLabel1,
            this.dateActualTextBox,
            this.toolStripSeparator1,
            this.findPatternTextBox,
            this.findNextButton,
            this.infoLabel,
            this.toolStripSplitButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(432, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // refreshButton
            // 
            this.refreshButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.refreshButton.Image = global::SOV.Social.Properties.Resources.refresh_16xLG;
            this.refreshButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.refreshButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(23, 22);
            this.refreshButton.Text = "Обновить дерево субъектов";
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // addNewLEButton
            // 
            this.addNewLEButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addNewLEButton.Image = global::SOV.Social.Properties.Resources.action_add_16xLG;
            this.addNewLEButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.addNewLEButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addNewLEButton.Name = "addNewLEButton";
            this.addNewLEButton.Size = new System.Drawing.Size(23, 22);
            this.addNewLEButton.Text = "Создать субъекта";
            this.addNewLEButton.Click += new System.EventHandler(this.addNewLEButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deleteButton.Image = global::SOV.Social.Properties.Resources.delete_12x12;
            this.deleteButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.deleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(23, 22);
            this.deleteButton.Text = "toolStripButton1";
            this.deleteButton.ToolTipText = "Удалить субъекта";
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(112, 22);
            this.toolStripLabel1.Text = "Дата актуальности:";
            // 
            // dateActualTextBox
            // 
            this.dateActualTextBox.Name = "dateActualTextBox";
            this.dateActualTextBox.Size = new System.Drawing.Size(67, 25);
            this.dateActualTextBox.Text = "01.07.2017";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // findPatternTextBox
            // 
            this.findPatternTextBox.Name = "findPatternTextBox";
            this.findPatternTextBox.Size = new System.Drawing.Size(45, 25);
            this.findPatternTextBox.TextChanged += new System.EventHandler(this.findPatternTextBox_TextChanged);
            // 
            // findNextButton
            // 
            this.findNextButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.findNextButton.Image = global::SOV.Social.Properties.Resources.Find_5650;
            this.findNextButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.findNextButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.findNextButton.Name = "findNextButton";
            this.findNextButton.Size = new System.Drawing.Size(23, 22);
            this.findNextButton.Text = "toolStripButton1";
            this.findNextButton.Click += new System.EventHandler(this.findNextButton_Click);
            // 
            // infoLabel
            // 
            this.infoLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(16, 22);
            this.infoLabel.Text = "...";
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuShowDivisionsToolStripMenuItem});
            this.toolStripSplitButton1.Image = global::SOV.Social.Properties.Resources.Property_501;
            this.toolStripSplitButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(32, 22);
            this.toolStripSplitButton1.Text = "Свойства дерева субъектов";
            // 
            // mnuShowDivisionsToolStripMenuItem
            // 
            this.mnuShowDivisionsToolStripMenuItem.Checked = true;
            this.mnuShowDivisionsToolStripMenuItem.CheckOnClick = true;
            this.mnuShowDivisionsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuShowDivisionsToolStripMenuItem.Name = "mnuShowDivisionsToolStripMenuItem";
            this.mnuShowDivisionsToolStripMenuItem.Size = new System.Drawing.Size(341, 22);
            this.mnuShowDivisionsToolStripMenuItem.Text = "Отображать подразделения юридического лица";
            this.mnuShowDivisionsToolStripMenuItem.CheckedChanged += new System.EventHandler(this.mnuShowDivisionsToolStripMenuItem_CheckedChanged);
            // 
            // UCLegalEntitiesTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlp);
            this.Name = "UCLegalEntitiesTree";
            this.Size = new System.Drawing.Size(432, 183);
            this.tlp.ResumeLayout(false);
            this.tlp.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tv;
        private System.Windows.Forms.TableLayoutPanel tlp;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton refreshButton;
        private System.Windows.Forms.ToolStripButton addNewLEButton;
        private System.Windows.Forms.ToolStripButton deleteButton;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox dateActualTextBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripTextBox findPatternTextBox;
        private System.Windows.Forms.ToolStripButton findNextButton;
        private System.Windows.Forms.ToolStripLabel infoLabel;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem mnuShowDivisionsToolStripMenuItem;
        private System.Windows.Forms.ImageList imageList1;
    }
}
