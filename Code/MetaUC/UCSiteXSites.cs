using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOV.Amur.Meta
{
    public partial class UCSiteXSites : UserControl
    {
        public UCSiteXSites()
        {
            InitializeComponent();
        }

        public int SiteNum1Or2
        {
            get { return dgv.Tag == null ? -1 : (int)dgv.Tag; }
            set
            {
                dgv.Tag = value;

                _iColSite = -1;
                _iColRelation = -1;
                dgv.Columns[0].HeaderText = "---";
                dgv.Columns[1].HeaderText = "---";

                switch (SiteNum1Or2)
                {
                    case 1: _iColSite = 1; _iColRelation = 0; break;
                    case 2: _iColSite = 0; _iColRelation = 1; break;
                    default:
                        break;
                }
                dgv.Columns[_iColRelation].HeaderText = "Отношение";
                dgv.Columns[_iColSite].HeaderText = "Пункт";

            }
        }

        int _iColSite, _iColRelation;
        public void Clear()
        {
            dgv.Rows.Clear();
        }
        public int ParentSiteId { get; set; }
        public void Fill(int siteId)
        {
            Fill(1, siteId);
        }
        public void Fill(int siteNum1Or2, int siteId)
        {
            dgv.Rows.Clear();
            SiteNum1Or2 = siteNum1Or2;
            ParentSiteId = siteId;

            List<SiteXSite> siteXsites = Meta.DataManager.GetInstance().SiteXSiteRepository.Select(siteId, SiteNum1Or2);
            if (siteXsites.Count == 0) return;

            List<SiteXSiteType> sxsTypes = Meta.DataManager.GetInstance().SiteXSiteRepository.SelectRelationTypes();

            List<Site> sites;

            switch (SiteNum1Or2)
            {
                case 1:
                    sites = Meta.DataManager.GetInstance().SiteRepository.Select(siteXsites.Select(x => x.SiteId2).Distinct().ToList());
                    break;
                case 2:
                    sites = Meta.DataManager.GetInstance().SiteRepository.Select(siteXsites.Select(x => x.SiteId1).Distinct().ToList());
                    break;
                default:
                    throw new Exception("Неизвестное положение пункта для определения отношения.");
            }

            foreach (var item in siteXsites)
            {
                DataGridViewRow row = dgv.Rows[dgv.Rows.Add()];
                row.Cells[_iColRelation].Value = sxsTypes.FirstOrDefault(x => x.Id == item.RelationTypeId).Name;
                row.Cells[_iColSite].Value = sites.FirstOrDefault(x => x.Id == ((SiteNum1Or2 == 1) ? item.SiteId2 : item.SiteId1))
                    .GetName(1, SiteTypeRepository.GetCash());
                row.Tag = item;
            }
        }

        private void AddToolStripButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not implemented yet. OSokolov@SOV.ru, 2016/12/12");
        }
    }
}
