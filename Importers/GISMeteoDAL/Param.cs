using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.GISMeteo
{
    public class Param
    {
        public int GisParamId { get; set; }

        public int? GisLevelId { get; set; }

        public int VariableIdAmur { get; set; }

        public double Multip { get; set; }

        public int Decimals { get; set; }

        public Param(int gisParamId, int? gisLevelId, int varIdAmur, string multip)
        {
            GisParamId = gisParamId;
            GisLevelId = gisLevelId;
            VariableIdAmur = varIdAmur;

            string separator = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            multip = multip.Replace(".", separator).Replace(",", separator);
            Multip = Convert.ToDouble(multip);

            Decimals = multip.IndexOf(separator[0]);
            if (Decimals < 0)
                Decimals = 0;
            else
            {
                Decimals = multip.Length - Decimals - 1;
            }
        }
    }
}
