namespace SOV.Amur.Data
{
    partial class FormDataGrpSVYM
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
            this.ucDataGrpSVYM1 = new SOV.Amur.Data.UCDataGrpSVYM();
            this.SuspendLayout();
            // 
            // ucDataGrpSVYM1
            // 
            this.ucDataGrpSVYM1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDataGrpSVYM1.Location = new System.Drawing.Point(0, 0);
            this.ucDataGrpSVYM1.Name = "ucDataGrpSVYM1";
            this.ucDataGrpSVYM1.Size = new System.Drawing.Size(619, 458);
            this.ucDataGrpSVYM1.TabIndex = 0;
            // 
            // FormDataGrpSVYM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 458);
            this.Controls.Add(this.ucDataGrpSVYM1);
            this.Name = "FormDataGrpSVYM";
            this.Text = "Наполненность данными";
            this.ResumeLayout(false);

        }

        #endregion

        private UCDataGrpSVYM ucDataGrpSVYM1;
    }
}