namespace SOV.Amur.Meta
{
    partial class UCCatalogFilterG
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCCatalogFilterG));
            this.tv = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuUncheckAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuCheckAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuUncheckAllToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.findToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.findNextToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tv
            // 
            this.tv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv.ImageIndex = 0;
            this.tv.ImageList = this.imageList1;
            this.tv.Location = new System.Drawing.Point(0, 25);
            this.tv.Margin = new System.Windows.Forms.Padding(0);
            this.tv.Name = "tv";
            this.tv.SelectedImageIndex = 0;
            this.tv.Size = new System.Drawing.Size(263, 254);
            this.tv.TabIndex = 0;
            this.tv.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tv_NodeMouseClick);
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
            this.mnuUncheckAllToolStripMenuItem.Click += new System.EventHandler(this.mnuUncheckAllToolStripMenuItem_Click);
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
            this.mnuCheckAllToolStripMenuItem.Click += new System.EventHandler(this.mnuCheckAllToolStripMenuItem_Click);
            // 
            // mnuUncheckAllToolStripMenuItem1
            // 
            this.mnuUncheckAllToolStripMenuItem1.Name = "mnuUncheckAllToolStripMenuItem1";
            this.mnuUncheckAllToolStripMenuItem1.Size = new System.Drawing.Size(165, 22);
            this.mnuUncheckAllToolStripMenuItem1.Text = "Очистить выбор";
            this.mnuUncheckAllToolStripMenuItem1.Click += new System.EventHandler(this.mnuUncheckAllToolStripMenuItem1_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tv, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(263, 279);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.findToolStripTextBox,
            this.findNextToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(263, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(44, 22);
            this.toolStripLabel1.Text = "Найти:";
            // 
            // findToolStripTextBox
            // 
            this.findToolStripTextBox.BackColor = System.Drawing.SystemColors.Info;
            this.findToolStripTextBox.Name = "findToolStripTextBox";
            this.findToolStripTextBox.Size = new System.Drawing.Size(100, 25);
            this.findToolStripTextBox.TextChanged += new System.EventHandler(this.findToolStripTextBox_TextChanged);
            // 
            // findNextToolStripButton
            // 
            this.findNextToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.findNextToolStripButton.Image = global::SOV.Amur.Meta.Properties.Resources.Find_5650;
            this.findNextToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.findNextToolStripButton.Name = "findNextToolStripButton";
            this.findNextToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.findNextToolStripButton.Text = "toolStripButton1";
            this.findNextToolStripButton.Click += new System.EventHandler(this.findNextToolStripButton_Click);
            // 
            // UCCatalogKeyTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UCCatalogKeyTree";
            this.Size = new System.Drawing.Size(263, 279);
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tv;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuUncheckAllToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem mnuCheckAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuUncheckAllToolStripMenuItem1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripTextBox findToolStripTextBox;
        private System.Windows.Forms.ToolStripButton findNextToolStripButton;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
    }
}
