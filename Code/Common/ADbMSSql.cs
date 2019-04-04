using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SOV.Common
{
    /// <summary>
    /// Доступ к БД через бибдиотеку MSSql
    /// </summary>
    public class ADbMSSql
    {
        public string ConnectionString { get; set; }

        public SqlConnection Connection
        {
            get
            {
                SqlConnection cnn = new SqlConnection(ConnectionString);
                cnn.Open();
                return cnn;
            }
        }
        public int DbListId { get; set; }
        public ADbMSSql(string cnns, int dbListId)
        {
            ConnectionString = cnns;
            DbListId = dbListId;
        }
        static public void AddParameter(SqlCommand cmd, string paramName, object value)
        {
            cmd.Parameters.Add(new SqlParameter(paramName, (value == null) ? DBNull.Value : value));
        }
        static public DateTime? GetValueDateTime(SqlDataReader rdr, string fieldName)
        {
            return rdr.IsDBNull(rdr.GetOrdinal(fieldName)) ? null : (DateTime?)(DateTime)rdr[fieldName];
        }
        static public int? GetValueInt(SqlDataReader rdr, string fieldName)
        {
            return rdr.IsDBNull(rdr.GetOrdinal(fieldName)) ? null : (int?)(int)rdr[fieldName];
        }

        static public string GetStringNullable(SqlDataReader rdr, string paramName)
        {
            int i = rdr.GetOrdinal(paramName);
            return rdr.IsDBNull(i) ? null : rdr[i].ToString();
        }
        static public int? GetIntNullable(SqlDataReader rdr, string paramName)
        {
            int i = rdr.GetOrdinal(paramName);
            return rdr.IsDBNull(i) ? null : (int?)(int)rdr[i];
        }
        static public string UpdateUser(string connectionString, User user)
        {
            Dictionary<string, string> dic = Common.StrVia.ToDictionary(connectionString);
            return "DATA SOURCE=" + dic["DATA SOURCE"] + ";"
                + "INITIAL CATALOG=" + dic["INITIAL CATALOG"] + ";"
                + "USER ID=" + user.Name + ";"
                + "PASSWORD=" + user.Password
            ;
        }
        static public User GetUser(string connectionString)
        {
            Dictionary<string, string> dic = Common.StrVia.ToDictionary(connectionString);
            return new User(dic["USER ID"], dic["PASSWORD"]);
        }
    }
}
