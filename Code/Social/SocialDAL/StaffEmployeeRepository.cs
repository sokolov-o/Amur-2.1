using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Common;
using Npgsql;

namespace SOV.Social
{
    public class StaffEmployeeRepository : BaseRepository<StaffEmployee>
    {
        internal StaffEmployeeRepository(Common.ADbNpgsql db) : base(db, "social.staff_employee") { }

        protected override object ParseData(NpgsqlDataReader rdr)
        {
            DateSF dateSF = DateSF.ParseData(rdr);
            return new StaffEmployee()
            {
                Id = (int)rdr["id"],
                EmployeeId = (int)rdr["employee_id"],
                StaffId = (int)rdr["staff_id"],
                Percent = (double)rdr["percent"],
                DateS = dateSF.DateS,
                DateF = dateSF.DateF
            };
        }
        public List<StaffEmployee> Select(List<int> staffIds, List<int> employeeIds, DateTime? dateActual)
        {
            Dictionary<string, object> fields = new Dictionary<string, object>()
            {
                { "staff_id", staffIds},
                { "employee_id", employeeIds },
                { "date_actual",dateActual }
            };
            return ExecQuery<StaffEmployee>(DateSF.GetSelectSql(TableName, fields), fields, ParseData);
        }
        public int Insert(StaffEmployee item)
        {
            return InsertWithReturn(GetFieldDictionary(item, false));
        }
        public void Update(StaffEmployee item)
        {
            Update(GetFieldDictionary(item, true));
        }

        static public Dictionary<string, object> GetFieldDictionary(StaffEmployee item, bool withId)
        {
            Dictionary<string, object> ret = new Dictionary<string, object>()
            {
                {"staff_id", item.StaffId},
                {"employee_id", item.EmployeeId},
                {"percent", item.Percent},
                {"date_s", item.DateS},
                {"date_f", item.DateF}
            };
            if (withId) ret.Add("id", item.Id);
            return ret;
        }
    }
}
