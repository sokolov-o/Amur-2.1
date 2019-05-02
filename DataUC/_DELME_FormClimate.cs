using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SOV.Amur.Meta;

namespace SOV.Amur.Data
{
    public partial class _DELME_FormClimate : Form
    {
        public _DELME_FormClimate()
        {
            InitializeComponent();

            Text = "Климатические данные";
        }
        public void Fill(List<int> sites, int[] yearSF)
        {
            ucDataTableClimate.Fill(sites, yearSF);
        }
    }
}
