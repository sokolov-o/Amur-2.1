namespace SOV.Amur.Data.Chart
{
    partial class FormSiteAndVarSelection
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
            this.ucStations = new SOV.Amur.Meta.UCStations();
            this.ucVariablesList = new SOV.Amur.Meta.UCVariablesList();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.separator = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucStations
            // 
            this.ucStations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucStations.Cursor = System.Windows.Forms.Cursors.Default;
            this.ucStations.EnableMenuStrip = false;
            this.ucStations.Location = new System.Drawing.Point(0, 0);
            this.ucStations.MultySelect = false;
            this.ucStations.Name = "ucStations";
            this.ucStations.SerchSiteInputSize = new System.Drawing.Size(130, 25);
            this.ucStations.SiteGroup = null;
            this.ucStations.SiteGroupId = null;
            this.ucStations.Size = new System.Drawing.Size(416, 290);
            this.ucStations.TabIndex = 0;
            this.ucStations.VisibleAddNewButton = false;
            this.ucStations.VisibleComplateButton = false;
            this.ucStations.VisibleEditStationButton = false;
            this.ucStations.VisibleNoSiteButton = false;
            this.ucStations.VisibleSiteGroups = false;
            this.ucStations.Load += new System.EventHandler(this.ucStations_Load);
            // 
            // ucVariablesList
            // 
            this.ucVariablesList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucVariablesList.AutoSize = true;
            this.ucVariablesList.Location = new System.Drawing.Point(428, 0);
            this.ucVariablesList.Name = "ucVariablesList";
            this.ucVariablesList.RowsViewMode = SOV.Amur.Meta.UCVariablesList.ROWS_VIEW.MINIMUM;
            this.ucVariablesList.ShowFilterButton = true;
            this.ucVariablesList.ShowToolBox = true;
            this.ucVariablesList.Size = new System.Drawing.Size(180, 290);
            this.ucVariablesList.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.saveButton);
            this.flowLayoutPanel1.Controls.Add(this.cancelButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 289);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(605, 36);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // saveButton
            // 
            this.saveButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.saveButton.Location = new System.Drawing.Point(3, 3);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 0;
            this.saveButton.Text = "Применить";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(84, 3);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Отменить";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // separator
            // 
            this.separator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.separator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.separator.Location = new System.Drawing.Point(420, 8);
            this.separator.Name = "separator";
            this.separator.Size = new System.Drawing.Size(5, 275);
            this.separator.TabIndex = 3;
            // 
            // FormSiteAndVarSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 325);
            this.Controls.Add(this.separator);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.ucVariablesList);
            this.Controls.Add(this.ucStations);
            this.Name = "FormSiteAndVarSelection";
            this.Text = "Станция и переменная";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Meta.UCStations ucStations;
        private Meta.UCVariablesList ucVariablesList;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Panel separator;
    }
}