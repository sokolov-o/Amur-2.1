using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Common;
using Npgsql;

namespace SOV.Amur.Meta
{
    public class VariableVirtualRepository : BaseRepository<VariableVirtual>
    {
        internal VariableVirtualRepository(Common.ADbNpgsql db) : base(db, "meta.variable_virtual_view") { }

        public static List<VariableVirtual> GetCash()
        {
            return GetCash(DataManager.GetInstance().VariableVirtualRepository);
        }

        /// <summary>
        /// Функция парсинга строки результата запроса. Используется по умолчанию в ExecQuery.
        /// </summary>
        protected override object ParseData(NpgsqlDataReader reader)
        {
            return new VariableVirtual()
            {
                Id = (int)reader["id"],
                VariableTypeId = (int)reader["variable_type_id"],
                TimeId = (int)reader["time_id"],
                UnitId = (int)reader["unit_id"],
                DataTypeId = (int)reader["data_type_id"],
                GeneralCategoryId = (int)reader["general_category_id"],
                SampleMediumId = (int)reader["sample_medium_id"],
                TimeSupport = (int)reader["time_support"]
            };
        }
        public virtual List<VariableVirtual> Select(List<int> variableTypeId, List<int> timeId, List<int> unitId, List<int> dataTypeId,
            List<int> generalCategoryId, List<int> sampleMediumId, List<int> timeSupport)
        {
            var fields = new Dictionary<string, object>()
            {
                {"variable_type_id", variableTypeId},
                {"time_id", timeId},
                {"unit_id", unitId},
                {"data_type_id", dataTypeId},
                {"general_category_id", generalCategoryId},
                {"sample_medium_id", sampleMediumId},
                {"time_support", timeSupport}
            };
            return Select(fields);
        }
    }
}
