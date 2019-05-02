namespace SOV.Common
{
    partial class UCImageGalleryLoader
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
            this.imgPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.inputPanel = new System.Windows.Forms.TableLayoutPanel();
            this.fileNamesInput = new System.Windows.Forms.TextBox();
            this.viewFilesButton = new System.Windows.Forms.Button();
            this.fileDialog = new System.Windows.Forms.OpenFileDialog();
            this.inputPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // imgPanel
            // 
            this.imgPanel.AutoScroll = true;
            this.imgPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.imgPanel.Location = new System.Drawing.Point(0, 0);
            this.imgPanel.Name = "imgPanel";
            this.imgPanel.Size = new System.Drawing.Size(165, 112);
            this.imgPanel.TabIndex = 0;
            // 
            // inputPanel
            // 
            this.inputPanel.ColumnCount = 2;
            this.inputPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.inputPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.inputPanel.Controls.Add(this.fileNamesInput, 0, 0);
            this.inputPanel.Controls.Add(this.viewFilesButton, 1, 0);
            this.inputPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.inputPanel.Location = new System.Drawing.Point(0, 118);
            this.inputPanel.Name = "inputPanel";
            this.inputPanel.RowCount = 1;
            this.inputPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.inputPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.inputPanel.Size = new System.Drawing.Size(165, 44);
            this.inputPanel.TabIndex = 4;
            // 
            // fileNamesInput
            // 
            this.fileNamesInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.fileNamesInput.Location = new System.Drawing.Point(3, 12);
            this.fileNamesInput.Name = "fileNamesInput";
            this.fileNamesInput.Size = new System.Drawing.Size(99, 20);
            this.fileNamesInput.TabIndex = 0;
            // 
            // viewFilesButton
            // 
            this.viewFilesButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.viewFilesButton.Location = new System.Drawing.Point(108, 12);
            this.viewFilesButton.Name = "viewFilesButton";
            this.viewFilesButton.Size = new System.Drawing.Size(52, 20);
            this.viewFilesButton.TabIndex = 1;
            this.viewFilesButton.Text = "Файлы";
            this.viewFilesButton.UseVisualStyleBackColor = true;
            this.viewFilesButton.Click += new System.EventHandler(this.viewFilesButton_Click);
            // 
            // fileDialog
            // 
            this.fileDialog.FileName = "openFileDialog1";
            this.fileDialog.Multiselect = true;
            // 
            // UCImageGalleryLoader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.inputPanel);
            this.Controls.Add(this.imgPanel);
            this.Name = "UCImageGalleryLoader";
            this.Size = new System.Drawing.Size(165, 162);
            this.inputPanel.ResumeLayout(false);
            this.inputPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel imgPanel;
        private System.Windows.Forms.TableLayoutPanel inputPanel;
        private System.Windows.Forms.TextBox fileNamesInput;
        private System.Windows.Forms.Button viewFilesButton;
        private System.Windows.Forms.OpenFileDialog fileDialog;
    }
}
