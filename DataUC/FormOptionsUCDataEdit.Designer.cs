namespace SOV.Amur.Data
{
    partial class FormOptionsUCDataEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormOptionsUCDataEdit));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.showDataDetailsCheckBox = new System.Windows.Forms.CheckBox();
            this.onlyRedCheckBox = new System.Windows.Forms.CheckBox();
            this.showChartCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.showDerivedCheckBox = new System.Windows.Forms.CheckBox();
            this.showAQCCheckBox = new System.Windows.Forms.CheckBox();
            this.showVarCodeTextCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(12, 112);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Применить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(93, 112);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Отменить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // showDataDetailsCheckBox
            // 
            this.showDataDetailsCheckBox.AutoSize = true;
            this.showDataDetailsCheckBox.Checked = true;
            this.showDataDetailsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showDataDetailsCheckBox.Location = new System.Drawing.Point(18, 12);
            this.showDataDetailsCheckBox.Name = "showDataDetailsCheckBox";
            this.showDataDetailsCheckBox.Size = new System.Drawing.Size(192, 17);
            this.showDataDetailsCheckBox.TabIndex = 2;
            this.showDataDetailsCheckBox.Text = "подробные сведения о значении";
            this.showDataDetailsCheckBox.UseVisualStyleBackColor = true;
            this.showDataDetailsCheckBox.CheckedChanged += new System.EventHandler(this.showDataDetailsCheckBox_CheckedChanged);
            // 
            // onlyRedCheckBox
            // 
            this.onlyRedCheckBox.AutoSize = true;
            this.onlyRedCheckBox.Location = new System.Drawing.Point(12, 89);
            this.onlyRedCheckBox.Name = "onlyRedCheckBox";
            this.onlyRedCheckBox.Size = new System.Drawing.Size(303, 17);
            this.onlyRedCheckBox.TabIndex = 3;
            this.onlyRedCheckBox.Text = "отображать  только строки с \"красными\" значениями";
            this.onlyRedCheckBox.UseVisualStyleBackColor = true;
            // 
            // showChartCheckBox
            // 
            this.showChartCheckBox.AutoSize = true;
            this.showChartCheckBox.Checked = true;
            this.showChartCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showChartCheckBox.Location = new System.Drawing.Point(228, 12);
            this.showChartCheckBox.Name = "showChartCheckBox";
            this.showChartCheckBox.Size = new System.Drawing.Size(126, 17);
            this.showChartCheckBox.TabIndex = 4;
            this.showChartCheckBox.Text = "отображать график";
            this.showChartCheckBox.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.showDerivedCheckBox);
            this.groupBox1.Controls.Add(this.showAQCCheckBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(210, 71);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // showDerivedCheckBox
            // 
            this.showDerivedCheckBox.AutoSize = true;
            this.showDerivedCheckBox.Checked = true;
            this.showDerivedCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showDerivedCheckBox.Location = new System.Drawing.Point(20, 46);
            this.showDerivedCheckBox.Name = "showDerivedCheckBox";
            this.showDerivedCheckBox.Size = new System.Drawing.Size(138, 17);
            this.showDerivedCheckBox.TabIndex = 4;
            this.showDerivedCheckBox.Text = "родители/наследники";
            this.showDerivedCheckBox.UseVisualStyleBackColor = true;
            // 
            // showAQCCheckBox
            // 
            this.showAQCCheckBox.AutoSize = true;
            this.showAQCCheckBox.Checked = true;
            this.showAQCCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showAQCCheckBox.Location = new System.Drawing.Point(20, 23);
            this.showAQCCheckBox.Name = "showAQCCheckBox";
            this.showAQCCheckBox.Size = new System.Drawing.Size(96, 17);
            this.showAQCCheckBox.TabIndex = 3;
            this.showAQCCheckBox.Text = "критконтроль";
            this.showAQCCheckBox.UseVisualStyleBackColor = true;
            // 
            // showVarCodeTextCheckBox
            // 
            this.showVarCodeTextCheckBox.AutoSize = true;
            this.showVarCodeTextCheckBox.Location = new System.Drawing.Point(228, 35);
            this.showVarCodeTextCheckBox.Name = "showVarCodeTextCheckBox";
            this.showVarCodeTextCheckBox.Size = new System.Drawing.Size(170, 17);
            this.showVarCodeTextCheckBox.TabIndex = 6;
            this.showVarCodeTextCheckBox.Text = "текст из кода справочников";
            this.showVarCodeTextCheckBox.UseVisualStyleBackColor = true;
            // 
            // FormOptionsUCDataEdit
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(491, 142);
            this.Controls.Add(this.showVarCodeTextCheckBox);
            this.Controls.Add(this.showDataDetailsCheckBox);
            this.Controls.Add(this.showChartCheckBox);
            this.Controls.Add(this.onlyRedCheckBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormOptionsUCDataEdit";
            this.Text = "Опции отображения для формы редактирования данных";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox showDataDetailsCheckBox;
        private System.Windows.Forms.CheckBox onlyRedCheckBox;
        private System.Windows.Forms.CheckBox showChartCheckBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox showDerivedCheckBox;
        private System.Windows.Forms.CheckBox showAQCCheckBox;
        private System.Windows.Forms.CheckBox showVarCodeTextCheckBox;
    }
}