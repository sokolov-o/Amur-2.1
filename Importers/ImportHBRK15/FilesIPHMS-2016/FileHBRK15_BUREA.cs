using FERHRI.Amur.Importer.HBRK15.ServiceReferenceAmur;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERHRI.Amur.Importer
{
    /// <summary>
    /// Файлы формата HBRK15: ежечасные прогнозы WRF в узлах станций Бурейского, Зейского водохранилищ.
    /// Передаются из ДВ УГМС с февраля 2016 г.(Романский С.).
    /// По договору с СКМ/Русгидро 2016 г.
    /// </summary>
    public class FileHBRK15_BUREA
    {
        static private object[/*dateUTCIni;Dictionary*/] Parse0(string filePath)
        {
            System.IO.StreamReader sr = null;
            string errMess = "Ошибка в строке {0} файла {1}.";
            Dictionary<string/*stationCode;stationName*/, double[/*Ta,Td,R,Rf*/][/*lag hours [1...72]*/]> ret =
                new Dictionary<string, double[][]>();

            int fieldsCount = 74;
            try
            {
                sr = new System.IO.StreamReader(filePath, Encoding.GetEncoding("windows-1251"));

                // HEADER
                string line = sr.ReadLine();
                if (line != "ЧАСОВОЙ ПРОГНОЗ ТЕМПЕРАТУРЫ, ТОЧКИ РОСЫ И ОСАДКОВ.")
                    throw new Exception(string.Format(errMess, 1, filePath));

                line = sr.ReadLine();
                string[] fields = line.Split(' ');
                if (fields[1] != "WRF-ARW:HBRK15." || fields[3] != "от:" || fields[5] != "ВСВ")
                    throw new Exception(string.Format(errMess, 2, filePath));
                int hour = int.Parse(fields[4]);
                fields = fields[6].Split('.');
                DateTime dateUTCIni = new DateTime(int.Parse(fields[2]), int.Parse(fields[1]), int.Parse(fields[0]), hour, 0, 0);

                line = sr.ReadLine();
                if (line != "Данные взяты из ближ. узла сетки прогноза.")
                    throw new Exception(string.Format(errMess, 3, filePath));

                line = sr.ReadLine();
                if (line != "Фаза осадков (Fs): 1 - жидкие; 2 - твердые; 3 - смешанные.")
                    throw new Exception(string.Format(errMess, 4, filePath));

                line = sr.ReadLine();
                fields = line.Split('|');
                if (fields[0].Trim() != "ЗАБЛ., ч.")
                    throw new Exception(string.Format(errMess, 5, filePath));
                if (fields.Length != fieldsCount)
                {
                    ConsoleColor cc = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ВНИМАНИЕ: Заблаговременность прогнозов " + ((fields.Length < fieldsCount) ? "<" : ">") + " 72 ч.");
                    Console.ForegroundColor = cc;

                    fieldsCount = fields.Length;
                }
                // DATA BODY
                int iLine = 5;
                while (!sr.EndOfStream)
                {
                    fields = sr.ReadLine().Split('|');
                    iLine++;
                    string[] fields1 = fields[0].Trim().Split(' ');

                    fields1[1] = fields1[1].Trim();
                    int iVar = (fields1[1] == "T2") ? 0 : (fields1[1] == "Td") ? 1 : (fields1[1] == "Os") ? 2 : (fields1[1] == "Fs") ? 3 : -1;
                    if (iVar < 0)
                        throw new Exception("Неизвестная переменная " + fields1[1] + " в строке " + iLine);

                    double[][] data = null;
                    string station = fields1[0].Trim() + ";" + fields[fieldsCount - 1].Trim();
                    if (!ret.TryGetValue(station, out data))
                    {
                        data = new double[4][];
                        for (int k = 0; k < 4; k++)
                        {
                            data[k] = new double[fieldsCount - 2];
                        }
                        ret.Add(station, data);
                    }
                    else
                    {
                        if (iVar == 0)
                            throw new Exception("Дубль станции " + fields1[0] + " в строке " + iLine);
                    }

                    for (int iLag = 1; iLag < fieldsCount - 1; iLag++)
                    {
                        if (string.IsNullOrEmpty(fields[iLag].Trim()))
                            data[iVar][iLag - 1] = double.NaN;
                        else
                        {
                            data[iVar][iLag - 1] = double.Parse(fields[iLag]);
                            //if (!double.TryParse(fields[iLag].Replace('.', ','), out data[iVar][iLag - 1]))
                            //    if (!double.TryParse(fields[iLag].Replace(',', '.'), out data[iVar][iLag - 1]))
                            //        throw new Exception("double.TryParse в строке " + iLine);
                        }
                    }
                }
                return new object[] { dateUTCIni, ret };
            }
            finally
            {
                if (sr != null) sr.Close();
            }
        }

        internal static object[/*DateTime, List<DataForecast>*/] Parse(string file)
        {
            object[] o = FileHBRK15_BUREA.Parse0(file);

            DateTime dateIni = (DateTime)o[0];
            Dictionary<string/*stationCode;stationName*/, double[/*Ta,Td,R,Rf*/][/*lag hours [1...72]*/]> dataFile
                = (Dictionary<string, double[][]>)o[1];

            dataFile.Remove(dataFile.Keys.FirstOrDefault(x => x.ToUpper().IndexOf("БЫССА") >= 0));

            // CONVERT TO DATAFORECAST
            List<DataForecast> ret = new List<DataForecast>();
            foreach (var kvp in dataFile)
            {
                ret.AddRange(Convert2DataForecast(dateIni, Program.GetSiteByStationIndex(kvp.Key.Split(';')[0].Trim(), 1).Id, kvp.Value));
            }
            ret = ret.FindAll(x => !double.IsNaN(x.Value));
            return new object[/*DateTime, List<DataForecast>*/] { dateIni, ret };
        }
        private static List<DataForecast> Convert2DataForecast(DateTime dateUTCFcsIni, int siteId, double[/*Ta, Td, R*/][/*1-72*/] data)
        {
            List<DataForecast> ret = new List<DataForecast>();
            int methodId = 100; // WRF-ARW:HBRK15
            int sourceId = 0;
            int offsetTypeId = 0;
            double offsetValue = 0;
            int[] variableIds = new int[] { 1012, 1013, 1014, 1023 };

            for (int i = 0; i < variableIds.Length; i++)
            {
                Catalog catalog = Program._svc.GetCatalog(Program._hSvc, siteId, variableIds[i], offsetTypeId, methodId, sourceId, offsetValue);

                if (catalog == null)
                {
                    catalog = Program._svc.SaveCatalog(Program._hSvc, new Catalog()
                    {
                        Id = -1,
                        SiteId = siteId,
                        VariableId = variableIds[i],
                        MethodId = methodId,
                        SourceId = sourceId,
                        OffsetTypeId = offsetTypeId,
                        OffsetValue = offsetValue
                    });

                    Console.WriteLine("Создана запись каталога данных: " + catalog.Id);
                }

                for (int j = 0; j < data[i].Length; j++)
                {
                    ret.Add(new DataForecast()
                    {
                        CatalogId = catalog.Id,
                        Value = data[i][j],
                        DateFcs = dateUTCFcsIni.AddHours(j + 1),
                        DateInsert = DateTime.Now,
                        LagFcs = j + 1
                    });
                }
            }
            return ret;
        }
    }
}
