using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERHRI.Amur.Logger
{
    public enum LogEntryType
    {
        Info = 0,
        Attention = 1,
        Error = 2
    }
    public class LoggerManager
    {
        public LoggerManager()
        {
        }

        static public LoggerManager Instance { get { return new LoggerManager(); } }

        public void Write(string msg, LogEntryType logEntryType, bool duplicateConsole = true)
        {
            if (duplicateConsole)
            {
                ConsoleColor cfc = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleForegroundColor(logEntryType);
                Console.WriteLine(msg);
                Console.ForegroundColor = cfc;
            }
        }
        ConsoleColor ConsoleForegroundColor(LogEntryType let)
        {
            switch (let)
            {
                case LogEntryType.Attention: return ConsoleColor.DarkGray;
                case LogEntryType.Error: return ConsoleColor.Red;
                case LogEntryType.Info: return ConsoleColor.Gray;
                default:
                    return ConsoleColor.White;
            }
        }
        public void WriteError(string msg)
        {
            Write(msg, LogEntryType.Error);
        }
        public void WriteAttention(string msg)
        {
            Write(msg, LogEntryType.Attention);
        }
        public void WriteInfo(string msg)
        {
            Write(msg, LogEntryType.Attention);
        }
    }
}
