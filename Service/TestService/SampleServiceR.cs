using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestService.AmurServiceRWReference;

namespace TestService
{
    class SampleServiceR
    {
        /// <summary>
        /// 
        /// Пример работы с БД "Амур" 2.0 с использованием сервиса доступа к данным AmurRWService.
        /// 
        /// OSokolov aka viator.im@gmail.com
        /// </summary>
        public static void Sample4NICPlaneta(ServiceClient client, long hSvc)
        {
            Console.WriteLine("\n>>>>>>>>>>>>>>>>>>> Sample4Planeta");

            #region СХЕМА META - получить данные справочников

            // Получение основных справочников

            // СПРАВОЧНИК типов переменных
            VariableType[] varTypes = client.GetVariableTypesAll(hSvc);
            Console.WriteLine("СПРАВОЧНИК типов переменных (всего {0}):", varTypes.Length);
            foreach (var varType in varTypes.OrderBy(x => x.Name))
            {
                Console.WriteLine("\t{0}/{1}", varType.Name, varType.Id);
            }

            // СПРАВОЧНИК ед. измерения
            Unit[] units = client.GetUnitsAll(hSvc);
            Console.WriteLine("СПРАВОЧНИК ед. измерения (всего {0}):", units.Length);
            foreach (var unit in units.Where(x => x.SIConvertion.HasValue).OrderBy(x => x.Name))
            {
                Console.WriteLine("\t{0}/{1}", unit.Name, unit.Id);
            }

            // СПРАВОЧНИК типов данных
            DataType[] dataTypes = client.GetDataTypesAll(hSvc);
            Console.WriteLine("СПРАВОЧНИК типов данных (всего {0}):", dataTypes.Length);
            foreach (var dataType in dataTypes.OrderBy(x => x.Name))
            {
                Console.WriteLine("\t{0}/{1}", dataType.Name, dataType.Id);
            }

            // СПРАВОЧНИК категорий
            GeneralCategory[] genCats = client.GetGeneralCategoryesAll(hSvc);
            Console.WriteLine("СПРАВОЧНИК категорий (всего {0}):", genCats.Length);
            foreach (var genCat in genCats.OrderBy(x => x.Name))
            {
                Console.WriteLine("\t{0}/{1}", genCat.Name, genCat.Id);
            }

            // СПРАВОЧНИК типов значений
            TestService.AmurServiceRWReference.ValueType[] valTypes = client.GetValueTypesAll(hSvc);
            Console.WriteLine("СПРАВОЧНИК типов значений (всего {0}):", valTypes.Length);
            foreach (var valType in valTypes.OrderBy(x => x.Name))
            {
                Console.WriteLine("\t{0}/{1}", valType.Name, valType.Id);
            }

            // СПРАВОЧНИК образцов
            SampleMedium[] samples = client.GetSampleMediumsAll(hSvc);
            Console.WriteLine("СПРАВОЧНИК образцов (всего {0}):", samples.Length);
            foreach (var sample in samples.OrderBy(x => x.Name))
            {
                Console.WriteLine("\t{0}/{1}", sample.Name, sample.Id);
            }

            // СПРАВОЧНИК типов смещений
            OffsetType[] offsetTypes = client.GetOffsetTypesAll(hSvc);
            OffsetType offsetType = offsetTypes.FirstOrDefault(x => x.Id == -777);
            Console.WriteLine("СПРАВОЧНИК типов смещений (всего {0}):", samples.Length);
            foreach (var off in offsetTypes.OrderBy(x => x.Name))
            {
                Console.WriteLine("\t{0}/{1}", off.Name, off.Id);
            }

            // СПРАВОЧНИК методов
            Method[] methods = client.GetMethodsAll(hSvc);
            MethodForecast[] methodFcss = client.GetMethodForecastsAll(hSvc);
            Console.WriteLine("СПРАВОЧНИК методов (всего {0}):", methods.Length);
            foreach (var meth in methods.OrderBy(x => x.Name))
            {
                Console.WriteLine("\t{0}/{1}", meth.Name, meth.Id);
            }
            // ПЕРЕМЕННЫЕ все

            Variable[] variables = client.GetVariablesAll(hSvc);
            Console.WriteLine("ПЕРЕМЕННЫЕ все (всего {0}):", variables.Length);
            foreach (var var in variables.OrderBy(x => x.NameRus))
            {
                Console.WriteLine("\t{0} {1}", var.NameRus, var.Id);
            }

            // ПЕРЕМЕННЫЕ по ключу

            variables = client.GetVariables(hSvc, null, null, null, null, null, null, null, null);
            Console.WriteLine("Переменные по id (всего {0}):", variables.Length);
            foreach (var var in variables.OrderBy(x => x.NameRus))
            {
                Console.WriteLine("\t{0} {1}", var.NameRus, var.Id);
            }

            // ПЕРЕМЕННЫЕ по id

            variables = client.GetVariablesByList(hSvc, new int[] { 2, 23, 14, 48, 10, 13, 47, 45, 57 });
            Console.WriteLine("Переменные по id (всего {0}):", variables.Length);
            foreach (var var in variables.OrderBy(x => x.NameRus))
            {
                Console.WriteLine("\t{0} {1}", var.NameRus, var.Id);
            }

            #endregion GET VARIABLES

            #region Получить пункты и станции в регионе
            //
            // ПОЯСНЕНИЯ:
            //
            // - Пункт (site), это пункт наблюдения
            // - Пункты принадлежат станциям (station).
            // - К одной станции могут относиться несколько пунктов одного или разных типов.
            // - Справочник для типов пунктов и типов станций один (расположен в одной таблице station_type).
            // - Пункт и станция имеют свой индекс (строковая величина, не int). Их индексы могут совпадать, а могут и отличаться.

            // Получить пункты в регионе, заданном южной, северной, западной и восточной границами в градусах широты и долготы.
            Site[] sites = client.GetSitesInBox(hSvc, 30, 90, 80, 180);
            sites = client.GetSitesByType(hSvc, 6);
            Console.WriteLine("Пункты в регионе (всего {0}):", sites.Length);

            // Получить пункты, к которым относятся выбранные выше пункты.
            int[] parentIds = sites.Where(x => x.ParentId.HasValue).Select(x => (int)x.ParentId).Distinct().ToArray();
            Site[] parentSites = client.GetSitesByList(hSvc, parentIds);

            //
            // ПОЯСНЕНИЯ:
            //
            // - Пункт (site) имеет атрибуты. Например, широта, долгота, смещение ВСВ, нуль поста и др.
            // - Атрибут имеет значение на данный момент времени. 
            // - Значение атрибута действует с определенного времени, указанного для него, до тех пор, 
            // пока не перекроется в другой момент времени другим значением.
            // - Внимание: так как атрибут является строковым, то он может быть пустым.
            // - Внимание: так как атрибут является строковым, то он может быть занесён "руками" с ошибкой и при конвертировании,
            //   например, широты в double, нужно использовать методы типа try.

            // Получить широту и долготу выбранных пунктов (как их атрибутов) на заданный момент времени ("на сегодня").
            // 1000 - код типа атрибута пункта для широты, 1000 - код типа атрибута пункта для долготы. 
            EntityAttrValue[] sitesAttrs = client.GetSitesAttrValues(hSvc, sites.Select(x => x.Id).ToArray(),
                new int[] { 1000, 1001 }, DateTime.Today);

            // Пройти по всем пунктам и вывести название, код станции, координаты пункта на консоль
            // Обратите внимание: координаты у пункта, не у станции. У станции нет координат.
            foreach (var site in sites)
            {
                Site parentSite = parentSites.FirstOrDefault(x => x.Id == site.ParentId);

                EntityAttrValue eav = sitesAttrs.FirstOrDefault(x => x.AttrTypeId == 1000 && x.EntityId == site.Id);
                string lat = eav == null ? "нет широты" : eav.Value;
                eav = sitesAttrs.FirstOrDefault(x => x.AttrTypeId == 1001 && x.EntityId == site.Id);
                string lon = eav == null ? "нет долготы" : eav.Value;

                Console.WriteLine("\t{0} {1}, {2} {3} {4}", parentSite.Name, parentSite.Code, site.Id, lat, lon);
            }
            #endregion

            #region Получить НЕ прогностические данные

            //
            // ПОЯСНЕНИЯ:
            //
            // - Данные в БД хранятся двух типов: прогностические и НЕ прогностические
            // - В данном блоке (#region) рассмотрен способ получения не прогностических данных, например, наблюдённых и/или расчётных.
            //
            // 1) Значение данных (измеренное, расчётное, модельное и прогностическое) характеризуется:
            //      - собственно значением (value),
            //      - локальной датой, временем к которому оно относится (date_loc),
            //      - ВСВ датой, временем к которому оно относится (date_utc),
            //      - флагом контроля качества (flag_aqc),
            //      - записью каталога данных для значения (catalog_id).
            //
            // 2) О записи каталога данных для значения (класс Catalog):
            //      - запись каталога хранится в отдельной таблице, связанной с таблицей данных.
            //      - понятие и сущность каталога данных создано для сжатия информации и быстрого поиска данных.
            //      - сущность каталога данных содержит следующие поля:
            //          - id - prim key, sequence
            //          Дальше уникальный ключ записи каталога:
            //          - site_id - код пункта
            //          - variable_id - код переменной 
            //          - offset_id - код смещения
            //          - offset_value - значение смещения
            //          - method_id - код метода, которым получены данные
            //          - source_id - код источника, в котором получены данные
            // 3) Для одной записи каталога и одной даты может быть несколько значений данных (с разными флагами контроля качества).
            // 4) Актуальность значения данных: актуальным называется значение имеющее наивысший флаг контроля качества не равный флагу удалённого значения. 
            //      Такое значение - единственное.

            //
            // Ниже ПЕРВЫЙ ВАРИАНТ получения данных (класс DataValue) где в запросе необходимо указать 
            // все параметры для записи каталога данных + датавремя + флаг + др. 
            // 
            // Примечание: практически во всех методах Get сервиса значение входного параметра равное null
            //      означает "все".

            // Определим некоторые параметры запроса
            byte? flagAQC = null;           // Все флаги
            bool isDateLOC = true;          // Выбирать данные по локальному времени пункта
            //bool isDateLOC = false;       // Выбирать данные по времени ВСВ
            bool isActualValueOnly = true;  // Только актуальные значения
            bool isSelectDeleted = false;   // Не выбирать значения, помеченные как удалённые
            // Получаем данные только для пунктов с типом 2 (гидрологические посты) - для примера
            int[] sitesId = sites.Select(x => x.Id).ToArray();//.Where(x => x.SiteTypeId == 2).Select(x => x.Id).ToArray();
            int[] methodId = null;      // все
            int[] sourceId = null;      // все
            int[] offsetTypeId = null;  // все
            double[] offsetValue = null;// все

            // Получить данные в виде словаря: ключ - запись каталога, значение - массив данных для ключа (записи каталога).
            Dictionary<Catalog, DataValue[]> dvs = client.GetDataValues(
                hSvc,                           // Ручка сервиса
                DateTime.Today.AddDays(-7),   // Время начала отбора данных
                DateTime.Today,                 // Время окончания отбора данных
                isDateLOC,                      // Выбирать данные по локальному времени?
                sitesId,                        // Список id пунктов для отбора данных (null для всех)
                variables.Select(x => x.Id).ToArray(), // Список id переменных для отбора данных (null для всех)
                methodId,                       // Список id методов для отбора данных (null для всех)
                sourceId,                       // Список id источников для отбора данных (null для всех)
                offsetTypeId,                   // Список id типов смещений для отбора данных (null для всех)
                offsetValue,                    // Список значений смещений для отбора данных (null для всех)
                flagAQC,                        // Список флагов автоматизированного контроля качества для отбора данных (null для всех)
                isActualValueOnly,              // Выбирать только актуальные значения?
                isSelectDeleted                 // Выбирать значения помеченные флагом удаления?
            );

            Console.WriteLine("\n\nВсего {0} записей каталога для отобранных пунктов и переменных.", dvs.Count);
            foreach (KeyValuePair<Catalog, DataValue[]> cataloпDV in dvs)
            {
                Catalog ctl = cataloпDV.Key;
                Console.WriteLine("\t{0} site {1} var {2} meth {3} src {4} off {5} offv {6} - count {7} ",
                    ctl.Id, ctl.SiteId, ctl.VariableId, ctl.MethodId, ctl.SourceId, ctl.OffsetTypeId, ctl.OffsetValue,
                    dvs[ctl].Length);
                foreach (DataValue dv in cataloпDV.Value)
                {
                    Console.WriteLine("\t\tDateUTC {0} Value {1} Flag {2}", dv.DateUTC, dv.Value, dv.FlagAQC);
                }
            }

            #endregion GET DATA

            #region Получить записи каталога данных

            //
            // ПОЯСНЕНИЯ:
            //
            // Можно отдельно получить записи каталога данных.
            // Это может быть нужно, например, если за указанный период времени данные не удается выбрать по заданным параметрам 
            // и нужно посмотреть - а были ли вообще такие данные.

            Catalog[] catalogs = client.GetCatalogList(hSvc,
                sites.Select(x => x.Id).ToArray(),
                variables.Select(x => x.Id).ToArray(),
                methodId,
                sourceId,
                offsetTypeId,
                offsetValue
                );
            Console.WriteLine("\n\nВсего {0} записей в каталоге для отобранных пунктов и переменных.", catalogs.Length);
            foreach (var ctl in catalogs.OrderBy(x => x.SiteId))
            {
                Console.WriteLine("\t{0} site {1} var {2} meth {3} src {4} off {5} offv {6}",
                    ctl.Id, ctl.SiteId, ctl.VariableId, ctl.MethodId, ctl.SourceId, ctl.OffsetTypeId, ctl.OffsetValue);
            }

            #endregion
        }
        /// <summary>
        /// Пример работы с БД "Амур" 2.0 с использованием сервиса доступа к данным AmurRWService.
        /// OSokolov aka viator.im@gmail.com
        /// </summary>
        public static void Sample4Bugaets(ServiceClient client, long hSvc)
        {
            Console.WriteLine("\n>>>>>>>>>>>>>>>>>>> Sample start");

            //#region NEW STATION CLASS 2017
            //MyNamespace.StationExt stationExt = MyNamespace.StationExt.GetStation(client, hSvc, "05761", DateTime.Now);
            //Console.WriteLine("Станция {0} {1}. Известные атрибуты: широта {2} долгота {3} UTC-сдвиг {4}", stationExt.Code, stationExt.Name, stationExt.Latitude, stationExt.Longitude, stationExt.UTCOffset);
            //#endregion

            #region СХЕМА SOCIAL
            LegalEntity[] les = client.GetLegalEntityesAll(hSvc); Console.WriteLine("СПРАВОЧНИК субъектов права (всего {0}):", les.Length);
            Org[] orgs = client.GetOrgsAll(hSvc); Console.WriteLine("СПРАВОЧНИК специфики субъекта для организаций (всего {0}):", orgs.Length);
            Person[] perss = client.GetPersonsAll(hSvc); Console.WriteLine("СПРАВОЧНИК специфики субъекта для физ. лиц (всего {0}):", perss.Length);
            #endregion SOCIAL

            #region СХЕМА META (получить данных справочников)

            // СПРАВОЧНИК типов переменных
            VariableType[] varTypes = client.GetVariableTypesAll(hSvc);
            Console.WriteLine("СПРАВОЧНИК типов переменных (всего {0}):", varTypes.Length);
            foreach (var varType in varTypes.OrderBy(x => x.Name))
            {
                Console.WriteLine("\t{0}/{1}", varType.Name, varType.Id);
            }

            // СПРАВОЧНИК ед. измерения
            Unit[] units = client.GetUnitsAll(hSvc);
            Console.WriteLine("СПРАВОЧНИК ед. измерения (всего {0}):", units.Length);
            foreach (var unit in units.Where(x => x.SIConvertion.HasValue).OrderBy(x => x.Name))
            {
                Console.WriteLine("\t{0}/{1}", unit.Name, unit.Id);
            }

            // СПРАВОЧНИК типов данных
            DataType[] dataTypes = client.GetDataTypesAll(hSvc);
            Console.WriteLine("СПРАВОЧНИК типов данных (всего {0}):", dataTypes.Length);
            foreach (var dataType in dataTypes.OrderBy(x => x.Name))
            {
                Console.WriteLine("\t{0}/{1}", dataType.Name, dataType.Id);
            }

            // СПРАВОЧНИК категорий
            GeneralCategory[] genCats = client.GetGeneralCategoryesAll(hSvc);
            Console.WriteLine("СПРАВОЧНИК категорий (всего {0}):", genCats.Length);
            foreach (var genCat in genCats.OrderBy(x => x.Name))
            {
                Console.WriteLine("\t{0}/{1}", genCat.Name, genCat.Id);
            }

            // СПРАВОЧНИК типов значений
            TestService.AmurServiceRWReference.ValueType[] valTypes = client.GetValueTypesAll(hSvc);
            Console.WriteLine("СПРАВОЧНИК типов значений (всего {0}):", valTypes.Length);
            foreach (var valType in valTypes.OrderBy(x => x.Name))
            {
                Console.WriteLine("\t{0}/{1}", valType.Name, valType.Id);
            }

            // СПРАВОЧНИК образцов
            SampleMedium[] samples = client.GetSampleMediumsAll(hSvc);
            Console.WriteLine("СПРАВОЧНИК образцов (всего {0}):", samples.Length);
            foreach (var sample in samples.OrderBy(x => x.Name))
            {
                Console.WriteLine("\t{0}/{1}", sample.Name, sample.Id);
            }

            // СПРАВОЧНИК типов смещений
            OffsetType[] offsetTypes = client.GetOffsetTypesAll(hSvc);
            OffsetType offsetType = offsetTypes.FirstOrDefault(x => x.Id == -777);
            Console.WriteLine("СПРАВОЧНИК типов смещений (всего {0}):", samples.Length);
            foreach (var off in offsetTypes.OrderBy(x => x.Name))
            {
                Console.WriteLine("\t{0}/{1}", off.Name, off.Id);
            }

            // СПРАВОЧНИК методов
            Method[] methods = client.GetMethodsAll(hSvc);
            MethodForecast[] methodFcss = client.GetMethodForecastsAll(hSvc);
            Console.WriteLine("СПРАВОЧНИК методов (всего {0}):", methods.Length);
            foreach (var meth in methods.OrderBy(x => x.Name))
            {
                Console.WriteLine("\t{0}/{1}", meth.Name, meth.Id);
            }
            // ПЕРЕМЕННЫЕ все

            Variable[] variables = client.GetVariablesAll(hSvc);
            Console.WriteLine("ПЕРЕМЕННЫЕ все (всего {0}):", variables.Length);
            foreach (var var in variables.OrderBy(x => x.NameRus))
            {
                Console.WriteLine("\t{0}", var.NameRus);
            }

            // ПЕРЕМЕННЫЕ по ключу

            variables = client.GetVariables(hSvc, null, null, null, null, null, null, null, null);
            Console.WriteLine("Переменные по id (всего {0}):", variables.Length);
            foreach (var var in variables.OrderBy(x => x.NameRus))
            {
                Console.WriteLine("\t{0}", var.NameRus);
            }

            // ПЕРЕМЕННЫЕ по id

            variables = client.GetVariablesByList(hSvc, new int[] { 2, 23, 14, 48, 10, 13, 47, 45, 57 });
            Console.WriteLine("Переменные по id (всего {0}):", variables.Length);
            foreach (var var in variables.OrderBy(x => x.NameRus))
            {
                Console.WriteLine("\t{0}", var.NameRus);
            }

            #endregion GET VARIABLES

            #region Получить пункты (site) по их типу

            int siteTypeId = 2;
            Site[] sites = client.GetSitesByType(hSvc, siteTypeId);
            Site[] parentSites = client.GetSitesByList(hSvc, sites.Where(x => x.ParentId.HasValue).Select(x => (int)x.ParentId).ToArray());
            Console.WriteLine("Пункты типа {1} (всего {0}):", sites.Length, siteTypeId);
            foreach (var site in sites)
            {
                Site parentSite = parentSites.FirstOrDefault(x => x.Id == site.ParentId);
                Console.WriteLine("\t{0} {1}, {2}", parentSite.Name, parentSite.Code, site.Id);
            }

            #endregion

            #region Получить организации - владельцев станций
            {
                foreach (Site station in parentSites)
                {
                    if (station.OrgId.HasValue)
                    {
                        LegalEntity le = client.GetLegalEntity(hSvc, (int)station.OrgId);
                        Console.WriteLine("Организация станции {0}: {1}", station.Name, le.NameRus);
                    }
                    else
                        Console.WriteLine("Для станции {0} не заполнено поле \"Организация\".", station.Name);
                }
            }
            #endregion

            #region Получить широту, долготу и смещение по времени от ВСВ для пункта на дату.

            EntityAttrValue lat = client.GetSiteAttrValue(hSvc, sites[0].Id, 1000, DateTime.Today); // Широта пункта на текущую дату
            EntityAttrValue lon = client.GetSiteAttrValue(hSvc, sites[0].Id, 1001, DateTime.Today); // Долгота пункта на текущую дату
            EntityAttrValue utcOffset = client.GetSiteAttrValue(hSvc, sites[0].Id, 1003, DateTime.Today); // ВСВ-смещение

            Console.WriteLine("Широта {0} Долгота {1} Пояс {2} пункта с индексом {3}",
                double.Parse(lat.Value), double.Parse(lon.Value), double.Parse(utcOffset.Value), parentSites[0].Code);

            EntityAttrValue[] eav = client.GetSitesAttrValues(hSvc, sites.Select(x => x.Id).ToArray(), new int[] { 1000, 1000, 1003 }, DateTime.Today);
            Console.WriteLine("Считано {0} значений атрибутов для {1} пунктов", eav.Length, sites.Length);

            #endregion

            #region Получить все типы атрибутов сайтов
            SiteAttrType[] sat = client.GetSiteAttrTypesAll(hSvc);
            Console.WriteLine("Справочник атрибутов пунктов (всего {0}):", sat.Length);
            foreach (var sa in sat)
            {
                Console.WriteLine("\t{0} {1}", sa.Name, sa.Id);
            }
            #endregion

            #region Получить записи каталога данных для пунктов и переменных
            int[] methodId = null;
            int[] sourceId = null;
            int[] offsetTypeId = null;
            double[] offsetValue = null;

            Catalog[] catalogs = client.GetCatalogList(hSvc,
                sites.Select(x => x.Id).ToArray(),
                variables.Select(x => x.Id).ToArray(),
                methodId,
                sourceId,
                offsetTypeId,
                offsetValue
                );
            Console.WriteLine(" Всего {0} записей в каталоге для отобранных пунктов и переменных.", catalogs.Length);
            foreach (var ctl in catalogs.OrderBy(x => x.SiteId))
            {
                Console.WriteLine("\t{0} site {1} var {2} meth {3} src {4} off {5} offv {6}",
                    ctl.Id, ctl.SiteId, ctl.VariableId, ctl.MethodId, ctl.SourceId, ctl.OffsetTypeId, ctl.OffsetValue);
            }

            #endregion

            #region Получить данные

            byte? flagAQC = null; // Все флаги качества данных
            bool isDateLOC = true;  // Выборка данных по времени станции
            bool isActualValueOnly = true; // Только актуальные значения
            bool isSelectDeleted = false; // Не выбирать значения, помеченные как удалённые

            int[] sitesId = sites.Where(x => x.TypeId == 2).Select(x => x.Id).ToArray();

            Dictionary<Catalog, DataValue[]> dvs = client.GetDataValues(hSvc,
                DateTime.Today.AddDays(-300),
                DateTime.Today,
                isDateLOC,
                sitesId,
                variables.Select(x => x.Id).ToArray(),
                methodId,
                sourceId,
                offsetTypeId,
                offsetValue,
                flagAQC,
                isActualValueOnly,
                isSelectDeleted
            );

            Console.WriteLine(" Всего {0} записей в каталоге для отобранных пунктов и переменных.", dvs.Count);
            foreach (var ctl in dvs.Keys.OrderBy(x => x.SiteId))
            {
                Console.WriteLine("\t{0} site {1} var {2} meth {3} src {4} off {5} offv {6} - count {7} ",
                    ctl.Id, ctl.SiteId, ctl.VariableId, ctl.MethodId, ctl.SourceId, ctl.OffsetTypeId, ctl.OffsetValue,
                    dvs[ctl].Length);
            }

            #endregion GET DATA
        }

