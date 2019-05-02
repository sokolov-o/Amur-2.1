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
    public partial class UCDataDetails : UserControl
    {
        public UCDataDetails()
        {
            InitializeComponent();
        }

        internal void Clear()
        {
            ucDataValueList.Clear();
            ucDataAQC.Clear();
        }

        internal void Fill(DataValue dv)
        {
            ucDataValueList.Fill(dv);
        }

        private void ucDataValueList_UCCurrentDataValueChangedEvent(DataValue dv)
        {
            ucDataAQC.Clear();
            ucDerived.Clear();

            if (dv != null)
            {
                ucDataAQC.Fill(dv.Id);
                ucDerived.Fill(dv.Id);
            }
        }
        public bool ShowAQC
        {
            get
            {
                return !splitContainer2.Panel1Collapsed;
            }
            set
            {
                splitContainer2.Panel1Collapsed = !value;
            }
        }
        public bool ShowDerived
        {
            get
            {
                return !splitContainer2.Panel2Collapsed;
            }
            set
            {
                splitContainer2.Panel2Collapsed = !value;
            }
        }

        private void ucDataValueList_UCCurrentDataValueActualizedEvent()
        {
            RaiseUCCurrentDataValueActualizedEvent();
        }
        public delegate void UCCurrentDataValueActualizedEventHandler();
        public event UCCurrentDataValueActualizedEventHandler UCCurrentDataValueActualizedEvent;
        protected virtual void RaiseUCCurrentDataValueActualizedEvent()
        {
            if (UCCurrentDataValueActualizedEvent != null)
            {
                UCCurrentDataValueActualizedEvent();
            }
        }

    }
}
