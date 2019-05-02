using Npgsql;
using System;

namespace SOV.UnitTest
{
    internal static class CommonFuncs
    {
        public static System.Diagnostics.StackFrame Frame()
        {
            return (new System.Diagnostics.StackFrame(0, true));
        }

        public static string GetDefaultConnectionString()
        {
            return Properties.Settings.Default.local_ConnectionString;
        }

        public static void SetDefaultConnectionString(string cnns)
        {
            Properties.Settings.Default["local_ConnectionString"] = cnns;
        }

        public static long TableCount(string tableName)
        {
            return (long)ExecSimpleQuery("select count(*) from " + tableName);
        }

        public static int TableMaxId(string tableName)
        {
            return (int)ExecSimpleQuery("select max(id) from " + tableName);
        }

        public static DateTime DbCurrDate()
        {
            return (DateTime)ExecSimpleQuery("Select now()::timestamp;");
        }

        public static object ExecSimpleQuery(string query)
        {
            using (NpgsqlConnection cnn = new NpgsqlConnection(GetDefaultConnectionString()))
            {
                cnn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, cnn))
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    return reader.FieldCount > 0 ? reader[0] : -1;
                }
            }
        }
    }
}
