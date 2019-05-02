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
using SOV.Common;
using SOV.Social;

namespace SOV.Amur.Meta
{
    public partial class UCCatalog : UserControl
    {
        public UCCatalog()
        {
            InitializeComponent();
        }
        public void Clear()
        {
            Id = -1;
            sitesCombo.SelectedIndex = -1;
            varsCombo.SelectedIndex = -1;
            methodsCombo.SelectedIndex = -1;
            sourcesCombo.SelectedIndex = -1;
            offsCombo.SelectedIndex = -1;
            offValue.Text = string.Empty;
        }

        int Id { get { return int.Parse(idTextBox.Text); } set { idTextBox.Text = value.ToString(); } }
        public Catalog Catalog
        {
            get
            {
                try
                {
                    return new Catalog(Id,
                        ((DicItem)sitesCombo.SelectedItem).Id,
                        ((Variable)varsCombo.SelectedItem).Id,
                        ((Method)methodsCombo.SelectedItem).Id,
                        ((LegalEntity)sourcesCombo.SelectedItem).Id,
                        ((OffsetType)offsCombo.SelectedItem).Id,
                        double.Parse(offValue.Text)
                    );
                }
                catch
                {
                    return null;
                }
            }
            set
            {
                Clear();

                if ((object)value != null)
                {
                    Id = value.Id;

                    foreach (DicItem item in sitesCombo.Items)
                    {
                        if (item.Id == value.SiteId)
                        {
                            sitesCombo.SelectedItem = item;
                            break;
                        }
                    }
                    foreach (Variable item in varsCombo.Items)
                    {
                        if (item.Id == value.VariableId)
                        {
                            varsCombo.SelectedItem = item;
                            break;
                        }
                    }
                    foreach (Method item in methodsCombo.Items)
                    {
                        if (item.Id == value.MethodId)
                        {
                            methodsCombo.SelectedItem = item;
                            break;
                        }
                    }
                    foreach (SOV.Social.LegalEntity item in sourcesCombo.Items)
                    {
                        if (item.Id == value.SourceId)
                        {
                            sourcesCombo.SelectedItem = item;
                            break;
                        }
                    }
                    foreach (OffsetType item in offsCombo.Items)
                    {
                        if (item.Id == value.OffsetTypeId)
                        {
                            offsCombo.SelectedItem = item;
                            break;
                        }
                    }
                    offValue.Text = value.OffsetValue.ToString();
                }
            }
        }

        private void UCCatalog_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                sitesCombo.Items.AddRange(Meta.Site.ToDicItemList(SiteRepository.GetCash(), 2, SiteTypeRepository.GetCash()).OrderBy(x => x.Name).ToArray());
                varsCombo.Items.AddRange(VariableRepository.GetCash().OrderBy(x => x.NameRus).ToArray());
                methodsCombo.Items.AddRange(MethodRepository.GetCash().OrderBy(x => x.Name).ToArray());
                sourcesCombo.Items.AddRange(Social.LegalEntityRepository.GetCash().OrderBy(x => x.NameRus).ToArray());
                offsCombo.Items.AddRange(OffsetTypeRepository.GetCash().OrderBy(x => x.Name).ToArray()); 
            }
        }
    }
}
