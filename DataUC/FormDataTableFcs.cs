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
    public partial class FormDataTableFcs : Form
    {
        public FormDataTableFcs(Sys.EntityInstanceValue dataFilterSAV)
        {
            InitializeComponent();
            ucDataFcsTable.DataFilterSAV = dataFilterSAV;
        }

        private void FormDataTableFcs_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Установите фильтр, выбрав соответствующую иконку на панели управления.", "Сначала!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //DataFilter df = new DataFilter(
            //    DateTime.Now.AddDays(-2), DateTime.Now.AddDays(5),
            //    null, true, false, true, 
            //    new CatalogFilter(new List<int>(new int[] { 1 }),
            //        new List<int>(new int[] { 1 }),
            //        new List<int>(new int[] { 100 }),
            //        new List<int>(new int[] { 0 }),
            //        new List<int>(new int[] { 0 }),
            //        0)
            //);
            //ucDataFcsTable.Fill(df);
        }
    }
}
