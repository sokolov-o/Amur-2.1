using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FERHRI.Common;

namespace HBRMsSql_AmurPg
{
    /// <summary>
    /// csv файл с данными (из excel-запроса к "Амур.HBR.MsSql"). 
    /// Структура файла = структуре таблицы "Амур.HBR.MsSql".DataValues.
    /// </summary>
    public class FileHBRMsSql
    {
        static string[] COLUMN_NAMES = new string[]
        {
            "ValueID","DataValue","ValueAccuracy",
            "LocalDateTime","UTCOffset","DateTimeUTC",
            "SiteID","VariableID","OffsetValue","OffsetTypeID","ValueCharacterID","MethodID","SourceID",
            "QualityControlLevelID","Version","DataSourceID","CollectDate","CensorCode","VersionComment"
        };
        static string DATETIME_FORMAT = "dd.MM.yyyy HH:mm";

        static internal List<DataValue> Parse(string filePath)
        {
            System.IO.StreamReader sr = null;

            try
            {
                // PREPARE

                FileInfo fileInfo = new FileInfo(filePath);
                Console.WriteLine(fileInfo.Name);

                string line;
                string[] cells;
                char splitter = ';';
                List<DataValue> ret = new List<DataValue>();

                sr = new System.IO.StreamReader(filePath, Encoding.GetEncoding("windows-1251"));

                // READ HEADER

                line = sr.ReadLine();
                string[] columns = line.Split(splitter);
                for (int i = 0; i < COLUMN_NAMES.Length; i++)
                {
                    if (COLUMN_NAMES[i] != columns[i])
                        throw new Exception("Имена столбцов в файле не совпадают с именами в коде class FileHBRMsSql.");
                }

                // READ DATA

                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine().Trim();
                    if (string.IsNullOrEmpty(line)) continue;
                    cells = line.Split(splitter);
                    DataValue dv = new DataValue();

                    // PARSE DATE & TIME
                    if (!FERHRI.Common.DateTimeProcess.TryParse(cells[3], DATETIME_FORMAT, out dv.LocalDateTime))
                    {
                        throw new Exception("Не распознана дата LOC.\n" + line);
                    }
                    dv.UTCOffset = double.Parse(cells[4]);
                    if (!FERHRI.Common.DateTimeProcess.TryParse(cells[5], DATETIME_FORMAT, out dv.DateTimeUTC))
                    {
                        throw new Exception("Не распознана дата UTC.\n" + line);
                    }
                    if (dv.DateTimeUTC != dv.LocalDateTime.AddHours(-dv.UTCOffset))
                        throw new Exception("Несоответствие LOC/UTC & utcOffset.\n" + line);

                    // PARSE OTHER
                    dv.ValueID = int.Parse(cells[0]);
                    dv.Value = double.Parse(cells[1]);
                    dv.ValueAccuracy = (string.IsNullOrEmpty(cells[2]) ? null : (int?)int.Parse(cells[2]));
                    dv.SiteID = int.Parse(cells[6]);
                    dv.VariableID = int.Parse(cells[7]);
                    dv.OffsetValue = (string.IsNullOrEmpty(cells[8]) ? null : (double?)double.Parse(cells[8]));
                    dv.OffsetTypeID = (string.IsNullOrEmpty(cells[9]) ? null : (int?)int.Parse(cells[9]));
                    dv.ValueCharacterID = int.Parse(cells[10]);
                    dv.MethodID = int.Parse(cells[11]);
                    dv.SourceID = int.Parse(cells[12]);
                    dv.QualityControlLevelID = int.Parse(cells[13]);
                    dv.Version = int.Parse(cells[14]);
                    dv.DataSourceID = int.Parse(cells[15]);
                    dv.CollectDate = FERHRI.Common.DateTimeProcess.Parse(cells[16], DATETIME_FORMAT);
                    dv.CensorCode = cells[17];
                    dv.VersionComment = cells[18];

                    ret.Add(dv);
                }
                return ret;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
            finally
            {
                if (sr != null) sr.Close();
            }
        }
        internal class DataValue
        {
            public int ValueID;
            public double Value;
            public int? ValueAccuracy;
            public DateTime LocalDateTime;
            public double UTCOffset;
            public DateTime DateTimeUTC;
            public int SiteID;
            public int VariableID;
            public double? OffsetValue;
            public int? OffsetTypeID;
            public int ValueCharacterID;
            public int MethodID;
            public int SourceID;
            public int QualityControlLevelID;
            public int Version;
            public int DataSourceID;
            public DateTime CollectDate;
            public string CensorCode;
            public string VersionComment;

        }
    }
}
