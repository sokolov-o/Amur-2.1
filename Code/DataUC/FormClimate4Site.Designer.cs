namespace SOV.Amur.Data
{
    partial class FormClimate4Site
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormClimate4Site));
            this.ucDataTableClimate = new SOV.Amur.Data.UCDataTableClimate2Site();
            this.SuspendLayout();
            // 
            // ucDataTableClimate
            // 
            this.ucDataTableClimate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDataTableClimate.Location = new System.Drawing.Point(0, 0);
            this.ucDataTableClimate.Name = "ucDataTableClimate";
            this.ucDataTableClimate.Size = new System.Drawing.Size(558, 318);
            this.ucDataTableClimate.TabIndex = 0;
            // 
            // FormClimate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 318);
            this.Controls.Add(this.ucDataTableClimate);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormClimate";
            this.Text = "FormClimate";
            this.Load += new System.EventHandler(this.FormClimate_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UCDataTableClimate2Site ucDataTableClimate;
    }
}