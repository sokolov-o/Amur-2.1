using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOV.Amur.Parser
{
    public partial class UCSysObj : UserControl
    {
        public UCSysObj()
        {
            InitializeComponent();
        }
        public SOV.Amur.Parser.DataManager ParserDM { get; set; }
        public SOV.Amur.Meta.DataManager MetaDM { get; set; }

        public SysObj SysObj
        {
            set
            {
                if (value == null)
                {
                    idTextBox.Text = nameTxtBox.Text = heapTextBox.Text = notesTextBox.Text = lastStartParamTextBox.Text = string.Empty;
                    sysObjTypeComboBox.SelectedIndex = 0;
                }
                else
                {
                    idTextBox.Text = value.Id.ToString();
                    nameTxtBox.Text = value.Name;
                    heapTextBox.Text = value.Heap;
                    notesTextBox.Text = value.Notes;
                    lastStartParamTextBox.Text = value.LastStartParam;

                    sysObjTypeComboBox.SelectedIndex = -1;
                    for (int i = 0; i < sysObjTypeComboBox.Items.Count; i++)
                    {
                        if (((KeyValuePair<int, string>)(sysObjTypeComboBox.Items[i])).Key == value.SysObjTypeId)
                            sysObjTypeComboBox.SelectedIndex = i;
                    }
                }
            }
        }

        private void UCSysObj_Load(object sender, EventArgs e)
        {
            if (ParserDM != null)
            {
                foreach (var obj in ParserDM.SysObjTypeRepository.Select())
                {
                    sysObjTypeComboBox.Items.Add(obj);
                }
            }
        }
    }
}
