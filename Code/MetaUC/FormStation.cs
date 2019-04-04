using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FERHRI.Amur.Meta
{
    public partial class FormStation : Form
    {
        public FormStation()
        {
            InitializeComponent();

            Text = "НОВЫЙ ПОСТ/СТАНЦИЯ";
            ucStationEdit.EnableSites = false;
            ucStationEdit.Station = new Station(-1, null, null, (int)EnumStationType.HydroPost, null, null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
