using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Common;
using Npgsql;

namespace SOV.Amur.Sys
{
    public class LogRepository : BaseRepository<Log>
    {
        internal LogRepository(Common.ADbNpgsql db) : base(db, "sys.log") { }
        
        public void Delete(DateTime dateS, DateTime dateF, int entityId)
        {
            string sql = "delete from sys.log where entity_id = :entity_id and date between :date_s and :date_f";
            var fields = new Dictionary<string, object>();
            fields.Add("entity_id", entityId);
            fields.Add("date_s", dateS);
            fields.Add("date_f", dateF);
            ExecSimpleQuery(sql + " and parent_id is not null", fields);
            ExecSimpleQuery(sql + " and parent_id is null", fields);
            return;
        }

        public int Insert(int entityId, string message, int? parentId = null, bool? isUrgent = false)
        {
            var fields = new Dictionary<string, object>()
            {
                {"entity_id", entityId},
                {"message", message},
                {"is_urgent", isUrgent},
                {"parent_id", parentId}
            };
            return InsertWithReturn(fields);
        }

        private List<Entity> selectLogtmpEntityes;

        override protected object ParseData(NpgsqlDataReader rdr)
        {
            return new Log(
                (int)rdr["id"],
                selectLogtmpEntityes.Find(x => x.Id == (int)rdr["entity_id"]),
                (DateTime)rdr["date"], (bool)rdr["is_urgent"], rdr["message"].ToString(),
                rdr.IsDBNull(rdr.GetOrdinal("parent_id")) ? null : (int?)(int)rdr["parent_id"]
            );
        }

        public List<Log> Select(DateTime dateS, DateTime dateF, int? entityId = null, bool isUrgentOnly = false)
        {
            var fields = new Dictionary<string, object>() { { "entity_id", entityId } };
            selectLogtmpEntityes = Sys.DataManager.GetInstance().SysEntityRepository.SelectEntity();
            string whereEntity = QueryBuilder.Where(fields);
            string sql = string.Format("Select * From {0} {1} ", TableName, (whereEntity == "" ? "Where " : whereEntity + " and "))
                        + " (date between :date_s and :date_f) "
                        + " and (:is_urgent_only is false or (:is_urgent_only and is_urgent))";
            fields.Add("date_s", dateS);
            fields.Add("date_f", dateF);
            fields.Add("is_urgent_only", isUrgentOnly);
            return ExecQuery<Log>(sql, fields);
        }
    }
}
