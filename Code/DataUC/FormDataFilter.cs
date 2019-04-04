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
using SOV.Amur.Meta;
using SOV.Social;

namespace SOV.Amur.Data
{
    public partial class FormDataFilter : Form
    {
        public Common.User User { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dfSysAttrValue">Значение атрибута, в котором будет сохранено текуще состояние фильтра.</param>
        public FormDataFilter(Sys.EntityInstanceValue dataFilterSAV = null)
        {
            InitializeComponent();

            Meta.DataManager dm = Meta.DataManager.GetInstance();
            Social.DataManager dmSocial = Social.DataManager.GetInstance();

            ucCatalogFilter.Fill(
                SiteRepository.GetCash(),
                VariableRepository.GetCash(),
                MethodRepository.GetCash(),
                LegalEntityRepository.GetCash(),
                OffsetTypeRepository.GetCash()
            );

            ucCatalogFilter.CatalogFilter = null;
            DataFilterSAV = dataFilterSAV;
        }
        Sys.EntityInstanceValue _DataFilterSAV;
        public Sys.EntityInstanceValue DataFilterSAV
        {
            get
            {
                return _DataFilterSAV;
            }
            set
            {
                _DataFilterSAV = value;
                saveFilterButton.Visible = false;
                if (value != null)
                {
                    saveFilterButton.Visible = true;
                    DataFilter = DataFilter.Parse(value.Value);
                }
            }
        }
        TextBox daysBeforeTextBox = new TextBox();

        public Meta.DataFilter DataFilter
        {
            get
            {
                return new DataFilter(
                    ucDateTimePeriod.DateTimePeriod,
                    FlagAQC,
                    isActualValueOnlyCheckBox.Checked,
                    selectDeletedDataValuesCheckBox.Checked,
                    refSiteCheckBox.Checked,
                    dateLOCRadioButton.Checked,

                    ucCatalogFilter.CatalogFilter
                );
            }
            set
            {
                ucDateTimePeriod.DateTimePeriod = value.DateTimePeriod;
                FlagAQC = value.FlagAQC;
                isActualValueOnlyCheckBox.Checked = value.IsActualValueOnly;
                selectDeletedDataValuesCheckBox.Checked = value.IsSelectDeleted;
                refSiteCheckBox.Checked = value.IsRefSiteData;

                ucCatalogFilter.CatalogFilter = value.CatalogFilter;
            }
        }
        Meta.DataFilter _dataFilterPrev = null;
        private void FormDataFilter_Load(object sender, EventArgs e)
        {
            _dataFilterPrev = DataFilter;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_dataFilterPrev != null)
                this.DataFilter = _dataFilterPrev;
            Close();
        }

        private byte? FlagAQC
        {
            get
            {
                return (string.IsNullOrEmpty(flagAQCTextBox.Text)) ? null : (byte?)byte.Parse(flagAQCTextBox.Text);
            }
            set
            {
                flagAQCTextBox.Text = value.HasValue ? value.ToString() : null;
            }
        }

        bool _SiteFilterEnabled;
        public bool SiteFilterEnabled
        {
            get
            {
                return _SiteFilterEnabled;
            }
            set
            {
                ucCatalogFilter.ShowTabs(false, true, true, true, true);
                _SiteFilterEnabled = value;
            }
        }

        private void saveFilterButton_Click(object sender, EventArgs e)
        {
            DataFilterSAV.Value = DataFilter.ToString();
        }
    }
}
