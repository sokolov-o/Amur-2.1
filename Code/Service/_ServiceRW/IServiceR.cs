using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SOV.Amur.Meta;
using SOV.Amur.Data;
using SOV.Social;
using SOV.Amur.DataP;
using SOV.Amur.Parser;

namespace SOV.Amur.Service
{
    /// <summary>
    /// 
    /// Интерфейс управления обработкой данных БД "Амур", ДВНИГМИ
    /// 
    /// ДВНИГМИ 2016, OSokolov@SOV.ru
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
        /// <summary>
        /// Получить набор методов.
        /// </summary>
        /// <returns>Набор методов.</returns>
        [OperationContract]
        List<Method> GetMethods(long hSvc, List<int> methodIds);
        /// <summary>
        /// Выбрать пункты по их строковым индексам (кодам).
        /// </summary>
        /// <param name="hSvc">Дескриптор сервиса.</param>
        /// <param name="siteIndices">Список кодов станций.</param>
        /// <returns>Список станций.</returns>
        [OperationContract]
        List<Site> GetSitesByCodes(long hSvc, List<string> siteIndices);
        /// <summary>
        /// Поиск первого родительского метода прогноза, у которого MethodOutputStoreParameters != null.
        /// </summary>
        /// <param name="methodId">Код метода, для которого ищется искомый.</param>
        /// <returns>Первый родительский метод прогноза, у которого MethodOutputStoreParameters != null && MethodForecast != null.</returns>
        [OperationContract]
        MethodForecast GetParentFcsMethod(long hSvc, int methodId);
        /// <summary>
        /// Получить словарь соответствия кодов пунктов их координатам.
        /// </summary>
        /// <param name="hSvc"></param>
        /// <param name="siteIds">Набор уникальных кодов пунктов.</param>
        /// <param name="dateActual">Дата актуальности координат.</param>
        /// <param name="siteAttrTypeIdLat">Код типа атрибута широта.</param>
        /// <param name="siteAttrTypeIdLon">Код типа атрибута долгота.</param>
        /// <returns>Cловарь соответствия кодов пунктов их координатам.</returns>
        [OperationContract]
        Dictionary<int, Geo.GeoPoint> GetSitesPoints(long hSvc, List<int> siteIds, DateTime dateActual, int siteAttrTypeIdLat, int siteAttrTypeIdLon);
        /// <summary>
        /// Получить набор географических объектов.
        /// </summary>
        /// <returns>Набор географических объектов.</returns>
        [OperationContract]
        Dictionary<int, GeoObject> GetGeoObjectsBySiteIds(long hSvc, List<int> stationIds);
        /// <summary>
        /// Получить все типы атрибутов пункта.
        /// </summary>
        /// <param name="hSvc">Ручка сервиса.</param>
        /// <returns></returns>
        [OperationContract]
        List<SiteAttrType> GetSiteAttrTypesAll(long hSvc);
        /// <summary>
        /// Получить набор пунктов в пределах указанной трапеции.
        /// </summary>
        /// <param name="hSvc">Ручка сервиса.</param>
        /// /// <param name="south">Юг.</param>
        /// <param name="north">Север.</param>
        /// <param name="west">Запад.</param>
        /// <param name="east">Восток.</param>
        /// <returns>Набор образцов.</returns>
        [OperationContract]
        List<Site> GetSitesInBox(long hSvc, double south, double north, double west, double east);
        /// <summary>
        /// Получить набор всех образцов.
        /// </summary>
        /// <param name="hSvc">Ручка сервиса.</param>
        /// <returns>Набор образцов.</returns>
        [OperationContract]
        List<SampleMedium> GetSampleMediumsAll(long hSvc);

