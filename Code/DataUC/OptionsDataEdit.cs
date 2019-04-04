using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Amur.Data
{
    public class OptionsDataEdit
    {
        public bool ShowDataDetails { get; set; }
        public bool ShowAQC { get; set; }
        public bool ShowDerived { get; set; }

        public bool ShowChart { get; set; }
        public bool OnlyRedValues { get; set; }
        public bool ShowVarCodeText { get; set; }

        public OptionsDataEdit(bool showDataDetails, bool onlyRedValues, bool showChart, bool showAQC, bool showDerived, bool showVarCodeText)
        {
            ShowDataDetails = showDataDetails;
            ShowAQC = showAQC;
            ShowDerived = showDerived;

            OnlyRedValues = onlyRedValues;
            ShowChart = showChart;
            ShowVarCodeText = showVarCodeText;
        }
    }
}
