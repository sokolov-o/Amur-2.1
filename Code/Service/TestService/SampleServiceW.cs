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
        /// 
        /// Пример 1 работы с сервисом доступа НА ЗАПИСЬ к данным БД "Амур", ДВНИГМИ
        /// 
        /// Создание новой станции.
        /// 
        /// OSokolov@ferhri.ru
        /// </summary>
        public static void Sample1(ServiceClient client, long hSvc)
        {
            try
            {
                // Содать станцию - вылетит exception, т.к. уже есть такая станция
                try
                {
                    Station station = new Station() { Id = -1, Code = "00000", Name = "OSokolov@ferhri.ru", TypeId = 1 };

                    int id = client.SaveStation(hSvc, station);
                    Console.WriteLine("РАЗ: Станция {0}", ((id > 0) ? " создана, id = " + id : " не создана!"));
                    id = client.SaveStation(hSvc, station);
                    Console.WriteLine("ДВА: Станция {0}", ((id > 0) ? " создана, id = " + id : " не создана!"));

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                // Записать в базу - вылетит exception, т.к. нет такого кода каталога catalogId
                try
                {
                    Console.WriteLine("Записать в базу - вылетит exception, т.к. нет такого кода каталога catalogId");

                    int catalogId = -987654321;
                    client.SaveValue(hSvc, catalogId, DateTime.Today, DateTime.Today.AddHours(10), -777.777, 0, null);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                // Выбрать данные

                int[] sitesId = new int[] { 284, 267 }; // МС Хабаровск и Комсомольск
                int[] variablesId = new int[] { 1, 7, 8, 11, 10 }; // Ветер напр., ветер скорость, ветер скорость порыв, давл. на ур. станции, темп. воздуха

                Dictionary<Catalog, DataValue[]> dataValues = client.GetDataValues(hSvc,
                    DateTime.Today.AddDays(-5), DateTime.Today.AddDays(1), true,
                    sitesId,
                    variablesId,
                    null, // Выбирать из всех методов последнее (актуальное) значение
                    null, // Выбирать из всех источников последнее (актуальное) значение
                    null, // Все смещения и уровни
                    null, // Все значения смещений и уровней
                    null, // Выбирать значения без учёта флага контроля данных
                    true, // Выбрать одно, последнее актуальное значение
                    false // Не выбирать значения, помеченные как удалённые
                );
                Console.WriteLine("\n\nОК - Выбрано {0} записей каталога из базы данных.", dataValues.Count);

                Variable[] variables = client.GetVariablesByList(hSvc, variablesId);
                Site[] sites = client.GetSitesByList(hSvc, sitesId);
                Console.WriteLine("\n\nОК - Выбрано {0} пункта и {1} переменных из базы данных.", sites.Length, variables.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

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
