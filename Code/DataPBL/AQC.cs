using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Amur.Meta;
using SOV.Amur.Data;

namespace SOV.Amur.DataP
{
    /// <summary>
    /// Automatic Quality Control (AQC)
    /// Автоматический критконтроль и корректировка данных.
    /// </summary>
    public class AQC
    {
        /// <summary>
        /// Код данной сущности - процедуры АКК.
        /// </summary>
        int THIS_SYS_ENTITY_ID = (int)Sys.EntityEnum.ProcedureAQC;
        /// <summary>
        /// Расширение временного интервала считывания данных "назад" для AQC тенденций (час).
        /// </summary>
        int HOURS_BACKWARD = 24;
        /// <summary>
        /// Значения атрибутов обрабатываемого сайта актуальные на дату АКК
        /// </summary>
        List<EntityAttrValue> _savs;
        /// <summary>
        /// Все типы атрибутов пункта
        /// </summary>
        List<EntityAttrType> _sats = Meta.DataManager.GetInstance().EntityAttrRepository.SelectAttrTypes("site");
        /// <summary>
        /// Все переменные
        /// </summary>
        List<Variable> _vars = Meta.DataManager.GetInstance().VariableRepository.Select();
        /// <summary>
        /// Все правила АКК
        /// </summary>
        List<AQCRole> _rolesAll = DataP.DataManager.GetInstance().AQCRepository.SelectRoles();
        /// <summary>
        /// Корневая запись лога для сообщений времени выполнения АКК.
        /// </summary>
        int _rootLogId = -1;

