using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOV.Amur.Meta
{
    public partial class FormCategorySetList : Form
    {
        public FormCategorySetList(EnumLanguage lang)
        {
            InitializeComponent();

            ucCategoryDefinition.Language = (int)lang;
            ucCategoryDefinition.Fill();
        }
    }
}
