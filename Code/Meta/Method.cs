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
    public class Method : IdName, IParent
    {
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int? ParentId { get; set; }
        [DataMember]
        public int? SourceLegalEntityId { get; set; }
        [DataMember]
        public short Order { get; set; }
        [DataMember]
        public Dictionary<string, string> MethodOutputStoreParameters { get; set; }

        //[DataMember]
        //public MethodForecast MethodForecast { get; set; }

        public Method() { }
        public Method(int id, string name, string description = null)
        {
            Id = id;
            Name = name;
            Description = description;
        }
        override public string ToString()
        {
            return Name + " : " + Id;
        }

        public static List<Common.DicItem> ToList<T1>(List<Method> items)
        {
            List<DicItem> ret = new List<DicItem>();
            if (items != null)
                foreach (var item in items)
                {
                    ret.Add(new DicItem(item.Id, item.ToString()));
                }
            return ret;
        }
        public string GetOutStoreParameter(string paramName, bool isThrowIfNotExists = false)
        {
            return GetMethodOutStoreParameter(MethodOutputStoreParameters, paramName, isThrowIfNotExists);
        }
        public static string GetMethodOutStoreParameter(Dictionary<string, string> methodOutStoreParameters, string paramName, bool isThrowIfNotExists = false)
        {
            string ret;
            if (!methodOutStoreParameters.TryGetValue(paramName.ToUpper(), out ret))
                throw new Exception(string.Format(
                    "Отсутствует поле <{1}> в списке параметров хранилища метода <{0}>.\n" +
                    "Укажите это поле для метода в таблице Amur.method.", methodOutStoreParameters, paramName));
            return ret;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pointMethod"></param>
        /// <returns></returns>
        public static object[/*Geo.EnumPointNearestType; Geo.EnumDistanceType*/] GetMethodPostprocessingParams(Dictionary<string, string> methodOutStoreParameters)
        {
            object[] ret = null;
            string s = GetMethodOutStoreParameter(methodOutStoreParameters, "parent_method_data_postprocessing", false);
            if (s != null)
            {
                Geo.EnumPointNearestType nearestType;
                switch (s)
                {
                    case "interpolation_linear": nearestType = Geo.EnumPointNearestType.Interpolate; break;
                    case "nearest_node": nearestType = Geo.EnumPointNearestType.Interpolate; break;
                    default: return null;
                        //throw new Exception("Неизвестное значение параметра [parent_method_data_postprocessing] = " + s + " в поле параметров метода.");
                }
                ret = new object[] { nearestType, Geo.EnumDistanceType.TheoremCos };
            }
            return ret;
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
            return Name;
        }
    }
}
