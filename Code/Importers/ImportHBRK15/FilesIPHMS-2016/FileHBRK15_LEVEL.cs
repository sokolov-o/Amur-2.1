using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FERHRI.Common;
using FERHRI.Amur.Importer.HBRK15.ServiceReferenceAmur;

namespace FERHRI.Amur.Importer
{
    /// <summary>
    /// Прогнозы WRF в узлах произвольных пунктов.
    /// Передаются из ДВ УГМС с 19 апреля 2016 г.(Романский С.) для
    /// обеспечения СГМО МШ ОМ.
    /// </summary>
    public class FileHBRK15_LEVEL
    {
        /// <summary>
        /// Соответствие строки, содержащей название пункта и переменной в файле - записи каталога данных БД Амур.
        /// </summary>
        static private int[] GetFileXAmurCatalog()
        {
            return new int[]
            {
                /*"PORT MAGADAN;TIDE", */42912,
                /*"PORT MAGADAN;SURGE", */42851,
                /*"PORT MAGADAN;LEVEL", */42919,

                /*"RU LISYANSKIY;TIDE", */42916,
                /*"RU LISYANSKIY;SURGE", */42914,
                /*"RU LISYANSKIY;LEVEL", */42918,

                /*"RU MAGADAN1;TIDE", */42917,
                /*"RU MAGADAN1;SURGE", */42915,
                /*"RU MAGADAN1;LEVEL", */42913
            };
        }
        static public object[/*DateTime, List<DataForecast>*/] Parse(string filePath)
        {
            System.IO.StreamReader sr = null;
            string[] fields;
            int[] fieldsLength = new int[] { 4, 4, 6, 6, 6, 6, 8, 6, 6, 7, 6, 6 };
            int[] fileXAmur = GetFileXAmurCatalog();
            int iLine = -1;

            try
            {
                sr = new System.IO.StreamReader(filePath, Encoding.GetEncoding("windows-1251"));

                // HEADER
                string line = sr.ReadLine();
                if (line.IndexOf("METEOROLOGICAL FORECASTING FROM") < 0)
                    throw new Exception(string.Format("Файл {0}\n не является файлом формата [Модель уровня Ю.В. Любицкого]", filePath));

                // 0 H 19. 4.2016
                line = line.Replace("METEOROLOGICAL FORECASTING FROM", "").Trim();
                DateTime dateIni;
                if (!Common.DateTimeProcess.TryParse(line, "HH XXX dd.MM.yyyy", out dateIni))
                    throw new Exception("Не удалось распознать дату прогноза от...");

                // DATA BODY
                IO.SkipLines(sr, 7);
                List<DataForecast> data = new List<DataForecast>();
                iLine = 8;

                int year = dateIni.Year;
                DateTime datePrev = DateTime.FromBinary(dateIni.ToBinary());

                // SCAN DATA LINES

                while (!sr.EndOfStream)
                {
                    // READ LINE
                    line = sr.ReadLine();
                    if (line == "THE END")
                        break;

                    fields = StrVia.Parse(line, fieldsLength);
                    iLine++;

                    // PARSE DATE
                    DateTime dateFcs = new DateTime
                        (
                        year,
                        int.Parse(fields[2]),
                        int.Parse(fields[1]),
                        int.Parse(fields[0]),
                        0, 0
                        );
                    if (datePrev.Month == 12 && dateIni.Month == 1)
                        dateIni.AddYears(1);
                    year = dateIni.Year;
                    datePrev = dateFcs;

                    // PARSE DATA
                    for (int i = 0; i < 9; i++)
                    {
                        if (fields[i + 3].Trim() != "")
                        {

                            data.Add(new DataForecast()
                            {
                                CatalogId = fileXAmur[i],
                                DateFcs = dateFcs,
                                LagFcs = (dateFcs - dateIni).TotalHours,
                                Value = double.Parse(fields[i + 3])
                            });
                        }
                    }
                }
                return new object[] { dateIni, data };
            }
            catch (Exception ex)
            {
                throw new Exception("File line " + iLine + "\n" + ex.Message);
            }
            finally
            {
                if (sr != null) sr.Close();
            }
        }
    }
}
