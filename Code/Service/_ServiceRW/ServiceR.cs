using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using SOV.Amur.Meta;
using SOV.Amur.Data;
using SOV.Social;
using SOV.Amur.DataP;

namespace SOV.Amur.Service
{
    //[ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public partial class Service : IService
    {
        #region META
        /// <summary>
        /// Поиск первого родительского метода прогноза, у которого MethodOutputStoreParameters != null.
        /// </summary>
        /// <param name="methodId">Код метода, для которого ищется искомый.</param>
        /// <returns>Первый родительский метод прогноза, у которого MethodOutputStoreParameters != null && MethodForecast != null.</returns>
        public MethodForecast GetParentFcsMethod(long hSvc, int methodId)
        {
            return DataManagerMeta(hSvc).MethodRepository.SelectParentFcsMethod(methodId);
        }
        /// <summary>
        /// Получить набор пунктов в пределах указанной трапеции.
        /// </summary>
        /// <param name="hSvc">Ручка сервиса.</param>
        /// /// <param name="south">Юг.</param>
        /// <param name="north">Север.</param>
        /// <param name="west">Запад.</param>
        /// <param name="east">Восток.</param>
        /// <returns>Набор образцов.</returns>
        public List<Site> GetSitesInBox(long hSvc, double south, double north, double west, double east)
        {
            return DataManagerMeta(hSvc).SiteRepository.SelectExtent(south, north, west, east);
        }
        /// <summary>
        /// Получить набор всех образцов.
        /// </summary>
        /// <param name="hSvc">Ручка сервиса.</param>
        /// <returns>Набор образцов.</returns>
        public List<SampleMedium> GetSampleMediumsAll(long hSvc)
        {
            return DataManagerMeta(hSvc).SampleMediumRepository.Select(null);
        }

        /// <summary>
        /// Получить регионы с вложенными регионами (с наследниками) или без них.
        /// </summary>
        /// <param name="hSvc">Ручка сервиса.</param>
        /// <param name="parentRegionsId">Коды регионов.</param>
        /// <param name="isWithChilds">Выбирать наследников для каждого региона или нет.</param>
        /// <returns>Объекты с наследниками или без.</returns>
        public List<Addr> GetAddrs(long hSvc, List<int> parentRegionsId, bool isWithChilds)
        {
            return DataManagerSocial(hSvc).AddrRepository.Select(parentRegionsId, isWithChilds);
        }
        /// <summary>
        /// Получить все регионы .
        /// </summary>
        /// <param name="hSvc">Ручка сервиса.</param>
        /// <returns></returns>
        public List<Addr> GetAddrsAll(long hSvc)
        {
            return DataManagerSocial(hSvc).AddrRepository.Select();
        }
        /// <summary>
        /// Получить метод.
        /// </summary>
        /// <param name="hSvc">Ручка сервиса.</param>
        /// <param name="methodId">Код метода.</param>
        /// <returns></returns>
        public Method GetMethod(long hSvc, int methodId)
        {
            return DataManagerMeta(hSvc).MethodRepository.Select(new List<int>(new int[] { methodId }))[0];
        }
        /// <summary>
        /// Получить организацию.
        /// </summary>
        /// <returns>Источник данных</returns>
        public Org GetOrg(long hSvc, int orgId)
        {
            return DataManagerSocial(hSvc).OrgRepository.SelectById(new List<int>(new int[] { orgId }))[0];
        }
        /// <summary>
        /// Получить набор организаций.
        /// </summary>
        /// <returns>Источник данных</returns>
        public List<Org> GetOrgsById(long hSvc, List<int> orgIds)
        {
            return DataManagerSocial(hSvc).OrgRepository.SelectById(orgIds);
        }
        /// <summary>
        /// Получить набор всех организаций.
        /// </summary>
        /// <returns>Все источники данных</returns>
        public List<Org> GetOrgsAll(long hSvc)
        {
            return DataManagerSocial(hSvc).OrgRepository.SelectAll();
        }
        /// <summary>
        /// Получить набор физ. лиц.
        /// </summary>
        /// <returns>Источник данных</returns>
        public Person GetPerson(long hSvc, int personIds)
        {
            return DataManagerSocial(hSvc).PersonRepository.SelectById(new List<int>(new int[] { personIds }))[0];
        }
        /// <summary>
        /// Получить набор физ. лиц.
        /// </summary>
        /// <returns>Источник данных</returns>
        public List<Person> GetPersonsById(long hSvc, List<int> personIds)
        {
            return DataManagerSocial(hSvc).PersonRepository.SelectById(personIds);
        }
        /// <summary>
        /// Получить набор всех физ. лиц.
        /// </summary>
        /// <returns>Все источники данных</returns>
        public List<Person> GetPersonsAll(long hSvc)
        {
            return DataManagerSocial(hSvc).PersonRepository.SelectAll();
        }
        /// <summary>
        /// Получить набор физ. лиц.
        /// </summary>
        /// <returns>Источник данных</returns>
        public LegalEntity GetLegalEntity(long hSvc, int leIds)
        {
            return DataManagerSocial(hSvc).LegalEntityRepository.SelectById(new List<int>(new int[] { leIds }))[0];
        }
        /// <summary>
        /// Получить набор физ. лиц.
        /// </summary>
        /// <returns>Источник данных</returns>
        public List<LegalEntity> GetLegalEntityesById(long hSvc, List<int> leIds)
        {
            return DataManagerSocial(hSvc).LegalEntityRepository.SelectById(leIds);
        }
        /// <summary>
        /// Получить набор всех физ. лиц.
        /// </summary>
        /// <returns>Все источники данных</returns>
        public List<LegalEntity> GetLegalEntityesAll(long hSvc)
        {
            return DataManagerSocial(hSvc).LegalEntityRepository.SelectAll();
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
        /// Получить переменную по её коду.
        /// </summary>
        /// <returns>Переменная.</returns>
        public Variable GetVariableById(long hSvc, int variableId)
        {
            List<Variable> ret = DataManagerMeta(hSvc).VariableRepository.Select(new List<int>(new int[] { variableId }));
            return ret.Count == 0 ? null : ret[0];
        }
        /// <summary>
        /// Получить переменную по ключу.
        /// </summary>
        /// <returns>Переменная.</returns>
        public Variable GetVariableByKey(long hSvc,
            int variableTypeId, int timeId, int unitId, int dataTypeId,
            int generalCategoryId, int sampleMediumId, int timeSupport, int valueTypeId)
        {
            return DataManagerMeta(hSvc).VariableRepository.Select(variableTypeId, timeId, unitId, dataTypeId, generalCategoryId, sampleMediumId, timeSupport, valueTypeId);
        }
        public List<Variable> GetVariables(long hSvc,
            List<int> variableTypeId, List<int> timeId, List<int> unitId, List<int> dataTypeId,
            List<int> generalCategoryId, List<int> sampleMediumId, List<int> timeSupport, List<int> valueTypeId)
        {
            return DataManagerMeta(hSvc).VariableRepository.Select(variableTypeId, timeId, unitId, dataTypeId, generalCategoryId, sampleMediumId, timeSupport, valueTypeId);
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
        /// Получить набор всех типов данных.
        /// </summary>
        /// <returns>Набор типов данных.</returns>
        public List<DataType> GetDataTypesAll(long hSvc)
        {
            return DataManagerMeta(hSvc).DataTypeRepository.Select();
        }
        /// <summary>
        /// Получить набор всех категорий данных.
        /// </summary>
        /// <returns>Набор категорий данных.</returns>
        public List<GeneralCategory> GetGeneralCategoryesAll(long hSvc)
        {
            return DataManagerMeta(hSvc).GeneralCategoryRepository.Select(null);
        }
        /// <summary>
        /// Получить набор всех географических объектов.
        /// </summary>
        /// <returns>Набор географических объектов.</returns>
        public List<GeoObject> GetGeoObjectsAll(long hSvc)
        {
            return DataManagerMeta(hSvc).GeoObjectRepository.Select(null);
        }
        /// <summary>
        /// Получить набор географических объектов.
        /// </summary>
        /// <returns>Набор географических объектов.</returns>
        public Dictionary<int, GeoObject> GetGeoObjectsBySiteIds(long hSvc, List<int> siteIds)
        {
            Meta.DataManager dm = DataManagerMeta(hSvc);

            List<SiteGeoObject> sgos = dm.SiteGeoObjectRepository.SelectBySites(siteIds);
            List<GeoObject> gos = dm.GeoObjectRepository.Select(sgos.Select(x => x.GeoObjectId).Distinct().ToList());

            Dictionary<int, GeoObject> ret = new Dictionary<int, GeoObject>();
            foreach (var siteId in siteIds)
            {
                GeoObject go = null;
                if (sgos.FirstOrDefault(x => x.SiteId == siteId) != null)
                    go = gos.FirstOrDefault(x => x.Id == go.Id);
                ret.Add(siteId, go);
            }
            return ret;
        }
        /// <summary>
        /// Получить набор всех типов географических объектов.
        /// </summary>
        /// <returns>Набор типов географических объектов.</returns>
        public List<GeoType> GetGeoObjectTypesAll(long hSvc)
        {
            return DataManagerMeta(hSvc).GeoTypeRepository.Select(null);
        }
        /// <summary>
        /// Получить набор всех метеозон.
        /// </summary>
        /// <returns>Набор метеозон.</returns>
        public List<MeteoZone> GetMeteoZonesAll(long hSvc)
        {
            return DataManagerMeta(hSvc).MeteoZoneRepository.Select();
        }
        /// <summary>
        /// Получить набор всех методов наблюдений и расчётов.
        /// </summary>
        /// <returns>Набор методов.</returns>
        public List<Method> GetMethodsAll(long hSvc)
        {
            return DataManagerMeta(hSvc).MethodRepository.Select();
        }
        /// <summary>
        /// Получить набор методов.
        /// </summary>
        /// <returns>Набор методов.</returns>
        public List<Method> GetMethods(long hSvc, List<int> methodIds)
        {
            return DataManagerMeta(hSvc).MethodRepository.Select(methodIds);
        }
        /// <summary>
        /// Получить набор всех методов прогноза.
        /// </summary>
        /// <returns>Набор методов прогноза.</returns>
        public List<MethodForecast> GetMethodForecastsAll(long hSvc)
        {
            return DataManagerMeta(hSvc).MethodForecastRepository.Select();
        }
        /// <summary>
        /// Получить набор всех типов смещений (уровней etc.).
        /// </summary>
        /// <returns>Набор типов смещений .</returns>
        public List<OffsetType> GetOffsetTypesAll(long hSvc)
        {
            return DataManagerMeta(hSvc).OffsetTypeRepository.Select();
        }
        /// <summary>
        /// Получить набор всех типов атрибутов сайтов.
        /// </summary>
        /// <returns>Набор типов атрибутов сайтов.</returns>
        public List<SiteAttrType> GetSiteAttrTypesAll(long hSvc)
        {
            return DataManagerMeta(hSvc).SiteAttrTypeRepository.Select();
        }
        /// <summary>
        /// Получить словарь флагов автоматического критконтроля данных.
        /// </summary>
        /// <returns>Словарь флагов автоматического критконтроля данных.</returns>
        public Dictionary<short, string[/*name, name_short*/]> GetFlagAQCAll(long hSvc)
        {
            return DataManagerMeta(hSvc).FlaAQCRepository.Select();
        }
        /// <summary>
        /// Получить набор всех связей между пунктами для указанного слева в связи пункта.
        /// </summary>
        /// <returns>Набор всех связей между пунктами для указанного слева в связи пункта.</returns>
        public List<SiteXSite> GetSiteXSitesByLeftSite(long hSvc, int siteId)
        {
            return DataManagerMeta(hSvc).SiteXSiteRepository.SelectBySite1(siteId);
        }
        /// <summary>
        /// Получить набор всех типов связей между пунктами.
        /// </summary>
        /// <returns>Набор типов связей между пунктами.</returns>
        public List<SiteXSiteType> GetSiteXSiteTypesAll(long hSvc, int siteId)
        {
            return DataManagerMeta(hSvc).SiteXSiteRepository.SelectRelationTypes();
        }
        /// <summary>
        /// Получить набор всех ед. измерения.
        /// </summary>
        /// <returns>Набор ед. измерения.</returns>
        public List<Unit> GetUnitsAll(long hSvc)
        {
            return DataManagerMeta(hSvc).UnitRepository.Select(null, null);
        }
        /// <summary>
        /// Получить набор кодов значений для переменной.
        /// </summary>
        /// <returns>Набор кодов значений для переменной.</returns>
        public List<VariableCode> GetVariableCodesAll(long hSvc, int variableId)
        {
            return DataManagerMeta(hSvc).VariableCodeRepository.Select(variableId);
        }
        /// <summary>
        /// Получить набор типов переменных.
        /// </summary>
        /// <returns>Набор типов переменных.</returns>
        public List<VariableType> GetVariableTypesAll(long hSvc)
        {
            return DataManagerMeta(hSvc).VariableTypeRepository.Select(null);
        }
        /// <summary>
        /// Получить набор всех типов значений.
        /// </summary>
        /// <returns>Набор типов значений.</returns>
        public List<SOV.Amur.Meta.ValueType> GetValueTypesAll(long hSvc)
        {
            return DataManagerMeta(hSvc).ValueTypeRepository.Select(null);
        }
        /// <summary>
        /// Получить набор всех связей между пунктами для указанного справа в связи пункта.
        /// </summary>
        /// <returns>Набор всех связей между пунктами для указанного справа в связи пункта.</returns>
        public List<SiteXSite> GetSiteXSitesByRightSite(long hSvc, int siteId)
        {
            return DataManagerMeta(hSvc).SiteXSiteRepository.SelectBySite2(siteId);
        }
        /// <summary>
        /// Получить пункты по их индексам/кодам.
        /// </summary>
        /// <returns>Пункты с указанными кодами.</returns>
        public List<Site> GetSitesByCodes(long hSvc, List<string> siteCodes)
        {
            return DataManagerMeta(hSvc).SiteRepository.SelectByCodes(siteCodes);
        }
        /// <summary>
        /// Выбрать пункты заданного региона.
        /// </summary>
        /// <param name="hSvc">Ручка сервиса.</param>
        /// <param name="addrRegionIds">Список кодов регионов.</param>
        /// <returns>Набор станций.</returns>
        public List<Site> GetSitesByAddrRegionIds(long hSvc, List<int> addrRegionIds)
        {
            return DataManagerMeta(hSvc).SiteRepository.SelectByAddrRegionIds(addrRegionIds);
        }
        /// <summary>
        /// Получить список типов пунктов.
        /// </summary>
        /// <param name="hSvc">Дескриптор сервиса.</param>
        /// <returns>Список типов станций.</returns>
        public List<SiteType> GetSiteTypes(long hSvc)
        {
            return DataManagerMeta(hSvc).SiteTypeRepository.Select();
        }
        /// <summary>
        /// Получить список наблюдательных пунктов по их id.
        /// </summary>
        /// <param name="hSvc">Ручка сервиса.</param>
        /// <param name="siteIdList">Список кодов пунктов для выборки.</param>
        /// <returns></returns>
        public List<Site> GetSitesByList(long hSvc, List<int> siteIdList)
        {
            return DataManagerMeta(hSvc).SiteRepository.Select(siteIdList);
        }
        /// <summary>
        /// Получить список наблюдательных пунктов по их типу.
        /// </summary>
        /// <param name="hSvc"></param>
        /// <param name="siteTypeId">Тип пунктов.</param>
        /// <returns></returns>
        public List<Site> GetSitesByType(long hSvc, int siteTypeId)
        {
            return DataManagerMeta(hSvc).SiteRepository.SelectByType(siteTypeId);
        }
        /// <summary>
        /// Выбрать пункты, относящиеся к группе и отсортировать их в порядке, указанном в группе.
        /// </summary>
        /// <param name="hSvc">Ручка сервиса.</param>
        /// <param name="siteGroupId">Код группы пунктов.</param>
        /// <returns>Список пунктов группы, отсортированные в порядке, указанном в группе.</returns>
        public List<Site> GetSitesByGroup(long hSvc, int siteGroupId)
        {
            return DataManagerMeta(hSvc).SiteRepository.SelectSitesByGroup(siteGroupId);
        }
        public EntityAttrValue GetSiteAttrValue(long hSvc, int siteId, int siteAttrTypeId, DateTime dateActual)
        {
            return DataManagerMeta(hSvc).EntityAttrRepository.SelectAttrValue("site", siteId, siteAttrTypeId, dateActual);
        }
        public List<EntityAttrValue> GetSitesAttrValue(long hSvc, List<int> siteId, int siteAttrTypeId, DateTime dateActual)
        {
            return DataManagerMeta(hSvc).EntityAttrRepository.SelectAttrValuesActual("site", siteId, new List<int>(new int[] { siteAttrTypeId }), dateActual);
        }
        public Dictionary<int, Geo.GeoPoint> GetSitesPoints(long hSvc, List<int> siteIds, DateTime dateActual, int siteAttrTypeIdLat, int siteAttrTypeIdLon)
        {
            Dictionary<int, Geo.GeoPoint> ret = new Dictionary<int, Geo.GeoPoint>();

            List<EntityAttrValue> eavs = GetSitesAttrValues(hSvc, siteIds, new List<int>() { siteAttrTypeIdLat, siteAttrTypeIdLon }, dateActual);

            foreach (var siteId in siteIds)
            {
                EntityAttrValue eavLat = eavs.FirstOrDefault(x => x.EntityId == siteId && x.AttrTypeId == siteAttrTypeIdLat);
                EntityAttrValue eavLon = eavs.FirstOrDefault(x => x.EntityId == siteId && x.AttrTypeId == siteAttrTypeIdLon);

                Geo.GeoPoint point = null;
                if (eavLat != null && eavLon != null)
                    point = new Geo.GeoPoint(double.Parse(eavLat.Value), double.Parse(eavLon.Value));

                ret.Add(siteId, point);
            }
            return ret;
        }
        public List<EntityAttrValue> GetSitesAttrValues(long hSvc, List<int> siteId, List<int> attrTypeId = null, DateTime? dateActual = null)
        {
            return DataManagerMeta(hSvc).EntityAttrRepository.SelectAttrValuesActual("site", siteId, attrTypeId, dateActual);
        }
        //public CatalogFilter GetCatalogFilter(long hSvc, int SiteGroupId, List<int> VariablesId, int? OffsetTypeId, double? OffsetValue, int MethodId, int SourceId)
        //{
        //    return DataManagerMeta(hSvc).GetCatalogFilter(SiteGroupId, VariablesId, OffsetTypeId, OffsetValue, MethodId, SourceId);
        //    return new CatalogFilter(null, null, null, null, null, null);
        //}

        #endregion META

        #region DATA
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
        public Dictionary<int, List<Curve>> GetCurvesBySites(long hSvc, List<int> sitesId, int variableIdX, int variableIdY,
            int curve_seria_type_id, DateTime seriaDateSS, DateTime seriaDateSF)
        {
            return DataManagerData(hSvc).CurveRepository.SelectCurvesBySites(sitesId, variableIdX, variableIdY, curve_seria_type_id, seriaDateSS, seriaDateSF);
        }
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
        public Curve GetCurveByCatalog(long hSvc, int catalog_id_x, int catalog_id_y,
            int curve_seria_type_id, DateTime seriaDateSS, DateTime seriaDateSF)
        {
            List<Curve> ret = DataManagerData(hSvc).CurveRepository.SelectCurvesByCatalog(catalog_id_x, catalog_id_y, curve_seria_type_id, seriaDateSS, seriaDateSF, true);
            return ret.Count == 1 ? ret[0] : null;
        }

        public Dictionary<long, DataSource> GetDataSources(long hSvc, List<long> dataValueIds)
        {
            return DataManagerData(hSvc).DataSourceRepository.Select(dataValueIds);
        }
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
        public List<Catalog> GetCatalogList(long hSvc,
            List<int> siteId, List<int> varId,
            List<int> methodId, List<int> sourceId,
            List<int> offsetTypeId, List<double> offsetValue)
        {
            return DataManagerMeta(hSvc).CatalogRepository.Select(siteId, varId, methodId, sourceId, offsetTypeId, offsetValue);
        }
        public Catalog GetCatalog(long hSvc, int siteId, int varId, int offsetTypeId, int methodId, int sourceId, double offsetValue)
        {
            List<Catalog> ret = GetCatalogList(hSvc,
                new List<int>(new int[] { siteId }),
                new List<int>(new int[] { varId }),
                new List<int>(new int[] { methodId }),
                new List<int>(new int[] { sourceId }),
                new List<int>(new int[] { offsetTypeId }),
                new List<double>(new double[] { (double)offsetValue }));

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
        /// <returns>Словарь записей каталога и соответствующих им данных. </returns>
        public Dictionary<Catalog, List<DataValue>> GetDataValues(long hSvc,
            DateTime dateS, DateTime dateF, bool isDateLOC,
            List<int> siteId, List<int> variableId,
            List<int> methodId, List<int> sourceId,
            List<int> offsetTypeId, List<double> offsetValue,
            byte? flagAQC = null,
            bool isActualValueOnly = true, bool isSelectDeleted = false
             )
        {
            return DataManagerData(hSvc).DataValueRepository.SelectA1(
             dateS, dateF, isDateLOC,
             siteId, variableId,
             methodId,
             sourceId,
             offsetTypeId,
             offsetValue,
             flagAQC,
             isActualValueOnly,
             isSelectDeleted
             );
        }
        public List<DataForecast> GetDataForecasts(long hSvc, int catalogId, DateTime dateFcsS, DateTime dateFcsF, double? fcsLag, bool isDateFcs)
        {
            return GetDataForecastsByIdList(hSvc, new List<int>(new int[] { catalogId }), dateFcsS, dateFcsF, fcsLag, isDateFcs)
                .ElementAt(0).Value;
        }
        /// <summary>
        /// Выбрать прогностические данные для указанного списка кодов записей каталога.
        /// </summary>
        /// <param name="hSvc"></param>
        /// <param name="catalogIds"></param>
        /// <param name="dateFcsS"></param>
        /// <param name="dateFcsF"></param>
        /// <param name="fcsLag"></param>
        /// <param name="isDateFcs"></param>
        /// <returns></returns>
        public Dictionary<int, List<DataForecast>> GetDataForecastsByIdList(long hSvc, List<int> catalogIds, DateTime dateFcsS, DateTime dateFcsF, double? fcsLag, bool isDateFcs)
        {
            Dictionary<int, List<DataForecast>> ret = new Dictionary<int, List<DataForecast>>();
            List<DataForecast> data = DataManagerData(hSvc).DataForecastRepository.SelectDataForecasts(catalogIds, dateFcsS, dateFcsF,
                fcsLag.HasValue ? new List<double>(new double[] { (double)fcsLag }) : null, isDateFcs);

            foreach (var catalogId in catalogIds)
            {
                ret.Add(catalogId, data.FindAll(x => x.CatalogId == catalogId));
            }
            return ret;
        }

        /// <summary>
        /// Существуют ли прогнозы от указанной даты (dateIni) для указанных кодов записей каталога данных (catalogId).
        /// </summary>
        /// <param name="hSvc">Ручка сервиса.</param>
        /// <param name="catalogIds">Список id записей каталога данных.</param>
        /// <param name="dateIni">Исходная дата прогноза (UTC).</param>
        /// <returns>Справочник, где ключ - кoд записи каталога, значение - признак наличия прогнстических данных.</returns>
        public Dictionary<int, bool> ExistsDataForecasts(long hSvc, List<int> catalogIds, DateTime dateIni)
        {
            return DataManagerData(hSvc).DataForecastRepository.Exists(catalogIds, dateIni);
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
            List<Parser.SysObj> sysObjs = DataManagerParser(hSvc).SysObjRepository.Select(new List<int>(new int[] { sysObjId }));
            Parser.SysObj ret =(sysObjs is null || sysObjs.Count == 0) ? null : sysObjs[0];

            System.IO.File.AppendAllText(_logFilePath, string.Format("GetParserSysObj {0}\n", DateTime.Now));
            return ret;
        }
        public List<Parser.SysParsersXSites> GetParserSysParsersXSites(long hSvc, int sysObjId)
        {
            System.IO.File.AppendAllText(_logFilePath, string.Format("GetParserSysParsersXSites {0}\n", DateTime.Now));
            return DataManagerParser(hSvc).SysParsersXSitesRepository.Select(sysObjId);
        }
        public List<Parser.SysParsersParams> GetParserSysParsersParams(long hSvc, List<int> sysParsersParamsSetIds)
        {
            System.IO.File.AppendAllText(_logFilePath, string.Format("GetParserSysParsersParams {0}\n", DateTime.Now));
            return DataManagerParser(hSvc).SysParsersParamsRepository.Select(sysParsersParamsSetIds);
        }

        #endregion PARSER
    }
}
