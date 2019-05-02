using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOV.Common
{
    public partial class FormExceptionMessage : Form
    {
        public FormExceptionMessage()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            textLabel.MaximumSize = new Size(Width - 50, 0);
        }

        public FormExceptionMessage(string mainText, string details = null) : this()
        {
            textLabel.Text = mainText ?? (details ?? "Пустое сообщение об ошибке");
            if (mainText != null && details != null)
                toolTip.SetToolTip(textLabel, details);
        }

        public FormExceptionMessage(RuDbException e) : this(e.RuMessage, e.Message) {}

        public FormExceptionMessage(Exception e) : this(e.Message) {}

        private void button_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
