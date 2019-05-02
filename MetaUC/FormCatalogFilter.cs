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
    public partial class FormCatalogFilter : Form
    {
        public FormCatalogFilter(Sys.EntityInstanceValue filterSysAttrValue, string caption)
        {
            InitializeComponent();

            if (filterSysAttrValue != null)
                ucCatalogFilter.CatalogFilter = Meta.CatalogFilter.Parse(filterSysAttrValue.Value);

            if (caption != null)
                Text = caption;
        }

        public SOV.Amur.Meta.CatalogFilter CatalogFilter
        {
            set
            {
                ucCatalogFilter.CatalogFilter = value;
            }
            get
            {
                return ucCatalogFilter.CatalogFilter;
            }
        }
        public void ShowCatalogFilterTabs(bool tabSite, bool tabVariable, bool tabOffset, bool tabMethod, bool tabSource)
        {
            ucCatalogFilter.ShowTabs(tabSite, tabVariable, tabOffset, tabMethod, tabSource);
        }
        public bool ShowCatalogFilterToolStrip
        {
            set
            {
                ucCatalogFilter.ShowToolStrip = value;
            }
        }
        public bool SelectAllButtonsVisible
        {
            set
            {
                ucCatalogFilter.SelectAllButtonsVisible = value;
            }
        }
        bool _isLoaded = false;
        private void FormCatalogFilter_Load(object sender, EventArgs e)
        {
            if (!_isLoaded)
                ucCatalogFilter.Fill(
                    SitesOnly == null ? SiteRepository.GetCash() : SitesOnly,
                    VariablesOnly == null ? VariableRepository.GetCash() : VariablesOnly,
                    MethodsOnly == null ? MethodRepository.GetCash() : MethodsOnly,
                    SourcesOnly == null ? SOV.Social.DataManager.GetInstance().LegalEntityRepository.SelectAll() : SourcesOnly,
                    OffsetTypesOnly == null ? OffsetTypeRepository.GetCash() : OffsetTypesOnly
                );
            _isLoaded = true;
        }

        public List<Site> SitesOnly { get; set; }
        public List<Variable> VariablesOnly { get; set; }
        public List<Method> MethodsOnly { get; set; }
        public List<Social.LegalEntity> SourcesOnly { get; set; }
        public List<OffsetType> OffsetTypesOnly { get; set; }
        public bool ShowItemsTypeOfSetGroup
        {
            set
            {
                ucCatalogFilter.ShowItemTypeOfSetGroup = value;
            }
        }
        internal void SetItemsTypeOfSet(UCCatalogFilter0.EnumItemTypeOfSet enumItemTypeOfSet)
        {
            ucCatalogFilter.SetItemsTypeOfSet(enumItemTypeOfSet);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
