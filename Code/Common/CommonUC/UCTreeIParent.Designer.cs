using System;

namespace SOV.Common
{
    partial class UCTreeIParent
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
            this.tv = new System.Windows.Forms.TreeView();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.refreshButton = new System.Windows.Forms.ToolStripButton();
            this.editButton = new System.Windows.Forms.ToolStripButton();
            this.addItemButton = new System.Windows.Forms.ToolStripButton();
            this.cloneItemButton = new System.Windows.Forms.ToolStripButton();
            this.deleteItemButton = new System.Windows.Forms.ToolStripButton();
            this.findTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.infoLabel = new System.Windows.Forms.ToolStripLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tv
            // 
            this.tv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv.Location = new System.Drawing.Point(0, 25);
            this.tv.Margin = new System.Windows.Forms.Padding(0);
            this.tv.Name = "tv";
            this.tv.Size = new System.Drawing.Size(211, 167);
            this.tv.TabIndex = 1;
            this.tv.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterSelect);
            this.tv.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tv_NodeMouseClick);
            this.tv.DoubleClick += new System.EventHandler(this.tv_DoubleClick);
            this.tv.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tv_KeyPress);
            // 
            // toolStrip
            // 
            this.toolStrip.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshButton,
            this.editButton,
            this.addItemButton,
            this.cloneItemButton,
            this.deleteItemButton,
            this.findTextBox,
            this.infoLabel});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip.Size = new System.Drawing.Size(211, 25);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip1";
            // 
            // refreshButton
            // 
            this.refreshButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.refreshButton.Image = global::SOV.Common.Properties.Resources.refresh;
            this.refreshButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(23, 22);
            this.refreshButton.Text = "Обновить дерево";
            this.refreshButton.ToolTipText = "Обновить список";
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // editButton
            // 
            this.editButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.editButton.Image = global::SOV.Common.Properties.Resources.edit_data;
            this.editButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(23, 22);
            this.editButton.Text = "toolStripButton1";
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // addItemButton
            // 
            this.addItemButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addItemButton.Image = global::SOV.Common.Properties.Resources.add;
            this.addItemButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addItemButton.Name = "addItemButton";
            this.addItemButton.Size = new System.Drawing.Size(23, 22);
            this.addItemButton.Text = "Добавить наследника";
            this.addItemButton.Click += new System.EventHandler(this.addNewButton_Click);
            // 
            // cloneItemButton
            // 
            this.cloneItemButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cloneItemButton.Image = global::SOV.Common.Properties.Resources.clone;
            this.cloneItemButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cloneItemButton.Name = "cloneItemButton";
            this.cloneItemButton.Size = new System.Drawing.Size(23, 22);
            this.cloneItemButton.Text = "Копировать элемент";
            this.cloneItemButton.Click += new System.EventHandler(this.cloneItemButton_Click);
            // 
            // deleteItemButton
            // 
            this.deleteItemButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deleteItemButton.Image = global::SOV.Common.Properties.Resources.delete;
            this.deleteItemButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteItemButton.Name = "deleteItemButton";
            this.deleteItemButton.Size = new System.Drawing.Size(23, 22);
            this.deleteItemButton.Text = "toolStripButton1";
            this.deleteItemButton.Click += new System.EventHandler(this.deleteItemButton_Click);
            // 
            // findTextBox
            // 
            this.findTextBox.Name = "findTextBox";
            this.findTextBox.Size = new System.Drawing.Size(50, 25);
            this.findTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.findTextBox_KeyPress);
            // 
            // infoLabel
            // 
            this.infoLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(16, 22);
            this.infoLabel.Text = "...";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Window;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.toolStrip, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tv, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(211, 192);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // UCTreeIParent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UCTreeIParent";
            this.Size = new System.Drawing.Size(211, 192);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tv;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton refreshButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStripButton addItemButton;
        private System.Windows.Forms.ToolStripButton deleteItemButton;
        private System.Windows.Forms.ToolStripButton cloneItemButton;
        private System.Windows.Forms.ToolStripButton editButton;
        private System.Windows.Forms.ToolStripTextBox findTextBox;
        private System.Windows.Forms.ToolStripLabel infoLabel;
    }
}
