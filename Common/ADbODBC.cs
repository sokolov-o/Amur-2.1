using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;

namespace SOV.Common
{
    public class _ADbODBC
    {
        public string ConnectionString { get; set; }
        System.Data.Odbc.OdbcConnection _cnn;
        public System.Data.Odbc.OdbcConnection Connection
        {
            get
            {
                if (_cnn.State == System.Data.ConnectionState.Closed)
                {
                    _cnn.Open();
                }
                return _cnn;
            }
        }
        public _ADbODBC(string cnns)
        {
            if (string.IsNullOrEmpty(cnns))
                throw new Exception("Строка соединения пустая.");

            ConnectionString = cnns;
            _cnn = new System.Data.Odbc.OdbcConnection(cnns);
        }
        public OdbcParameter GetParameter(string paramName, object value)
        {
            return new OdbcParameter(paramName, (value == null) ? DBNull.Value : value);
        }

    }
}
