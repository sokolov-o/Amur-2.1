using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SOV.Common;
using SOV.Amur.Meta;
using SOV.Social;

namespace SOV.Amur.Reports
{
    public partial class FormWaterObjectReport : Form
    {
        int repId = 30;
        List<DicItem> orgsDic = new List<DicItem>();
        List<DicItem> periodTypes = new List<DicItem>() {
            new DicItem((int)DateTimePeriod.Type.Day, "День", (object)"dd.MM.yyyy"),
            new DicItem((int)DateTimePeriod.Type.FstDecade, "Первая декада", (object)"MMMM yyyy"),
            new DicItem((int)DateTimePeriod.Type.SndDecade, "Вторая декада", (object)"MMMM yyyy"),
            new DicItem((int)DateTimePeriod.Type.ThdDecade, "Третья декада", (object)"MMMM yyyy"),
        };
        List<DicItem> reviewTypes = new List<DicItem>() {
            new DicItem((int)WaterObjReport.Type.Summer, "Летний"),
            new DicItem((int)WaterObjReport.Type.Winter, "Зимний")
        };

        DateTime? dateActual = DateTime.Today;

        public FormWaterObjectReport()
        {
            InitializeComponent();
            bottomLayoutPanel.SetColumnSpan(doneButton, 2);
            danger2CheckBox.Checked = danger1CheckBox.Checked = false;

            DataManager reportDM = DataManager.GetInstance();
            Social.DataManager socialDM = Social.DataManager.GetInstance();

            List<StaffPosition> staffPositions = socialDM.StaffPositionRepository.Select();
            foreach (StaffPosition pos in staffPositions)
            {
                authorPos1ComboBox.Items.Add(new DicItem(pos.Id, pos.NameRus));
                authorPos2ComboBox.Items.Add(new DicItem(pos.Id, pos.NameRus));
            }

            timeTypeComboBox.Items.Add(EnumDateType.LOC);
            timeTypeComboBox.Items.Add(EnumDateType.UTC);
            timeTypeComboBox.SelectedItem = EnumDateType.LOC;

            List<Org> orgs = reportDM.OrgRepository.SelectByReport((int)EnumReport.TEST);
            List<Staff> staffs = new List<Staff>();
            foreach (var item in orgs)
            {
                staffs.AddRange(socialDM.StaffRepository.SelectByEmployer(item.OrgId, dateActual));
            }
            List<StaffEmployee> ses = socialDM.StaffEmployeeRepository.Select(staffs.Select(x => x.Id).ToList(), null, dateActual);
            List<LegalEntity> persons = socialDM.LegalEntityRepository.Select(ses.Select(x => x.EmployeeId).ToList());
            List<LegalEntity> les = socialDM.LegalEntityRepository.Select(
                orgs.Select(x => x.OrgId).Union(persons.Select(x => x.Id)).ToList()
            );

            foreach (Org org in orgs)
            {
                DicItem orgDicItem = new DicItem(org.OrgId, les.Find(x => x.Id == org.OrgId).NameRus, org);
                List<DicItem> personDicList = new List<DicItem>();
                foreach (var staff in staffs.Where(x => x.Division.Employer.Id == org.OrgId))
                {
                    DicItem pos = new DicItem(staff.StaffPosition.Id, staffPositions.Find(x => x.Id == staff.StaffPosition.Id).NameRus);
                    int employeeId = ses.FirstOrDefault(x => x.StaffId == staff.Id).EmployeeId;
                    DicItem person = new DicItem(employeeId, les.Find(x => x.Id == employeeId).NameRus);
                    person.Childs = new List<DicItem> { pos };
                    personDicList.Add(person);
                }
                orgDicItem.Childs = personDicList;
                orgsDic.Add(orgDicItem);
                orgComboBox.Items.Add(orgDicItem);
            }
            if (orgComboBox.Items.Count > 0)
                orgComboBox.SelectedItem = orgComboBox.Items[0];

            periodTypeComboBox.Items.AddRange(periodTypes.ToArray());
            periodTypeComboBox.SelectedIndex = 0;

            typeComboBox.Items.AddRange(reviewTypes.ToArray());
            typeComboBox.SelectedIndex = 0;

            stationComboBox.Items.AddRange(Meta.DataManager.GetInstance().EntityGroupRepository.SelectByEntityTableName("site").ToArray());
            if (stationComboBox.Items.Count > 0)
                stationComboBox.SelectedIndex = stationComboBox.Items.Count - 1;
        }

