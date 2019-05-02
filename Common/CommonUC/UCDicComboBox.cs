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
    public partial class UCDicComboBox : UserControl
    {
        List<DicItem> _dic = new List<DicItem>();
        public UCDicComboBox()
        {
            InitializeComponent();
        }

        public List<DicItem> GetDicCopy()
        {
            List<DicItem> ret = new List<DicItem>();
            foreach (var item in _dic)
            {
                ret.Add(item);
            }
            return ret;
        }
        public void Clear()
        {
            comboBox1.Items.Clear();
            comboBox1.Text = string.Empty;
            _dic.Clear();
        }
        public int Count
        {
            get
            {
                return _dic.Count;
            }
        }
        public void AddRange(List<DicItem> dic)
        {
            _dic.AddRange(dic);

            foreach (var item in dic)
            {
                comboBox1.Items.Add(item);
            }
        }
        public void Add(DicItem di)
        {
            _dic.Add(di);
            comboBox1.Items.Add(di);
        }
        public bool Checked
        {
            get
            {
                return checkBox1.Checked;
            }
            set
            {
                checkBox1.Checked = comboBox1.Enabled = value;
            }
        }
        public bool CheckBoxVisible
        {
            get
            {
                return checkBox1.Visible;
            }
            set
            {
                checkBox1.Visible = value;
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Checked = checkBox1.Checked;
        }
        public DicItem SelectedItem
        {
            get
            {
                return comboBox1.SelectedItem == null ? null : (DicItem)comboBox1.SelectedItem;
            }
        }
        public int? SelectedId
        {
            get
            {
                return comboBox1.SelectedIndex >= 0 ? (int?)_dic[comboBox1.SelectedIndex].Id : null;
            }
            set
            {
                if (!DesignMode)
                {
                    comboBox1.SelectedIndex = -1;
                    if (value != null)
                    {
                        DicItem di = _dic.Find(pet => pet.Id == value);
                        if (di != null)
                            comboBox1.SelectedIndex = _dic.IndexOf(di);
                    }
                }
            }
        }

        public int SelectedIndex
        {
            get
            {
                return comboBox1.SelectedIndex;
            }
            set
            {
                comboBox1.SelectedIndex = value;
            }
        }
        #region EVENTS
        public delegate void SelectedIndexChangedEventHandler(int i);
        public event SelectedIndexChangedEventHandler SelectedIndexChangedEvent;
        protected virtual void RaiseSelectedIndexChangedEvent(int i)
        {
            if (SelectedIndexChangedEvent != null)
            {
                SelectedIndexChangedEvent(i);
            }
        }
        #endregion

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RaiseSelectedIndexChangedEvent(SelectedIndex);
        }
    }
}
