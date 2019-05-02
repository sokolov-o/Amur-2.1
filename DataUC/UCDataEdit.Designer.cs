namespace FERHRI.Amur.Data
{
    partial class UCDataEdit
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.ucDataTable = new FERHRI.Amur.Data.UCDataTableF1();
            this.ucDataHistory = new FERHRI.Amur.Data.UCDataValueList();
            this.ucDataAQC = new FERHRI.Amur.Data.UCDataAQC();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(623, 373);
            this.splitContainer1.SplitterDistance = 251;
            this.splitContainer1.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.ucDataTable);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(621, 249);
            this.splitContainer2.SplitterDistance = 380;
            this.splitContainer2.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.ucDataHistory);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.ucDataAQC);
            this.splitContainer3.Size = new System.Drawing.Size(237, 249);
            this.splitContainer3.SplitterDistance = 129;
            this.splitContainer3.TabIndex = 1;
            // 
            // ucDataTable
            // 
            this.ucDataTable.DataFilter = null;
            this.ucDataTable.DataFilterEnabled = false;
            this.ucDataTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDataTable.FormDataFilter = null;
            this.ucDataTable.Location = new System.Drawing.Point(0, 0);
            this.ucDataTable.Name = "ucDataTable";
            this.ucDataTable.Size = new System.Drawing.Size(380, 249);
            this.ucDataTable.TabIndex = 0;
            this.ucDataTable.UserSettings = null;
            this.ucDataTable.UCCurrentDataValueChangedEvent += new FERHRI.Amur.Data.UCDataTableF1.UCCurrentDataValueChangedEventHandler(this.ucDataTable_UCCurrentDataValueChangedEvent);
            this.ucDataTable.UCChangeOptionsEvent += new FERHRI.Amur.Data.UCDataTableF1.UCChangeOptionsEventHandler(this.ucDataTable_UCChangeOptionsEvent);
            // 
            // ucDataHistory
            // 
            this.ucDataHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDataHistory.Location = new System.Drawing.Point(0, 0);
            this.ucDataHistory.Name = "ucDataHistory";
            this.ucDataHistory.Size = new System.Drawing.Size(237, 129);
            this.ucDataHistory.TabIndex = 0;
            this.ucDataHistory.UCCurrentDataValueChangedEvent += new FERHRI.Amur.Data.UCDataValueList.UCCurrentDataValueChangedEventHandler(this.ucDataHistory_UCCurrentDataValueChangedEvent);
            this.ucDataHistory.UCCurrentDataValueActualizedEvent += new FERHRI.Amur.Data.UCDataValueList.UCCurrentDataValueActualizedEventHandler(this.ucDataHistory_UCCurrentDataValueActualizedEvent);
            // 
            // ucDataAQC
            // 
            this.ucDataAQC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDataAQC.Location = new System.Drawing.Point(0, 0);
            this.ucDataAQC.Name = "ucDataAQC";
            this.ucDataAQC.Size = new System.Drawing.Size(237, 116);
            this.ucDataAQC.TabIndex = 0;
            // 
            // UCDataEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "UCDataEdit";
            this.Size = new System.Drawing.Size(623, 373);
            this.Load += new System.EventHandler(this.UCDataEdit_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UCDataTableF1 ucDataTable;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private UCDataValueList ucDataHistory;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private UCDataAQC ucDataAQC;

    }
}
