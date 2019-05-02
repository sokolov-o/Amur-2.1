using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Amur.Sys
{
    public class EntityInstanceValue
    {
        public int AttrId { get; set; }
        public string EntityInstance { get; set; }
        public string Value { get; set; }

        public EntityInstanceValue()
        {
            Initialize(-1, null, null);
        }
        public EntityInstanceValue(int attrId, string entityInstance, string value)
        {
            Initialize(attrId, entityInstance, value);
        }
        void Initialize(int attrId, string entityInstance, string value)
        {
            AttrId = attrId;
            EntityInstance = entityInstance;
            Value = value;
        }
    }
    public class AttrValueCollection : List<EntityInstanceValue>
    {

        public AttrValueCollection()
        {
        }
        //public override void Add(AttrValue av)
        //{
        //    if (base.Exists(x => x.AttrId == av.AttrId && x.EntityInstance == av.EntityInstance))
        //        throw new Exception("Добавляемый атрибут уже присутствует в коллекции.");
        //    base.Add(av);
        //}
        /// <summary>
        /// Update or Insert if not exists.
        /// </summary>
        /// <param name="attrId"></param>
        /// <param name="value"></param>
        public void Update(int attrId, string entityInstance, string newValue)
        {
            EntityInstanceValue av = this.FirstOrDefault(x => x.AttrId == attrId && x.EntityInstance.ToUpper() == entityInstance.ToUpper());
            if (av != null)
                av.Value = newValue;
            else
                Add(new EntityInstanceValue(attrId, entityInstance, newValue));
        }
    }
}
