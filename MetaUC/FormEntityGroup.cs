using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOV.Amur.Meta
{
    public partial class FormEntityGroup : Form
    {
        public FormEntityGroup()
        {
            InitializeComponent();
        }

        private void ucEntityGroup1_Load(object sender, EventArgs e)
        {

        }

        private void ucEntityGroup1_UCSelectedItemChanged(EntityGroup eg)
        {
            Text = "Группа элементов справочников [" + eg.Name + " - " + eg.Id + "]";
        }
    }
}
