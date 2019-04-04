using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FERHRI.Common;
using Npgsql;

namespace FERHRI.Amur.Report
{
    public class ReportRepository : BaseRepository<Report>
    {
        internal ReportRepository(Common.ADbNpgsql db) : base(db, "report.report") { }

        protected override object ParseData(NpgsqlDataReader reader)
        {
            return new Report(
                (int)reader["id"],
                reader["name"].ToString(),
                reader["description"].ToString()
            );
        }
    }
}