        public static void SampleParser(ServiceClient client, long hSvc)
        {

            SysObj sysObj = client.GetParserSysObj(hSvc, 8);
            Console.WriteLine("SysObj.Name = {0}", (sysObj is null) ? "null" : sysObj.Name);

            SysParsersXSites[] pxs = client.GetParserSysParsersXSites(hSvc, 8);
            Console.WriteLine("SysParsersXSites.Count = {0}", (pxs is null || pxs.Length == 0) ? 0 : pxs.Length);

            SysParsersParams[] pps = client.GetParserSysParsersParams(hSvc, new int[] { 1, 2 });
            Console.WriteLine("GetParserSysParsersParams.Count = {0}", (pps is null || pps.Length == 0) ? 0 : pps.Length);
        }
        //////public static void Sample4DataForecast(ServiceClient client, long hSvc)
        //////{
        //////    #region GET FCS VARIABLES
        //////    List<Variable> variables = client.GetVariables(hSvc, null, null, null, null, null, null, null, new int[] { 3 }).ToList();
        //////    Console.WriteLine("ПЕРЕМЕННЫЕ (всего {0})", variables.Count);
        //////    #endregion

        //////    #region GET SITES BY TYPE
        //////    string[] stationCodeList = new string[] { "5132", "31981", "5280", "5167", "05148", "05160", "05122", "31878", "05094", "5171", "05092", "05085", "31942", "05135", "5166", "31935", "05761", "31939", "31961", "31962" };
        //////    Station[] stations = client.GetStationsByList(hSvc, new int[] { 2, 5 });
        //////    stations = client.GetStationsByIndices(hSvc, stationCodeList);
        //////    List<Site> sites = new List<Site>();
        //////    foreach (var station in stations)
        //////    {
        //////        List<Site> site = client.GetSitesByStation(hSvc, station.Id, null).ToList();
        //////        if (site != null && site.Count > 0 && site.Exists(x => x.TypeId == 1 || x.TypeId == 2))
        //////            sites.Add(site.Find(x => x.TypeId == 1 || x.TypeId == 2));
        //////        else
        //////            Console.WriteLine(string.Format("Для станции {0} отсутствует пункт наблюдения", station.Code));
        //////    }
        //////    Console.WriteLine(string.Format("Получено {0} постов для {1} станций", sites.Count, stationCodeList.Length));
        //////    #endregion

