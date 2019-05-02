namespace SOV.Amur.Meta
{
    partial class UCVariable
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
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.timeSupportTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.ucValueType = new SOV.Common.UCDicComboBox();
            this.ucSampleMedium = new SOV.Common.UCDicComboBox();
            this.ucGeneralCategory = new SOV.Common.UCDicComboBox();
            this.ucDataType = new SOV.Common.UCDicComboBox();
            this.ucTime = new SOV.Common.UCDicComboBox();
            this.ucUnit = new SOV.Common.UCDicComboBox();
            this.ucVariableType = new SOV.Common.UCDicComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.nameEngTextBox = new System.Windows.Forms.TextBox();
            this.codeNoDataTextBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.codeErrDataTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID:";
            // 
            // idTextBox
            // 
            this.idTextBox.Location = new System.Drawing.Point(137, 3);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.ReadOnly = true;
            this.idTextBox.Size = new System.Drawing.Size(200, 20);
            this.idTextBox.TabIndex = 1;
            this.idTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Переменная:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 187);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Ед. измерения:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Время измерения:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 162);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Тип данных:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 217);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Категория:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 244);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Проба/среда:";
            // 
            // timeSupportTextBox
            // 
            this.timeSupportTextBox.Location = new System.Drawing.Point(276, 109);
            this.timeSupportTextBox.Name = "timeSupportTextBox";
            this.timeSupportTextBox.Size = new System.Drawing.Size(61, 20);
            this.timeSupportTextBox.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(243, 111);
            this.label8.Name = "label8";
            this.label8.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label8.Size = new System.Drawing.Size(27, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Шаг";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(86, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Название (рус):";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nameTextBox.Location = new System.Drawing.Point(137, 29);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(200, 20);
            this.nameTextBox.TabIndex = 2;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 135);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(79, 13);
            this.label11.TabIndex = 27;
            this.label11.Text = "Тип значения:";
            // 
            // ucValueType
            // 
            this.ucValueType.CheckBoxVisible = false;
            this.ucValueType.Checked = true;
            this.ucValueType.Location = new System.Drawing.Point(137, 135);
            this.ucValueType.Name = "ucValueType";
            this.ucValueType.SelectedId = null;
            this.ucValueType.SelectedIndex = -1;
            this.ucValueType.Size = new System.Drawing.Size(200, 21);
            this.ucValueType.TabIndex = 6;
            // 
            // ucSampleMedium
            // 
            this.ucSampleMedium.CheckBoxVisible = false;
            this.ucSampleMedium.Checked = true;
            this.ucSampleMedium.Location = new System.Drawing.Point(137, 241);
            this.ucSampleMedium.Name = "ucSampleMedium";
            this.ucSampleMedium.SelectedId = null;
            this.ucSampleMedium.SelectedIndex = -1;
            this.ucSampleMedium.Size = new System.Drawing.Size(200, 21);
            this.ucSampleMedium.TabIndex = 10;
            // 
            // ucGeneralCategory
            // 
            this.ucGeneralCategory.CheckBoxVisible = false;
            this.ucGeneralCategory.Checked = true;
            this.ucGeneralCategory.Location = new System.Drawing.Point(137, 214);
            this.ucGeneralCategory.Name = "ucGeneralCategory";
            this.ucGeneralCategory.SelectedId = null;
            this.ucGeneralCategory.SelectedIndex = -1;
            this.ucGeneralCategory.Size = new System.Drawing.Size(200, 21);
            this.ucGeneralCategory.TabIndex = 9;
            // 
            // ucDataType
            // 
            this.ucDataType.CheckBoxVisible = false;
            this.ucDataType.Checked = true;
            this.ucDataType.Location = new System.Drawing.Point(137, 162);
            this.ucDataType.Name = "ucDataType";
            this.ucDataType.SelectedId = null;
            this.ucDataType.SelectedIndex = -1;
            this.ucDataType.Size = new System.Drawing.Size(200, 21);
            this.ucDataType.TabIndex = 7;
            // 
            // ucTime
            // 
            this.ucTime.CheckBoxVisible = false;
            this.ucTime.Checked = true;
            this.ucTime.Location = new System.Drawing.Point(137, 108);
            this.ucTime.Name = "ucTime";
            this.ucTime.SelectedId = null;
            this.ucTime.SelectedIndex = -1;
            this.ucTime.Size = new System.Drawing.Size(100, 21);
            this.ucTime.TabIndex = 4;
            // 
            // ucUnit
            // 
            this.ucUnit.CheckBoxVisible = false;
            this.ucUnit.Checked = true;
            this.ucUnit.Location = new System.Drawing.Point(137, 187);
            this.ucUnit.Name = "ucUnit";
            this.ucUnit.SelectedId = null;
            this.ucUnit.SelectedIndex = -1;
            this.ucUnit.Size = new System.Drawing.Size(200, 21);
            this.ucUnit.TabIndex = 8;
            // 
            // ucVariableType
            // 
            this.ucVariableType.CheckBoxVisible = false;
            this.ucVariableType.Checked = true;
            this.ucVariableType.Location = new System.Drawing.Point(137, 80);
            this.ucVariableType.Name = "ucVariableType";
            this.ucVariableType.SelectedId = null;
            this.ucVariableType.SelectedIndex = -1;
            this.ucVariableType.Size = new System.Drawing.Size(200, 21);
            this.ucVariableType.TabIndex = 3;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 58);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(86, 13);
            this.label10.TabIndex = 29;
            this.label10.Text = "Название (анг):";
            // 
            // nameEngTextBox
            // 
            this.nameEngTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nameEngTextBox.Location = new System.Drawing.Point(137, 55);
            this.nameEngTextBox.Name = "nameEngTextBox";
            this.nameEngTextBox.Size = new System.Drawing.Size(200, 20);
            this.nameEngTextBox.TabIndex = 28;
            // 
            // codeNoDataTextBox
            // 
            this.codeNoDataTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.codeNoDataTextBox.Location = new System.Drawing.Point(191, 268);
            this.codeNoDataTextBox.Name = "codeNoDataTextBox";
            this.codeNoDataTextBox.Size = new System.Drawing.Size(70, 20);
            this.codeNoDataTextBox.TabIndex = 30;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 271);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(186, 13);
            this.label12.TabIndex = 31;
            this.label12.Text = "Коды отсутствия и ошибки данных:";
            // 
            // codeErrDataTextBox
            // 
            this.codeErrDataTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.codeErrDataTextBox.Location = new System.Drawing.Point(267, 268);
            this.codeErrDataTextBox.Name = "codeErrDataTextBox";
            this.codeErrDataTextBox.Size = new System.Drawing.Size(70, 20);
            this.codeErrDataTextBox.TabIndex = 32;
            // 
            // UCVariable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.codeErrDataTextBox);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.codeNoDataTextBox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.nameEngTextBox);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.ucValueType);
            this.Controls.Add(this.ucSampleMedium);
            this.Controls.Add(this.ucGeneralCategory);
            this.Controls.Add(this.ucDataType);
            this.Controls.Add(this.ucTime);
            this.Controls.Add(this.ucUnit);
            this.Controls.Add(this.ucVariableType);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.timeSupportTextBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.idTextBox);
            this.Controls.Add(this.label1);
            this.Name = "UCVariable";
            this.Size = new System.Drawing.Size(340, 293);
            this.Load += new System.EventHandler(this.UCVariable_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox timeSupportTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox nameTextBox;
        private Common.UCDicComboBox ucVariableType;
        private Common.UCDicComboBox ucUnit;
        private Common.UCDicComboBox ucTime;
        private Common.UCDicComboBox ucDataType;
        private Common.UCDicComboBox ucGeneralCategory;
        private Common.UCDicComboBox ucSampleMedium;
        private Common.UCDicComboBox ucValueType;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox nameEngTextBox;
        private System.Windows.Forms.TextBox codeNoDataTextBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox codeErrDataTextBox;

    }
}
