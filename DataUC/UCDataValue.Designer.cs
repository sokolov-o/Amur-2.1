namespace SOV.Amur.Data
{
    partial class UCDataValue
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
            this.saveButton = new System.Windows.Forms.Button();
            this.dateLocLabel = new System.Windows.Forms.Label();
            this.dateLocText = new System.Windows.Forms.TextBox();
            this.dateUtcLabel = new System.Windows.Forms.Label();
            this.dateUtcText = new System.Windows.Forms.TextBox();
            this.valueLabel = new System.Windows.Forms.Label();
            this.flagLabel = new System.Windows.Forms.Label();
            this.varText = new System.Windows.Forms.TextBox();
            this.flagComboBox = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.valText = new System.Windows.Forms.TextBox();
            this.varLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.approve = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // saveButton
            // 
            this.saveButton.AutoSize = true;
            this.saveButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.saveButton.Enabled = false;
            this.saveButton.Location = new System.Drawing.Point(3, 3);
            this.saveButton.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(70, 23);
            this.saveButton.TabIndex = 0;
            this.saveButton.Text = "Сохранить";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.save_Click);
            // 
            // dateLocLabel
            // 
            this.dateLocLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dateLocLabel.AutoSize = true;
            this.dateLocLabel.Location = new System.Drawing.Point(3, 6);
            this.dateLocLabel.Name = "dateLocLabel";
            this.dateLocLabel.Size = new System.Drawing.Size(59, 13);
            this.dateLocLabel.TabIndex = 1;
            this.dateLocLabel.Text = "Дата ЛОК";
            // 
            // dateLocText
            // 
            this.dateLocText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dateLocText.Location = new System.Drawing.Point(88, 3);
            this.dateLocText.Name = "dateLocText";
            this.dateLocText.ReadOnly = true;
            this.dateLocText.Size = new System.Drawing.Size(207, 20);
            this.dateLocText.TabIndex = 2;
            // 
            // dateUtcLabel
            // 
            this.dateUtcLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dateUtcLabel.AutoSize = true;
            this.dateUtcLabel.Location = new System.Drawing.Point(3, 32);
            this.dateUtcLabel.Name = "dateUtcLabel";
            this.dateUtcLabel.Size = new System.Drawing.Size(57, 13);
            this.dateUtcLabel.TabIndex = 3;
            this.dateUtcLabel.Text = "Дата ВСВ";
            // 
            // dateUtcText
            // 
            this.dateUtcText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dateUtcText.Location = new System.Drawing.Point(88, 29);
            this.dateUtcText.Name = "dateUtcText";
            this.dateUtcText.ReadOnly = true;
            this.dateUtcText.Size = new System.Drawing.Size(207, 20);
            this.dateUtcText.TabIndex = 4;
            // 
            // valueLabel
            // 
            this.valueLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.valueLabel.AutoSize = true;
            this.valueLabel.Location = new System.Drawing.Point(3, 112);
            this.valueLabel.Name = "valueLabel";
            this.valueLabel.Size = new System.Drawing.Size(55, 13);
            this.valueLabel.TabIndex = 5;
            this.valueLabel.Text = "Значение";
            // 
            // flagLabel
            // 
            this.flagLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.flagLabel.AutoSize = true;
            this.flagLabel.Location = new System.Drawing.Point(3, 85);
            this.flagLabel.Name = "flagLabel";
            this.flagLabel.Size = new System.Drawing.Size(60, 13);
            this.flagLabel.TabIndex = 6;
            this.flagLabel.Text = "Флаг AQС";
            // 
            // varText
            // 
            this.varText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.varText.Location = new System.Drawing.Point(88, 55);
            this.varText.Name = "varText";
            this.varText.ReadOnly = true;
            this.varText.Size = new System.Drawing.Size(207, 20);
            this.varText.TabIndex = 7;
            // 
            // flagComboBox
            // 
            this.flagComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flagComboBox.Enabled = false;
            this.flagComboBox.FormattingEnabled = true;
            this.flagComboBox.Location = new System.Drawing.Point(88, 81);
            this.flagComboBox.Name = "flagComboBox";
            this.flagComboBox.Size = new System.Drawing.Size(207, 21);
            this.flagComboBox.TabIndex = 8;
            this.flagComboBox.SelectedIndexChanged += new System.EventHandler(this.onFieldChange);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.valText, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.dateLocLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flagComboBox, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.dateUtcLabel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.varText, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.dateUtcText, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.flagLabel, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.dateLocText, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.valueLabel, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.varLabel, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(298, 133);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // valText
            // 
            this.valText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.valText.Location = new System.Drawing.Point(88, 108);
            this.valText.Name = "valText";
            this.valText.Size = new System.Drawing.Size(207, 20);
            this.valText.TabIndex = 10;
            this.valText.TextChanged += new System.EventHandler(this.onFieldChange);
            // 
            // varLabel
            // 
            this.varLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.varLabel.AutoSize = true;
            this.varLabel.Location = new System.Drawing.Point(3, 58);
            this.varLabel.Name = "varLabel";
            this.varLabel.Size = new System.Drawing.Size(71, 13);
            this.varLabel.TabIndex = 9;
            this.varLabel.Text = "Переменная";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.approve, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.saveButton, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 132);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(298, 29);
            this.tableLayoutPanel2.TabIndex = 10;
            // 
            // approve
            // 
            this.approve.AutoSize = true;
            this.approve.Dock = System.Windows.Forms.DockStyle.Right;
            this.approve.Location = new System.Drawing.Point(212, 3);
            this.approve.Name = "approve";
            this.approve.Size = new System.Drawing.Size(83, 23);
            this.approve.TabIndex = 1;
            this.approve.Text = "Подтвердить";
            this.approve.UseVisualStyleBackColor = true;
            this.approve.Click += new System.EventHandler(this.approve_Click);
            // 
            // UCDataValue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UCDataValue";
            this.Size = new System.Drawing.Size(298, 161);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Label dateLocLabel;
        private System.Windows.Forms.TextBox dateLocText;
        private System.Windows.Forms.Label dateUtcLabel;
        private System.Windows.Forms.TextBox dateUtcText;
        private System.Windows.Forms.Label valueLabel;
        private System.Windows.Forms.Label flagLabel;
        private System.Windows.Forms.TextBox varText;
        private System.Windows.Forms.ComboBox flagComboBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox valText;
        private System.Windows.Forms.Label varLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button approve;
    }
}
