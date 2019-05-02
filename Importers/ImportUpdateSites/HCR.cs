using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Import.AmurServiceReference;

namespace Import
{
    /// <summary>
    /// Импорт данных из БД типа HCR (Sakura)
    /// </summary>
    class HCR
    {
        static Dictionary<int/*HCR*/, int/*Amur*/> _stationXsite = new Dictionary<int, int>()
        {
            //{4873301,31713}
        };
        static Dictionary<int/*HCR*/, int/*Amur*/> _paramXparam = new Dictionary<int, int>()
        {
            //{4873301,31713}
        };
        static Dictionary<int/*HCR*/, int/*Amur*/> _levelXoffset = new Dictionary<int, int>()
        {
            //{4873301,31713}
        };
        static Dictionary<Site, double/*utc_offset*/> _sites = new Dictionary<Site, double>();
        static List<Station> _stations = new List<Station>();
        /// <summary>
        /// Импорт данных HCR -> Amur: чтение, преобразование, запись.
        /// Всё в памяти, большие объёмы.
        /// </summary>
        internal static void Run(ServiceClient client, long hSvc,
            DateTime dateMeteoS, DateTime dateMeteoF, int stationRegionId, int[] parameters,
            int amurSourceId = 3 // ВНИИГМИ-МЦД (Amur.VVO dics)
        )
        {
            try
            {
                // READ


                List<DataValue> dataValues = null;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        const int _METHOD_ID = (int)FERHRI.Amur.Meta.EnumMethod.ObservationInSitu;
        static List<Catalog> _catalogs = new List<Catalog>();
    }
}
