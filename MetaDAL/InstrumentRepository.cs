using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Common;
using Npgsql;

namespace SOV.Amur.Meta
{
    public class InstrumentRepository : BaseRepository<Instrument>
    {
        internal InstrumentRepository(Common.ADbNpgsql db) : base(db, "meta.instrument") { }

        public static List<Instrument> GetCash()
        {
            return GetCash(DataManager.GetInstance().InstrumentRepository);
        }

        public Dictionary<string, object> GetFieldDictionaryes(Instrument item, bool withId)
        {
            Dictionary<string, object> ret = IdNameRE.GetFieldDictionary(item, withId);
            ret.Add("serial_num", item.SerialNum);
            ret.Add("parent_id", item.ParentId);
            return ret;
        }
        public int Insert(Instrument item)
        {
            return InsertWithReturn(GetFieldDictionaryes(item, false));
        }
        public void Update(Instrument item)
        {
            Update(GetFieldDictionaryes(item, true));
        }

        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return new Instrument((IdNameRE)IdNameRE.ParseData(rdr))
            {
                SerialNum = rdr["serial_num"].ToString(),
                ParentId = ADbNpgsql.GetValueInt(rdr, "parent_id")
            };
        }
    }
}
