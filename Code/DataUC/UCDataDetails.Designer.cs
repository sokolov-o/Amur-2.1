namespace SOV.Amur.Data
{
    partial class UCDataDetails
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
            this.ucDataAQC = new SOV.Amur.Data.UCDataAQC();
            this.ucDataValueList = new SOV.Amur.Data.UCDataValueList();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.ucDerived = new SOV.Amur.Data.UCDerived();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucDataAQC
            // 
            this.ucDataAQC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDataAQC.Location = new System.Drawing.Point(0, 0);
            this.ucDataAQC.Name = "ucDataAQC";
            this.ucDataAQC.Size = new System.Drawing.Size(346, 91);
            this.ucDataAQC.TabIndex = 0;
            // 
            // ucDataValueList
            // 
            this.ucDataValueList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDataValueList.Location = new System.Drawing.Point(0, 0);
            this.ucDataValueList.Name = "ucDataValueList";
            this.ucDataValueList.Size = new System.Drawing.Size(346, 178);
            this.ucDataValueList.TabIndex = 1;
            this.ucDataValueList.UCCurrentDataValueChangedEvent += new SOV.Amur.Data.UCDataValueList.UCCurrentDataValueChangedEventHandler(this.ucDataValueList_UCCurrentDataValueChangedEvent);
            this.ucDataValueList.UCCurrentDataValueActualizedEvent += new SOV.Amur.Data.UCDataValueList.UCCurrentDataValueActualizedEventHandler(this.ucDataValueList_UCCurrentDataValueActualizedEvent);
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
            this.splitContainer1.Panel1.Controls.Add(this.ucDataValueList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(346, 421);
            this.splitContainer1.SplitterDistance = 178;
            this.splitContainer1.TabIndex = 2;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.ucDataAQC);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.ucDerived);
            this.splitContainer2.Size = new System.Drawing.Size(346, 239);
            this.splitContainer2.SplitterDistance = 91;
            this.splitContainer2.TabIndex = 1;
            // 
            // ucDerived
            // 
            this.ucDerived.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDerived.Location = new System.Drawing.Point(0, 0);
            this.ucDerived.Name = "ucDerived";
            this.ucDerived.ShowDerived = true;
            this.ucDerived.ShowParent = true;
            this.ucDerived.Size = new System.Drawing.Size(346, 144);
            this.ucDerived.TabIndex = 0;
            // 
            // UCDataDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "UCDataDetails";
            this.Size = new System.Drawing.Size(346, 421);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UCDataAQC ucDataAQC;
        private UCDataValueList ucDataValueList;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private UCDerived ucDerived;
    }
}
