using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace FERHRI.Amur.ServiceRH16
{
    [DataContract]
    public class Variable
    {
        /// <summary>
        /// Код переменной БД Амур
        /// </summary>
        [DataMember]
        public int Id { get; set; }
        /// <summary>
        /// Название переменной
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        public int VariableTypeId { get; set; }
        public int TimeId { get; set; }
        public int DataTypeId { get; set; }

        public Variable(Meta.Variable var)
        {
            Id = var.Id;
            Name = var.Name;
            VariableTypeId = var.VariableTypeId;
            TimeId = var.TimeId;
            DataTypeId = var.DataTypeId;
            UnitId = var.UnitId;
        }
        static public List<Variable> ToList(List<Meta.Variable> vars)
        {
            List<Variable> ret = new List<Variable>();
            foreach (var item in vars)
            {
                ret.Add(new Variable(item));
            }
            return ret;
        }

        public int UnitId { get; set; }
    }
}