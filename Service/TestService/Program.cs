using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using TestService.AmurServiceRWReference;

namespace TestService
{
    class Program
    {
        static public string UserName { get; set; }
        static public string UserPassword { get; set; }

        /// <summary>
        /// Пример работы с сервисом AmurServiceRWЖ чтение, запись из/в БД "Амур"
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // Клиент сессии
            ServiceClient client = null;

            // Дескриптор в пределах сессии
            // Используется в методах сервиса для идентификации пользователя
            long hSvc = 0;

            Console.WriteLine("Введите имя пользователя:"); UserName = Console.ReadLine();
            Console.WriteLine("Введите пароль пользователя:"); UserPassword = Console.ReadLine();
            UserName = "OSokolov";
            UserPassword = "qq";
            Console.WriteLine("{0} {1}", UserName, UserPassword);

            try
            {
                // Инициализация клиента сервиса 
                //client = new ServiceClient();
                //client = GetClient(@"http://10.8.3.180:8001/Service.svc");
                client = GetClient(@"http://localhost:8001/Service.svc");

                // Дескриптор сеанса/сессии сервиса
                hSvc = client.Open(UserName, UserPassword);  // Корректный идентификатор сессии
                if (hSvc < 1)
                {
                    Console.WriteLine("Некорректный пользователь и/или пароль.");
                    return;
                }
                // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Собственно пример использования сервиса
                SampleServiceR.SampleParser(client, hSvc);
                //SampleServiceR.Sample4NICPlaneta(client, hSvc);
                //SampleServiceW.SampleForecast(client, hSvc);
                //SampleServiceR.Sample4Bugaets(client, hSvc);
                //SampleServiceR.Sample4DataForecast(client, hSvc);
                // <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
            }
            finally
            {
                // Обязательное закрытие сессий и клиента
                if (client != null)
                {
                    client.Close(hSvc);
                    client.Close();
                }

                Console.WriteLine("\n\nPress ENTER...");
                Console.ReadLine();
            }
        }
        public static ServiceClient GetClient(string url)
        {
            BasicHttpBinding binding = new BasicHttpBinding();
            binding.OpenTimeout = new TimeSpan(0, 1, 0);
            binding.CloseTimeout = new TimeSpan(0, 1, 0);
            binding.ReceiveTimeout = new TimeSpan(0, 5, 0);
            binding.SendTimeout = new TimeSpan(0, 5, 0);
            binding.TransferMode = TransferMode.Buffered;
            binding.MaxBufferSize = 2147483647;
            binding.MaxReceivedMessageSize = 2147483647;
            binding.MaxBufferPoolSize = 2147483647;
            binding.ReaderQuotas.MaxDepth = 32;
            binding.ReaderQuotas.MaxStringContentLength = 2147483647;
            binding.ReaderQuotas.MaxArrayLength = 2147483647;
            binding.ReaderQuotas.MaxBytesPerRead = 4096;
            binding.ReaderQuotas.MaxNameTableCharCount = 16384;
            binding.Security.Mode = BasicHttpSecurityMode.None;
            EndpointAddress address = new EndpointAddress(url);
            ServiceClient client = new ServiceClient(binding, address);
            ServiceEndpoint se = client.Endpoint;
            var operations = client.Endpoint.Contract.Operations;
            foreach (var operation in operations)
            {
                DataContractSerializerOperationBehavior behavior = operation.Behaviors.Find<DataContractSerializerOperationBehavior>();
                if (behavior != null)
                    behavior.MaxItemsInObjectGraph = 2147483647;
            }
            return client;
        }
    }
}
