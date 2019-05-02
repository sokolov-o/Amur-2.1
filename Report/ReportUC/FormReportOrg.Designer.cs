namespace SOV.Amur.Reports
{
    partial class FormReportOrg
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.orgTab = new System.Windows.Forms.TabPage();
            this.imagesTab = new System.Windows.Forms.TabPage();
            this.topPanel.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // topPanel
            // 
            this.topPanel.Controls.Add(this.tabControl);
            this.topPanel.Size = new System.Drawing.Size(325, 235);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.orgTab);
            this.tabControl.Controls.Add(this.imagesTab);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(325, 235);
            this.tabControl.TabIndex = 1;
            // 
            // orgTab
            // 
            this.orgTab.Location = new System.Drawing.Point(4, 22);
            this.orgTab.Name = "orgTab";
            this.orgTab.Padding = new System.Windows.Forms.Padding(3);
            this.orgTab.Size = new System.Drawing.Size(317, 209);
            this.orgTab.TabIndex = 0;
            this.orgTab.Text = "Данные";
            this.orgTab.UseVisualStyleBackColor = true;
            // 
            // imagesTab
            // 
            this.imagesTab.Location = new System.Drawing.Point(4, 22);
            this.imagesTab.Name = "imagesTab";
            this.imagesTab.Padding = new System.Windows.Forms.Padding(3);
            this.imagesTab.Size = new System.Drawing.Size(327, 0);
            this.imagesTab.TabIndex = 1;
            this.imagesTab.Text = "Изображение";
            this.imagesTab.UseVisualStyleBackColor = true;
            // 
            // FormReportOrg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 270);
            this.Name = "FormReportOrg";
            this.Text = "Организации для отчета";
            this.topPanel.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage orgTab;
        private System.Windows.Forms.TabPage imagesTab;
    }
}