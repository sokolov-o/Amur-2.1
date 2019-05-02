using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestService.AmurServiceRWReference;

namespace TestService
{
    class SampleServiceW
    {
        /// <summary>
        /// 1. Получить или создать запись каталога для прогностических данных.
        /// 2. Записать прогностическое значение.
        /// 3. Прочитать прогностическое значение.
        /// </summary>
        public static void SampleForecast(ServiceClient client, long hSvc)
        {
            try
            {
                // Получить запись каталога

                int siteId = 98; // Новомихайловка
                int variableId = 1184; // Расход воды, сут прог (м3/сек)
                int methodId = 1604;// HYD.TEST.EMG2ASHM
                int sourceId = 777; // FERHRI
                int offsetTypeId = 0;
                double offsetValue = 0;
                Catalog catalog = client.GetCatalog(hSvc, siteId, variableId, offsetTypeId, methodId, sourceId, offsetValue);

                if (catalog == null)
                {
                    catalog = client.SaveCatalog(hSvc, new Catalog()
                    {
                        Id = -1,
                        SiteId = siteId,
                        VariableId = variableId,
                        MethodId = methodId,
                        SourceId = sourceId,
                        OffsetTypeId = offsetTypeId,
                        OffsetValue = offsetValue
                    });

                    Console.WriteLine("Создана запись каталога данных: " + catalog.Id);
                }
                Console.WriteLine("Получена запись каталога данных.");

                // Записать прогнозы

                DateTime dateIni = DateTime.Today;
                DataForecast[] dfs = new DataForecast[]
                {
                    new DataForecast()
                    {
                        CatalogId = catalog.Id,
                        DateIni = dateIni,
                        DateFcs = dateIni,
                        LagFcs = 0,
                        Value = 777,
                        DateInsert = DateTime.Now
                    }
                    , new DataForecast()
                    {
                        CatalogId = catalog.Id,
                        DateIni = dateIni,
                        DateFcs = dateIni.AddHours(6),
                        LagFcs = 6,
                        Value = 777,
                        DateInsert = DateTime.Now
                    }
                };

                Dictionary<int, bool> existsFcs = client.ExistsDataForecasts(hSvc, new int[] { catalog.Id }, dateIni);
                if (!existsFcs[catalog.Id])
                {
                    client.SaveDataForecastList(hSvc, dfs);
                    Console.WriteLine("Сохранено {0} прогностических значений для записи каталога.", dfs.Length);
                }
                else
                    Console.WriteLine("Прогностические значения за исх. срок {0} были записаны ранее.", dateIni);

                // Получить прогнозы

                dfs = client.GetDataForecasts(hSvc, catalog.Id, DateTime.Today, DateTime.Today, null, false);
                Console.WriteLine("Получено {0} прогностических значений для записи каталога.", dfs.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
