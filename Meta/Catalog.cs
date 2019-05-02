using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace SOV.Amur.Meta
{
    [DataContract]
    public class Catalog
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int SiteId { get; set; }
        [DataMember]
        public int VariableId { get; set; }
        [DataMember]
        public int OffsetTypeId { get; set; }
        [DataMember]
        public double OffsetValue { get; set; }
        [DataMember]
        public int MethodId { get; set; }
        [DataMember]
        public int SourceId { get; set; }
        /// <summary>
        /// При значении null должен использоваться код каталога по умолчанию "НИЧЕГО" = 0.
        /// </summary>
        [DataMember]
        public int ParentId { get; set; }

        public Catalog
        (int Id, int SiteId, int VariableId, int MethodId, int SourceId, int OffsetTypeId, double OffsetValue, int parentId = 0)
        {
            this.Id = Id;
            this.SiteId = SiteId;
            this.VariableId = VariableId;
            this.OffsetTypeId = OffsetTypeId;
            this.OffsetValue = OffsetValue;
            this.MethodId = MethodId;
            this.SourceId = SourceId;
            this.ParentId = parentId;
        }
        public Catalog()
        {
            this.Id = -1;
            this.SiteId = -1;
            this.VariableId = -1;
            this.OffsetTypeId = -1;
            this.OffsetValue = -1;
            this.MethodId = -1;
            this.SourceId = -1;
        }

        public override string ToString()
        {
            return Id
                + ";" + SiteId
                + ";" + VariableId
                + ";" + OffsetTypeId
                + ";" + OffsetValue
                + ";" + MethodId
                + ";" + SourceId
                ;
        }
        /// <summary>
        /// Без учёта id.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(Catalog a, Catalog b)
        {
            return ((object)a == null || (object)b == null)
                ? false
                : a.MethodId == b.MethodId
                    && a.OffsetTypeId == b.OffsetTypeId
                    && a.OffsetValue == b.OffsetValue
                    && a.SiteId == b.SiteId
                    && a.SourceId == b.SourceId
                    && a.VariableId == b.VariableId
            ;
        }
        /// <summary>
        /// Без учёта id.
        /// </summary>
        public static bool operator !=(Catalog a, Catalog b)
        {
            return ((object)a != null && (object)b != null) ? !(a == b)
                : ((object)a == null && (object)b == null) ? true : false;
        }
        /// <summary>
        /// Найти все записи каталога из списка, удовлетворяющие заданному фильтру.
        /// </summary>
        /// <param name="ctls">Список записей каталога данных.</param>
        /// <param name="cf">Фильтр.</param>
        /// <returns></returns>
        public static List<Catalog> FindAll(List<Catalog> ctls, CatalogFilter cf)
        {
            return ctls.FindAll(x =>
                (cf.Sites == null || cf.Sites.Exists(y => y == x.SiteId))
                && (cf.Variables == null || cf.Variables.Exists(y => y == x.VariableId))
                && (cf.Methods == null || cf.Methods.Exists(y => y == x.MethodId))
                && (cf.Sources == null || cf.Sources.Exists(y => y == x.SourceId))
                && (cf.OffsetTypes == null || cf.OffsetTypes.Exists(y => y == x.OffsetTypeId))
                && (!cf.OffsetValue.HasValue || x.OffsetValue == cf.OffsetValue)
            );
        }
    }
}
