using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using SOV.Amur.Meta;

namespace SOV.Amur.Data
{
    [DataContract]
    public class DataValueCatalog
    {
        public DataValue DataValue { get; set; }
        public Catalog Catalog { get; set; }

        public DataValueCatalog
        (
            DataValue dataValue, Catalog catalog
        )
        {
            DataValue = new DataValue(dataValue.Id, dataValue.CatalogId, dataValue.Value, dataValue.DateLOC, dataValue.DateUTC, dataValue.FlagAQC, dataValue.UTCOffset);
            Catalog = new Catalog(catalog.Id, catalog.SiteId, catalog.VariableId, catalog.MethodId, catalog.SourceId, catalog.OffsetTypeId, catalog.OffsetValue);
        }
        public override string ToString()
        {
            return Catalog.ToString() + ";" + DataValue.ToString();
        }

        public static List<DataValueCatalog> GetList(List<Catalog> catalogList, List<DataValue> dataList)
        {
            Dictionary<int, Catalog> ctlDic = catalogList.ToDictionary(t => t.Id);
            List<DataValueCatalog> ret = new List<DataValueCatalog>();
            foreach (var dv in dataList)
            {
                Catalog ctlCur;
                if (ctlDic.TryGetValue(dv.CatalogId, out ctlCur))
                    ret.Add(new DataValueCatalog(dv, ctlCur));
            }
            return ret;
        }
    }
}
