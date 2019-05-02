namespace SOV.Amur.Data
{
    partial class UCDataTableClimate2Site
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.timeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.mnuYearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMonthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDecadeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPentadeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgv, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(315, 208);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(315, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.timeName});
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(3, 28);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.Size = new System.Drawing.Size(309, 177);
            this.dgv.TabIndex = 1;
            // 
            // timeName
            // 
            this.timeName.Frozen = true;
            this.timeName.HeaderText = "Время";
            this.timeName.Name = "timeName";
            this.timeName.ReadOnly = true;
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuYearToolStripMenuItem,
            this.mnuMonthToolStripMenuItem,
            this.mnuDecadeToolStripMenuItem,
            this.mnuPentadeToolStripMenuItem});
            this.toolStripDropDownButton1.Image = global::SOV.Amur.Data.Properties.Resources.Property_501;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
            this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
            // 
            // mnuYearToolStripMenuItem
            // 
            this.mnuYearToolStripMenuItem.Image = global::SOV.Amur.Data.Properties.Resources.CheckBoxChecked;
            this.mnuYearToolStripMenuItem.Name = "mnuYearToolStripMenuItem";
            this.mnuYearToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.mnuYearToolStripMenuItem.Text = "Год";
            this.mnuYearToolStripMenuItem.Click += new System.EventHandler(this.mnuYearToolStripMenuItem_Click);
            // 
            // mnuMonthToolStripMenuItem
            // 
            this.mnuMonthToolStripMenuItem.Image = global::SOV.Amur.Data.Properties.Resources.CheckBoxChecked;
            this.mnuMonthToolStripMenuItem.Name = "mnuMonthToolStripMenuItem";
            this.mnuMonthToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.mnuMonthToolStripMenuItem.Text = "Месяц";
            this.mnuMonthToolStripMenuItem.Click += new System.EventHandler(this.mnuMonthToolStripMenuItem_Click);
            // 
            // mnuDecadeToolStripMenuItem
            // 
            this.mnuDecadeToolStripMenuItem.Image = global::SOV.Amur.Data.Properties.Resources.CheckBoxChecked;
            this.mnuDecadeToolStripMenuItem.Name = "mnuDecadeToolStripMenuItem";
            this.mnuDecadeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.mnuDecadeToolStripMenuItem.Text = "Декада";
            this.mnuDecadeToolStripMenuItem.Click += new System.EventHandler(this.mnuDecadeToolStripMenuItem_Click);
            // 
            // mnuPentadeToolStripMenuItem
            // 
            this.mnuPentadeToolStripMenuItem.Image = global::SOV.Amur.Data.Properties.Resources.CheckBoxChecked;
            this.mnuPentadeToolStripMenuItem.Name = "mnuPentadeToolStripMenuItem";
            this.mnuPentadeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.mnuPentadeToolStripMenuItem.Text = "Пентада";
            this.mnuPentadeToolStripMenuItem.Click += new System.EventHandler(this.mnuPentadeToolStripMenuItem_Click);
            // 
            // UCDataTableClimate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UCDataTableClimate";
            this.Size = new System.Drawing.Size(315, 208);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeName;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem mnuYearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuMonthToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuDecadeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuPentadeToolStripMenuItem;
    }
}
