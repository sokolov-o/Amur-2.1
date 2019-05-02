namespace SOV.Amur.Meta
{
    partial class UCMethodForecast
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
            this.ucMethod = new SOV.Common.UCTextBox();
            this.leadTimeUnitUC = new SOV.Common.UCTextBox();
            this.leadTimesTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dateIniTextBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucMethod
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.ucMethod, 2);
            this.ucMethod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMethod.Enabled = false;
            this.ucMethod.Location = new System.Drawing.Point(49, 1);
            this.ucMethod.Margin = new System.Windows.Forms.Padding(1);
            this.ucMethod.Name = "ucMethod";
            this.ucMethod.ShowEditButton = false;
            this.ucMethod.ShowNullButton = false;
            this.ucMethod.Size = new System.Drawing.Size(329, 20);
            this.ucMethod.TabIndex = 0;
            this.ucMethod.Value = null;
            // 
            // leadTimeUnitUC
            // 
            this.leadTimeUnitUC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leadTimeUnitUC.Location = new System.Drawing.Point(213, 23);
            this.leadTimeUnitUC.Margin = new System.Windows.Forms.Padding(1);
            this.leadTimeUnitUC.Name = "leadTimeUnitUC";
            this.leadTimeUnitUC.ShowEditButton = true;
            this.leadTimeUnitUC.ShowNullButton = false;
            this.leadTimeUnitUC.Size = new System.Drawing.Size(165, 20);
            this.leadTimeUnitUC.TabIndex = 1;
            this.leadTimeUnitUC.Value = null;
            this.leadTimeUnitUC.UCEditButtonPressedEvent += new SOV.Common.UCTextBox.UCEditButtonPressedEventHandler(this.leadTimeUnitUC_UCEditButtonPressedEvent);
            // 
            // leadTimesTextBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.leadTimesTextBox, 3);
            this.leadTimesTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leadTimesTextBox.Location = new System.Drawing.Point(3, 60);
            this.leadTimesTextBox.Multiline = true;
            this.leadTimesTextBox.Name = "leadTimesTextBox";
            this.leadTimesTextBox.Size = new System.Drawing.Size(373, 61);
            this.leadTimesTextBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 22);
            this.label1.TabIndex = 3;
            this.label1.Text = "Метод:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label2, 2);
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(206, 22);
            this.label2.TabIndex = 4;
            this.label2.Text = "Время шага заблаговременности:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label3, 3);
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(373, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Прогностические заблаговременности (шаги, через ;):";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label4, 2);
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(206, 26);
            this.label4.TabIndex = 7;
            this.label4.Text = "Часы выпуска прогноза (UTC, через ;):";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dateIniTextBox
            // 
            this.dateIniTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dateIniTextBox.Location = new System.Drawing.Point(215, 127);
            this.dateIniTextBox.Name = "dateIniTextBox";
            this.dateIniTextBox.Size = new System.Drawing.Size(161, 20);
            this.dateIniTextBox.TabIndex = 6;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.ucMethod, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dateIniTextBox, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.leadTimesTextBox, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.leadTimeUnitUC, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(379, 150);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // UCMethodForecast
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UCMethodForecast";
            this.Size = new System.Drawing.Size(379, 150);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Common.UCTextBox ucMethod;
        private Common.UCTextBox leadTimeUnitUC;
        private System.Windows.Forms.TextBox leadTimesTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox dateIniTextBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
