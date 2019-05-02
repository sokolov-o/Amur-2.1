using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Common;
using Npgsql;

namespace SOV.Amur.Meta
{
    public class SiteInstrumentRepository : BaseRepository<Instrument>
    {
        internal SiteInstrumentRepository(Common.ADbNpgsql db) : base(db, "meta.site_instrument") { }

        public static List<Instrument> GetCash()
        {
            return GetCash(DataManager.GetInstance().SiteInstrumentRepository);
        }

        public Dictionary<string, object> GetFieldDictionaryes(SiteInstrument item, bool withId)
        {
            Dictionary<string, object> ret = DateSF.GetFieldDictionary(item);

            ret.Add("site_id", item.SiteId);
            ret.Add("instrument_id", item.InstrumentId);
            ret.Add("location_description", item.LocationDescription);
            ret.Add("description", item.Description);

            if (withId) ret.Add("id", item.Id);

            return ret;
        }
        public int Insert(SiteInstrument item)
        {
            return InsertWithReturn(GetFieldDictionaryes(item, false));
        }
        public void Update(SiteInstrument item)
        {
            Update(GetFieldDictionaryes(item, true));
        }
        public List<SiteInstrument> SelectBySiteIds(List<int> siteIds)
        {
            return ExecQuery<SiteInstrument>(
                "select * from " + TableName + " where site_id = any(:site_id)",
                new Dictionary<string, object>() { { "site_id", siteIds } },
                ParseData);

        }
        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return new SiteInstrument(DateSF.ParseData(rdr))
            {
                Id = (int)rdr["id"],
                SiteId = (int)rdr["site_id"],
                InstrumentId = (int)rdr["instrument_id"],
                LocationDescription = rdr["location_description"].ToString(),
                Description = rdr["description"].ToString()
            };
        }

    }
}
