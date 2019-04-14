using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SOV.Social;

namespace SOV.Amur.Meta
{
    public partial class FormSiteFilter : Form
    {
        public FormSiteFilter()
        {
            InitializeComponent();

            Common.DicItem dici = new Common.DicItem(-1, "(Все)");
            List<Common.DicItem> dicis;

            dicis = (new List<Common.DicItem>(new Common.DicItem[] { dici }));
            dicis.AddRange(SiteType.ToList<Common.DicItem>(SiteTypeRepository.GetCash()));
            stationTypeComboBox.DataSource = dicis;
            stationTypeComboBox.SelectedIndex = 0;

            dicis = new List<Common.DicItem>(new Common.DicItem[] { dici });
            dicis.AddRange(SiteType.ToList<Common.DicItem>(SiteTypeRepository.GetCash()));
            siteTypeComboBox.AddRange(dicis);
            siteTypeComboBox.SelectedIndex = 0;

            Social.Addr[] ars = Social.DataManager.GetInstance().AddrRepository.Select().OrderBy(x => x.Name).ToArray();

            AddrComboBox.Items.Add(new Addr(-1, -1, "(Все)", null, null));
            AddrComboBox.Items.AddRange(ars);
            AddrComboBox.SelectedIndex = 0;

            orgComboBox.Items.Add(new Addr(-1, -1, "(Все)", null, null));
            orgComboBox.Items.AddRange(ars);
            orgComboBox.SelectedIndex = 0;
        }
        public SiteFilter SiteFilter
        {
            get
            {
                SiteFilter ret = new SiteFilter()
                {
                    AddrId = ((Addr)AddrComboBox.SelectedItem).Id,
                    TypeId = ((Common.DicItem)siteTypeComboBox.SelectedItem).Id,
                    CodeLike = stationCodeLikeTextBox.Text,
                    NameLike = stationNameLikeTextBox.Text,
                    OrgId = ((Addr)orgComboBox.SelectedItem).Id
                };
                return ret;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        public List<Site> GetFilteredSites()
        {
            if (this.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return Meta.DataManager.GetInstance().SiteRepository.Select(SiteFilter);
            }
            return null;
        }
    }
}
