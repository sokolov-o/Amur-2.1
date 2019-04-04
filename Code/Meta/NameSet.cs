using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using FERHRI.Common;

namespace FERHRI.Amur.Meta
{
    /// <summary>
    /// Набор имён на разных языках и разных типов.
    /// </summary>
    [DataContract]
    public class NameSet : IdNameParent, IParent
    {
        /// <summary>
        /// Список элементов данного набора: наименований на определённом языке и соответствующего типа.
        /// Если null - список не инициализирован.
        /// </summary>
        [DataMember]
        public List<NameItem> NameItems = null;

        public int GetId()
        {
            return Id;
        }

        public string GetName()
        {
            return Name;
        }

        public int? GetParentId()
        {
            return ParentId;
        }
    }
}
