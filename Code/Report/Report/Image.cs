using System;
using System.IO;
using System.Runtime.Serialization;

namespace FERHRI.Amur.Report
{
    /// <summary>
    ///  Изображения отчетов
    /// </summary>
    [DataContract]
    public class Image
    {
        public Image(int id, Stream img)
        {
            this.id = id;
            this.img = img;
        }
        /// <summary>
        /// Идентифкиатор
        /// </summary>
        [DataMember]
        public int id { get; set; }
        /// <summary>
        /// Изображение
        /// </summary>
        [DataMember]
        public Stream img { get; set; }
    }
}
