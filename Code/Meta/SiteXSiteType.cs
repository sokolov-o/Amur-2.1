using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace FERHRI.Amur.Meta
{
    /// <summary>
    /// Тип отношения между пунктами.
    /// </summary>
    [DataContract]
    public class SiteXSiteType
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }

        public SiteXSiteType(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