        /// <summary>
        /// 
        /// Выполнение процедуры Автоматического Контроля Качества данных (АКК ака AQC):
        /// 
        /// - выборка из БД правил, в соответствие с которыми проводится АКК (ПАКК);
        /// 
        /// -----------------------------------------------------------------------------------
        /// - НП ЦИКЛ: организация цикла проведения АКК для каждого наблюдательного пункта (НП) 
        ///     указанного в фактических параметрах метода или для всех НП.
        ///     - выборка климатических данных;
        ///     
        ///     --------------------------------------------------------------------------------
        ///     - ПАКК ЦИКЛ: организация цикла проведения АКК для каждого ПАКК:
        ///         - выборка данных НП для переменной, указанной в ПАКК;
        ///         - выборка данных для ссылочного пункта (НПСС) для переменной, указанной в ПАКК;
        ///         - выборка климатических данных;
        ///         - выставление флага АКК (АККФ) в 0 для считанных данных (данные без АКК);
        ///         - удаление информации о ранее проведённом АКК для считанных данных (квалификаторов АКК - АККк) 
        ///             за исключением тех квалификаторов, которые помечены как "не удалять при проведении АКК";
        ///             
        ///         - проверка правила АКК для каждого значения и формирование наборов результатов проверки;
        ///     - ПАКК ЦИКЛ завершён.
        ///     ------------------------------------------------------------------------------
        ///     
        ///     - ЗАПИСЬ результатов АКК:
        ///         - запись АККк;
        ///         - выставление соответствующих флагов АКК для обработанных значений.
        ///         
        /// - НП ЦИКЛ завершён.
        /// -----------------------------------------------------------------------------------
        /// 
        /// </summary>
        public void Run(DateTime dateS, DateTime dateF, /*int yearSClm, int yearFClm,*/ List<int> siteIdList = null)
        {
            _rootLogId = Sys.DataManager.GetInstance().LogRepository.Insert(THIS_SYS_ENTITY_ID, "Start");


            // PREPARE SITES, DATAFILTER, CLIMATE etc.
            Common.DateTimePeriod dtp = new Common.DateTimePeriod(dateS.AddHours(-HOURS_BACKWARD), dateF, Common.DateTimePeriod.Type.Period, 0);
            DataFilter df = new DataFilter(dtp, null, false, false, true, true, new CatalogFilter());

            List<Site> sites = Meta.DataManager.GetInstance().SiteRepository.Select(siteIdList);
            // Climate nearest to date and longest for all variables
            List<Climate> climateAll = Data.DataManager.GetInstance().ClimateRepository.SelectClimateNearestMetaAndData(
                    dateS.Year, 1,
                    sites.Select(x => x.Id).ToList(), null,
                    df.CatalogFilter.OffsetTypes != null ? (int?)df.CatalogFilter.OffsetTypes[0] : null,
                    df.CatalogFilter.OffsetValue, null, null);

            List<DataValue> dvs = new List<DataValue>();
            List<DataValue> dvsRef = new List<DataValue>();
            //List<int> siteHydroSeasonMonthes = null; // месяцы начала гидросезонов пункта

            // LOOP SITES

            foreach (Site site in sites)
            {
                // Nearest to date climate data for all variables
                List<Climate> climateSite = Data.DataManager.GetInstance().ClimateRepository.SelectClimateData(climateAll.FindAll(x => x.SiteId == site.Id));
                _savs = Meta.DataManager.GetInstance().EntityAttrRepository.SelectAttrValues("site", new List<int>(new int[] { site.Id }), null, DateTime.Now).Where(x => x.Value != null).ToList();

                // LOOP ROLES

                int varIdPrev = -1;
                foreach (var role in _rolesAll.OrderBy(x => x.VariableId))
                {
                    #region READ SITE DATA 4 VARIABLE
                    if (role.VariableId != varIdPrev)
                    {
                        varIdPrev = role.VariableId;
                        dvs.Clear();
                        dvsRef.Clear();

                        // Read site data
                        df.CatalogFilter.Sites = new List<int>(new int[] { site.Id });
                        df.CatalogFilter.Variables = new List<int>(new int[] { role.VariableId });
                        dvs = Data.DataManager.GetInstance().DataValueRepository.SelectA(df).OrderBy(x => x.DateLOC).ToList();
                        if (dvs.Count != 0)
                        {
                            // FlagAQC -> zero
                            _repDV.UpdateFlagAQC(dvs.Select(x => x.Id).ToList(), 0);
                            // Delete data value's aqc
                            _repAQC.DeleteDataValueAQC(dvs.Select(x => x.Id).ToList());

                            // Read main/reference site data if exists
                            dvsRef = ReadReferenseSiteData(df, site);
                        }
                    }
                    if (dvs.Count == 0)
                        continue;
                    #endregion READ DATA 4 SITE

                    #region AQC ROLE SWITCH & PROCESS

                    switch (role.RoleType)
                    {
                        case "climate":
                            //AQCClimate(role, dv, clm);
                            break;
                        case "site_auto_diff":
                            AQCDiff(role, site, dvs, dvsRef);
                            break;
                        case "tendention":
                            //AQCTendention(role, dv, dvs, site, clm, siteHydroSeasonMonthes);
                            break;
                        case "990":
                            AQC990(role, dvs);
                            break;
                        default:
                            throw new Exception("Неизвестный тип правила для критконтроля значений: " + role.RoleType);
                    }

                    #endregion AQC ROLE SWITCH & PROCESS
                }

                // WRITE AQC RESULTS FOR SITE

                List<long> dvId = _aqcDV.Where(x => x.IsAQCApplied)
                    .Select(x => x.DataValueId).Distinct().ToList();
                _repDV.UpdateFlagAQC(dvId, (int)EnumFlagAQC.Success);

                _aqcDV.RemoveAll(x => x.IsAQCApplied);
                _repAQC.InsertDataValueAQC(_aqcDV);

                dvId = _aqcDV.Where(x =>
                    x.IsAQCApplied
                    && _rolesAll.Exists(y => y.Id == x.AQCRoleId && y.IsCritical))
                    .Select(x => x.DataValueId).Distinct().ToList();
                _repDV.UpdateFlagAQC(dvId, (int)EnumFlagAQC.Error);

                dvId = _aqcDV.Select(x => x.DataValueId).Distinct().Except(dvId).ToList();
                if (dvId.Count > 0)
                    _repDV.UpdateFlagAQC(dvId, (int)EnumFlagAQC.Success);

                _aqcDV.Clear();
            }
            Sys.DataManager.GetInstance().LogRepository.Insert(THIS_SYS_ENTITY_ID, "Finish", _rootLogId);
        }

