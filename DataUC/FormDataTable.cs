using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SOV.Amur.Sys;

namespace SOV.Amur.Data
{
    public partial class FormDataTable : Form
    {
        public FormDataTable(int userOrganisationId,
            EntityInstanceValue dataFilterSAV = null,
            Sys.EntityInstanceValue userDirExportSAV = null
            )
        {
            InitializeComponent();

            ucDataTable.FormDataFilter = new FormDataFilter(dataFilterSAV);
            ucDataTable.UserDirExportSAV = userDirExportSAV;
            ucDataTable.UserOrganisationId= userOrganisationId;
        }
        public UCDataTable UCDataTable
        {
            get
            {
                return ucDataTable;
            }
        }
    }
}
