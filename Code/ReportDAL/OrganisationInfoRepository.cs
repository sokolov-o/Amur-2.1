using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using FERHRI.Common;
using FERHRI.Amur.Meta;
using Npgsql;

namespace FERHRI.Amur.Report
{
    using OrgWithStaff = Dictionary<OrganisationInfo, List<KeyValuePair<Person, PersonPosition>>>;

    public class OrganisationInfoRepository : BaseRepository<OrganisationInfo>
    {
        internal OrganisationInfoRepository(Common.ADbNpgsql db) : base(db, "report.organisation_info") { }

        /// <summary>
        /// Получить словарь полей таблицы для отображения в Grid. 
        /// Ключ - имя поля класса; Значение - имя/title для отображения.
        /// </summary>
        public override List<TableRowField> ViewTableFields()
        {
            var orgs = Social.DataManager.GetInstance().LegalEntityRepository.SelectAll().Select(x => new DicItem(x.Id, x.NameRus));
            return new List<TableRowField>()
            {
                new TableRowField("orgId", "Организация", TableRowFieldType.ComboBox, orgs.ToList()),
                new TableRowField("text", "Текст отчета"),
                new TableRowField("phone", "Текст отчета"),
                new TableRowField("imgId", "Изображение")
            };
        }

        public override OrganisationInfo Select(int orgId)
        {
            var res = Select(new List<int>(new int[] { orgId }));
            return res.Count == 0 ? null : res[0];
        }

        public override List<OrganisationInfo> Select(List<int> idList = null)
        {
            var fields = new Dictionary<string, object>() { { "org_id", idList } };
            string sql = string.Format("select * from {0} where {1}", tableName, QueryDesigner.JoinByAnd(fields));
            return ExecQuery<OrganisationInfo>(sql, fields, ParseOnlyInfo);
        }

        private object ParseOnlyInfo(NpgsqlDataReader reader)
        {
            return new OrganisationInfo(
                (int)reader["org_id"],
                TryField<string>(reader, "text"),
                TryField<string>(reader, "phone"),
                (int)reader["img_id"],
                null
            );
        }

        public OrgWithStaff SelectWithStaff(List<int> idList = null)
        {
            OrgWithStaff res = new OrgWithStaff();
            Dictionary<string, object> fields = new Dictionary<string, object>() { { "id", idList } };
            string sql = "SELECT source.*, o.*, p.id as person_id, p.name as person, pp.id as pos_id, pp.name as pos " +
                        "FROM report.organisation_info as o " +
                        "right join report.staff as s on o.org_id = s.org_id " +
                        "join report.person as p on p.id = s.person_id " +
                        "join report.person_position as pp on pp.id = s.person_pos_id " +
                        "join meta.source as source on o.org_id = source.id " +
                        "where (:id is null or o.org_id = ANY(:id)) " +
                        "order by source.name";
            int lastOrgId = 0;
            List<KeyValuePair<Person, PersonPosition>> staff = new List<KeyValuePair<Person, PersonPosition>>();
            OrganisationInfo org = null;
            foreach (List<object> data in ExecQuery<object>(sql, fields, ParseOrgWithStaff))
            {
                staff.Add(new KeyValuePair<Person, PersonPosition>((Person)data[0], (PersonPosition)data[1]));
                org = (OrganisationInfo)data[2];
                if (lastOrgId != 0 && lastOrgId != org.orgId)
                    res.Add(org, staff);
                lastOrgId = org.orgId;
            }
            if (org != null)
                res.Add(org, staff);
            return res;
        }

        private object ParseOrgWithStaff(NpgsqlDataReader reader)
        {
            List<object> res = new List<object>();
            res.Add(new Person((int)reader["person_id"], reader["person"].ToString()));
            res.Add(new PersonPosition((int)reader["pos_id"], reader["pos"].ToString()));
            res.Add(this.ParseData(reader));
            return res;
        }

        protected override object ParseData(NpgsqlDataReader reader)
        {
            throw new NotImplementedException("OSokolov@201707 commented");
            //return new OrganisationInfo(
            //    (int)reader["org_id"],
            //    TryField<string>(reader, "text"),
            //    TryField<string>(reader, "phone"),
            //    (int)reader["img_id"],
            //    new Source(
            //        (int)reader["id"],
            //        TryField<string>(reader, "name"),
            //        TryField<string>(reader, "name_short"),
            //        TryField<string>(reader, "description"),
            //        TryField<string>(reader, "name_eng"),
            //        TryField<string>(reader, "description_eng"),
            //        TryField<string>(reader, "address"),
            //        TryField<string>(reader, "address_eng"),
            //        TryField<string>(reader, "emails"),
            //        TryField<string>(reader, "web_site")
            //    )
            //);
        }
    }
}
