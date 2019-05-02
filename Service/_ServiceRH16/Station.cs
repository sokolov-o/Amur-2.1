using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using FERHRI.Amur.Meta;

namespace FERHRI.Amur.ServiceRH16
{
    [DataContract]
    public class Station
    {
        /// <summary>
        /// Код пункта стандартных наблюдений БД Амур
        /// </summary>
        [DataMember]
        public int Id { get; set; }
        /// <summary>
        /// Код станции
        /// </summary>
        [DataMember]
        public string Code { get; set; }
        /// <summary>
        /// Название станции
        /// </summary>
        [DataMember]
        public string Name { get; set; }
        /// <summary>
        /// Час начала местных суток во времени ВСВ.
        /// </summary>
        public int UTCHourDayStart { get; set; }
        public int UTCOffset { get; set; }

        static internal List<Station> GetStations(List<Meta.Station> stationsAmur, List<Meta.Site> sitesAmur)
        {
            List<Station> ret = new List<Station>();
            foreach (var item in stationsAmur)
            {
                List<Meta.Site> sites = sitesAmur.FindAll(x => x.StationId == item.Id && x.SiteTypeId == 1);
                if (sites.Count != 1)
                    throw new Exception(string.Format("Для станции {0} найден не единственный пункт типа 1. Всего найдено {1} пунктов.", item, sites.Count));

                ret.Add(new Station() { Code = item.Code, Name = item.Name, Id = sites[0].Id });
            }
            return ret;
        }

        override public string ToString()
        {
            return Name + " " + Code + "(" + Id + ")";
        }
    }
}