        /// <summary>
        /// Получить значения периодических атрибутов пункта.
        /// </summary>
        /// <param name="hSvc">Ручка сервиса.</param>
        /// <param name="siteId">Запрашиваемые коды пунктов.</param>
        /// <param name="attrTypeId">Запрашиваемые коды типов атрибутов.</param>
        /// <param name="dateActual">Дата актуальности значения периодического атрибута.</param>
        /// <returns></returns>
        [OperationContract]
        List<EntityAttrValue> GetSitesAttrValues(long hSvc, List<int> siteId, List<int> attrTypeId = null, DateTime? dateActual = null);
        /// <summary>
        /// Получить метод
        /// </summary>
        /// <param name="hSvc">Ручка сервиса.</param>
        /// <param name="methodId">Запрашиваемый код метода.</param>
        /// <returns>Метод</returns>
        [OperationContract]
        Method GetMethod(long hSvc, int methodId);
        [OperationContract]
        EntityAttrValue GetSiteAttrValue(long hSvc, int siteId, int siteAttrTypeId, DateTime dateActual);
        [OperationContract]
        List<EntityAttrValue> GetSitesAttrValue(long hSvc, List<int> siteId, int siteAttrTypeId, DateTime dateActual);
        /// <summary>
        /// Получить пункты заданного типа.
        /// </summary>
        /// <param name="hSvc"></param>
        /// <param name="siteTypeId">Код тип пункта.</param>
        /// <returns></returns>
        [OperationContract]
        List<Site> GetSitesByType(long hSvc, int siteTypeId);
        /// <summary>
        /// Получить пункты в группе.
        /// </summary>
        /// <param name="hSvc">Код круппы.</param>
        /// <param name="siteGroupId"></param>
        /// <returns></returns>
        [OperationContract]
        List<Site> GetSitesByGroup(long hSvc, int siteGroupId);
        /// <summary>
        /// Выбрать пункты заданного региона.
        /// </summary>
        /// <param name="hSvc">Ручка сервиса.</param>
        /// <param name="addrRegionIds">Список кодов регионов.</param>
        /// <returns>Набор станций.</returns>
        [OperationContract]
        List<Site> GetSitesByAddrRegionIds(long hSvc, List<int> addrRegionIds);
        [OperationContract]
        List<SiteType> GetSiteTypes(long hSvc);
        /// <summary>
        /// Получить список пунктов по списку их кодов.
        /// </summary>
        /// <returns>Станция с указанным кодом.</returns>
        [OperationContract]
        List<Site> GetSitesByList(long hSvc, List<int> siteIdList);
        /// <summary>
        /// Выбрать переменные по их кодам.
        /// </summary>
        /// <param name="hSvc">Ручка сервиса.</param>
        /// <param name="variableIdList">Список кодов переменных.</param>
        /// <returns>Набор переменных.</returns>
        [OperationContract]
        List<Variable> GetVariablesByList(long hSvc, List<int> variableIdList);
        /// <summary>
        /// Выбрать переменную по её коду.
        /// </summary>
        /// <param name="hSvc">Ручка сервиса.</param>
        /// <param name="variableId">Код переменной.</param>
        /// <returns>Переменная.</returns>
        [OperationContract]
        Variable GetVariableById(long hSvc, int variableId);
        /// <summary>
        /// Выбрать переменную по заданным атрибутам.
        /// </summary>
        /// <param name="hSvc">Ручка сервиса.</param>
        /// <param name="variableTypeId">Тип переменной.</param>
        /// <param name="timeId">Тип временного интервала переменной.</param>
        /// <param name="unitId">Ед. измерения переменной.</param>
        /// <param name="dataTypeId">Тип данных переменной.</param>
        /// <param name="generalCategoryId">Категория переменной.</param>
        /// <param name="sampleMediumId">Среда измерения переменной.</param>
        /// <param name="timeSupport">Значение для временного интервала переменной.</param>
        /// <param name="valueTypeId">Тип значения переменной.</param>
        /// <returns>Набор переменных.</returns>
        [OperationContract]
        Variable GetVariableByKey(long hSvc, int variableTypeId, int timeId, int unitId, int dataTypeId, int generalCategoryId, int sampleMediumId, int timeSupport, int valueTypeId);
        /// <summary>
        /// Выбрать переменные по заданным атрибутам.
        /// Значение null любого атрибута, означает, что он не будет использоваться при выборке
        /// данных - будут использованы все его значения.
        /// </summary>
        /// <param name="hSvc">Ручка сервиса.</param>
        /// <param name="variableTypeId">Типы переменной.</param>
        /// <param name="timeId">Типы временного интервала переменной.</param>
        /// <param name="unitId">Ед. измерения переменной.</param>
        /// <param name="dataTypeId">Типы данных переменной.</param>
        /// <param name="generalCategoryId">Категории переменной.</param>
        /// <param name="sampleMediumId">Среды измерения переменной.</param>
        /// <param name="timeSupport">Значения для временного интервала переменной.</param>
        /// <param name="valueTypeId">Типы значения переменной.</param>
        /// <returns>Набор переменных.</returns>
        [OperationContract]
        List<Variable> GetVariables(long hSvc,
            List<int> variableTypeId, List<int> timeId, List<int> unitId, List<int> dataTypeId,
            List<int> generalCategoryId, List<int> sampleMediumId, List<int> timeSupport, List<int> valueTypeId);
        /// <summary>
        /// Получить набор всех переменных.
        /// </summary>
        /// <returns>Набор переменных.</returns>
        [OperationContract]
        List<Variable> GetVariablesAll(long hSvc);
        /// <summary>
        /// Получить набор всех типов данных.
        /// </summary>
        /// <returns>Набор типов данных.</returns>
        [OperationContract]
        List<DataType> GetDataTypesAll(long hSvc);
        /// <summary>
        /// Получить набор всех флагов АКД.
        /// </summary>
        /// <returns>Набор флагов АКД.</returns>
        [OperationContract]
        Dictionary<short, string[]> GetFlagAQCAll(long hSvc);
        /// <summary>
        /// Получить набор всех категорий данных.
        /// </summary>
        /// <returns>Набор категорий данных.</returns>
        [OperationContract]
        List<GeneralCategory> GetGeneralCategoryesAll(long hSvc);

