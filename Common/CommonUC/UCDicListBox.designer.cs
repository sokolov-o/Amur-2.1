namespace SOV.Common
{
    partial class UCDicListBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCDicListBox));
            this.dgv = new System.Windows.Forms.DataGridView();
            this.checkbox = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuShowToolbarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.addNewToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.updateToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.deleteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.selectAllToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.unselectAllToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.infoToolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.showSelectedToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.findToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.findNextToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.showOrderToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.downButton = new System.Windows.Forms.Button();
            this.upButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.checkbox,
            this.name,
            this.id});
            this.dgv.ContextMenuStrip = this.contextMenuStrip1;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(0, 25);
            this.dgv.Margin = new System.Windows.Forms.Padding(0);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.tableLayoutPanel1.SetRowSpan(this.dgv, 2);
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(274, 248);
            this.dgv.TabIndex = 1;
            this.dgv.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellValueChanged);
            this.dgv.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_RowEnter);
            this.dgv.DoubleClick += new System.EventHandler(this.dgv_DoubleClick);
            // 
            // checkbox
            // 
            this.checkbox.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.checkbox.FalseValue = "0";
            this.checkbox.HeaderText = "*";
            this.checkbox.IndeterminateValue = "";
            this.checkbox.Name = "checkbox";
            this.checkbox.TrueValue = "1";
            this.checkbox.Width = 17;
            // 
            // name
            // 
            this.name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.name.HeaderText = "Название";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // id
            // 
            this.id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.id.HeaderText = "КОД";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Width = 56;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuShowToolbarToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(330, 26);
            // 
            // mnuShowToolbarToolStripMenuItem
            // 
            this.mnuShowToolbarToolStripMenuItem.Name = "mnuShowToolbarToolStripMenuItem";
            this.mnuShowToolbarToolStripMenuItem.Size = new System.Drawing.Size(329, 22);
            this.mnuShowToolbarToolStripMenuItem.Text = "Показать/скрыть панель управления списком";
            this.mnuShowToolbarToolStripMenuItem.Click += new System.EventHandler(this.mnuShowToolbarToolStripMenuItem_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgv, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.downButton, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.upButton, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(310, 273);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel1.SetColumnSpan(this.toolStrip1, 2);
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewToolStripButton,
            this.updateToolStripButton,
            this.deleteToolStripButton,
            this.selectAllToolStripButton,
            this.unselectAllToolStripButton,
            this.infoToolStripLabel,
            this.showSelectedToolStripButton,
            this.toolStripSeparator1,
            this.findToolStripTextBox,
            this.findNextToolStripButton,
            this.toolStripSeparator2,
            this.showOrderToolStripButton,
            this.saveToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(310, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // addNewToolStripButton
            // 
            this.addNewToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addNewToolStripButton.Image = global::SOV.Common.Properties.Resources.add;
            this.addNewToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addNewToolStripButton.Name = "addNewToolStripButton";
            this.addNewToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.addNewToolStripButton.Text = "toolStripButton1";
            this.addNewToolStripButton.ToolTipText = "Добавить строку";
            this.addNewToolStripButton.Click += new System.EventHandler(this.addNewToolStripButton_Click);
            // 
            // updateToolStripButton
            // 
            this.updateToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.updateToolStripButton.Image = global::SOV.Common.Properties.Resources.edit_data;
            this.updateToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.updateToolStripButton.Name = "updateToolStripButton";
            this.updateToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.updateToolStripButton.Text = "Редактировать";
            this.updateToolStripButton.ToolTipText = "Изменить строку";
            this.updateToolStripButton.Click += new System.EventHandler(this.updateToolStripButton_Click);
            // 
            // deleteToolStripButton
            // 
            this.deleteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deleteToolStripButton.Image = global::SOV.Common.Properties.Resources.delete;
            this.deleteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteToolStripButton.Name = "deleteToolStripButton";
            this.deleteToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.deleteToolStripButton.Text = "toolStripButton1";
            this.deleteToolStripButton.ToolTipText = "Удалить строку";
            this.deleteToolStripButton.Click += new System.EventHandler(this.deleteToolStripButton_Click);
            // 
            // selectAllToolStripButton
            // 
            this.selectAllToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.selectAllToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("selectAllToolStripButton.Image")));
            this.selectAllToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.selectAllToolStripButton.Name = "selectAllToolStripButton";
            this.selectAllToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.selectAllToolStripButton.Text = "Выбрать все строки списка";
            this.selectAllToolStripButton.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // unselectAllToolStripButton
            // 
            this.unselectAllToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.unselectAllToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("unselectAllToolStripButton.Image")));
            this.unselectAllToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.unselectAllToolStripButton.Name = "unselectAllToolStripButton";
            this.unselectAllToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.unselectAllToolStripButton.Text = "Отменить выбор всех строк";
            this.unselectAllToolStripButton.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // infoToolStripLabel
            // 
            this.infoToolStripLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.infoToolStripLabel.Name = "infoToolStripLabel";
            this.infoToolStripLabel.Size = new System.Drawing.Size(13, 22);
            this.infoToolStripLabel.Text = "0";
            this.infoToolStripLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // showSelectedToolStripButton
            // 
            this.showSelectedToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.showSelectedToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("showSelectedToolStripButton.Image")));
            this.showSelectedToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showSelectedToolStripButton.Name = "showSelectedToolStripButton";
            this.showSelectedToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.showSelectedToolStripButton.Text = "Показать только выбранные строки";
            this.showSelectedToolStripButton.Click += new System.EventHandler(this.showSelectedToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // findToolStripTextBox
            // 
            this.findToolStripTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.findToolStripTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.findToolStripTextBox.Name = "findToolStripTextBox";
            this.findToolStripTextBox.Size = new System.Drawing.Size(50, 25);
            this.findToolStripTextBox.ToolTipText = "Введите чать наименования региона для поиска. Нажмите \"бинокль\".";
            this.findToolStripTextBox.TextChanged += new System.EventHandler(this.findToolStripTextBox_TextChanged);
            // 
            // findNextToolStripButton
            // 
            this.findNextToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.findNextToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("findNextToolStripButton.Image")));
            this.findNextToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.findNextToolStripButton.Name = "findNextToolStripButton";
            this.findNextToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.findNextToolStripButton.Text = "toolStripButton1";
            this.findNextToolStripButton.ToolTipText = "Найти следующее совпадение";
            this.findNextToolStripButton.Click += new System.EventHandler(this.findNextToolStripButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // showOrderToolStripButton
            // 
            this.showOrderToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.showOrderToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("showOrderToolStripButton.Image")));
            this.showOrderToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showOrderToolStripButton.Name = "showOrderToolStripButton";
            this.showOrderToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.showOrderToolStripButton.ToolTipText = "Изменить порядок строк списка";
            this.showOrderToolStripButton.Click += new System.EventHandler(this.showOrderToolStripButton_Click);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = global::SOV.Common.Properties.Resources.save_black;
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.saveToolStripButton.Text = "toolStripButton1";
            this.saveToolStripButton.ToolTipText = "Сохранить изменения";
            this.saveToolStripButton.Click += new System.EventHandler(this.toolStripButton1_Click_1);
            // 
            // downButton
            // 
            this.downButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.downButton.Image = ((System.Drawing.Image)(resources.GetObject("downButton.Image")));
            this.downButton.Location = new System.Drawing.Point(277, 152);
            this.downButton.Name = "downButton";
            this.downButton.Size = new System.Drawing.Size(30, 30);
            this.downButton.TabIndex = 2;
            this.downButton.UseVisualStyleBackColor = true;
            this.downButton.Click += new System.EventHandler(this.downButton_Click);
            // 
            // upButton
            // 
            this.upButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.upButton.Image = ((System.Drawing.Image)(resources.GetObject("upButton.Image")));
            this.upButton.Location = new System.Drawing.Point(277, 116);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(30, 30);
            this.upButton.TabIndex = 3;
            this.upButton.UseVisualStyleBackColor = true;
            this.upButton.Click += new System.EventHandler(this.upButton_Click);
            // 
            // UCDicListBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UCDicListBox";
            this.Size = new System.Drawing.Size(310, 273);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton selectAllToolStripButton;
        private System.Windows.Forms.ToolStripButton unselectAllToolStripButton;
        private System.Windows.Forms.DataGridViewCheckBoxColumn checkbox;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.ToolStripLabel infoToolStripLabel;
        private System.Windows.Forms.ToolStripButton showSelectedToolStripButton;
        private System.Windows.Forms.ToolStripTextBox findToolStripTextBox;
        private System.Windows.Forms.ToolStripButton findNextToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Button downButton;
        private System.Windows.Forms.Button upButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton showOrderToolStripButton;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuShowToolbarToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton addNewToolStripButton;
        private System.Windows.Forms.ToolStripButton updateToolStripButton;
        private System.Windows.Forms.ToolStripButton deleteToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
    }
}
