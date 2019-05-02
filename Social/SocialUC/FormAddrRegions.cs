using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FERHRI.Social
{
    public partial class FormAddrs : Form
    {
        public FormAddrs()
        {
            InitializeComponent();
        }

        private void ucAddr1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ucAddrs_UCSelectedItemChanged(Addr ar)
        {
            ucAddr.Addr = ar;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Addr ar = ucAddr.Addr;
            if (string.IsNullOrEmpty(ar.Name)) return;

            if (ar.Id < 0)
                ar.Id = DataManager.GetInstance().AddrRepository.Insert(ar);
            else
                DataManager.GetInstance().AddrRepository.Update(ar);

            UCAddrs uc = ucAddrsAdm.Focused ? ucAddrsAdm : ucAddrsOrg.Focused ? ucAddrsOrg : ucAddrsAll;

            uc.Fill(ar.Id);
            uc.Focus();
            ucAddr.Addr = uc.GetSelectedAddr();
        }

        private void ucAddrs_UCAddNewItem()
        {
            ucAddr.Addr = new Addr(-1, ucAddrsAdm.GetSelectedAddr().TypeId, null, null, ucAddrsAdm.GetSelectedAddr().Id);
        }

        private void ucAddrsOrg_UCSelectedItemChanged(Addr ar)
        {
            ucAddr.Addr = ar;
        }

        private void ucAddrsOrg_UCAddNewItem()
        {
            Addr ar = ucAddrsOrg.GetSelectedAddr();
            ucAddr.Addr = new Addr(-1, ar == null ? (int)Enums.AddrType.Organization : ar.TypeId, null, null,
                ar == null ? null : (int?)ar.Id);
        }

        private void ucAddrsAll_UCSelectedItemChanged(Addr ar)
        {
            ucAddr.Addr = ar;
        }

        private void ucAddrsAll_UCAddNewItem()
        {
            ucAddr.Addr = new Addr(-1, ucAddrsAll.GetSelectedAddr().TypeId, null, null, ucAddrsAll.GetSelectedAddr().Id);
        }
    }
}