        /// <summary>
        /// Получить набор всех географических объектов.
        /// </summary>
        /// <returns>Набор географических объектов.</returns>
        [OperationContract]
        List<GeoObject> GetGeoObjectsAll(long hSvc);

        /// <summary>
        /// Получить набор всех типов географических объектов.
        /// </summary>
        /// <returns>Набор типов географических объектов.</returns>
        [OperationContract]
        List<GeoType> GetGeoObjectTypesAll(long hSvc);
        /// <summary>
        /// Получить набор всех метеозон.
        /// </summary>
        /// <returns>Набор метеозон.</returns>
        [OperationContract]
        List<MeteoZone> GetMeteoZonesAll(long hSvc);
        /// <summary>
        /// Получить набор всех методов.
        /// </summary>
        /// <returns>Набор методов.</returns>
        [OperationContract]
        List<Method> GetMethodsAll(long hSvc);
        /// <summary>
        /// Получить набор всех методов прогноза.
        /// </summary>
        /// <returns>Набор методов прогноза.</returns>
        [OperationContract]
        List<MethodForecast> GetMethodForecastsAll(long hSvc);
        /// <summary>
        /// Получить набор всех типов смещений (уровней etc.).
        /// </summary>
        /// <returns>Набор типов смещений .</returns>
        [OperationContract]
        List<OffsetType> GetOffsetTypesAll(long hSvc);
        /// <summary>
        /// Получить набор всех связей между пунктами для указанного слева в связи пункта.
        /// </summary>
        /// <returns>Набор всех связей между пунктами для указанного слева в связи пункта.</returns>
        [OperationContract]
        List<SiteXSite> GetSiteXSitesByLeftSite(long hSvc, int siteId);
        /// <summary>
        /// Получить набор всех связей между пунктами для указанного справа в связи пункта.
        /// </summary>
        /// <returns>Набор всех связей между пунктами для указанного справа в связи пункта.</returns>
        [OperationContract]
        List<SiteXSite> GetSiteXSitesByRightSite(long hSvc, int siteId);
        /// <summary>
        /// Получить набор всех типов связей между пунктами.
        /// </summary>
        /// <returns>Набор типов связей между пунктами.</returns>
        [OperationContract]
        List<SiteXSiteType> GetSiteXSiteTypesAll(long hSvc, int siteId);
        /// <summary>
        /// Получить набор всех ед. измерения.
        /// </summary>
        /// <returns>Набор ед. измерения.</returns>
        [OperationContract]
        List<Unit> GetUnitsAll(long hSvc);
        /// <summary>
        /// Получить набор всех типов значений.
        /// </summary>
        /// <returns>Набор типов значений.</returns>
        [OperationContract]
        List<SOV.Amur.Meta.ValueType> GetValueTypesAll(long hSvc);
        /// <summary>
        /// Получить набор кодов значений для переменной.
        /// </summary>
        /// <returns>Набор кодов значений для переменной.</returns>
        [OperationContract]
        List<VariableCode> GetVariableCodesAll(long hSvc, int variableId);
        /// <summary>
        /// Получить набор типов переменных.
        /// </summary>
        /// <returns>Набор типов переменных.</returns>
        [OperationContract]
        List<VariableType> GetVariableTypesAll(long hSvc);

