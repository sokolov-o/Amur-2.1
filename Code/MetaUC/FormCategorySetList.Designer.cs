namespace SOV.Amur.Meta
{
    partial class FormCategorySetList
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
            this.ucCategoryDefinition = new SOV.Amur.Meta.UCCategorySet();
            this.SuspendLayout();
            // 
            // ucCategoryDefinition
            // 
            this.ucCategoryDefinition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucCategoryDefinition.Language = 0;
            this.ucCategoryDefinition.Location = new System.Drawing.Point(0, 0);
            this.ucCategoryDefinition.Name = "ucCategoryDefinition";
            this.ucCategoryDefinition.Size = new System.Drawing.Size(767, 485);
            this.ucCategoryDefinition.TabIndex = 0;
            // 
            // FormCategorySetList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 485);
            this.Controls.Add(this.ucCategoryDefinition);
            this.Name = "FormCategorySetList";
            this.Text = "Категории значений";
            this.ResumeLayout(false);

        }

        #endregion

        private UCCategorySet ucCategoryDefinition;
    }
}