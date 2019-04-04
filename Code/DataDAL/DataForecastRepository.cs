using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SOV.Common;
using Npgsql;

namespace SOV.Amur.Data
{
    public class DataForecastRepository : BaseRepository<DataForecast>
    {
        internal DataForecastRepository(Common.ADbNpgsql db) : base(db, "data.data_forecast") { }

        static public string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
        int BatchPortionLength = 1000;

        /// <summary>
        /// Записать значение.
        /// </summary>
        /// <param name="df">Значение для записи.</param>
        public void Insert(DataForecast df)
        {
            Insert(new List<DataForecast>(new DataForecast[] { df }));
        }
        public void Insert(List<DataForecast> dfs)
        {
            string sql = "insert into data.data_forecast (catalog_id, fcs_lag, date_fcs, value)"
                    + " values ({0}, {1}, '{2}', {3});";
            using (var cnn = _db.Connection)
            {
                using (Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand("", cnn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;

                    for (int i = 0; i < dfs.Count;)
                    {
                        cmd.CommandText = "";
                        for (int j = 0; j < BatchPortionLength && i != dfs.Count; j++, i++)
                        {
                            cmd.CommandText += string.Format(sql,
                                dfs[i].CatalogId,
                                dfs[i].LagFcs,
                                dfs[i].DateFcs.ToString(DATE_FORMAT),
                                dfs[i].Value.ToString().Replace(',', '.'));
                        }
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateFcsS"></param>
        /// <param name="dateFcsF"></param>
        /// <param name="catalogId">Null не допускается.</param>
        /// <param name="fcsLag">Все, если null.</param>
        /// <param name="isDateFcs">Запрос по датам прогноза или исходные даты с которых выполнялся прогноз.</param>
        /// <returns></returns>
        public List<DataForecast> Select(int catalogId, DateTime dateFcsS, DateTime dateFcsF, double? fcsLag, bool isDateFcs)
        {
            return SelectDataForecasts(new List<int>(new int[] { catalogId }), dateFcsS, dateFcsF, fcsLag.HasValue ? new List<double>(new double[] { (double)fcsLag }) : null, isDateFcs);
        }
        /// <summary>
        /// Функция парсинга строки результата запроса. Используется по умолчанию в ExecQuery.
        /// </summary>
        protected override object ParseData(NpgsqlDataReader reader)
        {
            return new DataForecast(
                (int)reader["catalog_id"],
                (double)reader["fcs_lag"],
                (DateTime)reader["date_fcs"],
                ((DateTime)reader["date_fcs"]).AddHours(-(int)reader["fcs_lag"]),
                (double)reader["value"],
                (DateTime)reader["date_insert"]
            );
        }

        public Dictionary<int, bool> Exists(List<int> catalogId, DateTime dateIni)
        {
            Dictionary<int, bool> ret = new Dictionary<int, bool>();
            string sql = "select case when exists(select catalog_id from data.data_forecast_view where catalog_id = :catalog_id and date_ini = :date_ini) then true else false end";

            using (var cnn = _db.Connection)
            {
                using (Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand(sql, cnn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("catalog_id", 0);
                    cmd.Parameters.AddWithValue("date_ini", dateIni);

                    for (int i = 0; i < catalogId.Count; i++)
                    {
                        cmd.Parameters[0].Value = catalogId[i];
                        ret.Add(catalogId[i], (bool)cmd.ExecuteScalar());
                    }
                }
            }
            return ret;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateS">Дата начала выбоки прогнозов.</param>
        /// <param name="dateF">Дата окончания выбоки прогнозов.</param>
        /// <param name="catalogId">Null не допускается.</param>
        /// <param name="fcsLag">Заблаговременность прогнозов. Все, если null.</param>
        /// <param name="isDateFcs">Указан интервал дат прогноза или исходных дат, с которых выполнялся прогноз?</param>
        /// <returns></returns>
        public List<DataForecast> SelectDataForecasts(
            List<int> catalogId,
            DateTime dateS, DateTime dateF,
            List<double> fcsLag,
            bool isDateFcs)
        {
            string sql = "data.select_data_forecast";
            var fields = new Dictionary<string, object>()
            {
                {"_date_s", dateS},
                {"_date_f", dateF},
                {"_catalog_id", catalogId},
                {"_fcs_lag", fcsLag},
                {"_is_date_fcs", isDateFcs}
            };
            return ExecQuery<DataForecast>(sql, fields, ParseDataForecast, CommandType.StoredProcedure);

            //List<DataForecast> ret = new List<DataForecast>();
            //using (var cnn = _db.Connection)
            //{
            //    using (Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand("data.select_data_forecast", cnn))
            //    {
            //        cmd.CommandType = System.Data.CommandType.StoredProcedure;

            //        cmd.Parameters.AddWithValue("_date_s", dateS.ToString(DATE_FORMAT));
            //        cmd.Parameters.AddWithValue("_date_f", dateF.ToString(DATE_FORMAT));
            //        cmd.Parameters.AddWithValue("_catalog_id", catalogId);
            //        cmd.Parameters.AddWithValue("_fcs_lag", fcsLag);
            //        cmd.Parameters.AddWithValue("_is_date_fcs", isDateFcs);

            //        using (Npgsql.NpgsqlDataReader rdr = cmd.ExecuteReader())
            //        {
            //            while (rdr.Read())
            //            {
            //                ret.Add(new DataForecast((int)rdr["catalog_id"], (double)rdr["fcs_lag"], (DateTime)rdr["date_fcs"],
            //                    (DateTime)rdr["date_ini"], (double)rdr["value"], (DateTime)rdr["date_insert"]));
            //            }
            //            return ret;
            //        }
            //    }
            //}
        }
        private object ParseDataForecast(NpgsqlDataReader reader)
        {
            return new DataForecast(
                (int)reader["catalog_id"],
                (double)reader["fcs_lag"],
                (DateTime)reader["date_fcs"],
                (DateTime)reader["date_ini"],
                (double)reader["value"],
                (DateTime)reader["date_insert"]
            );
        }
        /// <summary>
        /// Выборка минимальных и максимальных дат для набора записей каталога.
        /// возвращает Dictionary{int/*catalog_id*/, DateTime[/*min date_fcs, max date_fcs, min date_ini, max date_ini*/]}
        /// </summary>
        /// <param name="catalogIds">Коды каталога.</param>
        /// <returns>Dictionary int/*catalog_id*/, DateTime[/*min date_fcs, max date_fcs, min date_ini, max date_ini*/]</returns>
        public Dictionary<int/*catalog_id*/, DateTime[/*min date_fcs, max date_fcs, min date_ini, max date_ini*/]> SelectMinMaxDates(List<int> catalogIds)
        {
            var fields = new Dictionary<string, object>() { { "catalog_ids", catalogIds } };
            string sql = "Select catalog_id, min(date_fcs) as min_date_fcs, max(date_fcs) as max_date_fcs, " +
                            "min(date_ini) as min_date_ini, max(date_ini) as max_date_ini " +
                        "From data.data_forecast_view " +
                        QueryBuilder.Where(fields) + " " +
                        "Group by catalog_id";
            Dictionary<int, DateTime[]> ret = new Dictionary<int, DateTime[]>();
            foreach (var data in ExecQuery<Dictionary<string, object>>(sql, fields, base.ParseData))
                ret.Add(
                    (int)data["catalog_id"],
                    new DateTime[]{
                        (DateTime)data["min_date_fcs"], (DateTime)data["max_date_fcs"],
                        (DateTime)data["min_date_ini"], (DateTime)data["max_date_ini"]
                    }
                );
            return ret;
            //using (var cnn = _db.Connection)
            //{
            //    using (Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand("SELECT catalog_id, min(data_forecast_view.date_fcs) as min_date_fcs, max(data_forecast_view.date_fcs) as max_date_fcs , min(data_forecast_view.date_ini) as min_date_ini, max(data_forecast_view.date_ini) as max_date_ini FROM  data.data_forecast_view where data_forecast_view.catalog_id=any(:catalog_ids) Group by catalog_id", cnn))
            //    {
            //        cmd.Parameters.AddWithValue("catalog_ids", catalogIds);

            //        using (Npgsql.NpgsqlDataReader rdr = cmd.ExecuteReader())
            //        {
            //            while (rdr.Read())
            //            {
            //                ret.Add((int)rdr["catalog_id"], new DateTime[]{
            //                    (DateTime)rdr["min_date_fcs"], (DateTime)rdr["max_date_fcs"],
            //                    (DateTime)rdr["min_date_ini"], (DateTime)rdr["max_date_ini"]});
            //            }
            //            return ret;
            //        }
            //    }
            //}
        }

        public Dictionary<int, List<DateTime>> SelectGroupByCD(DateTime dateS, DateTime dateF, bool isDateFcs)
        {
            Dictionary<int, List<DateTime>> ret = new Dictionary<int, List<DateTime>>();
            string sql = "data.select_data_fcs_cd_grp";
            var fields = new Dictionary<string, object>()
            {
                {"_date_s", dateS }, //dateS.ToString(DATE_FORMAT)},
                {"_date_f", dateF }, //dateF.ToString(DATE_FORMAT)},
                {"_is_date_fcs", isDateFcs}
            };
            var queryResult = ExecQuery<Dictionary<string, object>>(sql, fields, base.ParseData, CommandType.StoredProcedure);
            foreach (var data in queryResult)
                if (ret.ContainsKey((int)data["_catalog_id"]))
                    ret[(int)data["_catalog_id"]].Add((DateTime)data["_date"]);
                else
                    ret.Add((int)data["_catalog_id"], new List<DateTime>() { (DateTime)data["_date"] });
            return ret;
            //using (var cnn = _db.Connection)
            //{
            //    using (Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand("data.select_data_fcs_cd_grp", cnn))
            //    {
            //        cmd.CommandType = System.Data.CommandType.StoredProcedure;

            //        cmd.Parameters.AddWithValue("_date_s", dateS.ToString(DATE_FORMAT));
            //        cmd.Parameters.AddWithValue("_date_f", dateF.ToString(DATE_FORMAT));
            //        cmd.Parameters.AddWithValue("_is_date_fcs", isDateFcs);

            //        using (Npgsql.NpgsqlDataReader rdr = cmd.ExecuteReader())
            //        {
            //            while (rdr.Read())
            //            {
            //                int catalogId = (int)rdr["_catalog_id"];
            //                List<DateTime> dates;

            //                if (!ret.ContainsKey(catalogId))
            //                {
            //                    dates = new List<DateTime>();
            //                    ret.Add(catalogId, dates);
            //                }
            //                else
            //                    if (!ret.TryGetValue(catalogId, out dates))
            //                {
            //                    dates = new List<DateTime>();
            //                }

            //                dates.Add((DateTime)rdr["_date"]);
            //            }
            //            return ret;
            //        }
            //    }
            //}
        }
        public List<DataForecast2> SelectDataForecasts2(int catalogId1, int catalogId2, DateTime dateFcsS, DateTime dateFcsF, List<double> fcsLag, bool isDateFcs)
        {
            List<DataForecast2> ret = new List<DataForecast2>();
            string sql = "data.select_data_forecast_2";
            var fields = new Dictionary<string, object>()
            {
                {"_date_fcs_s", dateFcsS.ToString(DATE_FORMAT)},
                {"_date_fcs_f", dateFcsF.ToString(DATE_FORMAT)},
                {"_catalog_id_1", catalogId1},
                {"_catalog_id_2", catalogId2},
                {"_fcs_lag", fcsLag},
                {"_is_date_fcs", isDateFcs}
            };
            return ExecQuery<DataForecast2>(sql, fields, ParseDataForecast2, CommandType.StoredProcedure);
            //using (var cnn = _db.Connection)
            //{
            //    using (Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand("data.select_data_forecast_2", cnn))
            //    {
            //        cmd.CommandType = System.Data.CommandType.StoredProcedure;

            //        cmd.Parameters.AddWithValue("_date_fcs_s", dateFcsS.ToString(DATE_FORMAT));
            //        cmd.Parameters.AddWithValue("_date_fcs_f", dateFcsF.ToString(DATE_FORMAT));
            //        cmd.Parameters.AddWithValue("_catalog_id_1", catalogId1);
            //        cmd.Parameters.AddWithValue("_catalog_id_2", catalogId2);
            //        cmd.Parameters.AddWithValue("_fcs_lag", fcsLag);
            //        cmd.Parameters.AddWithValue("_is_date_fcs", isDateFcs);

            //        using (Npgsql.NpgsqlDataReader rdr = cmd.ExecuteReader())
            //        {
            //            while (rdr.Read())
            //            {
            //                ret.Add(new DataForecast2(
            //                    (int)rdr["catalog_id"], (double)rdr["fcs_lag"], (DateTime)rdr["date_fcs"], (DateTime)rdr["date_ini"], (DateTime)rdr["date_insert"],
            //                    (double)rdr["value1"], (double)rdr["value2"])
            //                );
            //            }
            //            return ret;
            //        }
            //    }
            //}
        }
        private object ParseDataForecast2(NpgsqlDataReader reader)
        {
            return new DataForecast2(
                (int)reader["catalog_id"],
                (double)reader["fcs_lag"],
                (DateTime)reader["date_fcs"],
                (DateTime)reader["date_ini"],
                (DateTime)reader["date_insert"],
                (double)reader["value1"],
                (double)reader["value2"]);
        }
        public void Delete(List<DataForecast> dfs)
        {
            string sql = "delete from data.data_forecast where catalog_id = {0} and date_fcs = '{1}' and fcs_lag = {2};";
            using (var cnn = _db.Connection)
            {
                //using (Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand("delete from data.data_forecast" +
                //    " where catalog_id = :catalog_id and date_fcs = :date_fcs and fcs_lag = :fcs_lag", cnn))
                using (Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand("", cnn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;

                    for (int i = 0, k; i < dfs.Count;)
                    {
                        cmd.CommandText = "";
                        for (int j = 0; j < BatchPortionLength && i != dfs.Count; j++, i++)
                        {
                            cmd.CommandText += string.Format(sql,
                                dfs[i].CatalogId,
                                dfs[i].DateFcs.ToString(DATE_FORMAT),
                                dfs[i].LagFcs.ToString().Replace(",", "."));
                        }
                        k = cmd.ExecuteNonQuery();
                    }
                    //cmd.Parameters.AddWithValue("catalog_id", 0);
                    //cmd.Parameters.AddWithValue("date_fcs", DateTime.Now);
                    //cmd.Parameters.AddWithValue("fcs_lag", (double)0);

                    //foreach (var df in dfs)
                    //{
                    //    cmd.Parameters[0].Value = df.CatalogId;
                    //    cmd.Parameters[1].Value = df.DateFcs;
                    //    cmd.Parameters[2].Value = df.LagFcs;

                    //    cmd.ExecuteNonQuery();
                    //}
                }
            }
        }
    }
}
