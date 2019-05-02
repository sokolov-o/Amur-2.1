using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FERHRI.Common;

namespace FERHRI.Amur.Report
{
    using OrgWithStaff = Dictionary<Organisation, List<KeyValuePair<Person, PersonPosition>>>;

    public class OrganisationRepository : BaseRepository
    {
        internal OrganisationRepository(Common.ADbNpgsql db) : base(db, "report.organisation") { }

        public Organisation Select(int id)
        {
            List<Organisation> res = Select(new List<int>(new int[] { id }));
            return res.Count == 0 ? null : res[0];
        }

        public OrgWithStaff SelectWithStaff(List<int> idList = null)
        {
            OrgWithStaff res = new OrgWithStaff();
            Dictionary<string, object> fields = new Dictionary<string, object>() { { "id", idList } };
            string sql = "SELECT o.*, p.id as person_id, p.name as person, pp.id as pos_id, pp.name as pos " +
                            "FROM report.organisation as o " +
                            "right join report.staff as s on o.id = s.org_id " +
                            "join report.person as p on p.id = s.person_id " +
                            "join report.person_position as pp on pp.id = s.person_pos_id " +
                            "where (:id is null or o.id = ANY(:id))" +
                            "order by o.id";
            int lastOrgId = 0;
            List<KeyValuePair<Person, PersonPosition>> staff = new List<KeyValuePair<Person, PersonPosition>>();
            Organisation org = null;
            foreach (Dictionary<string, object> data in ExecQuery(sql, fields))
            {
                Person person = new Person((int)data["person_id"], data["person"].ToString());
                PersonPosition pos = new PersonPosition((int)data["pos_id"], data["pos"].ToString());
                staff.Add(new KeyValuePair<Person, PersonPosition>(person, pos));
                org = new Organisation(data);
                if (lastOrgId != 0 && lastOrgId != (int)data["id"])
                    res.Add(org, staff);
                lastOrgId = org.id;
            }
            if (org != null)
                res.Add(org, staff);
            return res;
        }

        public List<Organisation> Select(List<int> idList = null)
        {
            List<Organisation> res = new List<Organisation>();
            Dictionary<string, object> fields = new Dictionary<string, object>() { { "id", idList } };

            foreach (Dictionary<string, object> data in base.Select(fields))
                res.Add(new Organisation(data));
            return res;
        }
    }
}
