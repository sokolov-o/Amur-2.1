namespace SOV.Amur.Data
{
    partial class FormCurveProperties
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCurveProperties));
            this.curveDescriptionCheckBox = new System.Windows.Forms.CheckBox();
            this.seriaDescriptionCheckBox = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // curveDescriptionCheckBox
            // 
            this.curveDescriptionCheckBox.AutoSize = true;
            this.curveDescriptionCheckBox.Location = new System.Drawing.Point(6, 19);
            this.curveDescriptionCheckBox.Name = "curveDescriptionCheckBox";
            this.curveDescriptionCheckBox.Size = new System.Drawing.Size(107, 17);
            this.curveDescriptionCheckBox.TabIndex = 0;
            this.curveDescriptionCheckBox.Text = "к кривым поста";
            this.curveDescriptionCheckBox.UseVisualStyleBackColor = true;
            // 
            // seriaDescriptionCheckBox
            // 
            this.seriaDescriptionCheckBox.AutoSize = true;
            this.seriaDescriptionCheckBox.Location = new System.Drawing.Point(119, 19);
            this.seriaDescriptionCheckBox.Name = "seriaDescriptionCheckBox";
            this.seriaDescriptionCheckBox.Size = new System.Drawing.Size(130, 17);
            this.seriaDescriptionCheckBox.TabIndex = 1;
            this.seriaDescriptionCheckBox.Text = "к выбранной кривой";
            this.seriaDescriptionCheckBox.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(12, 63);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Применить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(93, 63);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Отменить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.curveDescriptionCheckBox);
            this.groupBox1.Controls.Add(this.seriaDescriptionCheckBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(307, 45);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Отображать примечания";
            // 
            // FormCurveProperties
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(331, 95);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCurveProperties";
            this.Text = "Свойства формы для кривых";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox curveDescriptionCheckBox;
        private System.Windows.Forms.CheckBox seriaDescriptionCheckBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}