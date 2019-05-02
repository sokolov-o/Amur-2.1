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
    public class CellDataValue
    {
        static public void Clear(DataGridViewCell cell)
        {
            cell.Tag = null;
            cell.Value = null;
            cell.Style.ForeColor = System.Drawing.Color.Black;
            cell.Style.BackColor = Color.White;
        }
        static public void SetDataValue(DataGridViewCell cell, List<DataValue> dvs)
        {
            cell.Tag = dvs;
            cell.Value = null;

            if (dvs.Count > 0)
            {
                cell.Value = dvs[0].Value;

                switch (dvs[0].FlagAQC)
                {
                    case (int)Meta.EnumFlagAQC.Error:
                        cell.Style.ForeColor = System.Drawing.Color.Red;
                        break;
                    case (int)Meta.EnumFlagAQC.Deleted:
                        cell.Style = new DataGridViewCellStyle(cell.OwningRow.DataGridView.DefaultCellStyle);
                        cell.Style.Font = new Font(cell.Style.Font, FontStyle.Strikeout);
                        break;
                }
                if (dvs.Count > 1)
                {
                    cell.Style.BackColor = Color.Pink;
                }
            }
        }
        static public DataValue GetDataValue(DataGridViewCell cell)
        {
            return cell.Tag == null || ((List<DataValue>)cell.Tag).Count == 0 || cell.Tag.GetType() != typeof(List<DataValue>)
                ? null : ((List<DataValue>)cell.Tag)[0];
        }
        static public List<DataValue> GetDataValues(DataGridViewCell cell)
        {
            return null;
        }
    }
}
