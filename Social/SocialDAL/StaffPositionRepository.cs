using System.Collections.Generic;
using SOV.Common;
using Npgsql;

namespace SOV.Social
{
    public class StaffPositionRepository : BaseRepository<StaffPosition>
    {
        internal StaffPositionRepository(Common.ADbNpgsql db) : base(db, "social.staff_position") { }

        protected override object ParseData(NpgsqlDataReader reader)
        {
            return new StaffPosition(DataManager.ParseIdNames(reader));
        }

        public List<StaffPosition> Select(string nameRusShortLike, string nameEngShortLike = null, string nameRusLike = null, string nameEngLike = null)
        {
            var fields = new Dictionary<string, object>
            {
                {"name_rus", nameRusLike},
                {"name_eng", nameEngLike},
                {"name_rus_short", nameRusShortLike},
                {"name_eng_short", nameEngShortLike}
            };
            return Select(fields, QueryOper.LIKE);
        }

        public override void Update(Dictionary<string, object> fields)
        {
            fields["id"] = int.Parse((string)fields["id"]);
            Update(new List<Dictionary<string, object>>() { fields }, new List<string>() { "id" });
        }
        public int Insert(StaffPosition item)
        {
            return InsertWithReturn(GetFieldDictionary(item, false));
        }
        public void Update(StaffPosition item)
        {
            Update(GetFieldDictionary(item, true));
        }
        static public Dictionary<string, object> GetFieldDictionary(StaffPosition item, bool withId)
        {
            return IdNames.GetFieldDictionary(item, withId);
        }
    }
}