        #endregion

        #region DATA
        /// <summary>
        /// Существуют ли прогнозы от указанной даты (dateIni) для указанных кодов записей каталога данных (catalogId).
        /// </summary>
        /// <param name="hSvc">Ручка сервиса.</param>
        /// <param name="catalogIds">Список id записей каталога данных.</param>
        /// <param name="dateIni">Исходная дата прогноза (UTC).</param>
        /// <returns>Справочник, где ключ - кoд записи каталога, значение - признак наличия прогнстических данных.</returns>
        [OperationContract]
        Dictionary<int, bool> ExistsDataForecasts(long hSvc, List<int> catalogIds, DateTime dateIni);
        /// <summary>
        /// Получить словарь, где ключи - id пункта, а значение - список кривых для указанных
        /// переменных и типа серии в заданном интервале актуальности серий.
        /// </summary>
        /// <param name="hSvc">Ручка сервиса.</param>
        /// <param name="sitesIds">Список пунктов.</param>
        /// <param name="variableIdX">Переменная X.</param>
        /// <param name="variableIdY">Переменная Y.</param>
        /// <param name="curve_seria_type_id">Тип серий для кривой.</param>
        /// <param name="seriaDateSS">Начало периода актуальности серии кривой.</param>
        /// <param name="seriaDateSF">Окончание периода актуальности серии кривой.</param>
        /// <returns></returns>
        [OperationContract]
        Dictionary<int, List<Curve>> GetCurvesBySites(long hSvc, List<int> sitesIds, int variableIdX, int variableIdY, int curve_seria_type_id, DateTime seriaDateSS, DateTime seriaDateSF);
        /// <summary>
        /// Получить кривую для указанных записей каталога и типа серии в заданном интервале актуальности серий.
        /// </summary>
        /// <param name="hSvc">Ручка сервиса.</param>
        /// <param name="catalog_id_x">Запись каталога переменой X.</param>
        /// <param name="catalog_id_y">Запись каталога переменой Y.</param>
        /// <param name="curve_seria_type_id">Тип серий для кривой.</param>
        /// <param name="seriaDateSS">Начало периода актуальности серии кривой.</param>
        /// <param name="seriaDateSF">Окончание периода актуальности серии кривой.</param>
        /// <returns></returns>
        [OperationContract]
        Curve GetCurveByCatalog(long hSvc, int catalog_id_x, int catalog_id_y, int curve_seria_type_id, DateTime seriaDateSS, DateTime seriaDateSF);

