using System;
using System.Collections.Generic;
using System.Linq;
using SOV.Common;
using Npgsql;

namespace SOV.Amur.Meta
{
    public partial class VariableRepository : BaseRepository<Variable>
    {
        internal VariableRepository(Common.ADbNpgsql db) : base(db, "meta.variable") { }

        public static List<Variable> GetCash()
        {
            return GetCash(DataManager.GetInstance().VariableRepository);
        }

        //public string ToStringWithFKNames(Variable variable)
        //{
        //    return variable.Id
        //        + ";" + variable.NameRus
        //        + ";" + VariableTypeRepository.GetCash().Find(x => x.Id == variable.VariableTypeId).Name
        //        + ";" + UnitRepository.GetCash().Find(x => x.Id == variable.TimeId).Name
        //        + ";" + UnitRepository.GetCash().Find(x => x.Id == variable.UnitId) ?? variable.UnitId.ToString()
        //        + ";" + DataTypeRepository.GetCash().Find(x => x.Id == variable.DataTypeId).Name
        //        + ";" + ValueTypeRepository.GetCash().Find(x => x.Id == variable.ValueTypeId).Name
        //        + ";" + GeneralCategoryRepository.GetCash().Find(x => x.Id == variable.GeneralCategoryId).Name
        //        + ";" + SampleMediumRepository.GetCash().Find(x => x.Id == variable.SampleMediumId).Name
        //        + ";" + variable.TimeSupport
        //    ;
        //}

        /// <summary>
        /// Функция парсинга строки результата запроса. Используется по умолчанию в ExecQuery.
        /// </summary>
        protected override object ParseData(NpgsqlDataReader reader)
        {
            return new Variable(
                (int)reader["id"],
                (int)reader["variable_type_id"],
                (int)reader["time_id"],
                (int)reader["unit_id"],
                (int)reader["data_type_id"],
                (int)reader["value_type_id"],
                (int)reader["general_category_id"],
                (int)reader["sample_medium_id"],
                (int)reader["time_support"],
                reader["name"].ToString(),
                reader["name_eng"].ToString(),
                (double)reader["code_no_data"],
                (double)reader["code_err_data"]
            );
        }
        /// <summary>
        /// Создать источник.
        /// </summary>
        /// <param name="item">Source instance.</param>
        /// <returns></returns>
        public int Insert(Variable item)
        {
            var fields = new Dictionary<string, object>()
            {
                {"variable_type_id", item.VariableTypeId},
                {"time_id", item.TimeId},
                {"unit_id", item.UnitId},
                {"data_type_id", item.DataTypeId},
                {"value_type_id", item.ValueTypeId},
                {"general_category_id", item.GeneralCategoryId},
                {"sample_medium_id", item.SampleMediumId},
                {"time_support", item.TimeSupport},
                {"name", item.NameRus},
                {"name_eng", item.NameEng},
                {"code_no_data", item.CodeNoData},
                {"code_err_data", item.CodeErrData}
            };
            return InsertWithReturn(fields);
        }
        public void Update(Variable item)
        {
            var fields = new Dictionary<string, object>()
            {
                {"variable_type_id", item.VariableTypeId},
                {"time_id", item.TimeId},
                {"unit_id", item.UnitId},
                {"data_type_id", item.DataTypeId},
                {"value_type_id", item.ValueTypeId},
                {"general_category_id", item.GeneralCategoryId},
                {"sample_medium_id", item.SampleMediumId},
                {"time_support", item.TimeSupport},
                {"name", item.NameRus},
                {"name_eng", item.NameEng},
                {"id", item.Id},
                {"code_no_data", item.CodeNoData},
                {"code_err_data", item.CodeErrData}
            };
            Update(fields);
        }

        public Variable Select(int variableTypeId, int timeId, int unitId, int dataTypeId, int generalCategoryId,
                                int sampleMediumId, int timeSupport, int valueTypeId)
        {
            var vars = Select(new List<int>() { variableTypeId }, new List<int>() { timeId }, new List<int>() { unitId },
                new List<int>() { dataTypeId }, new List<int>() { generalCategoryId },
                new List<int>() { sampleMediumId }, new List<int>() { timeSupport }, new List<int>() { valueTypeId });
            if (vars.Count == 1)
                return vars[0];
            return null;
        }
        /// <summary>
        /// Выбрать переменные.
        /// </summary>
        /// <param name="variableTypeId"></param>
        /// <param name="timeId"></param>
        /// <param name="unitId"></param>
        /// <param name="dataTypeId"></param>
        /// <param name="generalCategoryId"></param>
        /// <param name="sampleMediumId"></param>
        /// <param name="timeSupport"></param>
        /// <param name="valueTypeId"></param>
        /// <returns></returns>
        public virtual List<Variable> Select(List<int> variableTypeId, List<int> timeId, List<int> unitId, List<int> dataTypeId,
            List<int> generalCategoryId, List<int> sampleMediumId, List<int> timeSupport, List<int> valueTypeId)
        {
            var fields = new Dictionary<string, object>()
            {
                {"variable_type_id", variableTypeId},
                {"time_id", timeId},
                {"unit_id", unitId},
                {"data_type_id", dataTypeId},
                {"general_category_id", generalCategoryId},
                {"sample_medium_id", sampleMediumId},
                {"time_support", timeSupport},
                {"value_type_id", valueTypeId}
            };
            return Select(fields);
        }
        public Variable GetVariableDerived(int srcVar, int? varUnitsId, int timeUnitsId, int dataTypeId, int timeSupport)
        {
            string sql = "datap.get_variable_derived";
            var fields = new Dictionary<string, object>()
            {
                {"srcVarId", srcVar},
                {"varUnitsId", varUnitsId},
                {"timeUnitsId", timeUnitsId},
                {"dataTypeId", dataTypeId},
                {"timeSupport", timeSupport}
            };
            var ret = ExecQuery<Variable>(sql, fields, ParseData, System.Data.CommandType.StoredProcedure);
            if (ret.Count == 0)
                return null;
            if (ret.Count > 1)
                throw new Exception("ERROR! CountRow>1!");
            return ret[0];
        }
        public virtual Dictionary<VariableVirtual, List<Variable>> SelectByVariableVirtualIds(List<int> variableVirtualIds)
        {
            return SelectByVariableVirtuals(DataManager.GetInstance().VariableVirtualRepository.Select(variableVirtualIds));
        }
        public virtual Dictionary<VariableVirtual, List<Variable>> SelectByVariableVirtuals(List<VariableVirtual> variableVirtuals)
        {
            Dictionary<VariableVirtual, List<Variable>> ret = new Dictionary<VariableVirtual, List<Variable>>();

            foreach (var item in variableVirtuals)
            {
                ret.Add(
                    item,
                    Select(
                        new List<int>() { item.VariableTypeId },
                        new List<int>() { item.TimeId },
                        new List<int>() { item.UnitId },
                        new List<int>() { item.DataTypeId },
                        new List<int>() { item.GeneralCategoryId },
                        new List<int>() { item.SampleMediumId },
                        new List<int>() { item.TimeSupport },
                        null
                    )
                );
            }
            return ret;
        }
    }
}
