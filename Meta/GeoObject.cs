using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using SOV.Common;

namespace SOV.Amur.Meta
{
    [DataContract]
    public class GeoObject : IdName, IParent
    {
        [DataMember]
        public int GeoTypeId { get; set; }
        [DataMember]
        public int? FallIntoId { get; set; }
        [DataMember]
        public int OrderBy { get; set; }
        public double[,] Shape { get; set; }
        /// <summary>
        /// Нужен, т.к. WCF не использует n-мерные массивы.
        /// </summary>
        [DataMember]
        public double[][] Shape2D
        {
            get
            {
                double[][] ret = null;
                if (Shape != null)
                {
                    ret = new double[Shape.Length][];
                    for (int i = 0; i < Shape.Length; i++)
                    {
                        ret[i][0] = Shape[i, 0];
                        ret[i][1] = Shape[i, 1];
                    }
                }
                return ret;
            }
        }
        public GeoObject() { }
        public GeoObject(int id, int geoTypeId, string name, int? fallIntoId = null, int order = int.MinValue)
        {
            Id = id;
            GeoTypeId = geoTypeId;
            Name = name;
            FallIntoId = fallIntoId;
            OrderBy = order;
        }

        public static List<Common.DicItem> ToDicItemList(List<GeoObject> gos)
        {
            return gos.Select(x => x.ToDicItem()).ToList();
        }
        public static List<Common.DicItem> ToDicItemTree(List<GeoObject> gos)
        {
            List<Common.DicItem> ret = new List<Common.DicItem>();
            if (gos != null)
            {
                // GET & ADD PARENTS (null or if parent not exist in initial list)
                ret.AddRange(ToDicItemList(gos.Where(x => x.FallIntoId == null).ToList()));
                foreach (var go in gos.Where(x => x.FallIntoId != null))
                {
                    if (!gos.Exists(x => x.Id == go.FallIntoId))
                        ret.Add(go.ToDicItem());
                }
                ret = ret.OrderBy(x => x.Name).ToList();
                // ADD CHILDS TO PARENTS
                foreach (var go in ret)
                {
                    AddChilds(go, gos);
                }
            }
            return ret;
        }
        static void AddChilds(DicItem dici, List<GeoObject> gos)
        {
            dici.Childs = new List<DicItem>();
            dici.Childs.AddRange(ToDicItemList(gos.Where(x => x.FallIntoId == dici.Id).ToList()));
            dici.Childs = dici.Childs.OrderBy(x => int.Parse(x.NameShort)).ToList();
            foreach (var diciParent in dici.Childs)
            {
                AddChilds(diciParent, gos);
            }
        }
        public DicItem ToDicItem()
        {
            return new DicItem() { Id = Id, Name = Name, NameShort = this.OrderBy.ToString(), Entity = this };
        }

        public int? GetParentId()
        {
            return FallIntoId;
        }

        public int GetId()
        {
            return Id;
        }
        public override string ToString()
        {
            return Name;
        }
        public string GetName()
        {
            return ToString();
        }
    }
}
