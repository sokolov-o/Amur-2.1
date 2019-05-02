using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Common
{
    /// <summary>
    /// Период даты-времени открытый с конца.
    /// </summary>
    [DataContract]
    public class DateSF
    {
        [DataMember]
        public DateTime DateS { get; set; }
        [DataMember]
        public DateTime? DateF { get; set; }

        public DateSF()
        {
            DateS = DateTime.MinValue;
            DateF = null;
        }
        public DateSF(DateSF dateSF)
        {
            DateS = dateSF.DateS;
            DateF = dateSF.DateF;
        }
        public DateSF(DateTime dateS, DateTime? dateF)
        {
            DateS = dateS;
            DateF = dateF;
        }

        static public DateSF ParseData(NpgsqlDataReader reader)
        {
            return new DateSF()
            {
                DateS = (DateTime)reader["date_s"],
                DateF = ADbNpgsql.GetValueDateTime(reader, "date_f")
            };
        }
        static public string GetSelectSql(string tableName, Dictionary<string, object> fields)
        {
            object value = fields["date_actual"];
            fields.Remove("date_actual");

            if (value == null)
                return "select * from " + tableName + QueryBuilder.Where(fields);

            string groupBy = string.Join(",", fields.Keys.ToArray());
            string sql = QueryBuilder.Where(fields);

            sql = "select " + groupBy + ", max(date_s) max_date_s from " + tableName +
                sql + (string.IsNullOrEmpty(sql) ? " where " : " and ") + "(date_s <= :date_actual and (date_f is null or date_f >= :date_actual))" +
                " group by " + groupBy;

            sql = "select * from " + tableName + " t0 inner join (" + sql + ") t1 on t0.date_s = t1.max_date_s";
            foreach (var item in fields.Keys)
            {
                sql += " and t0." + item + " = t1." + item;
            }
            fields.Add("date_actual", value);
            return sql;
        }
        static public Dictionary<string, object> GetFieldDictionary(DateSF item)
        {
            return new Dictionary<string, object>() { { "date_s", item.DateS }, { "date_f", item.DateF } };
        }
    }
}
