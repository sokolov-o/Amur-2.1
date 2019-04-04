namespace SOV.Common
{
    partial class UCDateTimePeriod
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
            this.dateS = new System.Windows.Forms.DateTimePicker();
            this.dateF = new System.Windows.Forms.DateTimePicker();
            this.tlp = new System.Windows.Forms.TableLayoutPanel();
            this.periodTypeComboBox = new System.Windows.Forms.ComboBox();
            this.daysBeforeDateNowTextBox = new System.Windows.Forms.TextBox();
            this.tlp.SuspendLayout();
            this.SuspendLayout();
            // 
            // dateS
            // 
            this.dateS.CustomFormat = "dd.mm.yyyy HH:MM";
            this.dateS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dateS.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateS.Location = new System.Drawing.Point(185, 3);
            this.dateS.Name = "dateS";
            this.dateS.ShowCheckBox = true;
            this.dateS.Size = new System.Drawing.Size(149, 20);
            this.dateS.TabIndex = 0;
            this.dateS.Value = new System.DateTime(2016, 9, 23, 0, 0, 0, 0);
            // 
            // dateF
            // 
            this.dateF.CustomFormat = "dd.mm.yyyy HH:MM";
            this.dateF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dateF.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateF.Location = new System.Drawing.Point(340, 3);
            this.dateF.Name = "dateF";
            this.dateF.ShowCheckBox = true;
            this.dateF.Size = new System.Drawing.Size(150, 20);
            this.dateF.TabIndex = 1;
            this.dateF.Value = new System.DateTime(2016, 9, 23, 0, 0, 0, 0);
            // 
            // tlp
            // 
            this.tlp.ColumnCount = 4;
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp.Controls.Add(this.periodTypeComboBox, 0, 0);
            this.tlp.Controls.Add(this.dateS, 2, 0);
            this.tlp.Controls.Add(this.dateF, 3, 0);
            this.tlp.Controls.Add(this.daysBeforeDateNowTextBox, 1, 0);
            this.tlp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp.Location = new System.Drawing.Point(0, 0);
            this.tlp.Name = "tlp";
            this.tlp.RowCount = 1;
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlp.Size = new System.Drawing.Size(493, 25);
            this.tlp.TabIndex = 2;
            // 
            // periodTypeComboBox
            // 
            this.periodTypeComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.periodTypeComboBox.FormattingEnabled = true;
            this.periodTypeComboBox.Items.AddRange(new object[] {
            "Период",
            "Год, месяц",
            "N суток назад"});
            this.periodTypeComboBox.Location = new System.Drawing.Point(3, 3);
            this.periodTypeComboBox.Name = "periodTypeComboBox";
            this.periodTypeComboBox.Size = new System.Drawing.Size(120, 21);
            this.periodTypeComboBox.TabIndex = 2;
            this.periodTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.viewTypeComboBox_SelectedIndexChanged);
            // 
            // daysBeforeDateNowTextBox
            // 
            this.daysBeforeDateNowTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.daysBeforeDateNowTextBox.Location = new System.Drawing.Point(129, 3);
            this.daysBeforeDateNowTextBox.Name = "daysBeforeDateNowTextBox";
            this.daysBeforeDateNowTextBox.Size = new System.Drawing.Size(50, 20);
            this.daysBeforeDateNowTextBox.TabIndex = 3;
            // 
            // UCDateTimePeriod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlp);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UCDateTimePeriod";
            this.Size = new System.Drawing.Size(493, 25);
            this.tlp.ResumeLayout(false);
            this.tlp.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateS;
        private System.Windows.Forms.DateTimePicker dateF;
        private System.Windows.Forms.TableLayoutPanel tlp;
        private System.Windows.Forms.ComboBox periodTypeComboBox;
        private System.Windows.Forms.TextBox daysBeforeDateNowTextBox;
    }
}
