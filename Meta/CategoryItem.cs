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
    /// Определение элемента категории данных.
    /// </summary>
    [DataContract]
    public class CategoryItem : IdClass
    {
        public CategoryItem()
        {
            CategorySetId = -1;
            CategoryItemNameSetId = -1;
            Code = 1;
            OrderBy = 1;
            Value1 = 0;
            Value2 = 0;
        }

        public CategoryItem(CategoryItem cd)
        {
            Id = cd.Id;
            CategorySetId = cd.CategorySetId;
            CategoryItemNameSetId = cd.CategoryItemNameSetId;
            Code = cd.Code;
            OrderBy = cd.OrderBy;
            Value1 = cd.Value1;
            Value2 = cd.Value2;
        }

        [DataMember]
        public int CategorySetId = -1;
        [DataMember]
        public int CategoryItemNameSetId = -1;
        [DataMember]
        public int Code { get; set; }
        [DataMember]
        public int OrderBy { get; set; }
        [DataMember]
        public double? Value1 { get; set; }
        [DataMember]
        public double? Value2 { get; set; }
    }
    /// <summary>
    /// Класс используется для отображения в формах.
    /// </summary>
    [DataContract]
    public class CategoryItemLocalized : CategoryItem
    {
        public int Language;
        public CategoryItemLocalized(CategoryItem cd, int langId, List<NameItem> nameItems) : base(cd)
        {
            Language = langId;
            Localize(nameItems);
        }
        public void Localize(List<NameItem> nameItems)
        {
            ItemName = ItemNameShort = ItemDescription = null;
            if (nameItems != null)
            {
                ItemName = NameItem.GetName(nameItems, CategoryItemNameSetId, Language, 1);
                ItemNameShort = NameItem.GetName(nameItems, CategoryItemNameSetId, Language, 2);
                ItemDescription = NameItem.GetName(nameItems, CategoryItemNameSetId, Language, 3);
            }
        }

        //[DataMember]
        //public CategoryDefinition CategoryDefinition { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public string ItemNameShort { get; set; }
        [DataMember]
        public string ItemDescription { get; set; }

        //// Для отображения в формах, например, в DataGridView, нужны свойства
        //[DataMember]
        //public int Code { get { return CategoryDefinition.Code; } set { CategoryDefinition.Code = value; } }
        //[DataMember]
        //public double? Value1 { get { return CategoryDefinition.Value1; } set { CategoryDefinition.Value1 = value; } }
        //[DataMember]
        //public double? Value2 { get { return CategoryDefinition.Value2; } set { CategoryDefinition.Value2 = value; } }
    }
}