        //////    #region GET CATALOGS FOR SITES & VARIABLES && GET DATA FORECAST
        //////    int[] methodId = null;
        //////    int[] sourceId = null;
        //////    int[] offsetTypeId = null;
        //////    double[] offsetValue = null;

        //////    Catalog[] catalogs = client.GetCatalogList(hSvc,
        //////        sites.Select(x => x.Id).ToArray(),
        //////        variables.Select(x => x.Id).ToArray(),
        //////        methodId,
        //////        sourceId,
        //////        offsetTypeId,
        //////        offsetValue
        //////        );
        //////    Console.WriteLine("Всего {0} записей в каталоге для отобранных пунктов и переменных.", catalogs.Length);

        //////    // GET DATA VARIANT 1
        //////    foreach (var ctl in catalogs.OrderBy(x => x.SiteId))
        //////    {
        //////        Console.Write("\t{0} site {1} var {2} meth {3} src {4} off {5} offv {6}",
        //////            ctl.Id,
        //////            sites.Find(x => x.Id == ctl.SiteId).Code,
        //////            variables.Find(x => x.Id == ctl.VariableId).NameRus,
        //////            ctl.MethodId, ctl.SourceId, ctl.OffsetTypeId, ctl.OffsetValue);

        //////        DataForecast[] data = client.GetDataForecasts(hSvc, ctl.Id, DateTime.Now.AddDays(-10), DateTime.Now, null, false);
        //////        if (data.Length > 0)
        //////        {
        //////            Console.Write(" --- {0} data items readed", data.Length);
        //////        }
        //////        else
        //////            Console.Write(" --- no data");
        //////        Console.WriteLine();
        //////    }

