using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SOV.Amur.Meta;
using SOV.Amur.Data;

namespace SOV.Amur.Service
{
    public partial interface IService
    {
        #region META
        /// <summary>
        /// Записать элемент справочника кодов значений для переменной.
        /// <param name="hSvc">Ручка сервиса.</param>
        /// <param name= "vc">элемент справочника кодов значений для переменной.</param>
        /// </summary>
        [OperationContract]
        void SaveVariableCode(long hSvc, VariableCode vc);
        /// <summary>
        /// Вставить или обновить атрибут пункта.
        /// </summary>
        /// <param name="hSvc">Ручка сервиса.</param>
        /// <param name="eav">Значение атрибута пункта.</param>
        [OperationContract]
        void SaveSiteAttribute(long hSvc, EntityAttrValue eav);
        [OperationContract]
        int SaveSite(long hSvc, Site site);
        [OperationContract]
        Catalog SaveCatalog(long hSvc, Catalog catalog);
        #endregion META

        #region DATA
        //////[OperationContract]
        //////void UpdateCurveSeria(long hSvc, int seriaId, DateTime dateS, string description, List<Curve.Seria.Point> points, List<Curve.Seria.Coef> coefs);
        //////[OperationContract]
        //////void SaveCurveSeries(long hSvc, List<Curve.Seria> series);
        //////[OperationContract]
        //////int SaveCurve(long hSvc, Curve curve);
        /// <summary>
        /// Актуализировать значение - установить значению с указанным кодом ЕДИНСТВЕННЫЙ СРЕДИ ВСЕХ ЗНАЧЕНИЙ флаг 40 (Подтверждено специалистом).
        /// 
        /// Алгоритм:
        /// 1) Всем значения с флагом 40 (Подтверждено специалистом) переопределить флаг в 1 (Успешный критконтроль).
        /// 2) Установить значению с указанным кодом флаг 40 (Подтверждено специалистом).
        /// 
        /// OSokolov@SOV.ru 2017.01
        /// </summary>
        /// <param name="hSvc">Ручка сервиса.</param>
        /// <param name="dataValueId">Код актуализируемого значения.</param>
        [OperationContract]
        void ActualizeDataValue(long hSvc, long dataValueId);
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
