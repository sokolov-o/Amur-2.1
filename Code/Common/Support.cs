using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SOV.Common
{
    public class Support
    {
        public static double[] Allocate(int dimLength, double value)
        {
            double[] ret = new double[dimLength];
            for (int i = 0; i < ret.Length; i++) ret[i] = value;
            return ret;
        }
        public static double[][] Allocate(int dimLength0, int dimLength1, double value)
        {
            double[][] ret = new double[dimLength0][];
            for (int i = 0; i < ret.Length; i++) ret[i] = Allocate(dimLength1, value);
            return ret;
        }
        public static double[][][] Allocate(int dimLength0, int dimLength1, int dimLength2, double value)
        {
            double[][][] ret = new double[dimLength0][][];
            for (int i = 0; i < ret.Length; i++) ret[i] = Allocate(dimLength1, dimLength2, value);
            return ret;
        }
        /// <summary>
        /// ВНИМАНИЕ! Порядок байт в массиве может быть изменён, после вызова данного метода.
        /// </summary>
        /// <param name="byteOrder">Порядок в b</param>
        public static double[] ByteFloat2Double(byte[] b, int byteOrder)
        {

            if (Math.IEEERemainder(b.Length, 4) != 0)
                throw new Exception("Math.IEEERemainder(b.Length, 4) != 0 : " + b.Length);

            if (System.BitConverter.IsLittleEndian && byteOrder != 0)
            {
                byte bb;
                for (int i = 0; i < b.Length; i += 4)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        bb = b[i + j];
                        b[i + j] = b[i + 3 - j];
                        b[i + 3 - j] = bb;
                    }
                }
            }
            double[] ret = new double[b.Length / 4];
            for (int i = 0; i < ret.Length; i++)
            {
                //ret[i] = (double)System.BitConverter.ToSingle(b, i * 4);
                ret[i] = double.Parse(string.Format("{0:f7}", System.BitConverter.ToSingle(b, i * 4)));
                // ret[i] = IBM2Float(b, i * 4);
            }
            return ret;
        }
        public static int Copy(byte[] dst, int dstStartIdx, byte[] src, int srcStartIdx, int srcQ2Copy)
        {
            for (int i = 0; i < srcQ2Copy; i++)
                dst[dstStartIdx + i] = src[srcStartIdx + i];
            return dstStartIdx + srcQ2Copy; // next dst idx for copy
        }
        /// <summary>
        /// Сначала mult, потом add.
        /// </summary>
        /// <param name="data">Массив для приведения к реальному значению.</param>
        /// <param name="mult">Множитель - первая операция.</param>
        /// <param name="add">Дополнение - вторая операция.</param>
        public static void ToReal(double[] data, double? mult, double? add)
        {
            if (add.HasValue || mult.HasValue)
            {
                for (int i = 0; i < data.Length; i++)
                {
                    if (mult.HasValue) data[i] *= (double)mult;
                    if (add.HasValue) data[i] += (double)add;
                }
            }
        }
        public static double[/*valueInterp,elemQ*/] InterpolateLine(double[] dist, double[] values)
        {
            double sumDist = 0;
            double sumValueW = 0;
            int elemQ = 0;
            for (int i = 0; i < dist.Length; i++)
            {
                if (dist[i] == 0)
                {
                    sumValueW = values[i];
                    sumDist = 1;
                    elemQ = 1;
                    break;
                }
                sumDist += (1 / dist[i]);
                sumValueW += (values[i] / dist[i]);
                elemQ++;
            }
            if (elemQ > 0)
            {
                return new double[] { sumValueW / sumDist, elemQ };
            }
            else { return new double[] { double.NaN, elemQ }; }
        }
        /// <summary>
        /// Добавить значение к каждому элементу массива.
        /// </summary>
        /// <param name="values">Массив</param>
        /// <param name="addValue">Слагаемое.</param>
        public static void Add(double[] values, double addValue)
        {
            for (int i = 0; i < values.Length; i++)
            {
                values[i] += addValue;
            }
        }
        /// <summary>
        /// Умножить каждое значение массива на множитель.
        /// </summary>
        /// <param name="values">Массив.</param>
        /// <param name="multiplyer">Множитель.</param>
        public static void Multiply(double[] values, double multiplyer)
        {
            for (int i = 0; i < values.Length; i++)
            {
                values[i] *= multiplyer;
            }
        }
        /// <summary>
        /// Создать или получить журнал событий Windows для приложений.
        /// Для создпния требуются права администратора при запуске приложения.
        /// </summary>
        /// <param name="logName">Имя журнала.</param>
        /// <param name="source">Источник (приложение) в журнале.</param>
        /// <returns></returns>
        public static EventLog GetEventLog(string logName, string source)
        {
            try
            {
                if (!EventLog.Exists(logName))
                {
                    Console.WriteLine("EventLog.CreateEventSource(" + source + ", " + logName + ")");
                    EventLog.CreateEventSource(source, logName);
                    Console.WriteLine(string.Format("Создан сервис событий источника {0} в журнале {1}." +
                        "\nЗакройте приложение и перезапустите его для использования источника.", source, logName));
                    return null;
                }
                return new EventLog(logName, ".", source);

            }
            catch (Exception ex)
            {
                throw new Exception("Возможно отсутствуют права для создания системного журнала приложения Windows.\nПерезапустите приложение с правами администратора.", ex);
            }
        }
        public static void WriteEvent(string EVENT_LOG_SOURCE, string msg, EventLogEntryType type = EventLogEntryType.Information,
            bool toConsole = true, bool toEventLog = true,
            char strikeOutChar = ' ', int strikeOutLength = 80)
        {
            // ADD
            string add = "";
            switch (type)
            {
                case EventLogEntryType.Error: add = "*** "; break;
                case EventLogEntryType.FailureAudit: add = "** "; break;
                case EventLogEntryType.Warning: add = "* "; break;
                default: break;
            }
            // STRIKE
            StringBuilder strike = new StringBuilder();
            strike = (strikeOutChar == ' ') ? strike : strike.Append(strikeOutChar, strikeOutLength).Append('\n');
            // WRITE
            if (toConsole) Console.WriteLine(strike.ToString() + add + msg);
            if (toEventLog) EventLog.WriteEntry(EVENT_LOG_SOURCE, msg, type);
        }

        

    }
}
