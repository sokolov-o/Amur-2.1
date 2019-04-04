using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
//using FERHRI.Amur.Data;
using FERHRI.Common;
using System.Diagnostics;
using Import.AmurServiceReference;
using System.IO;

namespace Import
{
    /// <summary>
    /// Импорт длин лучей кромки льда из текстового файла в БД "Амур"
    /// OSokolov@201802
    /// </summary>
    public class IceBeams
    {
        /// <summary>
        /// Соответствие моря коду пункта (типа геообъект/море).
        /// Луч (его номер) для каждого моря идентифицируется в записи каталога данных величиной смещения для типа смещения "Номер луча отсёчения кромки льда".
        /// </summary>
        static Dictionary<string, int> sea_x_beamsite = new Dictionary<string, int>()
        {
            { "ber", 10297 },
            { "okh", 10417 },
            { "tat", 10418 }
        };
        static internal void Run(string filePath, ServiceAmurWCF srvc)
        {
            Console.WriteLine("READ BEAMS FROM FILE...");
            List<DataValue> dvs = Parse(filePath, srvc, ',');

            Console.Write("WRITE BEAMS TO AMUR...");
            if (dvs != null)
            {
                srvc.Client.SaveDataValueList(srvc.h, dvs.Where(x => !double.IsNaN(x.Value)).ToArray(), null);
                Console.WriteLine("В БД записано {0} значений.", dvs.Count);
            }
        }
        static private List<DataValue> Parse(string filePath, ServiceAmurWCF srvc, char fileFieldSplitter)
        {
            Console.WriteLine("FILE {0}", filePath);

            List<DataValue> ret = new List<DataValue>();
            List<Catalog> catalogs = new List<Catalog>();

            // CREATE DIR 4 MOVE IMPORTED
            string dirImported = (new FileInfo(filePath)).DirectoryName + @"\Imported";
            if (!Directory.Exists(dirImported))
                Directory.CreateDirectory(dirImported);

            char[] splitter = new char[] { fileFieldSplitter };

            // GET SEA FROM FILE NAME
            FileInfo finfo = new FileInfo(filePath);
            int seaSiteId = -1;
            foreach (KeyValuePair<string, int> item in sea_x_beamsite)
            {
                if (finfo.Name.IndexOf(item.Key) == 0)
                    seaSiteId = item.Value;
            }
            if (seaSiteId < 0)
                throw new Exception("(seaSiteId < 0) для файла " + filePath);

            // SCAN FILE

            Console.WriteLine(filePath);
            StreamReader sr = null;
            try
            {
                sr = new StreamReader(filePath, Encoding.GetEncoding(1251));
                int year = int.MaxValue;

                // SCAN DATA ROWS
                while (!sr.EndOfStream)
                {
                    string row = sr.ReadLine();
                    if (!string.IsNullOrEmpty(row))
                    {
                        string[] cells = row.Split(splitter);
                        int iLastCell = cells.Length - 2;

                        if (!string.IsNullOrEmpty(cells[cells.Length - 1]) && cells[cells.Length - 1] != "9999/")
                            throw new Exception("Нарушение формата в строке " + row);
                        if (cells[iLastCell].Trim() == "9999")
                            iLastCell--;

                        // IF YEAR
                        if (cells.Length == 2 && cells[0].Length == 4)
                        {
                            year = int.Parse(cells[0]);
                            Console.WriteLine("Year " + year);
                        }
                        // IF DATA
                        else
                        {
                            // GET DATE
                            int decade = int.Parse(cells[0]);
                            DateTime date = FERHRI.Amur.Meta.Time.GetDateSTimeNum(year, decade, (int)FERHRI.Amur.Meta.EnumTime.DecadeOfYear);

                            for (int i = 1; i <= iLastCell; i++)
                            {
                                // GET CATALOG

                                Catalog catalog = Program.GetCatalog(new Catalog()
                                {
                                    Id = -1,
                                    SiteId = seaSiteId,
                                    VariableId = 1193, // Расстояние, мор. миль
                                    MethodId = 1605, // Визуальный анализ
                                    SourceId = (int)FERHRI.Amur.Meta.EnumLegalEntity.FERHRI,
                                    OffsetTypeId = 105, // Порядковый номер в последовательности
                                    OffsetValue = i
                                }, srvc);

                                // GET DATAVALUE
                                DataValue dv = new DataValue()
                                {
                                    CatalogId = catalog.Id,
                                    UTCOffset = 0,
                                    DateLOC = date,
                                    DateUTC = date,
                                    FlagAQC = (byte)FERHRI.Amur.Meta.EnumFlagAQC.NoAQC,
                                    Value = int.Parse(cells[i])
                                };
                                ret.Add(dv);
                            }
                        }
                    }
                }
                // MOVE IMPORTED
                sr.Close();
                sr = null;
                File.Move(filePath, dirImported + "\\" + (new FileInfo(filePath)).Name);
            }
            finally
            {
                if (sr != null) sr.Close();
            }
            return ret;
        }
    }
}