        DataValueRepository _repDV = Data.DataManager.GetInstance().DataValueRepository;
        AQCRepository _repAQC = DataP.DataManager.GetInstance().AQCRepository;

        private void AQC990(AQCRole role, List<DataValue> dvs)
        {
            foreach (var dv in dvs)
            {
                if (dv.Value == 990)
                    if (!dvs.Exists(x => x.CatalogId == dv.CatalogId && x.Value == 0 && x.DateLOC == dv.DateLOC))
                    {
                        DataValue dv1 = new DataValue(-1, dv.CatalogId, 0.0, dv.DateLOC, dv.DateLOC, 1, dv.UTCOffset);
                        dv1.Id = _repDV.Insert(dv1);
                        _repAQC.InsertDataValueAQC(dv1.Id, role.Id, true);
                    }
                    else
                    {
                        DataValue dv1 = dvs.Where(x => x.CatalogId == dv.CatalogId && x.Value == 0 && x.DateLOC == dv.DateLOC).ElementAt(0);
                        _repDV.UpdateFlagAQC(new List<long>(new long[] { dv1.Id }), 1);
                    }

            }
        }
        private void AQCTendention(AQCRole role, DataValue dv, List<DataValue> dvs, Site site, List<Climate> clm, List<int[]> siteHydroSeasonMonthes)
        {
            // Найти данные для расчёта тенденции
            List<DataValue> dv1 = dvs.FindAll(x => x.DateLOC == dv.DateLOC.AddHours(int.Parse(role.Role["@HOURS_BACKWARD"])));
            if (dv1 != null && dv1.Count > 0)
            {
                // Найти климат
                Variable clmVariable = Meta.DataManager.GetInstance().VariableRepository.Select(int.Parse(role.Role["@CLM_VARIABLE_ID"]));
                List<Climate> clm1 = clm.FindAll(x =>
                    x.VariableId == clmVariable.Id && x.DataTypeId == (int)EnumDataType.Maximum
                    );

                DataValue dv2 = dv1.Find(x => x.Id == dv1.Max(y => y.Id));

                double? clmTendentionMax = clm1[0].Data[(short)Time.GetTimeNumYear(dv2.DateLOC, clmVariable.TimeId, siteHydroSeasonMonthes)[0]];
                if (clm1.Count != 1
                    || !(clmTendentionMax = clm1[0].Data[(short)Time.GetTimeNumYear(dv2.DateLOC, clmVariable.TimeId, siteHydroSeasonMonthes)[0]]).HasValue)
                {
                    throw new Exception("Отсутствует климат тенденций для пункта " + site.ToString());
                }

                // Проверить условие на тенденцию
                bool ok = Math.Abs(dv2.Value - dv.Value) <= (double)clmTendentionMax;
                _aqcDV.Add(new AQCDataValue() { DataValueId = dv.Id, AQCRoleId = role.Id, IsAQCApplied = true });
            }
        }

        private void AQCClimate(AQCRole role, DataValue dv, List<Climate> clm)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Результаты АКК для пункта
        /// </summary>
        List<AQCDataValue> _aqcDV = new List<AQCDataValue>();

