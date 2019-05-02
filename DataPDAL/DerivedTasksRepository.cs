using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Common;
using Npgsql;

namespace SOV.Amur.DataP
{
    public class DerivedTasksRepository
    {
        Common.ADbNpgsql _db;
        internal DerivedTasksRepository(Common.ADbNpgsql db)
        {
            _db = db;
        }
        public List<DerivedTask> SelectDerivedTasks(List<int> tasksId, int? unitTimeId, bool? is_fcs_data_type)
        {
            List<DerivedTask> ret = new List<DerivedTask>();
            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select * from datap.fn_derived_tasks_sel(:tasksId,:unitTimeId,:isFcs)", cnn))
                {


                    cmd.Parameters.Add(ADbNpgsql.GetParameter("tasksId", tasksId));
                    cmd.Parameters.Add(ADbNpgsql.GetParameter("unitTimeId", unitTimeId));
                    cmd.Parameters.Add(ADbNpgsql.GetParameter("isFcs", is_fcs_data_type));

                    //cmd.Parameters.AddWithValue("tasksId", tasksId);
                    //cmd.Parameters.AddWithValue("unitTimeId", unitTimeId);
                    //cmd.Parameters.AddWithValue("isFcs", is_fcs_data_type);
                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {

                        while (rdr.Read())
                        {
                            ret.Add(new DerivedTask(
                                (int)rdr["id"],
                                (int)rdr["method_dst_id"],
                                (int)rdr["station_type_id"],
                                (int)rdr["site_type_id"],
                                (int[])rdr["variable_src_id"],
                                (int)rdr["unit_time_dst_id"],
                                (int)rdr["offset_type_src_id"],
                                (double)rdr["offset_value_src"],
                                (int)rdr["method_src"],
                                (int)rdr["source_src"],
                                (int)rdr["time_support_dst"],
                                (int[])rdr["data_type_id_dst"],
                                rdr["params"].ToString(),
                                (bool)rdr["is_schedule"],
                                (bool)rdr["is_fcs_data"]
                                ));
                        }
                        return ret;
                    }
                }
            }

        }

        public List<DerivedTask> SelectDerivedTasks(int unitTimeId, bool isFcsDataType)
        {
            return SelectDerivedTasks(null, unitTimeId, isFcsDataType);
        }
        public DerivedTask SelectDerivedTask(int derivedTaskId)
        {
            return SelectDerivedTasks(new List<int>() { derivedTaskId }, null, null)[0];
        }



        public List<DerivedTask> SelectDerivedTasks(int unitTimeId, bool? isFcsDataType)
        {
            return SelectDerivedTasks(null, unitTimeId, isFcsDataType);
        }
    }
}
