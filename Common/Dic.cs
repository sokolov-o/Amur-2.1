using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SOV.Common
{
    /// <summary>
    /// Словарь: id, name, nameShort, Description
    /// </summary>
    [DataContract]
    public class DicItem : IdName
    {
        //[DataMember]
        //public int Id { get; set; }
        //[DataMember]
        //public string Name { get; set; }
        [DataMember]
        public string NameShort { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public bool IsSelected { get; set; }
        [DataMember]
        public Type Type { get; set; }
        [DataMember]
        public List<DicItem> Childs { get; set; }
        [DataMember]
        public DicItem ParentDicItem { get; set; }
        //[DataMember]
        //public object Entity { get; set; }

        public DicItem()
        {
            Id = -1;
            Name = null;
            NameShort = null;
            Description = null;
            IsSelected = false;

            Childs = new List<DicItem>();
        }
        public DicItem(int id, string name, string nameShort = null, string description = null, bool isSelected = false)
        {
            Id = id;
            Name = name;
            NameShort = nameShort;
            Description = description;
            IsSelected = isSelected;

            Childs = new List<DicItem>();
        }
        public DicItem(int id, string name, object entity)
        {
            Id = id;
            Name = name;
            NameShort = null;
            Description = null;
            IsSelected = false;
            Entity = entity;

            Childs = new List<DicItem>();
        }
        override public string ToString()
        {
            return Name;
        }
        public string ToStringBig()
        {
            return Name + ((NameShort == null) ? "" : "," + NameShort + " (" + Id + ")");
        }
        public Dictionary<int, string> ToDictionary(List<DicItem> list)
        {
            Dictionary<int, string> ret = new Dictionary<int, string>();
            foreach (DicItem item in list)
            {
                ret.Add(item.Id, item.Name);
            }
            return ret;
        }
        static public DicItem ToDicItem(IdNames idname)
        {
            return new DicItem() { Id = idname.Id, Name = idname.ToString() };
        }
        static public List<DicItem> ToDicItemList(List<IdNames> idnames)
        {
            List<DicItem> ret = new List<DicItem>();
            foreach (var item in idnames)
            {
                ret.Add(ToDicItem(item));
            }
            return ret;
        }
        /// <summary>
        /// Уровень словаря от 0.
        /// </summary>
        public int Level
        {
            get
            {
                int i = 0;
                DicItem dici = this;
                while (dici.ParentDicItem != null)
                {
                    dici = dici.ParentDicItem;
                    i++;
                }
                return i;
            }
        }
        public string Key
        {
            get
            {
                return Id + "+" + GetType().ToString();
            }
        }
    }
}
