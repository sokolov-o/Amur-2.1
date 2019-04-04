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
    /// Определение элемента категории данных.
    /// </summary>
    [DataContract]
    public class CategoryDefinition
    {
        [DataMember]
        public int CategoryId = -1;
        [DataMember]
        public int CategoryItemId = -1;
        [DataMember]
        public int Code { get; set; }
        [DataMember]
        public double? Value1 { get; set; }
        [DataMember]
        public double? Value2 { get; set; }
    }
    /// <summary>
    /// Класс используется для отображения в формах.
    /// </summary>
    [DataContract]
    public class CategoryItemLocalized
    {
        [DataMember]
        public CategoryDefinition CategoryDefinition { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public string ItemNameShort { get; set; }
        [DataMember]
        public string ItemDescription { get; set; }

        // Для отображения в формах, например, в DataGridView, нужны свойства
        [DataMember]
        public int Code { get { return CategoryDefinition.Code; } set { CategoryDefinition.Code = value; } }
        [DataMember]
        public double? Value1 { get { return CategoryDefinition.Value1; } set { CategoryDefinition.Value1 = value; } }
        [DataMember]
        public double? Value2 { get { return CategoryDefinition.Value2; } set { CategoryDefinition.Value2 = value; } }
    }
}
