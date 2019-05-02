using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Common;
using System.Runtime.Serialization;
using SOV.Amur.Meta;

namespace SOV.Amur.Meta
{
    /// <summary>
    /// Для всех списков класса значение null означает (все).
    /// Значение Count = 0 для списка недопустимо.
    /// </summary>
    [DataContract]
    public partial class CatalogFilter
    {
        List<int> _Sites;
        [DataMember]
        public List<int> Sites
        {
            get
            {
                return Catalogs == null ? _Sites : Catalogs.Select(x => x.SiteId).ToList();
            }
            set { _Sites = (value != null && value.Count == 0 ? null : value); }
        }
        List<int> _Variables;
        [DataMember]
        public List<int> Variables
        {
            get
            {
                return Catalogs == null ? _Variables : Catalogs.Select(x => x.VariableId).ToList();
            }
            set { _Variables = value != null && value.Count == 0 ? null : value; }
        }
        List<int> _Methods;
        [DataMember]
        public List<int> Methods
        {
            get
            {
                return Catalogs == null ? _Methods : Catalogs.Select(x => x.MethodId).ToList();
            }
            set { _Methods = value != null && value.Count == 0 ? null : value; }
        }
        List<int> _Sources;
        [DataMember]
        public List<int> Sources
        {
            get
            {
                return Catalogs == null ? _Sources : Catalogs.Select(x => x.SourceId).ToList();
            }
            set { _Sources = value != null && value.Count == 0 ? null : value; }
        }
        List<int> _OffsetTypes;
        [DataMember]
        public List<int> OffsetTypes
        {
            get
            {
                return Catalogs == null ? _OffsetTypes : Catalogs.Select(x => x.OffsetTypeId).ToList();
            }
            set { _OffsetTypes = value != null && value.Count == 0 ? null : value; }
        }
        double? _OffsetValue = null;
        [DataMember]
        public double? OffsetValue
        {
            get
            {
                if (Catalogs == null)
                    return _OffsetValue;
                else
                {
                    if (Catalogs.Count == 0 || Catalogs.Exists(x => x.OffsetValue != Catalogs[0].OffsetValue))
                        throw new Exception("(Catalogs.Count == 0 || Catalogs.Exists(x => x.OffsetValue != Catalogs[0].OffsetValue))");
                    return Catalogs[0].OffsetValue;
                }
            }
            set
            {
                _OffsetValue = value;
            }
        }
        List<Catalog> _Catalogs;
        /// <summary>
        /// Альтернативное использование фильтра для записей каталога:
        /// Либо CatalogFilter, либо Catalogs должны быть null.
        /// Приоритет в использовании: Catalogs. 
        /// То есть, если Catalogs != null && Sites != null - используется Catalogs.
        /// </summary>
        [DataMember]
        public List<Catalog> Catalogs
        {
            get
            {
                return _Catalogs;
            }
            set
            {
                if (Catalogs != null && Catalogs.Count == 0)
                    throw new Exception("(Catalogs != null && Catalogs.Count == 0)");
                _Catalogs = value;
            }
        }

        public override string ToString()
        {
            return
                "SITEIDS=" + StrVia.ToString(Sites)
                + ";VARIABLEIDS=" + StrVia.ToString(Variables)
                + ";METHODIDS=" + StrVia.ToString(Methods)
                + ";SOURCEIDS=" + StrVia.ToString(Sources)
                + ";OFFSETTYPEIDS=" + StrVia.ToString(OffsetTypes)
                + ";OFFSETVALUE=" + ((OffsetValue.HasValue) ? OffsetValue.ToString() : "")
            ;
        }
        static public CatalogFilter Parse(string str)
        {
            try
            {
                Dictionary<string, string> dic = StrVia.ToDictionary(str);

                return
                    new CatalogFilter(
                        StrVia.ToListInt(dic["SITEIDS"]),
                        StrVia.ToListInt(dic["VARIABLEIDS"]),
                        StrVia.ToListInt(dic["METHODIDS"]),
                        StrVia.ToListInt(dic["SOURCEIDS"]),
                        StrVia.ToListInt(dic["OFFSETTYPEIDS"]),
                        string.IsNullOrEmpty(dic["OFFSETVALUE"].Trim()) ? null : (double?)double.Parse(dic["OFFSETVALUE"])
                    );

            }
            catch
            {
                return new CatalogFilter();
            }
        }

        public CatalogFilter
        (
            List<int> Sites,
            List<int> Variables,
            List<int> Methods,
            List<int> Sources,
            List<int> OffsetTypes,
            double? OffsetValue
        )
        {
            this.Sites = Sites;
            this.Variables = Variables;
            this.Methods = Methods;
            this.Sources = Sources;
            this.OffsetTypes = OffsetTypes;
            this.OffsetValue = OffsetValue;
        }
        public CatalogFilter()
        {
            this.Sites = new List<int>();
            this.Variables = new List<int>();
            this.Methods = new List<int>();
            this.Sources = new List<int>();
            this.OffsetTypes = new List<int>();
            this.OffsetValue = null;
        }
        public CatalogFilter(Catalog catalog)
        {
            Sites = new List<int>() { catalog.SiteId };
            Variables = new List<int>() { catalog.VariableId };
            Methods = new List<int>() { catalog.MethodId };
            Sources = new List<int>() { catalog.SourceId };
            OffsetTypes = new List<int>() { catalog.OffsetTypeId };
            OffsetValue = catalog.OffsetValue;
        }
    }
}
