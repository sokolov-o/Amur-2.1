namespace SOV.Amur
{
    partial class FormTestChart
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
            this.ucChartHydro1 = new SOV.Amur.Data.UCChartHydro(0, null);
            this.SuspendLayout();
            // 
            // ucChartHydro1
            // 
            this.ucChartHydro1.EnableAxesTitle = true;
            this.ucChartHydro1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucChartHydro1.Location = new System.Drawing.Point(0, 0);
            this.ucChartHydro1.Name = "ucChartHydro1";
            this.ucChartHydro1.Size = new System.Drawing.Size(751, 415);
            this.ucChartHydro1.TabIndex = 0;
            this.ucChartHydro1.ToolbarVisible = true;
            //this.ucChartHydro1.UCRefreshEvent += new SOV.Amur.Data.UCChartHydro.UCRefreshEventHandler(this.ucChartHydro1_UCRefreshEvent);
            // 
            // FormTestChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 415);
            this.Controls.Add(this.ucChartHydro1);
            this.Name = "FormTestChart";
            this.Text = "FormTest";
            this.ResumeLayout(false);

        }

        #endregion

        private Amur.Data.UCChartHydro ucChartHydro1;
    }
}