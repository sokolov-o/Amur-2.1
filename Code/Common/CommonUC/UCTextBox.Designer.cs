namespace SOV.Common
{
    partial class UCTextBox
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
            this.textBox = new System.Windows.Forms.TextBox();
            this.editButton = new System.Windows.Forms.Button();
            this.nullButton = new System.Windows.Forms.Button();
            this.tlp.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlp
            // 
            this.tlp.ColumnCount = 3;
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp.Controls.Add(this.textBox, 0, 0);
            this.tlp.Controls.Add(this.editButton, 1, 0);
            this.tlp.Controls.Add(this.nullButton, 2, 0);
            this.tlp.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlp.Location = new System.Drawing.Point(0, 0);
            this.tlp.Margin = new System.Windows.Forms.Padding(0);
            this.tlp.Name = "tlp";
            this.tlp.RowCount = 1;
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp.Size = new System.Drawing.Size(214, 20);
            this.tlp.TabIndex = 0;
            // 
            // textBox
            // 
            this.textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox.Location = new System.Drawing.Point(0, 0);
            this.textBox.Margin = new System.Windows.Forms.Padding(0);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(174, 20);
            this.textBox.TabIndex = 0;
            this.textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // editButton
            // 
            this.editButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editButton.Image = global::SOV.Common.Properties.Resources.edit_data;
            this.editButton.Location = new System.Drawing.Point(174, 0);
            this.editButton.Margin = new System.Windows.Forms.Padding(0);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(20, 20);
            this.editButton.TabIndex = 1;
            this.editButton.TabStop = false;
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // nullButton
            // 
            this.nullButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nullButton.Image = global::SOV.Common.Properties.Resources.delete;
            this.nullButton.Location = new System.Drawing.Point(194, 0);
            this.nullButton.Margin = new System.Windows.Forms.Padding(0);
            this.nullButton.Name = "nullButton";
            this.nullButton.Size = new System.Drawing.Size(20, 20);
            this.nullButton.TabIndex = 2;
            this.nullButton.TabStop = false;
            this.nullButton.UseVisualStyleBackColor = true;
            this.nullButton.Click += new System.EventHandler(this.nullButton_Click);
            // 
            // UCTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlp);
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "UCTextBox";
            this.Size = new System.Drawing.Size(214, 20);
            this.tlp.ResumeLayout(false);
            this.tlp.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlp;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Button nullButton;
    }
}
