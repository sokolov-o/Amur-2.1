using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FERHRI.Common;

namespace FERHRI.Amur.Meta
{
    public partial class UCStation : UserControl
    {
        public UCStation()
        {
            InitializeComponent();
            FillDics();
        }
        public Station Station
        {
            get
            {
                return new Station
                    (
                    string.IsNullOrEmpty(idTextBox.Text) ? IdClass.NaNId : int.Parse(idTextBox.Text),
                    codeTextBox.Text,
                    nameTextBox.Text,
                    stationTypeComboBox.SelectedIndex >= 0 ? ((IdClass)stationTypeComboBox.SelectedItem).Id : IdClass.NaNId,
                    nameEngTextBox.Text,
                    regionComboBox.SelectedIndex >= 0 ? (int?)((IdClass)regionComboBox.SelectedItem).Id : null,
                    orgComboBox.SelectedIndex >= 0 ? (int?)((IdClass)orgComboBox.SelectedItem).Id : null
                    );
            }
            set
            {
                if (value == null) value = new Meta.Station();

                idTextBox.Text = value.Id.ToString();
                codeTextBox.Text = value.Code;
                nameTextBox.Text = value.Name;
                nameEngTextBox.Text = value.NameEng;

                object o = StationTypeRepository.GetCash().FirstOrDefault(x => x.Id == value.TypeId);
                stationTypeComboBox.SelectedIndex = o == null ? -1 : stationTypeComboBox.Items.IndexOf(o);
                //stationTypeBindingSource.Position = stationTypeBindingSource.Find("Id", value.TypeId);

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
                //stationTypeComboBox.DataSource = StationTypeRepository.GetCash().OrderBy(x => x.Name).ToList();
                stationTypeBindingSource.DataSource = StationTypeRepository.GetCash().Select(x => new IdName() { Id = x.Id, Name = x.Name }).OrderBy(x => x.Name).ToList();
                regionComboBox.DataSource = Social.AddrRepository.GetCash().OrderBy(x => x.Name).ToList();
                orgComboBox.DataSource = Social.LegalEntityRepository.GetCash().Where(x => x.Type == 'o').OrderBy(x => x.NameRus).ToList();
            }
        }
    }
}