        //////    // GET DATA VARIANT 2
        //////    Dictionary<int/*Catalog.Id*/, DataForecast[]> datadic = client.GetDataForecastsByIdList(hSvc, catalogs.Select(x => x.Id).ToArray(),
        //////        DateTime.Now.AddDays(-10), DateTime.Now, null, false);
        //////    foreach (KeyValuePair<int, DataForecast[]> kvp in datadic)
        //////    {
        //////        if (kvp.Value.Length > 0)
        //////            Console.WriteLine("{0} {1} ", kvp.Key, kvp.Value.Length);
        //////    }
        //////    #endregion
        //////}

        /// <summary>
        /// 
        /// Пример 1 работы с сервисом доступа к данным БД "Амур", ДВНИГМИ
        /// 
        /// 1. Получение данных с использованием тестового метода сервиса: 
        ///    всех станций для двух переменных за 10 часов.
        /// 2. Определение станций, параметров и др., присутствующих в полученном наборе значений.
        /// 3. Вывод метаинформации о данных на консоль.
        /// 
        /// OSokolov@ferhri.ru
        /// </summary>
        public static void Sample1(ServiceClient client, long hSvc)
        {
            // Получаем данные (из тестового метода - считывается 5 часов мая 2014 г для двух параметров по всем пунктам)
            DataValue[] dvs = new DataValue[0]; //client.TestDataValues(hSvc);

            // Определяем уникальные коды каталога данных для полученных значений
            List<int> ids = dvs.Select(x => x.CatalogId).Distinct().ToList();
            ids.RemoveRange(10, ids.Count - 10); // Удаляем часть записей, так как их м.б. много и сервис поломается по квоте. Как бороться пока не знаю.
            // Получаем элементы каталога
            Catalog[] ctls = client.GetCatalogListById(hSvc, ids.ToArray());

            // Определяем уникальные коды пунктов для элементов каталога
            ids = ctls.Select(x => x.SiteId).Distinct().ToList();
            // Получаем пункты
            Site[] sites = client.GetSitesByList(hSvc, ids.ToArray());

            // Определяем уникальные коды переменных
            ids = ctls.Select(x => x.VariableId).Distinct().ToList();
            // Получаем переменные
            Variable[] vars = client.GetVariablesByList(hSvc, ids.ToArray());

            Console.WriteLine("\n--- SAMPLE 1 ---\n");
            Console.WriteLine("{0} значений данных считано. В которых:", dvs.Length);
            Console.WriteLine("   {0} записей каталога.", ctls.Count());
            Console.WriteLine("   {0} пунктов.", sites.Count());
            Console.WriteLine("   {0} переменных.", vars.Count());
        }

