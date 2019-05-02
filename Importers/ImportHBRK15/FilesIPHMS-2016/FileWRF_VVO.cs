using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FERHRI.Common;
using FERHRI.Amur.Importer.HBRK15.ServiceReferenceAmur;
using System.IO;

namespace FERHRI.Amur.Importer
{
    /// <summary>
    /// Прогнозы WRF в узлах произвольных пунктов.
    /// Передаются из ДВНИГМИ с ~20 мая 2016 г.(Крохин В.В.) для
    /// обеспечения СГМО МШ ОМ.
    /// </summary>
    public class FileWRF_VVO
    {
        /// <summary>
        /// Соответствие строки, содержащей название пункта и переменной в файле - записи каталога данных БД Амур.
        /// </summary>
        static private Dictionary<string, int> _fileXAmurCatalog = new Dictionary<string, int>()
        {
            {"259130;P0", 42894},
            {"259130;T2", 42871},
            {"259130;UV", 42891},
            {"259130;DD", 42888},
            {"259130;GST", 58613},
            {"259130;VS", 42865 },
            {"259130;Os", 58614 },
            {"259130;RH", 58615},
            {"259130;D2", 58616},
            {"259130;NGO", 58617},

            {"DUKCHNS-1;P0", 42896},
            {"DUKCHNS-1;T2", 42873},
            {"DUKCHNS-1;UV", 42893},
            {"DUKCHNS-1;DD", 42890},
            {"DUKCHNS-1;GST", 58618},
            {"DUKCHNS-1;VS", 42867},
            {"DUKCHNS-1;Os", 58619},
            {"DUKCHNS-1;RH", 58620},
            {"DUKCHNS-1;D2", 58621},
            {"DUKCHNS-1;NGO", 58622},

            {"ULBERI-1;P0", 42895},
            {"ULBERI-1;T2", 42872},
            {"ULBERI-1;UV", 42892},
            {"ULBERI-1;DD", 42889},
            {"ULBERI-1;GST", 58623},
            {"ULBERI-1;VS", 42866},
            {"ULBERI-1;Os", 58624},
            {"ULBERI-1;RH", 58625},
            {"ULBERI-1;D2", 58626},
            {"ULBERI-1;NGO", 58627},

            {"25913PORT;P0", 58767},
            {"25913PORT;T2", 58764},
            {"25913PORT;UV", 58766},
            {"25913PORT;DD", 58765},
            {"25913PORT;GST", 58768},
            {"25913PORT;VS", 58763},
            {"25913PORT;Os", 58769},
            {"25913PORT;RH", 58770},
            {"25913PORT;D2", 58772},
            {"25913PORT;NGO", 58771},

            {"HYSY982;P0", 5921302},
            {"HYSY982;T2", 5921299},
            {"HYSY982;UV", 5921301},
            {"HYSY982;DD", 5921300},
            {"HYSY982;GST",5921308},
            {"HYSY982;VS", 5921303},
            {"HYSY982;Os", 5921306},
            {"HYSY982;RH", 5921307},
            {"HYSY982;D2", 5921304},
            {"HYSY982;NGO",5921305}
        };

        static internal object[/*DateTime, List<DataForecast>*/] Parse(string filePath, string fileSubType)//, FileType fileType)
        {
            System.IO.StreamReader sr = null;
            try
            {
                #region CHECK FILE
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
                //FilesConfig.KeepFile(fileInfo, "Arj");
                #endregion CHECK FILE

                sr = new System.IO.StreamReader(filePath, Encoding.GetEncoding("windows-1251")); ;
                string[] fields;
                char split = ';';
                //Dictionary<string, int> fileXAmur = _fileXAmurCatalog;

                // HEADER
                string line = sr.ReadLine();
                if (line != "Model: WRF-NMM-R15L43:sea-dv-15")
                    throw new Exception(string.Format("Файл {0}\n не является файлом формата [Model: WRF-NMM-R15L43:sea-dv-15]", filePath));

                // 12 ВСВ 19.04.2016
                line = IO.FindLine(sr, "Init Time:");
                line = line.Substring(11, 17);
                DateTime dateIni;
                if (!Common.DateTimeProcess.TryParse(line, "HH XXX dd.MM.yyyy", out dateIni))
                    throw new Exception("Не удалось распознать дату прогноза в строке: " + line);

                // ZZ & LAGS
                fields = IO.FindLine(sr, 0, "ZZ", split);
                int[] lags = new int[24];
                for (int i = 0; i < lags.Length; i++)
                {
                    lags[i] = int.Parse(fields[i + 1]);
                }

                // DATA BODY

                List<DataForecast> data = new List<DataForecast>();
                int iLine = 9;

                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    if (string.IsNullOrEmpty(line))
                        continue;
                    fields = line.Split(split);
                    iLine++;
                    string[] fields1 = fields[0].Trim().Split(split);

                    int catalogId;
                    if (!_fileXAmurCatalog.TryGetValue(fields[0].Trim() + split + fields[1].Trim(), out catalogId))
                        throw new Exception("В словаре отсутствует код записи каталога, соответствующей " + (fields[0].Trim() + split + fields[1].Trim()));
                    if(catalogId == 5921302)
                    {
                        Console.WriteLine("(catalogId == 5921302)");
                    }
                    for (int i = 0; i < lags.Length; i++)
                    {
                        if (fields[i + 2].Trim() != "")
                        {
                            data.Add(new DataForecast()
                            {
                                CatalogId = catalogId,
                                DateFcs = dateIni.AddHours(lags[i]),
                                LagFcs = lags[i],
                                Value = Common.StrVia.ParseDouble(fields[i + 2])
                            });
                        }
                    }
                }

                //fileType.Log.Add(fileInfo.Name, fileInfo.LastWriteTime.ToString());

                return new object[] { dateIni, data };
            }
            finally
            {
                if (sr != null) sr.Close();
            }
        }
    }
}
