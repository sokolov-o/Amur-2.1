namespace SOV.Common
{
    partial class UCInterval
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
            this.rightBound = new System.Windows.Forms.NumericUpDown();
            this.leftBound = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.rightBound)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftBound)).BeginInit();
            this.SuspendLayout();
            // 
            // rightBound
            // 
            this.rightBound.Location = new System.Drawing.Point(68, 1);
            this.rightBound.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.rightBound.Name = "rightBound";
            this.rightBound.Size = new System.Drawing.Size(45, 20);
            this.rightBound.TabIndex = 0;
            this.rightBound.ValueChanged += new System.EventHandler(this.rightBound_ValueChanged);
            // 
            // leftBound
            // 
            this.leftBound.Location = new System.Drawing.Point(1, 1);
            this.leftBound.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.leftBound.Name = "leftBound";
            this.leftBound.Size = new System.Drawing.Size(45, 20);
            this.leftBound.TabIndex = 1;
            this.leftBound.ValueChanged += new System.EventHandler(this.leftBound_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(52, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(10, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "-";
            // 
            // UCInterval
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.leftBound);
            this.Controls.Add(this.rightBound);
            this.Name = "UCInterval";
            this.Size = new System.Drawing.Size(113, 20);
            ((System.ComponentModel.ISupportInitialize)(this.rightBound)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftBound)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown rightBound;
        private System.Windows.Forms.NumericUpDown leftBound;
        private System.Windows.Forms.Label label1;
    }
}
