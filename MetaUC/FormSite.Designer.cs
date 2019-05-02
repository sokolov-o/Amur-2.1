namespace SOV.Amur.Meta
{
    partial class FormSite
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSite));
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.codeTtextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.stationUCTextBox = new SOV.Common.UCTextBox();
            this.siteTypeComboBox = new SOV.Common.UCDicComboBox();
            this.SuspendLayout();
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Location = new System.Drawing.Point(91, 59);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.Size = new System.Drawing.Size(320, 102);
            this.descriptionTextBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Тип пункта:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Примечание:";
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(15, 167);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Сохранить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(96, 167);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Отменить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Код пункта:";
            // 
            // codeTtextBox
            // 
            this.codeTtextBox.Location = new System.Drawing.Point(91, 6);
            this.codeTtextBox.Name = "codeTtextBox";
            this.codeTtextBox.Size = new System.Drawing.Size(320, 20);
            this.codeTtextBox.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 204);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Станция:";
            // 
            // stationUCTextBox
            // 
            this.stationUCTextBox.Location = new System.Drawing.Point(91, 204);
            this.stationUCTextBox.Margin = new System.Windows.Forms.Padding(1);
            this.stationUCTextBox.Name = "stationUCTextBox";
            this.stationUCTextBox.ShowEditButton = true;
            this.stationUCTextBox.ShowNullButton = false;
            this.stationUCTextBox.Size = new System.Drawing.Size(320, 20);
            this.stationUCTextBox.TabIndex = 10;
            this.stationUCTextBox.Value = null;
            this.stationUCTextBox.UCEditButtonPressedEvent += new SOV.Common.UCTextBox.UCEditButtonPressedEventHandler(this.stationUCTextBox_UCEditButtonPressedEvent);
            // 
            // siteTypeComboBox
            // 
            this.siteTypeComboBox.CheckBoxVisible = false;
            this.siteTypeComboBox.Checked = true;
            this.siteTypeComboBox.Location = new System.Drawing.Point(91, 32);
            this.siteTypeComboBox.Name = "siteTypeComboBox";
            this.siteTypeComboBox.SelectedId = null;
            this.siteTypeComboBox.SelectedIndex = -1;
            this.siteTypeComboBox.Size = new System.Drawing.Size(320, 21);
            this.siteTypeComboBox.TabIndex = 1;
            // 
            // FormSite
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(423, 279);
            this.Controls.Add(this.stationUCTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.codeTtextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.siteTypeComboBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.descriptionTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSite";
            this.Text = "FormSite";
            this.Load += new System.EventHandler(this.FormSite_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private Common.UCDicComboBox siteTypeComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox codeTtextBox;
        private System.Windows.Forms.Label label4;
        private Common.UCTextBox stationUCTextBox;
    }
}