        private void AQCDiff(AQCRole role, Site site, List<DataValue> dvs, List<DataValue> dvsRef)
        {
            EntityAttrType sat = null;

            // НАЙТИ тип атрибута пункта для переменной, указанной в правиле
            switch (role.VariableId)
            {
                case (int)EnumVariable.GageHeightF:
                case (int)EnumVariable.GageHeightAvgDay:
                    sat = _sats.Find(x => x.Id == (int)EnumSiteAttrType.DiffGageAGK);
                    break;
                case (int)EnumVariable.TempWaterF:
                    sat = _sats.Find(x => x.Id == (int)EnumSiteAttrType.DiffTempAGK);
                    break;
                default:
                    foreach (var dv in dvs)
                    {
                        _aqcDV.Add(new AQCDataValue() { DataValueId = dv.Id, AQCRoleId = role.Id, IsAQCApplied = false });
                    }
                    Sys.DataManager.GetInstance().LogRepository.Insert(THIS_SYS_ENTITY_ID,
                        "Неизвестная переменная роли."
                        + "\nРоль: " + role
                        + "\nПеременная: " + _vars.Find(x => x.Id == role.VariableId).NameRus,
                        _rootLogId, true
                    );
                    return;
            }
            // ОБЯЗАТЕЛЬНО ЛИ правило для данного типа пункта?
            if (!sat.Mandatories.Exists(x => x == site.TypeId))
                return;

            EntityAttrValue sav = _savs.Find(x => x.AttrTypeId == sat.Id);
            if (sav == null)
            {
                foreach (var dv in dvs) _aqcDV.Add(new AQCDataValue() { DataValueId = dv.Id, AQCRoleId = role.Id, IsAQCApplied = false });

                Sys.DataManager.GetInstance().LogRepository.Insert(THIS_SYS_ENTITY_ID,
                    "Для пункта отсутствует обязательный атрибут " + sat.Name + "."
                    + "\nПункт: " + Site.GetName(site, 2, SiteTypeRepository.GetCash())
                    + "(id=" + site.Id + ")"
                    , _rootLogId, true
                );
                return;
            }

            // ПРОВЕРКА правила

            double diff = double.Parse(sav.Value);
            foreach (var dv in dvs)
            {
                DataValue dvRef = dvsRef.Find(x => x.DateLOC == dv.DateLOC);// && x.Catalog.VariableId == dv.Catalog.VariableId);
                if (dvRef != null)
                {
                    bool ok = Math.Abs(dvRef.Value - dv.Value) <= diff;
                    _aqcDV.Add(new AQCDataValue() { DataValueId = dv.Id, AQCRoleId = role.Id, IsAQCApplied = true });
                }
            }
        }
        /// <summary>
        /// Выборка данных основного пункта для переменной вторичного пункта.
        /// </summary>
        /// <param name="df">Фильтр данных вторичного пункта.</param>
        /// <param name="site">Пункт, для которого производится поиск основного пункта. Если это основной пункт, то выборка данных не производится.</param>
        /// <returns></returns>
        private List<DataValue> ReadReferenseSiteData(DataFilter df, Site site)
        {
            if (df.CatalogFilter.Sites.Count != 1) throw new Exception("ALGORITHMIC ERROR: (df.SiteIdList.Count != 1)");

            List<DataValue> ret = new List<DataValue>();

            if (site.ParentId.HasValue)
            {
                Site refSite = Meta.DataManager.GetInstance().SiteRepository.Select((int)site.ParentId);

                if (refSite != null)
                {
                    List<int> id = df.CatalogFilter.Sites;
                    df.CatalogFilter.Sites = new List<int>(new int[] { refSite.Id });
                    bool isOneValue = df.IsActualValueOnly; df.IsActualValueOnly = true;

                    ret = Data.DataManager.GetInstance().DataValueRepository.SelectA(df);

                    df.CatalogFilter.Sites = id; df.IsActualValueOnly = isOneValue;
                }
            }
            return ret;
        }

        // public void Run(DateTime dateS, DateTime dateF, int yearSClm, int yearFClm, List<int> siteIdList = null)
        //{
        //    // PREPARE ROLES
        //    List<AQCRole> rolesAll = (new AQCRepository()).SelectRoles();

