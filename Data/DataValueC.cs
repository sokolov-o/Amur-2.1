using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Amur.Data
{
    public class DataValueC
    {
        public Meta.Catalog Catalog { get; set; }
        public DataValue DataValue { get; set; }

        private DataValueC() { }

        static public List<DataValueC> GetDataValueCList(List<Meta.Catalog> ctls, List<DataValue> dvs)
        {
            List<DataValueC> ret = new List<DataValueC>();
            foreach (DataValue dv in dvs)
            {
                ret.Add(new DataValueC() { Catalog = ctls.FirstOrDefault(x => x.Id == dv.CatalogId), DataValue = dv });
            }
            return ret;
        }
        public List<DataValueC> GetDataValueCList(Dictionary<Meta.Catalog, List<DataValue>> cdv)
        {
            List<DataValueC> ret = new List<DataValueC>();
            foreach (KeyValuePair<Meta.Catalog, List<DataValue>> kvp in cdv)
            {
                foreach (DataValue dv in kvp.Value)
                {
                    if (dv.CatalogId != kvp.Key.Id)
                        throw new Exception("(dv.CatalogId != kvp.Key.Id)");

                    ret.Add(new DataValueC() { Catalog = kvp.Key, DataValue = dv });
                }
            }
            return ret;
        }
    }
}
