using System;
using System.Collections.Generic;
using System.Linq;
using Npgsql;
using SOV.Common;

namespace SOV.Amur.Data
{
    public class ClimateRepository : BaseRepository<Climate>
    {
        internal ClimateRepository(Common.ADbNpgsql db) : base(db, "data.climate_0") { }

        public List<Climate> SelectClimateMetaAndData(
            List<int> siteId, List<int> variableId,
            int? offsetTypeId, double? offsetValue,
            int? dataTypeId,
            int? timeId,
            int? yearS, int? yearF)
        {
            return SelectClimateData(SelectClimateMeta(siteId, variableId, offsetTypeId, offsetValue, dataTypeId, timeId, yearS, yearF));
        }

        /// <summary>
        /// Выборка метаданных о климате, соответствующих запросу.
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="variableId"></param>
        /// <param name="offsetTypeId"></param>
        /// <param name="offsetValue"></param>
        /// <param name="dataTypeId"></param>
        /// <param name="timeId"></param>
        /// <param name="yearS"></param>
        /// <param name="yearF"></param>
        /// <returns></returns>
        public List<Climate> SelectClimateMeta(
            List<int> siteId, List<int> variableId,
            int? offsetTypeId, double? offsetValue,
            int? dataTypeId,
            int? timeId,
            int? yearS, int? yearF)
        {
            var fields = new Dictionary<string, object>()
            {
                { "site_id", siteId },
                { "variable_id", variableId },
                { "offset_type_id", offsetTypeId },
                { "offset_value", offsetValue },
                { "data_type_id", dataTypeId },
                { "time_id", timeId },
                { "year_s", yearS }, { "year_f", yearF },
            };
            return Select(fields);
        }
        /// <summary>
        /// Функция парсинга строки результата запроса. Используется по умолчанию в ExecQuery.
        /// </summary>
        protected override object ParseData(NpgsqlDataReader reader)
        {
            return new Climate(
                (int)reader["id"],
                (int)reader["site_id"],
                (int)reader["variable_id"],
                (int)reader["offset_type_id"],
                (double)reader["offset_value"],
                (int)reader["data_type_id"],
                (int)reader["time_id"],
                (Int16)reader["year_s"],
                (Int16)reader["year_f"]
            );
        }

        /// <summary>
        /// Выборка климатических данных с указанием года, к которому интервал окончания периода расчёта климата наиболее близок. 
        /// С дополнительным условием на начало периода расчёта климата.
        /// </summary>
        /// <param name="yearNearest2ClimateF">Год, к которому должен быть выбран климат по дате окончания периода расчёта климата.</param>
        /// <param name="yearClimateSMinOrMax">1 - минимальный год начала расчёта климата (максимальный период расчёта климата);
        /// 2 - максимальный год начала расчёта климата (минимальный период расчёта климата)</param>
        /// <param name="siteId"></param>
        /// <param name="variableId"></param>
        /// <param name="offsetTypeId"></param>
        /// <param name="offsetValue"></param>
        /// <param name="dataTypeId"></param>
        /// <param name="timeId"></param>
        public virtual List<Climate> SelectClimateNearestMetaAndData(
            int yearNearest2ClimateF, Int16 yearClimateSMinOrMax,
            List<int> siteId, List<int> variableId,
            int? offsetTypeId, double? offsetValue,
            int? dataTypeId,
            int? timeId)
        {
            return SelectClimateData(SelectClimateNearestMeta(yearNearest2ClimateF, yearClimateSMinOrMax, siteId, variableId, offsetTypeId, offsetValue, dataTypeId, timeId));
        }

        public List<Climate> SelectClimateNearestMeta(
            int yearFNearest2, Int16 yearSMinOrmax,
            List<int> siteId, List<int> variableId,
            int? offsetTypeId, double? offsetValue,
            int? dataTypeId,
            int? timeId)
        {
            string sql = "data.fn_climate_0_nearest_select";
            Dictionary<string, object> fields = new Dictionary<string, object>()
            {
                { "_year_f_nearest2", yearFNearest2 },
                { "_year_s_min_or_max", yearSMinOrmax },
                { "_site_id", siteId },
                { "_variable_id", variableId },
                { "_offset_type_id", offsetTypeId },
                { "_offset_value", offsetValue },
                { "_clm_data_type_id", dataTypeId },
                { "_clm_time_id", timeId },
            };
            return ExecQuery<Climate>(sql, fields, ParseData, System.Data.CommandType.StoredProcedure);
        }

        public List<Climate> SelectClimateData(List<Climate> climateList)
        {
            string sql = "Select * " +
                        "From data.climate_1 " +
                        "Where climate_0_id = ANY(:climate_0_id) ";
            var fields = new Dictionary<string, object>()
            {
                { "climate_0_id", climateList.Select(x => x.Id).ToList() }
            };
            foreach (var data in ExecQuery<Dictionary<string, object>>(sql, fields, base.ParseData))
                climateList.Find(x => x.Id == (int)data["climate_0_id"]).Data.Add(
                    (short)data["time_num"], (double)data["value"]
                );
            return climateList;
        }

        public void Insert(int clm0Id, Dictionary<short, double> data)
        {
            string sql = "Insert into data.climate_1 (climate_0_id, time_num, value) " +
                        "values (:climate_0_id, :time_num, :value)";
            var fieldsList = data.Select(dataField => new Dictionary<string, object>()
                {
                    {"climate_0_id", clm0Id},
                    {"time_num", dataField.Key},
                    {"value", dataField.Value}
                }).ToList();
            ExecSimpleQuery(sql, fieldsList);
        }
    }
}
