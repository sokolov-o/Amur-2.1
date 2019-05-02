namespace SOV.Amur.Meta
{
    partial class FormGeoObjects
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGeoObjects));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.ucGeoobTree = new SOV.Common.UCDicTree();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsEditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.ucStations = new SOV.Common.UCList();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(303, 338);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Геообъекты (в т.ч. реки)";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.ucGeoobTree, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(297, 319);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // ucGeoobTree
            // 
            this.ucGeoobTree.ContextMenuStrip = this.contextMenuStrip1;
            this.ucGeoobTree.ContextMenuStrip4Types = null;
            this.ucGeoobTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucGeoobTree.Location = new System.Drawing.Point(3, 3);
            this.ucGeoobTree.Name = "ucGeoobTree";
            this.ucGeoobTree.SelectedDicItem = null;
            this.ucGeoobTree.ShowAddButton = true;
            this.ucGeoobTree.ShowCloneButton = true;
            this.ucGeoobTree.ShowDeleteButton = true;
            this.ucGeoobTree.ShowRefreshButton = true;
            this.ucGeoobTree.ShowToolStrip = true;
            this.ucGeoobTree.Size = new System.Drawing.Size(291, 313);
            this.ucGeoobTree.TabIndex = 1;
            this.ucGeoobTree.User = null;
            this.ucGeoobTree.UCSelectedItemChanged += new SOV.Common.UCDicTree.UCSelectedItemChangedEventHandler(this.ucGeoobTree_UCSelectedItemChanged);
            this.ucGeoobTree.UCRefresh += new SOV.Common.UCDicTree.UCRefreshEventHandler(this.ucGeoobTree_UCRefresh);
            this.ucGeoobTree.UCAddNewItem += new SOV.Common.UCDicTree.UCAddItemEventHandler(this.ucGeoobTree_UCAddNewItem);
            this.ucGeoobTree.UCDeleteItem += new SOV.Common.UCDicTree.UCDeleteItemEventHandler(this.ucGeoobTree_UCDeleteItem);
            this.ucGeoobTree.UCCloneItem += new SOV.Common.UCDicTree.UCCloneItemEventHandler(this.ucGeoobTree_UCCloneItem);
            this.ucGeoobTree.UCNodesOrderChangedEvent += new SOV.Common.UCDicTree.UCNodesOrderChangedEventHandler(this.ucGeoobTree_UCNodesOrderChangedEvent);
            this.ucGeoobTree.UCEditNewItem += new SOV.Common.UCDicTree.UCEditItemEventHandler(this.ucGeoobTree_UCEditNewItem);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsEditToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(164, 26);
            // 
            // cmsEditToolStripMenuItem
            // 
            this.cmsEditToolStripMenuItem.Name = "cmsEditToolStripMenuItem";
            this.cmsEditToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.cmsEditToolStripMenuItem.Text = "Редактировать...";
            this.cmsEditToolStripMenuItem.Click += new System.EventHandler(this.cmsEditToolStripMenuItem_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.button1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(618, 373);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(312, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 513F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(303, 338);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel5);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(297, 332);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Станции и посты, относящиеся к объекту";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 3;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.ucStations, 2, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(291, 313);
            this.tableLayoutPanel5.TabIndex = 1;
            // 
            // ucStations
            // 
            this.ucStations.ColumnsHeadersVisible = false;
            this.ucStations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucStations.Location = new System.Drawing.Point(3, 3);
            this.ucStations.MultiSelect = false;
            this.ucStations.Name = "ucStations";
            this.ucStations.ShowAddNewToolbarButton = true;
            this.ucStations.ShowColumnHeaders = false;
            this.ucStations.ShowDeleteToolbarButton = true;
            this.ucStations.ShowFindItemToolbarButton = true;
            this.ucStations.ShowId = false;
            this.ucStations.ShowOrderControls = true;
            this.ucStations.ShowOrderToolbarButton = false;
            this.ucStations.ShowSaveToolbarButton = true;
            this.ucStations.ShowSelectAllToolbarButton = false;
            this.ucStations.ShowSelectedOnly = false;
            this.ucStations.ShowSelectedOnlyToolbarButton = false;
            this.ucStations.ShowToolbar = true;
            this.ucStations.ShowUnselectAllToolbarButton = false;
            this.ucStations.ShowUpdateToolbarButton = false;
            this.ucStations.Size = new System.Drawing.Size(285, 307);
            this.ucStations.TabIndex = 0;
            this.ucStations.UCItemOrderChangedEvent += new SOV.Common.UCList.UCItemOrderChangedEventHandler(this.ucStations_UCItemOrderChangedEvent);
            this.ucStations.UCAddNewEvent += new SOV.Common.UCList.UCAddNewEventHandler(this.ucStations_UCAddNewEvent);
            this.ucStations.UCDeleteEvent += new SOV.Common.UCList.UCDeleteEventHandler(this.ucStations_UCDeleteEvent);
            this.ucStations.UCSaveEvent += new SOV.Common.UCList.UCSaveEventHandler(this.ucStations_UCSaveEvent);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Dock = System.Windows.Forms.DockStyle.Left;
            this.button1.Location = new System.Drawing.Point(3, 347);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Закрыть";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormGeoObjects
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button1;
            this.ClientSize = new System.Drawing.Size(618, 373);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormGeoObjects";
            this.Text = "Редактирование геообъектов";
            this.Load += new System.EventHandler(this.FormWObjects_Load);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private Common.UCDicTree ucGeoobTree;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cmsEditToolStripMenuItem;
        private Common.UCList ucStations;
    }
}