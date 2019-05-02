using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SOV.Common;

namespace SOV.Amur.Meta
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

            siteTypeComboBox.AddRange(SiteType.ToList<DicItem>(SiteTypeRepository.GetCash()));
        }
        Site _site = null;
        public Site Site
        {
            set
            {
                if (value == null)
                    _site = new Site();
                else
                {
                    _site = value;// new Site(){ Id=value.Id,  ParentId= value.StationId, value.SiteTypeId, value.SiteCode, value.Description);
                    stationUCTextBox.Value = value.ParentId.HasValue ? DataManager.GetInstance().SiteRepository.Select((int)value.ParentId) : null;
                }
                siteTypeComboBox.SelectedId = _site.TypeId;
                codeTtextBox.Text = _site.Code;
                descriptionTextBox.Text = _site.Description;

                Text = "Пункт " + _site.Id + (codeTtextBox.Text == null ? "" : codeTtextBox.Text);
            }
            get
            {
                return new Site()
                {
                    Id = _site.Id,
                    ParentId = stationUCTextBox.Value == null ? null : (int?)((Site)stationUCTextBox.Value).Id,
                    TypeId = (int)siteTypeComboBox.SelectedId,
                    Code = codeTtextBox.Text,
                    Description = descriptionTextBox.Text
                };
            }
        }
        public FormSite(Site site)
        {
            InitializeComponent();

            siteTypeComboBox.AddRange(SiteType.ToList<DicItem>(SiteTypeRepository.GetCash()));
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
                    SiteRepository.GetCash().OrderBy(x => x.Name).ToArray(),
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
