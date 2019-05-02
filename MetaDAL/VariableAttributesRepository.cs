using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Common;
using Npgsql;

namespace SOV.Amur.Meta
{
    public class VariableAttributesRepository
    {
        Common.ADbNpgsql _db;
        internal VariableAttributesRepository(Common.ADbNpgsql db)
        {
            _db = db;
        }

        public VariableAttributes Select(int variableId)
        {
            List<VariableAttributes> ret = Select(new List<int>(new int[] { variableId }));
            return ret.Count == 0 ? null : ret[0];
        }
        public List<VariableAttributes> Select(List<int> variableIds = null)
        {
            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select * from meta.variable_attr where :variable_id is null or variable_id = ANY (:variable_id)", cnn))
                {
                    cmd.Parameters.AddWithValue("variable_id", variableIds);
                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        List<VariableAttributes> ret = new List<VariableAttributes>();
                        while (rdr.Read())
                        {
                            ret.Add(new VariableAttributes()
                            {
                                VariableId = (int)rdr["variable_id"],
                                AxisMin = (rdr.IsDBNull(rdr.GetOrdinal("axis_min"))) ? double.NaN : (double)rdr["axis_min"],
                                AxisMax = (rdr.IsDBNull(rdr.GetOrdinal("axis_max"))) ? double.NaN : (double)rdr["axis_max"],
                                AxisStep = (rdr.IsDBNull(rdr.GetOrdinal("axis_step"))) ? double.NaN : (double)rdr["axis_step"],
                                ValueFormat = (rdr.IsDBNull(rdr.GetOrdinal("value_format"))) ? null : rdr["value_format"].ToString()
                            });
                        }
                        return ret;
                    }
                }
            }
        }
        /// <summary>
        /// Update or insert if not exists
        /// </summary>
        public void Update(VariableAttributes varAttr)
        {
            if (Select(varAttr.VariableId) == null)
            {
                Insert(varAttr);
                return;
            }
            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    "update meta.variable_attr"
                    + " set axis_min=:axis_min, axis_max=:axis_max, axis_step=:axis_step, value_format=:value_format"
                    + " where variable_id = :variable_id", cnn))
                {
                    cmd.Parameters.AddWithValue("variable_id", varAttr.VariableId);
                    cmd.Parameters.AddWithValue("axis_min", double.IsNaN(varAttr.AxisMin) ? null : (double?)varAttr.AxisMin);
                    cmd.Parameters.AddWithValue("axis_max", double.IsNaN(varAttr.AxisMax) ? null : (double?)varAttr.AxisMax);
                    cmd.Parameters.AddWithValue("axis_step", double.IsNaN(varAttr.AxisStep) ? null : (double?)varAttr.AxisStep);
                    cmd.Parameters.AddWithValue("value_format", varAttr.ValueFormat);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        private void Insert(VariableAttributes varAttr)
        {
            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    "insert into meta.variable_attr"
                    + " (variable_id, axis_min, axis_max, axis_step, value_format)"
                    + " values (:variable_id, :axis_min, :axis_max, :axis_step, :value_format)", cnn))
                {
                    cmd.Parameters.AddWithValue("variable_id", varAttr.VariableId);
                    cmd.Parameters.AddWithValue("axis_min", double.IsNaN(varAttr.AxisMin) ? null : (double?)varAttr.AxisMin);
                    cmd.Parameters.AddWithValue("axis_max", double.IsNaN(varAttr.AxisMax) ? null : (double?)varAttr.AxisMax);
                    cmd.Parameters.AddWithValue("axis_step", double.IsNaN(varAttr.AxisStep) ? null : (double?)varAttr.AxisStep);
                    cmd.Parameters.AddWithValue("value_format", varAttr.ValueFormat);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
