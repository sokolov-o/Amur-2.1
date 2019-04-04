namespace SOV.Amur.Meta
{
    partial class UCVariableFilter
    {
        /// <summary> 
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Обязательный метод для поддержки конструктора - не изменяйте 
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCVariableFilter));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.allVarTypesCheckBox = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.allValueTypesCheckBox = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.allTimeCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timeSupportTextBox = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.allUnitsCheckBox = new System.Windows.Forms.CheckBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.allGeneralCategoriesCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.allDataTypesCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.allSamplesCheckBox = new System.Windows.Forms.CheckBox();
            this.varTypeListBox = new SOV.Common.UCList();
            this.valueTypeListBox = new SOV.Common.UCList();
            this.timeListBox = new SOV.Common.UCList();
            this.unitListBox = new SOV.Common.UCList();
            this.generalCategsListBox = new SOV.Common.UCList();
            this.dataTypesListBox = new SOV.Common.UCList();
            this.samplesListBox = new SOV.Common.UCList();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(207, 264);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Тип переменной";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.varTypeListBox, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.allVarTypesCheckBox, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(201, 245);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // allVarTypesCheckBox
            // 
            this.allVarTypesCheckBox.AutoSize = true;
            this.allVarTypesCheckBox.Checked = true;
            this.allVarTypesCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.allVarTypesCheckBox.Location = new System.Drawing.Point(3, 3);
            this.allVarTypesCheckBox.Name = "allVarTypesCheckBox";
            this.allVarTypesCheckBox.Size = new System.Drawing.Size(73, 17);
            this.allVarTypesCheckBox.TabIndex = 1;
            this.allVarTypesCheckBox.Text = "Все типы";
            this.allVarTypesCheckBox.UseVisualStyleBackColor = true;
            this.allVarTypesCheckBox.CheckedChanged += new System.EventHandler(this.allTypesCheckBox_CheckedChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(456, 302);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(432, 276);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Типы переменных и значений";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.groupBox2, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(426, 270);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(216, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(207, 264);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Тип значения";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.valueTypeListBox, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.allValueTypesCheckBox, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(201, 245);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // allValueTypesCheckBox
            // 
            this.allValueTypesCheckBox.AutoSize = true;
            this.allValueTypesCheckBox.Checked = true;
            this.allValueTypesCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.allValueTypesCheckBox.Location = new System.Drawing.Point(3, 3);
            this.allValueTypesCheckBox.Name = "allValueTypesCheckBox";
            this.allValueTypesCheckBox.Size = new System.Drawing.Size(73, 17);
            this.allValueTypesCheckBox.TabIndex = 1;
            this.allValueTypesCheckBox.Text = "Все типы";
            this.allValueTypesCheckBox.UseVisualStyleBackColor = true;
            this.allValueTypesCheckBox.CheckedChanged += new System.EventHandler(this.allValueTypesCheckBox_CheckedChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel4);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(432, 276);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Ед. и время";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.groupBox3, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.groupBox4, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(426, 270);
            this.tableLayoutPanel4.TabIndex = 4;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tableLayoutPanel5);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(207, 264);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Время";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.timeListBox, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.allTimeCheckBox, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.label1, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.timeSupportTextBox, 0, 3);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 4;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.Size = new System.Drawing.Size(201, 245);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // allTimeCheckBox
            // 
            this.allTimeCheckBox.AutoSize = true;
            this.allTimeCheckBox.Checked = true;
            this.allTimeCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.allTimeCheckBox.Location = new System.Drawing.Point(3, 3);
            this.allTimeCheckBox.Name = "allTimeCheckBox";
            this.allTimeCheckBox.Size = new System.Drawing.Size(73, 17);
            this.allTimeCheckBox.TabIndex = 1;
            this.allTimeCheckBox.Text = "Все типы";
            this.allTimeCheckBox.UseVisualStyleBackColor = true;
            this.allTimeCheckBox.CheckedChanged += new System.EventHandler(this.allTimeCheckBox_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(3, 206);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "time_span (через ;)";
            // 
            // timeSupportTextBox
            // 
            this.timeSupportTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timeSupportTextBox.Location = new System.Drawing.Point(3, 222);
            this.timeSupportTextBox.Name = "timeSupportTextBox";
            this.timeSupportTextBox.Size = new System.Drawing.Size(195, 20);
            this.timeSupportTextBox.TabIndex = 3;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tableLayoutPanel6);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(216, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(207, 264);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Ед. измерения";
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Controls.Add(this.unitListBox, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.allUnitsCheckBox, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(201, 245);
            this.tableLayoutPanel6.TabIndex = 0;
            // 
            // allUnitsCheckBox
            // 
            this.allUnitsCheckBox.AutoSize = true;
            this.allUnitsCheckBox.Checked = true;
            this.allUnitsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.allUnitsCheckBox.Location = new System.Drawing.Point(3, 3);
            this.allUnitsCheckBox.Name = "allUnitsCheckBox";
            this.allUnitsCheckBox.Size = new System.Drawing.Size(73, 17);
            this.allUnitsCheckBox.TabIndex = 1;
            this.allUnitsCheckBox.Text = "Все типы";
            this.allUnitsCheckBox.UseVisualStyleBackColor = true;
            this.allUnitsCheckBox.CheckedChanged += new System.EventHandler(this.allUnitsCheckBox_CheckedChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tableLayoutPanel7);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(448, 276);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Тип данных, категория, проба";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 3;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel7.Controls.Add(this.groupBox7, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.groupBox5, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.groupBox6, 1, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(448, 276);
            this.tableLayoutPanel7.TabIndex = 4;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.tableLayoutPanel10);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox7.Location = new System.Drawing.Point(152, 3);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(143, 270);
            this.groupBox7.TabIndex = 3;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Категория";
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 1;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel10.Controls.Add(this.generalCategsListBox, 0, 1);
            this.tableLayoutPanel10.Controls.Add(this.allGeneralCategoriesCheckBox, 0, 0);
            this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel10.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 2;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(137, 251);
            this.tableLayoutPanel10.TabIndex = 0;
            // 
            // allGeneralCategoriesCheckBox
            // 
            this.allGeneralCategoriesCheckBox.AutoSize = true;
            this.allGeneralCategoriesCheckBox.Checked = true;
            this.allGeneralCategoriesCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.allGeneralCategoriesCheckBox.Location = new System.Drawing.Point(3, 3);
            this.allGeneralCategoriesCheckBox.Name = "allGeneralCategoriesCheckBox";
            this.allGeneralCategoriesCheckBox.Size = new System.Drawing.Size(73, 17);
            this.allGeneralCategoriesCheckBox.TabIndex = 1;
            this.allGeneralCategoriesCheckBox.Text = "Все типы";
            this.allGeneralCategoriesCheckBox.UseVisualStyleBackColor = true;
            this.allGeneralCategoriesCheckBox.CheckedChanged += new System.EventHandler(this.allGeneralCategoriesCheckBox_CheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.tableLayoutPanel8);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(3, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(143, 270);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Тип данных";
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 1;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.Controls.Add(this.dataTypesListBox, 0, 1);
            this.tableLayoutPanel8.Controls.Add(this.allDataTypesCheckBox, 0, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 2;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(137, 251);
            this.tableLayoutPanel8.TabIndex = 0;
            // 
            // allDataTypesCheckBox
            // 
            this.allDataTypesCheckBox.AutoSize = true;
            this.allDataTypesCheckBox.Checked = true;
            this.allDataTypesCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.allDataTypesCheckBox.Location = new System.Drawing.Point(3, 3);
            this.allDataTypesCheckBox.Name = "allDataTypesCheckBox";
            this.allDataTypesCheckBox.Size = new System.Drawing.Size(73, 17);
            this.allDataTypesCheckBox.TabIndex = 1;
            this.allDataTypesCheckBox.Text = "Все типы";
            this.allDataTypesCheckBox.UseVisualStyleBackColor = true;
            this.allDataTypesCheckBox.CheckedChanged += new System.EventHandler(this.allDataTypesCheckBox_CheckedChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.tableLayoutPanel9);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Location = new System.Drawing.Point(301, 3);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(144, 270);
            this.groupBox6.TabIndex = 2;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Проба";
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 1;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.Controls.Add(this.samplesListBox, 0, 1);
            this.tableLayoutPanel9.Controls.Add(this.allSamplesCheckBox, 0, 0);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 2;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(138, 251);
            this.tableLayoutPanel9.TabIndex = 0;
            // 
            // allSamplesCheckBox
            // 
            this.allSamplesCheckBox.AutoSize = true;
            this.allSamplesCheckBox.Checked = true;
            this.allSamplesCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.allSamplesCheckBox.Location = new System.Drawing.Point(3, 3);
            this.allSamplesCheckBox.Name = "allSamplesCheckBox";
            this.allSamplesCheckBox.Size = new System.Drawing.Size(73, 17);
            this.allSamplesCheckBox.TabIndex = 1;
            this.allSamplesCheckBox.Text = "Все типы";
            this.allSamplesCheckBox.UseVisualStyleBackColor = true;
            this.allSamplesCheckBox.CheckedChanged += new System.EventHandler(this.allSamplesCheckBox_CheckedChanged);
            // 
            // varTypeListBox
            // 
            //this.varTypeListBox.AllowMultiSelect = true;
            //this.varTypeListBox.CheckedId = ((System.Collections.Generic.List<int>)(resources.GetObject("varTypeListBox.CheckedId")));
            this.varTypeListBox.ColumnsHeadersVisible = false;
            //this.varTypeListBox.CurrentDicItemId = null;
            //this.varTypeListBox.DicItemName = null;
            this.varTypeListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.varTypeListBox.Enabled = false;
            this.varTypeListBox.Location = new System.Drawing.Point(3, 26);
            this.varTypeListBox.Name = "varTypeListBox";
            //this.varTypeListBox.SelectedRowId = -1;
            this.varTypeListBox.ShowAddNewToolbarButton = false;
            //this.varTypeListBox.ShowCheckBox = true;
            this.varTypeListBox.ShowColumnHeaders = false;
            this.varTypeListBox.ShowDeleteToolbarButton = false;
            this.varTypeListBox.ShowFindItemToolbarButton = false;
            this.varTypeListBox.ShowId = true;
            this.varTypeListBox.ShowOrderControls = false;
            this.varTypeListBox.ShowOrderToolbarButton = false;
            this.varTypeListBox.ShowSaveToolbarButton = false;
            this.varTypeListBox.ShowSelectAllToolbarButton = false;
            this.varTypeListBox.ShowSelectedOnly = false;
            this.varTypeListBox.ShowSelectedOnlyToolbarButton = false;
            this.varTypeListBox.ShowToolbar = false;
            this.varTypeListBox.ShowUnselectAllToolbarButton = false;
            this.varTypeListBox.ShowUpdateToolbarButton = false;
            this.varTypeListBox.Size = new System.Drawing.Size(195, 216);
            this.varTypeListBox.TabIndex = 0;
            // 
            // valueTypeListBox
            // 
            //this.valueTypeListBox.AllowMultiSelect = true;
            //this.valueTypeListBox.CheckedId = ((System.Collections.Generic.List<int>)(resources.GetObject("valueTypeListBox.CheckedId")));
            this.valueTypeListBox.ColumnsHeadersVisible = false;
            //this.valueTypeListBox.CurrentDicItemId = null;
            //this.valueTypeListBox.DicItemName = null;
            this.valueTypeListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.valueTypeListBox.Enabled = false;
            this.valueTypeListBox.Location = new System.Drawing.Point(3, 26);
            this.valueTypeListBox.Name = "valueTypeListBox";
            //this.valueTypeListBox.SelectedRowId = -1;
            this.valueTypeListBox.ShowAddNewToolbarButton = false;
            //this.valueTypeListBox.ShowCheckBox = true;
            this.valueTypeListBox.ShowColumnHeaders = false;
            this.valueTypeListBox.ShowDeleteToolbarButton = false;
            this.valueTypeListBox.ShowFindItemToolbarButton = false;
            this.valueTypeListBox.ShowId = true;
            this.valueTypeListBox.ShowOrderControls = false;
            this.valueTypeListBox.ShowOrderToolbarButton = false;
            this.valueTypeListBox.ShowSaveToolbarButton = false;
            this.valueTypeListBox.ShowSelectAllToolbarButton = false;
            this.valueTypeListBox.ShowSelectedOnly = false;
            this.valueTypeListBox.ShowSelectedOnlyToolbarButton = false;
            this.valueTypeListBox.ShowToolbar = false;
            this.valueTypeListBox.ShowUnselectAllToolbarButton = false;
            this.valueTypeListBox.ShowUpdateToolbarButton = false;
            this.valueTypeListBox.Size = new System.Drawing.Size(195, 216);
            this.valueTypeListBox.TabIndex = 0;
            // 
            // timeListBox
            // 
            //this.timeListBox.AllowMultiSelect = true;
            //this.timeListBox.CheckedId = ((System.Collections.Generic.List<int>)(resources.GetObject("timeListBox.CheckedId")));
            this.timeListBox.ColumnsHeadersVisible = false;
            //this.timeListBox.CurrentDicItemId = null;
            //this.timeListBox.DicItemName = null;
            this.timeListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timeListBox.Enabled = false;
            this.timeListBox.Location = new System.Drawing.Point(3, 26);
            this.timeListBox.Name = "timeListBox";
            //this.timeListBox.SelectedRowId = -1;
            this.timeListBox.ShowAddNewToolbarButton = false;
            //this.timeListBox.ShowCheckBox = true;
            this.timeListBox.ShowColumnHeaders = false;
            this.timeListBox.ShowDeleteToolbarButton = false;
            this.timeListBox.ShowFindItemToolbarButton = false;
            this.timeListBox.ShowId = true;
            this.timeListBox.ShowOrderControls = false;
            this.timeListBox.ShowOrderToolbarButton = false;
            this.timeListBox.ShowSaveToolbarButton = false;
            this.timeListBox.ShowSelectAllToolbarButton = false;
            this.timeListBox.ShowSelectedOnly = false;
            this.timeListBox.ShowSelectedOnlyToolbarButton = false;
            this.timeListBox.ShowToolbar = false;
            this.timeListBox.ShowUnselectAllToolbarButton = false;
            this.timeListBox.ShowUpdateToolbarButton = false;
            this.timeListBox.Size = new System.Drawing.Size(195, 177);
            this.timeListBox.TabIndex = 0;
            // 
            // unitListBox
            // 
            //this.unitListBox.AllowMultiSelect = true;
            //this.unitListBox.CheckedId = ((System.Collections.Generic.List<int>)(resources.GetObject("unitListBox.CheckedId")));
            this.unitListBox.ColumnsHeadersVisible = false;
            //this.unitListBox.CurrentDicItemId = null;
            //this.unitListBox.DicItemName = null;
            this.unitListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.unitListBox.Enabled = false;
            this.unitListBox.Location = new System.Drawing.Point(3, 26);
            this.unitListBox.Name = "unitListBox";
            //this.unitListBox.SelectedRowId = -1;
            this.unitListBox.ShowAddNewToolbarButton = false;
            //this.unitListBox.ShowCheckBox = true;
            this.unitListBox.ShowColumnHeaders = false;
            this.unitListBox.ShowDeleteToolbarButton = false;
            this.unitListBox.ShowFindItemToolbarButton = false;
            this.unitListBox.ShowId = true;
            this.unitListBox.ShowOrderControls = false;
            this.unitListBox.ShowOrderToolbarButton = false;
            this.unitListBox.ShowSaveToolbarButton = false;
            this.unitListBox.ShowSelectAllToolbarButton = false;
            this.unitListBox.ShowSelectedOnly = false;
            this.unitListBox.ShowSelectedOnlyToolbarButton = false;
            this.unitListBox.ShowToolbar = false;
            this.unitListBox.ShowUnselectAllToolbarButton = false;
            this.unitListBox.ShowUpdateToolbarButton = false;
            this.unitListBox.Size = new System.Drawing.Size(195, 216);
            this.unitListBox.TabIndex = 0;
            // 
            // generalCategsListBox
            // 
            //this.generalCategsListBox.AllowMultiSelect = true;
            //this.generalCategsListBox.CheckedId = ((System.Collections.Generic.List<int>)(resources.GetObject("generalCategsListBox.CheckedId")));
            this.generalCategsListBox.ColumnsHeadersVisible = false;
            //this.generalCategsListBox.CurrentDicItemId = null;
            //this.generalCategsListBox.DicItemName = null;
            this.generalCategsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.generalCategsListBox.Enabled = false;
            this.generalCategsListBox.Location = new System.Drawing.Point(3, 26);
            this.generalCategsListBox.Name = "generalCategsListBox";
            //this.generalCategsListBox.SelectedRowId = -1;
            this.generalCategsListBox.ShowAddNewToolbarButton = false;
            //this.generalCategsListBox.ShowCheckBox = true;
            this.generalCategsListBox.ShowColumnHeaders = false;
            this.generalCategsListBox.ShowDeleteToolbarButton = false;
            this.generalCategsListBox.ShowFindItemToolbarButton = false;
            this.generalCategsListBox.ShowId = true;
            this.generalCategsListBox.ShowOrderControls = false;
            this.generalCategsListBox.ShowOrderToolbarButton = false;
            this.generalCategsListBox.ShowSaveToolbarButton = false;
            this.generalCategsListBox.ShowSelectAllToolbarButton = false;
            this.generalCategsListBox.ShowSelectedOnly = false;
            this.generalCategsListBox.ShowSelectedOnlyToolbarButton = false;
            this.generalCategsListBox.ShowToolbar = false;
            this.generalCategsListBox.ShowUnselectAllToolbarButton = false;
            this.generalCategsListBox.ShowUpdateToolbarButton = false;
            this.generalCategsListBox.Size = new System.Drawing.Size(131, 222);
            this.generalCategsListBox.TabIndex = 0;
            // 
            // dataTypesListBox
            // 
            //this.dataTypesListBox.AllowMultiSelect = true;
            //this.dataTypesListBox.CheckedId = ((System.Collections.Generic.List<int>)(resources.GetObject("dataTypesListBox.CheckedId")));
            this.dataTypesListBox.ColumnsHeadersVisible = false;
            //this.dataTypesListBox.CurrentDicItemId = null;
            //this.dataTypesListBox.DicItemName = null;
            this.dataTypesListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataTypesListBox.Enabled = false;
            this.dataTypesListBox.Location = new System.Drawing.Point(3, 26);
            this.dataTypesListBox.Name = "dataTypesListBox";
            //this.dataTypesListBox.SelectedRowId = -1;
            this.dataTypesListBox.ShowAddNewToolbarButton = false;
            //this.dataTypesListBox.ShowCheckBox = true;
            this.dataTypesListBox.ShowColumnHeaders = false;
            this.dataTypesListBox.ShowDeleteToolbarButton = false;
            this.dataTypesListBox.ShowFindItemToolbarButton = false;
            this.dataTypesListBox.ShowId = true;
            this.dataTypesListBox.ShowOrderControls = false;
            this.dataTypesListBox.ShowOrderToolbarButton = false;
            this.dataTypesListBox.ShowSaveToolbarButton = false;
            this.dataTypesListBox.ShowSelectAllToolbarButton = false;
            this.dataTypesListBox.ShowSelectedOnly = false;
            this.dataTypesListBox.ShowSelectedOnlyToolbarButton = false;
            this.dataTypesListBox.ShowToolbar = false;
            this.dataTypesListBox.ShowUnselectAllToolbarButton = false;
            this.dataTypesListBox.ShowUpdateToolbarButton = false;
            this.dataTypesListBox.Size = new System.Drawing.Size(131, 222);
            this.dataTypesListBox.TabIndex = 0;
            // 
            // samplesListBox
            // 
            //this.samplesListBox.AllowMultiSelect = true;
            //this.samplesListBox.CheckedId = ((System.Collections.Generic.List<int>)(resources.GetObject("samplesListBox.CheckedId")));
            this.samplesListBox.ColumnsHeadersVisible = false;
            //this.samplesListBox.CurrentDicItemId = null;
            //this.samplesListBox.DicItemName = null;
            this.samplesListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.samplesListBox.Enabled = false;
            this.samplesListBox.Location = new System.Drawing.Point(3, 26);
            this.samplesListBox.Name = "samplesListBox";
            //this.samplesListBox.SelectedRowId = -1;
            this.samplesListBox.ShowAddNewToolbarButton = false;
            //this.samplesListBox.ShowCheckBox = true;
            this.samplesListBox.ShowColumnHeaders = false;
            this.samplesListBox.ShowDeleteToolbarButton = false;
            this.samplesListBox.ShowFindItemToolbarButton = false;
            this.samplesListBox.ShowId = true;
            this.samplesListBox.ShowOrderControls = false;
            this.samplesListBox.ShowOrderToolbarButton = false;
            this.samplesListBox.ShowSaveToolbarButton = false;
            this.samplesListBox.ShowSelectAllToolbarButton = false;
            this.samplesListBox.ShowSelectedOnly = false;
            this.samplesListBox.ShowSelectedOnlyToolbarButton = false;
            this.samplesListBox.ShowToolbar = false;
            this.samplesListBox.ShowUnselectAllToolbarButton = false;
            this.samplesListBox.ShowUpdateToolbarButton = false;
            this.samplesListBox.Size = new System.Drawing.Size(132, 222);
            this.samplesListBox.TabIndex = 0;
            // 
            // UCVariableFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "UCVariableFilter";
            this.Size = new System.Drawing.Size(456, 302);
            this.Load += new System.EventHandler(this.UCVariableFilter_Load);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.tableLayoutPanel10.ResumeLayout(false);
            this.tableLayoutPanel10.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel9.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Common.UCList varTypeListBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckBox allVarTypesCheckBox;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Common.UCList valueTypeListBox;
        private System.Windows.Forms.CheckBox allValueTypesCheckBox;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private Common.UCList timeListBox;
        private System.Windows.Forms.CheckBox allTimeCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox timeSupportTextBox;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private Common.UCList unitListBox;
        private System.Windows.Forms.CheckBox allUnitsCheckBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
        private Common.UCList generalCategsListBox;
        private System.Windows.Forms.CheckBox allGeneralCategoriesCheckBox;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private Common.UCList dataTypesListBox;
        private System.Windows.Forms.CheckBox allDataTypesCheckBox;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private Common.UCList samplesListBox;
        private System.Windows.Forms.CheckBox allSamplesCheckBox;
    }
}
