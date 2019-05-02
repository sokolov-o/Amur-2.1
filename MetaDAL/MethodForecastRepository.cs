using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using SOV.Common;
using Npgsql;

namespace SOV.Amur.Meta
{
    public class MethodForecastRepository : BaseRepository<MethodForecast>
    {
        internal MethodForecastRepository(Common.ADbNpgsql db) : base(db, "meta.method_forecast") { }

        override public MethodForecast Select(int methodId)
        {
            List<MethodForecast> ret = Select(new List<int>(new int[] { methodId }));
            return ret.Count == 1 ? ret[0] : null;
        }
        override public List<MethodForecast> Select(List<int> methodIds = null)
        {
            List<MethodForecast> ret = new List<MethodForecast>();

            // READ DB
            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select * from meta.method_forecast where :method_id is null or method_id = ANY(:method_id)", cnn))
                {
                    cmd.Parameters.AddWithValue("method_id", methodIds);
                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ret.Add((MethodForecast)ParseData(rdr));
                        }
                    }
                }
            }

            // UPDATE METHOD
            List<Method> methods = DataManager.GetInstance().MethodRepository.Select(ret.Select(x => x.Method.Id).Distinct().ToList());
            ret.ForEach(x => x.Method = methods.First(y => y.Id == x.Method.Id));

            // RETURN
            return ret;
        }

        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return new MethodForecast(
                new Method((int)rdr["method_id"], "UNKNOW"),
                rdr.IsDBNull(rdr.GetOrdinal("lags")) ? null : rdr["lags"].ToString(),
                (int)rdr["time_id_fcs_lag"],
                rdr.IsDBNull(rdr.GetOrdinal("date_ini_hours_utc")) ? null : rdr["date_ini_hours_utc"].ToString(),
                rdr.IsDBNull(rdr.GetOrdinal("attr")) ? null : Common.StrVia.ToDictionary(rdr["attr"].ToString())
            );
        }
        public void InsertOrUpdate(MethodForecast item)
        {
            if (Select(item.Method.Id) == null)
                Insert(item);
            else
                Update(item);
        }

        // TODO: Убрать is_date_fcs_utc & max_count_in_day
        void Insert(MethodForecast item)
        {
            var fields = new Dictionary<string, object>()
            {
                {"method_id", item.Method.Id},
                {"time_id_fcs_lag", item.LeadTimeUnitId},
                {"is_date_fcs_utc", true},
                {"max_count_in_day", item.MaxPerDayCount},
                {"lags", string.Join(";", item.LeadTimes)},
                {"date_ini_hours_utc", string.Join(";", item.DateIniHoursUTC)},
                {"attr", StrVia.ToString(item.Attr)}
            };
            Insert(fields);
        }
        void Update(MethodForecast item)
        {
            ExecSimpleQuery(
                "update meta.method_forecast set time_id_fcs_lag=:time_id_fcs_lag, is_date_fcs_utc=:is_date_fcs_utc," +
                "max_count_in_day=:max_count_in_day, lags=:lags,date_ini_hours_utc=:date_ini_hours_utc, attr=:attr" +
                " where method_id = :method_id"
                ,
                new Dictionary<string, object>()
                {
                    {"method_id", item.Method.Id},
                    {"time_id_fcs_lag", item.LeadTimeUnitId},
                    {"is_date_fcs_utc", true},
                    {"max_count_in_day", item.MaxPerDayCount},
                    {"lags", string.Join(";", item.LeadTimes)},
                    {"date_ini_hours_utc", string.Join(";", item.DateIniHoursUTC)},
                    {"attr", StrVia.ToString(item.Attr)}
                }
            );
        }
        //public override void Insert(Dictionary<string, object> fields) { }
        public override int InsertWithReturn(Dictionary<string, object> fields) { return -1; }
        //public override void Update(Dictionary<string, object> fields) { }
        public override void Delete(Dictionary<string, object> fields) { }
        public override void Delete(MethodForecast mf)
        {
            Delete(new List<int>() { mf.Method.Id });
        }
        public override void Delete(List<int> methodIds)
        {
            Delete(new List<Dictionary<string, object>>() { new Dictionary<string, object>() { { "method_id", methodIds } } });
        }
        public override void Delete(int method_id)
        {
            Delete(new List<int>() { method_id });
        }
    }
}