        ///////// <summary>
        ///////// 
        ///////// Пример 2 работы с сервисом доступа к данным БД "Амур", ДВНИГМИ
        ///////// 
        ///////// 1. Формирование запроса к данным (период, станции, параметры и др.)
        ///////// 2. Получение данных.
        ///////// 3. Определение станций, параметров и др., присутствующих в полученном наборе значений.
        ///////// 4. Вывод метаинформации о данных на консоль.
        ///////// 
        ///////// OSokolov@ferhri.ru
        ///////// </summary>
        //////public static void Sample2(ServiceClient client, long hSvc)
        //////{
        //////    // Формирование запроса - ДАТЫ начала и окончания временного периода

        //////    DateTime dateSLOC = new DateTime(2016, 5, 1);
        //////    DateTime dateFLOC = new DateTime(2016, 6, 1);

        //////    // Формирование запроса через индексы (коды) станций

        //////    string[] stationIndexList = new string[]
        //////        {
        //////            "31866"
        //////            //"31735"
        //////            //,"05012"
        //////            //,"05013"
        //////        };

        //////    // Формирование запроса - ПЕРЕМЕННЫЕ

        //////    int[] variableIdList = new int[]
        //////        {
        //////            7, // WindSpeed
        //////            10, // TempAir
        //////            13, // TempWater
        //////            2,  // GageHeight
        //////            47  // GageHeightDayly
        //////        };

