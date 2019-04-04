using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using SOV.Common;
using Npgsql;

namespace SOV.Amur.Meta
{
    /// <summary>
    /// Репозитории для импорта
    /// </summary>
    public partial class VariableRepository 
    {
        /// <summary>
        /// Если нет id - создать.
        /// Если есть id - изменить.
        /// </summary>
        public void InsertWithId(Variable item)
        {
            if (Select(item.Id) != null)
            {
                Update(item);
            }
            else
                using (NpgsqlConnection cnn = _db.Connection)
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("insert into meta.variable" +
                        " (id, variable_type_id, time_id, unit_id, data_type_id, value_type_id, general_category_id, sample_medium_id, time_support, name)"
                        + " values (:id, :variable_type_id, :time_id, :unit_id, :data_type_id, :value_type_id, :general_category_id, :sample_medium_id, :time_support, :name)", cnn))
                    {
                        cmd.Parameters.AddWithValue(":id", item.Id);
                        cmd.Parameters.AddWithValue(":variable_type_id", item.VariableTypeId);
                        cmd.Parameters.AddWithValue(":time_id", item.TimeId);
                        cmd.Parameters.AddWithValue(":unit_id", item.UnitId);
                        cmd.Parameters.AddWithValue(":data_type_id", item.DataTypeId);
                        cmd.Parameters.AddWithValue(":value_type_id", item.ValueTypeId);
                        cmd.Parameters.AddWithValue(":general_category_id", item.GeneralCategoryId);
                        cmd.Parameters.AddWithValue(":sample_medium_id", item.SampleMediumId);
                        cmd.Parameters.AddWithValue(":time_support", item.TimeSupport);
                        cmd.Parameters.AddWithValue(":name", item.NameRus);

                        cmd.ExecuteNonQuery();
                    }
                }
        }
    }
}
