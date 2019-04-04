using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FERHRI.Common;

namespace FERHRI.Amur.Data
{
    public partial class UCDataEdit : UserControl
    {
        //public class Options
        //{
        //    public bool ShowDataDetails { get; set; }
        //    public bool ShowChart { get; set; }
        //    public bool OnlyRedValues { get; set; }

        //    public Options(bool showDataDetails, bool onlyRedValues, bool showChart)
        //    {
        //        ShowDataDetails = showDataDetails;
        //        OnlyRedValues = onlyRedValues;
        //        ShowChart = showChart;
        //    }
        //}
        public UCDataEdit(FormDataFilter formDataFilter, List<Sys.AttrValue> userSettings)
        {
            InitializeComponent();

            ucDataTable.FormDataFilter = formDataFilter;
            ucDataTable.UserSettings = userSettings;

            ucDataHistory.HideColumns = new List<string>(new string[] { "siteName", "variableName", "offsetTypeName", "offsetValue", "id" });
        }

        DataFilter DataFilter { get; set; }

        public void Fill(DataFilter dataFilter)
        {
            DataFilter = dataFilter;
            ucDataTable.Fill(dataFilter);

            AcceptOptions();
        }

        private void ucDataTable_UCCurrentDataValueChangedEvent(DataValue dv)
        {
            ucDataHistory.Clear();

            if (dv != null && _Options.ShowDataDetails)
            {
                Catalog catalog = ucDataTable.CatalogDV.Find(x => x.Id == dv.CatalogId);
                DataFilter df = new Data.DataFilter(dv.DateLOC, dv.DateLOC,
                    new List<int>(new int[] { catalog.SiteId }),
                    new List<int>(new int[] { catalog.VariableId }),
                    DataFilter.OffsetTypeId,
                    DataFilter.OffsetValue,
                    DataFilter.MethodId,
                    DataFilter.SourceId,
                    null,
                    false,
                    true,
                    false);
                List<DataValue> dvs = Data.DataManager.GetInstance().DataValueRepository.SelectA(df);
                Dictionary<long, DataSource> ds = Data.DataManager.GetInstance().DataSourceRepository.Select(dvs.Select(x => x.Id).ToList());

                ucDataHistory.Fill(dvs, ds);
            }
        }

        private void ucDataHistory_UCCurrentDataValueActualizedEvent()
        {
            Fill(DataFilter);
        }

        public bool DataFilterEnabled { get { return ucDataTable.DataFilterEnabled; } set { ucDataTable.DataFilterEnabled = value; } }

        private void ucDataHistory_UCCurrentDataValueChangedEvent(DataValue dv)
        {
            if (dv != null)
                ucDataAQC.Fill(dv.Id);
        }
        public OptionsDataEdit _Options { get; set; }
        void AcceptOptions()
        {
            splitContainer2.Panel2Collapsed = !_Options.ShowDataDetails;
            ucDataTable.AsseptOptions(_Options);
        }

        private void UCDataEdit_Load(object sender, EventArgs e)
        {
            _Options = new OptionsDataEdit(true, false, true, false, false);
            AcceptOptions();
        }
        private void ucDataTable_UCChangeOptionsEvent()
        {
            FormOptionsUCDataEdit frm = new FormOptionsUCDataEdit();
            frm.Options = _Options;
            frm.StartPosition = FormStartPosition.CenterParent;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                _Options = frm.Options;
                AcceptOptions();
            }
        }
    }
}
