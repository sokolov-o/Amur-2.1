using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FERHRI.Common;
using FERHRI.Amur.Meta;
using FERHRI.Amur.Data;

namespace HBRMsSql_AmurPg
{
    class Program
    {
        /// <summary>
        /// 
        /// 1. 2016.10 - Импорт данных из БД "Амур.HBR.MsSql" -> "Амур.VVO.Pg"
        /// 
        ///     1. Предварительно формируется csv файл с данными (из excel-запроса к "Амур.HBR.MsSql"),
        ///     который и импортируется. Структура файла = структуре таблицы "Амур.HBR.MsSql".DataValues.
        ///     
        ///     2. В app.config отражена перекодировка для тех variable_id, которые не совпадают.
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            DateTime dateS = DateTime.Now;

            Console.WriteLine(HBRMsSql_AmurPg.Settings.Default.AmurConnectionString);

            Import(HBRMsSql_AmurPg.Settings.Default.AmurConnectionString, HBRMsSql_AmurPg.Settings.Default.HBRMsSqlDataFilePath);

            Console.WriteLine("\n\nProgram finished. " + (DateTime.Now - dateS).TotalMinutes + " min elapsed.");
            Console.ReadKey();
        }

        private static void Import(string amurConnectionString, string filePath)
        {
            #region INITIALIZE DICTIONARY CONVERTERS

            Dictionary<int, int> siteIdConvert = new Dictionary<int, int>();
            siteIdConvert.Add(23, 23);
            siteIdConvert.Add(200, 200);
            siteIdConvert.Add(267, 267);

            Dictionary<int, int> variableIdConvert = new Dictionary<int, int>();
            variableIdConvert.Add(1, 1);
            variableIdConvert.Add(2, 2);
            variableIdConvert.Add(5, 5); 
            variableIdConvert.Add(6, 6);
            variableIdConvert.Add(7, 7);
            variableIdConvert.Add(11, 11);
            variableIdConvert.Add(12, 12);
            variableIdConvert.Add(13, 13);
            variableIdConvert.Add(19, 19);
            variableIdConvert.Add(22, 22);
            variableIdConvert.Add(23, 23);
            variableIdConvert.Add(28, 28);
            variableIdConvert.Add(33, 33);
            variableIdConvert.Add(34, 34);
            variableIdConvert.Add(45, 45);
            variableIdConvert.Add(46, 41); // разные
            variableIdConvert.Add(50, 50);
            variableIdConvert.Add(51, 51);
            variableIdConvert.Add(52, 52);
            variableIdConvert.Add(53, 53);
            variableIdConvert.Add(69, 1010); // разные


            Dictionary<int, int> methodIdConvert = new Dictionary<int, int>();
            methodIdConvert.Add(0, 0);

            Dictionary<int, int> sourceIdConvert = new Dictionary<int, int>();
            sourceIdConvert.Add(0, 0);

            Dictionary<int, int> offsetTypeIdConvert = new Dictionary<int, int>();
            offsetTypeIdConvert.Add(0, 0);

            #endregion DICTIONARY CONVERTERS

            FERHRI.Amur.Data.DataManager.SetDefaultConnectionString(amurConnectionString);
            FERHRI.Amur.Data.DataValueRepository dstDb = FERHRI.Amur.Data.DataManager.GetInstance().DataValueRepository;
            FERHRI.Amur.Meta.CatalogRepository ctlrep = FERHRI.Amur.Meta.DataManager.GetInstance().CatalogRepository;

            Console.WriteLine("Read data...");
            List<FileHBRMsSql.DataValue> dvs0 = FileHBRMsSql.Parse(filePath);

            Console.WriteLine("Convert data to FERHRI.Amur format...");
            List<FERHRI.Amur.Data.DataValue> dvs = Convert(
                dvs0,
                siteIdConvert,
                variableIdConvert,
                methodIdConvert,
                sourceIdConvert,
                offsetTypeIdConvert,
                ctlrep
            );

            Console.WriteLine("Write data...");
            dstDb.Insert(dvs);
        }
        /// <summary>
        /// Convert data to FERHRI.Amur format.
        /// </summary>
        /// <param name="dvs"></param>
        /// <param name="variableIdConvert"></param>
        /// <param name="amurConnectionString">Нужно для считывания Catalog и создания новых при необходимости.</param>
        /// <returns></returns>
        private static List<DataValue> Convert(List<FileHBRMsSql.DataValue> dvs,
            Dictionary<int, int> siteIdConvert,
            Dictionary<int, int> variableIdConvert,
            Dictionary<int, int> methodIdConvert,
            Dictionary<int, int> sourceIdConvert,
            Dictionary<int, int> offsetTypeIdConvert,
            FERHRI.Amur.Meta.CatalogRepository ctlrep)
        {
            List<DataValue> ret = new List<DataValue>();
            List<Catalog> catalogs = new List<Catalog>();
            int createdNum = 0;

            for (int i = 0; i < dvs.Count; i++)
            {
                if (dvs[i].Value == -9999) continue;

                #region GET CATALOG

                // CATALOG EXISTS?
                Catalog catalog = catalogs.FirstOrDefault(x =>
                    x.SiteId == siteIdConvert[dvs[i].SiteID]
                    && x.VariableId == variableIdConvert[dvs[i].VariableID]
                    && x.OffsetTypeId == offsetTypeIdConvert[dvs[i].OffsetTypeID.HasValue ? (int)dvs[i].OffsetTypeID : 0]
                    && x.OffsetValue == (dvs[i].OffsetValue.HasValue ? (double)dvs[i].OffsetValue : 0)
                    && x.MethodId == methodIdConvert[dvs[i].MethodID]
                    && x.SourceId == sourceIdConvert[dvs[i].SourceID]
                );
                if ((object)catalog == null)
                {
                    // CREATE CATALOG 
                    catalog = new Catalog(
                        -1,
                        siteIdConvert[dvs[i].SiteID],
                        variableIdConvert[dvs[i].VariableID],
                        methodIdConvert[dvs[i].MethodID],
                        sourceIdConvert[dvs[i].SourceID],
                        offsetTypeIdConvert[dvs[i].OffsetTypeID.HasValue ? (int)dvs[i].OffsetTypeID : 0],
                        (dvs[i].OffsetValue.HasValue ? (double)dvs[i].OffsetValue : 0)
                    );
                    Catalog catalogExists = ctlrep.Select(catalog.SiteId, catalog.VariableId, catalog.MethodId, catalog.SourceId, catalog.OffsetTypeId, catalog.OffsetValue);
                    if ((object)catalogExists == null)
                    {
                        catalog = ctlrep.Insert(catalog);
                        createdNum++;
                        Console.WriteLine(createdNum + " catalogs created. " + catalog.ToString());
                    }
                    else
                        catalog = catalogExists;

                    catalogs.Add(catalog);
                }

                #endregion GET CATALOG

                ret.Add(new DataValue(
                    -1,
                    catalog.Id,
                    dvs[i].Value,
                    dvs[i].LocalDateTime,
                    0,
                    (float)dvs[i].UTCOffset
                ));
            }
            return ret;
        }
    }
}
