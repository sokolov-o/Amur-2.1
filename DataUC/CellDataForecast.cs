using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using SOV.Common;

namespace SOV.Amur.Data
{
    public class CellDataForecast
    {
        static public void SetValue(DataGridViewCell cell, DataForecast value)
        {
            SetValues(cell, value == null ? null : new List<DataForecast>(new DataForecast[] { value }));
        }
        static public void SetValues(DataGridViewCell cell, List<DataForecast> values)
        {
            cell.Tag = values;
            cell.Value = null;

            if (values != null && values.Count > 0)
            {
                cell.Value = values[0].Value;
                if (values.Count > 1)
                {
                    cell.Style.BackColor = Color.RosyBrown;
                    cell.ToolTipText = "";
                    foreach (var item in values)
                    {
                        cell.ToolTipText += item.ToString() + "\n";
                    }
                }
            }
        }
        static public DataForecast GetValue(DataGridViewCell cell)
        {
            return cell.Tag == null || ((List<DataForecast>)cell.Tag).Count == 0 ? null : ((List<DataForecast>)cell.Tag)[0];
        }
        static public List<DataForecast> GetDataValues(DataGridViewCell cell)
        {
            return cell.Tag == null || ((List<DataForecast>)cell.Tag).Count == 0 ? null : (List<DataForecast>)cell.Tag;
        }
    }
}
