using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using SOV.Common;

namespace SOV.Amur.Meta
{
    /// <summary>
    /// Инструменты пункта наблюдений.
    /// </summary>
    [DataContract]
    public class SiteInstrument : DateSF
    {
        public SiteInstrument(DateSF dateSF) : base(dateSF) { }

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int SiteId { get; set; }
        [DataMember]
        public int InstrumentId { get; set; }
        [DataMember]
        public string LocationDescription { get; set; }
        [DataMember]
        public string Description { get; set; }
    }
}
