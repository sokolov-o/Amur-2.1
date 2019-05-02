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
    public partial class FormSelectStaffPosition : Form
    {
        public FormSelectStaffPosition()
        {
            InitializeComponent();

            ucStaffPositions.Fill();
        }
        public StaffPosition StaffPositionSelected
        {
            get
            {
                return ucStaffPositions.StaffPositionSelected;
            }
        }
        public bool ShowNameRusOnly
        {
            set
            {
                ucStaffPositions.UCShowNameRusOnly = true;
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

        private void ucStaffPositions_UCMouseDoubleClickEvent()
        {
            button1_Click(null, null);
        }
    }
}
