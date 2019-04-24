using System;
using System.Runtime.InteropServices;

namespace SOV.GISMeteo
{
    /// <summary>
    /// Структура данных для даты и времени в ГИС-Метео
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal class DateTimeStruct
    {
        /// <summary>
        /// Год
        /// </summary>
        public int Year;
        /// <summary>
        /// Месяц
        /// </summary>
        public int Month;
        /// <summary>
        /// День
        /// </summary>
        public int Day;
        /// <summary>
        /// Часы
        /// </summary>
        public int Hour;
        /// <summary>
        /// Минуты
        /// </summary>
        public int Minute;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="dt">Дата и время</param>
        public DateTimeStruct(DateTime dt)
        {
            Year = dt.Year;
            Month = dt.Month;
            Day = dt.Day;
            Hour = dt.Hour;//(dt.Hour / 3)*3;
            Minute = 0;
        }
        public DateTimeStruct()
        {
        }

        /// <summary>
        /// Функция перевода в строку
        /// </summary>
        /// <returns>Строка</returns>
        public override string ToString()
        {
            string retValue = string.Empty;

            retValue = String.Format("{0}.{1}.{2} {3}:{4}:00", Day, Month, Year, Hour, Minute);

            return retValue;
        }

        public DateTime ToDateTime()
        {
            return new DateTime(this.Year, this.Month, this.Day, this.Hour, this.Minute, 0);
        }
    }
}
