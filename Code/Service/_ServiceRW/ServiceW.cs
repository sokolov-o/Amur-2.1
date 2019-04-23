using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SOV.Amur;
using SOV.Amur.Meta;
using SOV.Amur.Data;

namespace SOV.Amur.Service
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public partial class Service : IService
    {
        #region DATAP
        ///////// <summary>
        ///////// Сохранить правило обработки данных.
        ///////// </summary>
        ///////// <param name="hSvc"></param>
        ///////// <param name="dvId"></param>
        ///////// <param name="roleId"></param>
        ///////// <param name="isAQCApplied"></param>
        public void SaveDataPRole(long hSvc, long dvId, int roleId, bool isAQCApplied)
        {
            DataManagerDataP(hSvc).AQCRepository.InsertDataValueAQC(dvId, roleId, isAQCApplied);
        }
        #endregion DATAP

        #region DATA
        /// <summary>
        /// Актуализация значения - Установить значению с кодом _id ЕДИНСТВЕННЫЙ СРЕДИ ВСЕХ ЗНАЧЕНИЙ флаг 40 (Подтверждено специалистом)..
        /// 1) Всем значения с флагом 40 (Подтверждено специалистом) переопределить флаг в 1 (Успешный критконтроль).
        /// 2) Установить значению с кодом _id флаг 40 (Подтверждено специалистом).
        /// OSokolov@SOV.ru 2017.01
        /// </summary>
        /// <param name="hSvc">Ручка сервиса.</param>
        /// <param name="dataValueId">Код актуализируемого значения.</param>
        public void ActualizeDataValue(long hSvc, long dataValueId)
        {
            DataManagerData(hSvc).DataValueRepository.Actualize(dataValueId);
        }
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
            long id = DataManagerData(hSvc).DataSourceRepository.Insert(dataSource);
            System.IO.File.AppendAllText(_logFilePath, string.Format("SaveDataSource {0} {1}\n", id, DateTime.Now));
            return id;
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
        /// <param name="dataSourceId">Код источника данных или null, если его нет.</param>
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
        /// <summary>
        /// Записать элемент справочника кодов значений для переменной.
        /// </summary>
        public void SaveVariableCode(long hSvc, VariableCode vc)
        {
            DataManagerMeta(hSvc).VariableCodeRepository.Insert(vc);
        }

        public int SaveSite(long hSvc, Site site)
        {
            return DataManagerMeta(hSvc).SiteRepository.Insert(site);
        }
        public void SaveSiteAttribute(long hSvc, EntityAttrValue eav)
        {
            DataManagerMeta(hSvc).EntityAttrRepository.InsertUpdateValue("site", eav.EntityId, eav.AttrTypeId, (DateTime)eav.DateS, eav.Value);
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
