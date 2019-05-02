namespace SOV.Common
{
    partial class FormDateTimePeriod
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
            SOV.Common.DateTimePeriod dateTimePeriod1 = new SOV.Common.DateTimePeriod();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDateTimePeriod));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.uc = new SOV.Common.UCDateTimePeriod();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(12, 37);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Применить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(93, 37);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Отменить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // uc
            // 
            this.uc.CustomDateFormat = "dd.MM.yyyy HH:mm";
            dateTimePeriod1.DaysBeforeDateNow = 7;
            dateTimePeriod1.PeriodType = SOV.Common.DateTimePeriod.Type.Period;
            this.uc.DateTimePeriod = dateTimePeriod1;
            this.uc.Location = new System.Drawing.Point(9, 9);
            this.uc.Margin = new System.Windows.Forms.Padding(0);
            this.uc.Name = "uc";
            this.uc.Size = new System.Drawing.Size(493, 25);
            this.uc.TabIndex = 0;
            this.uc.VisibleCountTextBox = false;
            this.uc.VisibleDateCheckBoxs = true;
            this.uc.VisibleDateSF = true;
            this.uc.VisibleViewTypeCheckBox = true;
            // 
            // FormDateTimePeriod
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(503, 67);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.uc);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDateTimePeriod";
            this.Text = "Определение временного периода";
            this.ResumeLayout(false);

        }

        #endregion

        private UCDateTimePeriod uc;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}