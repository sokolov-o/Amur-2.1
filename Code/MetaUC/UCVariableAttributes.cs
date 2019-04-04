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
    public partial class UCVariableAttributes : UserControl
    {
        public UCVariableAttributes()
        {
            InitializeComponent();
        }
        int _variableId = -1;
        public void Fill(int variableId)
        {
            Clear();

            _variableId = variableId;
            VariableAttributes va = DataManager.GetInstance().VariableAttributesRepository.Select(variableId);
            if (va != null)
                this.VariableAttributes = va;
        }
        public void Clear()
        {
            _variableId = -1;
            minTextBox.Text = maxTextBox.Text = stepTextBox.Text = valueFormatTextBox.Text = null;
        }

        VariableAttributes VariableAttributes
        {
            get
            {
                return new VariableAttributes()
                {
                    VariableId = _variableId,
                    AxisMin = string.IsNullOrEmpty(minTextBox.Text) ? double.NaN : double.Parse(minTextBox.Text),
                    AxisMax = string.IsNullOrEmpty(maxTextBox.Text) ? double.NaN : double.Parse(maxTextBox.Text),
                    AxisStep = string.IsNullOrEmpty(stepTextBox.Text) ? double.NaN : double.Parse(stepTextBox.Text),
                    ValueFormat = string.IsNullOrEmpty(valueFormatTextBox.Text) ? null : valueFormatTextBox.Text
                };
            }
            set
            {
                _variableId = value.VariableId;
                minTextBox.Text = double.IsNaN(value.AxisMin) ? null : value.AxisMin.ToString();
                maxTextBox.Text = double.IsNaN(value.AxisMax) ? null : value.AxisMax.ToString();
                stepTextBox.Text = double.IsNaN(value.AxisStep) ? null : value.AxisStep.ToString();
                valueFormatTextBox.Text = value.ValueFormat;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            DataManager.GetInstance().VariableAttributesRepository.Update(this.VariableAttributes);
        }
    }
}
