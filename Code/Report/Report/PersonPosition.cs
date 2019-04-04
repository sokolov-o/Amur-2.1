using System;
using System.Runtime.Serialization;

namespace FERHRI.Amur.Report
{
    /// <summary>
    ///  Должность
    /// </summary>
    [DataContract]
    public class PersonPosition
    {
        public PersonPosition(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
        /// <summary>
        /// Идентифкиатор
        /// </summary>
        [DataMember]
        public int id { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        [DataMember]
        public string name { get; set; }
    }
}
