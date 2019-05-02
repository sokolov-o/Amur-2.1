using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Common;

namespace SOV.Amur.Meta
{
    public class EntityGroup
    {
        /// <summary>
        /// Код группы.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Название группы сайтов.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Таблица сущности.
        /// </summary>
        public string TabName { get; set; }
        /// <summary>
        /// 
        /// Элементы группы сущностей. 
        /// Если null, значит не проинициализирована, 
        /// если Count == 0, значит пустая группа.
        /// 
        /// </summary>
        public List<object> Items { get; set; }

        public EntityGroup(int id, string name, string tabName)
        {
            Id = id;
            Name = name;
            TabName = tabName;
        }
        public override string ToString()
        {
            return Name;
        }
        //public static List<DicItem> ToListDicItem(List<EntityGroup> dc)
        //{
        //    List<DicItem> ret = new List<DicItem>();
        //    foreach (var item in dc)
        //    {
        //        ret.Add(ToDicItem(item));
        //    }
        //    return ret;
        //}
        //public static DicItem ToDicItem(EntityGroup sg)
        //{
        //    return new DicItem(sg.Id, sg.Name, sg.TabName);
        //}
    }
}
