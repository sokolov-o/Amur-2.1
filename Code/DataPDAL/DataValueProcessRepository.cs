using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace SOV.Amur.DataP
{
    public class DataValueProcessRepository
    {
        Common.ADbNpgsql _db;
        internal DataValueProcessRepository(Common.ADbNpgsql db)
        {
            _db = db;
        }
        public List<DataGrpSVYM> SelectDataGrpSVYM(List<int> siteIds, List<int> variableIds, int yearSUTC, int yearFUTC, List<int> monthes)
        {
            List<DataGrpSVYM> ret = new List<DataGrpSVYM>();
            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand("data.select_data_value_grp_svym", cnn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandTimeout = 5 * 60;

                    cmd.Parameters.AddWithValue("_site_ids", siteIds);
                    cmd.Parameters.Add(Common.ADbNpgsql.GetParameter("_var_ids", variableIds));
                    cmd.Parameters.AddWithValue("_year_s_utc", yearSUTC);
                    cmd.Parameters.AddWithValue("_year_f_utc", yearFUTC);
                    cmd.Parameters.AddWithValue("_monthes", monthes);

                    using (Npgsql.NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            DataGrpSVYM d = new DataGrpSVYM()
                            {
                                SiteId = (int)rdr["site_id"],
                                VariableId = (int)rdr["variable_id"],
                                Year = (int)rdr["year"],
                                Month = (int)rdr["month"],
                                Count = (int)rdr["count"],

                                MinDateTimeUTC = (DateTime)rdr["min_date_utc"],
                                MaxDateTimeUTC = (DateTime)rdr["max_date_utc"],

                                AvgValue = (double)rdr["avg_value"],
                                SumValue = (double)rdr["sum_value"],
                                MinValue = (double)rdr["min_value"],
                                MaxValue = (double)rdr["max_value"]
                            };
                            ret.Add(d);
                        }
                        return ret;
                    }
                }
            }
        }
    }
}
