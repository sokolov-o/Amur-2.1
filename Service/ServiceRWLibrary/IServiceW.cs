using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using FERHRI.Amur.Meta;
using FERHRI.Amur.Data;

namespace FERHRI.Amur.Service
{
    public partial interface IService
    {
        #region META
        [OperationContract]
        void UpdateStation(long hSvc, Station station);
        /// <summary>
        /// Вставить или обновить атрибут пункта.
        /// </summary>
        /// <param name="hSvc"></param>
        /// <param name="eav"></param>
        [OperationContract]
        void SaveSiteAttribute(long hSvc, EntityAttrValue eav);
        [OperationContract]
        int SaveSite(long hSvc, Site site);
        [OperationContract]
        int SaveStation(long hSvc, Station station);
        [OperationContract]
        Catalog SaveCatalog(long hSvc, Catalog catalog);
        #endregion META

        #region DATA
        [OperationContract]
        long SaveValue(long hSvc, int catalogId, DateTime dateUTC, DateTime dateLOC, double value, byte flagAQC = 0, long? dataSourceId = null);
        [OperationContract]
        long SaveDataValue(long hSvc, DataValue dv);
        [OperationContract]
        void SaveDataValueXSource(long hSvc, int dataValueId, long dataSourceId);
        [OperationContract]
        long SaveDataSource(long hSvc, DataSource dataSource);
        [OperationContract]
        void SaveDataValueList(long hSvc, List<DataValue> dvs, long? dataSourceId);
        [OperationContract]
        void SaveDataForecastList(long hSvc, List<DataForecast> dvs);
        #endregion DATA
        
        #region DATAP
        [OperationContract]
        void SaveDataPRole(long hSvc, long dvId, int roleId, bool isAQCApplied);
        #endregion DATAP

        #region PARSER
        [OperationContract]
        void SaveParserSysObjLastStartParam(long hSvc, int sysObjId, string lastStartParam);
        #endregion PARSER

    }
}
