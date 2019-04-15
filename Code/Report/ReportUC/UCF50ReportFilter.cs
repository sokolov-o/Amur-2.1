using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SOV.Amur.Meta;

namespace SOV.Amur.Reports
{
    public partial class UCF50ReportFilter : UserControl
    {
        public UCF50ReportFilter()
        {
            InitializeComponent();

        }

        private void timeTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (TimeUnit)
            {
                case (int)EnumTime.Month:
                    label1.Text = "Год, месяц";
                    decadeOfMonthTextBox.Visible = false;
                    break;
                case (int)EnumTime.DecadeOfYear:
                    label1.Text = "Год, месяц, декада месяца";
                    decadeOfMonthTextBox.Visible = true;
                    break;
                default:
                    throw new Exception("Выбран неизвестный временной интервал...");
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        public int SiteGroup { get { return (int)siteGroupComboBox.SelectedId; } }
        public byte? FlagAQC { get { return (flagAQCComboBox.SelectedId == -1) ? null : (byte?)flagAQCComboBox.SelectedId; } }
        public int Year { get { return int.Parse(yearTextBox.Text); } }
        public int Month { get { return int.Parse(monthTextBox.Text); } }
        public int? DecadeOfMonth
        {
            get
            {
                return string.IsNullOrEmpty(decadeOfMonthTextBox.Text)
                    || timeUnitComboBox.SelectedIndex == 0
                    ? null : (int?)int.Parse(decadeOfMonthTextBox.Text);
            }
        }

        private void UCF50ReportFilter_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            timeUnitComboBox.Add(new Common.DicItem((int)EnumTime.Month, "Месяц"));
            timeUnitComboBox.Add(new Common.DicItem((int)EnumTime.DecadeOfYear, "Декада"));
            TimeUnit = (int)EnumTime.Month;

            List<EntityGroup> egList = Meta.DataManager.GetInstance().EntityGroupRepository.Select();
            List<Common.DicItem> diList = egList.Select(x => new Common.DicItem() { Id = x.Id, Name = x.Name, Entity = x }).ToList();
            siteGroupComboBox.AddRange(diList);

            siteGroupComboBox.SelectedId = 9;

            flagAQCComboBox.Add(new Common.DicItem(-1, "Все"));
            flagAQCComboBox.Add(new Common.DicItem(0, "Не проводился"));
            flagAQCComboBox.Add(new Common.DicItem(1, "Проведён успешно"));
            flagAQCComboBox.Add(new Common.DicItem(2, "Проведён, есть ошибки"));
            flagAQCComboBox.SelectedId = -1;

            yearTextBox.Text = DateTime.Today.Year.ToString();
            monthTextBox.Text = DateTime.Today.Month.ToString();
            decadeOfMonthTextBox.Text = "1";
        }

        public int TimeUnit
        {
            get { return (int)timeUnitComboBox.SelectedId; }
            set { timeUnitComboBox.SelectedId = value; }
        }

        public bool ShowTimeUnitControl
        {
            get
            {
                return timeUnitComboBox.Visible;
            }
            set
            {
                timeUnitComboBox.Visible = label3.Visible = value;
            }
        }
    }
}