        /// <summary>
        /// Выбрать прогностические данные для указанного списка кодов записей каталога.
        /// </summary>
        /// <param name="hSvc">Ручка сервиса.</param>
        /// <param name="catalogIds"></param>
        /// <param name="dateFcsS"></param>
        /// <param name="dateFcsF"></param>
        /// <param name="fcsLag"></param>
        /// <param name="isDateFcs"></param>
        /// <returns></returns>
        [OperationContract]
        Dictionary<int, List<DataForecast>> GetDataForecastsByIdList(long hSvc, List<int> catalogIds, DateTime dateFcsS, DateTime dateFcsF, double? fcsLag, bool isDateFcs);
        /// <summary>
        /// Получить набор источников данных для указанных (с помощью кода записи) записей с данными.
        /// </summary>
        /// <param name="hSvc">Ручка сервиса.</param>
        /// <param name="dataValueIds">Набор кодов (id) записей данных для которых необходимо получить источники данных.</param>
        /// <returns>Словарь, где ключ - код записи данных, значение - источник данных.</returns>
        [OperationContract]
        Dictionary<long, DataSource> GetDataSources(long hSvc, List<long> dataValueIds);
        ///// <summary>
        ///// Выборка данных вместе с записями каталога.
        ///// Внимание! Порядок данных, их сортировка в соответствие с флагом контроля, методом и кодом - не сохраняется
        ///// </summary>
        ///// <param name="dateS"></param>
        ///// <param name="dateF"></param>
        ///// <param name="isDateLOC"></param>
        ///// <param name="siteId"></param>
        ///// <param name="variableId"></param>
        ///// <param name="offsetTypeId"></param>
        ///// <param name="offsetValue"></param>
        ///// <param name="isOneValue"></param>
        ///// <param name="isSelectDeleted"></param>
        ///// <param name="methodId"></param>
        ///// <param name="sourceId"></param>
        ///// <param name="flagAQC"></param>
        ///// <returns>Словарь записей каталога и соответствующих им данных. 
        ///// Внимание! Порядок данных, их сортировка в соответствие с флагом контроля, методом и кодом - не сохраняется.</returns>
        //[OperationContract]
        //[Obsolete("Используйте другой метод GetDataValues. sov@2017.07.05", false)]
        //Dictionary<Catalog, List<DataValue>> GetDataValues(long hSvc,
        //    DateTime dateS, DateTime dateF, bool isDateLOC,
        //    List<int> siteId, List<int> variableId,
        //    List<int> offsetTypeId, double? offsetValue,
        //    bool isOneValue, bool isSelectDeleted = false,
        //    int? methodId = null, int? sourceId = null,
        //    byte? flagAQC = null);
        /// <summary>
        /// Выборка данных вместе с записями каталога.
        /// </summary>
        /// /// <param name="hSvc">Ручка сервиса.</param> 
        /// <param name="dateS"></param>
        /// <param name="dateF"></param>
        /// <param name="isDateLOC"></param>
        /// <param name="siteId">Список кодов пунктов.</param>
        /// <param name="variableId">Список кодов переменных.</param>
        /// <param name="offsetTypeId">Список кодов типов смещений.</param>
        /// <param name="offsetValue">Список кодов значений смещений.</param>
        /// <param name="methodId">Список кодов методов.</param>
        /// <param name="sourceId">Список кодов источников.</param>
        /// <param name="flagAQC">Флаг контроля значения или null для всех.</param>
        /// <param name="isActualValueOnly">Только одно, актуальное, значение?</param>
        /// <param name="isSelectDeleted">Выбирать удалённые?</param>
        /// <returns>Словарь записей каталога и соответствующих им данных. 
        [OperationContract]
        Dictionary<Catalog, List<DataValue>> GetDataValues(long hSvc,
            DateTime dateS, DateTime dateF, bool isDateLOC,
            List<int> siteId, List<int> variableId,
            List<int> methodId, List<int> sourceId,
            List<int> offsetTypeId, List<double> offsetValue,
            byte? flagAQC = null,
            bool isActualValueOnly = true, bool isSelectDeleted = false);
        [OperationContract]
        List<DataForecast> GetDataForecasts(long hSvc, int catalogId, DateTime dateFcsS, DateTime dateFcsF, double? fcsLag, bool isDateFcs);
        //[OperationContract]
        //List<DataValue> TestDataValues(long hSvc);
        [OperationContract]
        List<Catalog> GetCatalogListById(long hSvc, List<int> ctlId);
        /// <summary>
        /// Получить записи каталога (не климатические).
        /// </summary>
        /// <param name="hSvc"></param>
        /// <param name="siteId">Список кодов пунктов или все пункты, если null.</param>
        /// <param name="varId">Список кодов переменных или все, если null.</param>
        /// <param name="offsetTypeId">Список кодов смещения или все, если null.</param>
        /// <param name="methodId">Список кодов метода или все, если null.</param>
        /// <param name="sourceId">Список кодов источника или все, если null.</param>
        /// <param name="offsetValue">Значения смещения или все, если null.</param>
        /// <returns>Список записей каталога, удовлетворяющих условию отбора.</returns>
        [OperationContract]
        List<Catalog> GetCatalogList(long hSvc, List<int> siteId, List<int> varId, List<int> methodId, List<int> sourceId, List<int> offsetTypeId, List<double> offsetValue);
        //List<Catalog> GetCatalogList(long hSvc, List<int> siteId, List<int> varId,
        //    int? offsetTypeId, int? methodId, int? sourceId, double? offsetValue);

