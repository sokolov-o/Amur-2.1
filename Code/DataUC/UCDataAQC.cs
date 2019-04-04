using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SOV.Amur.DataP;

namespace SOV.Amur.Data
{
    public partial class UCDataAQC : UserControl
    {
        public UCDataAQC()
        {
            InitializeComponent();
        }

        List<AQCRole> _aqcRoles = null;

        public void Fill(long dataValueId)
        {
            Clear();
            DataP.DataManager dm = DataP.DataManager.GetInstance();
            List<AQCDataValue> aqc = dm.AQCRepository.SelectDataValueAQC(dataValueId);
            if (_aqcRoles == null) _aqcRoles = dm.AQCRepository.SelectRoles();

            foreach (var item in aqc)
            {
                DataGridViewRow row = dgv.Rows[dgv.Rows.Add()];
                row.Cells[0].Value = item.IsAQCApplied;
                row.Cells[1].Value = _aqcRoles.First(x => x.Id == item.AQCRoleId).RoleDescription;
            }
        }

        internal void Clear()
        {
            dgv.Rows.Clear();
        }
    }
}
