using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Runtime.Serialization;

namespace SOV.Amur.Data
{
    [DataContract]
    public class DataSource
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public int SiteId { get; set; }
        [DataMember]
        public int CodeFormId { get; set; }
        [DataMember]
        public DateTime DateUTC { get; set; }
        [DataMember]
        public DateTime DateUTCRecieve { get; set; }
        [DataMember]
        public DateTime DateLOCInsert { get; set; }
        [DataMember]
        public string Value { get; set; }
        string _hash = null;
        [DataMember]
        public string Hash
        {
            get
            {
                return _hash;
            }
            set
            {
                if (value != null)
                {
                    byte[] hash;
                    hash = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(value.Trim()));
                    _hash = Convert.ToBase64String(hash);
                }
                else
                {
                    _hash = null;
                }
            }

        }

        public DataSource
        (
            long Id,
            int SiteId,
            int CodeFormId,
            DateTime DateUTC,
            DateTime DateUTCRecieve,
            DateTime DateLOCInsert,
            string Value
        )
        {
            this.Id = Id;
            this.SiteId = SiteId;
            this.CodeFormId = CodeFormId;
            this.DateUTC = DateUTC;
            this.DateUTCRecieve = DateUTCRecieve;
            this.DateLOCInsert = DateLOCInsert;
            this.Value = Value;
            this.Hash = Value;
        }
    }
}
