using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FERHRI.Amur.DataP;
using FERHRI.Amur.Meta;
using FERHRI.Common;
using System.Configuration;
using System.Collections.Specialized;


namespace FERHRI.Amur
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Data.DataManager.SetDefaultConnectionString(ConfigurationManager.ConnectionStrings["FERHRI.Amur.Properties.Settings.AmurDataConnectionString"].ConnectionString);
                Meta.DataManager.SetDefaultConnectionString(ConfigurationManager.ConnectionStrings["FERHRI.Amur.Properties.Settings.AmurMetaConnectionString"].ConnectionString);
                DataP.DataManager.SetDefaultConnectionString(ConfigurationManager.ConnectionStrings["FERHRI.Amur.Properties.Settings.AmurDataPConnectionString"].ConnectionString);
                Sys.DataManager.SetDefaultConnectionString(ConfigurationManager.ConnectionStrings["FERHRI.Amur.Properties.Settings.AmurSysConnectionString"].ConnectionString);

                string[] unitTimesStr = ConfigurationManager.AppSettings["unitTime"].Split(',');//, 104, 321, 318, 106
                DataP.DataManager dataPDM = DataP.DataManager.GetInstance();

                bool? isFcsDataType = null;
                if (!ConfigurationManager.AppSettings["isFcsDataType"].Equals("null"))
                    isFcsDataType = bool.Parse(ConfigurationManager.AppSettings["isFcsDataType"]);
                bool isWriterLog = true;
                foreach (var unitTime in unitTimesStr)
                {
                    List<DerivedTask> dtc = dataPDM.DerivedTasksRepository.SelectDerivedTasks(int.Parse(unitTime), isFcsDataType);
                    //List<DerivedTask> dtc = new List<DerivedTask>() { dataPDM.DerivedTasksRepository.SelectDerivedTask(41) };
                    foreach (var dt in dtc)
                    {
                        if (dt.IsSchedule && dt.IsRun(DateTime.Now.Date))
                            DataDerived.CalcDerived(dt, null, null, isWriterLog);
                        //DataDerived.CalcDerived(dt, null, null, isWriterLog);
                        //for (int year = 1967; year <= 2009; year++)
                        //{
                        //        DataDerived.CalcDerived(dt,new DateTime(2016,1,1), DateTime.Now, isWriterLog);
                        //  }
                    }
                }
            }
            catch (Exception ex)
            {
                Sys.DataManager.GetInstance().LogRepository.Insert(3, ex.Message, null, true);
            }
            finally { }
        }
    }
}
