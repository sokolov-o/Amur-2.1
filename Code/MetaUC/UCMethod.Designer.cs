namespace SOV.Amur.Meta
{
    partial class UCMethod
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
            this.label1 = new System.Windows.Forms.Label();
            this.methodNameTextBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.methodDetailsTypeComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.deleteMethodDetailsButton = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.outputStoreParametersTextBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.orderTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.ucLESource = new SOV.Common.UCTextBox();
            this.ucParentMethod = new SOV.Common.UCTextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Метод:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // methodNameTextBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.methodNameTextBox, 3);
            this.methodNameTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.methodNameTextBox.Location = new System.Drawing.Point(101, 3);
            this.methodNameTextBox.Name = "methodNameTextBox";
            this.methodNameTextBox.Size = new System.Drawing.Size(273, 20);
            this.methodNameTextBox.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.methodNameTextBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.orderTextBox, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.label5, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.idTextBox, 3, 5);
            this.tableLayoutPanel1.Controls.Add(this.ucLESource, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.ucParentMethod, 1, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(377, 269);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Info;
            this.tableLayoutPanel1.SetColumnSpan(this.toolStrip1, 4);
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.methodDetailsTypeComboBox,
            this.deleteMethodDetailsButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 244);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(377, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(91, 22);
            this.toolStripLabel1.Text = "Детали метода:";
            // 
            // methodDetailsTypeComboBox
            // 
            this.methodDetailsTypeComboBox.Name = "methodDetailsTypeComboBox";
            this.methodDetailsTypeComboBox.Size = new System.Drawing.Size(121, 25);
            this.methodDetailsTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.methodDetailsTypeComboBox_SelectedIndexChanged);
            // 
            // deleteMethodDetailsButton
            // 
            this.deleteMethodDetailsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deleteMethodDetailsButton.Image = global::SOV.Amur.Meta.Properties.Resources.DeleteHS;
            this.deleteMethodDetailsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteMethodDetailsButton.Name = "deleteMethodDetailsButton";
            this.deleteMethodDetailsButton.Size = new System.Drawing.Size(23, 22);
            this.deleteMethodDetailsButton.Text = "toolStripButton1";
            this.deleteMethodDetailsButton.Click += new System.EventHandler(this.deleteMethodDetailsButton_Click);
            // 
            // groupBox1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox1, 4);
            this.groupBox1.Controls.Add(this.outputStoreParametersTextBox);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 125);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(371, 68);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Параметры хранилища метода";
            // 
            // outputStoreParametersTextBox
            // 
            this.outputStoreParametersTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputStoreParametersTextBox.Location = new System.Drawing.Point(3, 16);
            this.outputStoreParametersTextBox.Multiline = true;
            this.outputStoreParametersTextBox.Name = "outputStoreParametersTextBox";
            this.outputStoreParametersTextBox.Size = new System.Drawing.Size(365, 49);
            this.outputStoreParametersTextBox.TabIndex = 4;
            // 
            // groupBox2
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox2, 4);
            this.groupBox2.Controls.Add(this.descriptionTextBox);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 51);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(371, 68);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Описание метода";
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.descriptionTextBox.Location = new System.Drawing.Point(3, 16);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.Size = new System.Drawing.Size(365, 49);
            this.descriptionTextBox.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(3, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 22);
            this.label2.TabIndex = 9;
            this.label2.Text = "Источник:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(3, 196);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 22);
            this.label3.TabIndex = 10;
            this.label3.Text = "Метод-родитель:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 218);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "По порядку:";
            // 
            // orderTextBox
            // 
            this.orderTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orderTextBox.Location = new System.Drawing.Point(101, 221);
            this.orderTextBox.Name = "orderTextBox";
            this.orderTextBox.Size = new System.Drawing.Size(121, 20);
            this.orderTextBox.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(228, 218);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(19, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Id:";
            // 
            // idTextBox
            // 
            this.idTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.idTextBox.Enabled = false;
            this.idTextBox.Location = new System.Drawing.Point(253, 221);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.ReadOnly = true;
            this.idTextBox.Size = new System.Drawing.Size(121, 20);
            this.idTextBox.TabIndex = 15;
            // 
            // ucLESource
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.ucLESource, 3);
            this.ucLESource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucLESource.Location = new System.Drawing.Point(99, 27);
            this.ucLESource.Margin = new System.Windows.Forms.Padding(1);
            this.ucLESource.Name = "ucLESource";
            this.ucLESource.ShowEditButton = true;
            this.ucLESource.ShowNullButton = false;
            this.ucLESource.Size = new System.Drawing.Size(277, 20);
            this.ucLESource.TabIndex = 16;
            this.ucLESource.Value = null;
            this.ucLESource.UCEditButtonPressedEvent += new SOV.Common.UCTextBox.UCEditButtonPressedEventHandler(this.ucLESource_UCEditButtonPressedEvent);
            // 
            // ucParentMethod
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.ucParentMethod, 3);
            this.ucParentMethod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucParentMethod.Location = new System.Drawing.Point(99, 197);
            this.ucParentMethod.Margin = new System.Windows.Forms.Padding(1);
            this.ucParentMethod.Name = "ucParentMethod";
            this.ucParentMethod.ShowEditButton = true;
            this.ucParentMethod.ShowNullButton = false;
            this.ucParentMethod.Size = new System.Drawing.Size(277, 20);
            this.ucParentMethod.TabIndex = 17;
            this.ucParentMethod.Value = null;
            this.ucParentMethod.UCEditButtonPressedEvent += new SOV.Common.UCTextBox.UCEditButtonPressedEventHandler(this.parentMethodTextBox_UCEditButtonPressedEvent);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.Info;
            this.splitContainer1.Size = new System.Drawing.Size(381, 379);
            this.splitContainer1.SplitterDistance = 273;
            this.splitContainer1.TabIndex = 3;
            // 
            // UCMethod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "UCMethod";
            this.Size = new System.Drawing.Size(381, 379);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox methodNameTextBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox outputStoreParametersTextBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox orderTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Common.UCTextBox ucLESource;
        private Common.UCTextBox ucParentMethod;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox methodDetailsTypeComboBox;
        private System.Windows.Forms.ToolStripButton deleteMethodDetailsButton;
    }
}
