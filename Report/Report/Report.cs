using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace SOV.Amur.Reports
{
    /// <summary>
    ///  Отчет
    /// </summary>
    [DataContract]
    public class Report
    {
        public Report(int id, string name, string description)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
        }
        /// <summary>
        /// Идентифкиатор
        /// </summary>
        [DataMember]
        public int Id { get; set; }
        /// <summary>
        /// Название
        /// </summary>
        [DataMember]
        public string Name { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        [DataMember]
        public string Description { get; set; }
        /// <summary>
        /// Составное название 
        /// </summary>
        [DataMember]
        public string NameFull
        {
            get { return Name + ": \"" + Description + "\""; }
        }
    }
}
