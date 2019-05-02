using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace SOV.Common
{
    /// <summary>
    /// Доступ к БД Postgre через бибдиотеку Npgsql
    /// NpgsqlConnection conn = new NpgsqlConnection("Server=127.0.0.1;Port=5432;User Id=joe;Password=secret;Database=joedata;");
    /// </summary>
    public class ADbNpgsql
    {
        public string ConnectionString { get; set; }

        public NpgsqlConnection Connection
        {
            get
            {
                NpgsqlConnection cnn = new NpgsqlConnection(ConnectionString);
                cnn.Open();
                return cnn;
            }
        }
        public ADbNpgsql(string cnns)
        {
            if (string.IsNullOrEmpty(cnns))
                throw new Exception("Строка соединения пустая.");

            ConnectionString = cnns;
            //_cnn = new NpgsqlConnection(cnns);
        }
        static public NpgsqlParameter GetParameter(string paramName, object value)
        {
            return new NpgsqlParameter(paramName, (value == null) ? DBNull.Value : value);
        }
        static public string ConnectionStringUpdateUser(string connectionString, User user)
        {
            Dictionary<string, string> dic = Common.StrVia.ToDictionary(connectionString);
            return "SERVER=" + dic["SERVER"] + ";"
                + "PORT=" + dic["PORT"] + ";"
                + "DATABASE=" + dic["DATABASE"] + ";"
                + "USER ID=" + user.Name + ";"
                + "PASSWORD=" + user.Password
            ;
        }
        static public string ConnectionStringUpdateDataBase(string connectionString, string dataBaseName)
        {
            Dictionary<string, string> dic = Common.StrVia.ToDictionary(connectionString);
            return "SERVER=" + dic["SERVER"] + ";"
                + "PORT=" + dic["PORT"] + ";"
                + "DATABASE=" + dataBaseName + ";"
                + "USER ID=" + dic["USER ID"] + ";"
                + "PASSWORD=" + dic["PASSWORD"]
            ;
        }
        static public User ConnectionStringGetUser(string connectionString)
        {
            Dictionary<string, string> dic = Common.StrVia.ToDictionary(connectionString);
            string userName, pwd;
            return new User(
                dic.TryGetValue("USER ID", out userName) ? userName : null,
                dic.TryGetValue("PASSWORD", out pwd) ? pwd : null
                );
            ;
        }
        static public string ConnectionStringWithoutUser(string connectionString)
        {
            Dictionary<string, string> dic = Common.StrVia.ToDictionary(connectionString);
            return "SERVER=" + dic["SERVER"] + ";"
                + "PORT=" + dic["PORT"] + ";"
                + "DATABASE=" + dic["DATABASE"] + ";"
            ;
        }
        static public User GetUser(string connectionString)
        {
            Dictionary<string, string> dic = Common.StrVia.ToDictionary(connectionString);
            return new User(dic["USER ID"], dic["PASSWORD"]);
        }
        public void TestConnection()
        {
            TestConnection(this.ConnectionString);
        }
        static public bool IsConnectionOK(string connectionString)
        {
            //try
            //{
                TestConnection(connectionString);
                return true;
            //}
            //catch
            //{
            //    return false;
            //}
        }
        static public void TestConnection(string connectionString)
        {
            //try
            //{
            using (NpgsqlConnection cnn = new NpgsqlConnection(connectionString))
            {
                //string logPath = @"c:/ferhri/log/rw";
                //System.IO.File.AppendAllText(logPath, string.Format("TestConnection {0}\t{1}\n\r", DateTime.Now, connectionString));

                cnn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand("select 1", cnn))
                {
                    cmd.ExecuteNonQuery();

                }
                cnn.Close();
            }

            //    return null;
            //}
            //catch (Exception ex)
            //{
            //    return ex;
            //}
        }
        static public char? GetValueChar(NpgsqlDataReader rdr, string fieldName)
        {
            return rdr.IsDBNull(rdr.GetOrdinal(fieldName)) ? null : (char?)((string)rdr[fieldName])[0];
        }
        static public int? GetValueInt(NpgsqlDataReader rdr, string fieldName)
        {
            return rdr.IsDBNull(rdr.GetOrdinal(fieldName)) ? null : (int?)rdr[fieldName];
        }
        static public object GetParameter(string fieldName, NpgsqlTypes.NpgsqlDbType npgsqlDbType, object value)
        {
            return new NpgsqlParameter(fieldName, npgsqlDbType) { IsNullable = true, Value = value == null ? value : DBNull.Value };
        }
        static public object GetValue(NpgsqlDataReader rdr, string fieldName)
        {
            return rdr.IsDBNull(rdr.GetOrdinal(fieldName)) ? null : rdr[fieldName];
        }
        static public string GetValueString(NpgsqlDataReader rdr, string fieldName)
        {
            return rdr.IsDBNull(rdr.GetOrdinal(fieldName)) ? null : rdr[fieldName].ToString();
        }
        static public double? GetValueDouble(NpgsqlDataReader rdr, string fieldName)
        {
            return rdr.IsDBNull(rdr.GetOrdinal(fieldName)) ? null : (double?)double.Parse(rdr[fieldName].ToString());
        }
        static public DateTime? GetValueDateTime(NpgsqlDataReader rdr, string fieldName)
        {
            return rdr.IsDBNull(rdr.GetOrdinal(fieldName)) ? null : (DateTime?)(DateTime)rdr[fieldName];
        }

        static public byte[] GetValueByteArr(NpgsqlDataReader rdr, string fieldName)
        {
            return rdr.IsDBNull(rdr.GetOrdinal(fieldName)) ? null : (byte[])rdr[fieldName];

        }
    }
}
