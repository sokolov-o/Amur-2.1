using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FERHRI.Amur.Meta;

namespace FERHRI.Amur.Data
{
    public partial class FormClimate4Site : Form
    {
        public Site Site { get; set; }
        public int[] YearSF { get; set; }

        public FormClimate4Site(Site site, int[] yearSF)
        {
            InitializeComponent();

            this.Site = site;
            YearSF = yearSF;

            Text = "Климатические данные " + site.GetName(StationRepository.GetCash(), StationTypeRepository.GetCash(), 1);
        }
        public void Fill()
        {
            ucDataTableClimate.Fill(Site.Id, YearSF);
        }

        private void FormClimate_Load(object sender, EventArgs e)
        {
            Fill();
        }
    }
}
