using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using SOV.Common;

namespace SOV.Social
{
    [DataContract]
    public class LegalEntity : IdNames, IParent
    {
        /// <summary>
        /// Тип объекта права: o - организация, p - физ. лицо.
        /// </summary>
        [DataMember]
        public char Type { get; set; }
        [DataMember]
        public int? AddrId { get; set; }
        [DataMember]
        public string AddrAdd { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Phones { get; set; }
        [DataMember]
        public string WebSite { get; set; }
        [DataMember]
        public int? ParentId { get; set; }
        /// <summary>
        /// Организация (Org) или физ. лицо (Person).
        /// </summary>
        [DataMember]
        public object Entity { get; set; }

        /// <summary>
        /// Изображения
        /// </summary>
        [DataMember]
        public List<int> Imgs { get; set; }

        public string FullAddr { get; set; }

        public string Parent { get; set; }

        public string TypeName { get; set; }

        public static List<DicItem> Types = new List<DicItem>()
        {
            new DicItem(1, "Юр. Лицо", (object) 'o'),
            new DicItem(2, "Физ. Лицо", (object) 'p')
        };
        public LegalEntity() { }
        public LegalEntity(IdNames idn)
            : base(idn)
        {
        }

        public static string ToStringAddress(Addr addr, string addrAdd, AddrType addrType)
        {
            if (addr == null) return "";

            return
                addrType.NameShort +
                ". " + addr.Name +
                (string.IsNullOrEmpty(addrAdd) ? "" : ", " + addrAdd);
        }

        public int? GetParentId()
        {
            return ParentId;
        }

        public int GetId()
        {
            return Id;
        }

        public string GetName()
        {
            return ToString();
        }
    }
}