        //////    // Формирование запроса - прочие атрибуты запроса

        //////    int[] offsetTypeId = null;      // Все типы смещений
        //////    double[] offsetValue = null;     // Все значения смещений
        //////    bool isActualOnly = true;       // Выбирать только актуальные (последние) значения
        //////    bool isSelectDeleted = false;   // Выбирать значения, помеченные как удалённые
        //////    byte? flagAQC = null;           // Выбирать значения с любым флагом АКК
        //////    int[] methodId = null;           // Все методы получения данных (наблюдённые, расчётные и др.)
        //////    int[] sourceId = null;           // Все источники данных

        //////    // Получение значений данных, соответствующих запросу

        //////    Dictionary<Catalog, DataValue[]> dvs = client.GetDataValues(hSvc,
        //////        dateSLOC, dateFLOC, true,
        //////        sites.Select(x => x.Id).Distinct().ToArray(),
        //////        variableIdList,
        //////        methodId, sourceId,
        //////        offsetTypeId, offsetValue,
        //////        flagAQC,
        //////        isActualOnly, isSelectDeleted
        //////    );

        //////    Console.WriteLine("***********************SAMPLE 2");
        //////    Console.WriteLine("Считано {0} записей каталога", dvs.Count);
        //////    foreach (KeyValuePair<Catalog, DataValue[]> kvp in dvs)
        //////    {
        //////        Console.WriteLine("\t Catalog.Id={0} DataValues count {1}", kvp.Key.Id, kvp.Value.Length);
        //////    }


