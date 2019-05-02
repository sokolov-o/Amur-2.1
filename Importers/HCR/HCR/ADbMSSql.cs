using System;
using System.Collections.Generic;
//using System.Collections.Generic;
using System.Data.SqlClient;
//using System.Linq;
//using System.Text;

namespace Viaware.Sakura.Db
{
    public abstract class ADbMSSql
    {
        public int? DbListId { get; set; }
        public string ConnectionString { get; set; }

        public ADbMSSql(string cnns)
        {
            this.cnn = new SqlConnection(cnns);
            this.cnn.Open();
            ConnectionString = cnns;
        }
        public ADbMSSql(string cnns, int dbListId)
        {
            this.cnn = new SqlConnection(cnns);
            this.cnn.Open();
            ConnectionString = cnns;

            DbListId = dbListId;
        }
        public string Name
        {
            get
            {
                ConnectionString2Db cnn = new ConnectionString2Db(ConnectionString2Db.PROVIDER_SQLCLIENT, ConnectionString);
                return cnn.initialCatalog;
            }
        }
        /// <summary>
        /// No connection!
        /// </summary>
        public ADbMSSql()
        {
        }
        public SqlConnection cnn { get; set; }
        public void Dispose(SqlCommand cmd, SqlDataReader rdr)
        {
            if (rdr != null)
            {
                rdr.Close(); rdr.Dispose();
            }
            cmd.Parameters.Clear();
            cmd.Dispose();
        }
        public SqlParameter GetParameter(string paramName, object value)
        {
            return new SqlParameter(paramName, (value == null) ? DBNull.Value : value);
        }
        /// <summary>
        /// Создать массив строк формата "sql in" из кодов (Id) коллекции.
        /// В каждой строке содержится не более sqlInMaxQ кодов.
        /// </summary>
        /// <param name="sqlInMaxQ">Кол. Id элементов коллекции, включаемых в одну строку. От этого зависит кол. djpdhfoftvs[ строк "sql in".</param>
        /// <returns></returns>
        static public List<string> ToStringSqlIn(int[] id, int sqlInMaxQ)
        {
            List<string> ret = new List<string>();

            for (int i = 0; i < id.Length; )
            {
                int k = (i + sqlInMaxQ > id.Length) ? id.Length : i + sqlInMaxQ;
                string inId = string.Empty;

                for (int j = i; j < k; j++)
                {
                    inId += id[j] + ",";
                }
                i = k;
                if (!string.IsNullOrEmpty(inId))
                {
                    ret.Add(inId.Substring(0, inId.Length - 1));
                }
            }
            return ret;
        }

    }
}
