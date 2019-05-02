namespace SOV.Amur.Meta
{
    partial class UCEntityGroup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCEntityGroup));
            this.tv = new System.Windows.Forms.TreeView();
            this.groupContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.insertGroupItemButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.freeItemsList = new SOV.Common.UCList();
            this.freeItemsFilterButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupItemsList = new SOV.Common.UCList();
            this.saveGroupItemsBtton = new System.Windows.Forms.Button();
            this.deleteGroupItemButton = new System.Windows.Forms.Button();
            this.groupContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tv
            // 
            this.tv.ContextMenuStrip = this.groupContextMenuStrip;
            this.tv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv.ImageIndex = 0;
            this.tv.ImageList = this.imageList1;
            this.tv.Location = new System.Drawing.Point(3, 3);
            this.tv.Name = "tv";
            this.tv.SelectedImageIndex = 0;
            this.tv.Size = new System.Drawing.Size(203, 411);
            this.tv.TabIndex = 9;
            this.tv.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // groupContextMenuStrip
            // 
            this.groupContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.renameToolStripMenuItem,
            this.toolStripSeparator1,
            this.refreshToolStripMenuItem});
            this.groupContextMenuStrip.Name = "groupContextMenuStrip";
            this.groupContextMenuStrip.Size = new System.Drawing.Size(248, 98);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Image = global::SOV.Amur.Meta.Properties.Resources.action_add_16xLG;
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
            this.addToolStripMenuItem.Text = "Создать группу...";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = global::SOV.Amur.Meta.Properties.Resources.delete_12x12;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
            this.deleteToolStripMenuItem.Text = "Удалить группу...";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Image = global::SOV.Amur.Meta.Properties.Resources.Edit;
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
            this.renameToolStripMenuItem.Text = "Переименовать группу...";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(244, 6);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Image = global::SOV.Amur.Meta.Properties.Resources.refresh_16xLG;
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
            this.refreshToolStripMenuItem.Text = "Обновить группы справочника";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "GroupByType_5544_32.bmp");
            this.imageList1.Images.SetKeyName(1, "XSDSchema_ElementIcon_16x16.png");
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
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel2);
            this.splitContainer1.Size = new System.Drawing.Size(751, 436);
            this.splitContainer1.SplitterDistance = 215;
            this.splitContainer1.TabIndex = 10;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(215, 436);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Справочники и группы";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tv, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 417F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(209, 417);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.insertGroupItemButton, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.groupBox3, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.saveGroupItemsBtton, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.deleteGroupItemButton, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(532, 436);
            this.tableLayoutPanel2.TabIndex = 7;
            // 
            // insertGroupItemButton
            // 
            this.insertGroupItemButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.insertGroupItemButton.Location = new System.Drawing.Point(226, 206);
            this.insertGroupItemButton.Name = "insertGroupItemButton";
            this.insertGroupItemButton.Size = new System.Drawing.Size(80, 23);
            this.insertGroupItemButton.TabIndex = 9;
            this.insertGroupItemButton.Text = "<- Добавить";
            this.insertGroupItemButton.UseVisualStyleBackColor = true;
            this.insertGroupItemButton.Click += new System.EventHandler(this.insertGroupItemButton_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tableLayoutPanel3);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(312, 3);
            this.groupBox3.Name = "groupBox3";
            this.tableLayoutPanel2.SetRowSpan(this.groupBox3, 2);
            this.groupBox3.Size = new System.Drawing.Size(217, 400);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Элементы не в группе";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.freeItemsList, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.freeItemsFilterButton, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(211, 381);
            this.tableLayoutPanel3.TabIndex = 5;
            // 
            // freeItemsDicListBox
            // 
            this.freeItemsList.ColumnsHeadersVisible = true;
            this.freeItemsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.freeItemsList.Location = new System.Drawing.Point(3, 3);
            this.freeItemsList.Name = "freeItemsDicListBox";
            this.freeItemsList.ShowAddNewToolbarButton = false;
            this.freeItemsList.ShowColumnHeaders = true;
            this.freeItemsList.ShowDeleteToolbarButton = false;
            this.freeItemsList.ShowFindItemToolbarButton = true;
            this.freeItemsList.ShowId = true;
            this.freeItemsList.ShowOrderControls = false;
            this.freeItemsList.ShowOrderToolbarButton = false;
            this.freeItemsList.ShowSaveToolbarButton = true;
            this.freeItemsList.ShowSelectAllToolbarButton = true;
            this.freeItemsList.ShowSelectedOnly = false;
            this.freeItemsList.ShowSelectedOnlyToolbarButton = true;
            this.freeItemsList.ShowToolbar = true;
            this.freeItemsList.ShowUnselectAllToolbarButton = true;
            this.freeItemsList.ShowUpdateToolbarButton = false;
            this.freeItemsList.Size = new System.Drawing.Size(205, 346);
            this.freeItemsList.TabIndex = 4;
            // 
            // freeItemsFilterButton
            // 
            this.freeItemsFilterButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.freeItemsFilterButton.Image = global::SOV.Amur.Meta.Properties.Resources.filter_16xLG;
            this.freeItemsFilterButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.freeItemsFilterButton.Location = new System.Drawing.Point(3, 355);
            this.freeItemsFilterButton.Name = "freeItemsFilterButton";
            this.freeItemsFilterButton.Size = new System.Drawing.Size(205, 23);
            this.freeItemsFilterButton.TabIndex = 5;
            this.freeItemsFilterButton.Text = "Фильтр элементов";
            this.freeItemsFilterButton.UseVisualStyleBackColor = true;
            this.freeItemsFilterButton.Click += new System.EventHandler(this.freeItemsFilterButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupItemsList);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.tableLayoutPanel2.SetRowSpan(this.groupBox2, 2);
            this.groupBox2.Size = new System.Drawing.Size(217, 400);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Элементы в группе";
            // 
            // groupItemsDicListBox
            // 
            this.groupItemsList.ColumnsHeadersVisible = true;
            this.groupItemsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupItemsList.Location = new System.Drawing.Point(3, 16);
            this.groupItemsList.Name = "groupItemsDicListBox";
            this.groupItemsList.ShowAddNewToolbarButton = false;
            this.groupItemsList.ShowColumnHeaders = true;
            this.groupItemsList.ShowDeleteToolbarButton = false;
            this.groupItemsList.ShowFindItemToolbarButton = true;
            this.groupItemsList.ShowId = true;
            this.groupItemsList.ShowOrderControls = true;
            this.groupItemsList.ShowOrderToolbarButton = false;
            this.groupItemsList.ShowSaveToolbarButton = true;
            this.groupItemsList.ShowSelectAllToolbarButton = true;
            this.groupItemsList.ShowSelectedOnly = false;
            this.groupItemsList.ShowSelectedOnlyToolbarButton = true;
            this.groupItemsList.ShowToolbar = true;
            this.groupItemsList.ShowUnselectAllToolbarButton = true;
            this.groupItemsList.ShowUpdateToolbarButton = false;
            this.groupItemsList.Size = new System.Drawing.Size(211, 381);
            this.groupItemsList.TabIndex = 4;
            // 
            // saveGroupItemsBtton
            // 
            this.saveGroupItemsBtton.Dock = System.Windows.Forms.DockStyle.Left;
            this.saveGroupItemsBtton.Image = global::SOV.Amur.Meta.Properties.Resources.SaveBlack;
            this.saveGroupItemsBtton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.saveGroupItemsBtton.Location = new System.Drawing.Point(3, 409);
            this.saveGroupItemsBtton.Name = "saveGroupItemsBtton";
            this.saveGroupItemsBtton.Size = new System.Drawing.Size(82, 24);
            this.saveGroupItemsBtton.TabIndex = 7;
            this.saveGroupItemsBtton.Text = "Сохранить";
            this.saveGroupItemsBtton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.saveGroupItemsBtton.UseVisualStyleBackColor = true;
            this.saveGroupItemsBtton.Click += new System.EventHandler(this.saveGroupItemsBtton_Click);
            // 
            // deleteGroupItemButton
            // 
            this.deleteGroupItemButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.deleteGroupItemButton.Location = new System.Drawing.Point(226, 177);
            this.deleteGroupItemButton.Name = "deleteGroupItemButton";
            this.deleteGroupItemButton.Size = new System.Drawing.Size(80, 23);
            this.deleteGroupItemButton.TabIndex = 8;
            this.deleteGroupItemButton.Text = "Удалить ->";
            this.deleteGroupItemButton.UseVisualStyleBackColor = true;
            this.deleteGroupItemButton.Click += new System.EventHandler(this.deleteGroupItemButton_Click);
            // 
            // UCEntityGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "UCEntityGroup";
            this.Size = new System.Drawing.Size(751, 436);
            this.Load += new System.EventHandler(this.UCEntityGroup_Load);
            this.groupContextMenuStrip.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Common.UCList groupItemsList;
        private System.Windows.Forms.TreeView tv;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ContextMenuStrip groupContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox3;
        private Common.UCList freeItemsList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button insertGroupItemButton;
        private System.Windows.Forms.Button saveGroupItemsBtton;
        private System.Windows.Forms.Button deleteGroupItemButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button freeItemsFilterButton;
    }
}
