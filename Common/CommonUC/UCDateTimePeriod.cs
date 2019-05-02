using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SOV.Common
{
    /// <summary>
    /// Временной период с возможностью открытости с обеих сторон.
    /// Значение границы периода null означает открытость.
    /// </summary>
    public partial class UCDateTimePeriod : UserControl
    {
        public UCDateTimePeriod()
        {
            InitializeComponent();

            DateTimePeriod = new Common.DateTimePeriod();
            CustomDateFormat = "dd.MM.yyyy HH:mm";
        }

        public DateTimePeriod.Type PeriodType
        {
            get
            {
                return (DateTimePeriod.Type)periodTypeComboBox.SelectedIndex;
            }
            set
            {
                switch (value)
                {
                    case DateTimePeriod.Type.Period:
                        dateS.CustomFormat = CustomDateFormat;
                        dateF.CustomFormat = CustomDateFormat;
                        VisibleDateCheckBoxs = true;
                        VisibleDateSF = true;
                        break;
                    case DateTimePeriod.Type.YearMonth:
                        dateS.CustomFormat = "yyyy";
                        dateF.CustomFormat = "MM";
                        VisibleDateCheckBoxs = false;
                        VisibleDateSF = true;
                        break;
                    case DateTimePeriod.Type.DaysBeforeCurDate:
                        VisibleCountTextBox = true;
                        break;
                    case DateTimePeriod.Type.Years:
                        dateS.CustomFormat = "yyyy";
                        dateF.CustomFormat = "yyyy";
                        VisibleDateCheckBoxs = false;
                        VisibleDateSF = true;
                        VisibleCountTextBox = false;
                        break;
                    default:
                        throw new Exception("Неизвестный временной период.");
                }
            }
        }
        public bool VisibleDateCheckBoxs
        {
            get
            {
                return dateS.ShowCheckBox;
            }
            set
            {
                dateS.ShowCheckBox = dateF.ShowCheckBox = value;
                dateS.Checked = dateF.Checked = true;
            }
        }
        public bool VisibleCountTextBox
        {
            get
            {
                return !VisibleDateSF;
            }
            set
            {
                VisibleDateSF = !value;
            }
        }
        public bool VisibleDateSF
        {
            get
            {
                return tlp.ColumnStyles[2].Width == 0 ? false : true;
            }
            set
            {
                if (value)
                {
                    tlp.ColumnStyles[2].Width = tlp.ColumnStyles[3].Width = 50;
                    tlp.ColumnStyles[2].SizeType = tlp.ColumnStyles[3].SizeType = SizeType.Percent;

                    tlp.ColumnStyles[1].SizeType = SizeType.Absolute;
                    tlp.ColumnStyles[1].Width = 0;
                }
                else
                {
                    tlp.ColumnStyles[1].SizeType = SizeType.Percent;
                    tlp.ColumnStyles[1].Width = 50;

                    tlp.ColumnStyles[2].Width = tlp.ColumnStyles[3].Width = 0;
                    tlp.ColumnStyles[2].SizeType = tlp.ColumnStyles[3].SizeType = SizeType.Absolute;
                }
            }
        }
        int viewTypeCheckBoxWidth = 120;
        public bool VisibleViewTypeCheckBox
        {
            get
            {
                return tlp.ColumnStyles[0].Width == 0 ? false : true;
            }
            set
            {
                if (value)
                {
                    tlp.ColumnStyles[0].SizeType = SizeType.Absolute;
                    tlp.ColumnStyles[0].Width = viewTypeCheckBoxWidth;
                }
                else
                {
                    tlp.ColumnStyles[0].SizeType = SizeType.Absolute;
                    tlp.ColumnStyles[0].Width = 0;
                }
            }
        }

        private void viewTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            PeriodType = (DateTimePeriod.Type)periodTypeComboBox.SelectedIndex;
        }

        public string CustomDateFormat { get; set; }
        public int? YearS
        {
            set
            {
                dateS.Checked = false;
                if (value.HasValue)
                {
                    dateS.Checked = true;
                    dateS.Value = new DateTime((int)value, 1, 1);
                }
            }
            get
            {
                DateTimePeriod dtp = DateTimePeriod;
                return dtp.DateS.HasValue ? (int?)((DateTime)dtp.DateS).Year : null;
            }
        }
        public int? YearF
        {
            set
            {
                dateF.Checked = false;
                if (value.HasValue)
                {
                    dateF.Checked = true;
                    dateF.Value = new DateTime((int)value, 12, 31);
                }
            }
            get
            {
                DateTimePeriod dtp = DateTimePeriod;
                return dtp.DateF.HasValue ? (int?)((DateTime)dtp.DateF).Year : null;
            }
        }
        public DateTimePeriod DateTimePeriod
        {
            get
            {
                bool isHH = CustomDateFormat.IndexOf("HH") >= 0;
                bool ismm = CustomDateFormat.IndexOf("mm") >= 0;
                bool isss = CustomDateFormat.IndexOf("ss") >= 0;
                return new DateTimePeriod(
                    //dateS.Checked ? (DateTime?)DateTime.FromBinary(dateS.Value.ToBinary()) : null,
                    //dateF.Checked ? (DateTime?)DateTime.FromBinary(dateF.Value.ToBinary()) : null,
                    dateS.Checked ? (DateTime?)new DateTime(dateS.Value.Year, dateS.Value.Month, dateS.Value.Day,
                    isHH ? dateS.Value.Hour : 0,
                    ismm ? dateS.Value.Minute : 0,
                    isss ? dateS.Value.Second : 0) : null,
                    dateF.Checked ? (DateTime?)new DateTime(dateF.Value.Year, dateF.Value.Month, dateF.Value.Day,
                    isHH ? dateF.Value.Hour : 0,
                    ismm ? dateF.Value.Minute : 0,
                    isss ? dateF.Value.Second : 0) : null,

                    PeriodType,
                    string.IsNullOrEmpty(daysBeforeDateNowTextBox.Text) ? 0 : int.Parse(daysBeforeDateNowTextBox.Text)
                );
            }
            set
            {
                if (value == null) value = new Common.DateTimePeriod();

                dateS.Checked = value.DateS.HasValue;
                dateS.Value = dateS.Checked ? (DateTime)value.DateS : DateTime.Today;
                dateF.Checked = value.DateF.HasValue;
                dateF.Value = dateF.Checked ? (DateTime)value.DateF : DateTime.Today;
                periodTypeComboBox.SelectedIndex = (int)value.PeriodType;
                daysBeforeDateNowTextBox.Text = value.DaysBeforeDateNow.ToString();
            }
        }
    }
}
