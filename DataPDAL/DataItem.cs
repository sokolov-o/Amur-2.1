using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace FERHRI.Amur.DataP
{
    /// <summary>
    /// Элемент данных (значение) для аггрегирования.
    /// </summary>
    public class DataItem
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public DateTime Date { get; set; }
        /// <summary>
        /// Значения, определённым связанные с экземпляром элемента данных.
        /// Например, значения, по которым был получен экземпляр.
        /// </summary>
        public List<DataItem> DataItemsRelated { get; set; }
    }
}
