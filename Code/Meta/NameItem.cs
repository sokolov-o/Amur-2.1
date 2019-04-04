using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using SOV.Common;

namespace SOV.Amur.Meta
{
    /// <summary>
    /// Наименование на определённом языке и соответствующего типа.
    /// </summary>
    [DataContract]
    public class NameItem : IdName
    {
            //[DataMember]
            //public int Id { get; set; }
            //[DataMember]
            //public string Name { get; set; }
        [DataMember]
        public int NameSetId { get; set; }
        /// <summary>
        /// Код языка. 1 - рус, 2 - англ.
        /// </summary>
        [DataMember]
        public int LangId { get; set; }
        /// <summary>
        /// Код типа наименования. 1 - полное, 2 - краткое, 3 - описание наименования.
        /// </summary>
        [DataMember]
        public int NameTypeId { get; set; }

        /// <summary>
        /// Получить имя из набора имён names.
        /// </summary>
        /// <param name="names"></param>
        /// <param name="nameSetId"></param>
        /// <param name="langId"></param>
        /// <param name="nameTypeId"></param>
        /// <returns>Наименование или null</returns>
        static public string GetName(List<NameItem> names, int nameSetId, int langId, int nameTypeId)
        {
            NameItem ret = names.FirstOrDefault(x => x.NameSetId == nameSetId && x.LangId == langId && x.NameTypeId == nameTypeId);
            return ret == null
                ? null //"NONAME_" + nameSetId + "_" + langId + "_" + nameTypeId
                : ret.Name
                ;
        }
    }
}
