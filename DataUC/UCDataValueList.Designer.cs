namespace SOV.Amur.Data
{
    partial class UCDataValueList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCDataValueList));
            this.dgv = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuColumnsVisibleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonColumnsVisible = new System.Windows.Forms.Button();
            this.ucDicColumnsVisible = new SOV.Common.UCList();
            this.mnuActualizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuShowTelegrammToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuShowDeletedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.ContextMenuStrip = this.contextMenuStrip1;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(3, 3);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(353, 229);
            this.dgv.TabIndex = 0;
            this.dgv.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_RowEnter);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuActualizeToolStripMenuItem,
            this.mnuShowTelegrammToolStripMenuItem,
            this.toolStripSeparator1,
            this.mnuDeleteToolStripMenuItem,
            this.mnuShowDeletedToolStripMenuItem,
            this.mnuColumnsVisibleToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(223, 120);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(219, 6);
            // 
            // mnuColumnsVisibleToolStripMenuItem
            // 
            this.mnuColumnsVisibleToolStripMenuItem.Image = global::SOV.Amur.Data.Properties.Resources._3_three_columns_9714;
            this.mnuColumnsVisibleToolStripMenuItem.Name = "mnuColumnsVisibleToolStripMenuItem";
            this.mnuColumnsVisibleToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.mnuColumnsVisibleToolStripMenuItem.Text = "Отобразить столбцы...";
            this.mnuColumnsVisibleToolStripMenuItem.Click += new System.EventHandler(this.mnuColumnsVisibleToolStripMenuItem_Click_1);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.dgv, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(565, 235);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(362, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 229);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Выбор столбцов таблицы";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.buttonColumnsVisible, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.ucDicColumnsVisible, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(194, 210);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // buttonColumnsVisible
            // 
            this.buttonColumnsVisible.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonColumnsVisible.Location = new System.Drawing.Point(3, 184);
            this.buttonColumnsVisible.Name = "buttonColumnsVisible";
            this.buttonColumnsVisible.Size = new System.Drawing.Size(188, 23);
            this.buttonColumnsVisible.TabIndex = 0;
            this.buttonColumnsVisible.Text = "Применить";
            this.buttonColumnsVisible.UseVisualStyleBackColor = true;
            this.buttonColumnsVisible.Click += new System.EventHandler(this.buttonColumnsVisible_Click);
            // 
            // ucDicColumnsVisible
            // 
            //this.ucDicColumnsVisible.AllowMultiSelect = true;
            //this.ucDicColumnsVisible.CheckedId = ((System.Collections.Generic.List<int>)(resources.GetObject("ucDicColumnsVisible.CheckedId")));
            this.ucDicColumnsVisible.ColumnsHeadersVisible = true;
            //this.ucDicColumnsVisible.CurrentDicItemId = null;
            //this.ucDicColumnsVisible.DicItemName = null;
            this.ucDicColumnsVisible.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDicColumnsVisible.Location = new System.Drawing.Point(3, 3);
            this.ucDicColumnsVisible.Name = "ucDicColumnsVisible";
            //this.ucDicColumnsVisible.SelectedRowId = -1;
            this.ucDicColumnsVisible.ShowAddNewToolbarButton = true;
            //this.ucDicColumnsVisible.ShowCheckBox = true;
            this.ucDicColumnsVisible.ShowColumnHeaders = true;
            this.ucDicColumnsVisible.ShowDeleteToolbarButton = true;
            this.ucDicColumnsVisible.ShowFindItemToolbarButton = false;
            this.ucDicColumnsVisible.ShowId = false;
            this.ucDicColumnsVisible.ShowOrderControls = false;
            this.ucDicColumnsVisible.ShowOrderToolbarButton = false;
            this.ucDicColumnsVisible.ShowSaveToolbarButton = false;
            this.ucDicColumnsVisible.ShowSelectAllToolbarButton = true;
            this.ucDicColumnsVisible.ShowSelectedOnly = false;
            this.ucDicColumnsVisible.ShowSelectedOnlyToolbarButton = true;
            this.ucDicColumnsVisible.ShowToolbar = true;
            this.ucDicColumnsVisible.ShowUnselectAllToolbarButton = true;
            this.ucDicColumnsVisible.ShowUpdateToolbarButton = true;
            this.ucDicColumnsVisible.Size = new System.Drawing.Size(188, 175);
            this.ucDicColumnsVisible.TabIndex = 1;
            // 
            // mnuActualizeToolStripMenuItem
            // 
            this.mnuActualizeToolStripMenuItem.Image = global::SOV.Amur.Data.Properties.Resources.base_exclamationmark_32;
            this.mnuActualizeToolStripMenuItem.Name = "mnuActualizeToolStripMenuItem";
            this.mnuActualizeToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.mnuActualizeToolStripMenuItem.Text = "Актуализировать значение";
            this.mnuActualizeToolStripMenuItem.Click += new System.EventHandler(this.mnuActualizeToolStripMenuItem_Click);
            // 
            // mnuShowTelegrammToolStripMenuItem
            // 
            this.mnuShowTelegrammToolStripMenuItem.Image = global::SOV.Amur.Data.Properties.Resources.text_16xLG;
            this.mnuShowTelegrammToolStripMenuItem.Name = "mnuShowTelegrammToolStripMenuItem";
            this.mnuShowTelegrammToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.mnuShowTelegrammToolStripMenuItem.Text = "Телеграмма";
            this.mnuShowTelegrammToolStripMenuItem.Click += new System.EventHandler(this.mnuShowTelegrammToolStripMenuItem_Click);
            // 
            // mnuDeleteToolStripMenuItem
            // 
            this.mnuDeleteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("mnuDeleteToolStripMenuItem.Image")));
            this.mnuDeleteToolStripMenuItem.Name = "mnuDeleteToolStripMenuItem";
            this.mnuDeleteToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.mnuDeleteToolStripMenuItem.Text = "Удалить";
            this.mnuDeleteToolStripMenuItem.Click += new System.EventHandler(this.mnuDeleteToolStripMenuItem_Click);
            // 
            // mnuShowDeletedToolStripMenuItem
            // 
            this.mnuShowDeletedToolStripMenuItem.Image = global::SOV.Amur.Data.Properties.Resources.CheckBoxChecked;
            this.mnuShowDeletedToolStripMenuItem.Name = "mnuShowDeletedToolStripMenuItem";
            this.mnuShowDeletedToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.mnuShowDeletedToolStripMenuItem.Text = "Показывать удалённые";
            this.mnuShowDeletedToolStripMenuItem.Click += new System.EventHandler(this.mnuShowDeletedToolStripMenuItem_Click);
            // 
            // UCDataValueList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UCDataValueList";
            this.Size = new System.Drawing.Size(565, 235);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuDeleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuShowDeletedToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button buttonColumnsVisible;
        private SOV.Common.UCList ucDicColumnsVisible;
        private System.Windows.Forms.ToolStripMenuItem mnuColumnsVisibleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuActualizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuShowTelegrammToolStripMenuItem;
    }
}
