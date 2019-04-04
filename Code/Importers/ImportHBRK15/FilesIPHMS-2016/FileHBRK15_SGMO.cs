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
    public class FileHBRK15_SGMO
    {
        /// <summary>
        /// Соответствие строки, содержащей название пункта и переменной в файле - записи каталога данных БД Амур.
        /// </summary>
        static private Dictionary<string, int> GetFileXAmurCatalog()
        {
            Dictionary<string, int> ret = new Dictionary<string, int>();

            ret.Add("П МАГАДАН;P0", 42882);
            ret.Add("П МАГАДАН;T2", 42874);
            ret.Add("П МАГАДАН;UV", 42880);
            ret.Add("П МАГАДАН;DD", 42877);
            ret.Add("П МАГАДАН;GST", 44374);
            ret.Add("П МАГАДАН;VS", 42885);
            ret.Add("П МАГАДАН;Os", 43060);
            ret.Add("П МАГАДАН;RH", 43063);
            ret.Add("П МАГАДАН;D2", 43065);
            ret.Add("П МАГАДАН;NGO", 43058);

            ret.Add("ДУКЧИНС-1;P0", 42884);
            ret.Add("ДУКЧИНС-1;T2", 42876);
            ret.Add("ДУКЧИНС-1;UV", 42881);
            ret.Add("ДУКЧИНС-1;DD", 42879);
            ret.Add("ДУКЧИНС-1;GST", 44371);
            ret.Add("ДУКЧИНС-1;VS", 42887);
            ret.Add("ДУКЧИНС-1;Os", 43059);
            ret.Add("ДУКЧИНС-1;RH", 43062);
            ret.Add("ДУКЧИНС-1;D2", 43066);
            ret.Add("ДУКЧИНС-1;NGO", 43057);

            ret.Add("УЛЬБЕРИ-1;P0", 42883);
            ret.Add("УЛЬБЕРИ-1;T2", 42875);
            ret.Add("УЛЬБЕРИ-1;UV", 42878);
            ret.Add("УЛЬБЕРИ-1;DD", 42920);
            ret.Add("УЛЬБЕРИ-1;GST", 44373);
            ret.Add("УЛЬБЕРИ-1;VS", 42886);
            ret.Add("УЛЬБЕРИ-1;Os", 43064);
            ret.Add("УЛЬБЕРИ-1;RH", 43056);
            ret.Add("УЛЬБЕРИ-1;D2", 43067);
            ret.Add("УЛЬБЕРИ-1;NGO", 43061);

            ret.Add("HYSY982;P0", 5844257);
            ret.Add("HYSY982;T2", 5844265);
            ret.Add("HYSY982;UV", 5844264);
            ret.Add("HYSY982;DD", 5844259);
            ret.Add("HYSY982;GST", 5844263);
            ret.Add("HYSY982;VS", 5844256);
            ret.Add("HYSY982;Os", 5844261);
            ret.Add("HYSY982;RH", 5844262);
            ret.Add("HYSY982;D2", 5844258);
            ret.Add("HYSY982;NGO", 5844260);

            return ret;
        }
        static public object[/*DateTime, List<DataForecast>*/] Parse(string filePath)
        {
            System.IO.StreamReader sr = null;
            string[] fields;
            char split = ';';
            Dictionary<string, int> fileXAmur = GetFileXAmurCatalog();

            try
            {
                sr = new System.IO.StreamReader(filePath, Encoding.GetEncoding("windows-1251"));

                // HEADER
                string line = sr.ReadLine();
                if (line != "Модель WRF: Хаб-15")
                    throw new Exception(string.Format("Файл {0}\n не является файлом формата [Модель WRF: Хаб-15]", filePath));

                // 12 ВСВ 19.04.2016
                line = IO.FindLine(sr, "Прогноз от:");
                line = line.Substring(12, 17);
                DateTime dateIni;
                if (!Common.DateTimeProcess.TryParse(line, "HH XXX dd.MM.yyyy", out dateIni))
                    throw new Exception("Не удалось распознать дату прогноза от...");

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
                    if (!fileXAmur.TryGetValue(fields[0].Trim() + split + fields[1].Trim(), out catalogId))
                        throw new Exception("В словаре отсутствует код записи каталога, соответствующей " + (fields[0].Trim() + split + fields[1].Trim()));

                    for (int i = 0; i < lags.Length; i++)
                    {
                        if (fields[i + 2].Trim() != "")
                        {
                            data.Add(new DataForecast()
                            {
                                CatalogId = catalogId,
                                DateFcs = dateIni.AddHours(lags[i]),
                                LagFcs = lags[i],
                                Value =Common.StrVia.ParseDouble(fields[i + 2])
                            });
                        }
                    }
                }
                return new object[] { dateIni, data };
            }
            finally
            {
                if (sr != null) sr.Close();
            }
        }
    }
}
