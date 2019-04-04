using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOV.Amur.Parser
{
    public partial class FormParsers : Form
    {
        public FormParsers(SOV.Amur.Parser.DataManager parserDM, SOV.Amur.Meta.DataManager metaDM)
        {
            InitializeComponent();

            sysObjBindingSource.DataSource = parserDM.SysObjRepository.Select();

            ucParsers.ParserDM = parserDM;
            ucParsers.MetaDM = metaDM;

            objListBox_SelectedIndexChanged(null, null);
        }

        private void objListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ucParsers.SysObj = (SysObj)objListBox.SelectedItem;
        }

        private void saveParserButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not worked now... OSokolov 201612");
        }
    }
}
