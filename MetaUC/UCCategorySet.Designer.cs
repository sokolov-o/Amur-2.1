namespace SOV.Amur.Meta
{
    partial class UCCategorySet
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ucCategorySetList = new SOV.Common.UCList();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.addNewItemButton = new System.Windows.Forms.ToolStripButton();
            this.saveButton = new System.Windows.Forms.ToolStripButton();
            this.deleteItemButton = new System.Windows.Forms.ToolStripButton();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.categoryItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.upButton = new System.Windows.Forms.Button();
            this.downButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ucNameItem = new SOV.Amur.Meta.UCNameItems();
            this.itemNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemNameShortDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.settingsButton = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.categoryItemBindingSource)).BeginInit();
            this.groupBox3.SuspendLayout();
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
            this.splitContainer1.Size = new System.Drawing.Size(628, 357);
            this.splitContainer1.SplitterDistance = 209;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ucCategorySetList);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(209, 357);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Наборы категорий";
            // 
            // ucCategorySetList
            // 
            this.ucCategorySetList.ColumnsHeadersVisible = true;
            this.ucCategorySetList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucCategorySetList.Location = new System.Drawing.Point(3, 16);
            this.ucCategorySetList.MultiSelect = false;
            this.ucCategorySetList.Name = "ucCategorySetList";
            this.ucCategorySetList.ShowAddNewToolbarButton = true;
            this.ucCategorySetList.ShowColumnHeaders = true;
            this.ucCategorySetList.ShowDeleteToolbarButton = true;
            this.ucCategorySetList.ShowFindItemToolbarButton = true;
            this.ucCategorySetList.ShowId = true;
            this.ucCategorySetList.ShowOrderControls = false;
            this.ucCategorySetList.ShowOrderToolbarButton = false;
            this.ucCategorySetList.ShowSaveToolbarButton = false;
            this.ucCategorySetList.ShowSelectAllToolbarButton = false;
            this.ucCategorySetList.ShowSelectedOnly = false;
            this.ucCategorySetList.ShowSelectedOnlyToolbarButton = false;
            this.ucCategorySetList.ShowToolbar = true;
            this.ucCategorySetList.ShowUnselectAllToolbarButton = false;
            this.ucCategorySetList.ShowUpdateToolbarButton = true;
            this.ucCategorySetList.Size = new System.Drawing.Size(203, 338);
            this.ucCategorySetList.TabIndex = 0;
            this.ucCategorySetList.UCSelectedItemChanged += new SOV.Common.UCList.UCSelectedItemChangedEventHandler(this.ucCategorySetList_UCSelectedItemChanged);
            this.ucCategorySetList.UCAddNewEvent += new SOV.Common.UCList.UCAddNewEventHandler(this.ucCategorySetList_UCAddNewEvent);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.splitContainer2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(415, 357);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Категории";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 16);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer2.Size = new System.Drawing.Size(409, 338);
            this.splitContainer2.SplitterDistance = 218;
            this.splitContainer2.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgvItems, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.upButton, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.downButton, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(409, 218);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewItemButton,
            this.saveButton,
            this.deleteItemButton,
            this.settingsButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(353, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // addNewItemButton
            // 
            this.addNewItemButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addNewItemButton.Image = global::SOV.Amur.Meta.Properties.Resources.action_add_16xLG1;
            this.addNewItemButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addNewItemButton.Name = "addNewItemButton";
            this.addNewItemButton.Size = new System.Drawing.Size(23, 22);
            this.addNewItemButton.Text = "toolStripButton1";
            this.addNewItemButton.Click += new System.EventHandler(this.addNewItemButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveButton.Image = global::SOV.Amur.Meta.Properties.Resources.SaveBlack;
            this.saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(23, 22);
            this.saveButton.Text = "Сохранить исправления и порядок категорий";
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // deleteItemButton
            // 
            this.deleteItemButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deleteItemButton.Image = global::SOV.Amur.Meta.Properties.Resources.DeleteHS;
            this.deleteItemButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteItemButton.Name = "deleteItemButton";
            this.deleteItemButton.Size = new System.Drawing.Size(23, 22);
            this.deleteItemButton.Text = "toolStripButton1";
            this.deleteItemButton.Click += new System.EventHandler(this.deleteItemButton_Click);
            // 
            // dgvItems
            // 
            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.AllowUserToDeleteRows = false;
            this.dgvItems.AutoGenerateColumns = false;
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.itemNameDataGridViewTextBoxColumn,
            this.itemNameShortDataGridViewTextBoxColumn,
            this.Value1,
            this.Value2,
            this.Code});
            this.dgvItems.DataSource = this.categoryItemBindingSource;
            this.dgvItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvItems.Location = new System.Drawing.Point(3, 28);
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.RowHeadersVisible = false;
            this.tableLayoutPanel1.SetRowSpan(this.dgvItems, 2);
            this.dgvItems.Size = new System.Drawing.Size(347, 187);
            this.dgvItems.TabIndex = 1;
            this.dgvItems.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvItems_ColumnHeaderMouseClick);
            // 
            // categoryItemBindingSource
            // 
            this.categoryItemBindingSource.DataSource = typeof(SOV.Amur.Meta.CategoryItemLocalized);
            this.categoryItemBindingSource.CurrentChanged += new System.EventHandler(this.categoryItemLocalizedBindingSource_CurrentChanged);
            // 
            // upButton
            // 
            this.upButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.upButton.Location = new System.Drawing.Point(356, 95);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(50, 23);
            this.upButton.TabIndex = 2;
            this.upButton.Text = "Вверх";
            this.upButton.UseVisualStyleBackColor = true;
            this.upButton.Click += new System.EventHandler(this.upButton_Click);
            // 
            // downButton
            // 
            this.downButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.downButton.Location = new System.Drawing.Point(356, 124);
            this.downButton.Name = "downButton";
            this.downButton.Size = new System.Drawing.Size(50, 23);
            this.downButton.TabIndex = 3;
            this.downButton.Text = "Вниз";
            this.downButton.UseVisualStyleBackColor = true;
            this.downButton.Click += new System.EventHandler(this.downButton_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ucNameItem);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(409, 116);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Варианты названия категории";
            // 
            // ucNameItem
            // 
            this.ucNameItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucNameItem.Location = new System.Drawing.Point(3, 16);
            this.ucNameItem.Name = "ucNameItem";
            this.ucNameItem.Size = new System.Drawing.Size(403, 97);
            this.ucNameItem.TabIndex = 0;
            this.ucNameItem.UCShowToolStrip = true;
            // 
            // itemNameDataGridViewTextBoxColumn
            // 
            this.itemNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.itemNameDataGridViewTextBoxColumn.DataPropertyName = "ItemName";
            this.itemNameDataGridViewTextBoxColumn.HeaderText = "Название категории";
            this.itemNameDataGridViewTextBoxColumn.Name = "itemNameDataGridViewTextBoxColumn";
            this.itemNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // itemNameShortDataGridViewTextBoxColumn
            // 
            this.itemNameShortDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.itemNameShortDataGridViewTextBoxColumn.DataPropertyName = "ItemNameShort";
            this.itemNameShortDataGridViewTextBoxColumn.HeaderText = "Кратко";
            this.itemNameShortDataGridViewTextBoxColumn.Name = "itemNameShortDataGridViewTextBoxColumn";
            this.itemNameShortDataGridViewTextBoxColumn.ReadOnly = true;
            this.itemNameShortDataGridViewTextBoxColumn.Width = 68;
            // 
            // Value1
            // 
            this.Value1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Value1.DataPropertyName = "Value1";
            this.Value1.HeaderText = "Слева >=";
            this.Value1.Name = "Value1";
            this.Value1.Width = 78;
            // 
            // Value2
            // 
            this.Value2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Value2.DataPropertyName = "Value2";
            this.Value2.HeaderText = "Справа <=";
            this.Value2.Name = "Value2";
            this.Value2.Width = 84;
            // 
            // Code
            // 
            this.Code.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Code.DataPropertyName = "Code";
            this.Code.HeaderText = "Код";
            this.Code.Name = "Code";
            this.Code.Width = 51;
            // 
            // settingsButton
            // 
            this.settingsButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.settingsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.settingsButton.Image = global::SOV.Amur.Meta.Properties.Resources.Property_501;
            this.settingsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(23, 22);
            this.settingsButton.Text = "toolStripButton1";
            this.settingsButton.Click += new System.EventHandler(this.settingsButton_Click);
            // 
            // UCCategorySet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "UCCategorySet";
            this.Size = new System.Drawing.Size(628, 357);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.categoryItemBindingSource)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private Common.UCList ucCategorySetList;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton addNewItemButton;
        private System.Windows.Forms.ToolStripButton deleteItemButton;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.BindingSource categoryItemBindingSource;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox3;
        private UCNameItems ucNameItem;
        private System.Windows.Forms.ToolStripButton saveButton;
        private System.Windows.Forms.Button upButton;
        private System.Windows.Forms.Button downButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemNameShortDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.ToolStripButton settingsButton;
    }
}
