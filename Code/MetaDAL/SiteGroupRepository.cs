using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using FERHRI.Common;

namespace FERHRI.Amur.Meta
{
    public class SiteGroupRepository 
    {
        string THIS_ENTITY_NAME = "site_view";

        Common.ADbNpgsql _db;
        internal SiteGroupRepository(Common.ADbNpgsql db)
        {
            _db = db;
        }
        public SiteGroup SelectGroup(int siteGroupId)
        {
            List<EntityGroup> ret = DataManager.GetInstance(_db.ConnectionString).EntityGroupRepository.SelectGroups(new List<int>(new int[] { siteGroupId }));
            return ret.Count == 1 ? new SiteGroup(ret[0]) : null;
        }
        public SiteGroup SelectGroupFK(int siteGroupId)
        {
            SiteGroup ret = null;
            Dictionary<EntityGroup, List<int[]>> eg = DataManager.GetInstance(_db.ConnectionString).EntityGroupRepository.SelectGroupsFK(new List<int>(new int[] { siteGroupId }));
            if (eg.Count == 1)
            {
                ret = new SiteGroup(eg.Keys.ElementAt(0));
                FillFK(ret, eg.Values.ElementAt(0).Select(x => x[0]).ToList());
            }
            return ret;
        }
        private void FillFK(SiteGroup ret, List<int> siteIdList)
        {
            // SITE
            ret.SiteList = DataManager.GetInstance(_db.ConnectionString).SiteRepository.Select(siteIdList);

            // STATION
            List<int> stationIds = ret.SiteList.Select(x => x.StationId).Distinct().ToList();
            ret.StationList = DataManager.GetInstance(_db.ConnectionString).StationRepository.Select(stationIds);
        }
        /// <summary>
        /// Выбрать все группы пунктов наблюдений.
        /// </summary>
        /// <returns>Список групп.</returns>
        public List<SiteGroup> SelectGroups()
        {
            List<SiteGroup> ret = new List<SiteGroup>();
            EntityGroupRepository rep = DataManager.GetInstance(_db.ConnectionString).EntityGroupRepository;
            foreach (var item in rep.SelectGroups(THIS_ENTITY_NAME))
            {
                ret.Add(new SiteGroup(item));
            }
            return ret;
        }
        /// <summary>
        /// Выбрать все группы пунктов наблюдений с объектами по внешним ключам: пунктам и станциям.
        /// </summary>
        /// <returns>Список групп.</returns>
        public List<SiteGroup> SelectGroupsFK()
        {
            List<SiteGroup> ret = new List<SiteGroup>();
            EntityGroupRepository rep = DataManager.GetInstance(_db.ConnectionString).EntityGroupRepository;

            foreach (var item in rep.SelectGroupsFK(THIS_ENTITY_NAME))
            {
                SiteGroup sg = new SiteGroup(item.Key);
                FillFK(sg, item.Value.Select(x => x[0]).ToList());
                ret.Add(sg);
            }
            return ret;
        }

        public int InsertGroup(string groupName)
        {
            EntityGroupRepository rep = DataManager.GetInstance(_db.ConnectionString).EntityGroupRepository;
            return rep.InsertGroup(groupName, THIS_ENTITY_NAME);
        }

        public void UpdateGroup(int siteGroupId, string groupName)
        {
            EntityGroupRepository rep = DataManager.GetInstance(_db.ConnectionString).EntityGroupRepository;
            rep.UpdateGroup(siteGroupId, groupName);
        }

        public void DeleteSite(int siteGroupId, int siteId)
        {
            EntityGroupRepository rep = DataManager.GetInstance(_db.ConnectionString).EntityGroupRepository;
            rep.DeleteGroupItem(siteGroupId, siteId);
        }

        public void InsertSite(int groupId, int siteId)
        {
            EntityGroupRepository rep = DataManager.GetInstance(_db.ConnectionString).EntityGroupRepository;
            rep.InsertGroupEntity(groupId, siteId, 1);
        }

        public void DeleteGroup(int groupId)
        {
            EntityGroupRepository rep = DataManager.GetInstance(_db.ConnectionString).EntityGroupRepository;
            rep.DeleteGroup(groupId);
        }
    }
}
