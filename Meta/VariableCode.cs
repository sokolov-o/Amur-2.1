using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Amur.Meta
{
    /// <summary>
    /// Коды классификаторов для переменных: со значениями и наименованиями кодов.
    /// Например, ледовые явления.
    /// </summary>
    [DataContract]
    public class VariableCode
    {
        [DataMember]
        public int VariableId { get; set; }
        [DataMember]
        public int Code { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string NameShort { get; set; }
        [DataMember]
        public string Description { get; set; }

        public VariableCode()
        {
            VariableId = int.MinValue;
            Code = int.MinValue;
            Name = null;
            NameShort = null;
            Description = null;
        }
        public VariableCode
        (
            int VariableId,
            int Code,
            string Name,
            string NameShort,
            string Description
        )
        {
            this.VariableId = VariableId;
            this.Code = Code;
            this.Name = Name;
            this.NameShort = NameShort;
            this.Description = Description;
        }
        public override string ToString()
        {
            return VariableId + ";" + Code + ";" + Name;
        }
        /// <summary>
        /// Парсинг значения явления (ледового и др.) из базы данных: 6061, 6060 и т.п.
        /// </summary>
        /// <param name="value">Коды явлений, как значение из БД.</param>
        /// <returns></returns>
        public static List<int> Parse(double value)
        {
            List<int> ret = new List<int>();
            int i = int.Parse(value.ToString()), j;
            while (true)
            {
                j = (int)Math.Floor(i / 100.0);
                ret.Add(i - j * 100);

                if (j == 0) break;
                i = j;
            }
            return ret;
        }
    }
}
