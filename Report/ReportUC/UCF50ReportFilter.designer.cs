namespace SOV.Amur.Reports
{
    partial class UCF50ReportFilter
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
            this.yearTextBox = new System.Windows.Forms.TextBox();
            this.decadeOfMonthTextBox = new System.Windows.Forms.TextBox();
            this.monthTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.siteGroupComboBox = new SOV.Common.UCDicComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.flagAQCComboBox = new SOV.Common.UCDicComboBox();
            this.timeUnitComboBox = new SOV.Common.UCDicComboBox();
            this.SuspendLayout();
            // 
            // yearTextBox
            // 
            this.yearTextBox.Location = new System.Drawing.Point(183, 33);
            this.yearTextBox.Name = "yearTextBox";
            this.yearTextBox.Size = new System.Drawing.Size(49, 20);
            this.yearTextBox.TabIndex = 2;
            // 
            // decadeOfMonthTextBox
            // 
            this.decadeOfMonthTextBox.Location = new System.Drawing.Point(293, 33);
            this.decadeOfMonthTextBox.Name = "decadeOfMonthTextBox";
            this.decadeOfMonthTextBox.Size = new System.Drawing.Size(49, 20);
            this.decadeOfMonthTextBox.TabIndex = 4;
            this.decadeOfMonthTextBox.Visible = false;
            // 
            // monthTextBox
            // 
            this.monthTextBox.Location = new System.Drawing.Point(238, 33);
            this.monthTextBox.Name = "monthTextBox";
            this.monthTextBox.Size = new System.Drawing.Size(49, 20);
            this.monthTextBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Год, месяц";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Группа постов";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Временной интервал";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // siteGroupComboBox
            // 
            this.siteGroupComboBox.CheckBoxVisible = false;
            this.siteGroupComboBox.Checked = true;
            this.siteGroupComboBox.Location = new System.Drawing.Point(122, 59);
            this.siteGroupComboBox.Name = "siteGroupComboBox";
            this.siteGroupComboBox.SelectedIndex = -1;
            this.siteGroupComboBox.Size = new System.Drawing.Size(220, 21);
            this.siteGroupComboBox.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Флаг АКК";
            // 
            // flagAQCComboBox
            // 
            this.flagAQCComboBox.CheckBoxVisible = false;
            this.flagAQCComboBox.Checked = true;
            this.flagAQCComboBox.Location = new System.Drawing.Point(122, 86);
            this.flagAQCComboBox.Name = "flagAQCComboBox";
            this.flagAQCComboBox.SelectedIndex = -1;
            this.flagAQCComboBox.Size = new System.Drawing.Size(220, 21);
            this.flagAQCComboBox.TabIndex = 9;
            // 
            // timeTypeComboBox
            // 
            this.timeUnitComboBox.CheckBoxVisible = false;
            this.timeUnitComboBox.Checked = true;
            this.timeUnitComboBox.Location = new System.Drawing.Point(123, 9);
            this.timeUnitComboBox.Name = "timeTypeComboBox";
            this.timeUnitComboBox.SelectedIndex = -1;
            this.timeUnitComboBox.Size = new System.Drawing.Size(219, 21);
            this.timeUnitComboBox.TabIndex = 11;
            // 
            // UCF50ReportFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.timeUnitComboBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.flagAQCComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.monthTextBox);
            this.Controls.Add(this.decadeOfMonthTextBox);
            this.Controls.Add(this.yearTextBox);
            this.Controls.Add(this.siteGroupComboBox);
            this.Name = "UCF50ReportFilter";
            this.Size = new System.Drawing.Size(345, 110);
            this.Load += new System.EventHandler(this.UCF50ReportFilter_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Common.UCDicComboBox siteGroupComboBox;
        private System.Windows.Forms.TextBox yearTextBox;
        private System.Windows.Forms.TextBox decadeOfMonthTextBox;
        private System.Windows.Forms.TextBox monthTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private Common.UCDicComboBox flagAQCComboBox;
        private Common.UCDicComboBox timeUnitComboBox;
    }
}
