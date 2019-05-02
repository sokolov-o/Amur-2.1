using System.Collections.Generic;
using FERHRI.Common;
using System.Runtime.Serialization;

namespace FERHRI.Amur.Meta
{
    [DataContract]
    public class _DELME_Source
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameShort { get; set; }
        public string Description { get; set; }

        public string NameEng { get; set; }
        public string DescriptionEng { get; set; }
        public string Address { get; set; }
        public string AddressEng { get; set; }
        public string Emails { get; set; }
        public string WebSite { get; set; }

        public _DELME_Source(int id, string name, string nameShort, string description, string nameEng = null,
                    string descriptionEng = null, string address = null, string addressEng = null, 
                    string emails = null, string webSite = null)
        {
            Id = id;
            Name = name;
            Description = description;
            NameEng = nameEng;
            DescriptionEng = descriptionEng;
            Address = address;
            AddressEng = addressEng;
            Emails = emails;
            WebSite = webSite;
        }

        override public string ToString()
        {
            return Name + " : " + Id;
        }

        public static List<Common.DicItem> ToList<T1>(List<_DELME_Source> items)
        {
            List<DicItem> ret = new List<DicItem>();
            if (items != null)
                foreach (var item in items)
                {
                    ret.Add(new DicItem(item.Id, item.Name));
                }
            return ret;
        }
    }
}
