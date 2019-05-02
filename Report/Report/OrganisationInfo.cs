using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using FERHRI.Amur.Meta;
using FERHRI.Social;

namespace FERHRI.Amur.Report
{
    /// <summary>
    ///  Изображения отчетов
    /// </summary>
    [DataContract]
    public class OrganisationInfo
    {
        public OrganisationInfo(int orgId, string text, string phone, int imgId, LegalEntity source)
        {
            this.orgId = orgId;
            this.text = text;
            this.phone = phone;
            this.source = source;
            this.imgId = imgId;
        }
        /// <summary>
        /// Идентифкиатор организации в таблице Meta.source
        /// </summary>
        [DataMember]
        public int orgId { get; set; }
        /// <summary>
        /// Текс заголовка отчета
        /// </summary>
        [DataMember]
        public string text { get; set; }
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
        /// <summary>
        /// Организация
        /// </summary>
        [DataMember]
        public LegalEntity source { get; set; }

        public override string ToString()
        {
            return source.NameRus;
        }
    }
}
