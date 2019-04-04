using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Npgsql;

namespace SOV.Common
{
    /// <summary>
    /// Базовый репозиторий. Содержит общие функции работы с БД. T - основной класс-обертка для результатов запроса. 
    /// </summary>
    public class BaseRepository<T> where T : class
    {
        /// <summary>
        /// Максимальное количество запросов в одной транзакции
        /// </summary>
        private int BatchPortionLength = 1000;

        protected Common.ADbNpgsql _db;
        public string TableName { get; protected set; }

        /// <summary>
        /// Закешированные данные репозитория.
        /// </summary>
        protected static List<T> dicCache;

        protected static List<T> GetCash(BaseRepository<T> rep)
        {
            return dicCache ?? (dicCache = rep.Select());
        }

        public static void ClearCache()
        {
            if (dicCache != null)
                dicCache.Clear();
            dicCache = null;
        }

        public NpgsqlConnection Connection
        {
            get
            {
                return _db.Connection;
            }
        }

        protected delegate object DataParserDelegator(NpgsqlDataReader reader);
        protected DataParserDelegator DefDataParser;

        public BaseRepository(Common.ADbNpgsql db, string tableName)
        {
            this._db = db;
            this.TableName = tableName;
            this.DefDataParser = new DataParserDelegator(ParseData);
        }

        /// <summary>
        /// Функция парсинга строки результата запроса. Используется по умолчанию в ExecQuery.
        /// </summary>
        /// <overloads>Функция парсинга строки результата запроса. Используется по умолчанию в ExecQuery.</overloads>
        protected virtual object ParseData(NpgsqlDataReader reader)
        {
            var res = new Dictionary<string, object>();
            for (int i = 0; i < reader.FieldCount; ++i)
                res.Add(reader.GetName(i), reader.GetValue(i));
            return res;
        }

        /// <summary>
        /// Функция парсинга id
        /// </summary>
        /// <overloads>Функция парсинга строки результата запроса. Используется по умолчанию в ExecQuery.</overloads>
        protected virtual object ParseId(NpgsqlDataReader reader)
        {
            return reader.GetValue(0);
        }

        public virtual List<T> Select()
        {
            return ExecQuery<T>(string.Format("select * from {0}", TableName), new Dictionary<string, object>());
        }

        public virtual T Select(int id)
        {
            List<T> res = Select(new List<int>(new int[] { id }));
            return res.Count == 0 ? null : res[0];
        }

        public virtual List<T> Select(List<int> idList)
        {
            return Select(new Dictionary<string, object>() { { "id", idList } });
        }

        public virtual List<T> SelectAllFields()
        {
            return SelectAllFields(new Dictionary<string, object>() { { "id", null } });
        }

        public virtual List<T> SelectAllFields(Dictionary<string, object> fields)
        {
            return SelectAllFields("Select * From " + TableName, fields);
        }

        protected List<T> SelectAllFields(
            string sql,
            Dictionary<string, object> fields,
            DataParserDelegator parser = null,
            QueryOper oper = QueryOper.EQ,
            QueryPredicate predicate = QueryPredicate.AND)
        {
            sql = string.Format("{0} {1}", sql, QueryBuilder.Where(fields, oper, predicate));
            return ExecQuery<T>(sql, fields, parser);
        }

        /// <summary>
        /// Выбрать поля таблицы, соответствующие значениям параметров
        /// </summary>
        /// <param name="fields">Словарь параметров, где ключ - название параметра в запросе</param>
        /// <param name="oper">Операция сравнения параметров. По-умолчанию '='</param>
        /// <param name="predicate">Логический предикат, соединяющий блоки параметров. По-умолчанию 'and'</param>
        protected List<T> Select(
            Dictionary<string, object> fields,
            QueryOper oper = QueryOper.EQ,
            QueryPredicate predicate = QueryPredicate.AND)
        {
            string sql = string.Format("select * from {0} {1}", TableName, QueryBuilder.Where(fields, oper, predicate));
            return ExecQuery<T>(sql, fields);
        }

        public virtual void Insert(Dictionary<string, object> fields)
        {
            Insert(new List<Dictionary<string, object>>() { fields });
        }

        public virtual int InsertWithReturn(Dictionary<string, object> fields)
        {
            return InsertWithReturn(new List<Dictionary<string, object>>() { fields });
        }

        protected string GetInsertQuery(List<Dictionary<string, object>> fieldsList, string insertAtTableName = null)
        {
            if (fieldsList.Count == 0 || fieldsList.Any(x => x.Count == 0))
                throw new Exception("Insert: Пустой список полей");
            return string.Format("Insert into {0}({1}) values ({2});",
                insertAtTableName ?? TableName,
                string.Join(",", fieldsList[0].Keys),
                string.Join(",", fieldsList[0].Keys.Select(x => ":" + x.Replace("\"", "")))
            );
        }

