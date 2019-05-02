using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FERHRI.Amur.Meta;
using FERHRI.Amur.Data;

namespace Import
{
    class Program
    {
        //static string _cnnsAmur = "Server=localhost;Port=5432;Database=ferhri.amur;user id=OSokolov;password=qq";
        static string _cnnsAmur = "Server=10.11.203.20;Port=5432;Database=ferhri.amur;user id=OSokolov;password=qq";
        //static string _cnnsAmur = "Server=10.8.3.180;Port=5432;Database=ferhri.amur;user id=OSokolov;password=qq";
        static bool isAmurServiceNeeded = true;
        static AmurServiceReference.ServiceClient client = isAmurServiceNeeded ? new AmurServiceReference.ServiceClient() : null;
        static long h = isAmurServiceNeeded ? client.Open(FERHRI.Common.User.Parse(Import.Properties.Settings.Default.User).Name, FERHRI.Common.User.Parse(Import.Properties.Settings.Default.User).Password) : -777;
        static internal ServiceAmurWCF srvc = new ServiceAmurWCF() { Client = client, h = h };

        static void Main(string[] args)
        {
            // Ещё их надо
            FERHRI.Amur.Meta.DataManager.SetDefaultConnectionString(_cnnsAmur);
            FERHRI.Amur.Data.DataManager.SetDefaultConnectionString(_cnnsAmur);
            //FERHRI.Amur.Meta.DataManager _dm = DataManager.GetInstance();

            #region Импорт "лучей" границ кромки льда (от Вражкина, 2018.02)
            Import.IceBeams.Run(@"D:\Documents\FERHRI\DATA\Ocean\Ice\Вражкин Beams 2018\tat_ice.new", srvc);
            #endregion

            #region Импорт кривых расходов ПУГМС
            //Import.CurveImport.Run(@"Data Source=10.11.25.111;Initial Catalog=hydro;user id=dt;password=dt");
            #endregion

            #region Импорт месячных индексов
            //Indeces.Run(Indeces.InputFileType.SEAKC, @"D:\Downloads\СЕАКЦ Индексы\indeces_all.txt", srvs);
            #endregion

            #region RESTRUCT CLIMATE 2017
            //AmurClimate2DataValue.Run();
            #endregion

            #region Импорт суточных метео-данных по приморским метео-станциям, которые подготовлены Гончуковым Л. В. в 2017.08.
            //PUGMS.Run(PUGMS.InputFileType.TaRd_201710, @"C:\Users\sov\Downloads\Приморье Метео Гончуков\Data 201710\", srvs);
            #endregion

            #region Импорт данных по Тунгусске (осадки: Настя в формате Гарцмана -> SOV -> Бугаец -> конверт -> SOV -> БД)
            //ImportTungus.Run(@"\\srv1\DataIni\_УГМС_ДВ\Бассейн Тунгусски 2017\Метео\", client, hSvc); //@"C:\Users\sov\Documents\Data\Tunguss 2017"
            #endregion

            #region Импорт словарей КН-02 (от Истомина)
            //ImportKH02Dictionaryes201702.Import(client, hSvc, "Server=10.8.3.188;Port=5432;Database=db;user id=warep;password=123warep456$");
            #endregion

            #region Импорт постов (от Герасимова)
            //ImportSitesMeteo201701.Run(new string[]
            //    {
            //        //@"D:\Documents\FERHRI\ПРОЕКТЫ\Амур\Data\Станции\T1-METEO_список ст_Амурская обл_с коорд_ИмяLat.csv",
            //        //@"D:\Documents\FERHRI\ПРОЕКТЫ\Амур\Data\Станции\T1-METEO_список ст_ЕАО_с коорд_ИмяLat.csv",
            //        //@"D:\Documents\FERHRI\ПРОЕКТЫ\Амур\Data\Станции\T1-METEO_список ст_Камч край_с коорд_ИмяLat.csv",
            //        //@"D:\Documents\FERHRI\ПРОЕКТЫ\Амур\Data\Станции\T1-METEO_список ст_Магд обл_с коорд_ИмяLat.csv",
            //        //@"D:\Documents\FERHRI\ПРОЕКТЫ\Амур\Data\Станции\T1-METEO_список ст_Прим край_с коорд_ИмяLat.csv",
            //        //@"D:\Documents\FERHRI\ПРОЕКТЫ\Амур\Data\Станции\T1-METEO_список ст_Сах обл_с коорд_ИмяLat.csv",
            //        //@"D:\Documents\FERHRI\ПРОЕКТЫ\Амур\Data\Станции\T1-METEO_список ст_Хаб край_с коорд_ИмяLat.csv"
            //        //@"D:\Documents\FERHRI\ПРОЕКТЫ\Амур\Data\Станции\T1-METEO_список ст_Чукотка_с коорд_ИмяLat.csv",
            //        //@"D:\Documents\FERHRI\ПРОЕКТЫ\Амур\Data\Станции\T1-METEO_список ст_Якутия_с коорд_ИмяLat.csv"
            //        @"D:\Documents\FERHRI\НИР\Амур\Data\Станции\Метеостанции КНР (Третьяков 2017).txt"
            //    },
            //      242, // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! на addr (ДВ УГМС) - переделать на legal_entity !!!!!!!!!!!
            //    _dm,
            //    ImportSitesMeteo201701.FileType.Tretyakov
            //);
            #endregion

            Console.WriteLine("\n\nPress ENTER...");
            Console.ReadLine();
        }

        static List<AmurServiceReference.Catalog> _catalogs = new List<AmurServiceReference.Catalog>();
        static int _catalogsInsertedQ = 0;
        static public AmurServiceReference.Catalog GetCatalog(AmurServiceReference.Catalog catalogFind, ServiceAmurWCF srvc)
        {
            AmurServiceReference.Catalog ret = _catalogs.FirstOrDefault(x =>
                x.SiteId == catalogFind.SiteId
                && x.VariableId == catalogFind.VariableId
                && x.MethodId == catalogFind.MethodId
                && x.SourceId == catalogFind.SourceId
                && x.OffsetTypeId == catalogFind.OffsetTypeId
                && x.OffsetValue == catalogFind.OffsetValue
            );

            if (ret == null)
            {
                ret = srvc.Client.GetCatalog(srvc.h,
                    catalogFind.SiteId,
                    catalogFind.VariableId,
                    catalogFind.OffsetTypeId,
                    catalogFind.MethodId,
                    catalogFind.SourceId,
                    catalogFind.OffsetValue);

                if (ret == null)
                {
                    ret = srvc.Client.SaveCatalog(srvc.h, catalogFind);
                    Console.WriteLine("Catalog with id {0} inserted. Insert num {1}", ret.ToString(), ++_catalogsInsertedQ);
                }
                _catalogs.Add(ret);
            }
            return ret;
        }
    }
}
