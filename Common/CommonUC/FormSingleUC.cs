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
    public partial class FormSingleUC : Form
    {
        public FormSingleUC(UserControl uc, string title = "", int width = 300, int height = 300)
        {
            InitializeComponent();

            Text = title;
            uc.Dock = DockStyle.Fill;
            Controls.Add(uc);
            StartPosition = FormStartPosition.CenterScreen;
            Width = width;
            Height = height;
        }
    }
}