        //    // PREPARE SITES, DATAFILTER & MISCEL DEF'S.
        //    DataFilter df = new DataFilter(dateS.AddHours(-HoursBackward), dateF,
        //        null, null, null, null, null, null, null, false, false);

        //    List<Site> sites = (new Meta.SiteRepository()).Select(siteIdList);

        //    List<DataValue> dvs = new List<DataValue>();
        //    List<DataValue> dvsRef = new List<DataValue>();
        //    List<int> siteHydroSeasonMonthes = null; // месяцы начала гидросезонов пункта

        //    // LOOP SITES

        //    foreach (Site site in sites)
        //    {
        //        // SELECT CLIMATE
        //        List<_DELME_Climate> clm = new List<_DELME_Climate>();
        //        clm.AddRange((new _DELME_ClimateRepository()).SelectClimate(null, site.Id, null, df.OffsetTypeId, df.OffsetValue, null, null, yearSClm, yearFClm));

        //        // LOOP ROLES

        //        int varIdPrev = -1;
        //        foreach (var role in rolesAll.OrderBy(x => x.VariableId))
        //        {
        //            #region READ DATA 4 SITE
        //            if (role.VariableId != varIdPrev)
        //            {
        //                varIdPrev = role.VariableId;
        //                dvs.Clear();
        //                dvsRef.Clear();

        //                // Read site data
        //                df.SiteIdList = new List<int>(new int[] { site.Id });
        //                df.VariableIdList = new List<int>(new int[] { role.VariableId });
        //                dvs = (new DataValueRepository()).SelectA(df).OrderBy(x => x.DateLOC).ToList();
        //                if (dvs.Count != 0)
        //                {
        //                    // FlagAQC -> zero
        //                    repDV.UpdateFlagAQC(dvs.Select(x => x.Id).ToList(), 0);
        //                    // Delete data value's aqc
        //                    repAQC.DeleteDataValueAQC(dvs.Select(x => x.Id).ToList());

        //                    // Read main (reference) site data for auto-sites
        //                    dvsRef = ReadReferenseSiteData(df, site);
        //                }
        //            }
        //            if (dvs.Count == 0)
        //                continue;
        //            #endregion READ DATA 4 SITE

        //            // AQC ROLE SWITCH & PROCESS

        //            switch (role.RoleType)
        //            {
        //                case "climate":
        //                    //AQCClimate(role, dv, clm);
        //                    break;
        //                case "site_auto_diff":
        //                    AQCSiteAutoDiff(role, site, dvs, dvsRef);
        //                    break;
        //                case "tendention":
        //                    //AQCTendention(role, dv, dvs, site, clm, siteHydroSeasonMonthes);
        //                    break;
        //                case "990":
        //                    AQC990(role, dvs);
        //                    break;
        //                default:
        //                    throw new Exception("Неизвестный тип правила для критконтроля значений: " + role.RoleType);
        //            }
        //        }

        //        // WRITE AQC RESULTS FOR SITE

        //        if (_aqcDV.Count > 0)
        //        {
        //            repAQC.InsertDataValueAQC(_aqcDV);

        //            List<long> dvId = _aqcDV.Where(x => x.IsAQCApplied && rolesAll.Exists(y => y.Id == x.AQCRole.Id && y.IsCritical)).Select(x => x.DataValueId).Distinct().ToList();
        //            if (dvId.Count > 0)
        //                repDV.UpdateFlagAQC(dvId, (int)EnumFlagAQC.Error);

        //            dvId = _aqcDV.Select(x => x.DataValueId).Distinct().Except(dvId).ToList();
        //            if (dvId.Count > 0)
        //                repDV.UpdateFlagAQC(dvId, (int)EnumFlagAQC.Success);

        //            _aqcDV.Clear();
        //        }
        //    }
        //}
    }
}
