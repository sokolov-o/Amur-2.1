using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOV.Social
{
    public partial class FormSelectLegalEntities : Form
    {
        public FormSelectLegalEntities(Enums.LegalEntityType leType)
        {
            InitializeComponent();

            ucLegalEntitiesTree.ShowDeleteButton = false;
            ucLegalEntitiesTree.ShowAddNewButton= false;
            ucLegalEntitiesTree.FillByType(leType);
        }
        public LegalEntity LegalEntitySelected
        {
            get
            {
                return ucLegalEntitiesTree.LegalEntitySelected;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ucLegalEntitiesTree_UCDoubleClickEvent()
        {
            button1_Click(null, null);
        }
    }
}