        protected int InsertWithReturn(List<Dictionary<string, object>> fieldsList)
        {
            return int.Parse(ExecSimpleQuery(GetInsertQuery(fieldsList), fieldsList, true, "select lastval();"));
        }

        protected List<int> InsertWithFullReturn(List<Dictionary<string, object>> fieldsList)
        {
            ExecSimpleQuery(GetInsertQuery(fieldsList), fieldsList);
            string sql = string.Format("Select id From {0} order by id desc LIMIT {1}", TableName, fieldsList.Count);
            return ExecQuery<int>(sql, new Dictionary<string, object>(), ParseId);
        }

        protected void Insert(List<Dictionary<string, object>> fieldsList)
        {
            ExecSimpleQuery(GetInsertQuery(fieldsList), fieldsList);
        }

        /// <summary>
        /// Обновить значения поля таблицы. По умолчанию, поле 'id' считается ключевым и используется для определения,
        ///  редактируемого поля (блок Where в запросе). Для изменения ключевого поля/назначениях нескольких,
        ///  метод необходимо переопределить.
        /// </summary>
        /// <param name="fields">Словарь параметров, где ключ - название параметра в запросе</param>

        public virtual void Update(Dictionary<string, object> fields)
        {
            Update(new List<Dictionary<string, object>>() { fields }, new List<string>() { "id" });
        }

        protected string GetUpdateQuery(
            List<Dictionary<string, object>> fieldsList,
            List<string> whereFields,
            string updateAtTableName = null)
        {
            return string.Format("update {0} {1} {2};",
                updateAtTableName ?? TableName,
                QueryBuilder.Set(fieldsList[0].Where(x => !whereFields.Contains(x.Key)).ToDictionary(x => x.Key, x => x.Value)),
                QueryBuilder.Where(fieldsList[0].Where(x => whereFields.Contains(x.Key)).ToDictionary(x => x.Key, x=> x.Value))
            );
        }

        protected void Update(List<Dictionary<string, object>> fieldsList, List<string> whereFields)
        {
            if (fieldsList.Count == 0 || fieldsList.Any(x => x.Count == 0))
                throw new Exception("Update: Пустой список полей");
            ExecSimpleQuery(GetUpdateQuery(fieldsList, whereFields), fieldsList);
        }

        public virtual void Delete(T obj)
        {
            Delete((int)ObjectHandler.FieldVal<T>(obj, "id"));
        }

        public virtual void Delete(int id)
        {
            Delete(new List<int> { id });
        }

        public virtual void Delete(List<int> ids)
        {
            Delete(new Dictionary<string, object>() { { "id", ids } });
        }

        /// <summary>
        /// Удалить поле таблицы. По умолчанию, поле 'id' считается ключевым и используется для определения,
        ///  удаляемого поля (блок Where в запросе). Для изменения ключевого поля/назначениях нескольких,
        ///  метод необходимо переопределить.
        /// </summary>
        /// <param name="fields">Словарь параметров, где ключ - название параметра в запросе</param>

        public virtual void Delete(Dictionary<string, object> fields)
        {
            Delete(new List<Dictionary<string, object>>() { new Dictionary<string, object> { { "id", fields["id"] } } });
        }

        protected string GetDeleteQuery(List<Dictionary<string, object>> fieldsList, string deteleAtTableName = null)
        {
            return string.Format("delete from {0} {1};",
                deteleAtTableName ?? TableName,
                QueryBuilder.Where(fieldsList[0])
            );
        }

        protected virtual void Delete(List<Dictionary<string, object>> fieldsList)
        {
            if (fieldsList.Count == 0 || fieldsList.Any(x => x.Count == 0))
                throw new Exception("Delete: Пустой список полей");
            ExecSimpleQuery(GetDeleteQuery(fieldsList), fieldsList);
        }

