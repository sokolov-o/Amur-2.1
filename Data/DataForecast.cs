using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace SOV.Amur.Data
{
    //[DataContract]
    //public class DataForecast0
    //{
    //    [DataMember]
    //    public int Id { get; set; }
    //    [DataMember]
    //    public int CatalogId { get; set; }
    //    [DataMember]
    //    public int TimeIdFcsLag { get; set; }
    //    [DataMember]
    //    public bool IsDateFcsUTC { get; set; }
    //    [DataMember]
    //    public List<DataForecast> DataForecasts { get; set; }

    //    public DataForecast0(int catalogId, int timeIdFcsLag, bool isDateFcsUTC, int id = -1)
    //    {
    //        CatalogId = catalogId;
    //        TimeIdFcsLag = timeIdFcsLag;
    //        IsDateFcsUTC = isDateFcsUTC;
    //        Id = id;
    //    }
    //}
    [DataContract]
    public class DataForecast
    {
        [DataMember]
        public int CatalogId { get; set; }
        [DataMember]
        public double Value { get; set; }
        [DataMember]
        public DateTime DateFcs { get; set; }
        [DataMember]
        public DateTime DateIni { get; set; }
        [DataMember]
        public DateTime DateInsert { get; set; }
        [DataMember]
        public double LagFcs { get; set; }

        public DataForecast
        (int catalogId, double lagFcs, DateTime dateFcs, DateTime dateIni, double value, DateTime dateInsert)
        {
            this.CatalogId = catalogId;
            this.Value = value;
            this.DateFcs = dateFcs;
            this.DateIni = dateIni;
            this.DateInsert = dateInsert;
            this.LagFcs = lagFcs;
        }

        public override string ToString()
        {
            return
                this.LagFcs.ToString("00.0") + ";" +
                this.Value + ";" +
                this.DateIni + ";" +
                this.DateFcs + ";" +
                this.DateInsert + ";" +
                this.CatalogId
            ;
        }
    }
}
