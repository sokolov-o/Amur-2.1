namespace SOV.Amur.Meta
{
    partial class UCSiteEdit
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.ucSite = new SOV.Amur.Meta.UCSite();
            this.saveButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ucStationSites = new SOV.Amur.Meta.UCSiteXSites();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ucEntityAttrValues = new SOV.Amur.Meta.UCEntityAttrValues();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.ucCatalogs = new SOV.Amur.Meta.UCCatalogs();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.ucSiteInstruments = new SOV.Amur.Meta.UCSiteInstruments();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.ucSiteXSites1 = new SOV.Amur.Meta.UCSiteXSites();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.ucSiteXSites2 = new SOV.Amur.Meta.UCSiteXSites();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.geoObjectsListBox = new SOV.Common.UCList();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(755, 110);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Пост/станция";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.ucSite, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.saveButton, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(749, 91);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // ucStation
            // 
            this.ucSite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSite.Location = new System.Drawing.Point(7, 7);
            this.ucSite.Margin = new System.Windows.Forms.Padding(7);
            this.ucSite.Name = "ucStation";
            this.ucSite.Size = new System.Drawing.Size(655, 83);
            this.ucSite.TabIndex = 0;
            // 
            // saveButton
            // 
            this.saveButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.saveButton.Location = new System.Drawing.Point(672, 3);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(74, 91);
            this.saveButton.TabIndex = 2;
            this.saveButton.Text = "Сохранить";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tabControl2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(759, 494);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(3, 119);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(755, 372);
            this.tabControl2.TabIndex = 5;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.splitContainer1);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(747, 346);
            this.tabPage5.TabIndex = 1;
            this.tabPage5.Text = "Наблюдательные пункты";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ucStationSites);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(741, 340);
            this.splitContainer1.SplitterDistance = 182;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 4;
            // 
            // ucStationSites
            // 
            this.ucStationSites.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucStationSites.Location = new System.Drawing.Point(0, 0);
            this.ucStationSites.Margin = new System.Windows.Forms.Padding(7);
            this.ucStationSites.Name = "ucStationSites";
            this.ucStationSites.Size = new System.Drawing.Size(178, 336);
            this.ucStationSites.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(553, 336);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(545, 310);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Атрибуты пункта";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ucEntityAttrValues);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(539, 304);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Атрибуты наблюдательного пункта";
            // 
            // ucEntityAttrValues
            // 
            this.ucEntityAttrValues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucEntityAttrValues.EntityId = 0;
            this.ucEntityAttrValues.EntityName = null;
            this.ucEntityAttrValues.EntityTypeId = 0;
            this.ucEntityAttrValues.Location = new System.Drawing.Point(3, 16);
            this.ucEntityAttrValues.Margin = new System.Windows.Forms.Padding(7);
            this.ucEntityAttrValues.Name = "ucEntityAttrValues";
            this.ucEntityAttrValues.Size = new System.Drawing.Size(533, 285);
            this.ucEntityAttrValues.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.ucCatalogs);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(545, 310);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Записи каталога данных";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // ucCatalogs
            // 
            this.ucCatalogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucCatalogs.Location = new System.Drawing.Point(0, 0);
            this.ucCatalogs.Margin = new System.Windows.Forms.Padding(7);
            this.ucCatalogs.Name = "ucCatalogs";
            this.ucCatalogs.ShowDataValueEventHandler = null;
            this.ucCatalogs.Size = new System.Drawing.Size(545, 310);
            this.ucCatalogs.TabIndex = 0;
            this.ucCatalogs.UCAddCatalogButtonVisible = false;
            this.ucCatalogs.UCCatalogFilterVisible = false;
            this.ucCatalogs.UCCatalogsText = "";
            this.ucCatalogs.UCCatalogVisible = true;
            this.ucCatalogs.UCDeleteCatalogButtonVisible = false;
            this.ucCatalogs.UCShowOrderbyButtons = false;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.ucSiteInstruments);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Margin = new System.Windows.Forms.Padding(1);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(545, 310);
            this.tabPage6.TabIndex = 3;
            this.tabPage6.Text = "Приборы";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // ucSiteInstruments
            // 
            this.ucSiteInstruments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSiteInstruments.Location = new System.Drawing.Point(0, 0);
            this.ucSiteInstruments.Margin = new System.Windows.Forms.Padding(0);
            this.ucSiteInstruments.Name = "ucSiteInstruments";
            this.ucSiteInstruments.Size = new System.Drawing.Size(545, 310);
            this.ucSiteInstruments.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(545, 310);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Связанные пункты";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(3, 3);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.groupBox5);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.groupBox6);
            this.splitContainer3.Size = new System.Drawing.Size(539, 304);
            this.splitContainer3.SplitterDistance = 140;
            this.splitContainer3.SplitterWidth = 2;
            this.splitContainer3.TabIndex = 2;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.ucSiteXSites1);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(0, 0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(539, 140);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Пункты справа";
            // 
            // ucSiteXSites1
            // 
            this.ucSiteXSites1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSiteXSites1.Location = new System.Drawing.Point(3, 16);
            this.ucSiteXSites1.Margin = new System.Windows.Forms.Padding(7);
            this.ucSiteXSites1.Name = "ucSiteXSites1";
            this.ucSiteXSites1.SiteNum1Or2 = 1;
            this.ucSiteXSites1.Size = new System.Drawing.Size(533, 121);
            this.ucSiteXSites1.TabIndex = 0;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.ucSiteXSites2);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Location = new System.Drawing.Point(0, 0);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(539, 162);
            this.groupBox6.TabIndex = 1;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Пункты слева";
            // 
            // ucSiteXSites2
            // 
            this.ucSiteXSites2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSiteXSites2.Location = new System.Drawing.Point(3, 16);
            this.ucSiteXSites2.Margin = new System.Windows.Forms.Padding(7);
            this.ucSiteXSites2.Name = "ucSiteXSites2";
            this.ucSiteXSites2.SiteNum1Or2 = 2;
            this.ucSiteXSites2.Size = new System.Drawing.Size(533, 143);
            this.ucSiteXSites2.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.geoObjectsListBox);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(747, 346);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "Гео-объекты станции";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // geoObjectsListBox
            // 
            this.geoObjectsListBox.ColumnsHeadersVisible = true;
            this.geoObjectsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.geoObjectsListBox.Location = new System.Drawing.Point(3, 3);
            this.geoObjectsListBox.MultiSelect = false;
            this.geoObjectsListBox.Name = "geoObjectsListBox";
            this.geoObjectsListBox.ShowAddNewToolbarButton = true;
            this.geoObjectsListBox.ShowColumnHeaders = true;
            this.geoObjectsListBox.ShowDeleteToolbarButton = true;
            this.geoObjectsListBox.ShowFindItemToolbarButton = true;
            this.geoObjectsListBox.ShowId = true;
            this.geoObjectsListBox.ShowOrderControls = false;
            this.geoObjectsListBox.ShowOrderToolbarButton = false;
            this.geoObjectsListBox.ShowSaveToolbarButton = false;
            this.geoObjectsListBox.ShowSelectAllToolbarButton = false;
            this.geoObjectsListBox.ShowSelectedOnly = false;
            this.geoObjectsListBox.ShowSelectedOnlyToolbarButton = false;
            this.geoObjectsListBox.ShowToolbar = true;
            this.geoObjectsListBox.ShowUnselectAllToolbarButton = false;
            this.geoObjectsListBox.ShowUpdateToolbarButton = false;
            this.geoObjectsListBox.Size = new System.Drawing.Size(741, 340);
            this.geoObjectsListBox.TabIndex = 0;
            this.geoObjectsListBox.UCAddNewEvent += new SOV.Common.UCList.UCAddNewEventHandler(this.geoObjectsListBox_UCAddNewEvent);
            this.geoObjectsListBox.UCDeleteEvent += new SOV.Common.UCList.UCDeleteEventHandler(this.geoObjectsListBox_UCDeleteEvent);
            // 
            // UCStationEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UCStationEdit";
            this.Size = new System.Drawing.Size(759, 494);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UCSite ucSite;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private UCSiteXSites ucStationSites;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.GroupBox groupBox3;
        private UCEntityAttrValues ucEntityAttrValues;
        private System.Windows.Forms.GroupBox groupBox5;
        private UCSiteXSites ucSiteXSites1;
        private System.Windows.Forms.GroupBox groupBox6;
        private UCSiteXSites ucSiteXSites2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private UCCatalogs ucCatalogs;
        private System.Windows.Forms.TabPage tabPage6;
        private UCSiteInstruments ucSiteInstruments;
        private Common.UCList geoObjectsListBox;
    }
}
