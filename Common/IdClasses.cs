using System;
using System.Runtime.Serialization;
using Npgsql;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SOV.Common
{
    [DataContract]
    public class IdNameParent : IdName
    {
        [DataMember]
        public int? ParentId { get; set; }
    }
    [DataContract]
    public class IdClass
    {
        [DataMember]
        public int Id { get; set; }

        public IdClass() { Id = NaNId; }

        public const int NaNId = int.MinValue;

        static public List<object> GetRoots(List<object> iparentItems)
        {
            List<object> ret = new List<object>();

            ret.AddRange(iparentItems.Where(x => !((IParent)x).GetParentId().HasValue));
            ret.AddRange(iparentItems.Where(x => ((IParent)x).GetParentId().HasValue && !iparentItems.Exists(y => ((IParent)y).GetId() == ((IParent)x).GetParentId())));

            return ret;
        }
    }

    [DataContract]
    public class IdName : IdClass
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public object Entity { get; set; }

        public IdName()
        {
            Name = null;
        }
        public IdName(IdName item)
        {
            Id = item.Id;
            Name = item.Name;
            Entity = item.Entity;
        }

        public override string ToString()
        {
            return Name;
        }
        public static Dictionary<string, object> GetFields(IdName item)
        {
            return new Dictionary<string, object>()
            {
                {"id", item.Id},
                {"name", item.Name}
            };
        }
        public static IdName ParseData(NpgsqlDataReader rdr)
        {
            return new IdName()
            {
                Id = (int)rdr["id"],
                Name = rdr["name"].ToString()
            };
        }
    }

    [DataContract]
    public class IdNameTree : IdName
    {
        public List<IdNameTree> Childs { get; set; }
    }


    [DataContract]
    public class IdNameRus : IdClass
    {
        [DataMember]
        public string NameRus { get; set; }
        [DataMember]
        public string NameRusShort { get; set; }

        public IdNameRus()
        {
            NameRus = null;
            NameRusShort = null;
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(NameRusShort) ? NameRus : NameRusShort;
        }

        public static object ParseData(NpgsqlDataReader rdr)
        {
            return new IdNameRus()
            {
                Id = (int)rdr["id"],
                NameRus = rdr["name_rus"].ToString(),
                NameRusShort = ADbNpgsql.GetValueString(rdr, "name_rus_short")
            };
        }
        static public Dictionary<string, object> GetFieldDictionary(IdNameRus item, bool withId)
        {
            Dictionary<string, object> ret = new Dictionary<string, object>()
            {
                { "name_rus", item.NameRus},
                { "name_rus_short", item.NameRusShort}
            };
            if (withId)
                ret.Add("id", item.Id);
            return ret;
        }
    }

    [DataContract]
    public class IdNames : IdNameRus
    {
        [DataMember]
        public string NameEng { get; set; }
        [DataMember]
        public string NameEngShort { get; set; }

        public IdNames()
        {
            NameEng = null;
            NameEngShort = null;
        }
        public IdNames Clone()
        {
            return new IdNames()
            {
                Id = -1,
                NameRus = "#" + NameRus,
                NameRusShort = "#" + NameRusShort,
                NameEng = "#" + NameEng,
                NameEngShort = "#" + NameEngShort,
                RusEng = "#" + RusEng
            };
        }
        public IdNames(IdNames idNames)
        {
            Id = idNames.Id;
            NameRus = idNames.NameRus;
            NameRusShort = idNames.NameRusShort;
            NameEng = idNames.NameEng;
            NameEngShort = idNames.NameEngShort;
        }
        public string RusEng = "rus"; // "eng" 

        public override string ToString()
        {
            return string.Format("{0}", RusEng == "rus"
                ? base.ToString()
                : string.IsNullOrEmpty(NameEngShort) ? NameEng : NameEngShort
            );
        }
        /// <summary>
        /// Существует вхождение заданной строки (переведённой в верхний регистр) хотя бы в одном имени экземпляра класса?
        /// </summary>
        /// <param name="value">Часть строки для поиска.</param>
        /// <returns>Существует или нет.</returns>
        public bool Exists(string value)
        {
            string value1 = value.ToUpper();

            if ((!string.IsNullOrEmpty(NameRusShort) && NameRusShort.ToUpper().IndexOf(value1) != -1)
            || (!string.IsNullOrEmpty(NameRus) && NameRus.ToUpper().IndexOf(value1) != -1)
            || (!string.IsNullOrEmpty(NameEngShort) && NameEngShort.ToUpper().IndexOf(value1) != -1)
            || (!string.IsNullOrEmpty(NameEng) && NameEng.ToUpper().IndexOf(value1) != -1)
            )
            {
                return true;
            }
            return false;
        }
        new public static object ParseData(NpgsqlDataReader rdr)
        {
            IdNameRus nr = (IdNameRus)IdNameRus.ParseData(rdr);

            return new IdNames()
            {
                Id = nr.Id,
                NameRus = nr.NameRus,
                NameRusShort = nr.NameRusShort,
                NameEng = rdr["name_eng"].ToString(),
                NameEngShort = rdr["name_eng_short"].ToString(),
            };
        }
        static public Dictionary<string, object> GetFieldDictionary(IdNames item, bool withId)
        {
            Dictionary<string, object> ret = GetFieldDictionary((IdNameRus)item, withId);
            ret.Add("name_eng", item.NameEng);
            ret.Add("name_eng_short", item.NameEngShort);

            return ret;
        }
    }

    [DataContract]
    public class IdNameRE : IdClass
    {
        [DataMember]
        public string NameRus { get; set; }
        [DataMember]
        public string NameEng { get; set; }

        public IdNameRE()
        {
            NameRus = null;
            NameEng = null;
        }
        public IdNames Clone()
        {
            return new IdNames()
            {
                Id = -1,
                NameRus = "#" + NameRus,
                NameEng = "#" + NameRus,
                RusEng = "#" + RusEng
            };
        }
        public IdNameRE(IdNames idNames)
        {
            Id = idNames.Id;
            NameRus = idNames.NameRus;
            NameEng = idNames.NameEngShort;
        }
        public IdNameRE(IdNameRE idNames)
        {
            Id = idNames.Id;
            NameRus = idNames.NameRus;
            NameEng = idNames.NameEng;
        }
        public string RusEng = "rus"; // "eng" 

        public string Name
        {
            get
            {
                return RusEng == "rus" ? NameRus : NameEng;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}", RusEng == "rus"
                ? base.ToString()
                : string.IsNullOrEmpty(NameEng) ? NameRus : NameEng
            );
        }
        static public Dictionary<string, object> GetFieldDictionary(IdNameRE item, bool withId)
        {
            Dictionary<string, object> ret = new Dictionary<string, object>()
            {
                { "name_rus", item.NameRus },
                { "name_eng", item.NameEng}
            };
            if (withId) ret.Add("id", item.Id);
            return ret;
        }
        public static object ParseData(NpgsqlDataReader rdr)
        {
            return new IdNameRE()
            {
                Id = (int)rdr["id"],
                NameRus = rdr["name_rus"].ToString(),
                NameEng = rdr["name_eng"].ToString()
            };
        }
    }
}
