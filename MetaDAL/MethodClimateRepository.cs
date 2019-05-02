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
    public class MethodClimateRepository : BaseRepository<MethodClimate>
    {
        internal MethodClimateRepository(Common.ADbNpgsql db) : base(db, "meta.method_climate") { }

        public List<MethodClimate> Select(int yearS, int yearF)
        {
            return base.SelectAllFields(new Dictionary<string, object>()
            {
                {"year_s", yearS},
                { "year_f", yearF}
            });
        }
        override public MethodClimate Select(int methodId)
        {
            List<MethodClimate> ret = Select(new List<int>(new int[] { methodId }));
            return ret.Count == 1 ? ret[0] : null;
        }

        override public List<MethodClimate> Select(List<int> methodIds)
        {
            List<MethodClimate> ret = new List<MethodClimate>();

            // READ DB
            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select * from meta.method_climate where :method_id is null or method_id = ANY(:method_id)", cnn))
                {
                    cmd.Parameters.AddWithValue("method_id", methodIds);
                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ret.Add((MethodClimate)ParseData(rdr));
                        }
                    }
                }
            }
            return ret;
        }
        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return new MethodClimate() { MethodId = (int)rdr["method_id"], YearS = (int)rdr["year_s"], YearF = (int)rdr["year_f"] };
        }
        public void InsertOrUpdate(MethodClimate item)
        {
            if (Select(item.MethodId) == null)
                Insert(item);
            else
                Update(item);
        }
        void Insert(MethodClimate item)
        {
            var fields = new Dictionary<string, object>()
            {
                {"method_id", item.MethodId},
                {"year_s", item.YearS},
                {"year_f", item.YearF}
            };
            base.Insert(fields);
        }
        void Update(MethodClimate item)
        {
            var fields = new Dictionary<string, object>()
            {
                {"method_id", item.MethodId},
                {"year_s", item.YearS},
                {"year_f", item.YearF}
            };
            ExecSimpleQuery("update meta.method_climate set year_s = :year_s, year_f = :year_f where method_id = :method_id", fields);
        }
        public override void Insert(Dictionary<string, object> fields) { }
        public override int InsertWithReturn(Dictionary<string, object> fields) { return -1; }
        public override void Update(Dictionary<string, object> fields) { }
        public override void Delete(Dictionary<string, object> fields) { }
        public override void Delete(MethodClimate obj) { }
        public override void Delete(List<int> methodIds)
        {
            var fields = new Dictionary<string, object>()
            {
                {"method_id", methodIds}
            };
            ExecSimpleQuery("delete meta.method_climate where method_id = any(:method_id)", fields);
        }
        public override void Delete(int method_id)
        {
            Delete(new List<int>() { method_id });
        }
    }
}