using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOV.Amur.Data
{
    public partial class UCDerived : UserControl
    {
        public UCDerived()
        {
            InitializeComponent();
        }
        public void Clear()
        {
            parentDataValues.Clear();
            derivedDataValues.Clear();
        }
        public void Fill(long dvId)
        {
            DataValueRepository dvrep = Data.DataManager.GetInstance().DataValueRepository;

            if (ShowParent) parentDataValues.Fill(dvrep.SelectParents(dvId));
            if (ShowDerived) derivedDataValues.Fill(dvrep.SelectDeriveds(dvId));
        }
        public bool ShowParent
        {
            get
            {
                TableLayoutColumnStyleCollection tcs = tableLayoutPanel1.ColumnStyles;
                return tcs[0].SizeType == SizeType.Percent;
            }
            set
            {
                TableLayoutColumnStyleCollection tcs = tableLayoutPanel1.ColumnStyles;
                if (value)
                {
                    tcs[0].SizeType = SizeType.Percent;
                    tcs[0].Width = 50;
                }
                else
                {
                    tcs[0].SizeType = SizeType.Absolute;
                    tcs[0].Width = 0;
                }
            }
        }
        public bool ShowDerived
        {
            get
            {
                TableLayoutColumnStyleCollection tcs = tableLayoutPanel1.ColumnStyles;
                return tcs[1].SizeType == SizeType.Percent;
            }
            set
            {
                TableLayoutColumnStyleCollection tcs = tableLayoutPanel1.ColumnStyles;
                if (value)
                {
                    tcs[1].SizeType = SizeType.Percent;
                    tcs[1].Width = 50;
                }
                else
                {
                    tcs[1].SizeType = SizeType.Absolute;
                    tcs[1].Width = 0;
                }
            }
        }
    }
}
