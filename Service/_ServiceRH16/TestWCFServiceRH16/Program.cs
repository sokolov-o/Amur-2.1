using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWCFServiceRH16.ServiceReferenceAmurRH16;

namespace TestWCFServiceRH16
{
    class Program
    {
        /// <summary>
        /// 
        /// Тестирование сервиса "Русгидро-2016" - сервиса доступа к наблюдённым и прогностическим модельным данным базы данных "Амур (ДВНИГМИ)".
        /// 
        /// Сервис предоставляет данные агрегированные за сутки. 
        /// В настоящей версии сервиса агрегация производится "на лету" по исходным срочным наблюдённым и прогностическим данным.
        /// Алгоритмы агрегации описаны в ТЗ на разработку сервиса.
        /// 
        /// TODO: Проверьте и подкорректируйте адрес сервиса в файле App.config.
        /// 
        /// Автор: OSokolov@ferhri.ru, ФГБУ "ДВНИГМИ", Владивосток-2016. 
        /// 
        /// </summary>
        /// <param name="args">Отсутствуют.</param>
        static void Main(string[] args)
        {
            DateTime dateRun = DateTime.Now;
            Console.WriteLine("Start " + dateRun);
            Console.Write("Соединение с сервисом...");

            // Открыть сессию клиента.
            ServiceClient svc = new ServiceClient();
            svc.Open();
            // Получить дескриптор для чтения данных.
            long hSrv = svc.Open("ext_rh16", "??????");
            if (hSrv > 0)
                Console.WriteLine(" Успешно.");
            else
            {
                Console.WriteLine(" Ошибка получения дескриптора сессии {0}. Проверьте имя пользователя и пароль.", hSrv);
                Console.ReadKey();
                return;
            }

            // Установить дату (сутки) выборки данных
            DateTime dateSelect = new DateTime(2017, 08, 20);

            // ВЫБРАТЬ доступный клиенту период времени данных наблюдений и прогнозов (даты начала и окончания)
            List<DateTime> dateSF = svc.GetAvailableTimePeriod(hSrv);
            // ВЫБРАТЬ доступный клиенту список станций
            List<Station> stations = svc.GetAvailableStations(hSrv);
            // ВЫБРАТЬ доступные клиенту список переменных
            List<Variable> variables = svc.GetAvailableVariables(hSrv);

            Console.WriteLine("Период данных, доступный для выборки: " + dateSF[0] + " - " + dateSF[1]);
            Console.WriteLine("Пример выборки данных для " + dateSelect);
            Console.WriteLine("Нажмите ВВОД для продолжения...");
            Console.ReadKey();

            // ЦИКЛ по переменным
            int iVar = 1;
            List<int> stationsId = stations.Select(x => x.Id).ToList();
            foreach (var variable in variables)
            {
                Console.WriteLine("{0}. {1} ({2})", iVar++, variable.Name, variable.Id);
                int iStation = 1;

                // ВЫБРАТЬ наблюдения для всех станций текущей переменной
                List<DataValue> dvso = svc.GetDataObservations(hSrv, stationsId, variable.Id, dateSelect);
                foreach (var data in dvso)
                {
                    Station station = stations.First(x => x.Id == data.StationId);
                    Console.WriteLine("НАБЛ {0:00}. Станция {1} {2} Значение {3:0.00} Кол. {4}", iStation++, station.Code, station.Name, data.Value, data.Count);
                }

                // ВЫБРАТЬ прогнозы для всех станций текущей переменной
                //
                // ATTENTHION: 
                //  Ключ словаря - датавремя с которого осуществлялся прогноз (исходная дата прогноза, date_ini). 
                //  Дата на которую прогнозируем (дата прогноза, date_fcs) содержится к классе DataForecast.
                Dictionary<DateTime, List<DataForecast>> ddv = svc.GetDataForecasts(hSrv, stationsId, variable.Id, dateSelect);
                foreach (KeyValuePair<DateTime, List<DataForecast>> kvp in ddv.OrderBy(x => x.Key))
                {
                    Console.WriteLine("ПРОГ {0:yyyyMMdd HH} --> {1:yyyyMMdd HH}", kvp.Key, dateSelect);
                    iStation = 1;
                    IOrderedEnumerable<DataForecast> df = kvp.Value.OrderBy(x => x.MinLagFcs).OrderBy(x => x.StationId);
                    foreach (DataForecast data in df)
                    {
                        Station station = stations.First(x => x.Id == data.StationId);
                        Console.WriteLine(
                            "  {0:00}. Станция {1} {2} Значение {3:00.00} Заблаг. (час) [{4:00}-{5:00}] Кол. {6}",
                            iStation++, station.Code, station.Name, data.Value, data.MinLagFcs, data.MaxLagFcs, data.Count);
                    }
                }
            }

            // Закрыть сессию
            svc.Close();

            Console.WriteLine(string.Format("Time elapsed {0:0.00} min", (DateTime.Now - dateRun).TotalMinutes));
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nFINISHED. PRESS ANY KEY.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();
        }
    }
}
