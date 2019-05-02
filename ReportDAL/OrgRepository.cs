using System.Collections.Generic;
using SOV.Common;
using SOV.Social;
using Npgsql;

namespace SOV.Amur.Reports
{
    public class OrgRepository : BaseRepository<Org>
    {
        internal OrgRepository(Common.ADbNpgsql db) : base(db, "report.org") { }

        protected override object ParseData(NpgsqlDataReader reader)
        {
            return new Org
            (
                (int)reader["report_id"],
                (int)reader["org_id"],
                ADbNpgsql.GetValueString(reader, "param"),
                ADbNpgsql.GetValueInt(reader, "img_id")
            );
        }

        public override void Delete(Org org)
        {
            Delete(new Dictionary<string, object> { { "org_id", org.OrgId }, { "report_id", org.ReportId } });
        }

        public override void Delete(Dictionary<string, object> fields)
        {
            fields = new Dictionary<string, object> { { "org_id", fields["org_id"] }, { "report_id", fields["report_id"] } };
            Delete(new List<Dictionary<string, object>>() { fields });
        }

        public override void Update(Dictionary<string, object> fields)
        {
            Update(new List<Dictionary<string, object>>() { fields }, new List<string>() { "org_id", "report_id" });
        }

        public Org Select(int reportId, int orgId)
        {
            var fields = new Dictionary<string, object>() { { "report_id", reportId }, { "org_id", orgId } };
            var res = Select(fields);
            return res != null ? res[0] : null;
        }

        public List<Org> SelectByReport(int reportId)
        {
            var fields = new Dictionary<string, object>() { { "report_id", reportId } };
            return Select(fields);
        }

        public override List<Org> SelectAllFields(Dictionary<string, object> fields)
        {
            string sql = "Select {0}.*, i.img as img_data, e.name_rus as org_view, r.name as report_view " +
                         " From {0} Left Join {1} as i on i.id = img_id " +
                         " Left Join {2} as e on e.id = org_id " +
                         " Left Join {3} as r on r.id = report_id ";
            string imgTable = Social.DataManager.GetInstance().ImageRepository.TableName;
            string reportTable = DataManager.GetInstance().ReportRepository.TableName;
            string entityTable = Social.DataManager.GetInstance().LegalEntityRepository.TableName;
            sql = string.Format(sql, TableName, imgTable, entityTable, reportTable);
            return ExecQuery<Org>(sql, new Dictionary<string, object>(), AllFieldsParse);
        }

        private object AllFieldsParse(NpgsqlDataReader reader)
        {
            var res = (Org)ParseData(reader);
            res.Img = ADbNpgsql.GetValueByteArr(reader, "img_data");
            res.OrgView = reader["org_view"].ToString();
            res.ReportView = reader["report_view"].ToString();
            return res;
        }
    }
}
