namespace SOV.Common.TableIUD
{
    partial class FormTableView<T, F>
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
            this.components = new System.ComponentModel.Container();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.insertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.insertToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.updateToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.deleteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.refreshToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.viewTypeStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.gridTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.treeTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.insertToolStripMenuItem,
            this.updateToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.refreshToolStripMenuItem});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(155, 92);
            // 
            // insertToolStripMenuItem
            // 
            this.insertToolStripMenuItem.Name = "insertToolStripMenuItem";
            this.insertToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.insertToolStripMenuItem.Text = "Создать";
            this.insertToolStripMenuItem.Click += new System.EventHandler(this.defaultOnInsertClick);
            // 
            // updateToolStripMenuItem
            // 
            this.updateToolStripMenuItem.Name = "updateToolStripMenuItem";
            this.updateToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.updateToolStripMenuItem.Text = "Редактировать";
            this.updateToolStripMenuItem.Click += new System.EventHandler(this.defaultOnUpdateClick);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.deleteToolStripMenuItem.Text = "Удалить";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripButton_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.refreshToolStripMenuItem.Text = "Обновить";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.defaultOnRefreshViwer);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.insertToolStripButton,
            this.updateToolStripButton,
            this.deleteToolStripButton,
            this.refreshToolStripButton,
            this.viewTypeStripDropDownButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(410, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // insertToolStripButton
            // 
            this.insertToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.insertToolStripButton.Image = global::SOV.Common.Properties.Resources.add;
            this.insertToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.insertToolStripButton.Name = "insertToolStripButton";
            this.insertToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.insertToolStripButton.Text = "Создать";
            this.insertToolStripButton.Click += new System.EventHandler(this.defaultOnInsertClick);
            // 
            // updateToolStripButton
            // 
            this.updateToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.updateToolStripButton.Image = global::SOV.Common.Properties.Resources.edit_data;
            this.updateToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.updateToolStripButton.Name = "updateToolStripButton";
            this.updateToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.updateToolStripButton.Text = "Редактировать";
            this.updateToolStripButton.Click += new System.EventHandler(this.defaultOnUpdateClick);
            // 
            // deleteToolStripButton
            // 
            this.deleteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deleteToolStripButton.Image = global::SOV.Common.Properties.Resources.delete;
            this.deleteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteToolStripButton.Name = "deleteToolStripButton";
            this.deleteToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.deleteToolStripButton.Text = "Удалить";
            this.deleteToolStripButton.Click += new System.EventHandler(this.deleteToolStripButton_Click);
            // 
            // refreshToolStripButton
            // 
            this.refreshToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.refreshToolStripButton.Image = global::SOV.Common.Properties.Resources.refresh;
            this.refreshToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.refreshToolStripButton.Name = "refreshToolStripButton";
            this.refreshToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.refreshToolStripButton.Text = "Обновить";
            this.refreshToolStripButton.Click += new System.EventHandler(this.defaultOnRefreshViwer);
            // 
            // viewTypeStripDropDownButton
            // 
            this.viewTypeStripDropDownButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.viewTypeStripDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.viewTypeStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gridTypeToolStripMenuItem,
            this.treeTypeToolStripMenuItem});
            this.viewTypeStripDropDownButton.Image = global::SOV.Common.Properties.Resources.clone;
            this.viewTypeStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.viewTypeStripDropDownButton.Name = "viewTypeStripDropDownButton";
            this.viewTypeStripDropDownButton.Size = new System.Drawing.Size(29, 22);
            this.viewTypeStripDropDownButton.Text = "Вид";
            // 
            // gridTypeToolStripMenuItem
            // 
            this.gridTypeToolStripMenuItem.Name = "gridTypeToolStripMenuItem";
            this.gridTypeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.gridTypeToolStripMenuItem.Text = "Таблица";
            this.gridTypeToolStripMenuItem.Click += new System.EventHandler(this.gridTypeToolStripMenuItem_Click);
            // 
            // treeTypeToolStripMenuItem
            // 
            this.treeTypeToolStripMenuItem.Enabled = false;
            this.treeTypeToolStripMenuItem.Name = "treeTypeToolStripMenuItem";
            this.treeTypeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.treeTypeToolStripMenuItem.Text = "Дерево";
            this.treeTypeToolStripMenuItem.Click += new System.EventHandler(this.treeTypeToolStripMenuItem_Click);
            // 
            // FormTableView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 262);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FormTableView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Просмотр таблицы ";
            this.Load += new System.EventHandler(this.defaultOnRefreshViwer);
            this.contextMenu.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem insertToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton insertToolStripButton;
        private System.Windows.Forms.ToolStripButton updateToolStripButton;
        private System.Windows.Forms.ToolStripButton deleteToolStripButton;
        private System.Windows.Forms.ToolStripButton refreshToolStripButton;
        private System.Windows.Forms.ToolStripDropDownButton viewTypeStripDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem gridTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem treeTypeToolStripMenuItem;
    }
}