        //////    // Определяем уникальные коды каталога данных для полученных значений
        //////    int[] ids = dvs.Select(x => x.Key.Id).Distinct().ToArray();
        //////    // Получаем элементы каталога
        //////    Catalog[] ctls = client.GetCatalogListById(hSvc, ids);

        //////    // Определяем уникальные коды пунктов для элементов каталога
        //////    ids = ctls.Select(x => x.SiteId).Distinct().ToArray();
        //////    // Получаем пункты
        //////    Site[] sites1 = client.GetSitesByList(hSvc, ids);

        //////    // Определяем уникальные коды переменных
        //////    ids = ctls.Select(x => x.VariableId).Distinct().ToArray();
        //////    // Получаем переменные
        //////    Variable[] vars1 = client.GetVariablesByList(hSvc, ids);

        //////    // Вывод результатов на консоль

        //////    Console.WriteLine("\n--- SAMPLE 2 ---\n");
        //////    Console.WriteLine("Период {0} - {1}", dateSLOC.ToString("dd.MM.yyyy"), dateFLOC.ToString("dd.MM.yyyy"));
        //////    Console.WriteLine("Запрошено {0} пунктов {1} переменных",
        //////        sites.Count(), variableIdList.Length);
        //////    Console.WriteLine("   всего XXX значений для {1} записей каталога",
        //////        ctls.Length);
        //////}
    }
}
