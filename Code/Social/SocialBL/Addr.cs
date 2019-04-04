using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Common;
using System.Runtime.Serialization;

namespace SOV.Social
{
    [DataContract]
    public class Addr : IdClass
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string NameShort { get; set; }
        [DataMember]
        public int TypeId { get; set; }
        [DataMember]
        public int? ParentId { get; set; }
        /// <summary>
        /// Объекты наследники.
        /// null - не инициализировались,
        /// Count = 0 - нет детей.
        /// </summary>
        [DataMember]
        public List<Addr> Childs { get; set; }

        public int GetId()
        {
            return Id;
        }
        public Addr(int id, int typeId, string name, string nameShort, int? parentId)
        {
            Id = id;
            Name = name;
            NameShort = nameShort;
            TypeId = typeId;
            ParentId = parentId;
            Childs = null;
        }
        public Addr()
        {
        }
        public override string ToString()
        {
            {
                return Name;
            }
        }
        /// <summary>
        /// Выбрать наследников из указанного списка в дерево наследников, с рекурсией.
        /// </summary>
        /// <param name="ars">Список с детьми.</param>
        public void FillChilds(List<Addr> ars)
        {
            Childs = new List<Addr>();
            foreach (var arChild in ars.Where(x => x.ParentId == Id))
            {
                arChild.FillChilds(ars);
                Childs.Add(arChild);
            }
        }
        /// <summary>
        /// Выбрать наследников из указанного списка в дерево наследников, с рекурсией.
        /// </summary>
        /// <param name="childs">Список с детьми.</param>
        static public void FillChilds(List<Addr> parents, List<Addr> childs)
        {
            foreach (var parent in parents)
            {
                parent.FillChilds(childs);
            }
        }
    }
}
