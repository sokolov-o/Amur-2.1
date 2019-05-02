using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using SOV.Amur.Meta;

namespace SOV.Amur.Data
{
    [DataContract]
    public class DataValue2017
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public int CatalogId { get; set; }
        [DataMember]
        public double Value { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public int DateTypeId { get; set; }
        [DataMember]
        public byte FlagAQC { get; set; }
        [DataMember]
        public float UTCOffset { get; set; }

        public DataValue2017
        (
            long id,
            int catalogId,
            double value,
            int dateTypeId,
            DateTime date,
            byte flagAQC,
            float uTCOffset
        )
        {
            this.Id = id;
            this.CatalogId = catalogId;
            this.Value = value;
            this.DateTypeId = dateTypeId;
            this.FlagAQC = flagAQC;
            this.UTCOffset = uTCOffset;
            this.Date = date;
        }
        public override string ToString()
        {
            return Id + "\t;" + Value
                + ";" + DateTypeId
                + ";" + Date.ToString("yyyy-MM-dd HH:mm")
                + ";" + FlagAQC
                + ";" + CatalogId
                ;
        }


    }
}
