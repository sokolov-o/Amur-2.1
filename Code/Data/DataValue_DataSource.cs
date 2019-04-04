using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Amur.Data
{
    public class DataValue_DataSource
    {
        public int DataValueId { get; set; }
        public int DataSourceId { get; set; }

        public DataValue_DataSource(int DataValueId, int DataSourceId)
        {
            this.DataValueId = DataValueId;
            this.DataSourceId = DataSourceId;
        }
    }
}
