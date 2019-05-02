using System;
using System.Runtime.Serialization;

namespace FERHRI.Amur.Report
{
    /// <summary>
    ///  Человек
    /// </summary>
    [DataContract]
    public class Person
    {
        public Person(int id, string name)
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
