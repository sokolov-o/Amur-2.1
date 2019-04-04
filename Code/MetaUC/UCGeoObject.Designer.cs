namespace SOV.Amur.Meta
{
    partial class UCGeoObject
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCGeoObject));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.orderTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.childsDicListBox = new SOV.Common.UCList();
            this.fallIntoDicComboBox = new SOV.Common.UCDicComboBox();
            this.geoTypeDicComboBox = new SOV.Common.UCDicComboBox();
            this.refreshTypeList = new System.Windows.Forms.Button();
            this.refreshGeoobList = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.fallIntoDicComboBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.geoTypeDicComboBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.nameTextBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.orderTextBox, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.refreshTypeList, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.refreshGeoobList, 2, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(273, 335);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox1, 3);
            this.groupBox1.Controls.Add(this.childsDicListBox);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 113);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(267, 219);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Впадают \"в\" или являются частью геообъекта";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 29);
            this.label3.TabIndex = 6;
            this.label3.Text = "Впадает в:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 29);
            this.label1.TabIndex = 4;
            this.label1.Text = "Тип:";
            // 
            // nameTextBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.nameTextBox, 2);
            this.nameTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nameTextBox.Location = new System.Drawing.Point(95, 3);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(175, 20);
            this.nameTextBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 26);
            this.label2.TabIndex = 2;
            this.label2.Text = "Наименование:";
            // 
            // orderTextBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.orderTextBox, 2);
            this.orderTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orderTextBox.Location = new System.Drawing.Point(95, 87);
            this.orderTextBox.Name = "orderTextBox";
            this.orderTextBox.Size = new System.Drawing.Size(175, 20);
            this.orderTextBox.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Location = new System.Drawing.Point(3, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 26);
            this.label4.TabIndex = 12;
            this.label4.Text = "Порядок:";
            // 
            // childsDicListBox
            // 
            this.childsDicListBox.ColumnsHeadersVisible = false;
            this.childsDicListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.childsDicListBox.Location = new System.Drawing.Point(3, 16);
            this.childsDicListBox.Name = "childsDicListBox";
            this.childsDicListBox.ShowAddNewToolbarButton = true;
            this.childsDicListBox.ShowColumnHeaders = false;
            this.childsDicListBox.ShowDeleteToolbarButton = true;
            this.childsDicListBox.ShowFindItemToolbarButton = false;
            this.childsDicListBox.ShowId = false;
            this.childsDicListBox.ShowOrderControls = true;
            this.childsDicListBox.ShowOrderToolbarButton = false;
            this.childsDicListBox.ShowSaveToolbarButton = false;
            this.childsDicListBox.ShowSelectAllToolbarButton = false;
            this.childsDicListBox.ShowSelectedOnly = false;
            this.childsDicListBox.ShowSelectedOnlyToolbarButton = false;
            this.childsDicListBox.ShowToolbar = true;
            this.childsDicListBox.ShowUnselectAllToolbarButton = false;
            this.childsDicListBox.ShowUpdateToolbarButton = false;
            this.childsDicListBox.Size = new System.Drawing.Size(261, 200);
            this.childsDicListBox.TabIndex = 0;
            // 
            // fallIntoDicComboBox
            // 
            this.fallIntoDicComboBox.CheckBoxVisible = false;
            this.fallIntoDicComboBox.Checked = true;
            this.fallIntoDicComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fallIntoDicComboBox.Location = new System.Drawing.Point(95, 58);
            this.fallIntoDicComboBox.Name = "fallIntoDicComboBox";
            this.fallIntoDicComboBox.SelectedId = null;
            this.fallIntoDicComboBox.SelectedIndex = -1;
            this.fallIntoDicComboBox.Size = new System.Drawing.Size(144, 23);
            this.fallIntoDicComboBox.TabIndex = 7;
            // 
            // geoTypeDicComboBox
            // 
            this.geoTypeDicComboBox.CheckBoxVisible = false;
            this.geoTypeDicComboBox.Checked = true;
            this.geoTypeDicComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.geoTypeDicComboBox.Location = new System.Drawing.Point(95, 29);
            this.geoTypeDicComboBox.Name = "geoTypeDicComboBox";
            this.geoTypeDicComboBox.SelectedId = null;
            this.geoTypeDicComboBox.SelectedIndex = -1;
            this.geoTypeDicComboBox.Size = new System.Drawing.Size(144, 23);
            this.geoTypeDicComboBox.TabIndex = 5;
            // 
            // refreshTypeList
            // 
            this.refreshTypeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.refreshTypeList.Image = global::SOV.Amur.Meta.Properties.Resources.refresh_16xLG;
            this.refreshTypeList.Location = new System.Drawing.Point(245, 29);
            this.refreshTypeList.Name = "refreshTypeList";
            this.refreshTypeList.Size = new System.Drawing.Size(25, 23);
            this.refreshTypeList.TabIndex = 13;
            this.refreshTypeList.UseVisualStyleBackColor = true;
            this.refreshTypeList.Click += new System.EventHandler(this.refreshTypeList_Click);
            // 
            // refreshGeoobList
            // 
            this.refreshGeoobList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.refreshGeoobList.Image = global::SOV.Amur.Meta.Properties.Resources.refresh_16xLG;
            this.refreshGeoobList.Location = new System.Drawing.Point(245, 58);
            this.refreshGeoobList.Name = "refreshGeoobList";
            this.refreshGeoobList.Size = new System.Drawing.Size(25, 23);
            this.refreshGeoobList.TabIndex = 14;
            this.refreshGeoobList.UseVisualStyleBackColor = true;
            this.refreshGeoobList.Click += new System.EventHandler(this.refreshGeoobList_Click);
            // 
            // UCGeoObject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UCGeoObject";
            this.Size = new System.Drawing.Size(273, 335);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private Common.UCList childsDicListBox;
        private System.Windows.Forms.TextBox orderTextBox;
        private Common.UCDicComboBox fallIntoDicComboBox;
        private System.Windows.Forms.Label label3;
        private Common.UCDicComboBox geoTypeDicComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button refreshTypeList;
        private System.Windows.Forms.Button refreshGeoobList;

    }
}
