using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SOV.Amur.Meta
{
    [DataContract]
    [Serializable]
    public class EntityAttrType
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        /// <summary>
        /// Коды типов сайтов для которых этот атрибут "обязательно желателен!". 
        /// </summary>
        [DataMember]
        public List<int> Mandatories { get; set; }

        public EntityAttrType(int id, string name, string mandatories)
        {
            Id = id;
            Name = name;
            Mandatories = SOV.Common.StrVia.ToListInt(mandatories);
        }
        public EntityAttrType(int id, string name, List<int> mandatories)
        {
            Id = id;
            Name = name;
            Mandatories = mandatories;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
