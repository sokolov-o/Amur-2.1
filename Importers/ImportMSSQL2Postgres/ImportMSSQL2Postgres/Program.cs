using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportMSSQL2Postgres
{
    class Program
    {
        internal static string CNNS_MS =
            //"Data Source=192.168.203.163\\MSSQLSERVER12;Integrated Security=SSPI;Initial Catalog=Amur";
        "Data Source=FERHRI-nout;Integrated Security=SSPI;Initial Catalog=Amur;";
        static string CNNS_PG =
            "Server = 192.168.203.163; Port = 5432; User Id = postgres; Password = qq; Database = hymera;";
        //"Server = localhost; Port = 5432; User Id = postgres; Password = qq; Database = hymera;";

        static void Main(string[] args)
        {
            DateTime dateS = DateTime.Now;

            FERHRI.Amur.Data.Repository.ConnectionString = CNNS_PG;
            FERHRI.Amur.Meta.Repository.ConnectionString = CNNS_PG;

            FERHRI.Amur.Meta.Repository.FillDicCash();

            //Import.Station(cnnsPg, cnnsMs);
            //Import.Method(cnnsPg, cnnsMs);
            //Import.Source(cnnsPg, cnnsMs);
            //Import.Unit(cnnsPg, cnnsMs);
            //Import.DataType(cnnsPg, cnnsMs);
            //Import.GeneralCategory(cnnsPg, cnnsMs);
            //Import.ValueType(cnnsPg, cnnsMs);
            //Import.SampleMedium(cnnsPg, cnnsMs);
            //Import.VariableType(cnnsPg, cnnsMs);
            //Import.Variable();
            //Import.GeoType(cnnsPg, cnnsMs);
            //Import.GeoObject(cnnsPg, cnnsMs);
            //Import.StationGeoObject(cnnsPg, cnnsMs);
            //Import.SiteGroup(cnnsPg, cnnsMs);

            //Import.DataValue(cnnsPg, cnnsMs);
            //Import.DataSource(cnnsPg, cnnsMs);

            //Import.SiteAttr();
            Import.Climate();
            //Import.Categories();

            Console.WriteLine("\n\nProgram finished. " + (DateTime.Now - dateS).TotalMinutes + " min elapsed.");
            Console.ReadKey();
        }
    }
}
