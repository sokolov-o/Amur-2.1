namespace FERHRI.Social
{
    partial class FormAddrs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAddrs));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tabControlAdm = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.ucAddr = new FERHRI.Social.UCAddr();
            this.ucAddrsAdm = new FERHRI.Social.UCAddrs();
            this.ucAddrsOrg = new FERHRI.Social.UCAddrs();
            this.ucAddrsAll = new FERHRI.Social.UCAddrs();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControlAdm.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.button2, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 154F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(608, 540);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(602, 148);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Объект";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.ucAddr, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.button1, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(596, 129);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 99);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Сохранить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tabControlAdm);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 157);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(602, 351);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Дерево объектов";
            // 
            // tabControlAdm
            // 
            this.tabControlAdm.Controls.Add(this.tabPage1);
            this.tabControlAdm.Controls.Add(this.tabPage2);
            this.tabControlAdm.Controls.Add(this.tabPage3);
            this.tabControlAdm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlAdm.Location = new System.Drawing.Point(3, 16);
            this.tabControlAdm.Name = "tabControlAdm";
            this.tabControlAdm.SelectedIndex = 0;
            this.tabControlAdm.Size = new System.Drawing.Size(596, 332);
            this.tabControlAdm.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ucAddrsAdm);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(588, 306);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Админ регионы";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.ucAddrsOrg);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(588, 306);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Организации";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.ucAddrsAll);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(588, 306);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Все объекты";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(3, 514);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Закрыть";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ucAddr
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.ucAddr, 2);
            this.ucAddr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucAddr.Location = new System.Drawing.Point(3, 3);
            this.ucAddr.Name = "ucAddr";
            this.ucAddr.Size = new System.Drawing.Size(590, 90);
            this.ucAddr.TabIndex = 0;
            this.ucAddr.Load += new System.EventHandler(this.ucAddr1_Load);
            // 
            // ucAddrsAdm
            // 
            this.ucAddrsAdm.AddrTypeIdIn = null;
            this.ucAddrsAdm.AddrTypeIdNotIn = 40;
            this.ucAddrsAdm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucAddrsAdm.Location = new System.Drawing.Point(3, 3);
            this.ucAddrsAdm.Name = "ucAddrsAdm";
            this.ucAddrsAdm.Size = new System.Drawing.Size(582, 300);
            this.ucAddrsAdm.TabIndex = 0;
            this.ucAddrsAdm.UCSelectedItemChanged += new UCAddrs.UCSelectedItemChangedEventHandler(this.ucAddrs_UCSelectedItemChanged);
            this.ucAddrsAdm.UCAddNewItem += new UCAddrs.UCAddNewItemEventHandler(this.ucAddrs_UCAddNewItem);
            // 
            // ucAddrsOrg
            // 
            this.ucAddrsOrg.AddrTypeIdIn = 40;
            this.ucAddrsOrg.AddrTypeIdNotIn = null;
            this.ucAddrsOrg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucAddrsOrg.Location = new System.Drawing.Point(3, 3);
            this.ucAddrsOrg.Name = "ucAddrsOrg";
            this.ucAddrsOrg.Size = new System.Drawing.Size(582, 300);
            this.ucAddrsOrg.TabIndex = 1;
            this.ucAddrsOrg.UCSelectedItemChanged += new FERHRI.Social.UCAddrs.UCSelectedItemChangedEventHandler(this.ucAddrsOrg_UCSelectedItemChanged);
            this.ucAddrsOrg.UCAddNewItem += new UCAddrs.UCAddNewItemEventHandler(this.ucAddrsOrg_UCAddNewItem);
            // 
            // ucAddrsAll
            // 
            this.ucAddrsAll.AddrTypeIdIn = null;
            this.ucAddrsAll.AddrTypeIdNotIn = null;
            this.ucAddrsAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucAddrsAll.Location = new System.Drawing.Point(0, 0);
            this.ucAddrsAll.Name = "ucAddrsAll";
            this.ucAddrsAll.Size = new System.Drawing.Size(588, 306);
            this.ucAddrsAll.TabIndex = 2;
            this.ucAddrsAll.UCSelectedItemChanged += new FERHRI.Social.UCAddrs.UCSelectedItemChangedEventHandler(this.ucAddrsAll_UCSelectedItemChanged);
            this.ucAddrsAll.UCAddNewItem += new FERHRI.Social.UCAddrs.UCAddNewItemEventHandler(this.ucAddrsAll_UCAddNewItem);
            // 
            // FormAddrs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 540);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormAddrs";
            this.Text = "Страны, регионы, организации...";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tabControlAdm.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UCAddrs ucAddrsAdm;
        private UCAddr ucAddr;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TabControl tabControlAdm;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private UCAddrs ucAddrsOrg;
        private System.Windows.Forms.TabPage tabPage3;
        private UCAddrs ucAddrsAll;
    }
}