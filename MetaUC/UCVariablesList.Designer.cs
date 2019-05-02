namespace SOV.Amur.Meta
{
    partial class UCVariablesList
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameRus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.variableName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unitsTimeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeSupport = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unitsName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valueTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.generalCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sampleMedium = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name_eng = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.code_no_data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.code_err_data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.infoLabel = new System.Windows.Forms.ToolStripLabel();
            this.filterButton = new System.Windows.Forms.ToolStripButton();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuShowToolBoxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dgv, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(724, 319);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.nameRus,
            this.variableName,
            this.unitsTimeName,
            this.timeSupport,
            this.unitsName,
            this.valueTypeName,
            this.dataTypeName,
            this.generalCategory,
            this.sampleMedium,
            this.name_eng,
            this.code_no_data,
            this.code_err_data});
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(3, 28);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(718, 288);
            this.dgv.TabIndex = 1;
            this.dgv.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_RowEnter);
            // 
            // id
            // 
            this.id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.id.HeaderText = "Код";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Width = 5;
            // 
            // nameRus
            // 
            this.nameRus.HeaderText = "Название (рус)";
            this.nameRus.Name = "nameRus";
            this.nameRus.ReadOnly = true;
            // 
            // variableName
            // 
            this.variableName.HeaderText = "Переменная";
            this.variableName.Name = "variableName";
            this.variableName.ReadOnly = true;
            // 
            // unitsTimeName
            // 
            this.unitsTimeName.HeaderText = "Время";
            this.unitsTimeName.Name = "unitsTimeName";
            this.unitsTimeName.ReadOnly = true;
            // 
            // timeSupport
            // 
            this.timeSupport.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.timeSupport.HeaderText = "Временной интервал";
            this.timeSupport.Name = "timeSupport";
            this.timeSupport.ReadOnly = true;
            this.timeSupport.Width = 5;
            // 
            // unitsName
            // 
            this.unitsName.HeaderText = "Ед. изм.";
            this.unitsName.Name = "unitsName";
            this.unitsName.ReadOnly = true;
            // 
            // valueTypeName
            // 
            this.valueTypeName.HeaderText = "Тип значения";
            this.valueTypeName.Name = "valueTypeName";
            this.valueTypeName.ReadOnly = true;
            // 
            // dataTypeName
            // 
            this.dataTypeName.HeaderText = "Тип данных";
            this.dataTypeName.Name = "dataTypeName";
            this.dataTypeName.ReadOnly = true;
            // 
            // generalCategory
            // 
            this.generalCategory.HeaderText = "Категория";
            this.generalCategory.Name = "generalCategory";
            this.generalCategory.ReadOnly = true;
            // 
            // sampleMedium
            // 
            this.sampleMedium.HeaderText = "Проба";
            this.sampleMedium.Name = "sampleMedium";
            this.sampleMedium.ReadOnly = true;
            // 
            // name_eng
            // 
            this.name_eng.HeaderText = "Название (англ)";
            this.name_eng.Name = "name_eng";
            this.name_eng.ReadOnly = true;
            // 
            // code_no_data
            // 
            this.code_no_data.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.code_no_data.HeaderText = "ОТСУТ";
            this.code_no_data.Name = "code_no_data";
            this.code_no_data.ReadOnly = true;
            this.code_no_data.Width = 69;
            // 
            // code_err_data
            // 
            this.code_err_data.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.code_err_data.HeaderText = "ОШИБ";
            this.code_err_data.Name = "code_err_data";
            this.code_err_data.ReadOnly = true;
            this.code_err_data.Width = 64;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.infoLabel,
            this.filterButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(724, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // infoLabel
            // 
            this.infoLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(16, 22);
            this.infoLabel.Text = "...";
            this.infoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // filterButton
            // 
            this.filterButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.filterButton.Image = global::SOV.Amur.Meta.Properties.Resources.filter_16xLG;
            this.filterButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.filterButton.Name = "filterButton";
            this.filterButton.Size = new System.Drawing.Size(23, 22);
            this.filterButton.Text = "Установить фильтр переменных";
            this.filterButton.Click += new System.EventHandler(this.filterButton_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuShowToolBoxToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(279, 26);
            // 
            // mnuShowToolBoxToolStripMenuItem
            // 
            this.mnuShowToolBoxToolStripMenuItem.Name = "mnuShowToolBoxToolStripMenuItem";
            this.mnuShowToolBoxToolStripMenuItem.Size = new System.Drawing.Size(278, 22);
            this.mnuShowToolBoxToolStripMenuItem.Text = "Показать/скрыть панель управления";
            this.mnuShowToolBoxToolStripMenuItem.Click += new System.EventHandler(this.mnuShowToolBoxToolStripMenuItem_Click);
            // 
            // UCVariablesList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UCVariablesList";
            this.Size = new System.Drawing.Size(724, 319);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel infoLabel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuShowToolBoxToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameRus;
        private System.Windows.Forms.DataGridViewTextBoxColumn variableName;
        private System.Windows.Forms.DataGridViewTextBoxColumn unitsTimeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeSupport;
        private System.Windows.Forms.DataGridViewTextBoxColumn unitsName;
        private System.Windows.Forms.DataGridViewTextBoxColumn valueTypeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataTypeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn generalCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn sampleMedium;
        private System.Windows.Forms.DataGridViewTextBoxColumn name_eng;
        private System.Windows.Forms.DataGridViewTextBoxColumn code_no_data;
        private System.Windows.Forms.DataGridViewTextBoxColumn code_err_data;
        private System.Windows.Forms.ToolStripButton filterButton;
    }
}
