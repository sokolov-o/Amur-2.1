using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace SOV.Amur.Meta
{
    /// <summary>
    /// Значение атрибута сущности.
    /// </summary>
    [DataContract]
    [Serializable]
    public class EntityAttrValue
    {
        /// <summary>
        /// Наборы атрибутов значений на дату актуальности.
        /// </summary>
        public enum AttrSet
        {
            /// <summary>
            /// Существующие (не пустые) атрибуты.
            /// </summary>
            Exists = 0,
            /// <summary>
            /// Все = Существующие (не пустые) и Обязательные атрибуты.
            /// </summary>
            All = 1,
            /// <summary>
            /// Обязательные атрибуты.
            /// </summary>
            MandatoryOnly = 2,
            /// <summary>
            /// Не существующие (пустые) обязательные атрибуты.
            /// </summary>
            MandatoryNotExists = 3,
            /// <summary>
            /// Не обязательные атрибуты.
            /// </summary>
            NotMandatory = 4,
            /// <summary>
            /// Пустые атрибуты.
            /// </summary>
            Empty = 5
        }

        [DataMember]
        public int EntityId { get; set; }
        [DataMember]
        public int AttrTypeId { get; set; }
        [DataMember]
        public DateTime DateS { get; set; }
        [DataMember]
        public string Value { get; set; }

        public EntityAttrValue
        (
            int EntityId,
            int AttrTypeId,
            DateTime DateS,
            string Value
        )
        {
            this.EntityId = EntityId;
            this.AttrTypeId = AttrTypeId;
            this.DateS = DateS;
            this.Value = Value;
        }
        public static EntityAttrValue GetEntityAttrValue(List<EntityAttrValue> list, int entityId, int attrTypeId, DateTime actualdate)
        {
            if (list.Count == 0)
                return null;
            EntityAttrValue ret = null;
            DateTime? dateMax = null;
            foreach (var eav in list)
            {
                if (eav.EntityId == entityId && eav.AttrTypeId == attrTypeId && (DateTime)eav.DateS <= actualdate)
                {
                    bool isAct = false;
                    if (dateMax.HasValue)
                    {
                        if ((DateTime)dateMax < eav.DateS)
                            isAct = true;
                    }
                    else
                        isAct = true;
                    if (isAct)
                    {
                        ret = eav;
                        dateMax = eav.DateS;
                    }
                }
            }
            if (ret != null)
                return ret;
            else
                return null;
        }
    }
}
