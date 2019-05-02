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
    public partial class UCSitesSelect : UserControl
    {
        public UCSitesSelect()
        {
            InitializeComponent();

            UCList2.ShowId = UCList3.ShowId = false;
        }

        private void UCList3_Load(object sender, EventArgs e)
        {
            UCList3.SetDataSource(new List<IdName>() { new Common.IdName() { Id = -1, Name = "(ВСЕ)" } });

            UCList3.AddRange(SiteTypeRepository.GetCash().ToList<object>());
        }
        List<int> _ExcludeSites = new List<int>();
        /// <summary>
        /// Исключать указанные пункты из списка.
        /// </summary>
        /// <param name="siteId"></param>
        public List<int> ExcludeSites { get { return _ExcludeSites; } set { _ExcludeSites = value == null ? new List<int>() : value; } }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            UCList2.Clear();
            List<int> siteTypeIds = UCList3.GetSelectedItemsId();
            if (siteTypeIds.Count != 0)
            {
                List<Site> sites;
                if (siteTypeIds.IndexOf(-1) >= 0)
                {
                    sites = DataManager.GetInstance().SiteRepository.Select();
                }
                else
                {
                    sites = DataManager.GetInstance().SiteRepository.SelectByTypes(siteTypeIds);
                }
                sites.RemoveAll(x => ExcludeSites.Exists(y => y == x.Id));

                //List<Station> stations = DataManager.GetInstance().StationRepository.Select(sites.Select(x => x.StationId).Distinct().ToList());
                //List<StationType> siteTypes = DataManager.GetInstance().SiteTypeRepository.Select();

                //UCList2.AddRange(Meta.Site.ToDicItemList(sites, StationRepository.GetCash(), SiteTypeRepository.GetCash(), 2)
                //    .OrderBy(x => x.Name)
                //    .ToList());
                UCList2.AddRange(
                    sites.Select(x => new IdName() { Id = x.Id, Name = x.GetName(1, SiteTypeRepository.GetCash()) })
                    .OrderBy(x => x.Name)
                    .ToList<object>());
            }
        }

        internal List<object> GetSelectedDicItems()
        {
            return UCList2.GetSelectedItems();
        }

        internal void Clear()
        {
            UCList2.Clear();
        }

        internal void Remove(List<int> dicIds)
        {
            UCList2.RemoveById(dicIds);
        }
    }
}
