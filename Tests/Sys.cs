using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SOV.Amur.Sys;

namespace SOV.UnitTest
{
    [TestClass]
    public class EntityTest
    {
        static SysEntityRepository repo;
        static string testInstance1 = "testInst1";
        static string testInstance2 = "testInst2";
        static string testVal1 = "testVal1";
        static string testVal2 = "testVal2";
        static long preTableCount;

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            repo = DataManager.GetInstance(CommonFuncs.GetDefaultConnectionString()).SysEntityRepository;
            preTableCount = CommonFuncs.TableCount(repo.TableName);

            ClassCleanup();
            repo.Insert((int)AttrEnum.UserSiteGroupId, testInstance1, testVal1);
            repo.Insert((int)AttrEnum.UserOrganizationId, testInstance1, testVal2);
            repo.Insert((int)AttrEnum.UserOrganizationId, testInstance2, testVal2);
        }

        [TestMethod]
        public void Insert()
        {
            var tmpTableCount = CommonFuncs.TableCount(repo.TableName);
            Assert.AreEqual(preTableCount + 3, tmpTableCount);
        }

        [TestMethod]
        public void SelectAllAttr()
        {
            var attrCollection = repo.SelectAllAttr((int)EntityEnum.User, testInstance1);
            Assert.AreEqual(2, attrCollection.Count);
            Assert.AreEqual(testVal1, attrCollection[0].Value);
            Assert.AreEqual(testVal2, attrCollection[1].Value);
            Assert.AreEqual((int)AttrEnum.UserSiteGroupId, attrCollection[0].AttrId);
            Assert.AreEqual((int)AttrEnum.UserOrganizationId, attrCollection[1].AttrId);
        }

        private static bool compareAttrLists(AttrValueCollection collection, List<EntityInstanceValue> list)
        {
            var isEqual = true;
            foreach (var attrItr in collection)
            {
                var tmp = list.Find(x => x.AttrId == attrItr.AttrId);
                isEqual &= tmp != null && tmp.EntityInstance.Equals(attrItr.EntityInstance) && tmp.Value.Equals(attrItr.Value);
            }
            return isEqual;
        }

        [TestMethod]
        public void SelectValues()
        {
            var attrCollection = repo.SelectAllAttr((int)EntityEnum.User, testInstance1);
            var attrList = repo.SelectValues((int)EntityEnum.User, testInstance1);
            var attrList2 = repo.SelectValues((int)EntityEnum.User, testInstance2);
            var attrList3 = repo.SelectValues((int)EntityEnum.User, "fake");
            var attrList4 = repo.SelectValues((int)EntityEnum.ProcedureAQC, testInstance1);

            Assert.AreEqual(2, attrList.Count);
            Assert.AreEqual(1, attrList2.Count);
            Assert.AreEqual(0, attrList3.Count);
            Assert.AreEqual(0, attrList4.Count);
            Assert.IsTrue(compareAttrLists(attrCollection, attrList));
        }

        [TestMethod]
        public void SelectAttr()
        {
            var attr = repo.SelectAttr((int)AttrEnum.UserSiteGroupId);
            var attrName = "Рабочая группа пунктов пользователя";
            Assert.AreEqual(attrName, attr.Name);
        }

        [TestMethod]
        public void SelectAttrs()
        {
            var enumAttrsCount = Enum.GetNames(typeof(AttrEnum)).Length;
            var attrs = repo.SelectAttrs((int)EntityEnum.User);
            Assert.AreEqual(enumAttrsCount, attrs.Count);
        }

        [TestMethod]
        public void SelectValue()
        {
            var val1 = repo.SelectValue((int)AttrEnum.UserSiteGroupId, testInstance1);
            var val2 = repo.SelectValue((int)AttrEnum.UserOrganizationId, testInstance1);
            var val3 = repo.SelectValue((int)AttrEnum.UserOrganizationId, testInstance2);
            var val4 = repo.SelectValue((int)AttrEnum.UserSiteGroupId, testInstance2);

            Assert.AreEqual(testVal1, val1.Value);
            Assert.AreEqual(testVal2, val2.Value);
            Assert.AreEqual(testVal2, val3.Value);
            Assert.AreEqual(null, val4);
        }

        [TestMethod]
        public void Update()
        {
            var updatedVal1 = "updatedTest";
            repo.Update((int)AttrEnum.UserSiteGroupId, testInstance1, updatedVal1);
            var val1 = repo.SelectValue((int)AttrEnum.UserSiteGroupId, testInstance1);
            Assert.AreEqual(updatedVal1, val1.Value);
        }

        [TestMethod]
        public void SelectInstances()
        {
            var instanceList = repo.SelectInstances((int)EntityEnum.User);
            Assert.IsTrue(instanceList.Contains(testInstance1));
            Assert.IsTrue(instanceList.Contains(testInstance2));
        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            string sql = "Delete From " + repo.TableName + " where entity_instance = '{0}'";
            CommonFuncs.ExecSimpleQuery(string.Format(sql, testInstance1));
            CommonFuncs.ExecSimpleQuery(string.Format(sql, testInstance2));
        }
    }

    [TestClass]
    public class LogTest
    {
        static LogRepository repo;
        static int entityId1 = 1;
        static int entityId2 = 1;
        static string mess1 = "test MSG";
        static string mess2 = "test MSG2";
        static int id1;
        static DateTime sDate;
        static long preTableCount;

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            repo = DataManager.GetInstance(CommonFuncs.GetDefaultConnectionString()).LogRepository;

            preTableCount = CommonFuncs.TableCount(repo.TableName);
            sDate = CommonFuncs.DbCurrDate();
            repo.Insert(entityId1, mess1);
            id1 = CommonFuncs.TableMaxId(repo.TableName);
            repo.Insert(entityId2, mess2, id1, true);
        }

        [TestMethod]
        public void Insert()
        {
            Assert.AreEqual(preTableCount + 2, CommonFuncs.TableCount(repo.TableName));
        }

        [TestMethod]
        public void Select()
        {
            List<Log> arr = repo.Select(sDate.AddSeconds(-1), CommonFuncs.DbCurrDate().AddSeconds(1)).OrderBy(x => x.Id).ToList();
            Assert.AreEqual(arr.Count, 2);
            Assert.AreEqual(arr[0].Entity.Id, entityId1);
            Assert.AreEqual(arr[1].Entity.Id, entityId2);
            Assert.AreEqual(arr[0].Message, mess1);
            Assert.AreEqual(arr[1].Message, mess2);
            Assert.AreEqual(arr[1].ParentId, arr[0].Id);
        }

        [TestMethod]
        public void Delete()
        {
            repo.Delete(sDate.AddSeconds(-1), CommonFuncs.DbCurrDate().AddSeconds(1), entityId1);
            Assert.AreEqual(CommonFuncs.TableCount(repo.TableName), preTableCount);
        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            string sql = "Delete From " + repo.TableName + " where message = '{0}'";
            CommonFuncs.ExecSimpleQuery(string.Format(sql, mess2));
            CommonFuncs.ExecSimpleQuery(string.Format(sql, mess1));
        }
    }
}
