using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FERHRI.Amur.Report
{
    /// <summary>
    ///  Изображения отчетов
    /// </summary>
    [DataContract]
    public class Organisation
    {
        public Organisation(int id, string name, string fullName, string address, string email, string phone, int imgId)
        {
            this.id = id;
            this.name = name;
            this.fullName = fullName;
            this.address = address;
            this.email = email;
            this.phone = phone;
            this.imgId = imgId;
        }
        public Organisation(Dictionary<string, object> data)
        {
            this.id = (int)data["id"];
            this.name = data["name"].ToString();
            this.fullName = data["full_name"].ToString();
            this.address = data["address"].ToString();
            this.email = data["email"].ToString();
            this.phone = data["phone"].ToString();
            this.imgId = (int)data["img_id"];
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
        public string name{ get; set; }
        /// <summary>
        /// Полное имя
        /// </summary>
        [DataMember]
        public string fullName { get; set; }
        /// <summary>
        /// Адрес
        /// </summary>
        [DataMember]
        public string address { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        [DataMember]
        public string email { get; set; }
        /// <summary>
        /// Телефон
        /// </summary>
        [DataMember]
        public string phone { get; set; }
        /// <summary>
        /// Идентификатор изображения в таблице report.image
        /// </summary>
        [DataMember]
        public int imgId { get; set; }

        public override string ToString()
        {
            return name;
        }
    }
}
