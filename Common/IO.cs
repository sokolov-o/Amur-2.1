using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Common
{
    public class IO
    {
        /// <summary>
        /// Найти первую строку файла содержащую в поле/столбце указанное значение часть.
        /// </summary>
        static public string[] FindLine(System.IO.StreamReader sr, int fieldIdx, string fieldValue, char fieldSplitter, bool throwIfNotFounded = true)
        {
            string[] fields;
            string line;

            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                fields = line.Split(fieldSplitter);
                if (fields[fieldIdx].Trim() == fieldValue)
                    return fields;
            }
            if (sr.EndOfStream)
                throw new Exception(string.Format("В файле не найдено поле [" + fieldValue + "]"));
            return null;
        }
        /// <summary>
        /// Найти первую строку файла содержащую указанную часть.
        /// </summary>
        static public string FindLine(System.IO.StreamReader sr, string stringPart, bool throwIfNotFounded = true)
        {
            string line;

            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                if (line.IndexOf(stringPart) >= 0)
                    return line;
            }
            if (sr.EndOfStream)
                throw new Exception(string.Format("В файле не найдено строка, содержащая [" + stringPart + "]"));
            return null;
        }
        static public void SkipLines(System.IO.StreamReader sr, int lineQ)
        {
            while (lineQ-- > 0)
            {
                sr.ReadLine();
            }
        }
    }
}
