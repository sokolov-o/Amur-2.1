namespace SOV.Amur.Data
{
    partial class _DELME_FormClimate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_DELME_FormClimate));
            this.ucDataTableClimate = new SOV.Amur.Data._DELME_UCDataTableClimate();
            this.SuspendLayout();
            // 
            // ucDataTableClimate
            // 
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
            this.ResumeLayout(false);

        }

        #endregion

        private _DELME_UCDataTableClimate ucDataTableClimate;

    }
}