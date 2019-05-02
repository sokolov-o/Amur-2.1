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
    public partial class UCMethodClimate : UserControl
    {
        public UCMethodClimate()
        {
            InitializeComponent();
        }
        MethodClimate _MethodClimate;
        public MethodClimate MethodClimate
        {
            set
            {
                _MethodClimate = value;
                yearSTextBox.Text = yearFTextBox.Text = string.Empty;

                if (value != null)
                {
                    yearSTextBox.Text = value.YearS.ToString();
                    yearFTextBox.Text = value.YearF.ToString();
                }
            }
            get
            {
                if (_MethodClimate == null || string.IsNullOrEmpty(yearSTextBox.Text) || string.IsNullOrEmpty(yearFTextBox.Text))
                    return null;
                return new MethodClimate()
                {
                    MethodId = _MethodClimate.MethodId,
                    YearS = int.Parse(yearSTextBox.Text),
                    YearF = int.Parse(yearFTextBox.Text)
                };

            }
        }
        public void Save()
        {
            MethodClimate meth = MethodClimate;
            if (meth != null)
                DataManager.GetInstance().MethodClimateRepository.InsertOrUpdate(meth);
        }
    }
}
