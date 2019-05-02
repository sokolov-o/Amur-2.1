using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FERHRI.Amur.Meta;
using FERHRI.Amur.Data;

namespace Import
{
    /// <summary>
    /// РАЗОВЫЙ импорт таблиц climate в табл data_value.
    /// Повторный импорт не приведёт к багам - можно запускать, но незачем.
    /// 
    /// Работа с базой не через сервис.
    /// 
    /// OSokolov@201710
    /// </summary>
    class AmurClimate2DataValue
    {
        static FERHRI.Amur.Meta.DataManager dmm = FERHRI.Amur.Meta.DataManager.GetInstance();
        static FERHRI.Amur.Data.DataManager dmd = FERHRI.Amur.Data.DataManager.GetInstance();
        //static int _METHOD_CLM = (int)EnumMethod.Climate;
        static int _SOURCE_CLM = 0; // UGMS DV

        static internal void Run()
        {
            // Получить все клим данные из табл climate
            List<Climate> climDataAll = dmd.ClimateRepository.SelectClimateMetaAndData(null, null, null, null, null, null, null, null);
            Console.WriteLine("{0} total climate meta data items.", climDataAll.Count);

            // LOOP BY CLIMATE 0
            for (int i = 0; i < climDataAll.Count; i++)
            {
                Console.Write("{1:0}% - transfer climate meta {0}.", i, (double)i / climDataAll.Count * 100);

                // GET CATALOG 4 CLIMATE
                FERHRI.Amur.Meta.Catalog catalog = GetCatalog4(climDataAll[i]);

                // GET CATALOG ASSOCIATED VARS
                FERHRI.Amur.Meta.Variable var = _Variables.FirstOrDefault(x => x.Id == catalog.VariableId);
                FERHRI.Amur.Meta.MethodClimate mf = _MethodClimates.FirstOrDefault(x => x.MethodId == catalog.MethodId);

                int[] hydroSeasonsMonthStart = null;
                if (var.TimeId == (int)EnumTime.HydroSeason)
                {
                    FERHRI.Amur.Meta.EntityAttrValue eav = dmm.EntityAttrRepository.SelectAttrValue("site", climDataAll[i].SiteId, (int)EnumSiteAttrType.HydroSeasonMonthStart, DateTime.Now);
                    if (eav == null)
                        throw new Exception(string.Format("У пункта {0} отсутствует атрибут \"Номера месяцев начала гидрологических сезонов\".", catalog.SiteId));
                    hydroSeasonsMonthStart = Time.ParseSeasonsMonthesStart(eav.Value);
                }

                // CONVERT: CLIMATE DATA -> DATA VALUES
                List<FERHRI.Amur.Data.DataValue> dvs = new List<FERHRI.Amur.Data.DataValue>();
                foreach (KeyValuePair<short, double> item in climDataAll[i].Data)
                {
                    DateTime date = Time.GetDateSTimeNum(mf.YearS, item.Key, var.TimeId, hydroSeasonsMonthStart);

                    dvs.Add(new FERHRI.Amur.Data.DataValue(-1, catalog.Id, item.Value, date, date, 0, 0));
                }

                // WRITE DATA VALUES
                Console.WriteLine("\tInsert {0} data values...", dvs.Count);
                dmd.DataValueRepository.Insert(dvs);
            }
        }
        static List<MethodClimate> _MethodClimates = new List<MethodClimate>();
        static List<FERHRI.Amur.Meta.Variable> _Variables = new List<FERHRI.Amur.Meta.Variable>();

        private static FERHRI.Amur.Meta.Catalog GetCatalog4(Climate climate)
        {
            #region GET CLIMATE METHOD

            FERHRI.Amur.Meta.MethodClimate methodClimate = _MethodClimates.FirstOrDefault(x => x.YearS == climate.YearS && x.YearF == climate.YearF);
            if (methodClimate == null)
            {
                List<FERHRI.Amur.Meta.MethodClimate> methodClimates = dmm.MethodClimateRepository.Select(climate.YearS, climate.YearF);
                if (methodClimates.Count == 0)
                    throw new Exception(string.Format("Не найден метод расчёта климата с периодом лет {0}-{1}.", climate.YearS, climate.YearF));
                if (methodClimates.Count > 1)
                    throw new Exception(string.Format("Найдено более одного метода расчёта климата с периодом лет {0}-{1}.", climate.YearS, climate.YearF));
                methodClimate = methodClimates[0];
                _MethodClimates.Add(methodClimate);
            }

            #endregion

            #region GET CLIMATE VARIABLE

            int timeSupport = 1;
            FERHRI.Amur.Meta.Variable climateVariable = dmm.VariableRepository.Select(climate.VariableId);

            FERHRI.Amur.Meta.Variable var = new FERHRI.Amur.Meta.Variable(
                -1,
                climateVariable.VariableTypeId,
                climate.TimeId,
                climateVariable.UnitId,
                climate.DataTypeId,
                (int)EnumValueType.DerivedValue,
                climateVariable.GeneralCategoryId,
                climateVariable.SampleMediumId,
                timeSupport,
                string.IsNullOrEmpty(climateVariable.Name) ? null : "Климат " + climate.TimeId + "/" + climate.DataTypeId + " " + climateVariable.Name,
                string.IsNullOrEmpty(climateVariable.NameEng) ? null : "Climate " + climateVariable.NameEng,
                climateVariable.CodeNoData,
                climateVariable.CodeErrData
            );

            FERHRI.Amur.Meta.Variable variable = _Variables.FirstOrDefault(x
                => x.VariableTypeId == var.VariableTypeId
                && x.TimeId == var.TimeId
                && x.UnitId == var.UnitId
                && x.DataTypeId == var.DataTypeId
                && x.GeneralCategoryId == var.GeneralCategoryId
                && x.SampleMediumId == var.SampleMediumId
                && x.TimeSupport == var.TimeSupport
                && x.ValueTypeId == var.ValueTypeId);
            if (variable == null)
            {
                variable = dmm.VariableRepository.Select(
                    var.VariableTypeId,
                    var.TimeId,
                    var.UnitId,
                    var.DataTypeId,
                    var.GeneralCategoryId,
                    var.SampleMediumId,
                    var.TimeSupport,
                    var.ValueTypeId);

                if (variable == null)
                {
                    try
                    {
                        var.Id = dmm.VariableRepository.Insert(var);
                        variable = var;
                        Console.Write("\tVar inserted {0}", variable.ToString());
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                _Variables.Add(variable);
            }
            #endregion

            // GET CLIMATE CATALOG

            FERHRI.Amur.Meta.Catalog ctl = new FERHRI.Amur.Meta.Catalog()
            {
                SiteId = climate.SiteId,
                VariableId = variable.Id,
                MethodId = methodClimate.MethodId,
                SourceId = _SOURCE_CLM, // UGMS DV
                OffsetTypeId = climate.OffsetTypeId,
                OffsetValue = climate.OffsetValue
            };
            FERHRI.Amur.Meta.Catalog catalog = dmm.CatalogRepository.Select(ctl.SiteId, ctl.VariableId, ctl.MethodId, ctl.SourceId, ctl.OffsetTypeId, ctl.OffsetValue);
            if ((object)catalog == null)
            {
                catalog = dmm.CatalogRepository.Insert(ctl);
                Console.Write(" Catalog inserted {0}", catalog.ToString());
            }

            Console.WriteLine();
            return catalog;
        }
    }
}
