using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Data.Common;
using Npgsql;
using System.IO;
using System.Xml.Linq;

namespace SOV.Common
{
    public class RuDbException : DbException
    {
        private static readonly Dictionary<string, string> errors = new Dictionary<string, string>();

        public RuDbException(DbException e) : base(e.Message, e)
        {
            string file = Properties.Settings.Default.ConfigFile;
            if (!File.Exists(file))
                return;
            try
            {
                using (var lStream = new StreamReader(file, System.Text.Encoding.UTF8))
                {
                    XElement lRoot = XElement.Load(lStream);
                    foreach (var elm in lRoot.Element("DbErrors").Elements())
                        errors.Add(elm.Name.ToString().Split('_')[1], elm.Attribute("text").Value);
                }

                string val = "";
                if (e.GetType() == typeof(PostgresException) && !errors.TryGetValue(((PostgresException)e).SqlState, out val) ||
                    e.GetType() == typeof(NpgsqlException) && !errors.TryGetValue(((NpgsqlException)e).ErrorCode.ToString(), out val) ||
                    e.GetType() == typeof(OdbcException) && !errors.TryGetValue(((OdbcException)e).Errors[0].SQLState, out val) ||
                    e.GetType() == typeof(SqlException) && !errors.TryGetValue(((SqlException)e).Errors[0].State.ToString(), out val)
                    )
                    return;
                RuMessage = "Ошибка: " + val;

            }
            catch (Exception innerEx)
            {
                RuMessage = "Ошибка: парсинга config.xml\n\n" + innerEx.ToString();
            }
        }

        public string RuMessage { get; private set; }
    }
}
