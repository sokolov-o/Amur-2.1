using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOV.Social
{
    public partial class UCStaffPositions : UserControl
    {
        public UCStaffPositions()
        {
            InitializeComponent();
        }
        public void Fill()
        {
            staffPositionBindingSource.DataSource = DataManager.GetInstance().StaffPositionRepository.Select().OrderBy(x => x.NameRusShort).ToList();
        }

        public StaffPosition StaffPositionSelected
        {
            get
            {
                if (staffPositionBindingSource != null && staffPositionBindingSource.Current != null)
                {
                    return (StaffPosition)staffPositionBindingSource.Current;
                }
                return null;
            }
        }
        public bool UCShowNameRusOnly
        {
            set
            {
                foreach (DataGridViewColumn item in dgv.Columns)
                {
                    if (value)
                        item.Visible = (item.Name == "id" || item.Name == "nameRus") ? true : false;
                    else
                        item.Visible = true;
                }
            }
        }
        public delegate void UCMouseDoubleClickEventHandler();
        public event UCMouseDoubleClickEventHandler UCMouseDoubleClickEvent;
        private void dgv_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (UCMouseDoubleClickEvent != null)
            {
                UCMouseDoubleClickEvent();
            }
        }
    }
}
