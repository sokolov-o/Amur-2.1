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
    /// <summary>
    /// Таблица значений с выводом кол, ср, суммы, мин, макс в примечании к заголовку первого столбца.
    /// Используется, например, для отображения derived и parent значений.
    /// 
    /// OSokolov@SOV.ru
    /// 20160313
    /// </summary>
    public partial class UCDataValuesCMMC : UserControl
    {
        public UCDataValuesCMMC()
        {
            InitializeComponent();
        }
        public void Fill(List<DataValue> dvs)
        {
            dataValueBindingSource.DataSource = dvs;
            dgv.Columns[0].ToolTipText = null;

            dgv.Columns[0].ToolTipText = (dvs != null && dvs.Count > 0)
                ? "Количество = " + dvs.Count
                  + "\nСреднее\t" + dvs.Average(x => x.Value) 
                  + "\nСумма\t" + dvs.Sum(x => x.Value)
                  + "\nМин\t" + dvs.Min(x => x.Value)
                  + "\nМакс\t" + dvs.Max(x => x.Value)
                : null;
        }
        public void Clear()
        {
            dataValueBindingSource.DataSource = null;
            dgv.Columns[0].ToolTipText = null;

            RaiseUCCurrentDataValueChangedEvent(null);
        }

        #region EVENTS
        public delegate void UCCurrentDataValueChangedEventHandler(DataValue dv);
        public event UCCurrentDataValueChangedEventHandler UCCurrentDataValueChangedEvent;
        protected virtual void RaiseUCCurrentDataValueChangedEvent(DataValue dv)
        {
            if (UCCurrentDataValueChangedEvent != null)
            {
                UCCurrentDataValueChangedEvent(dv);
            }
        }
        #endregion

        DataValue CurDataValue
        {
            get
            {
                return (dataValueBindingSource == null || dataValueBindingSource.Current == null)
                    ? null
                    : (DataValue)dataValueBindingSource.Current;
                ;
            }
        }
        private void dgv_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            RaiseUCCurrentDataValueChangedEvent(CurDataValue);
        }
    }
}
