using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOV.Amur.Meta
{
    public partial class FormSitesAttrs : Form
    {
        public FormSitesAttrs()
        {
            InitializeComponent();
        }

        private void ucSiteObjects_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ucSites.EAVEdited.Count > 0)
            {
                if (MessageBox.Show("Имеются несохранённые изменения данных. Вы уверены, что хотите закрыть форму и НЕ СОХРАНЯТЬ изменения?", "Запрос сохранения изменений", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
            }
            Close();
        }
        static string DATE_FORMAT = "dd.MM.yyyy";

        private void FormSitesAttrs_Load(object sender, EventArgs e)
        {
            dateActualTextBox.Text = DateTime.Today.ToString(DATE_FORMAT);

            List<SiteAttrType> sat = DataManager.GetInstance().SiteAttrTypeRepository.Select().OrderBy(x => x.Name).ToList();
            foreach (SiteAttrType item in sat)
            {
                ToolStripItem tsi = siteAttrsDropDown.DropDownItems.Add(item.Name);
                tsi.Tag = item;
                tsi.Click += new EventHandler(SiteAttrEventHandler);
            }
            throw new NotImplementedException();
            //////ucEntityAttrValue.EntityAttrTypes = SiteAttrType.ToEntityAttrTypeList(sat);
            //////ucEntityAttrValue.UCEnableEntityAttrTypeComboBox = false;
        }

        internal void SiteAttrEventHandler(object sender, EventArgs e)
        {
            ToolStripItem tsi = (ToolStripItem)sender;
            SiteAttrType sat = (SiteAttrType)tsi.Tag;

            if (SiteAttrTypesSelected.FirstOrDefault(x => x.Id == sat.Id) == null)
            {
                SiteAttrTypesSelected.Add(sat);
                tsi.Font = new Font(tsi.Font, FontStyle.Bold);
            }
            else
            {
                SiteAttrTypesSelected.Remove(sat);
                tsi.Font = new Font(tsi.Font, FontStyle.Regular);
            }
        }

        void FillSites()
        {
            if (DateActual.HasValue)
            {
                ucSites.SiteAttrTypes = SiteAttrTypesSelected;
                ucSites.SiteAttrDateActual = (DateTime)DateActual;
                ucSites.Fill(ucSites.SiteGroupId);
            }
        }
        List<SiteAttrType> SiteAttrTypesSelected = new List<SiteAttrType>();
        public DateTime? DateActual
        {
            get
            {
                DateTime ret;
                if (!DateTime.TryParse(dateActualTextBox.Text, out ret))
                {
                    MessageBox.Show("Некорректный формат даты. Введите дату в формате " + DATE_FORMAT, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
                return ret;
            }
        }

        private void refreshSitesButton_Click(object sender, EventArgs e)
        {
            FillSites();
        }

        private void saveEAVButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
            //////DataManager.GetInstance().EntityAttrRepository.InsertUpdateValue("site", ucEntityAttrValue.EntityAttrValue);
        }

        private void ucSites_UCEntityAttrValueChangedEvent(string siteName, EntityAttrValue eav)
        {
            throw new NotImplementedException();
            try
            {
                eavGroupBox.Text = string.IsNullOrEmpty(siteName) ? "NULL" : siteName;
                //////ucEntityAttrValue.EntityAttrValue = eav;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
    }
}