        /// <summary>
        /// Получить записm каталога (не климат).
        /// </summary>   
        [OperationContract]
        Catalog GetCatalog(long hSvc, int siteId, int varId,
            int offsetTypeId, int methodId, int sourceId, double offsetValue);
        #endregion

        //////#region DATAP
        [OperationContract]
        List<AQCDataValue> GetDataPDataValueAQC(long hSvc, long dvId);
        //////#endregion DATAP

        #region PARSER
        [OperationContract]
        SysObj GetParserSysObj(long hSvc, int sysObjId);
        [OperationContract]
        List<SysParsersXSites> GetParserSysParsersXSites(long hSvc, int sysObjId);
        [OperationContract]
        List<SysParsersParams> GetParserSysParsersParams(long hSvc, List<int> sysParsersParamsSetIds);
        #endregion PARSER

        #region SOCIAL

        /// <summary>
        /// Получить организацию.
        /// </summary>
        /// <returns>Источник данных</returns>
        [OperationContract]
        Org GetOrg(long hSvc, int orgId);
        /// <summary>
        /// Получить набор организаций.
        /// </summary>
        [OperationContract]
        List<Org> GetOrgsById(long hSvc, List<int> orgIds);
        /// <summary>
        /// Получить набор всех организаций.
        /// </summary>
        [OperationContract]
        List<Org> GetOrgsAll(long hSvc);
        /// <summary>
        /// Получить физ. лицо.
        /// </summary>
        [OperationContract]
        Person GetPerson(long hSvc, int personIds);
        /// <summary>
        /// Получить набор физ. лиц.
        /// </summary>
        [OperationContract]
        List<Person> GetPersonsById(long hSvc, List<int> personIds);
        /// <summary>
        /// Получить набор всех физ. лиц.
        /// </summary>
        [OperationContract]
        List<Person> GetPersonsAll(long hSvc);
        /// <summary>
        /// Получить регионы с вложенными регионами (с наследниками) или без них.
        /// </summary>
        /// <param name="hSvc">Ручка сервиса.</param>
        /// <param name="parentAddrRegionsId">Коды регионов-родителей.</param>
        /// <param name="isWithChilds">Выбирать наследников для каждого региона или нет.</param>
        /// <returns>Объекты с наследниками или без.</returns>
        [OperationContract]
        List<Addr> GetAddrs(long hSvc, List<int> parentAddrRegionsId, bool isWithChilds);
        /// <summary>
        /// Получить все регионы без наследников .
        /// </summary>
        /// <param name="hSvc">Ручка сервиса.</param>
        /// <returns></returns>
        [OperationContract]
        List<Addr> GetAddrsAll(long hSvc);
        /// <summary>
        /// Получить субъект права.
        /// </summary>
        [OperationContract]
        LegalEntity GetLegalEntity(long hSvc, int leIds);
        /// <summary>
        /// Получить набор субъектов права.
        /// </summary>
        [OperationContract]
        List<LegalEntity> GetLegalEntityesById(long hSvc, List<int> leIds);
        /// <summary>
        /// Получить набор всех субъектов права.
        /// </summary>
        [OperationContract]
        List<LegalEntity> GetLegalEntityesAll(long hSvc);

        #endregion SOCIAL
    }
}
