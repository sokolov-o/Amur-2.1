using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SOV.Amur.Sys;
using SOV.Amur.Meta;
using SOV.Common;

namespace SOV.Amur
{
    static class Program
    {
        static public string ConnectionStringAmur { get; set; }
        static public User User { get; set; }
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // USER & connection string

            string suser = global::SOV.Amur.Properties.Settings.Default.User;
            if (!string.IsNullOrEmpty(suser))
            {
                User = new Common.User(suser.Split(';')[0], suser.Split(';')[1]);
            }

            Common.FormUserPassword frm = new Common.FormUserPassword(
                StrVia.ToDictionaryPairs(global::SOV.Amur.Properties.Settings.Default.AmurConnectionString, '/'), 
                User);
            if (frm.ShowDialog() != DialogResult.OK) return;
            User = frm.User;
            if (User == null)
                return;

            ConnectionStringAmur = frm.ConnectionString;

            // SCHEMAS CONNECTION STRINGS
            Data.DataManager.SetDefaultConnectionString(ConnectionStringAmur);
            Meta.DataManager.SetDefaultConnectionString(ConnectionStringAmur);
            DataP.DataManager.SetDefaultConnectionString(ConnectionStringAmur);
            Reports.DataManager.SetDefaultConnectionString(ConnectionStringAmur);
            Social.DataManager.SetDefaultConnectionString(ConnectionStringAmur);
            Sys.DataManager.SetDefaultConnectionString(ConnectionStringAmur);

            ReadUserSettings();

            Application.Run(new FormMain());
        }

        static public AttrValueCollection UserSettings { get; set; }

        static private void ReadUserSettings()
        {
            try
            {
                UserSettings = Sys.DataManager.GetInstance().SysEntityRepository.
                    SelectAllAttr((int)Sys.EntityEnum.User, Program.User.Name);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка при чтении настроек пользователя.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void TestSomething()
        {
            MessageBox.Show("Programm.TestSomething static method is worked now...");

            #region ESTIMATE SITE VARIABLE

            int method_id = 103; // Прогноз WRF.VVO.OS
            //int method_id = 100; // Прогноз WRF-ARW:HBRK15
            //int obsCatalogId = 3535; // Ta, Magadan
            //int obsCatalogId = 3538; // Pmsl, Magadan
            //int obsCatalogId = 3536; // Wvel, Magadan
            int obsCatalogId = 3534; // Wdir, Magadan
            DateTime dateS = new DateTime(2016, 5, 20), dateF = new DateTime(2016, 9, 1);
            List<double> fcsLags = null;

            Catalog obsCatalog = Meta.DataManager.GetInstance().CatalogRepository.Select(obsCatalogId);
            List<EnumMathVar> estMathVars = new List<EnumMathVar>(new EnumMathVar[] 
            { 
                EnumMathVar.DEVAVG, EnumMathVar.ABSDEV, EnumMathVar.R,
                EnumMathVar.PV2Pr,EnumMathVar.PV5Pr,EnumMathVar.PV10Pr,
                EnumMathVar.SIGMA0_WDIR_RD,EnumMathVar.SIGMA31_WDIR_RD,EnumMathVar.SIGMA61_WDIR_RD,EnumMathVar.SIGMA91_WDIR_RD,EnumMathVar.SIGMA_WDIR_SIN
            });
            string outFilePath = @"D:\Temp\4export\Амур\ObsVsFcs." + method_id + ".EstimationByDayNight.csv";

            Dictionary<string, Dictionary<double, List<Estimation>>>[] daynightLag0012Estims = ObsVsFcs.ObsVsFcs.GetEstimationsByDayNight0012(
                obsCatalog,
                dateS, dateF,
                method_id, 777, fcsLags, estMathVars);
            ObsVsFcs.ObsVsFcs.ToFile(outFilePath, daynightLag0012Estims, ';');
            #endregion ESTIMATE SITE VARIABLE

            //#region FOG PROBABILITY
            //double[] probFogDay = DataP.Observations.CalcProbabFogDay(new List<int>(new int[] { 326 }), new DateTime(2016, 6, 1), new DateTime(2016, 6, 30), 51, 1057);
            //#endregion FOG PROBABILITY

            MessageBox.Show("Programm.TestSomething static method is finished...");
        }
    }
}
