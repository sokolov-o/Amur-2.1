using System;
using System.Collections.Generic;
using System.Linq;
using SOV.Common;
using Npgsql;

namespace SOV.Amur.Data
{
    public class DataSourceRepository : BaseRepository<DataSource>
    {
        internal DataSourceRepository(Common.ADbNpgsql db) : base(db, "data.data_source") {}
        /// <summary>
        /// Функция парсинга строки результата запроса. Используется по умолчанию в ExecQuery.
        /// </summary>
        protected override object ParseData(NpgsqlDataReader reader)
        {
            return new DataSource(
                (long)reader["id"],
                (int)reader["site_id"],
                (int)reader["code_form_id"],
                (DateTime)reader["date_utc"],
                (DateTime)reader["date_utc_recieve"],
                (DateTime)reader["date_loc_insert"],
                reader["value"].ToString()
            );
        }
        public DataSource SelectByDataValue(long dvId)
        {
            Dictionary<long, DataSource> ret = Select(new List<long>(new long[] { dvId }));
            return ret.Count == 0 ? null : ret[0];
        }

        public Dictionary<long, DataSource> Select(List<long> dvIds)
        {
            Dictionary<long, DataSource> ret = new Dictionary<long, DataSource>();
            if (dvIds.Count == 0)
                return ret;
            var fields = new Dictionary<string, object>() {{ "data_value_id", dvIds }};
            string sql = "Select dvds.data_value_id, ds.* from data.data_source ds " +
                         " Inner join data.datavalue_datasource dvds on dvds.data_source_id = ds.id " +
                         QueryBuilder.Where(fields);
            foreach (var data in ExecQuery<Dictionary<string, object>>(sql, fields, ParseDataWithDataValue))
                ret.Add((long)data["data_value_id"], (DataSource)data["data_source"]);
            return ret;
        }
        private object ParseDataWithDataValue(NpgsqlDataReader reader)
        {
            return new Dictionary<string, object>()
            {
                {"data_value_id", (long) reader["data_value_id"]},
                {"data_source", ParseData(reader)}
            };
        }

        public bool Exists(DateTime dateUTC, int siteId, int codeFormId, string hash)
        {
            string sql = "Exists(Select * from data.data_source " +
                         " Where date_utc = :date_utc and site_id = :site_id and " +
                         " code_form_id = :code_form_id and hash = :hash)";
            var fields = new Dictionary<string, object>()
            {
                { "date_utc", dateUTC },
                { "site_id", siteId },
                { "code_form_id", codeFormId },
            };
            return bool.Parse(ExecSimpleQuery(sql, fields, true));
        }

        public void Delete(long dataValueId)
        {
            string sql = "Delete from data.datavalue_datasource where data_value_id=:data_value_id";
            var fields = new Dictionary<string, object>() { {"data_value_id", dataValueId} };
            ExecSimpleQuery(sql, fields);
        }

        //internal void Insert(Dictionary<long, DataSource> ds)
        //{
        //    foreach (var item in ds)
        //    {
        //        InsertDataValueXSource(item.Key, item.Value.Id);
        //    }
        //}

        public void InsertDataValueXSource(long dataValueId, long dataSourceId)
        {
            string sql = "Insert into data.datavalue_datasource values(:data_value_id, :data_source_id)";
            var fields = new Dictionary<string, object>()
            {
                { "data_value_id", dataValueId },
                { "data_source_id", dataSourceId }
            };
            ExecSimpleQuery(sql, fields);
        }

        /// <summary>
        /// Записать значение.
        /// </summary>
        /// <param name="item">Значение для записи.</param>
        /// <returns></returns>
        public void InsertWithId(DataSource item)
        {
            var fields = new Dictionary<string, object>()
            {
                { "id", item.Id },
                { "site_id", item.SiteId },
                { "code_form_id", item.CodeFormId },
                { "date_utc", item.DateUTC.ToString(Common.Miscel.TIMESTAMP_FORMAT) },
                { "date_utc_recieve", item.DateUTCRecieve.ToString(Common.Miscel.TIMESTAMP_FORMAT) },
                { "date_loc_insert", item.DateLOCInsert.ToString(Common.Miscel.TIMESTAMP_FORMAT) },
                { "value", item.Value.Replace("\0", "") },
                { "hash", item.Hash },
            };
            Insert(fields);
        }

        //public void Insert(DataSource item)
        //{
        //    var fields = new Dictionary<string, object>()
        //    {
        //        { "site_id", item.SiteId },
        //        { "code_form_id", item.CodeFormId },
        //        { "date_utc", item.DateUTC },
        //        { "date_utc_recieve", item.DateUTCRecieve },
        //        { "date_loc_insert", item.DateLOCInsert },
        //        { "value", item.Value.Replace("\0", "") },
        //        { "hash", item.Hash },
        //    };
        //    Insert(fields);
        //}

        public long Insert(DataSource item)
        {
           
            var fields = new Dictionary<string, object>()
            {
                { "site_id", item.SiteId },
                { "code_form_id", item.CodeFormId },
                { "date_utc", item.DateUTC },
                { "date_utc_recieve", item.DateUTCRecieve },
                { "date_loc_insert", item.DateLOCInsert },
                { "value", item.Value.Replace("\0", "") },
                { "hash", item.Hash },
            };

            return InsertWithReturn(fields);
        }
    }
}
