namespace SOV.Amur.Meta
{
    partial class FormVariablesList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormVariablesList));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ucVariablesList = new SOV.Amur.Meta.UCVariablesList();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ucVariable = new SOV.Amur.Meta.UCVariable();
            this.button1 = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.newButton = new System.Windows.Forms.Button();
            this.new1Button = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ucVariableAttributes = new SOV.Amur.Meta.UCVariableAttributes();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.ucVariablesList, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.button1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.saveButton, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.newButton, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.new1Button, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(920, 538);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // ucVariablesList
            // 
            this.ucVariablesList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucVariablesList.Location = new System.Drawing.Point(3, 3);
            this.ucVariablesList.Name = "ucVariablesList";
            this.tableLayoutPanel1.SetRowSpan(this.ucVariablesList, 3);
            this.ucVariablesList.ShowToolBox = true;
            this.ucVariablesList.Size = new System.Drawing.Size(562, 503);
            this.ucVariablesList.TabIndex = 0;
            this.ucVariablesList.UCVariableChangedEvent += new SOV.Amur.Meta.UCVariablesList.UCVariableChangedEventHandler(this.ucVariablesList_UCVariableChangedEvent);
            // 
            // groupBox1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox1, 3);
            this.groupBox1.Controls.Add(this.ucVariable);
            this.groupBox1.Location = new System.Drawing.Point(571, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(346, 312);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Переменная";
            // 
            // ucVariable
            // 
            this.ucVariable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucVariable.Filter = null;
            this.ucVariable.Location = new System.Drawing.Point(3, 16);
            this.ucVariable.Name = "ucVariable";
            this.ucVariable.Size = new System.Drawing.Size(340, 293);
            this.ucVariable.TabIndex = 1;
            this.ucVariable.Variable = null;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Left;
            this.button1.Location = new System.Drawing.Point(3, 512);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Закрыть";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // saveButton
            // 
            this.saveButton.Image = ((System.Drawing.Image)(resources.GetObject("saveButton.Image")));
            this.saveButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.saveButton.Location = new System.Drawing.Point(571, 321);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(88, 23);
            this.saveButton.TabIndex = 4;
            this.saveButton.Text = "Сохранить";
            this.saveButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // newButton
            // 
            this.newButton.Image = ((System.Drawing.Image)(resources.GetObject("newButton.Image")));
            this.newButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.newButton.Location = new System.Drawing.Point(665, 321);
            this.newButton.Name = "newButton";
            this.newButton.Size = new System.Drawing.Size(80, 23);
            this.newButton.TabIndex = 5;
            this.newButton.Text = "Создать";
            this.newButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.newButton.UseVisualStyleBackColor = true;
            this.newButton.Click += new System.EventHandler(this.newButton_Click);
            // 
            // new1Button
            // 
            this.new1Button.Image = ((System.Drawing.Image)(resources.GetObject("new1Button.Image")));
            this.new1Button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.new1Button.Location = new System.Drawing.Point(751, 321);
            this.new1Button.Name = "new1Button";
            this.new1Button.Size = new System.Drawing.Size(127, 23);
            this.new1Button.TabIndex = 6;
            this.new1Button.Text = "Создать на основе";
            this.new1Button.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.new1Button.UseVisualStyleBackColor = true;
            this.new1Button.Click += new System.EventHandler(this.new1Button_Click);
            // 
            // groupBox2
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox2, 3);
            this.groupBox2.Controls.Add(this.ucVariableAttributes);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(571, 350);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(346, 121);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Атрибуты переменной";
            // 
            // ucVariableAttributes
            // 
            this.ucVariableAttributes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucVariableAttributes.Location = new System.Drawing.Point(3, 16);
            this.ucVariableAttributes.Name = "ucVariableAttributes";
            this.ucVariableAttributes.Size = new System.Drawing.Size(340, 102);
            this.ucVariableAttributes.TabIndex = 0;
            // 
            // FormVariablesList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 538);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormVariablesList";
            this.Text = "Список параметров/переменных";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UCVariablesList ucVariablesList;
        private UCVariable ucVariable;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button newButton;
        private System.Windows.Forms.Button new1Button;
        private System.Windows.Forms.GroupBox groupBox2;
        private UCVariableAttributes ucVariableAttributes;
    }
}