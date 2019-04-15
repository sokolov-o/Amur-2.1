namespace SOV.Amur.Reports
{
    partial class UCF50DGV
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.rptCaption = new System.Windows.Forms.TextBox();
            this.WaterObjectName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gagePoyma = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gageNya = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gageOya = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GageAnomaly = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DatesGageMaxString = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DatesGageMinString = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PoymaNotes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Notes_ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataFlagString = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.siteName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gageAvg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gageMax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gageMin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gageClm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precipitation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.countDays = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.f50CollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.f50CollectionBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dgv, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.rptCaption, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(600, 365);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToOrderColumns = true;
            this.dgv.AutoGenerateColumns = false;
            this.dgv.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.WaterObjectName,
            this.siteName,
            this.gageAvg,
            this.gageMax,
            this.gageMin,
            this.gageClm,
            this.gagePoyma,
            this.gageNya,
            this.gageOya,
            this.GageAnomaly,
            this.precipitation,
            this.DatesGageMaxString,
            this.DatesGageMinString,
            this.PoymaNotes,
            this.countDays,
            this.Notes_,
            this.DataFlagString});
            this.dgv.DataSource = this.f50CollectionBindingSource;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(3, 29);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.Size = new System.Drawing.Size(594, 333);
            this.dgv.TabIndex = 0;
            // 
            // rptCaption
            // 
            this.rptCaption.BackColor = System.Drawing.SystemColors.Window;
            this.rptCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rptCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(204)));
            this.rptCaption.ForeColor = System.Drawing.Color.Black;
            this.rptCaption.Location = new System.Drawing.Point(0, 0);
            this.rptCaption.Margin = new System.Windows.Forms.Padding(0);
            this.rptCaption.Multiline = true;
            this.rptCaption.Name = "rptCaption";
            this.rptCaption.Size = new System.Drawing.Size(600, 26);
            this.rptCaption.TabIndex = 1;
            this.rptCaption.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // WaterObjectName
            // 
            this.WaterObjectName.DataPropertyName = "WaterObjectName";
            this.WaterObjectName.HeaderText = "Водный объект";
            this.WaterObjectName.Name = "WaterObjectName";
            this.WaterObjectName.ReadOnly = true;
            // 
            // gagePoyma
            // 
            this.gagePoyma.DataPropertyName = "GagePoyma";
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Info;
            this.gagePoyma.DefaultCellStyle = dataGridViewCellStyle2;
            this.gagePoyma.HeaderText = "Пойма";
            this.gagePoyma.Name = "gagePoyma";
            this.gagePoyma.ReadOnly = true;
            // 
            // gageNya
            // 
            this.gageNya.DataPropertyName = "GageNya";
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Info;
            this.gageNya.DefaultCellStyle = dataGridViewCellStyle3;
            this.gageNya.HeaderText = "НЯ";
            this.gageNya.Name = "gageNya";
            this.gageNya.ReadOnly = true;
            // 
            // gageOya
            // 
            this.gageOya.DataPropertyName = "GageOya";
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Info;
            this.gageOya.DefaultCellStyle = dataGridViewCellStyle4;
            this.gageOya.HeaderText = "ОЯ";
            this.gageOya.Name = "gageOya";
            this.gageOya.ReadOnly = true;
            // 
            // GageAnomaly
            // 
            this.GageAnomaly.DataPropertyName = "GageAnomaly";
            this.GageAnomaly.HeaderText = "Аномалия уровня";
            this.GageAnomaly.Name = "GageAnomaly";
            this.GageAnomaly.ReadOnly = true;
            // 
            // DatesGageMaxString
            // 
            this.DatesGageMaxString.DataPropertyName = "DatesGageMaxString";
            this.DatesGageMaxString.HeaderText = "Дата макс.";
            this.DatesGageMaxString.Name = "DatesGageMaxString";
            this.DatesGageMaxString.ReadOnly = true;
            // 
            // DatesGageMinString
            // 
            this.DatesGageMinString.DataPropertyName = "DatesGageMinString";
            this.DatesGageMinString.HeaderText = "Дата мин.";
            this.DatesGageMinString.Name = "DatesGageMinString";
            this.DatesGageMinString.ReadOnly = true;
            // 
            // PoymaNotes
            // 
            this.PoymaNotes.DataPropertyName = "DatesGagePoymaString";
            this.PoymaNotes.HeaderText = "Даты выхода воды на пойму";
            this.PoymaNotes.Name = "PoymaNotes";
            this.PoymaNotes.ReadOnly = true;
            // 
            // Notes_
            // 
            this.Notes_.DataPropertyName = "Notes";
            this.Notes_.HeaderText = "Примечания";
            this.Notes_.Name = "Notes_";
            this.Notes_.ReadOnly = true;
            // 
            // DataFlagString
            // 
            this.DataFlagString.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DataFlagString.DataPropertyName = "DataFlagString";
            this.DataFlagString.HeaderText = "ДФ";
            this.DataFlagString.Name = "DataFlagString";
            this.DataFlagString.ReadOnly = true;
            this.DataFlagString.ToolTipText = "Признак отсутствия агрегированных данных в базе данных. У - уровень, О - осадки о" +
    "пределены по исходным, наблюдённым значениям, + - всё считано из базы данных.";
            this.DataFlagString.Width = 52;
            // 
            // siteName
            // 
            this.siteName.DataPropertyName = "SiteName";
            this.siteName.HeaderText = "Пункт";
            this.siteName.Name = "siteName";
            this.siteName.ReadOnly = true;
            // 
            // gageAvg
            // 
            this.gageAvg.DataPropertyName = "GageAvg";
            this.gageAvg.HeaderText = "Уровень средн.";
            this.gageAvg.Name = "gageAvg";
            this.gageAvg.ReadOnly = true;
            // 
            // gageMax
            // 
            this.gageMax.DataPropertyName = "GageMax";
            this.gageMax.HeaderText = "Уровень макс";
            this.gageMax.Name = "gageMax";
            this.gageMax.ReadOnly = true;
            // 
            // gageMin
            // 
            this.gageMin.DataPropertyName = "GageMin";
            this.gageMin.HeaderText = "Уровень  мин";
            this.gageMin.Name = "gageMin";
            this.gageMin.ReadOnly = true;
            // 
            // gageClm
            // 
            this.gageClm.DataPropertyName = "GageClm";
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Info;
            this.gageClm.DefaultCellStyle = dataGridViewCellStyle1;
            this.gageClm.HeaderText = "Норма";
            this.gageClm.Name = "gageClm";
            this.gageClm.ReadOnly = true;
            // 
            // precipitation
            // 
            this.precipitation.DataPropertyName = "Precipitation";
            this.precipitation.HeaderText = "Осадки, мм";
            this.precipitation.Name = "precipitation";
            this.precipitation.ReadOnly = true;
            // 
            // countDays
            // 
            this.countDays.DataPropertyName = "CountDays";
            this.countDays.HeaderText = "Дней";
            this.countDays.Name = "countDays";
            this.countDays.ReadOnly = true;
            // 
            // f50CollectionBindingSource
            // 
            this.f50CollectionBindingSource.DataSource = typeof(SOV.Amur.Reports.F50Collection);
            // 
            // UCF50DGV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UCF50DGV";
            this.Size = new System.Drawing.Size(600, 365);
            this.Load += new System.EventHandler(this.UCF50DGV_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.f50CollectionBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource f50CollectionBindingSource;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.TextBox rptCaption;
        private System.Windows.Forms.DataGridViewTextBoxColumn WaterObjectName;
        private System.Windows.Forms.DataGridViewTextBoxColumn siteName;
        private System.Windows.Forms.DataGridViewTextBoxColumn gageAvg;
        private System.Windows.Forms.DataGridViewTextBoxColumn gageMax;
        private System.Windows.Forms.DataGridViewTextBoxColumn gageMin;
        private System.Windows.Forms.DataGridViewTextBoxColumn gageClm;
        private System.Windows.Forms.DataGridViewTextBoxColumn gagePoyma;
        private System.Windows.Forms.DataGridViewTextBoxColumn gageNya;
        private System.Windows.Forms.DataGridViewTextBoxColumn gageOya;
        private System.Windows.Forms.DataGridViewTextBoxColumn GageAnomaly;
        private System.Windows.Forms.DataGridViewTextBoxColumn precipitation;
        private System.Windows.Forms.DataGridViewTextBoxColumn DatesGageMaxString;
        private System.Windows.Forms.DataGridViewTextBoxColumn DatesGageMinString;
        private System.Windows.Forms.DataGridViewTextBoxColumn PoymaNotes;
        private System.Windows.Forms.DataGridViewTextBoxColumn countDays;
        private System.Windows.Forms.DataGridViewTextBoxColumn Notes_;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataFlagString;
    }
}
