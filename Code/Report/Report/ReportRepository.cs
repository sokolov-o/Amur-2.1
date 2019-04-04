using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FERHRI.Common;
using Npgsql;

namespace FERHRI.Amur.Report
{
    public class ReportRepository : ADbNpgsql
    {
        ReportRepository()
            : base(Repository.ConnectionString)
        {
        }
        static public ReportRepository Instance { get { return new ReportRepository(); } }

        public Report Select(int reportId)
        {
            List<Report> ret = Select(new List<int>(new int[] { reportId }));
            return ret.Count == 0 ? null : ret[0];
        }

        public List<Report> Select(List<int> reportIdList = null)
        {
            List<Report> ret = new List<Report>();

            using (NpgsqlConnection cnn = Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select * from report.report"
                + " where (:reportId is null or id = ANY(:reportId))", cnn))
                {
                    cmd.Parameters.AddWithValue("reportId", reportIdList);
                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ret.Add(new Report(
                                (int)rdr["id"],
                                rdr["name"].ToString(),
                                rdr["description"].ToString()
                            ));
                        }
                        return ret;
                    }
                }
            }
        }
    }
}
