namespace SOV.Amur.Meta
{
    partial class FormSiteFilter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSiteFilter));
            this.stationTypeComboBox = new System.Windows.Forms.ComboBox();
            this.siteTypeComboBox = new SOV.Common.UCDicComboBox();
            this.stationCodeLikeTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.stationNameLikeTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.AddrComboBox = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.orgComboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // stationTypeComboBox
            // 
            this.stationTypeComboBox.DisplayMember = "Name";
            this.stationTypeComboBox.FormattingEnabled = true;
            this.stationTypeComboBox.Location = new System.Drawing.Point(91, 12);
            this.stationTypeComboBox.Name = "stationTypeComboBox";
            this.stationTypeComboBox.Size = new System.Drawing.Size(286, 21);
            this.stationTypeComboBox.TabIndex = 4;
            this.stationTypeComboBox.ValueMember = "Id";
            // 
            // siteTypeComboBox
            // 
            this.siteTypeComboBox.CheckBoxVisible = false;
            this.siteTypeComboBox.Checked = true;
            this.siteTypeComboBox.Location = new System.Drawing.Point(91, 39);
            this.siteTypeComboBox.Name = "siteTypeComboBox";
            this.siteTypeComboBox.SelectedId = null;
            this.siteTypeComboBox.SelectedIndex = -1;
            this.siteTypeComboBox.Size = new System.Drawing.Size(286, 21);
            this.siteTypeComboBox.TabIndex = 7;
            // 
            // stationCodeLikeTextBox
            // 
            this.stationCodeLikeTextBox.Location = new System.Drawing.Point(91, 66);
            this.stationCodeLikeTextBox.Name = "stationCodeLikeTextBox";
            this.stationCodeLikeTextBox.Size = new System.Drawing.Size(100, 20);
            this.stationCodeLikeTextBox.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Код станции:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Тип пункта:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Тип станции:";
            // 
            // stationNameLikeTextBox
            // 
            this.stationNameLikeTextBox.Location = new System.Drawing.Point(148, 92);
            this.stationNameLikeTextBox.Name = "stationNameLikeTextBox";
            this.stationNameLikeTextBox.Size = new System.Drawing.Size(229, 20);
            this.stationNameLikeTextBox.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Регион:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(130, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Наименование станции:";
            // 
            // AddrComboBox
            // 
            this.AddrComboBox.FormattingEnabled = true;
            this.AddrComboBox.Location = new System.Drawing.Point(91, 118);
            this.AddrComboBox.Name = "AddrComboBox";
            this.AddrComboBox.Size = new System.Drawing.Size(286, 21);
            this.AddrComboBox.TabIndex = 16;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(15, 172);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 17;
            this.button1.Text = "Применить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(96, 172);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 18;
            this.button2.Text = "Отменить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // orgComboBox
            // 
            this.orgComboBox.FormattingEnabled = true;
            this.orgComboBox.Location = new System.Drawing.Point(91, 145);
            this.orgComboBox.Name = "orgComboBox";
            this.orgComboBox.Size = new System.Drawing.Size(286, 21);
            this.orgComboBox.TabIndex = 20;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 148);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Организация:";
            // 
            // FormSiteFilter
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(389, 200);
            this.Controls.Add(this.orgComboBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.AddrComboBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.stationNameLikeTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.stationCodeLikeTextBox);
            this.Controls.Add(this.siteTypeComboBox);
            this.Controls.Add(this.stationTypeComboBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormSiteFilter";
            this.Text = "Фильтр станций и их пунктов";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox stationTypeComboBox;
        private Common.UCDicComboBox siteTypeComboBox;
        private System.Windows.Forms.TextBox stationCodeLikeTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox stationNameLikeTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox AddrComboBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox orgComboBox;
        private System.Windows.Forms.Label label6;
    }
}