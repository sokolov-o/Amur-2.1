using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Common;
using Npgsql;

namespace SOV.Social
{
    public class StaffRepository : BaseRepository<Staff>
    {
        internal StaffRepository(Common.ADbNpgsql db) : base(db, "social.staff") { }

        protected override object ParseData(NpgsqlDataReader reader)
        {
            DateSF dateSF = DateSF.ParseData(reader);
            return new Staff()
            {
                Id = (int)reader["id"],
                Division = new Division() { Id = (int)reader["division_id"] },
                StaffPosition = new StaffPosition() { Id = (int)reader["staff_position_id"] },
                DateS = dateSF.DateS,
                DateF = dateSF.DateF
            };
        }
        public List<Staff> SelectByEmployer(int employerId, DateTime? dateActual)
        {
            List<Division> divisions = DataManager.GetInstance().DivisionRepository.Select(employerId, dateActual);
            if (divisions.Count == 0)
                return new List<Staff>();

            Dictionary<string, object> fields = new Dictionary<string, object>()
                {
                    { "division_id", divisions.Select(x=>x.Id).ToList()},
                    { "date_actual", dateActual}
                };
            List<Staff> ret = ExecQuery<Staff>(DateSF.GetSelectSql(TableName, fields), fields, ParseData);
            return UpdateFK(ret, divisions);
        }
        List<Staff> UpdateFK(List<Staff> ret, List<Division> divisions)
        {
            divisions = divisions ?? DataManager.GetInstance().DivisionRepository.Select(ret.Select(x => x.Division.Id).ToList());
            ret.ForEach(x => x.Division = divisions.Find(y => y.Id == x.Division.Id));

            List<StaffPosition> staffs = DataManager.GetInstance().StaffPositionRepository.Select(ret.Select(x => x.StaffPosition.Id).ToList());
            ret.ForEach(x => x.StaffPosition = staffs.Find(y => y.Id == x.StaffPosition.Id));

            return ret;
        }

        public int Insert(Staff staff)
        {
            return InsertWithReturn(GetFieldDictionary(staff, false));
        }
        public void Update(Staff staff)
        {
            Update(GetFieldDictionary(staff, true));
        }
        static public Dictionary<string, object> GetFieldDictionary(Staff item, bool withId)
        {
            Dictionary<string, object> ret = new Dictionary<string, object>()
            {
                { "division_id", item.Division.Id },
                { "staff_position_id", item.StaffPosition.Id}
            };
            ret.Union(DateSF.GetFieldDictionary(item));

            if (withId)
                ret.Add("id", item.Id);

            return ret;
        }
    }
}
