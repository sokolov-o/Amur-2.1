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
    public partial class FormAddrs : Form
    {
        public FormAddrs()
        {
            InitializeComponent();
        }

        private void ucAddrs_UCSelectedItemChanged(Addr ar)
        {
            ucAddr.Addr = ar;
        }

        private void ucAddrs_UCAddNewItem()
        {
            ucAddr.Addr = new Addr() { Id = -1, TypeId = ucAddrs.GetSelectedAddr().TypeId, ParentId = ucAddrs.GetSelectedAddr().Id };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Addr ar = ucAddr.Addr;
            if (string.IsNullOrEmpty(ar.Name)) return;

            if (ar.Id < 0)
                ar.Id = DataManager.GetInstance().AddrRepository.Insert(ar);
            else
                DataManager.GetInstance().AddrRepository.Update(ar);

            ucAddrs.Fill(ar.Id);
            ucAddrs.Focus();
            ucAddr.Addr = ucAddrs.GetSelectedAddr();
        }
    }
}
