namespace SOV.Amur.Meta
{
    partial class UCCatalogFilter0
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCCatalogFilter0));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuUncheckAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuCheckAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuUncheckAllToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.comboGroups = new System.Windows.Forms.ComboBox();
            this.itemsTypeOfSetGroupBox = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.allRadioButton = new System.Windows.Forms.RadioButton();
            this.handsetRadioButton = new System.Windows.Forms.RadioButton();
            this.groupRadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ucList = new SOV.Common.UCList();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.itemsTypeOfSetGroupBox.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "checked");
            this.imageList1.Images.SetKeyName(1, "unchecked");
            this.imageList1.Images.SetKeyName(2, "variableGroup");
            this.imageList1.Images.SetKeyName(3, "siteGroup");
            this.imageList1.Images.SetKeyName(4, "offset");
            this.imageList1.Images.SetKeyName(5, "method");
            this.imageList1.Images.SetKeyName(6, "NavForward.png");
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuUncheckAllToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(166, 26);
            // 
            // mnuUncheckAllToolStripMenuItem
            // 
            this.mnuUncheckAllToolStripMenuItem.Name = "mnuUncheckAllToolStripMenuItem";
            this.mnuUncheckAllToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.mnuUncheckAllToolStripMenuItem.Text = "Очистить выбор";
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCheckAllToolStripMenuItem,
            this.mnuUncheckAllToolStripMenuItem1});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(166, 48);
            // 
            // mnuCheckAllToolStripMenuItem
            // 
            this.mnuCheckAllToolStripMenuItem.Name = "mnuCheckAllToolStripMenuItem";
            this.mnuCheckAllToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.mnuCheckAllToolStripMenuItem.Text = "Выбрать все";
            // 
            // mnuUncheckAllToolStripMenuItem1
            // 
            this.mnuUncheckAllToolStripMenuItem1.Name = "mnuUncheckAllToolStripMenuItem1";
            this.mnuUncheckAllToolStripMenuItem1.Size = new System.Drawing.Size(165, 22);
            this.mnuUncheckAllToolStripMenuItem1.Text = "Очистить выбор";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.comboGroups, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.itemsTypeOfSetGroupBox, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.groupBox1, 0, 2);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 66F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(242, 233);
            this.tableLayoutPanel3.TabIndex = 5;
            // 
            // comboGroups
            // 
            this.comboGroups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboGroups.FormattingEnabled = true;
            this.comboGroups.Location = new System.Drawing.Point(3, 53);
            this.comboGroups.Name = "comboGroups";
            this.comboGroups.Size = new System.Drawing.Size(236, 21);
            this.comboGroups.TabIndex = 6;
            this.comboGroups.SelectedIndexChanged += new System.EventHandler(this.combo_SelectedIndexChanged);
            // 
            // itemsTypeOfSetGroupBox
            // 
            this.itemsTypeOfSetGroupBox.Controls.Add(this.tableLayoutPanel1);
            this.itemsTypeOfSetGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.itemsTypeOfSetGroupBox.Location = new System.Drawing.Point(3, 3);
            this.itemsTypeOfSetGroupBox.Name = "itemsTypeOfSetGroupBox";
            this.itemsTypeOfSetGroupBox.Size = new System.Drawing.Size(236, 44);
            this.itemsTypeOfSetGroupBox.TabIndex = 7;
            this.itemsTypeOfSetGroupBox.TabStop = false;
            this.itemsTypeOfSetGroupBox.Text = "Тип набора значений";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.allRadioButton, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.handsetRadioButton, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupRadioButton, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(230, 25);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // allRadioButton
            // 
            this.allRadioButton.AutoSize = true;
            this.allRadioButton.Checked = true;
            this.allRadioButton.Location = new System.Drawing.Point(3, 3);
            this.allRadioButton.Name = "allRadioButton";
            this.allRadioButton.Size = new System.Drawing.Size(44, 17);
            this.allRadioButton.TabIndex = 0;
            this.allRadioButton.TabStop = true;
            this.allRadioButton.Text = "Все";
            this.allRadioButton.UseVisualStyleBackColor = true;
            this.allRadioButton.CheckedChanged += new System.EventHandler(this.allRadioButton_CheckedChanged);
            // 
            // handsetRadioButton
            // 
            this.handsetRadioButton.AutoSize = true;
            this.handsetRadioButton.Location = new System.Drawing.Point(119, 3);
            this.handsetRadioButton.Name = "handsetRadioButton";
            this.handsetRadioButton.Size = new System.Drawing.Size(57, 17);
            this.handsetRadioButton.TabIndex = 2;
            this.handsetRadioButton.Text = "Набор";
            this.handsetRadioButton.UseVisualStyleBackColor = true;
            this.handsetRadioButton.CheckedChanged += new System.EventHandler(this.setofRadioButton_CheckedChanged);
            // 
            // groupRadioButton
            // 
            this.groupRadioButton.AutoSize = true;
            this.groupRadioButton.Location = new System.Drawing.Point(53, 3);
            this.groupRadioButton.Name = "groupRadioButton";
            this.groupRadioButton.Size = new System.Drawing.Size(60, 17);
            this.groupRadioButton.TabIndex = 1;
            this.groupRadioButton.Text = "Группа";
            this.groupRadioButton.UseVisualStyleBackColor = true;
            this.groupRadioButton.CheckedChanged += new System.EventHandler(this.groupRadioButton_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ucList);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(236, 150);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Справочник";
            // 
            // ucList
            // 
            this.ucList.ColumnsHeadersVisible = false;
            this.ucList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucList.Enabled = false;
            this.ucList.Location = new System.Drawing.Point(3, 16);
            this.ucList.MultiSelect = false;
            this.ucList.Name = "ucList";
            this.ucList.ShowAddNewToolbarButton = false;
            this.ucList.ShowColumnHeaders = false;
            this.ucList.ShowDeleteToolbarButton = false;
            this.ucList.ShowFindItemToolbarButton = false;
            this.ucList.ShowId = true;
            this.ucList.ShowOrderControls = false;
            this.ucList.ShowOrderToolbarButton = false;
            this.ucList.ShowSaveToolbarButton = false;
            this.ucList.ShowSelectAllToolbarButton = false;
            this.ucList.ShowSelectedOnly = false;
            this.ucList.ShowSelectedOnlyToolbarButton = false;
            this.ucList.ShowToolbar = false;
            this.ucList.ShowUnselectAllToolbarButton = false;
            this.ucList.ShowUpdateToolbarButton = false;
            this.ucList.Size = new System.Drawing.Size(230, 131);
            this.ucList.TabIndex = 4;
            // 
            // UCCatalogFilter0
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel3);
            this.Name = "UCCatalogFilter0";
            this.Size = new System.Drawing.Size(242, 233);
            this.Load += new System.EventHandler(this.UCCatalogFilter0_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.itemsTypeOfSetGroupBox.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuUncheckAllToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem mnuCheckAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuUncheckAllToolStripMenuItem1;
        private Common.UCList ucList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.ComboBox comboGroups;
        private System.Windows.Forms.RadioButton allRadioButton;
        private System.Windows.Forms.RadioButton handsetRadioButton;
        private System.Windows.Forms.RadioButton groupRadioButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox itemsTypeOfSetGroupBox;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
