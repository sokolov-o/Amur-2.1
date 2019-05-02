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

                object o;
                o = ((List<IdName>)siteTypeBindingSource.DataSource).FirstOrDefault(x => x.Id == value.TypeId);
                siteTypeComboBox.SelectedIndex = o == null ? -1 : siteTypeComboBox.Items.IndexOf(o);

                o = ((List<IdName>)addrBindingSource.DataSource).FirstOrDefault(x => x.Id == value.AddrRegionId);
                regionComboBox.SelectedIndex = o == null ? -1 : regionComboBox.Items.IndexOf(o);

                o = ((List<IdName>)legalEntityBindingSource.DataSource).FirstOrDefault(x => x.Id == value.OrgId);
                orgComboBox.SelectedIndex = o == null ? -1 : orgComboBox.Items.IndexOf(o);
            }
        }

        private void FillDics()
        {
            if (!DesignMode)
            {
                siteTypeBindingSource.DataSource = SiteTypeRepository.GetCash().Select(x => new IdName() { Id = x.Id, Name = x.Name }).OrderBy(x => x.Name).ToList();
                addrBindingSource.DataSource = Social.AddrRepository.GetCash().Select(x => new IdName() { Id = x.Id, Name = x.Name }).OrderBy(x => x.Name).ToList();
                legalEntityBindingSource.DataSource = Social.LegalEntityRepository.GetCash().Where(x => x.Type == 'o').Select(x => new IdName() { Id = x.Id, Name = x.NameRus }).OrderBy(x => x.Name).ToList();
            }
        }

        private void UCSite_Load(object sender, EventArgs e)
        {
            FillDics();
        }
    }
}
