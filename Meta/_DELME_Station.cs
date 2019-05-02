using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Common;
using System.Runtime.Serialization;

namespace SOV.Amur.Meta
{
    [DataContract]
    public class _DELME_Station : IdNames
    {
        [DataMember]
        public string Code { get { return base.NameRusShort; } set { base.NameRusShort = value; } }
        new private string NameRusShort { get; set; }
        [DataMember]
        public string Name { get { return base.NameRus; } set { base.NameRus = value; } }
        new private string NameRus { get; set; }
        //[DataMember]
        //public int Id { get; set; }
        //[DataMember]
        //public string Code { get; set; }
        //[DataMember]
        //public string Name { get; set; }
        //[DataMember]
        //public string NameEng { get; set; }
        [DataMember]
        public int TypeId { get; set; }
        /// <summary>
        /// Код региона.
        /// </summary>
        [DataMember]
        public int? AddrRegionId { get; set; }
        /// <summary>
        /// Код организации.
        /// </summary>
        [DataMember]
        public int? OrgId { get; set; }

        public _DELME_Station(int id, string code, string name, int typeId, string nameEng, int? addrRegionId, int? orgId)
        {
            Id = id;
            Code = code;
            Name = name;
            NameEng = nameEng;
            TypeId = typeId;
            AddrRegionId = addrRegionId;
            OrgId = orgId;
        }
        public _DELME_Station()
        {
            Id = -1;
            TypeId = -1;
        }
        public override string ToString()
        {
            {
                return Code + " " + Name + ", " + Id + ", " + TypeId;
            }
        }

        public static List<DicItem> ToList<T1>(List<_DELME_Station> stations, List<SiteType> stationTypes)
        {
            List<DicItem> ret = new List<DicItem>();
            if (stations != null && stationTypes != null)
                foreach (var station in stations)
                {
                    SiteType st = stationTypes.Find(x => x.Id == station.TypeId);
                    ret.Add(new DicItem(station.Id, station.Code + " " + station.Name + ", " + ((st == null) ? station.TypeId.ToString() : st.NameShort)));
                }
            return ret;

        }
        public static List<IdNames> ToListIdNames(List<_DELME_Station> stations)
        {
            List<IdNames> ret = new List<IdNames>();
            if (stations != null)
                foreach (var station in stations)
                {
                    IdNames idn = new IdNames()
                    {
                        Id = station.Id,
                        NameRusShort = station.Name,
                        NameRus = station.Code,
                        NameEng = station.NameEng,
                        NameEngShort = null,
                        RusEng = "rus"
                    };
                    ret.Add(idn);
                }
            return ret;
        }
    }
}