        /// <summary>
        /// Выполнить запрос. 'U' - класс-обертки для результатов данного запроса.
        /// </summary>
        /// <param name="sql">Строка - текст запроса</param>
        /// <param name="fields">Словарь параметров, где ключ - название параметра в запросе</param>
        /// <param name="dataParser">Делегатор на функцию парсинга выбранных данных (если null - используется DefDataParser)</param>
        /// <param name="commandType">Тип строки запроса</param>
        protected List<U> ExecQuery<U>(
            string sql,
            Dictionary<string, object> fields,
            DataParserDelegator dataParser = null,
            System.Data.CommandType commandType = System.Data.CommandType.Text)
        {
            List<U> res = new List<U>();
            dataParser = dataParser ?? DefDataParser;
            try
            {
                using (NpgsqlConnection cnn = _db.Connection)
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, cnn))
                {
                    cmd.CommandType = commandType;
                    foreach (var field in fields)
                    {
                        cmd.Parameters.AddWithValue(field.Key, field.Value ?? DBNull.Value);
                        //if(field.Value == null)
                        //{
                        //    cmd.Parameters[field.Key].NpgsqlValue = null;
                        //}
                    }
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                            res.Add((U)dataParser(reader));
                        return res;
                    }
                }
            }
            catch (System.Data.Common.DbException e)
            {
                throw new RuDbException(e);
            }
        }

        /// <summary>
        /// Выполнить простой запрос, без возвращения данных (Insert, Update...), либо со скалярным результатом. 
        /// </summary>
        /// <param name="sql">Строка - текст запроса</param>
        /// <param name="fields">Словарь параметров, где ключ - название параметра в запросе.</param>
        /// <param name="hasResult">True - возврящает string (скаляр), False - возврящает null</param>
        /// <param name="extraQuery">Дополнительный запрос. Выполняется в одной сессии, после основного.</param>
        /// <param name="commandType">Тип строки запроса</param>
        protected string ExecSimpleQuery(string sql, Dictionary<string, object> fields, bool hasResult = false,
                                        string extraQuery = "",
                                        System.Data.CommandType commandType = System.Data.CommandType.Text)
        {
            return ExecSimpleQuery(sql, new List<Dictionary<string, object>>() { fields }, hasResult, extraQuery, commandType);
        }

        /// <summary>
        /// Выполнить цикл простых запросов, без возвращения данных (Insert, Update...), либо со скалярным результатом. 
        /// Цикл выполняется по каждому элементу fieldsList.
        /// </summary>
        /// <param name="sql">Строка - текст запроса</param>
        /// <param name="fieldsList">Список словарей параметров, где ключ - название параметра в запросе.</param>
        /// <param name="hasResult">True - возврящает string (скаляр), False - возврящает null</param>
        /// <param name="extraQuery">Дополнительный запрос. Выполняется в одной сессии, после основного цикла.</param>
        /// <param name="commandType">Тип строки запроса</param>
        protected string ExecSimpleQuery(string sql, List<Dictionary<string, object>> fieldsList, bool hasResult = false,
                                        string extraQuery = "",
                                        System.Data.CommandType commandType = System.Data.CommandType.Text)
        {
            try
            {
                using (NpgsqlConnection cnn = _db.Connection)
                using (NpgsqlCommand cmd = new NpgsqlCommand("", cnn))
                    for (int i = 0, counter = 0; i < fieldsList.Count; ++i, ++counter)
                    {
                        var tmpSql = sql;
                        foreach (var field in fieldsList[i])
                        {
                            string key = field.Key.Replace("\"", "");
                            Regex reg = new Regex("([=|\\s|,|\\(]+:)" + key + "([\\s|;|,|\\)]+|$)");
                            tmpSql = reg.Replace(tmpSql, "${1}" + key + i + "${2}");
                            cmd.Parameters.AddWithValue(key + i, field.Value ?? DBNull.Value);
                        }
                        cmd.CommandType = commandType;
                        cmd.CommandText += tmpSql;
                        // ******************************** OSokolov 2017.12.22
                        cmd.CommandText = cmd.CommandText.Replace(", order=", ", \"order\"=");
                        cmd.CommandText = cmd.CommandText.Replace(", order =", ", \"order\"=");
                        cmd.CommandText = cmd.CommandText.Replace(",order,", ",\"order\",");
                        // ******************************** 
                        if (i != fieldsList.Count - 1 && counter != BatchPortionLength) continue;
                        if (i == fieldsList.Count - 1) cmd.CommandText += extraQuery;
                        if (i == fieldsList.Count - 1 && hasResult) return cmd.ExecuteScalar().ToString();
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "";
                        counter = 0;
                    }
            }
            catch (System.Data.Common.DbException e)
            {
                throw new RuDbException(e);
            }
            return null;
        }
        public virtual int? SelectMaxId()
        {
            using (NpgsqlConnection cnn = _db.Connection)
            using (NpgsqlCommand cmd = new NpgsqlCommand(string.Format("select max(id) from {0}", TableName), cnn))
            {
                using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                {
                    rdr.Read();
                    return rdr.IsDBNull(0) ? null : (int?)int.Parse(rdr[0].ToString());
                }
            }
        }
        public virtual short? SelectMaxOrder()
        {
            using (NpgsqlConnection cnn = _db.Connection)
            using (NpgsqlCommand cmd = new NpgsqlCommand(string.Format("select max(\"order\") from {0}", TableName), cnn))
            {
                using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                {
                    rdr.Read();
                    return rdr.IsDBNull(0) ? null : (short?)short.Parse(rdr[0].ToString());
                }
            }
        }
    }
}
