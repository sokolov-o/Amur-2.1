namespace SOV.Amur.Meta
{
    partial class FormEntityGroup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEntityGroup));
            this.ucEntityGroup1 = new SOV.Amur.Meta.UCEntityGroup();
            this.SuspendLayout();
            // 
            // ucEntityGroup1
            // 
            this.ucEntityGroup1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucEntityGroup1.Location = new System.Drawing.Point(0, 0);
            this.ucEntityGroup1.Name = "ucEntityGroup1";
            this.ucEntityGroup1.Size = new System.Drawing.Size(698, 620);
            this.ucEntityGroup1.TabIndex = 0;
            this.ucEntityGroup1.UCSelectedItemChanged += new SOV.Amur.Meta.UCEntityGroup.UCSelectedEntityGroupChangedEventHandler(this.ucEntityGroup1_UCSelectedItemChanged);
            this.ucEntityGroup1.Load += new System.EventHandler(this.ucEntityGroup1_Load);
            // 
            // FormEntityGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 620);
            this.Controls.Add(this.ucEntityGroup1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormEntityGroup";
            this.Text = "Группы элементов справочников";
            this.ResumeLayout(false);

        }

        #endregion

        private UCEntityGroup ucEntityGroup1;
    }
}