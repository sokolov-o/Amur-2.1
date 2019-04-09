using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SOV.Common;

namespace SOV.Amur.Meta
{
    public partial class UCSite : UserControl
    {
        public UCSite()
        {
            InitializeComponent();
            FillDics();
        }
        public Site Site
        {
            get
            {
                return new Site()
                {
                    Id = string.IsNullOrEmpty(idTextBox.Text) ? IdClass.NaNId : int.Parse(idTextBox.Text),
                    Code = codeTextBox.Text,
                    Name = nameTextBox.Text,
                    TypeId = siteTypeComboBox.SelectedIndex >= 0 ? ((IdClass)siteTypeComboBox.SelectedItem).Id : IdClass.NaNId,
                    AddrRegionId = regionComboBox.SelectedIndex >= 0 ? (int?)((IdClass)regionComboBox.SelectedItem).Id : null,
                    OrgId = orgComboBox.SelectedIndex >= 0 ? (int?)((IdClass)orgComboBox.SelectedItem).Id : null
                };
            }
            set
            {
                if (value == null) value = new Meta.Site();

                idTextBox.Text = value.Id.ToString();
                codeTextBox.Text = value.Code;
                nameTextBox.Text = value.Name;
                nameEngTextBox.Text = "Не реализовано...";

                object o = SiteTypeRepository.GetCash().FirstOrDefault(x => x.Id == value.TypeId);
                siteTypeComboBox.SelectedIndex = o == null ? -1 : siteTypeComboBox.Items.IndexOf(o);

                o = value.AddrRegionId.HasValue ? Social.AddrRepository.GetCash().Find(x => x.Id == (int)value.AddrRegionId) : null;
                regionComboBox.SelectedIndex = o == null ? -1 : regionComboBox.Items.IndexOf(o);

                o = value.OrgId.HasValue ? Social.LegalEntityRepository.GetCash().FirstOrDefault(x => x.Id == (int)value.OrgId) : null;
                orgComboBox.SelectedIndex = o == null ? -1 : orgComboBox.Items.IndexOf(o);
            }
        }

        private void FillDics()
        {
            if (!DesignMode)
            {
                stationTypeBindingSource.DataSource = SiteTypeRepository.GetCash().Select(x => new IdName() { Id = x.Id, Name = x.Name }).OrderBy(x => x.Name).ToList();
                regionComboBox.DataSource = Social.AddrRepository.GetCash().OrderBy(x => x.Name).ToList();
                orgComboBox.DataSource = Social.LegalEntityRepository.GetCash().Where(x => x.Type == 'o').OrderBy(x => x.NameRus).ToList();
            }
        }
    }
}
