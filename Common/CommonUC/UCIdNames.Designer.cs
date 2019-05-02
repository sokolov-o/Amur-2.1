namespace SOV.Common
{
    partial class UCIdNames
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
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
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.tlp = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.nameRusTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nameEngTextBox = new System.Windows.Forms.TextBox();
            this.nameRusShortTextBox = new System.Windows.Forms.TextBox();
            this.nameEngShortTextBox = new System.Windows.Forms.TextBox();
            this.tlp.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlp
            // 
            this.tlp.BackColor = System.Drawing.SystemColors.Control;
            this.tlp.ColumnCount = 2;
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tlp.Controls.Add(this.label2, 0, 2);
            this.tlp.Controls.Add(this.nameRusTextBox, 0, 1);
            this.tlp.Controls.Add(this.label1, 0, 0);
            this.tlp.Controls.Add(this.nameEngTextBox, 0, 3);
            this.tlp.Controls.Add(this.nameRusShortTextBox, 1, 0);
            this.tlp.Controls.Add(this.nameEngShortTextBox, 1, 2);
            this.tlp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp.Location = new System.Drawing.Point(2, 2);
            this.tlp.Margin = new System.Windows.Forms.Padding(1);
            this.tlp.Name = "tlp";
            this.tlp.RowCount = 4;
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp.Size = new System.Drawing.Size(363, 192);
            this.tlp.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(1, 96);
            this.label2.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(164, 22);
            this.label2.TabIndex = 3;
            this.label2.Text = "Английский (краткое, полное):";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nameRusTextBox
            // 
            this.nameRusTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tlp.SetColumnSpan(this.nameRusTextBox, 2);
            this.nameRusTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nameRusTextBox.Location = new System.Drawing.Point(1, 23);
            this.nameRusTextBox.Margin = new System.Windows.Forms.Padding(1);
            this.nameRusTextBox.Multiline = true;
            this.nameRusTextBox.Name = "nameRusTextBox";
            this.nameRusTextBox.Size = new System.Drawing.Size(361, 72);
            this.nameRusTextBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(1, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Русский (краткое,полное):";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nameEngTextBox
            // 
            this.nameEngTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tlp.SetColumnSpan(this.nameEngTextBox, 2);
            this.nameEngTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nameEngTextBox.Location = new System.Drawing.Point(1, 119);
            this.nameEngTextBox.Margin = new System.Windows.Forms.Padding(1);
            this.nameEngTextBox.Multiline = true;
            this.nameEngTextBox.Name = "nameEngTextBox";
            this.nameEngTextBox.Size = new System.Drawing.Size(361, 72);
            this.nameEngTextBox.TabIndex = 5;
            // 
            // nameRusShortTextBox
            // 
            this.nameRusShortTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nameRusShortTextBox.Location = new System.Drawing.Point(167, 1);
            this.nameRusShortTextBox.Margin = new System.Windows.Forms.Padding(1);
            this.nameRusShortTextBox.MaxLength = 50;
            this.nameRusShortTextBox.Name = "nameRusShortTextBox";
            this.nameRusShortTextBox.Size = new System.Drawing.Size(195, 20);
            this.nameRusShortTextBox.TabIndex = 1;
            // 
            // nameEngShortTextBox
            // 
            this.nameEngShortTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nameEngShortTextBox.Location = new System.Drawing.Point(167, 97);
            this.nameEngShortTextBox.Margin = new System.Windows.Forms.Padding(1);
            this.nameEngShortTextBox.MaxLength = 50;
            this.nameEngShortTextBox.Name = "nameEngShortTextBox";
            this.nameEngShortTextBox.Size = new System.Drawing.Size(195, 20);
            this.nameEngShortTextBox.TabIndex = 4;
            // 
            // UCIdNames
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Controls.Add(this.tlp);
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "UCIdNames";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.Size = new System.Drawing.Size(367, 196);
            this.Load += new System.EventHandler(this.UCIdNames_Load);
            this.tlp.ResumeLayout(false);
            this.tlp.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlp;
        private System.Windows.Forms.TextBox nameEngShortTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox nameRusShortTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nameRusTextBox;
        private System.Windows.Forms.TextBox nameEngTextBox;
    }
}
