using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FERHRI.Amur.Importer
{
    internal class LogFile
    {
        internal enum ActionType { Import = 1, Skipped = 2 }

        static public string DATE_FORMAT_YMDHMS = "yyyyMMdd HH:mm:ss";

        internal class Row
        {
            internal ActionType ActionType;
            internal DateTime ActionTime;
            internal string FilePath;
            internal DateTime FileLastWriteTime;

            internal string ToString(char splitter)
            {
                return "" + (int)ActionType + splitter +
                        ActionTime.ToString(DATE_FORMAT_YMDHMS) + splitter +
                        FilePath + splitter +
                        FileLastWriteTime.ToString(DATE_FORMAT_YMDHMS)
                ;
            }
        }

        internal string Dir { get; set; }
        internal string Name { get; set; }
        internal string Path { get { return string.IsNullOrEmpty(Name) ? null : Dir + "\\" + Name; } }

        internal LogFile(string dir, string fileName)
        {
            Dir = dir;
            Name = fileName;
        }

        internal List<LogFile.Row> ReadLog(ActionType? actionType)
        {
            StreamReader sr = null;
            List<LogFile.Row> ret = new List<LogFile.Row>(1000);
            string line;
            try
            {
                if (File.Exists(Path))
                {
                    sr = new StreamReader(Path);
                    while (!sr.EndOfStream)
                    {
                        line = sr.ReadLine();
                        if (string.IsNullOrEmpty(line))
                            continue;
                        string[] s = line.Split(Splitter);
                        ActionType at = (ActionType)int.Parse(s[0]);
                        //DateTime actionTime = new DateTime(Common.DateTimeProcess.Parse(s[1], DATE_FORMAT_YMDHMS).Ticks, DateTimeKind.Local);

                        if (!actionType.HasValue || actionType == at)
                        {
                            ret.Add(new Row
                            {
                                ActionType = at,
                                ActionTime = new DateTime(Common.DateTimeProcess.Parse(s[1], DATE_FORMAT_YMDHMS).Ticks, DateTimeKind.Local),
                                FilePath = s[2],
                                FileLastWriteTime = new DateTime(Common.DateTimeProcess.Parse(s[3], DATE_FORMAT_YMDHMS).Ticks, DateTimeKind.Local)
                            });
                        }
                    }
                }
                return ret;
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
        }
        internal void ClearLog()
        {
            if (File.Exists(Path))
                File.Delete(Path);
            File.Create(Path).Close();
        }
        internal char Splitter = ';';
        internal void WriteLog(List<LogFile.Row> rows)
        {
            StreamWriter sw = null;
            try
            {
                if (!File.Exists(Path))
                    File.Create(Path).Close();

                sw = new StreamWriter(Path, true);
                foreach (var row in rows)
                {
                    sw.WriteLine(row.ToString(Splitter));
                }
            }
            finally
            {
                if (sw != null)
                    sw.Close();
            }
        }
    }
}
