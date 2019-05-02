namespace SOV.Amur.Meta
{
    partial class FormMethods
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
            SOV.Amur.Meta.Method method4 = new SOV.Amur.Meta.Method();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMethods));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.ucMethodTree = new SOV.Common.UCTreeIParent();
            this.methodTLP = new System.Windows.Forms.TableLayoutPanel();
            this.saveMethodButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ucMethod = new SOV.Amur.Meta.UCMethod();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.methodTLP.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 468F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(686, 468);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.SteelBlue;
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.SteelBlue;
            this.splitContainer1.Panel2.Controls.Add(this.methodTLP);
            this.splitContainer1.Size = new System.Drawing.Size(686, 468);
            this.splitContainer1.SplitterDistance = 214;
            this.splitContainer1.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.toolStrip2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.ucMethodTree, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(212, 466);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackColor = System.Drawing.SystemColors.Info;
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip2.Size = new System.Drawing.Size(212, 25);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripLabel2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(51, 22);
            this.toolStripLabel2.Text = "Методы";
            // 
            // ucMethodTree
            // 
            this.ucMethodTree.ContextMenuStrip4Types = null;
            this.ucMethodTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMethodTree.Location = new System.Drawing.Point(0, 25);
            this.ucMethodTree.Margin = new System.Windows.Forms.Padding(0);
            this.ucMethodTree.Name = "ucMethodTree";
            this.ucMethodTree.ShowAddButton = true;
            this.ucMethodTree.ShowCloneButton = true;
            this.ucMethodTree.ShowDeleteButton = true;
            this.ucMethodTree.ShowEditButton = false;
            this.ucMethodTree.ShowFindTextBox = true;
            this.ucMethodTree.ShowRefreshButton = true;
            this.ucMethodTree.ShowToolStrip = true;
            this.ucMethodTree.Size = new System.Drawing.Size(212, 441);
            this.ucMethodTree.TabIndex = 1;
            this.ucMethodTree.UCSelectedItemChanged += new SOV.Common.UCTreeIParent.UCSelectedItemChangedEventHandler(this.methodTree_UCSelectedItemChanged);
            this.ucMethodTree.UCRefresh += new SOV.Common.UCTreeIParent.UCRefreshEventHandler(this.methodTree_UCRefresh);
            this.ucMethodTree.UCAddNewItem += new SOV.Common.UCTreeIParent.UCAddItemEventHandler(this.ucMethodTree_UCAddNewItem);
            this.ucMethodTree.UCDeleteItem += new SOV.Common.UCTreeIParent.UCDeleteItemEventHandler(this.ucMethodTree_UCDeleteItem);
            this.ucMethodTree.UCCloneItem += new SOV.Common.UCTreeIParent.UCCloneItemEventHandler(this.ucMethodTree_UCCloneItem);
            // 
            // methodTLP
            // 
            this.methodTLP.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.methodTLP.ColumnCount = 1;
            this.methodTLP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.methodTLP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.methodTLP.Controls.Add(this.saveMethodButton, 0, 1);
            this.methodTLP.Controls.Add(this.groupBox1, 0, 0);
            this.methodTLP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.methodTLP.Location = new System.Drawing.Point(0, 0);
            this.methodTLP.Margin = new System.Windows.Forms.Padding(0);
            this.methodTLP.Name = "methodTLP";
            this.methodTLP.RowCount = 2;
            this.methodTLP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.methodTLP.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.methodTLP.Size = new System.Drawing.Size(466, 466);
            this.methodTLP.TabIndex = 0;
            // 
            // saveMethodButton
            // 
            this.saveMethodButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.saveMethodButton.Image = global::SOV.Amur.Meta.Properties.Resources.SaveBlack;
            this.saveMethodButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.saveMethodButton.Location = new System.Drawing.Point(3, 440);
            this.saveMethodButton.Name = "saveMethodButton";
            this.saveMethodButton.Size = new System.Drawing.Size(460, 23);
            this.saveMethodButton.TabIndex = 1;
            this.saveMethodButton.Text = "Сохранить";
            this.saveMethodButton.UseVisualStyleBackColor = true;
            this.saveMethodButton.Click += new System.EventHandler(this.saveMethodButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.ucMethod);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(460, 431);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Метод";
            // 
            // ucMethod
            // 
            this.ucMethod.BackColor = System.Drawing.SystemColors.Control;
            this.ucMethod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMethod.Location = new System.Drawing.Point(3, 16);
            this.ucMethod.Margin = new System.Windows.Forms.Padding(0);
            method4.Description = "";
            method4.Id = -1;
            method4.MethodOutputStoreParameters = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("method4.MethodOutputStoreParameters")));
            method4.Name = "";
            method4.Order = ((short)(32767));
            method4.ParentId = null;
            method4.SourceLegalEntityId = null;
            this.ucMethod.Method = method4;
            this.ucMethod.Name = "ucMethod";
            this.ucMethod.Size = new System.Drawing.Size(454, 412);
            this.ucMethod.TabIndex = 0;
            // 
            // FormMethods
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 468);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FormMethods";
            this.Text = "Методы наблюдений, прогнозов и др.";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.methodTLP.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private Common.UCTreeIParent ucMethodTree;
        private System.Windows.Forms.TableLayoutPanel methodTLP;
        private UCMethod ucMethod;
        private System.Windows.Forms.Button saveMethodButton;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}