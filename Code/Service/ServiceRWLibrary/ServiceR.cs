using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using FERHRI.Amur.Meta;
using FERHRI.Amur.Data;
using FERHRI.Amur.DataP;

namespace FERHRI.Amur.Service
{
    //[ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public partial class Service : IService
    {
        #region META
        public Method GetMethod(long hSvc, int methodId)
        {
            return DataManagerMeta(hSvc).MethodRepository.Select(new List<int>(new int[] { methodId }))[0];
        }
        public Source GetSource(long hSvc, int sourceId)
        {
            return DataManagerMeta(hSvc).SourceRepository.Select(new List<int>(new int[] { sourceId }))[0];
        }
        /// <summary>
        /// Получить набор переменных по их кодам.
        /// </summary>
        /// <returns>Набор переменных.</returns>
        public List<Variable> GetVariablesByList(long hSvc, List<int> variableIdList)
        {
            if (variableIdList == null || variableIdList.Count == 0)
                return null;
            List<Variable> ret = DataManagerMeta(hSvc).VariableRepository.Select(variableIdList);
            return ret;
        }
        /// <summary>
        /// Получить набор всех переменных.
        /// </summary>
        /// <returns>Набор переменных.</returns>
        public List<Variable> GetVariablesAll(long hSvc)
        {
            return DataManagerMeta(hSvc).VariableRepository.Select();
        }

        /// <summary>
        /// Получить станцию по её индексу/коду.
        /// </summary>
        /// <returns>Станция с указанным кодом.</returns>
        public Station GetStationByIndex(long hSvc, string stationIndex)
        {
            return DataManagerMeta(hSvc).StationRepository.Select(stationIndex);
        }
        /// <summary>
        /// Получить набор станций по их кодам.
        /// </summary>
        /// <returns>Набор станций.</returns>
        public List<Station> GetStationsByList(long hSvc, List<int> stationIdList)
        {
            return DataManagerMeta(hSvc).StationRepository.Select(stationIdList);
        }
        /// <summary>
        /// Получить список всех типов станций.
        /// </summary>
        /// <returns>Список типов станций.</returns>
        public List<StationType> GetStationTypes(long hSvc)
        {
            return DataManagerMeta(hSvc).StationTypeRepository.Select();
        }
        /// <summary>
        /// Список наблюдательных пунктов указанной станции.
        /// </summary>
        /// <param name="stationId">Код станции.</param>
        /// <param name="siteTypeId">Тип пункта.</param>
        /// <returns></returns>
        public List<Site> GetSitesByStation(long hSvc, int stationId, int? siteTypeId = null)
        {
            return DataManagerMeta(hSvc).SiteRepository.Select(stationId, siteTypeId);
        }
        /// <summary>
        /// Список наблюдательных пунктов.
        /// </summary>
        /// <returns></returns>
        public List<Site> GetSitesByList(long hSvc, List<int> siteIdList)
        {
            return DataManagerMeta(hSvc).SiteRepository.Select(siteIdList);
        }
        public List<Site> GetSitesByType(long hSvc, int siteTypeId)
        {
            return DataManagerMeta(hSvc).SiteRepository.SelectByType(siteTypeId);
        }
        public List<Site> GetSitesByGroup(long hSvc, int siteGroupId)
        {
            return DataManagerMeta(hSvc).SiteGroupRepository.SelectGroupFK(siteGroupId).SiteList;
        }
        /// <summary>
        /// Получить список всех типов наблюдательных пунктов.
        /// </summary>
        /// <returns>Список типов пунктов.</returns>
        public List<SiteType> GetSiteTypes(long hSvc)
        {
            return DataManagerMeta(hSvc).SiteTypeRepository.Select();
        }
        public EntityAttrValue GetSiteAttrValue(long hSvc, int siteId, int siteAttrTypeId, DateTime dateActual)
        {
            return DataManagerMeta(hSvc).EntityAttrRepository.SelectAttrValue("site", siteId, siteAttrTypeId, dateActual);
        }
        public List<EntityAttrValue> GetSitesAttrValue(long hSvc, List<int> siteId, int siteAttrTypeId, DateTime dateActual)
        {
            return DataManagerMeta(hSvc).EntityAttrRepository.SelectAttrValuesActual("site", siteId, new List<int>(new int[] { siteAttrTypeId }), dateActual);
        }
        public List<EntityAttrValue> GetSitesAttrValues(long hSvc, List<int> siteId, List<int> attrTypeId = null, DateTime? dateActual = null)
        {
            return DataManagerMeta(hSvc).EntityAttrRepository.SelectAttrValuesActual("site", siteId, attrTypeId, dateActual);
        }
        public CatalogFilter GetCatalogFilter(long hSvc, int SiteGroupId, List<int> VariablesId, int? OffsetTypeId, double? OffsetValue, int MethodId, int SourceId)
        {
            //return DataManagerMeta(hSvc).GetCatalogFilter(SiteGroupId, VariablesId, OffsetTypeId, OffsetValue, MethodId, SourceId);
            return new CatalogFilter(null, null, null, null, null, null);
        }

        #endregion META

        #region DATA
        /// <summary>
        /// Получить записи каталога (не климатические).
        /// </summary>
        /// <param name="hSvc"></param>
        /// <param name="siteId">Список кодов пунктов или все пункты, если null.</param>
        /// <param name="varId">Список кодов переменных или все, если null.</param>
        /// <param name="offsetTypeId">Код смещения или все, если null.</param>
        /// <param name="methodId">Код метода или все, если null.</param>
        /// <param name="sourceId">Код источника или все, если null.</param>
        /// <param name="offsetValue">Значение смещения или все, если null.</param>
        /// <returns>Список записей каталога, удовлетворяющих условию отбора.</returns>
        public List<Catalog> GetCatalogList(long hSvc, List<int> siteId, List<int> varId, int? offsetTypeId, int? methodId, int? sourceId, double? offsetValue)
        {
            return DataManagerMeta(hSvc).CatalogRepository.Select(siteId, varId,
                offsetTypeId, methodId, sourceId, offsetValue);
        }
        public Catalog GetCatalog(long hSvc, int siteId, int varId, int offsetTypeId, int methodId, int sourceId, double offsetValue)
        {
            List<Catalog> ret = GetCatalogList(hSvc,
                new List<int>(new int[] { siteId }),
                new List<int>(new int[] { varId }),
                offsetTypeId, methodId, sourceId, offsetValue);

            return (ret.Count == 1) ? ret[0] : null;
        }

        public List<Catalog> GetCatalogListById(long hSvc, List<int> ctlId)
        {
            if (ctlId == null || ctlId.Count == 0)
                return new List<Catalog>();
            List<Catalog> ret = DataManagerMeta(hSvc).CatalogRepository.Select(ctlId);
            return ret;
        }
        /// <summary>
        /// Набор данных, соответствующих указанному фильтру.
        /// </summary>
        /// <param name="dateSLOC">Начало периода отбора данных.</param>
        /// <param name="dateFLOC">Окончание периода отбора данных.</param>
        /// <param name="siteId">Код пункта.</param>
        /// <param name="variableId">Код переменной.</param>
        /// <param name="offsetTypeId">Код типа смещения.</param>
        /// <param name="offsetValue">Значение смещения.</param>
        /// <param name="isOneValue">Отбирать только актуальные значения?</param>
        /// <param name="isSelectDeleted">Отбирать значения, помеченные как удалённые?</param>
        /// <param name="methodId">Код метода.</param>
        /// <param name="sourceId">Код источника.</param>
        /// <param name="flagAQC">Флаг АКК.</param>
        /// <returns>Набор данных.</returns>
        public List<DataValue> GetDataValues(long hSvc,
            DateTime dateSLOC, DateTime dateFLOC,
            List<int> siteId, List<int> variableId, int? offsetTypeId, double? offsetValue,
            bool isOneValue, bool isSelectDeleted = false,
            int? methodId = null, int? sourceId = null, byte? flagAQC = null)
        {
            return DataManagerData(hSvc).DataValueRepository.SelectA(
             dateSLOC, dateFLOC,
             siteId, variableId, offsetTypeId, offsetValue,
             isOneValue, isSelectDeleted,
             methodId, sourceId, flagAQC);
        }
        public List<DataForecast> GetDataForecasts(long hSvc, int catalogId, DateTime dateFcsS, DateTime dateFcsF, double? fcsLag, bool isDateFcs)
        {
            return DataManagerData(hSvc).DataForecastRepository.SelectDataForecasts(new List<int>(new int[] { catalogId }), dateFcsS, dateFcsF,
                fcsLag.HasValue ? new List<double>(new double[] { (double)fcsLag }) : null, isDateFcs);
        }
        /// <summary>
        /// ДЛЯ ОТЛАДКИ СЕРВИСА!
        /// </summary>
        /// <param name="variableId"></param>
        /// <param name="offsetTypeId"></param>
        /// <param name="offsetValue"></param>
        /// <param name="isOneValue"></param>
        /// <param name="isSelectDeleted"></param>
        /// <param name="methodId"></param>
        /// <param name="sourceId"></param>
        /// <param name="flagAQC"></param>
        /// <returns></returns>
        public List<DataValue> TestDataValues(long hSvc)
        {
            int hoursBackward = 5;
            List<int> varIds = new List<int>(new int[] { 2, 47 });
            DateTime dateSLOC, dateFLOC = new DateTime(2014, 6, 1);
            dateSLOC = dateFLOC.AddHours(-hoursBackward);

            int? offsetTypeId = null; double? offsetValue = null;
            bool isOneValue = true; bool isSelectDeleted = false;
            int? methodId = null; int? sourceId = null; byte? flagAQC = null;

            List<DataValue> ret = DataManagerData(hSvc).DataValueRepository.SelectA(
             dateSLOC, dateFLOC,
             null, varIds,
             offsetTypeId, offsetValue,
             isOneValue, isSelectDeleted,
             methodId, sourceId, flagAQC);

            return ret;
        }

        #endregion DATA

        #region DATAP
        public List<AQCDataValue> GetDataPDataValueAQC(long hSvc, long dvId)
        {
            return DataManagerDataP(hSvc).AQCRepository.SelectDataValueAQC(dvId);
        }
        #endregion DATAP

        #region PARSER
        public Parser.SysObj GetParserSysObj(long hSvc, int sysObjId)
        {
            return DataManagerParser(hSvc).SysObjRepository.Select(new List<int>(new int[] { sysObjId }))[0];
        }
        public List<Parser.SysParsersXSites> GetParserSysParsersXSites(long hSvc, int sysObjId)
        {
            return DataManagerParser(hSvc).SysParsersXSitesRepository.Select(sysObjId);
        }
        public List<Parser.SysParsersParams> GetParserSysParsersParams(long hSvc, List<int> sysParsersParamsSetIds)
        {
            return DataManagerParser(hSvc).SysParsersParamsRepository.Select(sysParsersParamsSetIds);
        }

        #endregion PARSER
    }
}
