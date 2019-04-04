using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FERHRI.Common;

namespace FERHRI.Amur.Meta
{
    public partial class FormSite : Form
    {
        /// <summary>
        /// Новый сайт для станции/поста.
        /// </summary>
        /// <param name="stationId">Код поста.</param>
        public FormSite()
        {
            InitializeComponent();

            siteTypeComboBox.AddRange(StationType.ToList<DicItem>(StationTypeRepository.GetCash()));
        }
        Site _site = null;
        public Site Site
        {
            set
            {
                if (value == null)
                    _site = new Site(-1, -1, -1, null, null);
                else
                    _site = new Site(value.Id, value.StationId, value.SiteTypeId, value.SiteCode, value.Description);

                siteTypeComboBox.SelectedId = _site.SiteTypeId;
                codeTtextBox.Text = _site.SiteCode;
                descriptionTextBox.Text = _site.Description;
                List<Station> stations = StationRepository.GetCash();
                stationUCTextBox.Value = stations.FirstOrDefault(x => x.Id == value.StationId);

                Text = "Пункт " + _site.Id + (codeTtextBox.Text == null ? "" : codeTtextBox.Text);
            }
            get
            {
                return new Site(
                    _site.Id,
                    //_site.StationId, 
                    ((Station)stationUCTextBox.Value).Id,
                    (int)siteTypeComboBox.SelectedId,
                    codeTtextBox.Text,
                    descriptionTextBox.Text);
            }
        }
        public FormSite(Site site)
        {
            InitializeComponent();

            siteTypeComboBox.AddRange(StationType.ToList<DicItem>(StationTypeRepository.GetCash()));
            Site = site;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormSite_Load(object sender, EventArgs e)
        {
        }
        FormListBox frm = null;
        private void stationUCTextBox_UCEditButtonPressedEvent()
        {
            if (frm == null)
            {
                frm = new FormListBox("Выберите станцию",
                    StationRepository.GetCash().OrderBy(x => x.NameRus).ToArray(),
                    "NameRusShort"
                    , false);
                frm.StartPosition = FormStartPosition.CenterScreen;
            }

            if (frm.ShowDialog() == DialogResult.OK)
            {
                List<object> stations = frm.GetSelectedItems();
                if (stations == null || stations.Count != 1)
                {
                    MessageBox.Show("Необходимо выбрать ОДНУ станцию, к которой относится пункт.");
                }
                else
                {
                    stationUCTextBox.Value = stations[0];
                }
            }
        }
    }
}