        private void danger1CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            danger1TextBox.Visible = danger1Label.Enabled = danger1CheckBox.Checked;
            int i = middleLayoutPanel.GetRow(danger1TextBox);
            middleLayoutPanel.RowStyles[i].SizeType = danger1CheckBox.Checked ? SizeType.Percent : SizeType.AutoSize;
        }

        private void danger2CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            danger2TextBox.Visible = danger2Label.Enabled = danger2CheckBox.Checked;
            int i = middleLayoutPanel.GetRow(danger2TextBox);
            middleLayoutPanel.RowStyles[i].SizeType = danger2CheckBox.Checked ? SizeType.Percent : SizeType.AutoSize;
        }

        private void orgComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            author1ComboBox.Items.Clear();
            author2ComboBox.Items.Clear();
            foreach (DicItem i in ((DicItem)orgComboBox.SelectedItem).Childs)
            {
                author1ComboBox.Items.Add(i);
                author2ComboBox.Items.Add(i);
            }
            author1ComboBox.SelectedIndex = author2ComboBox.SelectedIndex = author1ComboBox.Items.Count > 0 ? 0 : -1;
            authorPos2ComboBox.SelectedIndex = authorPos1ComboBox.SelectedIndex = author1ComboBox.Items.Count > 0 ? 0 : -1;
        }

        private void periodTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            periodDateS.CustomFormat = (string)((DicItem)periodTypeComboBox.SelectedItem).Entity;
        }

        private void author1checkBox_CheckedChanged(object sender, EventArgs e)
        {
            authorPos1ComboBox.Enabled = author1ComboBox.Enabled = author1checkBox.Checked;
        }

        private void author2checkBox_CheckedChanged(object sender, EventArgs e)
        {
            authorPos2ComboBox.Enabled = author2ComboBox.Enabled = author2checkBox.Checked;
        }

        private void doneButton_Click(object sender, EventArgs e)
        {
            Org org = (Org)((DicItem)orgComboBox.SelectedItem).Entity;
            Report rep = DataManager.GetInstance().ReportRepository.Select(repId);
            DataFilter dataFilter = new DataFilter()
            {
                IsDateLOC = (EnumDateType)timeTypeComboBox.SelectedItem == EnumDateType.LOC,
                DateTimePeriod = new DateTimePeriod(
                    periodDateS.Value,
                    null,
                    (DateTimePeriod.Type)((DicItem)periodTypeComboBox.SelectedItem).Id,
                    0
                ),
                CatalogFilter = new CatalogFilter()
                {
                    Methods = null,
                    Sources = null,
                    OffsetTypes = new List<int>(new int[] { (int)EnumOffsetType.NoOffset }),
                    OffsetValue = 0
                },
            };

            WaterObjReport waterObjReport = new WaterObjReport(
                Meta.DataManager.GetInstance().EntityGroupRepository.Select(((EntityGroup)stationComboBox.SelectedItem).Id),
                dataFilter,
                rep,
                org,
                (WaterObjReport.Type)((DicItem)typeComboBox.SelectedItem).Id,
                viewTextBox.Text,
                danger1CheckBox.Checked ? danger1TextBox.Text : "",
                danger2CheckBox.Checked ? danger2TextBox.Text : "",
                periodDateS.Value,
                author1checkBox.Checked ? ((DicItem)author1ComboBox.SelectedItem).Name : "",
                author2checkBox.Checked ? ((DicItem)author2ComboBox.SelectedItem).Name : "",
                author1checkBox.Checked ? ((DicItem)authorPos1ComboBox.SelectedItem).Name : "",
                author2checkBox.Checked ? ((DicItem)authorPos2ComboBox.SelectedItem).Name : ""
            );
            new FormSingleUC(new UCReport(waterObjReport), rep.NameFull, 600, 600).Show();
        }
    }
}
