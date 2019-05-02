using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SOV.Common
{
    public partial class UCComboBox : UserControl
    {

        public UCComboBox()
        {
            InitializeComponent();
        }

        public void Clear()
        {
            idNamesBindingSource.Clear();
        }
        public void DataSource(object[] idNamesItems)
        {
            idNamesBindingSource.DataSource = idNamesItems;
            comboBox.SelectedIndex = -1;
        }

        public int Count { get { return idNamesBindingSource.Count; } }

        public bool Checked
        {
            get
            {
                return checkBox.Checked;
            }
            set
            {
                checkBox.Checked = comboBox.Enabled = value;
            }
        }
        public bool ShowCheckBox
        {
            get
            {
                return checkBox.Visible;
            }
            set
            {
                checkBox.Visible = value;
            }
        }
        public void SetSelectedId(int id)
        {
            comboBox.SelectedIndex = -1;
            for (int i = 0; i < idNamesBindingSource.Count; i++)
            {
                if (((IdClass)idNamesBindingSource[i]).Id == id)
                {
                    comboBox.SelectedIndex = i;
                    break;
                    //idClassBindingSource.Position = i;
                }
            }
        }

        public object SelectedItem
        {
            set
            {
                if (value != null)
                    SetSelectedId(((IdClass)value).Id);
                else
                    comboBox.SelectedIndex = -1;
            }
            get
            {
                return comboBox.SelectedItem == null ? null : comboBox.SelectedItem;
            }
        }

        private void UCComboBoxIdNames_Load(object sender, EventArgs e)
        {
            Checked = true;
            ShowCheckBox = false;
        }

        //#region EVENTS
        //public delegate void SelectedIndexChangedEventHandler(int i);
        //public event SelectedIndexChangedEventHandler SelectedIndexChangedEvent;
        //protected virtual void RaiseSelectedIndexChangedEvent(int i)
        //{
        //    if (SelectedIndexChangedEvent != null)
        //    {
        //        SelectedIndexChangedEvent(i);
        //    }
        //}

        //#endregion
    }
}
