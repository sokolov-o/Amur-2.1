using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Amur.Meta
{
    [DataContract]
    public class Unit
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Abbreviation { get; set; }
        [DataMember]
        public string NameEng { get; set; }
        [DataMember]
        public string AbbreviationEng { get; set; }
        [DataMember]
        public double? SIConvertion { get; set; }

        public Unit(int id, string type, string name, string abbr, string nameEng, string abbrEng, double? siConvertion)
        {
            Id = id;
            Type = type;
            Name = name;
            Abbreviation = abbr;
            NameEng = nameEng;
            AbbreviationEng = abbrEng;
            SIConvertion = siConvertion;
        }
        public Unit(int id)
        {
            Id = id;
            Type = null;
            Name = null;
            Abbreviation = null;
            NameEng = null;
            AbbreviationEng = null;
            SIConvertion = null;
        }

        public override string ToString()
        {
            return this.Name;
        }

        public static List<Common.DicItem> ToList<T1>(List<Unit> coll)
        {
            List<Common.DicItem> ret = new List<Common.DicItem>();
            if (coll != null)
                foreach (var item in coll)
                {
                    ret.Add(new Common.DicItem(item.Id, item.Name));
                }
            return ret;
        }
    }
}
