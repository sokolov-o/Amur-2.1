using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOV.Amur.Meta
{
    public partial class UCMethodForecast : UserControl
    {
        public UCMethodForecast()
        {
            InitializeComponent();
        }
        MethodForecast _MethodForecast = null;
        public MethodForecast MethodForecast
        {
            set
            {
                _MethodForecast = value;
                ucMethod.Value = null;
                leadTimeUnitUC.Value = null;
                leadTimesTextBox.Text = null;
                dateIniTextBox.Text = null;

                if (value != null)
                {
                    ucMethod.Value = value.Method;
                    leadTimeUnitUC.Value = DataManager.GetInstance().UnitRepository.Select(value.LeadTimeUnitId);
                    leadTimesTextBox.Text = Common.StrVia.ToString(value.LeadTimes, ";");
                    dateIniTextBox.Text = Common.StrVia.ToString(value.DateIniHoursUTC, ";");
                }
            }
            get
            {
                return new MethodForecast()
                {
                    Method = (Method)ucMethod.Value,
                    LeadTimeUnitId = ((Unit)leadTimeUnitUC.Value).Id,
                    LeadTimes = string.IsNullOrEmpty(leadTimesTextBox.Text) ? null : Common.StrVia.ToArrayDouble(leadTimesTextBox.Text),
                    DateIniHoursUTC = string.IsNullOrEmpty(dateIniTextBox.Text) ? null : Common.StrVia.ToArrayDouble(dateIniTextBox.Text)

                };
            }
        }
        public void Save()
        {
            MethodForecast meth = MethodForecast;
            if (meth != null)
                DataManager.GetInstance().MethodForecastRepository.InsertOrUpdate(meth);
        }

        private void leadTimeUnitUC_UCEditButtonPressedEvent()
        {
            Common.FormListBox frm = new Common.FormListBox(
                "Выбор временного интервала прогноза",
                Meta.DataManager.GetInstance().UnitRepository.Select(null, "Time")
                    //Select(x => new Common.IdName() { Id = x.Id, Name = x.Name })
                    .ToArray(),
                "Name",
                true);
            frm.StartPosition = FormStartPosition.CenterScreen;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                leadTimeUnitUC.Value = frm.GetSelectedItem();
            }
        }
    }
}
