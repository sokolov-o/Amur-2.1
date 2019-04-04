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
    public class DataValue
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public int CatalogId { get; set; }
        [DataMember]
        public double Value { get; set; }
        [DataMember]
        public DateTime DateLOC { get; set; }
        [DataMember]
        public DateTime DateUTC { get; set; }
        [DataMember]
        public byte FlagAQC { get; set; }
        [DataMember]
        public float UTCOffset { get; set; }

        public DataValue
        (
            long Id,
            int catalogId,
            double Value,
            DateTime DateLOC,
            DateTime DateUTC,
            byte FlagAQC,
            float UTCOffset
        )
        {
            this.Id = Id;
            this.CatalogId = catalogId;
            this.Value = Value;
            this.DateLOC = DateLOC;
            this.FlagAQC = FlagAQC;
            this.UTCOffset = UTCOffset;

            this.DateUTC = DateUTC;// DateLOC.AddHours(-UTCOffset);
        }
        public override string ToString()
        {
            return Id + "\t;" + Value + ";" + DateLOC.ToString("yyyy-MM-dd HH:mm")
                + ";" + FlagAQC
                + ";" + CatalogId
                ;
        }

        public DateTime Date(EnumDateType type)
        {
            return type == EnumDateType.LOC ? DateLOC : DateUTC;
        }
    }

}
