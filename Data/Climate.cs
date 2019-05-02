using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Amur.Data
{
    public class Climate
    {
        public int Id { get; set; }
        
        public int SiteId { get; set; }
        public int VariableId { get; set; }
        public int OffsetTypeId { get; set; }
        public double OffsetValue { get; set; }
        public int DataTypeId { get; set; }
        public int TimeId { get; set; }
        
        public int YearS { get; set; }
        public int YearF { get; set; }
        /// <summary>
        /// Словарь климатических значений: номер временного периода и соотв-е ему режимное значение.
        /// </summary>
        public Dictionary<Int16, double> Data { get; set; }

        public Climate(int id, int siteId, int variableId, int offsetTypeId, double offsetValue, 
            int dataTypeId, int timeId, int yearS, int yearF)
        {
            Id = id;
            SiteId = siteId;
            VariableId = variableId;
            OffsetTypeId = offsetTypeId;
            OffsetValue = offsetValue;

            DataTypeId = dataTypeId;
            TimeId = timeId;
            YearS = yearS;
            YearF = yearF;

            Data = new Dictionary<Int16, double>();
        }
        /// <summary>
        /// Осреднение в интервале timeNum. Например, осреднить декады в пределах месяца.
        /// return double[/*среднее;кол. значений в осреднении*/]
        /// </summary>
        /// <param name="timeNumS">Номер начала интервала осреднения.</param>
        /// <param name="timeNumF">Номер окончания интервала осреднения.</param>
        /// <returns>double[/*среднее;кол. значений в осреднении*/] or null</returns>
        public double[/*среднее;кол. значений в осреднении*/] Avg(int timeNumS, int timeNumF)
        {
            var values = Data.Where(x => x.Key >= timeNumS && x.Key <= timeNumF).Select(x => x.Value);
            return values.Count() > 0 ? new double[] { values.Average(), values.Count() } : null;
        }
    }
}
