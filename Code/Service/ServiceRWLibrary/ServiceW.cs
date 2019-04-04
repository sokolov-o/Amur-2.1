using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using FERHRI.Amur;
using FERHRI.Amur.Meta;
using FERHRI.Amur.Data;

namespace FERHRI.Amur.Service
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public partial class Service : IService
    {
        #region DATAP
        public void SaveDataPRole(long hSvc, long dvId, int roleId, bool isAQCApplied)
        {
            DataManagerDataP(hSvc).AQCRepository.InsertDataValueAQC(dvId, roleId, isAQCApplied);
        }
        #endregion DATAP

        #region DATA
        public long SaveValue(long hSvc, int catalogId, DateTime dateUTC, DateTime dateLOC, double value, byte flagAQC = 0, long? dataSourceId = null)
        {
            return DataManagerData(hSvc).DataValueRepository.Insert(catalogId, dateUTC, dateLOC, value, flagAQC = 0, dataSourceId);
        }
        public long SaveDataValue(long hSvc, DataValue dv)
        {
            return DataManagerData(hSvc).DataValueRepository.Insert(dv);
        }
        public long SaveDataSource(long hSvc, DataSource dataSource)
        {
            return DataManagerData(hSvc).DataSourceRepository.Insert(dataSource);
        }
        public void SaveDataValueXSource(long hSvc, int dataValueId, long dataSourceId)
        {
            DataManagerData(hSvc).DataSourceRepository.InsertDataValueXSource(dataValueId, dataSourceId);
        }
        public Catalog SaveCatalog(long hSvc, Catalog catalog)
        {
            return DataManagerMeta(hSvc).CatalogRepository.Insert(catalog);
        }
        /// <summary>
        /// Запись набора значений.
        /// </summary>
        /// <param name="hSvc">Дескриптор сессии.</param>
        /// <param name="dvs">Набор значений для записи.</param>
        /// <param name="portionLength">Размер порции записи в количестве элементов массива dvs. 
        /// <param name="dataSource">Источник данных или null, если его нет.</param>
        /// Например, в массиве 100 элементов, а пишем по 10 (portionLength = 10).</param>
        /// <returns></returns>
        public void SaveDataValueList(long hSvc, List<DataValue> dvs, long? dataSourceId)
        {
            DataManagerData(hSvc).DataValueRepository.Insert(dvs, dataSourceId);
        }
        public void SaveDataForecastList(long hSvc, List<DataForecast> df)
        {
            DataManagerData(hSvc).DataForecastRepository.Insert(df);
        }
        #endregion DATA

        #region META

        public int SaveStation(long hSvc, Station station)
        {
            return DataManagerMeta(hSvc).StationRepository.Insert(station);
        }
        public int SaveSite(long hSvc, Site site)
        {
            return DataManagerMeta(hSvc).SiteRepository.Insert(site);
        }
        public void SaveSiteAttribute(long hSvc, EntityAttrValue eav)
        {
            DataManagerMeta(hSvc).EntityAttrRepository.InsertUpdateValue("site", eav.EntityId, eav.AttrTypeId, (DateTime)eav.DateS, eav.Value);
        }
        public void UpdateStation(long hSvc, Station station)
        {
            DataManagerMeta(hSvc).StationRepository.Update(station);
        }
        #endregion META

        #region PARSER
        public void SaveParserSysObjLastStartParam(long hSvc, int sysObjId, string lastStartParam)
        {
            DataManagerParser(hSvc).SysObjRepository.UpdateLastStartParam(sysObjId, lastStartParam);
        }
        #endregion PARSER
    }
}
