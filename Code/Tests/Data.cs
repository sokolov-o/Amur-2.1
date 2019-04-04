using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SOV.Amur.Data;

namespace SOV.UnitTest
{
    [TestClass]
    public class DataValueTest
    {
        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
        }

        [TestMethod]
        public void Update()
        {
        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
        }
    }

    [TestClass]
    public class DataForecastTest
    {
        static DataForecastRepository repo;
        static string deleteQuery = "Delete From {0} Where catalog_id = {1} and date_fcs = '{2}' and value = {3} and fcs_lag = {4}";
        static Random random;
        static List<DataForecast> InsertedData;

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            random = new Random();
            repo = DataManager.GetInstance(CommonFuncs.GetDefaultConnectionString()).DataForecastRepository;
            InsertedData = new List<DataForecast>();
        }

        private static DataForecast TmpDataForecast(DateTime? _frcDate = null, DateTime? _insertDate = null,
                                                    double? _value = null, double? _lag = null, int? _catalog = null)
        {
            //TODO: Select random id from data.catalog
            int catalog = _catalog ?? 1;
            double lag = _lag ?? 1;
            double value = _value ?? random.Next((int)-1e6, (int)1e6);
            DateTime insertDate = _insertDate ?? TrimMilisec(DateTime.Now);
            DateTime frcDate = _frcDate ?? TrimMilisec(DateTime.Now);
            return new DataForecast(catalog, lag, frcDate, insertDate, value, insertDate);
        }

        [TestMethod]
        public void InsertArray()
        {
            var arraySize = 2002;
            var preTableCount = CommonFuncs.TableCount(repo.TableName);
            var arr = new List<DataForecast>();
            var df = TmpDataForecast();
            for (int i = 0; i < arraySize; ++i)
                arr.Add(TmpDataForecast(df.DateFcs.AddHours(i), df.DateInsert, df.Value, df.LagFcs, df.CatalogId));
            InsertedData.AddRange(arr);
            repo.Insert(arr);
            Assert.AreEqual(preTableCount + arraySize, CommonFuncs.TableCount(repo.TableName));
        }

        static private DateTime TrimMilisec(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second);
        }

        static private DateTime TrimSec(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, 0);
        }

        static private void CompareDateForecast(DataForecast expected, DataForecast real)
        {
            Assert.AreEqual(expected.Value, real.Value);
            Assert.AreEqual(TrimMilisec(expected.DateFcs), TrimMilisec(real.DateFcs));
            Assert.AreEqual(expected.CatalogId, real.CatalogId);
            Assert.AreEqual(expected.LagFcs, real.LagFcs);
        }

        [TestMethod]
        public void SelectDataForecasts()
        {
            var lag3 = random.Next((int)-1e6, (int)1e6);
            var df1 = TmpDataForecast();
            var df2 = TmpDataForecast(df1.DateFcs.AddHours(1), df1.DateInsert, df1.Value, df1.LagFcs, df1.CatalogId);
            var df3 = TmpDataForecast(df1.DateFcs.AddHours(2), df1.DateInsert, df1.Value, lag3, df1.CatalogId);
            InsertedData.AddRange(new List<DataForecast>() { df1, df2, df3 });
            repo.Insert(InsertedData);
            var res = repo.Select(df1.CatalogId, df1.DateFcs.AddSeconds(-1), df2.DateFcs.AddSeconds(1), null, true);
            res = res.OrderBy(x => x.DateFcs).ToList();
            Assert.AreEqual(2, res.Count, "2 elements count");
            CompareDateForecast(df1, res[0]);
            CompareDateForecast(df2, res[1]);

            res = repo.Select(df1.CatalogId, df1.DateFcs.AddSeconds(-1), df3.DateFcs.AddSeconds(1), null, true);
            Assert.AreEqual(3, res.Count, "3 elements count");

            res = repo.Select(df1.CatalogId, df1.DateFcs.AddSeconds(-1), df3.DateFcs.AddSeconds(1), lag3, true);
            Assert.AreEqual(1, res.Count);
            Assert.AreEqual(lag3, res[0].LagFcs);
        }

        [TestMethod]
        public void SelectMinMaxDates()
        {
            var df1 = TmpDataForecast();
            var df2 = TmpDataForecast(df1.DateFcs, df1.DateInsert, df1.Value, df1.LagFcs, 2);
            var currDate = CommonFuncs.DbCurrDate();
            InsertedData.AddRange(new List<DataForecast>() { df1, df2 });
            repo.Insert(InsertedData);
            var res = repo.SelectMinMaxDates(new List<int>() { df1.CatalogId, df2.CatalogId });
            Assert.AreEqual(2, res.Count);
            Assert.AreEqual(TrimMilisec(df1.DateFcs), TrimMilisec(res[df1.CatalogId][0]));
            //Assert.IsTrue(TrimSec(currDate), TrimSec(res[df1.CatalogId][3]));
            Assert.AreEqual(TrimMilisec(df2.DateFcs), TrimMilisec(res[df2.CatalogId][0]));
            //Assert.AreEqual(TrimSec(currDate), TrimSec(res[df2.CatalogId][3]));
        }

        [TestMethod]
        public void SelectGroupByCD()
        {
            var df1 = TmpDataForecast();
            var df2 = TmpDataForecast(df1.DateFcs.AddHours(1), df1.DateInsert, df1.Value, df1.LagFcs, 2);
            var currDate = CommonFuncs.DbCurrDate().AddSeconds(-1);
            InsertedData.AddRange(new List<DataForecast>() { df1, df2 });
            repo.Insert(InsertedData);
            var res = repo.SelectGroupByCD(df1.DateFcs.AddSeconds(-1), df2.DateFcs.AddSeconds(1), true);
            Assert.AreEqual(2, res.Count, "count 2 elems");
            Assert.AreEqual(TrimMilisec(df1.DateFcs), TrimMilisec(res[df1.CatalogId][0]));
            Assert.AreEqual(TrimMilisec(df2.DateFcs), TrimMilisec(res[df2.CatalogId][0]));
            res = repo.SelectGroupByCD(df1.DateFcs.AddSeconds(-1), df2.DateFcs.AddSeconds(-1), true);
            Assert.AreEqual(1, res.Count, "count 1 elem");
            Assert.AreEqual(TrimMilisec(df1.DateFcs), TrimMilisec(res[df1.CatalogId][0]));
        }

        private void CompareDateForecast2(DataForecast2 expected, DataForecast df1, DataForecast df2)
        {
            Assert.AreEqual(expected.CatalogId, df1.CatalogId);
            Assert.AreEqual(expected.Value1, df1.Value);
            Assert.AreEqual(expected.Value2, df2.Value);
            Assert.AreEqual(TrimMilisec(expected.DateFcs), TrimMilisec(df1.DateFcs));
            Assert.AreEqual(expected.LagFcs, df1.LagFcs);
        }

        [TestMethod]
        public void SelectDataForecasts2()
        {
            var df1 = TmpDataForecast();
            var df2 = TmpDataForecast(df1.DateFcs, df1.DateInsert, df1.Value, df1.LagFcs, 2);
            var df3 = TmpDataForecast(df1.DateFcs.AddHours(1), df1.DateInsert, df1.Value, df1.LagFcs, 2);

            var df4 = TmpDataForecast(df1.DateFcs.AddHours(1), df1.DateInsert, df1.Value + 1, df1.LagFcs + 1, 1);
            var df5 = TmpDataForecast(df1.DateFcs.AddHours(1), df1.DateInsert, df1.Value + 2, df1.LagFcs + 1, 2);

            InsertedData.AddRange(new List<DataForecast>() { df1, df2, df3, df4, df5});
            repo.Insert(InsertedData);
            var res = repo.SelectDataForecasts2(df1.CatalogId, df2.CatalogId,
                                df1.DateFcs.AddSeconds(-1), df1.DateFcs.AddHours(2),
                                new List<double>() { df1.LagFcs }, true);
            Assert.AreEqual(1, res.Count, CommonFuncs.Frame().GetFileLineNumber());
            CompareDateForecast2(res[0], df1, df2);
            res = repo.SelectDataForecasts2(df1.CatalogId, df2.CatalogId,
                                df1.DateFcs.AddSeconds(1), df1.DateFcs.AddHours(2),
                                new List<double>() { df1.LagFcs }, true);
            Assert.AreEqual(0, res.Count, CommonFuncs.Frame().GetFileLineNumber());

            res = repo.SelectDataForecasts2(df1.CatalogId, df2.CatalogId,
                df1.DateFcs.AddSeconds(-1), df1.DateFcs.AddHours(2),
                new List<double>() { df1.LagFcs, df5.LagFcs }, true);
            Assert.AreEqual(2, res.Count, CommonFuncs.Frame().GetFileLineNumber());

            res = repo.SelectDataForecasts2(df1.CatalogId, df2.CatalogId,
                                df1.DateFcs.AddSeconds(1), df1.DateFcs.AddHours(2),
                                new List<double>() { df5.LagFcs }, true);
            Assert.AreEqual(1, res.Count, CommonFuncs.Frame().GetFileLineNumber());
            CompareDateForecast2(res[0], df4, df5);
        }

        [TestMethod]
        public void Delete()
        {
            var df1 = TmpDataForecast();
            var df2 = TmpDataForecast(df1.DateFcs.AddHours(1));
            var df3 = TmpDataForecast(df1.DateFcs.AddHours(2));
            var oldCount = CommonFuncs.TableCount(repo.TableName);
            InsertedData.AddRange(new List<DataForecast>() { df1, df2, df3 });
            repo.Insert(InsertedData);
            repo.Delete(new List<DataForecast>() { df1, df2 });
            Assert.AreEqual(oldCount + 1, CommonFuncs.TableCount(repo.TableName));
            repo.Delete(new List<DataForecast>() { df3 });
            Assert.AreEqual(oldCount, CommonFuncs.TableCount(repo.TableName));
        }

        private static void DeleteDF(DataForecast df)
        {
            CommonFuncs.ExecSimpleQuery(
                string.Format(deleteQuery, repo.TableName, df.CatalogId, TrimMilisec(df.DateFcs), df.Value, df.LagFcs)
            );
        }

        [TestCleanup]
        public void CleanUp()
        {
            foreach (var df in InsertedData)
                DeleteDF(df);
            InsertedData.Clear();
        }
    }
}