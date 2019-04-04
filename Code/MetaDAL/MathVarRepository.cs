using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Common;
using Npgsql;

namespace SOV.Amur.Meta
{
    public class MathVarRepository : BaseRepository<MathVar>
    {
        internal MathVarRepository(Common.ADbNpgsql db) : base(db, "meta.mathvar") { }

        public static List<MathVar> GetCash()
        {
            return GetCash(DataManager.GetInstance().MathVarRepository);
        }

        public Dictionary<string, object> GetFieldDictionaryes(MathVar item, bool withId)
        {
            Dictionary<string, object> ret = IdNameRE.GetFieldDictionary(item, withId);
            ret.Add("name_short", item.NameShort);
            return ret;
        }
        public int Insert(MathVar item)
        {
            return InsertWithReturn(GetFieldDictionaryes(item, false));
        }
        public void Update(MathVar item)
        {
            Update(GetFieldDictionaryes(item, true));
        }

        protected override object ParseData(NpgsqlDataReader rdr)
        {
            IdNameRE item = (IdNameRE)IdNameRE.ParseData(rdr);
            return new MathVar()
            {
                Id = item.Id,
                NameRus = item.NameRus,
                NameEng = item.NameEng,
                NameShort = rdr["name_short"].ToString()
            };
        }
    }
}
