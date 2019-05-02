using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using SOV.Common;
using SOV.Social;

namespace SOV.Amur.Reports
{
    /// <summary>
    ///  Изображения отчетов
    /// </summary>
    [DataContract]
    public class Org
    {
        public Org(int reportId, int orgId, string param, int? imgId)
        {
            this.OrgId = orgId;
            this.Param = param;
            this.ReportId = reportId;
            this.ImgId = imgId;
        }
        /// <summary>
        /// Идентифкиатор отчета в таблице report.report
        /// </summary>
        [DataMember]
        public int ReportId { get; set; }
        /// <summary>
        /// Идентифкиатор организации в таблице social.legal_entity
        /// </summary>
        [DataMember]
        public int OrgId { get; set; }
        /// <summary>
        /// Текстовые параметры отчета
        /// </summary>
        [DataMember]
        public string Param { get; set; }
        /// <summary>
        /// Идентификатор изображения в таблице social.image
        /// </summary>
        [DataMember]
        public int? ImgId { get; set; }

        public byte[] Img { get; set; }

        public string ReportView { get; set; }

        public string OrgView { get; set; }

        //return ImgId.HasValue ? Social.DataManager.GetInstance().ImageRepository.Select(ImgId.Value).Img : null;

        public override string ToString()
        {
            return "";
        }

        /// <summary>
        /// Получить список полей таблицы для отображения в Grid. 
        /// </summary>
        public static List<TableField> ViewTableFields()
        {
            return new List<TableField>()
            {
                new ComboBoxTableField("Тип отчета", "report_id", "ReportId", "ReportView") {LockedUpdate = true},
                new ComboBoxTableField("Организация", "org_id", "OrgId", "OrgView") {LockedUpdate = true},
                new TextTableField("Параметры", "param", "Param"),
                new ImageGalleryTableField("Изображение", "img_id", "ImgId", "Img")
            };
        }
    }
}
