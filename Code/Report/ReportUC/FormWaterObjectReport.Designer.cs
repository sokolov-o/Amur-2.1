namespace SOV.Amur.Reports
{
    partial class FormWaterObjectReport
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
            this.topLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.stationComboBox = new System.Windows.Forms.ComboBox();
            this.stationLabel = new System.Windows.Forms.Label();
            this.orgComboBox = new System.Windows.Forms.ComboBox();
            this.orgLabel = new System.Windows.Forms.Label();
            this.periodLabel = new System.Windows.Forms.Label();
            this.typeLabel = new System.Windows.Forms.Label();
            this.typeComboBox = new System.Windows.Forms.ComboBox();
            this.periodLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.periodTypeComboBox = new System.Windows.Forms.ComboBox();
            this.periodDateS = new System.Windows.Forms.DateTimePicker();
            this.timeTypeComboBox = new System.Windows.Forms.ComboBox();
            this.bottomLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.authorPos2ComboBox = new System.Windows.Forms.ComboBox();
            this.authorPos1ComboBox = new System.Windows.Forms.ComboBox();
            this.author1ComboBox = new System.Windows.Forms.ComboBox();
            this.author2ComboBox = new System.Windows.Forms.ComboBox();
            this.author2checkBox = new System.Windows.Forms.CheckBox();
            this.author1checkBox = new System.Windows.Forms.CheckBox();
            this.doneButton = new System.Windows.Forms.Button();
            this.danger1CheckBox = new System.Windows.Forms.CheckBox();
            this.middleLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.viewLabel = new System.Windows.Forms.Label();
            this.danger2LayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.danger2CheckBox = new System.Windows.Forms.CheckBox();
            this.danger2Label = new System.Windows.Forms.Label();
            this.danger1LayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.danger1Label = new System.Windows.Forms.Label();
            this.danger2TextBox = new System.Windows.Forms.RichTextBox();
            this.danger1TextBox = new System.Windows.Forms.RichTextBox();
            this.viewTextBox = new System.Windows.Forms.RichTextBox();
            this.topLayoutPanel.SuspendLayout();
            this.periodLayoutPanel.SuspendLayout();
            this.bottomLayoutPanel.SuspendLayout();
            this.middleLayoutPanel.SuspendLayout();
            this.danger2LayoutPanel.SuspendLayout();
            this.danger1LayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // topLayoutPanel
            // 
            this.topLayoutPanel.ColumnCount = 2;
            this.topLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.topLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.topLayoutPanel.Controls.Add(this.stationComboBox, 1, 0);
            this.topLayoutPanel.Controls.Add(this.stationLabel, 0, 0);
            this.topLayoutPanel.Controls.Add(this.orgComboBox, 1, 1);
            this.topLayoutPanel.Controls.Add(this.orgLabel, 0, 1);
            this.topLayoutPanel.Controls.Add(this.periodLabel, 0, 3);
            this.topLayoutPanel.Controls.Add(this.typeLabel, 0, 2);
            this.topLayoutPanel.Controls.Add(this.typeComboBox, 1, 2);
            this.topLayoutPanel.Controls.Add(this.periodLayoutPanel, 1, 3);
            this.topLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.topLayoutPanel.Name = "topLayoutPanel";
            this.topLayoutPanel.RowCount = 4;
            this.topLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.topLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.topLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.topLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.topLayoutPanel.Size = new System.Drawing.Size(441, 100);
            this.topLayoutPanel.TabIndex = 0;
            // 
            // stationComboBox
            // 
            this.stationComboBox.FormattingEnabled = true;
            this.stationComboBox.Location = new System.Drawing.Point(103, 3);
            this.stationComboBox.Name = "stationComboBox";
            this.stationComboBox.Size = new System.Drawing.Size(157, 21);
            this.stationComboBox.TabIndex = 4;
            // 
            // stationLabel
            // 
            this.stationLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.stationLabel.Location = new System.Drawing.Point(3, 4);
            this.stationLabel.Name = "stationLabel";
            this.stationLabel.Size = new System.Drawing.Size(94, 17);
            this.stationLabel.TabIndex = 3;
            this.stationLabel.Text = "Группа пунктов";
            // 
            // orgComboBox
            // 
            this.orgComboBox.FormattingEnabled = true;
            this.orgComboBox.Location = new System.Drawing.Point(103, 28);
            this.orgComboBox.Name = "orgComboBox";
            this.orgComboBox.Size = new System.Drawing.Size(157, 21);
            this.orgComboBox.TabIndex = 0;
            this.orgComboBox.SelectedIndexChanged += new System.EventHandler(this.orgComboBox_SelectedIndexChanged);
            // 
            // orgLabel
            // 
            this.orgLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.orgLabel.Location = new System.Drawing.Point(3, 29);
            this.orgLabel.Name = "orgLabel";
            this.orgLabel.Size = new System.Drawing.Size(74, 17);
            this.orgLabel.TabIndex = 0;
            this.orgLabel.Text = "Организация";
            // 
            // periodLabel
            // 
            this.periodLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.periodLabel.AutoSize = true;
            this.periodLabel.Location = new System.Drawing.Point(3, 81);
            this.periodLabel.Name = "periodLabel";
            this.periodLabel.Size = new System.Drawing.Size(45, 13);
            this.periodLabel.TabIndex = 2;
            this.periodLabel.Text = "Период";
            // 
            // typeLabel
            // 
            this.typeLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.typeLabel.AutoSize = true;
            this.typeLabel.Location = new System.Drawing.Point(3, 56);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(26, 13);
            this.typeLabel.TabIndex = 5;
            this.typeLabel.Text = "Тип";
            // 
            // typeComboBox
            // 
            this.typeComboBox.FormattingEnabled = true;
            this.typeComboBox.Location = new System.Drawing.Point(103, 53);
            this.typeComboBox.Name = "typeComboBox";
            this.typeComboBox.Size = new System.Drawing.Size(157, 21);
            this.typeComboBox.TabIndex = 6;
            // 
            // periodLayoutPanel
            // 
            this.periodLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.periodLayoutPanel.ColumnCount = 3;
            this.periodLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.periodLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 117F));
            this.periodLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.periodLayoutPanel.Controls.Add(this.periodTypeComboBox, 0, 0);
            this.periodLayoutPanel.Controls.Add(this.periodDateS, 1, 0);
            this.periodLayoutPanel.Controls.Add(this.timeTypeComboBox, 2, 0);
            this.periodLayoutPanel.Location = new System.Drawing.Point(100, 75);
            this.periodLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.periodLayoutPanel.Name = "periodLayoutPanel";
            this.periodLayoutPanel.RowCount = 1;
            this.periodLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.periodLayoutPanel.Size = new System.Drawing.Size(341, 25);
            this.periodLayoutPanel.TabIndex = 1;
            // 
            // periodTypeComboBox
            // 
            this.periodTypeComboBox.FormattingEnabled = true;
            this.periodTypeComboBox.Location = new System.Drawing.Point(3, 3);
            this.periodTypeComboBox.Name = "periodTypeComboBox";
            this.periodTypeComboBox.Size = new System.Drawing.Size(157, 21);
            this.periodTypeComboBox.TabIndex = 0;
            this.periodTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.periodTypeComboBox_SelectedIndexChanged);
            // 
            // periodDateS
            // 
            this.periodDateS.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.periodDateS.Location = new System.Drawing.Point(167, 3);
            this.periodDateS.Name = "periodDateS";
            this.periodDateS.Size = new System.Drawing.Size(111, 20);
            this.periodDateS.TabIndex = 1;
            // 
            // timeTypeComboBox
            // 
            this.timeTypeComboBox.FormattingEnabled = true;
            this.timeTypeComboBox.Location = new System.Drawing.Point(284, 3);
            this.timeTypeComboBox.Name = "timeTypeComboBox";
            this.timeTypeComboBox.Size = new System.Drawing.Size(54, 21);
            this.timeTypeComboBox.TabIndex = 2;
            // 
            // bottomLayoutPanel
            // 
            this.bottomLayoutPanel.ColumnCount = 3;
            this.bottomLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.bottomLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.bottomLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.bottomLayoutPanel.Controls.Add(this.authorPos2ComboBox, 0, 1);
            this.bottomLayoutPanel.Controls.Add(this.authorPos1ComboBox, 0, 0);
            this.bottomLayoutPanel.Controls.Add(this.author1ComboBox, 1, 0);
            this.bottomLayoutPanel.Controls.Add(this.author2ComboBox, 1, 1);
            this.bottomLayoutPanel.Controls.Add(this.author2checkBox, 2, 1);
            this.bottomLayoutPanel.Controls.Add(this.author1checkBox, 2, 0);
            this.bottomLayoutPanel.Controls.Add(this.doneButton, 0, 2);
            this.bottomLayoutPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomLayoutPanel.Location = new System.Drawing.Point(0, 285);
            this.bottomLayoutPanel.Name = "bottomLayoutPanel";
            this.bottomLayoutPanel.RowCount = 3;
            this.bottomLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.bottomLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.bottomLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.bottomLayoutPanel.Size = new System.Drawing.Size(441, 78);
            this.bottomLayoutPanel.TabIndex = 1;
            // 
            // authorPos2ComboBox
            // 
            this.authorPos2ComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.authorPos2ComboBox.FormattingEnabled = true;
            this.authorPos2ComboBox.Location = new System.Drawing.Point(3, 28);
            this.authorPos2ComboBox.Name = "authorPos2ComboBox";
            this.authorPos2ComboBox.Size = new System.Drawing.Size(204, 21);
            this.authorPos2ComboBox.TabIndex = 8;
            // 
            // authorPos1ComboBox
            // 
            this.authorPos1ComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.authorPos1ComboBox.FormattingEnabled = true;
            this.authorPos1ComboBox.Location = new System.Drawing.Point(3, 3);
            this.authorPos1ComboBox.Name = "authorPos1ComboBox";
            this.authorPos1ComboBox.Size = new System.Drawing.Size(204, 21);
            this.authorPos1ComboBox.TabIndex = 7;
            // 
            // author1ComboBox
            // 
            this.author1ComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.author1ComboBox.FormattingEnabled = true;
            this.author1ComboBox.Location = new System.Drawing.Point(213, 3);
            this.author1ComboBox.Name = "author1ComboBox";
            this.author1ComboBox.Size = new System.Drawing.Size(204, 21);
            this.author1ComboBox.TabIndex = 0;
            // 
            // author2ComboBox
            // 
            this.author2ComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.author2ComboBox.FormattingEnabled = true;
            this.author2ComboBox.Location = new System.Drawing.Point(213, 28);
            this.author2ComboBox.Name = "author2ComboBox";
            this.author2ComboBox.Size = new System.Drawing.Size(204, 21);
            this.author2ComboBox.TabIndex = 1;
            // 
            // author2checkBox
            // 
            this.author2checkBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.author2checkBox.Checked = true;
            this.author2checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.author2checkBox.Location = new System.Drawing.Point(424, 28);
            this.author2checkBox.Name = "author2checkBox";
            this.author2checkBox.Size = new System.Drawing.Size(14, 14);
            this.author2checkBox.TabIndex = 3;
            this.author2checkBox.UseVisualStyleBackColor = true;
            this.author2checkBox.CheckedChanged += new System.EventHandler(this.author2checkBox_CheckedChanged);
            // 
            // author1checkBox
            // 
            this.author1checkBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.author1checkBox.Checked = true;
            this.author1checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.author1checkBox.Location = new System.Drawing.Point(424, 3);
            this.author1checkBox.Name = "author1checkBox";
            this.author1checkBox.Size = new System.Drawing.Size(14, 14);
            this.author1checkBox.TabIndex = 2;
            this.author1checkBox.UseVisualStyleBackColor = true;
            this.author1checkBox.CheckedChanged += new System.EventHandler(this.author1checkBox_CheckedChanged);
            // 
            // doneButton
            // 
            this.doneButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.doneButton.Location = new System.Drawing.Point(3, 53);
            this.doneButton.Name = "doneButton";
            this.doneButton.Size = new System.Drawing.Size(75, 23);
            this.doneButton.TabIndex = 6;
            this.doneButton.Text = "Готово";
            this.doneButton.UseVisualStyleBackColor = true;
            this.doneButton.Click += new System.EventHandler(this.doneButton_Click);
            // 
            // danger1CheckBox
            // 
            this.danger1CheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.danger1CheckBox.AutoSize = true;
            this.danger1CheckBox.Checked = true;
            this.danger1CheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.danger1CheckBox.Location = new System.Drawing.Point(424, 3);
            this.danger1CheckBox.Name = "danger1CheckBox";
            this.danger1CheckBox.Size = new System.Drawing.Size(14, 14);
            this.danger1CheckBox.TabIndex = 2;
            this.danger1CheckBox.UseVisualStyleBackColor = true;
            this.danger1CheckBox.CheckedChanged += new System.EventHandler(this.danger1CheckBox_CheckedChanged);
            // 
            // middleLayoutPanel
            // 
            this.middleLayoutPanel.AutoSize = true;
            this.middleLayoutPanel.ColumnCount = 1;
            this.middleLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.middleLayoutPanel.Controls.Add(this.viewLabel, 0, 0);
            this.middleLayoutPanel.Controls.Add(this.danger2LayoutPanel, 0, 4);
            this.middleLayoutPanel.Controls.Add(this.danger1LayoutPanel, 0, 2);
            this.middleLayoutPanel.Controls.Add(this.danger2TextBox, 0, 5);
            this.middleLayoutPanel.Controls.Add(this.danger1TextBox, 0, 3);
            this.middleLayoutPanel.Controls.Add(this.viewTextBox, 0, 1);
            this.middleLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.middleLayoutPanel.Location = new System.Drawing.Point(0, 100);
            this.middleLayoutPanel.Name = "middleLayoutPanel";
            this.middleLayoutPanel.RowCount = 6;
            this.middleLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.middleLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.middleLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.middleLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.middleLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.middleLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.middleLayoutPanel.Size = new System.Drawing.Size(441, 185);
            this.middleLayoutPanel.TabIndex = 2;
            // 
            // viewLabel
            // 
            this.viewLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.viewLabel.AutoSize = true;
            this.viewLabel.Location = new System.Drawing.Point(201, 3);
            this.viewLabel.Name = "viewLabel";
            this.viewLabel.Size = new System.Drawing.Size(39, 13);
            this.viewLabel.TabIndex = 0;
            this.viewLabel.Text = "Обзор";
            // 
            // danger2LayoutPanel
            // 
            this.danger2LayoutPanel.ColumnCount = 2;
            this.danger2LayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.danger2LayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.danger2LayoutPanel.Controls.Add(this.danger2CheckBox, 1, 0);
            this.danger2LayoutPanel.Controls.Add(this.danger2Label, 0, 0);
            this.danger2LayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.danger2LayoutPanel.Location = new System.Drawing.Point(0, 122);
            this.danger2LayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.danger2LayoutPanel.Name = "danger2LayoutPanel";
            this.danger2LayoutPanel.RowCount = 1;
            this.danger2LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.danger2LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.danger2LayoutPanel.Size = new System.Drawing.Size(441, 20);
            this.danger2LayoutPanel.TabIndex = 7;
            // 
            // danger2CheckBox
            // 
            this.danger2CheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.danger2CheckBox.AutoSize = true;
            this.danger2CheckBox.Checked = true;
            this.danger2CheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.danger2CheckBox.Location = new System.Drawing.Point(424, 3);
            this.danger2CheckBox.Name = "danger2CheckBox";
            this.danger2CheckBox.Size = new System.Drawing.Size(14, 14);
            this.danger2CheckBox.TabIndex = 3;
            this.danger2CheckBox.UseVisualStyleBackColor = true;
            this.danger2CheckBox.CheckedChanged += new System.EventHandler(this.danger2CheckBox_CheckedChanged);
            // 
            // danger2Label
            // 
            this.danger2Label.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.danger2Label.AutoSize = true;
            this.danger2Label.Location = new System.Drawing.Point(90, 3);
            this.danger2Label.Name = "danger2Label";
            this.danger2Label.Size = new System.Drawing.Size(241, 13);
            this.danger2Label.TabIndex = 2;
            this.danger2Label.Text = "Предупреждение о неблагоприятном явлении";
            // 
            // danger1LayoutPanel
            // 
            this.danger1LayoutPanel.ColumnCount = 2;
            this.danger1LayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.danger1LayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.danger1LayoutPanel.Controls.Add(this.danger1Label, 0, 0);
            this.danger1LayoutPanel.Controls.Add(this.danger1CheckBox, 1, 0);
            this.danger1LayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.danger1LayoutPanel.Location = new System.Drawing.Point(0, 61);
            this.danger1LayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.danger1LayoutPanel.Name = "danger1LayoutPanel";
            this.danger1LayoutPanel.RowCount = 1;
            this.danger1LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.danger1LayoutPanel.Size = new System.Drawing.Size(441, 20);
            this.danger1LayoutPanel.TabIndex = 6;
            // 
            // danger1Label
            // 
            this.danger1Label.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.danger1Label.AutoSize = true;
            this.danger1Label.Location = new System.Drawing.Point(110, 3);
            this.danger1Label.Name = "danger1Label";
            this.danger1Label.Size = new System.Drawing.Size(201, 13);
            this.danger1Label.TabIndex = 1;
            this.danger1Label.Text = "Предупреждение об опасном явлении";
            // 
            // danger2TextBox
            // 
            this.danger2TextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.danger2TextBox.Location = new System.Drawing.Point(3, 145);
            this.danger2TextBox.Name = "danger2TextBox";
            this.danger2TextBox.Size = new System.Drawing.Size(435, 37);
            this.danger2TextBox.TabIndex = 5;
            this.danger2TextBox.Text = "";
            // 
            // danger1TextBox
            // 
            this.danger1TextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.danger1TextBox.Location = new System.Drawing.Point(3, 84);
            this.danger1TextBox.Name = "danger1TextBox";
            this.danger1TextBox.Size = new System.Drawing.Size(435, 35);
            this.danger1TextBox.TabIndex = 4;
            this.danger1TextBox.Text = "";
            // 
            // viewTextBox
            // 
            this.viewTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewTextBox.Location = new System.Drawing.Point(3, 23);
            this.viewTextBox.Name = "viewTextBox";
            this.viewTextBox.Size = new System.Drawing.Size(435, 35);
            this.viewTextBox.TabIndex = 3;
            this.viewTextBox.Text = "";
            // 
            // FormWaterObjectReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 363);
            this.Controls.Add(this.middleLayoutPanel);
            this.Controls.Add(this.bottomLayoutPanel);
            this.Controls.Add(this.topLayoutPanel);
            this.Name = "FormWaterObjectReport";
            this.Text = "Обзор водных объектов";
            this.topLayoutPanel.ResumeLayout(false);
            this.topLayoutPanel.PerformLayout();
            this.periodLayoutPanel.ResumeLayout(false);
            this.bottomLayoutPanel.ResumeLayout(false);
            this.middleLayoutPanel.ResumeLayout(false);
            this.middleLayoutPanel.PerformLayout();
            this.danger2LayoutPanel.ResumeLayout(false);
            this.danger2LayoutPanel.PerformLayout();
            this.danger1LayoutPanel.ResumeLayout(false);
            this.danger1LayoutPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel topLayoutPanel;
        private System.Windows.Forms.ComboBox orgComboBox;
        private System.Windows.Forms.Label orgLabel;
        private System.Windows.Forms.TableLayoutPanel periodLayoutPanel;
        private System.Windows.Forms.ComboBox periodTypeComboBox;
        private System.Windows.Forms.DateTimePicker periodDateS;
        private System.Windows.Forms.Label periodLabel;
        private System.Windows.Forms.TableLayoutPanel bottomLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel middleLayoutPanel;
        private System.Windows.Forms.Label viewLabel;
        private System.Windows.Forms.Label danger1Label;
        private System.Windows.Forms.Label danger2Label;
        private System.Windows.Forms.RichTextBox danger2TextBox;
        private System.Windows.Forms.RichTextBox danger1TextBox;
        private System.Windows.Forms.RichTextBox viewTextBox;
        private System.Windows.Forms.TableLayoutPanel danger2LayoutPanel;
        private System.Windows.Forms.CheckBox danger2CheckBox;
        private System.Windows.Forms.TableLayoutPanel danger1LayoutPanel;
        private System.Windows.Forms.CheckBox danger1CheckBox;
        private System.Windows.Forms.ComboBox author1ComboBox;
        private System.Windows.Forms.ComboBox author2ComboBox;
        private System.Windows.Forms.CheckBox author1checkBox;
        private System.Windows.Forms.CheckBox author2checkBox;
        private System.Windows.Forms.Button doneButton;
        private System.Windows.Forms.ComboBox stationComboBox;
        private System.Windows.Forms.Label stationLabel;
        private System.Windows.Forms.ComboBox authorPos1ComboBox;
        private System.Windows.Forms.ComboBox authorPos2ComboBox;
        private System.Windows.Forms.ComboBox timeTypeComboBox;
        private System.Windows.Forms.Label typeLabel;
        private System.Windows.Forms.ComboBox typeComboBox;
    }
}