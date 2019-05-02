namespace FERHRI.Amur.Meta
{
    partial class UCCategoryDefinition
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
            this.ucCategoryList = new FERHRI.Common.UCList();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addNewItemButton = new System.Windows.Forms.ToolStripButton();
            this.editItemButton = new System.Windows.Forms.ToolStripButton();
            this.deleteItemButton = new System.Windows.Forms.ToolStripButton();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.itemNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemNameShortDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.categoryItemLocalizedBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ucNameItem = new FERHRI.Amur.Meta.UCNameItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.categoryItemLocalizedBindingSource)).BeginInit();
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
            this.groupBox1.Controls.Add(this.ucCategoryList);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(209, 357);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Категория";
            // 
            // ucCategoryList
            // 
            this.ucCategoryList.AreAllItemsSelected = false;
            this.ucCategoryList.ColumnsHeadersVisible = true;
            this.ucCategoryList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucCategoryList.Location = new System.Drawing.Point(3, 16);
            this.ucCategoryList.MultiSelect = false;
            this.ucCategoryList.Name = "ucCategoryList";
            this.ucCategoryList.ShowAddNewToolbarButton = true;
            this.ucCategoryList.ShowColumnHeaders = true;
            this.ucCategoryList.ShowDeleteToolbarButton = true;
            this.ucCategoryList.ShowFindItemToolbarButton = true;
            this.ucCategoryList.ShowId = true;
            this.ucCategoryList.ShowOrderControls = false;
            this.ucCategoryList.ShowOrderToolbarButton = false;
            this.ucCategoryList.ShowSaveToolbarButton = false;
            this.ucCategoryList.ShowSelectAllToolbarButton = false;
            this.ucCategoryList.ShowSelectedOnly = false;
            this.ucCategoryList.ShowSelectedOnlyToolbarButton = false;
            this.ucCategoryList.ShowToolbar = true;
            this.ucCategoryList.ShowUnselectAllToolbarButton = false;
            this.ucCategoryList.ShowUpdateToolbarButton = true;
            this.ucCategoryList.Size = new System.Drawing.Size(203, 338);
            this.ucCategoryList.TabIndex = 0;
            this.ucCategoryList.UCSelectedItemChanged += new FERHRI.Common.UCList.UCSelectedItemChangedEventHandler(this.ucCategoryList_UCSelectedItemChanged);
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
            this.groupBox2.Text = "Элементы категории";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgvItems, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(409, 218);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewItemButton,
            this.editItemButton,
            this.deleteItemButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(409, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // dgvItems
            // 
            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.AllowUserToDeleteRows = false;
            this.dgvItems.AutoGenerateColumns = false;
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Code,
            this.itemNameDataGridViewTextBoxColumn,
            this.itemNameShortDataGridViewTextBoxColumn,
            this.Value1,
            this.Value2});
            this.dgvItems.DataSource = this.categoryItemLocalizedBindingSource;
            this.dgvItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvItems.Location = new System.Drawing.Point(3, 28);
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.ReadOnly = true;
            this.dgvItems.RowHeadersVisible = false;
            this.dgvItems.Size = new System.Drawing.Size(403, 187);
            this.dgvItems.TabIndex = 1;
            // 
            // Code
            // 
            this.Code.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Code.DataPropertyName = "Code";
            this.Code.HeaderText = "Код";
            this.Code.Name = "Code";
            this.Code.ReadOnly = true;
            this.Code.Width = 51;
            // 
            // Value1
            // 
            this.Value1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Value1.DataPropertyName = "Value1";
            this.Value1.HeaderText = "Слева >=";
            this.Value1.Name = "Value1";
            this.Value1.ReadOnly = true;
            this.Value1.Width = 78;
            // 
            // Value2
            // 
            this.Value2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Value2.DataPropertyName = "Value2";
            this.Value2.HeaderText = "Справа <=";
            this.Value2.Name = "Value2";
            this.Value2.ReadOnly = true;
            this.Value2.Width = 84;
            // 
            // addNewItemButton
            // 
            this.addNewItemButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addNewItemButton.Image = global::FERHRI.Amur.Meta.Properties.Resources.action_add_16xLG1;
            this.addNewItemButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addNewItemButton.Name = "addNewItemButton";
            this.addNewItemButton.Size = new System.Drawing.Size(23, 22);
            this.addNewItemButton.Text = "toolStripButton1";
            this.addNewItemButton.Click += new System.EventHandler(this.addNewItemButton_Click);
            // 
            // editItemButton
            // 
            this.editItemButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.editItemButton.Image = global::FERHRI.Amur.Meta.Properties.Resources.Editdatasetwithdesigner_8449;
            this.editItemButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editItemButton.Name = "editItemButton";
            this.editItemButton.Size = new System.Drawing.Size(23, 22);
            this.editItemButton.Text = "toolStripButton1";
            // 
            // deleteItemButton
            // 
            this.deleteItemButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deleteItemButton.Image = global::FERHRI.Amur.Meta.Properties.Resources.DeleteHS;
            this.deleteItemButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteItemButton.Name = "deleteItemButton";
            this.deleteItemButton.Size = new System.Drawing.Size(23, 22);
            this.deleteItemButton.Text = "toolStripButton1";
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
            // categoryItemLocalizedBindingSource
            // 
            this.categoryItemLocalizedBindingSource.DataSource = typeof(FERHRI.Amur.Meta.CategoryItemLocalized);
            this.categoryItemLocalizedBindingSource.CurrentChanged += new System.EventHandler(this.categoryItemLocalizedBindingSource_CurrentChanged);
            // 
            // ucNameItem
            // 
            this.ucNameItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucNameItem.Location = new System.Drawing.Point(3, 16);
            this.ucNameItem.Name = "ucNameItem";
            this.ucNameItem.Size = new System.Drawing.Size(403, 97);
            this.ucNameItem.TabIndex = 0;
            // 
            // UCCategoryDefinition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "UCCategoryDefinition";
            this.Size = new System.Drawing.Size(628, 357);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.categoryItemLocalizedBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private Common.UCList ucCategoryList;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton addNewItemButton;
        private System.Windows.Forms.ToolStripButton editItemButton;
        private System.Windows.Forms.ToolStripButton deleteItemButton;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.BindingSource categoryItemLocalizedBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemNameShortDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value2;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox3;
        private UCNameItem ucNameItem;
    }
}
