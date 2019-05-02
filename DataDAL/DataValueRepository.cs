using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using SOV.Common;
using Npgsql;
using System.ComponentModel;
using SOV.Amur.Meta;

namespace SOV.Amur.Data
{
    public class DataValueRepository : BaseRepository<DataValue>
    {
        internal DataValueRepository(Common.ADbNpgsql db) : base(db, "data.data_value") { }

        static public string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";

        public DataValue Select(long id)
        {
            List<DataValue> ret = Select(new List<long>(new long[] { id }));
            return ret.Count == 1 ? ret[0] : null;
        }
        public List<DataValue> Select(List<long> dvsId)
        {
            using (var cnn = _db.Connection)
            {
                using (Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand("select * from data.data_value where id = ANY(:id)", cnn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("id", dvsId);

                    using (Npgsql.NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        List<DataValue> ret = new List<DataValue>();
                        while (rdr.Read())
                        {
                            ret.Add(Parse(rdr));
                        }
                        return ret;
                    }
                }
            }
        }
        [Obsolete("Use SelectDataValue(long catalogId, DateTime dateUTC, double value)")]
        public long? SelectDataId(long catalogId, DateTime dateLoc, double value)
        {
            using (var cnn = _db.Connection)
            {
                using (Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand("select id from data.data_value"
                    + " where catalog_id = " + catalogId
                    + " and date_loc = '" + dateLoc.ToString(DATE_FORMAT)
                    + "' and value = " + value, cnn))
                {
                    using (Npgsql.NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                            return (long)rdr["id"];
                        return null;
                    }
                }
            }
        }
        public DataValue SelectDataValue(long catalogId, DateTime dateUTC, double value)
        {
            using (var cnn = _db.Connection)
            {
                using (Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand("select * from data.data_value"
                    + " where catalog_id = :catalog_id and date_utc = :date_utc and value = :value", cnn))
                {
                    cmd.Parameters.AddWithValue("catalog_id", catalogId);
                    cmd.Parameters.AddWithValue("date_utc", dateUTC);
                    cmd.Parameters.AddWithValue("value", value);

                    using (Npgsql.NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                            return Parse(rdr);
                        return null;
                    }
                }
            }
        }
        private void Delete(long id)
        {
            using (var cnn = _db.Connection)
            {
                using (Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand("delete from data.data_value where id = " + id, cnn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private DataValue Parse(Npgsql.NpgsqlDataReader rdr)
        {
            return new DataValue(
                (Int64)rdr["id"],
                (int)rdr["catalog_id"],
                (double)rdr["value"],
                (DateTime)rdr["date_loc"],
                (DateTime)rdr["date_utc"],
                (byte)(Int16)rdr["flag_aqc"],
                (float)rdr["utc_offset"]
            );
        }
        public List<DataValue> SelectA(DataFilter dataFilter)
        {
            if (dataFilter.CatalogFilter.Catalogs != null)
            {
                return SelectA(
                    (DateTime)dataFilter.DateTimePeriod.DateS, (DateTime)dataFilter.DateTimePeriod.DateF,
                    dataFilter.CatalogFilter.Catalogs.Select(x => x.Id).ToList(),
                    dataFilter.IsActualValueOnly,
                    dataFilter.IsSelectDeleted,
                    dataFilter.FlagAQC,
                    dataFilter.IsDateLOC
                );
            }
            else
                return SelectA(
                    (DateTime)dataFilter.DateTimePeriod.DateS, (DateTime)dataFilter.DateTimePeriod.DateF,
                    dataFilter.IsDateLOC,
                    dataFilter.CatalogFilter.Sites, dataFilter.CatalogFilter.Variables,
                    dataFilter.CatalogFilter.OffsetTypes,
                    dataFilter.CatalogFilter.OffsetValue,
                    dataFilter.IsActualValueOnly, dataFilter.IsSelectDeleted,
                    dataFilter.CatalogFilter.Methods,
                    dataFilter.CatalogFilter.Sources,
                    dataFilter.FlagAQC
                );
        }
        /// <summary>
        /// Выборка данных вместе с записями каталога.
        /// Сохраняется порядок данных, их сортировка в соответствие с флагом контроля, методом и кодом.
        /// </summary>
        /// <param name="dateS"></param>
        /// <param name="dateF"></param>
        /// <param name="isDateLOC"></param>
        /// <param name="siteId"></param>
        /// <param name="variableId"></param>
        /// <param name="offsetTypeId"></param>
        /// <param name="offsetValue"></param>
        /// <param name="isOneValue"></param>
        /// <param name="isSelectDeleted"></param>
        /// <param name="methodId"></param>
        /// <param name="sourceId"></param>
        /// <param name="flagAQC"></param>
        /// <returns>Набор данных. 
        /// Сохраняется порядок данных, их сортировка в соответствие с флагом контроля, методом и кодом.</returns>
        public List<DataValue> SelectA(
            DateTime dateS, DateTime dateF, bool isDateLOC,
            List<int> siteId, List<int> variableId,
            List<int> offsetTypeId, double? offsetValue,
            bool isOneValue, bool isSelectDeleted = false,
            List<int> methodId = null, List<int> sourceId = null,
            byte? flagAQC = null)
        {
            List<Catalog> ctls = SOV.Amur.Meta.DataManager.GetInstance(_db.ConnectionString).CatalogRepository.Select(
                siteId, variableId, methodId, sourceId, offsetTypeId,
                offsetValue.HasValue ? new List<double>() { (double)offsetValue } : null);
            return SelectA(dateS, dateF, ctls.Select(x => (int)x.Id).ToList(), isOneValue, isSelectDeleted, flagAQC, isDateLOC);
        }
        /// <summary>
        /// Выборка данных вместе с записями каталога.
        /// Внимание! Порядок данных, их сортировка в соответствие с флагом контроля, методом и кодом - не сохраняется
        /// </summary>
        /// <param name="dateS"></param>
        /// <param name="dateF"></param>
        /// <param name="isDateLOC"></param>
        /// <param name="siteId"></param>
        /// <param name="variableId"></param>
        /// <param name="offsetTypeId"></param>
        /// <param name="offsetValue"></param>
        /// <param name="isOneValue"></param>
        /// <param name="isSelectDeleted"></param>
        /// <param name="methodId"></param>
        /// <param name="sourceId"></param>
        /// <param name="flagAQC"></param>
        /// <returns>Словарь записей каталога и соответствующих им данных. 
        /// Внимание! Порядок данных, их сортировка в соответствие с флагом контроля, методом и кодом - не сохраняется.</returns>
        public Dictionary<Catalog, List<DataValue>> SelectA1(
            DateTime dateS, DateTime dateF, bool isDateLOC,
            List<int> siteId, List<int> variableId,
            List<int> methodId, List<int> sourceId,
            List<int> offsetTypeId, List<double> offsetValue,
            byte? flagAQC = null,
            bool isOneValue = true, bool isSelectDeleted = false
        )
        {
            List<Catalog> ctls = SOV.Amur.Meta.DataManager.GetInstance(_db.ConnectionString).CatalogRepository.Select(
                siteId, variableId, methodId, sourceId, offsetTypeId,
                offsetValue
            );

            Dictionary<Catalog, List<DataValue>> ret = new Dictionary<Catalog, List<DataValue>>();
            if (ctls.Count != 0)
            {
                List<DataValue> dvs = SelectA(dateS, dateF, ctls.Select(x => (int)x.Id).ToList(), isOneValue, isSelectDeleted, flagAQC, isDateLOC);
                foreach (var ctl in ctls)
                {
                    List<DataValue> dvs1 = dvs.FindAll(x => x.CatalogId == ctl.Id);
                    ret.Add(ctl, dvs1 == null ? new List<DataValue>() : dvs1);
                }
            }
            return ret;
        }

        public List<DataValue> SelectA(
            DateTime dateS, DateTime dateF, List<int> catalogId,
            bool isActualValue, bool isSelectDeleted = false, byte? flagAQC = null, bool isDateLOC = true)
        {
            if (catalogId == null)
                throw new Exception("Не определены записи каталога данных для считывания данных.");
            var fields = new Dictionary<string, object>()
                {
                    {"_date_s", dateS},
                    {"_date_f", dateF},
                    {"_catalog_id", catalogId},
                    {"_flag_aqc", (short?) flagAQC},
                    {"_is_actual_value", isActualValue},
                    { "_is_sel_deleted", isSelectDeleted},
                    { "_is_date_loc", isDateLOC}
            };
            string sql = "select * from data.select_data_value_2017"
                + "(:_date_s::timestamp,:_date_f::timestamp,:_catalog_id::integer[],:_flag_aqc::smallint,:_is_actual_value::boolean,:_is_sel_deleted::boolean,:_is_date_loc::boolean)";
            return ExecQuery<DataValue>(sql, fields, Parse, System.Data.CommandType.Text);
        }

        //    public List<DataValue> _DELME_SelectA(
        //DateTime dateS, DateTime dateF, List<int> catalogId,
        //bool isActualValue, bool isSelectDeleted = false, byte? flagAQC = null, bool isDateLOC = true)
        //    {
        //        if (catalogId == null) 
        //            throw new Exception("Не определены записи каталога данных для считывания данных: или null, или Count = 0.");

        //        List<DataValue> ret = new List<DataValue>();
        //        using (NpgsqlConnection cnn = _db.Connection)
        //        {
        //            using (Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand("data.select_data_value_2017", cnn))
        //            {
        //                cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //                cmd.CommandTimeout = 5 * 60;

        //                cmd.Parameters.AddWithValue("_date_s", dateS.ToString(DATE_FORMAT));
        //                cmd.Parameters.AddWithValue("_date_f", dateF.ToString(DATE_FORMAT));
        //                cmd.Parameters.Add(ADbNpgsql.GetParameter("_catalog_id", catalogId.ToArray()));
        //                cmd.Parameters.Add(ADbNpgsql.GetParameter("_flagAQC", flagAQC)).NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Smallint;
        //                cmd.Parameters.AddWithValue("_is_actual_value", isActualValue);
        //                cmd.Parameters.AddWithValue("is_sel_deleted", isSelectDeleted);
        //                cmd.Parameters.AddWithValue("is_date_loc", isDateLOC);

        //                using (Npgsql.NpgsqlDataReader rdr = cmd.ExecuteReader())
        //                {
        //                    while (rdr.Read())
        //                    {
        //                        ret.Add(Parse(rdr));
        //                    }
        //                    return ret;
        //                }
        //            }
        //        }
        //    }

        /// <summary>
        /// Установка флага значение в состояние "удалено".
        /// </summary>
        /// <param name="id">Код значения.</param>
        public void DeleteDataValue(long id)
        {
            UpdateFlagAQC(new List<long>(new long[] { id }), 255);
        }
        /// <summary>
        /// 
        /// Актуализировать значение (по требованию оператора): 
        /// 
        ///     1) Переместить значение с заданным в параметрах метода Id в конец таблицы данных (с максимальным кодом) 
        ///         с сохранением всех внешних связей существующего, актуализируемого значения.
        ///     2) Провести автокритконтроль вставленного значения (AQC).
        ///     3) Установить критерий QCL = 1.
        ///     4) Установить флаг AQC = 1 даже в случае наличия критических квалификаторов значения.
        /// </summary>
        /// <param name="id">Код значения для его актуализации.</param>
        /// <returns>Новый id актуализированного значения.</returns>
        public void Actualize(long id)
        {
            using (var cnn = _db.Connection)
            {
                using (var cmd = new Npgsql.NpgsqlCommand("data.actualize_data_value(:id)", cnn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Установка флага указанных значений за исключением значений с флагом 255 (удалено).
        /// </summary>
        /// <param name="dvId">Список кодов значений у которых будет изменён флаг.</param>
        /// <param name="flagAQC">Устанавливаемый флаг АКК.</param>
        public void UpdateFlagAQC(List<long> dvId, int flagAQC)
        {
            if (dvId.Count > 0)
                using (var cnn = _db.Connection)
                {
                    using (Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand(
                        "update data.data_value set flag_aqc = " + flagAQC
                        + " where id in (" + StrVia.ToString(dvId) + ") and flag_aqc <> 255", cnn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
        }

        int _batchPortionLength = 1000;

        /// <summary>
        /// Количество операторов insert в пакете при записи массивов пакетами.
        /// </summary>
        public int BatchPortionLength
        {
            get { return _batchPortionLength; }
            set
            {
                if (value > 0)
                    _batchPortionLength = value;
            }
        }
        /// <summary>
        /// Запись значения.
        /// </summary>
        public long Insert(int catalogId, DateTime dateUTC, DateTime dateLOC, double value, byte flagAQC = 0, long? dataSourceId = null)
        {
            string sql = "select * from data.insert_data_value_201611"
                + " (:catalog_id::integer, :date_utc::timestamp without time zone, :date_loc::timestamp without time zone,"
                + " :value::double precision, :flag_aqc::smallint, :data_source_id::bigint);";
            //string sql = "data.insert_data_value_201611";


            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand(sql, cnn))
                {
                    //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("catalog_id", catalogId);
                    cmd.Parameters.AddWithValue("date_utc", dateUTC);
                    cmd.Parameters.AddWithValue("date_loc", dateLOC);
                    cmd.Parameters.AddWithValue("value", value);
                    cmd.Parameters.AddWithValue("flag_aqc", flagAQC);
                    //cmd.Parameters.AddWithValue("_data_source_id", NpgsqlTypes.NpgsqlDbType.Bigint, dataSourceId.HasValue ? dataSourceId : null);
                    cmd.Parameters.Add(ADbNpgsql.GetParameter("data_source_id", dataSourceId));

                    return (long)cmd.ExecuteScalar();
                }
            }
        }

        /// <summary>
        /// Запись массива значений порциями (для скорости).
        /// </summary>
        /// <param name="dvs">Массив значений. Флаг АКК во внимание не принимается - записывается 0.</param>
        /// <param name="dvsPortionLength">Количество записываемых элементов в одной команде insert.</param>
        public void Insert(List<DataValue> dvs, long? data_source_id = null)
        {
            string sql = "select * from data.insert_data_value_201611"
                + " (:catalog_id::integer, :date_utc::timestamp without time zone, :date_loc::timestamp without time zone,"
                + " :value::double precision, :flag_aqc::smallint, :data_source_id::bigint);";

            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand("", cnn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;

                    for (int i = 0; i < dvs.Count;)
                    {
                        cmd.CommandText = "";
                        for (int j = 0; j < BatchPortionLength && i != dvs.Count; j++, i++)
                        {
                            cmd.CommandText += sql
                                .Replace(":catalog_id", dvs[i].CatalogId.ToString())
                                .Replace(":date_utc", "'" + dvs[i].DateUTC.ToString(DATE_FORMAT) + "'")
                                .Replace(":date_loc", "'" + dvs[i].DateLOC.ToString(DATE_FORMAT) + "'")
                                .Replace(":value", dvs[i].Value.ToString().Replace(",", "."))
                                .Replace(":flag_aqc", dvs[i].FlagAQC.ToString().Replace(",", "."))
                                .Replace(":data_source_id", ((data_source_id.HasValue) ? data_source_id.ToString() : "null"));
                        }

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            //string sql = "select * from data.insert_data_value"
            //    + " (:catalog_id::integer, :date_loc::timestamp without time zone, :value::double precision, :utc_offset::real, :data_source_id::bigint);";

            //using (var cnn = _db.Connection)
            //{
            //    using (Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand("", cnn))
            //    {
            //        cmd.CommandType = System.Data.CommandType.Text;

            //        for (int i = 0; i < dvs.Count; )
            //        {
            //            cmd.CommandText = "";
            //            for (int j = 0; j < BatchPortionLength && i != dvs.Count; j++, i++)
            //            {
            //                cmd.CommandText += sql
            //                    .Replace(":catalog_id", dvs[i].CatalogId.ToString())
            //                    .Replace(":value", dvs[i].Value.ToString().Replace(",", "."))
            //                    .Replace(":date_loc", "'" + dvs[i].DateLOC.ToString(DATE_FORMAT) + "'")
            //                    .Replace(":utc_offset", dvs[i].UTCOffset.ToString())
            //                    .Replace(":data_source_id", ((data_source_id.HasValue) ? data_source_id.ToString() : "null"));
            //            }

            //            cmd.ExecuteNonQuery();
            //        }
            //    }
            //}
        }

        /// <summary>
        /// Выбор кодов исходных/родительских значений для заданного кода наследованного значения.
        /// </summary>
        /// <param name="derivedId">Код наследованного значения.</param>
        /// <returns>Коды исходных/родительских значений.</returns>
        public List<long> SelectDerivedValueId(long derivedId)
        {
            using (var cnn = _db.Connection)
            {
                using (Npgsql.NpgsqlCommand cmd =
                    new Npgsql.NpgsqlCommand("select parent_data_value_id from data.datavalue_derived where derived_data_value_id = " + derivedId, cnn))
                {
                    using (Npgsql.NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        List<long> ret = new List<long>();
                        while (rdr.Read())
                        {
                            ret.Add((Int64)rdr["parent_data_value_id"]);
                        }
                        return ret;
                    }
                }
            }
        }

        public void DeleteDerivedValue(long derivedDataValueId)
        {
            using (var cnn = _db.Connection)
            {
                using (Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand("delete from data.datavalue_derived where derived_data_value_id = " + derivedDataValueId, cnn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Вставка связки кода значения и кодов значений, из которых оно получено.
        /// </summary>
        /// <param name="derived_id">Код наследованного значения.</param>
        /// <param name="parent_data_value_id">Коды исходных/родительских значений.</param>
        public void InsertDerivedValue(long derived_id, List<long> parent_id)
        {
            string sql = "insert into data.datavalue_derived values (" + derived_id + ", :parent_id);";

            using (var cnn = _db.Connection)
            {
                using (Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand("", cnn))
                {
                    for (int i = 0; i < parent_id.Count;)
                    {
                        cmd.CommandText = "";
                        for (int j = 0; j < BatchPortionLength && i != parent_id.Count; j++, i++)
                        {
                            cmd.CommandText += sql.Replace(":parent_id", parent_id[i].ToString());
                        }
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        /// <summary>
        /// Запись значения.
        /// </summary>
        /// <param name="dv">Id во внимание не принимается.</param>
        public long Insert(DataValue dv)
        {
            return Insert(dv.CatalogId, dv.DateUTC, dv.DateLOC, dv.Value, dv.FlagAQC);
            //string sql = "data.insert_data_value"
            //    + " (:catalog_id::integer, :date_loc::timestamp without time zone, :value::double precision, :utc_offset::real);";

            //using (var cnn = _db.Connection)
            //{
            //    using (Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand("", cnn))
            //    {
            //        cmd.CommandType = System.Data.CommandType.StoredProcedure;

            //        cmd.CommandText = sql
            //            .Replace(":catalog_id", dv.CatalogId.ToString())
            //            .Replace(":value", dv.Value.ToString().Replace(",", "."))
            //            .Replace(":date_loc", "'" + dv.DateLOC.ToString(DATE_FORMAT) + "'")
            //            .Replace(":utc_offset", dv.UTCOffset.ToString());
            //        return (long)cmd.ExecuteScalar();
            //    }
            //}
        }
        /// <summary>
        /// Выбрать значения, которые наследованы от указанного в параметрах.
        /// </summary>
        /// <param name="parentId">Значение-родитель</param>
        /// <returns></returns>
        public List<DataValue> SelectDeriveds(long parentId)
        {
            using (var cnn = _db.Connection)
            {
                using (Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand(
                    "select data_value.* from data.datavalue_derived"
                    + " inner join data.data_value on data_value.id = derived_data_value_id"
                    + " where parent_data_value_id = " + parentId, cnn))
                {
                    using (Npgsql.NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        List<DataValue> ret = new List<DataValue>();
                        while (rdr.Read())
                        {
                            ret.Add(Parse(rdr));
                        }
                        return ret;
                    }

                }
            }
        }
        /// <summary>
        /// Выбрать значения, на основе которых получено значение указанное в параметрах.
        /// </summary>
        /// <param name="derivedId">Значение-наследник</param>
        /// <returns></returns>
        public List<DataValue> SelectParents(long derivedId)
        {
            List<DataValue> ret = new List<DataValue>();

            using (var cnn = _db.Connection)
            {
                using (Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand(
                    "select data_value.* from data.datavalue_derived"
                    + " inner join data.data_value on data_value.id = parent_data_value_id"
                    + " where derived_data_value_id = " + derivedId, cnn))
                {
                    using (Npgsql.NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ret.Add(Parse(rdr));
                        }
                        return ret;
                    }

                }
            }
        }

        public Dictionary<DateTime, List<DataValue>> SelectDataForDerived(int srcVariable, int unitsTime, DateTime dateS, DateTime dateF, List<byte> flagAQC,
            int siteId, int offsetTypeId, double offsetValue, int methodSetId, int sourceSetId)
        {
            Dictionary<DateTime, List<DataValue>> ret = new Dictionary<DateTime, List<DataValue>>();
            using (var cnn = _db.Connection)
            {
                using (Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand("select * from datap.fn_data_values_select(:variableIdSrc,:dateS,:dateF,:flagAQC,:siteId,:unitTimeId,:offsetTypeID,:offsetValue,:methodSetId,:sourceSetId)", cnn))
                {
                    cmd.Parameters.AddWithValue(":variableIdSrc", srcVariable);
                    cmd.Parameters.AddWithValue(":dateS", dateS);
                    cmd.Parameters.AddWithValue(":dateF", dateF);
                    cmd.Parameters.AddWithValue(":flagAQC", flagAQC);
                    cmd.Parameters.AddWithValue(":siteId", siteId);
                    cmd.Parameters.AddWithValue(":unitTimeId", unitsTime);
                    cmd.Parameters.AddWithValue(":offsetTypeID", offsetTypeId);
                    cmd.Parameters.AddWithValue(":offsetValue", offsetValue);
                    cmd.Parameters.AddWithValue(":methodSetId", methodSetId);
                    cmd.Parameters.AddWithValue(":sourceSetId", sourceSetId);

                    using (Npgsql.NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            DataValue d = Parse(rdr);
                            DateTime date = Convert.ToDateTime(rdr["date_day"]);
                            List<DataValue> vec;
                            if (ret.TryGetValue(date, out vec))
                                vec.Add(d);
                            else
                                ret.Add(date, new List<DataValue>() { d });
                        }
                        return ret;
                    }
                }
            }
        }
        public Dictionary<DateTime, List<DataValue>> SelectDataForDerivedDay(int methodDerId, int siteId, int srcVariable,
            DateTime dateS, DateTime dateF, List<byte> flagAQC, int offsetTypeId, double offsetValue, int methodSetId, int sourceSetId)
        {
            Dictionary<DateTime, List<DataValue>> ret = new Dictionary<DateTime, List<DataValue>>();
            using (var cnn = _db.Connection)
            {
                using (Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand("select * from datap.fn_data_values_day_select" +
                    "(:methodDerId,:siteId,:variableIdSrc,:dateS,:dateF,:flagAQC,:offsetTypeID,:offsetValue,:methodSetId,:sourceSetId)", cnn))
                {
                    cmd.Parameters.AddWithValue(":methodDerId", methodDerId);
                    cmd.Parameters.AddWithValue(":siteId", new List<int> { siteId });
                    cmd.Parameters.AddWithValue(":variableIdSrc", srcVariable);
                    cmd.Parameters.AddWithValue(":dateS", dateS);
                    cmd.Parameters.AddWithValue(":dateF", dateF);
                    cmd.Parameters.AddWithValue(":flagAQC", flagAQC);
                    cmd.Parameters.AddWithValue(":offsetTypeID", offsetTypeId);
                    cmd.Parameters.AddWithValue(":offsetValue", offsetValue);
                    cmd.Parameters.AddWithValue(":methodSetId", methodSetId);
                    cmd.Parameters.AddWithValue(":sourceSetId", sourceSetId);

                    using (Npgsql.NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            DataValue d = Parse(rdr);
                            DateTime date = Convert.ToDateTime(rdr["date_day"]);
                            List<DataValue> vec;
                            if (ret.TryGetValue(date, out vec))
                                vec.Add(d);
                            else
                                ret.Add(date, new List<DataValue>() { d });
                        }
                        return ret;
                    }
                }
            }
        }

        /// <summary>
        /// Выборка синхронных данных для двух записей каталога за период времени.
        /// Для выборки данных каждой записи каталога используется функция data.select_data_value_b.
        /// </summary>
        /// <param name="dateSLOC"></param>
        /// <param name="dateFLOC"></param>
        /// <param name="catalogId1"></param>
        /// <param name="catalogId2"></param>
        /// <param name="flagAQC"></param>
        /// <returns></returns>
        public List<DataValue2> SelectDataValueB2(DateTime dateSLOC, DateTime dateFLOC, int catalogId1, int catalogId2, short[] flagAQC)
        {
            List<DataValue2> ret = new List<DataValue2>();
            using (var cnn = _db.Connection)
            {
                using (Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand("data.select_data_value_b2", cnn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("_date_s_loc", dateSLOC);
                    cmd.Parameters.AddWithValue("_date_f_loc", dateFLOC);
                    cmd.Parameters.AddWithValue("_catalog_id_1", catalogId1);
                    cmd.Parameters.AddWithValue("_catalog_id_2", catalogId2);
                    cmd.Parameters.AddWithValue("_flag_aqc", flagAQC);

                    using (Npgsql.NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ret.Add(new DataValue2()
                            {
                                CatalogId1 = catalogId1,
                                CatalogId2 = catalogId2,
                                DateLOC = (DateTime)rdr["date_loc"],
                                DateUTC = (DateTime)rdr["date_utc"],
                                Value1 = (double)rdr["value1"],
                                Value2 = (double)rdr["value2"],
                                FlagAQC = (byte)(Int16)rdr["flag_aqc"]
                            });
                        }
                        return ret;
                    }
                }
            }
        }
        /// <summary>
        /// Выборка поля - день начала тенденции
        /// </summary>
        /// <param name="day">Один из дней ряда тенденции</param>
        /// <param name="var">Переменная по которой определяется тенденция</param>
        /// <param name="site">Пункт</param>
        /// <returns></returns>
        public DataValue SelectTrendStart(DateTime day, int var, int site)
        {
            int trendSign = 0;
            DataFilter dataFilter = new DataFilter()
            {
                DateTimePeriod = new DateTimePeriod(day, null, DateTimePeriod.Type.Day, 0),
                CatalogFilter = new CatalogFilter()
                {
                    Methods = null,
                    Sources = null,
                    Sites = new List<int>(new int[] { site }),
                    Variables = new List<int>(new int[] { var }),
                    OffsetTypes = new List<int>(new int[] { (int)EnumOffsetType.NoOffset }),
                    OffsetValue = 0
                },
                FlagAQC = (byte)EnumFlagAQC.NoAQC,
                IsActualValueOnly = true,
                IsRefSiteData = false,
                IsSelectDeleted = false
            };
            //Выход, если нет данных за указанный день
            if (SelectA(dataFilter).Count == 0)
                return null;
            //Уменьшать период на месяц в поисках начала тенденции
            do
            {
                dataFilter.DateTimePeriod = new DateTimePeriod(
                    dataFilter.DateTimePeriod.DateS.Value.AddDays(-30),
                    dataFilter.DateTimePeriod.DateS.Value,
                    DateTimePeriod.Type.Period, 0
                );

                List<DataValue> data = SelectA(dataFilter).OrderByDescending(x => x.DateUTC).ToList();
                for (int i = 0; i < data.Count - 1; ++i)
                {
                    trendSign = trendSign == 0 ? ((data[0].Value - data[1].Value) > 0 ? 1 : -1) : trendSign;
                    if ((data[i].Value - data[i + 1].Value) * trendSign < 0)
                        return data[i];
                }
            }
            //Уменьшать начало периода пока год > 1800
            //TODO: вынести границу в константу (взять из БД)
            while (dataFilter.DateTimePeriod.DateS.Value.Year > 1900);
            return null;
        }
    }
}
