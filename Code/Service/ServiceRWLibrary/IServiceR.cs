using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using FERHRI.Amur.Meta;
using FERHRI.Amur.Data;
using FERHRI.Amur.DataP;

namespace FERHRI.Amur.Service
{
    /// <summary>
    /// 
    /// Интерфейс управления обработкой данных БД "Амур", ДВНИГМИ
    /// 
    /// ДВНИГМИ 2016, OSokolov@ferhri.ru
    /// </summary>
    [ServiceContract]
    public partial interface IService
    {
        #region COMMON
        /// <summary>
        /// Открытие рабочей сессии.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        /// <param name="password">Пароль.</param>
        /// <returns>Идентификатор сессии - целое число большее нуля.</returns>
        [OperationContract]
        long Open(string userName, string password);
        /// <summary>
        /// Закрытие рабочей сессии по её идентификатору.
        /// </summary>
        /// <param name="hSvc">Идентификатор сессии, полученный методом Open.</param>
        [OperationContract]
        void Close(long hSvc);
        #endregion

        #region META

        [OperationContract]
        List<EntityAttrValue> GetSitesAttrValues(long hSvc, List<int> siteId, List<int> attrTypeId = null, DateTime? dateActual = null);
        [OperationContract]
        Method GetMethod(long hSvc, int methodId);
        [OperationContract]
        Source GetSource(long hSvc, int sourceId);
        [OperationContract]
        CatalogFilter GetCatalogFilter(long hSvc, int SiteGroupId, List<int> VariablesId, int? OffsetTypeId, double? OffsetValue, int MethodId, int SourceId);
        [OperationContract]
        EntityAttrValue GetSiteAttrValue(long hSvc, int siteId, int siteAttrTypeId, DateTime dateActual);
        [OperationContract]
        List<EntityAttrValue> GetSitesAttrValue(long hSvc, List<int> siteId, int siteAttrTypeId, DateTime dateActual);
        [OperationContract]
        List<Site> GetSitesByType(long hSvc, int siteTypeId);
        [OperationContract]
        List<Site> GetSitesByGroup(long hSvc, int siteGroupId);
        /// <summary>
        /// Получить станцию по её индексу/коду.
        /// </summary>
        /// <returns>Станция с указанным кодом.</returns>
        [OperationContract]
        Station GetStationByIndex(long hSvc, string index);
        /// <summary>
        /// Получить станции по списку id станций.
        /// </summary>
        /// <returns>Станция с указанным кодом.</returns>
        [OperationContract]
        List<Station> GetStationsByList(long hSvc, List<int> stationIdList);
        [OperationContract]
        List<StationType> GetStationTypes(long hSvc);
        [OperationContract]
        List<Site> GetSitesByStation(long hSvc, int stationId, int? siteTypeId = null);
        [OperationContract]
        List<SiteType> GetSiteTypes(long hSvc);
        [OperationContract]
        List<Site> GetSitesByList(long hSvc, List<int> siteIdList);
        /// <summary>
        /// Получить набор станций по их кодам.
        /// </summary>
        /// <returns>Набор станций.</returns>
        [OperationContract]
        List<Variable> GetVariablesByList(long hSvc, List<int> variableIdList);
        /// <summary>
        /// Получить набор всех переменных.
        /// </summary>
        /// <returns>Набор переменных.</returns>
        [OperationContract]
        List<Variable> GetVariablesAll(long hSvc);

        #endregion

        #region DATA
        [OperationContract]
        List<DataValue> GetDataValues(long hSvc,
            DateTime dateSLOC, DateTime dateFLOC,
            List<int> siteId, List<int> variableId, int? offsetTypeId, double? offsetValue,
            bool isOneValue, bool isSelectDeleted = false,
            int? methodId = null, int? sourceId = null, byte? flagAQC = null);
        [OperationContract]
        List<DataForecast> GetDataForecasts(long hSvc, int catalogId, DateTime dateFcsS, DateTime dateFcsF, double? fcsLag, bool isDateFcs);
        [OperationContract]
        List<DataValue> TestDataValues(long hSvc);
        [OperationContract]
        List<Catalog> GetCatalogListById(long hSvc, List<int> ctlId);
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
        [OperationContract]
        List<Catalog> GetCatalogList(long hSvc, List<int> siteId, List<int> varId,
            int? offsetTypeId, int? methodId, int? sourceId, double? offsetValue);
        /// <summary>
        /// Получить записm каталога (не климат).
        /// </summary>   
        [OperationContract]
        Catalog GetCatalog(long hSvc, int siteId, int varId,
            int offsetTypeId, int methodId, int sourceId, double offsetValue);
        #endregion

        #region DATAP
        [OperationContract]
        List<AQCDataValue> GetDataPDataValueAQC(long hSvc, long dvId);
        #endregion DATAP

        #region PARSER
        [OperationContract]
        Parser.SysObj GetParserSysObj(long hSvc, int sysObjId);
        [OperationContract]
        List<Parser.SysParsersXSites> GetParserSysParsersXSites(long hSvc, int sysObjId);
        [OperationContract]
        List<Parser.SysParsersParams> GetParserSysParsersParams(long hSvc, List<int> sysParsersParamsSetIds);
        #endregion PARSER
    }
}
