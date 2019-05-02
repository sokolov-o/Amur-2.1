using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Import.AmurServiceReference;
//using FERHRI.Amur.Meta;

namespace Import
{
    /// <summary>
    /// 
    /// Импорт индексов.
    /// 
    /// Индекс	Год	Месяц	Значение
    /// aos	1981	1	0.44
    /// aos	1981	2	0.28
    /// aos	1981	3	-0.55
    ///
    /// OSokolov@201711
    /// 
    /// </summary>
    class Indeces
    {
        internal enum InputFileType
        {
            // Индексы СЕАКЦ

            // Индекс	Год	Месяц	Значение
            // aos	1981	1	0.44
            // aos	1981	2	0.28
            // aos	1981	3	-0.55
            SEAKC = 1
        }

        static internal void Run(InputFileType fileType, string filePath, ServiceAmurWCF srvc)
        {
            List<DataValue> dvs;

            switch (fileType)
            {
                case InputFileType.SEAKC:
                    dvs = ParseSEAKC(filePath, srvc, '\t');
                    break;
                default:
                    throw new NotImplementedException();
            }
            if (dvs != null)
            {
                srvc.Client.SaveDataValueList(srvc.h, dvs.Where(x => !double.IsNaN(x.Value)).ToArray(), null);
                Console.WriteLine("В БД записано {0} значений.", dvs.Count);
            }
        }

        const int _METHOD_ID = (int)FERHRI.Amur.Meta.EnumMethod.ObservationInSitu;
        const int _SOURCE_ID = 243; // PUGMS
        const int _OFFSETTYPE_ID = (int)FERHRI.Amur.Meta.EnumOffsetType.NoOffset;
        const double _OFFSET_VALUE = 0;
        static Dictionary<string, int> _index_X_catalogId = new Dictionary<string, int>()
        {
            { "aos",2415881},{ "eao",2415875},{ "euo",2415877},{ "nao",2415880},{ "pna",2415879},{ "pol",2481122},{ "wao",2415876},{ "wpo",2415878}
            // select * from data.data_value where catalog_id in (2415881,2415875,2415877,2415880,2415879,2481122,2415876,2415878)
        };


        static private List<DataValue> ParseSEAKC(string fileName, ServiceAmurWCF srvc, char fileFieldSplitter)
        {
            List<DataValue> ret = new List<DataValue>();

            // CREATE DIR 4 MOVE IMPORTED
            string dirImported = (new FileInfo(fileName)).DirectoryName + @"\Imported";
            if (!Directory.Exists(dirImported))
                Directory.CreateDirectory(dirImported);

            char[] splitter = new char[] { fileFieldSplitter };

            // SCAN YEARS

            Console.WriteLine(fileName);
            StreamReader sr = null;
            try
            {
                sr = new StreamReader(fileName, Encoding.GetEncoding(1251));

                // SCAN HEADER
                sr.ReadLine().Trim().Split(splitter);

                // SCAN DATA ROWS
                while (!sr.EndOfStream)
                {
                    string row = sr.ReadLine();
                    if (!string.IsNullOrEmpty(row))
                    {
                        string[] cells = row.Split(splitter);

                        ret.Add(new DataValue()
                        {
                            CatalogId = _index_X_catalogId[cells[0]],
                            DateLOC = new DateTime(int.Parse(cells[1]), int.Parse(cells[2]), 1),
                            DateUTC = new DateTime(int.Parse(cells[1]), int.Parse(cells[2]), 1),
                            UTCOffset = 0,
                            Value = double.Parse(cells[3])
                        });
                    }
                }
                // MOVE IMPORTED
                sr.Close();
                sr = null;
                File.Move(fileName, dirImported + "\\" + (new FileInfo(fileName)).Name);

            }
            finally
            {
                if (sr != null) sr.Close();
            }
            return ret;
        }
    }
}
