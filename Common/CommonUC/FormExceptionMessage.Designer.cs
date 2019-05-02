namespace SOV.Common
{
    partial class FormExceptionMessage
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
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Text = "FormExceptionMessage";

            this.textLabel = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.button = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // 
            // FormExceptionMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.MaximumSize = new System.Drawing.Size(290, 170);
            this.Controls.Add(this.button);
            this.Controls.Add(this.textLabel);
            this.Name = "FormExceptionMessage";
            this.Text = "Сообщение об ошибке";
            this.ResumeLayout(false);
            this.PerformLayout();

            // 
            // textLabel
            // 
            this.textLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                           | System.Windows.Forms.AnchorStyles.Left)
                                                                          | System.Windows.Forms.AnchorStyles.Right)));
            this.textLabel.AutoSize = true;
            this.textLabel.Location = new System.Drawing.Point(25, 25);
            this.textLabel.Name = "textLabel";
            this.textLabel.TabIndex = 0;
            // 
            // toolTip
            // 
            this.toolTip.IsBalloon = true;
            // 
            // button
            // 
            this.button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button.Location = new System.Drawing.Point(20, 100);
            this.button.Name = "button";
            this.button.Size = new System.Drawing.Size(75, 23);
            this.button.TabIndex = 1;
            this.button.Text = "OK";
            this.button.UseVisualStyleBackColor = true;
            this.button.Click += new System.EventHandler(this.button_Click);
        }

        #endregion

        private System.Windows.Forms.Label textLabel;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button button;
    }
}