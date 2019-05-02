namespace SOV.Amur.Data
{
    partial class FormDataFilter
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
            SOV.Common.DateTimePeriod dateTimePeriod1 = new SOV.Common.DateTimePeriod();
            SOV.Amur.Meta.CatalogFilter catalogFilter1 = new SOV.Amur.Meta.CatalogFilter();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDataFilter));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ucDateTimePeriod = new SOV.Common.UCDateTimePeriod();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ucCatalogFilter = new SOV.Amur.Meta.UCCatalogFilter();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.selectDeletedDataValuesCheckBox = new System.Windows.Forms.CheckBox();
            this.isActualValueOnlyCheckBox = new System.Windows.Forms.CheckBox();
            this.refSiteCheckBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.flagAQCTextBox = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.saveFilterButton = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dateLOCRadioButton = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.button1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.button2, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.saveFilterButton, 2, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(529, 500);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(3, 474);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Применить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox1, 3);
            this.groupBox1.Controls.Add(this.ucDateTimePeriod);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(523, 44);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Временной период";
            // 
            // ucDateTimePeriod
            // 
            this.ucDateTimePeriod.CustomDateFormat = "dd.MM.yyyy HH:mm";
            dateTimePeriod1.DaysBeforeDateNow = 7;
            dateTimePeriod1.PeriodType = SOV.Common.DateTimePeriod.Type.Period;
            this.ucDateTimePeriod.DateTimePeriod = dateTimePeriod1;
            this.ucDateTimePeriod.Location = new System.Drawing.Point(3, 16);
            this.ucDateTimePeriod.Margin = new System.Windows.Forms.Padding(0);
            this.ucDateTimePeriod.Name = "ucDateTimePeriod";
            this.ucDateTimePeriod.Size = new System.Drawing.Size(476, 25);
            this.ucDateTimePeriod.TabIndex = 1;
            this.ucDateTimePeriod.VisibleCountTextBox = false;
            this.ucDateTimePeriod.VisibleDateCheckBoxs = true;
            this.ucDateTimePeriod.VisibleDateSF = true;
            this.ucDateTimePeriod.VisibleViewTypeCheckBox = true;
            // 
            // groupBox2
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox2, 3);
            this.groupBox2.Controls.Add(this.ucCatalogFilter);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 53);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(523, 329);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Фильтр каталога данных";
            // 
            // ucCatalogFilter
            // 
            catalogFilter1.Methods = null;
            catalogFilter1.OffsetTypes = null;
            catalogFilter1.OffsetValue = null;
            catalogFilter1.Sites = null;
            catalogFilter1.Sources = null;
            catalogFilter1.Variables = null;
            this.ucCatalogFilter.CatalogFilter = catalogFilter1;
            this.ucCatalogFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucCatalogFilter.Location = new System.Drawing.Point(3, 16);
            this.ucCatalogFilter.Name = "ucCatalogFilter";
            this.ucCatalogFilter.ShowToolStrip = true;
            this.ucCatalogFilter.Size = new System.Drawing.Size(517, 310);
            this.ucCatalogFilter.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox3, 3);
            this.groupBox3.Controls.Add(this.tableLayoutPanel2);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 388);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(523, 80);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Разное";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 285F));
            this.tableLayoutPanel2.Controls.Add(this.selectDeletedDataValuesCheckBox, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.isActualValueOnlyCheckBox, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.refSiteCheckBox, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.flagAQCTextBox, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.groupBox4, 3, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(517, 61);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // selectDeletedDataValuesCheckBox
            // 
            this.selectDeletedDataValuesCheckBox.AutoSize = true;
            this.selectDeletedDataValuesCheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectDeletedDataValuesCheckBox.Location = new System.Drawing.Point(236, 3);
            this.selectDeletedDataValuesCheckBox.Name = "selectDeletedDataValuesCheckBox";
            this.selectDeletedDataValuesCheckBox.Size = new System.Drawing.Size(279, 20);
            this.selectDeletedDataValuesCheckBox.TabIndex = 10;
            this.selectDeletedDataValuesCheckBox.Text = "показывать удалённые";
            this.selectDeletedDataValuesCheckBox.UseVisualStyleBackColor = true;
            // 
            // isActualValueOnlyCheckBox
            // 
            this.isActualValueOnlyCheckBox.AutoSize = true;
            this.isActualValueOnlyCheckBox.Checked = true;
            this.isActualValueOnlyCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.isActualValueOnlyCheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.isActualValueOnlyCheckBox.Location = new System.Drawing.Point(3, 3);
            this.isActualValueOnlyCheckBox.Name = "isActualValueOnlyCheckBox";
            this.isActualValueOnlyCheckBox.Size = new System.Drawing.Size(102, 20);
            this.isActualValueOnlyCheckBox.TabIndex = 7;
            this.isActualValueOnlyCheckBox.Text = "Одно значение";
            this.isActualValueOnlyCheckBox.UseVisualStyleBackColor = true;
            // 
            // refSiteCheckBox
            // 
            this.refSiteCheckBox.AutoSize = true;
            this.refSiteCheckBox.Checked = true;
            this.refSiteCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tableLayoutPanel2.SetColumnSpan(this.refSiteCheckBox, 3);
            this.refSiteCheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.refSiteCheckBox.Location = new System.Drawing.Point(3, 29);
            this.refSiteCheckBox.Name = "refSiteCheckBox";
            this.refSiteCheckBox.Size = new System.Drawing.Size(227, 29);
            this.refSiteCheckBox.TabIndex = 9;
            this.refSiteCheckBox.Text = "отображать данные ссылочного пункта";
            this.refSiteCheckBox.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(111, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 26);
            this.label2.TabIndex = 6;
            this.label2.Text = "Макс флаг АКК:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // flagAQCTextBox
            // 
            this.flagAQCTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flagAQCTextBox.Location = new System.Drawing.Point(206, 3);
            this.flagAQCTextBox.Name = "flagAQCTextBox";
            this.flagAQCTextBox.Size = new System.Drawing.Size(24, 20);
            this.flagAQCTextBox.TabIndex = 8;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(84, 474);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Отменить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // saveFilterButton
            // 
            this.saveFilterButton.Location = new System.Drawing.Point(165, 474);
            this.saveFilterButton.Name = "saveFilterButton";
            this.saveFilterButton.Size = new System.Drawing.Size(118, 23);
            this.saveFilterButton.TabIndex = 12;
            this.saveFilterButton.Text = "Запомнить фильтр";
            this.saveFilterButton.UseVisualStyleBackColor = true;
            this.saveFilterButton.Click += new System.EventHandler(this.saveFilterButton_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radioButton2);
            this.groupBox4.Controls.Add(this.dateLOCRadioButton);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(236, 29);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox4.Size = new System.Drawing.Size(279, 29);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            // 
            // dateLOCRadioButton
            // 
            this.dateLOCRadioButton.AutoSize = true;
            this.dateLOCRadioButton.Checked = true;
            this.dateLOCRadioButton.Location = new System.Drawing.Point(3, 9);
            this.dateLOCRadioButton.Name = "dateLOCRadioButton";
            this.dateLOCRadioButton.Size = new System.Drawing.Size(195, 17);
            this.dateLOCRadioButton.TabIndex = 0;
            this.dateLOCRadioButton.TabStop = true;
            this.dateLOCRadioButton.Text = "выборка по локальному времени";
            this.dateLOCRadioButton.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(204, 9);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(61, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "по ВСВ";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // FormDataFilter
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(529, 500);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormDataFilter";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Фильтр запроса данных";
            this.Load += new System.EventHandler(this.FormDataFilter_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Common.UCDateTimePeriod ucDateTimePeriod;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox isActualValueOnlyCheckBox;
        private System.Windows.Forms.TextBox flagAQCTextBox;
        private System.Windows.Forms.CheckBox refSiteCheckBox;
        private SOV.Amur.Meta.UCCatalogFilter ucCatalogFilter;
        private System.Windows.Forms.CheckBox selectDeletedDataValuesCheckBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button saveFilterButton;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton dateLOCRadioButton;
    }
}