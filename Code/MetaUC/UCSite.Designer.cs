namespace SOV.Amur.Meta
{
    partial class UCSite
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
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.codeTextBox = new System.Windows.Forms.TextBox();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.siteTypeComboBox = new System.Windows.Forms.ComboBox();
            this.siteTypeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.nameEngTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.regionComboBox = new System.Windows.Forms.ComboBox();
            this.addrBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.orgComboBox = new System.Windows.Forms.ComboBox();
            this.legalEntityBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.siteTypeBindingSource)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.addrBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.legalEntityBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // idTextBox
            // 
            this.idTextBox.Enabled = false;
            this.idTextBox.Location = new System.Drawing.Point(203, 3);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(54, 20);
            this.idTextBox.TabIndex = 0;
            this.idTextBox.TabStop = false;
            // 
            // codeTextBox
            // 
            this.codeTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codeTextBox.Location = new System.Drawing.Point(86, 3);
            this.codeTextBox.Name = "codeTextBox";
            this.codeTextBox.Size = new System.Drawing.Size(111, 20);
            this.codeTextBox.TabIndex = 0;
            // 
            // nameTextBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.nameTextBox, 2);
            this.nameTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nameTextBox.Location = new System.Drawing.Point(86, 30);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(171, 20);
            this.nameTextBox.TabIndex = 2;
            // 
            // siteTypeComboBox
            // 
            this.siteTypeComboBox.DataSource = this.siteTypeBindingSource;
            this.siteTypeComboBox.DisplayMember = "Name";
            this.siteTypeComboBox.FormattingEnabled = true;
            this.siteTypeComboBox.Location = new System.Drawing.Point(325, 3);
            this.siteTypeComboBox.Name = "siteTypeComboBox";
            this.siteTypeComboBox.Size = new System.Drawing.Size(171, 21);
            this.siteTypeComboBox.TabIndex = 1;
            this.siteTypeComboBox.ValueMember = "Id";
            // 
            // siteTypeBindingSource
            // 
            this.siteTypeBindingSource.DataSource = typeof(SOV.Common.IdName);
            // 
            // nameEngTextBox
            // 
            this.nameEngTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nameEngTextBox.Location = new System.Drawing.Point(325, 30);
            this.nameEngTextBox.Name = "nameEngTextBox";
            this.nameEngTextBox.Size = new System.Drawing.Size(171, 20);
            this.nameEngTextBox.TabIndex = 3;
            this.nameEngTextBox.Text = "Не реализовано...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(3, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 26);
            this.label1.TabIndex = 5;
            this.label1.Text = "Назв рус:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(263, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 26);
            this.label2.TabIndex = 6;
            this.label2.Text = "Назв анг:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.Controls.Add(this.codeTextBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.regionComboBox, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.orgComboBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.nameTextBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.siteTypeComboBox, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.label5, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.nameEngTextBox, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.idTextBox, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(499, 80);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(263, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 27);
            this.label3.TabIndex = 7;
            this.label3.Text = "Регион:";
            // 
            // regionComboBox
            // 
            this.regionComboBox.DataSource = this.addrBindingSource;
            this.regionComboBox.DisplayMember = "Name";
            this.regionComboBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.regionComboBox.FormattingEnabled = true;
            this.regionComboBox.Location = new System.Drawing.Point(325, 56);
            this.regionComboBox.Name = "regionComboBox";
            this.regionComboBox.Size = new System.Drawing.Size(171, 21);
            this.regionComboBox.TabIndex = 5;
            this.regionComboBox.ValueMember = "Id";
            // 
            // addrBindingSource
            // 
            this.addrBindingSource.DataSource = typeof(SOV.Common.IdName);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Location = new System.Drawing.Point(3, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 27);
            this.label4.TabIndex = 9;
            this.label4.Text = "Организация:";
            // 
            // orgComboBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.orgComboBox, 2);
            this.orgComboBox.DataSource = this.legalEntityBindingSource;
            this.orgComboBox.DisplayMember = "Name";
            this.orgComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orgComboBox.FormattingEnabled = true;
            this.orgComboBox.Location = new System.Drawing.Point(86, 56);
            this.orgComboBox.Name = "orgComboBox";
            this.orgComboBox.Size = new System.Drawing.Size(171, 21);
            this.orgComboBox.TabIndex = 4;
            this.orgComboBox.ValueMember = "Id";
            // 
            // legalEntityBindingSource
            // 
            this.legalEntityBindingSource.DataSource = typeof(SOV.Common.IdName);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(261, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Тип:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.label6.Location = new System.Drawing.Point(1, 0);
            this.label6.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 27);
            this.label6.TabIndex = 12;
            this.label6.Text = "Индекс:";
            // 
            // UCSite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UCSite";
            this.Size = new System.Drawing.Size(499, 80);
            this.Load += new System.EventHandler(this.UCSite_Load);
            ((System.ComponentModel.ISupportInitialize)(this.siteTypeBindingSource)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.addrBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.legalEntityBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.TextBox codeTextBox;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.ComboBox siteTypeComboBox;
        private System.Windows.Forms.TextBox nameEngTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox regionComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox orgComboBox;
        private System.Windows.Forms.BindingSource addrBindingSource;
        private System.Windows.Forms.BindingSource legalEntityBindingSource;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.BindingSource siteTypeBindingSource;
    }
}
