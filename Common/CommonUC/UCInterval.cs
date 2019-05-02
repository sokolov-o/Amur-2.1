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
    public partial class UCInterval : UserControl
    {
        public UCInterval()
        {
            InitializeComponent();
        }
        public decimal[] GetInterval()
        {
            return new decimal[] { leftBound.Value, rightBound.Value };
        }
        public void SetMinMax(int[] minMax)
        {
            SetMinMax(minMax, minMax);
        }
        bool isSetMinMax = false;
        public void SetMinMax(int[] leftMinMax, int[] rightMinMax)
        {
            isSetMinMax = true;

            leftBound.DecimalPlaces = rightBound.DecimalPlaces = 0;

            leftBound.Minimum = leftMinMax[0];
            leftBound.Maximum = leftMinMax[1];
            leftBound.Value = leftBound.Minimum;

            rightBound.Minimum = rightMinMax[0];
            rightBound.Maximum = rightMinMax[1];
            rightBound.Value = rightBound.Maximum;

            isSetMinMax = false;
            leftBound_ValueChanged(null, null);
        }
        public void SetInterval(decimal left, decimal right)
        {
            leftBound.Value = left;
            rightBound.Value = right;
        }

        private void leftBound_ValueChanged(object sender, EventArgs e)
        {
            if (!isSetMinMax)
            {
                if (leftBound.Value > rightBound.Value)
                {
                    Console.Beep();
                    leftBound.Value = rightBound.Value >= leftBound.Minimum && rightBound.Value <= leftBound.Maximum ? rightBound.Value : leftBound.Minimum;
                }
            }
        }

        private void rightBound_ValueChanged(object sender, EventArgs e)
        {
            if (!isSetMinMax)
            {
                if (leftBound.Value > rightBound.Value)
                {
                    Console.Beep();
                    rightBound.Value = leftBound.Value >= rightBound.Minimum && leftBound.Value <= rightBound.Maximum ? leftBound.Value : rightBound.Maximum;
                }
            }
        }
    }
}
