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
    /// Файлы формата HBRK15: ежечасные прогнозы WRF в узлах станций.
    /// Передаются из ДВ УГМС с февраля 2016 г.(Романский С.).
    /// </summary>
    public class FileHBRK15
    {
        static internal object[/*dateUTCIni;Dictionary*/] Parse(string filePath, string fileSubType)//, FileType fileType)
        {
            try
            {
                //Dictionary<string, string> fileLogBefore = fileType.ReadLog();

                // SCAN DIR FILES

                //FileInfo fileInfo = new FileInfo(filePath);
                //if (fileInfo.Name == fileType.LogFileName)
                //    return null;
                //Console.Write(fileInfo.Name + "\t");

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

                //object[] ret;

                // GET FILE SUBTYPE

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
                //FilesConfig.KeepFile(fileInfo, "Arj");

                // SWITCH FILE SUBTYPE

                //ret = null;
                switch (fileSubType)
                {
                    case "BUREA":
                        return FileHBRK15_BUREA.Parse(filePath);
                    case "SGMO":
                        return FileHBRK15_SGMO.Parse(filePath);
                    case "LEVEL":
                        return FileHBRK15_LEVEL.Parse(filePath);
                    default:
                        throw new Exception("Неизвестный подтип файла для иморта: " + fileSubType);
                }
                //fileType.Log.Add(fileInfo.Name, fileInfo.LastWriteTime.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        //static object[/*DateTime, List<DataForecast>*/] ParseFilesBUREA(string file, Station[] stations, Site[] sites)
        //{
        //    object[] o = FileHBRK15_BUREA.Parse(file, stations, sites);

        //    DateTime dateIni = (DateTime)o[0];
        //    Dictionary<string/*stationCode;stationName*/, double[/*Ta,Td,R,Rf*/][/*lag hours [1...72]*/]> dataFile
        //        = (Dictionary<string, double[][]>)o[1];

        //    dataFile.Remove(dataFile.Keys.FirstOrDefault(x => x.ToUpper().IndexOf("БЫССА") >= 0));

        //    // CONVERT TO DATAFORECAST
        //    int i = 0;
        //    List<DataForecast> ret = null;
        //    foreach (var kvp in dataFile)
        //    {
        //        ret = Convert2DataForecast(dateIni, GetSiteId(kvp.Key, stations, sites), kvp.Value);
        //        ret = ret.FindAll(x => !double.IsNaN(x.Value));
        //    }
        //    return new object[/*DateTime, List<DataForecast>*/] { dateIni, ret };
        //}
    }
}
