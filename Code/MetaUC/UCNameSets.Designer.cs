namespace SOV.Amur.Meta
{
    partial class UCNameSets
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.addNewNameSetButton = new System.Windows.Forms.ToolStripButton();
            this.saveOrderButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.langComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.likeTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.findLikeButton = new System.Windows.Forms.ToolStripButton();
            this.addNewFromSearchBoxButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.existsListBox = new SOV.Common.UCList();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.settingsButton = new System.Windows.Forms.ToolStripButton();
            this.ucNameItem = new SOV.Amur.Meta.UCNameItems();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(466, 413);
            this.splitContainer1.SplitterDistance = 242;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(466, 242);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Доступные наименования";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.existsListBox, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(460, 223);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewNameSetButton,
            this.saveOrderButton,
            this.toolStripSeparator1,
            this.langComboBox,
            this.toolStripLabel1,
            this.likeTextBox,
            this.findLikeButton,
            this.addNewFromSearchBoxButton,
            this.toolStripSeparator2,
            this.settingsButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(460, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // addNewNameSetButton
            // 
            this.addNewNameSetButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addNewNameSetButton.Image = global::SOV.Amur.Meta.Properties.Resources.action_add_16xLG1;
            this.addNewNameSetButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addNewNameSetButton.Name = "addNewNameSetButton";
            this.addNewNameSetButton.Size = new System.Drawing.Size(23, 22);
            this.addNewNameSetButton.Text = "Создать новый набор имён";
            this.addNewNameSetButton.Click += new System.EventHandler(this.addNewnameSetButton_Click);
            // 
            // saveOrderButton
            // 
            this.saveOrderButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveOrderButton.Image = global::SOV.Amur.Meta.Properties.Resources.SaveBlack;
            this.saveOrderButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveOrderButton.Name = "saveOrderButton";
            this.saveOrderButton.Size = new System.Drawing.Size(23, 22);
            this.saveOrderButton.Text = "Сохранить порядок элементов для сортировки";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // langComboBox
            // 
            this.langComboBox.Items.AddRange(new object[] {
            "Rus",
            "Eng"});
            this.langComboBox.Name = "langComboBox";
            this.langComboBox.Size = new System.Drawing.Size(75, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(44, 22);
            this.toolStripLabel1.Text = "Найти:";
            // 
            // likeTextBox
            // 
            this.likeTextBox.Name = "likeTextBox";
            this.likeTextBox.Size = new System.Drawing.Size(60, 25);
            // 
            // findLikeButton
            // 
            this.findLikeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.findLikeButton.Image = global::SOV.Amur.Meta.Properties.Resources.Find_5650;
            this.findLikeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.findLikeButton.Name = "findLikeButton";
            this.findLikeButton.Size = new System.Drawing.Size(23, 22);
            this.findLikeButton.Text = "Найти наименование, содержащее фразу на указанном языке";
            this.findLikeButton.Click += new System.EventHandler(this.findLikeButton_Click);
            // 
            // addNewFromSearchBoxButton
            // 
            this.addNewFromSearchBoxButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addNewFromSearchBoxButton.Image = global::SOV.Amur.Meta.Properties.Resources.action_add_16xLG;
            this.addNewFromSearchBoxButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addNewFromSearchBoxButton.Name = "addNewFromSearchBoxButton";
            this.addNewFromSearchBoxButton.Size = new System.Drawing.Size(23, 22);
            this.addNewFromSearchBoxButton.Text = "Выбрать имя или добавить новое имя на основе строки введённой в поиске, если его " +
    "нет в базе данных";
            this.addNewFromSearchBoxButton.Click += new System.EventHandler(this.addNewFromSearchBoxButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // existsListBox
            // 
            this.existsListBox.ColumnsHeadersVisible = true;
            this.existsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.existsListBox.Location = new System.Drawing.Point(3, 28);
            this.existsListBox.MultiSelect = true;
            this.existsListBox.Name = "existsListBox";
            this.existsListBox.ShowAddNewToolbarButton = false;
            this.existsListBox.ShowColumnHeaders = true;
            this.existsListBox.ShowDeleteToolbarButton = false;
            this.existsListBox.ShowFindItemToolbarButton = false;
            this.existsListBox.ShowId = false;
            this.existsListBox.ShowOrderControls = false;
            this.existsListBox.ShowOrderToolbarButton = false;
            this.existsListBox.ShowSaveToolbarButton = false;
            this.existsListBox.ShowSelectAllToolbarButton = false;
            this.existsListBox.ShowSelectedOnly = false;
            this.existsListBox.ShowSelectedOnlyToolbarButton = false;
            this.existsListBox.ShowToolbar = false;
            this.existsListBox.ShowUnselectAllToolbarButton = false;
            this.existsListBox.ShowUpdateToolbarButton = false;
            this.existsListBox.Size = new System.Drawing.Size(454, 192);
            this.existsListBox.TabIndex = 1;
            this.existsListBox.UCSelectedItemChanged += new SOV.Common.UCList.UCSelectedItemChangedEventHandler(this.existsListBox_UCSelectedItemChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ucNameItem);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(466, 167);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Наименования выбранного элемента";
            // 
            // settingsButton
            // 
            this.settingsButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.settingsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.settingsButton.Image = global::SOV.Amur.Meta.Properties.Resources.Property_501;
            this.settingsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(23, 22);
            this.settingsButton.Text = "Показать/скрыть группу редактирования элементов набора имен";
            this.settingsButton.Click += new System.EventHandler(this.settingsButton_Click);
            // 
            // ucNameItem
            // 
            this.ucNameItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucNameItem.Location = new System.Drawing.Point(3, 16);
            this.ucNameItem.Name = "ucNameItem";
            this.ucNameItem.Size = new System.Drawing.Size(460, 148);
            this.ucNameItem.TabIndex = 0;
            this.ucNameItem.UCShowToolStrip = true;
            // 
            // UCNameSets
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "UCNameSets";
            this.Size = new System.Drawing.Size(466, 413);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripComboBox langComboBox;
        private System.Windows.Forms.ToolStripTextBox likeTextBox;
        private System.Windows.Forms.ToolStripButton findLikeButton;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private UCNameItems ucNameItem;
        private System.Windows.Forms.ToolStripButton addNewNameSetButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton saveOrderButton;
        private Common.UCList existsListBox;
        private System.Windows.Forms.ToolStripButton addNewFromSearchBoxButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton settingsButton;
    }
}
