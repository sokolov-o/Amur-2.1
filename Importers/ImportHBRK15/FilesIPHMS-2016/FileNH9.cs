using FERHRI.Amur.Importer.HBRK15.ServiceReferenceAmur;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERHRI.Amur.Importer
{
    /// <summary>
    /// Файл *.csv с данными метео-наблюдений на буровой NH9, МагаШельф-2016.
    /// В шапке указаны catalog.id, поэтому файл достаточно общего формата.
    /// Исключение - корявое время: через / может быть указано местное и время гринвича.
    /// 
    /// Файл - Сводная метеосводка NH9.csv
    /// </summary>
    public class FileNH9
    {
        static internal object[/*List<DataValue>*/] Parse(string filePath,string fileSubType)//, FileType fileType)
        {
            System.IO.StreamReader sr = null;

            try
            {
                //Dictionary<string, string> fileLogBefore = fileType.ReadLog();

                //// SCAN DIR FILES

                //FileInfo fileInfo = new FileInfo(filePath);
                //Console.Write(fileInfo.Name + "\t");

                //// GET FILE SUBTYPE

                //string fileSubType = null;
                //foreach (var item in fileType.FileSubTypes)
                //{
                //    if (item.FileNames.FirstOrDefault(x => x == fileInfo.Name) != null)
                //    {
                //        fileSubType = item.Type;
                //        break;
                //    }
                //}
                //if (fileSubType == null)
                //{
                //    Console.WriteLine(" Пропущен...");
                //    return null;
                //}

                //string dateImport;
                //if (fileLogBefore.TryGetValue(fileInfo.Name, out dateImport))
                //{
                //    if (dateImport == fileInfo.LastWriteTime.ToString())
                //    {
                //        fileType.Log.Add(fileInfo.Name, dateImport);
                //        Console.WriteLine(" Импортирован ранее...");
                //        return null;
                //    }
                //}
                //if (!string.IsNullOrEmpty(fileType.SubDir2Keep))
                //    FilesConfig.KeepFile(fileInfo, fileType.SubDir2Keep);

                // SWITCH FILE SUBTYPE

                string line;
                string[] cells;
                char splitter = ';';
                DateTime dateLOC, dateUTC;
                List<DataValue> dataValues = new List<DataValue>();

                switch (fileSubType)
                {
                    case "MAGSHELF_2016_NH9":
                        #region PARSE MAGSHELF_2016_NH9

                        sr = new System.IO.StreamReader(filePath, Encoding.GetEncoding("windows-1251"));

                        line = sr.ReadLine();
                        string[] columns = line.Split(splitter);
                        int utcOffset = int.Parse(columns[1].Split(' ')[1]);

                        // READ FILE ROWS

                        while (!sr.EndOfStream)
                        {
                            line = sr.ReadLine().Trim();
                            if (string.IsNullOrEmpty(line)) continue;
                            cells = line.Split(splitter);

                            // PARSE DATE & TIME
                            if (!Common.DateTimeProcess.TryParse(cells[0], "dd.MM.yyyy", out dateLOC))
                            {
                                throw new Exception("Не распознана дата.\n" + line);
                            }
                            dateLOC = dateLOC + TimeSpan.Parse(cells[1].Split('/')[0]);
                            dateUTC = dateLOC.AddHours(-utcOffset);
                            if (dateUTC.TimeOfDay != TimeSpan.Parse(cells[1].Split('/')[1]))
                                throw new Exception("Несоответствие LOC/UTC & utcOffset.\n" + line);

                            // PARSE VALUES
                            int catalogId;
                            double value;
                            for (int i = 2; i < cells.Length; i++)
                            {
                                if (columns[i].IndexOf("ctl_id") < 0) continue;

                                catalogId = int.Parse(columns[i].Split('=')[1]);
                                if (!string.IsNullOrEmpty(cells[i]))
                                {
                                    // Особенности для давления: давление записано через / (гПа / мб). Берём гПа.
                                    if (cells[i].IndexOf("/") != -1)
                                    {
                                        cells[i] = cells[i].Split('/')[0];
                                    }
                                    // Особенности для скорость ветра
                                    if (cells[i].IndexOf("штиль") != -1)
                                    {
                                        cells[i] = "0";
                                    }
                                    if (double.TryParse(cells[i], out value))
                                    {
                                        dataValues.Add(new DataValue() { Id = -1, CatalogId = catalogId, DateLOC = dateLOC, DateUTC = dateUTC, FlagAQC = 0, UTCOffset = utcOffset, Value = value });
                                    }
                                }
                            }
                        }
                        #endregion PARSE MAGSHELF_2016_NH9
                        break;
                    default:
                        throw new Exception("Неизвестный подтип файла для иморта: " + fileSubType);
                }
                //fileType.Log.Add(fileInfo.Name, fileInfo.LastWriteTime.ToString());

                return new object[] { dataValues };
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
    }
}
