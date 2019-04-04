using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOV.Common
{
    public partial class UCTextBox : UserControl
    {
        public UCTextBox()
        {
            InitializeComponent();
        }
        public object Value
        {
            set
            {
                Text = value == null ? "NULL" : value.ToString();
                Tag = value;
            }
            get
            {
                return Tag;
            }
        }
        override public string Text
        {
            set
            {
                textBox.Text = value;
            }
        }
        public bool ShowEditButton
        {
            get
            {
                return tlp.ColumnStyles[1].SizeType == SizeType.AutoSize;
            }
            set
            {
                if (value)
                {
                    tlp.ColumnStyles[1].SizeType = SizeType.AutoSize;
                }
                else
                {
                    tlp.ColumnStyles[1].SizeType = SizeType.Absolute;
                    tlp.ColumnStyles[1].Width = 0;
                }
            }
        }
        public bool ShowNullButton
        {
            get
            {
                return tlp.ColumnStyles[2].SizeType == SizeType.AutoSize;
            }
            set
            {
                if (value)
                {
                    tlp.ColumnStyles[2].SizeType = SizeType.AutoSize;
                }
                else
                {
                    tlp.ColumnStyles[2].SizeType = SizeType.Absolute;
                    tlp.ColumnStyles[2].Width = 0;
                }
            }
        }
        public delegate void UCEditButtonPressedEventHandler();
        public event UCEditButtonPressedEventHandler UCEditButtonPressedEvent;

        private void editButton_Click(object sender, EventArgs e)
        {
            if (UCEditButtonPressedEvent != null)
            {
                UCEditButtonPressedEvent();
            }
        }

        private void nullButton_Click(object sender, EventArgs e)
        {
            Value = null;
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            Console.Beep();
            e.Handled = true;
        }
    }
}
