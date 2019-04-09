using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Amur.Data;
using SOV.Amur.Meta;
using SOV.Amur.Sys;
using SOV.Common;

namespace SOV.Amur.DataP
{

    public static class DataDerived
    {
        public static List<Object[]/*[DataValue2017,List<long>]*/> Derived(Dictionary<DateTime, List<DataValue2017>> dataSrc, List<Variable> varDst,
            int dateTypeIdDst,
            int siteId, int offsetTypeId, double offsetValue,
            int utcOffset, int methodId, int sourceId)
        {
            //определяем catalog_id для полученных данных, если нет, создаем
            byte flagAQC = 0;
            Meta.CatalogRepository cr = Meta.DataManager.GetInstance().CatalogRepository;
            Dictionary<Variable, Catalog> ctlDic = new Dictionary<Variable, Catalog>();
            foreach (var v in varDst)
            {
                var ctls = cr.Select(
                    new List<int> { siteId }, new List<int> { v.Id },
                    new List<int> { methodId }, new List<int> { sourceId },
                    new List<int> { offsetTypeId }, offsetValue);
                switch (ctls.Count)
                {
                    case 0:
                        Catalog ctl = cr.Insert(new Catalog(-1, siteId, v.Id, methodId, sourceId, offsetTypeId, offsetValue));
                        ctlDic.Add(v, ctl);
                        break;
                    case 1:
                        ctlDic.Add(v, ctls[0]);
                        break;
                    default:
                        throw new Exception("ERROR! Catalog is not 1");
                }
            }

            List<Object[]> ret = new List<Object[]>();
            foreach (var date in dataSrc.Keys)
            {
                List<DataValue2017> dataVec;
                if (!dataSrc.TryGetValue(date, out dataVec))
                    continue;
                List<long> vecId = new List<long>();
                List<double> vecData = new List<double>();
                foreach (var d in dataVec.ToList())//.Where(t => t.SiteId == siteId && t.OffsetTypeId == offsetTypeId && t.OffsetValue == offsetValue).ToList())
                {
                    vecId.Add(d.Id);
                    vecData.Add(d.Value);
                }
                if (!(vecId.Count > 0))
                    continue;
                double value = double.NaN;


                foreach (var variable in varDst)
                {
                    value = Derived((Meta.EnumDataType)variable.DataTypeId, vecData);
                    Catalog catalog;
                    if (ctlDic.TryGetValue(variable, out catalog))
                    {
                        DataValue2017 dv = new DataValue2017(-1, catalog.Id, value, dateTypeIdDst, date, flagAQC, utcOffset);
                        ret.Add(new object[] { dv, vecId });
                    }
                }
            }
            return ret;
        }

        public static double Derived(EnumDataType enumDataType, List<double> vecData)
        {
            double value;
            switch (enumDataType)
            {
                case Meta.EnumDataType.Average://Average
                    value = vecData.ToArray().Average();
                    break;
                case Meta.EnumDataType.Maximum://Max
                    value = vecData.ToArray().Max();
                    break;
                case Meta.EnumDataType.Minimum://Min
                    value = vecData.ToArray().Min();
                    break;
                case Meta.EnumDataType.Cumulative://Sum
                    value = vecData.ToArray().Sum();
                    break;
                default:
                    throw new Exception("UNKNOWN DataTypeId=" + enumDataType);
            }
            return value;
        }


        public static void CalcDerived(DerivedTask dt, DateTime? dateS, DateTime? dateF, bool isWriterLog)
        {
            SiteRepository siteRepos = Meta.DataManager.GetInstance().SiteRepository;
            int? siteGroupId = dt.GetSiteGroupId();
            List<Site> sites;

            // Расчет либо для группы пунктов, либо для отдельного пункта.
            // Если не задана ни группа, ни отдельный пункт, то расчет выполняется для всех пунктов заданного в dt типа.
            // Если и типа нет - excewption

            if (siteGroupId != null)
            {
                List<int[]> sitesId = Meta.DataManager.GetInstance().EntityGroupRepository.SelectEntities((int)siteGroupId);
                sites = siteRepos.Select(sitesId.Select(x => x[0]).ToList());
            }
            else
            {
                int? siteId = dt.GetSiteId();
                if (siteId != null)
                    sites = siteRepos.Select(new List<int>() { (int)siteId });
                else
                    sites = siteRepos.SelectByType(dt.SiteTypeId);
            }

            #region DateSF
            if (!dt.IsFcsData)
            {
                if (!dateF.HasValue)
                {
                    dateF = DateTime.Now;
                }
                if (!dateS.HasValue)
                {
                    dateS = dt.GetDateShift(((DateTime)dateF).Date);
                }
            }
            else
            {
                if (!dateS.HasValue)
                {
                    dateS = dt.GetDateShift(DateTime.Now.Date);
                }
                if (!dateF.HasValue)
                {

                    dateF = DateTime.Now.AddHours(72);
                }

            }
            #endregion

            int logId = isWriterLog ? Sys.DataManager.GetInstance().LogRepository.Insert(3, "START task=" + dt.Id, null, false) : -1;

            switch (dt.MethodDerId)
            {
                case 4:
                case 101:
                    foreach (var site in sites)
                    {
                        CalcDerived(dt, (DateTime)dateS, (DateTime)dateF, isWriterLog, site, logId);
                    }
                    break;
                case 50://расчет Дефицит упругости водяного пара
                case 51:
                case 2://Интерполяция с кривой связи Q=f(H)
                    CalcData(dt, (DateTime)dateS, (DateTime)dateF, isWriterLog, sites, logId);
                    break;
            }

            if (isWriterLog)
                Sys.DataManager.GetInstance().LogRepository.Insert(3, "FINISH", logId, false);
        }

        private static void CalcData(DerivedTask dt, DateTime dateS, DateTime dateF, bool isWriterLog, List<Site> sites, int logId)
        {

            CalcData cd = new CalcData(sites, dt.VariableSrcId, dateS, dateF, dt.DateTypeId, dt.OffsetTypeId,
                dt.OffsetValue, (int)dt.MethodSrc, (int)dt.SourceSrc, dt.DataTypeId, dt.MethodDerId, dt.TimeSupport, isWriterLog);
            cd.LogId = logId;
            switch (dt.MethodDerId)
            {
                case 2://Интерполяция с кривой связи Q=f(H)
                    cd.RunInterpQH();
                    break;
                case 50:
                case 51:
                    cd.RunSaturatedVapourPressureDeficit();
                    break;
            }
        }


        public static void CalcDerived(DerivedTask dt, DateTime dateS, DateTime dateF, bool isWriterLog, Site site, int logId)
        {
            switch (dt.TypeTask)
            {
                case Enums.TypeTask.DAY:
                    DerivedDay dd = new DerivedDay(dt.MethodDerId, site.Id, dt.VariableSrcId[0], dateS, dateF, dt.DateTypeId, dt.OffsetTypeId,
                dt.OffsetValue, (int)dt.MethodSrc, (int)dt.SourceSrc, dt.DataTypeId, dt.TimeSupport, isWriterLog);
                    //if (isWriterLog)
                    //dd.InsertLog(null, false, "DerivedDay. TaskId=" + dt.Id + "; SiteId=" + site.Id);
                    dd.LogId = logId;
                    if (dt.IsFcsData)
                        dd.RunFcs();
                    else
                        dd.RunFct();
                    break;
                case Enums.TypeTask.UNIT_TIME:
                    DerivedUnitTime dut = new DerivedUnitTime(dt.MethodDerId, site.Id, dt.VariableSrcId, dateS, dateF, dt.DateTypeId,
                        dt.OffsetTypeId, dt.OffsetValue, dt.MethodSrc, dt.SourceSrc, (EnumTime)dt.UnitTimeId, dt.DataTypeId, dt.TimeSupport, isWriterLog);
                    //if (isWriterLog)
                    //    dut.InsertLog(null, false, "DerivedUnitTime. TaskId=" + dt.Id + "; SiteId=" + site.Id);
                    dut.LogId = logId;
                    dut.Run();
                    break;
            }
        }

        public static int InsertDataValueDer(List<object[/*DataValue2017 , List<long>*/]> dvs, DataValue2017Repository dvr)
        {
            int countInsert = 0;
            foreach (var dataIns in dvs)
            {

                var dvIns = (DataValue2017)dataIns[0];
                DataValue2017 dv = dvr.SelectDataValue(dvIns.CatalogId, dvIns.DateTypeId, dvIns.Date, dvIns.Value);

                List<long> parentIdArr = ((List<long>)dataIns[1]);
                parentIdArr.Sort();
                if (dv != null)
                {
                    var parIds = dvr.SelectDerivedValueId(dv.Id);
                    parIds.Sort();
                    bool isChan = true;
                    if (parentIdArr.Count == parIds.Count)
                    {
                        isChan = false;
                        for (int i = 0; i < parentIdArr.Count; i++)
                        {
                            if (parentIdArr[i] != parIds[i])
                            {
                                isChan = true;
                                continue;
                            }
                        }
                    }
                    if (!isChan)
                        continue;
                    dvr.DeleteDerivedValue(dv.Id);
                    dvr.InsertDerivedValue(dv.Id, parentIdArr);
                }
                else
                {
                    long idIns = dvr.Insert(dvIns);
                    dvr.InsertDerivedValue(idIns, parentIdArr);
                    countInsert++;
                }
            }
            return countInsert;
        }

    }
    public class CalcData : DerivedPar
    {
        private List<Site> Sites;
        private int[] VariableSrcId;
        private DateTime DateS;
        private DateTime DateF;
        private int OffsetTypeId;
        private double OffsetValue;
        private int MethodSrcId;
        private int SourceSrcId;
        private int[] DataTypeId;
        private int TimeSupportDst;
        private int DateTypeId;

        public CalcData(List<Site> sites, int[] variableSrcId, DateTime dateS, DateTime dateF, int dateTypeId, int offsetTypeId, double offsetValue, int methodSrcId,
            int sourceSrcId, int[] dataTypeId, int methodDstId, int timeSupportDst, bool isWriterLog)
            : base(methodDstId, isWriterLog, -1)
        {
            this.Sites = sites;
            this.VariableSrcId = variableSrcId;
            this.DateS = dateS;
            this.DateF = dateF;
            this.OffsetTypeId = offsetTypeId;
            this.OffsetValue = offsetValue;
            this.MethodSrcId = methodSrcId;
            this.SourceSrcId = sourceSrcId;
            this.DataTypeId = dataTypeId;
            this.TimeSupportDst = timeSupportDst;
            this.DateTypeId = dateTypeId;
        }

        internal void RunSaturatedVapourPressureDeficit()
        {
            Meta.DataManager metaDM = Meta.DataManager.GetInstance();
            Data.DataManager dataDM = Data.DataManager.GetInstance();

            //определить varTA и varTd
            Variable varTA = null, varTD = null;
            List<Variable> vars = metaDM.VariableRepository.Select(VariableSrcId.ToList());
            foreach (var v in vars)
            {
                if (v.VariableTypeId == 84)
                    varTD = v;
                if (v.VariableTypeId == 269 && v.SampleMediumId == 1)//температура воздуха
                    varTA = v;
            }
            if (!(varTA != null && varTD != null))
            {
                InsertLog(LogId, true, "VariableSrc not TA and TD. @siteId=");
                return;
            }
            if (!(varTA.GeneralCategoryId == varTD.GeneralCategoryId && varTA.GeneralCategoryId == 2
                && varTA.TimeId == varTD.TimeId// && varTA.TimeId==100
                && varTA.DataTypeId == varTD.DataTypeId && varTA.DataTypeId == 5
                && varTA.SampleMediumId == varTD.SampleMediumId && varTA.SampleMediumId == 1
                && varTA.TimeSupport == varTD.TimeSupport// && varTA.TimeSupport==0
                && varTA.ValueTypeId == varTD.ValueTypeId
                ))
            {
                InsertLog(LogId, true, "Неправильные переменные");
                return;
            }
            //определить новую переменную
            int varTypeIdDst = 276;
            int unitIdDst = 315;
            int valueTypeIdDst = varTA.ValueTypeId == (int)EnumValueType.Forecast ? varTA.ValueTypeId : (int)EnumValueType.DerivedValue;
            Variable varDst = metaDM.VariableRepository.Select(varTypeIdDst, varTA.TimeId, unitIdDst, varTA.DataTypeId, varTA.GeneralCategoryId, varTA.SampleMediumId,
               varTA.TimeSupport, valueTypeIdDst);
            if (varDst == null)
            {
                InsertLog(LogId, true, "Нет dstVariable. @varTypeId=" + varTypeIdDst
                                                        + ";@timeId=" + varTA.TimeId
                                                        + ";@utitId=" + unitIdDst
                                                        + ";@dataTypeId=" + varTA.DataTypeId
                                                        + ";@generalCategoryId=" + varTA.GeneralCategoryId
                                                        + ";@sampleMediumId" + varTA.SampleMediumId
                                                        + ";@timeSupport" + varTA.TimeSupport
                                                        + ";@valueTypeId" + valueTypeIdDst);
                return;
            }

            Dictionary<int/*siteId*/, Catalog> catalogSrcTA = metaDM.CatalogRepository.Select(
                Sites.Select(t => t.Id).ToList(), new List<int>() { varTA.Id },
                new List<int>() { MethodSrcId }, new List<int>() { SourceSrcId },
                    new List<int>() { OffsetTypeId }, OffsetValue).ToDictionary(t => t.SiteId);

            Dictionary<int/*siteId*/, Catalog> catalogSrcTD = metaDM.CatalogRepository.Select(
                Sites.Select(t => t.Id).ToList(), new List<int>() { varTD.Id },
                new List<int>() { MethodSrcId }, new List<int>() { SourceSrcId },
                new List<int>() { OffsetTypeId }, OffsetValue).ToDictionary(t => t.SiteId);

            Dictionary<int/*siteId*/, Catalog> catalogDst = metaDM.CatalogRepository.Select(
                Sites.Select(t => t.Id).ToList(), new List<int>() { varDst.Id },
                new List<int>() { MethodDstId }, new List<int>() { SourceDstId },
                new List<int>() { OffsetTypeId }, OffsetValue).ToDictionary(t => t.SiteId);
            foreach (var site in Sites)
            {

                //определить catalogId для обеих переменных
                Catalog ctlTASrc, ctlTDSrc, ctlDst;
                if (!catalogSrcTA.TryGetValue(site.Id, out ctlTASrc) || !catalogSrcTD.TryGetValue(site.Id, out ctlTDSrc))
                {
                    InsertLog(LogId, true, "Нет CatalogIdSrc. @siteId=" + site.Id);
                    continue;
                }
                if (!catalogDst.TryGetValue(site.Id, out ctlDst))
                {
                    ctlDst = metaDM.CatalogRepository.Insert(new Catalog(-1, site.Id, varDst.Id, MethodDstId, SourceDstId, OffsetTypeId, OffsetValue));
                    catalogDst.Add(site.Id, ctlDst);
                }
                switch (varTA.ValueTypeId)
                {
                    case (int)EnumValueType.Forecast://forecast 

                        List<DataForecast> retFcs = new List<DataForecast>();
                        //считать
                        List<DataForecast2> fcsData = dataDM.DataForecastRepository.SelectDataForecasts2(ctlTASrc.Id, ctlTDSrc.Id, DateS, DateF, null, true);
                        // Расчитать дефицит по исходным прогнозам Ta и Td
                        foreach (var item in fcsData)
                        {
                            //проверку на градусы цельсия!!!
                            if (varTA.UnitId != varTD.UnitId || varTA.UnitId != 96)
                                throw new NotImplementedException();
                            retFcs.Add(new DataForecast(ctlDst.Id,
                                item.LagFcs, item.DateFcs, item.DateIni,
                                Common.Phisics.GetSaturatedVapourPressureDeficit(item.Value1, item.Value2), DateTime.Now)
                            );
                        }

                        foreach (var dfcs in retFcs)
                        {
                            var fcs = dataDM.DataForecastRepository.Select(ctlDst.Id, dfcs.DateFcs, dfcs.DateFcs, dfcs.LagFcs, true);
                            if (fcs.Count > 0)
                                continue;
                            dataDM.DataForecastRepository.Insert(dfcs);

                        }
                        break;
                    case (int)EnumValueType.FieldObservation:
                        List<DataValue2017> retFct = new List<DataValue2017>();
                        List<DataValue2017> src = dataDM.DataValue2017Repository.SelectA(DateS, DateF, new List<int>() { ctlTASrc.Id, ctlTDSrc.Id },
                            true, false, null, DateTypeId);
                        Dictionary<DateTime, DataValue2017[]> srcData = new Dictionary<DateTime, DataValue2017[]>();
                        foreach (var dv in src)
                        {
                            DataValue2017[] dvR;
                            int ind = -1;
                            if (!srcData.TryGetValue(dv.Date, out dvR))
                            {
                                dvR = new DataValue2017[2] { null, null };
                                srcData.Add(dv.Date, dvR);
                            }
                            ind = dv.CatalogId == ctlTASrc.Id ? 0 : 1;
                            dvR[ind] = dv;
                        }

                        List<Object[]> dvs = new List<object[]>();
                        foreach (var date in srcData.Keys)
                        {
                            DataValue2017[] dvR = srcData[date];
                            if (dvR[0] != null && dvR[1] != null && dvR[0].UTCOffset == dvR[1].UTCOffset)//????
                            {
                                double value = Common.Phisics.GetSaturatedVapourPressureDeficit(dvR[0].Value, dvR[1].Value);
                                dvs.Add(
                                new Object[] { new DataValue2017(-1, ctlDst.Id, value,DateTypeId, date,0, dvR[0].UTCOffset) ,
                                    new List<long>(){dvR[0].Id,dvR[1].Id}}
                                );
                            }
                        }
                        Stopwatch sw = Stopwatch.StartNew();
                        int countInsert = DataDerived.InsertDataValueDer(dvs, dataDM.DataValue2017Repository);
                        sw.Stop();
                        Console.WriteLine("Year " + DateS.Year + ". Insert " + countInsert + " rows " + sw.Elapsed.TotalSeconds + " sec");
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        internal void RunInterpQH()
        {
            Meta.DataManager metaDM = Meta.DataManager.GetInstance();
            Data.DataManager dataDM = Data.DataManager.GetInstance();

            int curveSeriaType = 1;//оперативная кривая

            if (VariableSrcId.Length != 1)
                throw new Exception("VariableSrcId.Length != 1");
            Variable varH = metaDM.VariableRepository.Select(VariableSrcId[0]);
            Variable varQ = metaDM.VariableRepository.Select(14);
            //определить новую переменную
            int varTypeIdDst = 88;
            int unitIdDst = 36;
            int dataTypeDstId = 14;
            int valueTypeIdDst = (int)EnumValueType.DerivedValue;
            Variable varDst = metaDM.VariableRepository.Select(varTypeIdDst, varH.TimeId, unitIdDst, dataTypeDstId, varH.GeneralCategoryId, varH.SampleMediumId,
               varH.TimeSupport, valueTypeIdDst);
            if (varDst == null)
            {
                InsertLog(LogId, true, "Нет dstVariable. @varTypeId=" + varTypeIdDst
                                                        + ";@timeId=" + varH.TimeId
                                                        + ";@utitId=" + unitIdDst
                                                        + ";@dataTypeId=" + dataTypeDstId
                                                        + ";@generalCategoryId=" + varH.GeneralCategoryId
                                                        + ";@sampleMediumId" + varH.SampleMediumId
                                                        + ";@timeSupport" + varH.TimeSupport
                                                        + ";@valueTypeId" + valueTypeIdDst);
                return;
            }

            Dictionary<int/*siteId*/, Catalog> catalogSrcH = metaDM.CatalogRepository.Select(
                Sites.Select(t => t.Id).ToList(), new List<int>() { varH.Id },
                new List<int>() { MethodSrcId }, new List<int>() { SourceSrcId },
                    new List<int>() { OffsetTypeId }, OffsetValue).ToDictionary(t => t.SiteId);
            Dictionary<int/*siteId*/, Catalog> catalogSrcQ = metaDM.CatalogRepository.Select(
                Sites.Select(t => t.Id).ToList(), new List<int>() { varQ.Id },
                new List<int>() { MethodSrcId }, new List<int>() { SourceSrcId },
                    new List<int>() { OffsetTypeId }, OffsetValue).ToDictionary(t => t.SiteId);

            Dictionary<int/*siteId*/, Catalog> catalogDst = metaDM.CatalogRepository.Select(
                Sites.Select(t => t.Id).ToList(), new List<int>() { varDst.Id },
                new List<int>() { MethodDstId }, new List<int>() { SourceDstId },
                new List<int>() { OffsetTypeId }, OffsetValue).ToDictionary(t => t.SiteId);

            Dictionary<int/*siteId*/, List<Catalog>> catalogSrcHData = metaDM.CatalogRepository.Select(
               Sites.Select(t => t.Id).ToList(), new List<int>() { varH.Id },
               new List<int>() { MethodSrcId }, null,
                   new List<int>() { OffsetTypeId }, OffsetValue).GroupBy(t => t.SiteId).ToDictionary(t => t.Key, q => q.ToList());

            foreach (var site in Sites)
            {
                int countRow = 0;
                Console.WriteLine();
                Console.Write("site " + site.Code + ".");
                //определить catalogId для обеих переменных
                Catalog ctlHSrc, ctlQSrc, ctlDst;
                if (!catalogSrcH.TryGetValue(site.Id, out ctlHSrc) || !catalogSrcQ.TryGetValue(site.Id, out ctlQSrc))
                {
                    InsertLog(LogId, true, "Нет CatalogIdSrc. @siteId=" + site.Id);
                    continue;
                }
                if (!catalogDst.TryGetValue(site.Id, out ctlDst))
                {
                    ctlDst = metaDM.CatalogRepository.Insert(new Catalog(-1, site.Id, varDst.Id, MethodDstId, SourceDstId, OffsetTypeId, OffsetValue));
                    catalogDst.Add(site.Id, ctlDst);
                }
                switch (varH.ValueTypeId)
                {
                    case (int)EnumValueType.Forecast://forecast 
                        throw new Exception("Not supported valueType=forecast!");
                    case (int)EnumValueType.FieldObservation:
                        //находим кривую для исходных данных
                        List<Curve> curveListHX = dataDM.CurveRepository.SelectCurvesByCatalog(ctlHSrc.Id, ctlQSrc.Id, curveSeriaType, DateS, DateF, true);
                        List<Curve> curveListHY = dataDM.CurveRepository.SelectCurvesByCatalog(ctlQSrc.Id, ctlHSrc.Id, curveSeriaType, DateS, DateF, true);
                        if (curveListHX.Count + curveListHY.Count != 1)
                        {
                            InsertLog(LogId, true, "Ошибка кривой для CatalogIdSrc=" + ctlHSrc.Id + " в интервале дат c " + DateS.ToShortDateString() + " по " + DateF.ToShortDateString());
                            continue;
                        }
                        Curve curve = (curveListHX.Union(curveListHY).ToList())[0];
                        bool isHX = false;
                        if (curve.CatalogIdX == ctlHSrc.Id)
                            isHX = true;

                        List<Catalog> ctlHSrcData;
                        if (!catalogSrcHData.TryGetValue(site.Id, out ctlHSrcData))
                            continue;

                        //считываем исходные данные
                        //List<DataValue2017> src = dataDM.DataValue2017Repository.SelectA(DateS, DateF, new List<int>() { ctlHSrc.Id },
                        //    true, false, null, DateTypeId);

                        List<DataValue> src = dataDM.DataValueRepository.SelectA(DateS, DateF, ctlHSrcData.Select(t => t.Id).ToList(), true, false, null, DateTypeId == 2 ? true : false);
                        //
                        //List<Object[]> dvs = new List<object[]>();

                        foreach (var dv in src)
                        {
                            //DateTime dateCur = dv.Date;
                            DateTime dateCur = DateTypeId == 2 ? dv.DateLOC : dv.DateUTC;
                            double srcValue = dv.Value;
                            Curve.Seria ser = Curve.Seria.Coef.GetSeria4Date(curve.Series, dateCur);
                            if (ser == null)
                                continue;
                            List<Curve.Seria.Point> poins = ser.Points.Select(t => new Curve.Seria.Point() { X = isHX ? t.X : t.Y, Y = isHX ? t.Y : t.X }).ToList();
                            poins.Sort((x, y) => x.X.CompareTo(y.X));
                            double minH = poins[0].X;
                            double maxH = poins[poins.Count - 1].X;
                            if (srcValue < minH || srcValue > maxH)
                                continue;
                            double dstValue = double.NaN;
                            for (int i = 1; i < poins.Count; i++)
                            {
                                if (srcValue >= poins[i - 1].X && srcValue <= poins[i].X)
                                {
                                    dstValue = Common.MathSupport.InterpolateLine(poins[i - 1].X, poins[i].X, poins[i - 1].Y, poins[i].Y, srcValue);
                                    break;
                                }
                            }
                            if (double.IsNaN(dstValue))
                                continue;

                            DataValue dvTest = dataDM.DataValueRepository.SelectDataValue(ctlDst.Id, dv.DateUTC, dstValue);

                            long dvId = dataDM.DataValueRepository.Insert(new DataValue(-1, ctlDst.Id, dstValue, dv.DateLOC, dv.DateUTC, 0, dv.UTCOffset));
                            if (dvTest != null && dvTest.Id == dvId)
                                dataDM.DataValueRepository.DeleteDerivedValue(dvId);
                            dataDM.DataValueRepository.InsertDerivedValue(dvId, new List<long>() { dv.Id });
                            countRow++;
                            //dvs.Add(
                            //new Object[] { new DataValue2017(-1, ctlDst.Id, dstValue,DateTypeId, dateCur,0, dv.UTCOffset) ,
                            //        new List<long>(){dv.Id}}
                            //);

                        }
                        //int countInsert = DataDerived.InsertDataValueDer(dvs, dataDM.DataValue2017Repository);
                        break;
                    default:
                        throw new NotImplementedException();
                }
                Console.Write("insert " + countRow + " row(s).");
            }
        }
    }
    public abstract class DerivedPar
    {
        private SysEntityRepository SysRep { get; set; }
        private LogRepository LogRep { get; set; }
        private const int ENTITY_ID = 3;
        protected int SourceDstId { get; set; }
        protected int MethodDstId { get; set; }

        /// <summary>
        /// Записывать log?
        /// </summary>
        public bool IsWriteLog { get; internal set; }
        public int LogId { get; set; }

        public DerivedPar(int methodDstId, bool isWriteLog, int logId)
        {
            IsWriteLog = isWriteLog;
            MethodDstId = methodDstId;
            LogId = logId;
            SysRep = Sys.DataManager.GetInstance().SysEntityRepository;
            LogRep = Sys.DataManager.GetInstance().LogRepository;
            SourceDstId = SelectSourceId();
        }
        private int SelectSourceId()
        {
            var user = ADbNpgsql.GetUser(Sys.DataManager.GetDefaultConnectionString());
            var sys = SysRep.SelectValue(3, user.Name);
            if (sys != null)
                return int.Parse(sys.Value);
            else
                throw new Exception("ERROR in SourseId!");
        }
        public void InsertLog(int? parentId, bool isUrgent, string message)
        {
            if (IsWriteLog)
                if (parentId.HasValue)
                    LogRep.Insert(ENTITY_ID, message, parentId, isUrgent);
                else
                    LogId = LogRep.Insert(ENTITY_ID, message, null, isUrgent);
        }
    }

    public class DerivedUnitTime : DerivedPar
    {
        /// <summary>
        /// Пункт, для которого проводится агрегация данных
        /// </summary>
        public int SiteId { get; set; }
        /// <summary>
        /// Переменная, для которой проводится агрегация данных.
        /// </summary>
        public int[] VariablesId { get; set; }
        /// <summary>
        /// дата начала периода выборки данных.
        /// </summary>
        public DateTime DateS { get; set; }
        /// <summary>
        /// дата окончания периода выборки данных.
        /// </summary>
        public DateTime DateF { get; set; }

        public int OffsetTypeId { get; set; }
        public double OffsetValue { get; set; }

        public int? MethodIdIn { get; set; }
        public int? SourceIdIn { get; set; }
        public int TimeSupport { get; set; }
        public int[] DataTypeId { get; set; }
        public EnumTime TimeId { get; set; }
        public int SrcDateTypeId { get; set; }

        public DerivedUnitTime(int methodDstId, int siteId, int[] variablesIdSrc, DateTime dateS, DateTime dateF, int dateTypeId,
                          int offsetTypeId, double offsetValue, int? methodIdSrc, int? sourceIdSrc, EnumTime unitTimeId,
            int[] dataTypeSrcId, int timeSupport, bool isWriteLog)
            : base(methodDstId, isWriteLog, -1)
        {
            SiteId = siteId;
            VariablesId = variablesIdSrc;
            DateS = dateS;
            DateF = dateF;
            OffsetTypeId = offsetTypeId;
            OffsetValue = offsetValue;
            MethodIdIn = methodIdSrc;
            SourceIdIn = sourceIdSrc;
            DataTypeId = dataTypeSrcId;
            TimeSupport = timeSupport;
            TimeId = unitTimeId;
            SrcDateTypeId = dateTypeId;
        }

        public static List<int[/*месяц начала;месяц окончания*/]> ParseSeasonMonthes(string seasMonthStr)
        {
            List<int[]> ret = new List<int[]>();

            string[] s = seasMonthStr.Split(new char[] { ';' });
            bool isEmpty = false;

            for (int i = 0; i < s.Length; i++)
            {
                // CHECK FORMAT
                if (string.IsNullOrEmpty(s[i]))
                {
                    isEmpty = true;
                    continue;
                }
                if (isEmpty)
                    throw new Exception("Некорректный формат строки с данными о месяцах начала сезонов: " + seasMonthStr);

                ret.Add(new int[] { int.Parse(s[i]), 0 });
            }
            ret[0][1] = ret[0][0] - 1;
            ret[ret.Count - 1][1] = ret[0][0] - 1;
            for (int i = 1; i < ret.Count - 1; i++)
            {
                ret[i][1] = ret[i + 1][0] - 1;
            }
            for (int i = 0; i < ret.Count; i++)
            {
                ret[i][1] = (ret[i][1] < 1) ? 12 : ret[i][1];
            }

            return ret;
        }

        public void Run()
        {
            // Решить  проблему с utcOffset = 0;
            byte flagAQC = 1;
            int utcOffset = 0;
            DataValue2017Repository dvr = Data.DataManager.GetInstance().DataValue2017Repository;
            VariableRepository vr = Meta.DataManager.GetInstance().VariableRepository;
            EntityAttrRepository siteAR = Meta.DataManager.GetInstance().EntityAttrRepository;
            EntityAttrValue siteAttrValue = siteAR.SelectAttrValue("site", SiteId, (int)EnumSiteAttrType.HydroSeasonMonthStart, DateTime.Now);
            List<int[]> seasonMonthes = (siteAttrValue != null) ? ParseSeasonMonthes(siteAttrValue.Value) : null;
            List<Variable> varDst = new List<Variable>();
            for (int i = 0; i < DataTypeId.Length; i++)
            {
                Variable v = null;
                for (int j = 0; j < VariablesId.Length; j++)
                {
                    Variable vI = vr.GetVariableDerived(VariablesId[j], null, (int)TimeId, DataTypeId[i], TimeSupport);
                    if (vI == null)
                    {
                        v = null;
                        InsertLog(LogId, true, "Нет variableIdDst! @variabliIdSrc=" + VariablesId[j] +
                        ";@unitTime=" + TimeId + ";@dataType=" + (EnumDataType)DataTypeId[i] + ";@timeSupport=" + TimeSupport);
                        break;
                    }
                    if (i > 0 && v.Id != vI.Id)
                    {
                        v = null;
                        InsertLog(LogId, true, "VariableIdDst Error! @variabliIdSrc=" + VariablesId[0] + "," + VariablesId[j] +
                        ";@unitTime=" + TimeId + ";@dataType=" + (EnumDataType)DataTypeId[i] + ";@timeSupport=" + TimeSupport);
                        break;
                    }
                    v = vI;
                }
                if (v != null)
                    varDst.Add(v);
            }
            if (varDst.Count == 0)
                return;
            //DateTime dateS = UnitTime.GetDateSTimeNum(DateS, (int)UnitTimeId, seasonMonthes, isDay820);
            //DateTime dateF = UnitTime.GetDateSTimeNum(DateF.AddDays(1), (int)UnitTimeId, seasonMonthes, isDay820).AddSeconds(-1);
            Dictionary<DateTime, bool> dataExist = new Dictionary<DateTime, bool>();
            DateTime date = DateS.Date;
            while (date <= DateF)
            {
                dataExist.Add(date, false);
                date = date.AddDays(1);
            }
            List<DataValue2017> dataSrc = new List<DataValue2017>();
            bool isNulls = false;
            foreach (var variableId in VariablesId)
            {
                Variable v = vr.Select(variableId);
                if (v.TimeId != (int)EnumTime.Day)
                    throw new Exception("SrcVariable.UnitsTime!=Day");
                List<DataValue2017> dataSrcI = dvr.SelectA(DateS, DateF, SrcDateTypeId,
                    new List<int>() { SiteId }, new List<int>() { variableId },
                    new List<int>() { OffsetTypeId }, OffsetValue,
                    true, false,
                    MethodIdIn.HasValue ? new List<int>() { (int)MethodIdIn } : null,
                    SourceIdIn.HasValue ? new List<int>() { (int)SourceIdIn } : null,
                        flagAQC);
                foreach (var dv in dataSrcI)
                {
                    DateTime dateTest = dv.Date;
                    bool isEx;
                    if (dataExist.TryGetValue(dateTest.Date, out isEx))
                    {
                        if (!isEx)
                        {
                            dataSrc.Add(dv);
                            dataExist[dateTest.Date] = true;
                        }
                    }
                }
                isNulls = false;
                foreach (var d in dataExist.Keys)
                {
                    if (!dataExist[d])
                    {
                        isNulls = true;
                        break;
                    }
                }
                if (!isNulls)
                    break;
            }
            if (dataSrc.Count > 0)
            {
                DateTime dateS = dataSrc.Min(t => t.Date);
                DateTime dateF = dataSrc.Max(t => t.Date);
                if (dateS.Date > Time.GetDateSTimeNum(dateS, (int)TimeId, seasonMonthes))
                    dateS = Time.GetDateSNextTimeNum(dateS.Date, (int)TimeId, seasonMonthes);
                dateF = Time.GetDateSTimeNum(dateF.AddDays(1), (int)TimeId, seasonMonthes).AddSeconds(-1);
                Dictionary<DateTime, List<DataValue2017>> data = new Dictionary<DateTime, List<DataValue2017>>();
                foreach (var dv in dataSrc)
                {
                    DateTime dateI = dv.Date;
                    if (!(dateI >= dateS && dateI <= dateF))
                        continue;
                    DateTime dateTNS = Time.GetDateSTimeNum(dateI, (int)TimeId, seasonMonthes);
                    List<DataValue2017> dvs;
                    if (data.TryGetValue(dateTNS, out dvs))
                        dvs.Add(dv);
                    else
                        data.Add(dateTNS, new List<DataValue2017>() { dv });
                }


                List<object[]> dvsDer = DataDerived.Derived(data, varDst, 8/*NOT APPLICABLE*/, SiteId, OffsetTypeId, OffsetValue, utcOffset, MethodDstId, SourceDstId);
                int countInsert = 0;
                foreach (var dataIns in dvsDer)
                {
                    var dvIns = (DataValue2017)dataIns[0];
                    DataValue2017 dv = dvr.SelectDataValue(dvIns.CatalogId, dvIns.DateTypeId, dvIns.Date, dvIns.Value);
                    List<long> parentIdArr = ((List<long>)dataIns[1]);
                    parentIdArr.Sort();
                    if (dv != null)
                    {
                        var parIds = dvr.SelectDerivedValueId(dv.Id);
                        parIds.Sort();
                        bool isChan = true;
                        if (parentIdArr.Count == parIds.Count)
                        {
                            isChan = false;
                            for (int i = 0; i < parentIdArr.Count; i++)
                            {
                                if (parentIdArr[i] != parIds[i])
                                {
                                    isChan = true;
                                    continue;
                                }
                            }
                        }
                        if (!isChan)
                            continue;
                    }
                    long idIns = dvr.Insert(dvIns);
                    dvr.InsertDerivedValue(idIns, parentIdArr);
                    countInsert++;

                }
                //InsertLog(LogId, false, "Insert " + countInsert + " rows.");
            }
            else
                InsertLog(LogId, false, "No source data. @siteId=" + SiteId);
            //InsertLog(LogId, false, "Finish");
        }


    }

    public class DerivedDay : DerivedPar
    {
        /// <summary>
        /// Пункт, для которого проводится контроль данных. Либо все пункты, если null.
        /// </summary>
        public int SiteId { get; set; }
        /// <summary>
        /// Переменная, для которой проводится контроль данных.
        /// </summary>
        public int VariableId { get; set; }
        /// <summary>
        /// дата начала периода выборки данных.
        /// </summary>
        public DateTime DateS { get; set; }
        /// <summary>
        /// дата окончания периода выборки данных.
        /// </summary>
        public DateTime DateF { get; set; }
        public int DateTypeDstId { get; set; }
        public int OffsetTypeId { get; set; }
        public double OffsetValue { get; set; }

        public int MethodSrcSetId { get; set; }
        public int SourceSrcSetId { get; set; }
        public int TimeSupport { get; set; }
        public int[] DstDataTypeId { get; set; }
        public EnumTime UnitsTime = EnumTime.Day;// { get; set; }

        public DerivedDay(int methodDstId, int siteId, int variableSrcId, DateTime dateS, DateTime dateF, int dateTypeDstId,
                          int offsetTypeId, double offsetValue, int methodSrcSetId, int sourceSrcSetId, int[] dataTypeId,
            int timeSupport, bool isWriteLog)
            : base(methodDstId, isWriteLog, -1)
        {
            SiteId = siteId;
            VariableId = variableSrcId;
            DateTypeDstId = dateTypeDstId;
            DateS = dateS;
            DateF = dateF;
            OffsetTypeId = offsetTypeId;
            OffsetValue = offsetValue;
            MethodSrcSetId = methodSrcSetId;
            SourceSrcSetId = sourceSrcSetId;
            DstDataTypeId = dataTypeId;
            TimeSupport = timeSupport;
        }

        public void RunFct()
        {
            byte[] flagAQC = new byte[] { 0, 1 };
            int utcOffset = 0;
            DataValue2017Repository dvr = Data.DataManager.GetInstance().DataValue2017Repository;
            VariableRepository vr = Meta.DataManager.GetInstance().VariableRepository;
            DerivedDayAttrRepository ddaR = DataP.DataManager.GetInstance().DerivedDayAttrRepository;

            List<Variable> varDst = new List<Variable>();
            for (int i = 0; i < DstDataTypeId.Length; i++)
            {
                Variable v = vr.GetVariableDerived(VariableId, null, (int)UnitsTime, DstDataTypeId[i], TimeSupport);
                if (v != null)
                    varDst.Add(v);
                else
                    InsertLog(LogId, true, "Нет variableIdDst! @siteId=" + SiteId + ";@variabliIdSrc=" + VariableId +
                        ";@dataTypeId={" + string.Join(",", DstDataTypeId) + "};@timeSupport=" + TimeSupport);
            }
            if (varDst.Count == 0)
            {
                InsertLog(LogId, false, "Finish whith error.");
                return;
            }
            if (!ddaR.IsExistDerivedDaySiteAttr(MethodDstId, SiteId, VariableId, DateS, DateF, OffsetTypeId, OffsetValue))
            {
                InsertLog(LogId, true, "Нет записи в таблице datap.derived_day_site_attr. "
                    + "@SiteId=" + SiteId
                    + ";@variabliId=" + VariableId
                    + ";@TimeId=" + UnitsTime
                    + ";@OffsetTypeId=" + OffsetTypeId
                    + ";@OffsetValue=" + OffsetValue);
                InsertLog(LogId, false, "Finish whith error.");
                return;
            }
            Dictionary<DateTime, List<DataValue2017>> dataSrc = dvr.SelectDataForDerivedDay(MethodDstId, SiteId, VariableId, DateS, DateF, flagAQC.ToList(),
                OffsetTypeId, OffsetValue, MethodSrcSetId, SourceSrcSetId);

            if (dataSrc.Count > 0)
            {
                List<object[]> dvs = DataDerived.Derived(dataSrc, varDst, DateTypeDstId, SiteId,
                    OffsetTypeId, OffsetValue, utcOffset, MethodDstId, SourceDstId);
                int countInsert = DataDerived.InsertDataValueDer(dvs, dvr);

                //sw.Stop();
                //InsertLog(LogId, false, "Insert " + countInsert + " rows.");
            }
            else
            {
                Console.WriteLine("No source data. @siteId=" + SiteId);
                //InsertLog(LogId, false, "No source data. @siteId=" + SiteId);
            }
            //Console.WriteLine("Total Write " + countInsert + " rows = " + sw.Elapsed.TotalMilliseconds);
            //InsertLog(LogId, false, "Finish");
        }

        public void RunFcs()
        {
            Meta.DataManager metaDM = Meta.DataManager.GetInstance();
            Data.DataManager dataDM = Data.DataManager.GetInstance();
            DataP.DataManager dataPDM = DataP.DataManager.GetInstance();



            List<Variable> varDst = new List<Variable>();
            for (int i = 0; i < DstDataTypeId.Length; i++)
            {
                Variable v = metaDM.VariableRepository.GetVariableDerived(VariableId, null, (int)UnitsTime, DstDataTypeId[i], TimeSupport);
                if (v != null)
                    varDst.Add(v);
                else
                    InsertLog(LogId, true, "Нет variableIdDst! @siteId=" + SiteId + ";@variabliIdSrc=" + VariableId +
                        ";@dataTypeId={" + string.Join(",", DstDataTypeId) + "};@timeSupport=" + TimeSupport);
            }
            if (varDst.Count == 0)
            {
                InsertLog(LogId, false, "Finish whith error.");
                return;
            }

            #region catalog Dictionary Dst
            Dictionary<Variable, Catalog> ctlDstDic = new Dictionary<Variable, Catalog>();
            foreach (var v in varDst)
            {
                var ctls = metaDM.CatalogRepository.Select(
                    new List<int> { SiteId }, new List<int> { v.Id },
                    new List<int> { MethodDstId }, new List<int> { SourceDstId },
                    new List<int> { OffsetTypeId }, OffsetValue);
                switch (ctls.Count)
                {
                    case 0:
                        Catalog ctl = metaDM.CatalogRepository.Insert(new Catalog(-1, SiteId, v.Id, MethodDstId, SourceDstId, OffsetTypeId, OffsetValue));
                        ctlDstDic.Add(v, ctl);
                        break;
                    case 1:
                        ctlDstDic.Add(v, ctls[0]);
                        break;
                    default:
                        throw new Exception("ERROR! Catalog is not 1");
                }
            }
            #endregion

            MethodForecast methodFcsDst = metaDM.MethodForecastRepository.Select(MethodDstId);
            MethodForecast methodFcsSrc = metaDM.MethodForecastRepository.Select(MethodSrcSetId);
            Variable varSrc = metaDM.VariableRepository.Select(VariableId);
            int countFcsInDay = (int)methodFcsSrc.MaxPerDayCount;
            if (varSrc.TimeId == 100 && varSrc.TimeSupport != 0)
                countFcsInDay = 24 * 60 * 60 / varSrc.TimeSupport;

            double percentNullMax = 0;
            int countMaxNull = (int)Math.Round((double)countFcsInDay * percentNullMax / 100);

            Site site = metaDM.SiteRepository.Select(SiteId);
            //////Station stn = metaDM.StationRepository.Select(site.StationId);
            var mz = metaDM.EntityAttrRepository.SelectAttrValues("site", new List<int>() { SiteId }, new List<int>() { (int)EnumSiteAttrType.MeteoZoneId }, DateS);
            int meteoZoneId;
            if (mz.Count != 1 || !int.TryParse(mz[0].Value, out meteoZoneId))
            {
                InsertLog(LogId, true, "Нет корректного meteoZoneId! @siteId=" + SiteId + "@dateActual=" + DateS.ToShortDateString());
                return;
            }

            List<EntityAttrValue> siteAttrValue = metaDM.EntityAttrRepository.SelectAttrValues("site", new List<int>() { SiteId },
                new List<int>() { (int)EnumSiteAttrType.UTCOffset });
            EntityAttrValue meteoZoneEAV = EntityAttrValue.GetEntityAttrValue(siteAttrValue, SiteId, (int)EnumSiteAttrType.MeteoZoneId, DateS);

            DerivedDayAttr dda = dataPDM.DerivedDayAttrRepository.Select(MethodDstId, DateTypeDstId, site.TypeId, meteoZoneId, VariableId, OffsetTypeId, OffsetValue);
            if (!(dda != null))
                dda = dataPDM.DerivedDayAttrRepository.Select(MethodDstId, DateTypeDstId, site.TypeId, -1, VariableId, OffsetTypeId, OffsetValue);
            if (!(dda != null))
            {
                InsertLog(LogId, true, "Нет записи в таблице datap.derived_day_site_attr. "
                    + "@methodId" + MethodDstId
                    + ";@siteId=" + SiteId
                    + ";@variabliId=" + VariableId
                    + ";@OffsetTypeId=" + OffsetTypeId
                    + ";@OffsetValue=" + OffsetValue);
                InsertLog(LogId, false, "Finish whith error.");
                return;
            }

            DateTime dateSFcs, dateFFcs;
            bool isDateTypeEqual;
            List<EntityAttrValue> utcOffsetValue = null;
            //если считать надо по дате, в которой хранятся прогнозы
            if ((dda.SrcDateTypeId == 1) || (dda.SrcDateTypeId != 1))
            {
                dateSFcs = dda.IsIncludeF ? DateS.Date.AddHours(dda.HourAddForStart).AddSeconds(1) : DateS.Date.AddHours(dda.HourAddForStart);
                dateFFcs = dda.IsIncludeF ? DateF.Date.AddDays(1).AddHours(dda.HourAddForStart) : DateF.Date.AddDays(1).AddHours(dda.HourAddForStart).AddSeconds(-1);
                isDateTypeEqual = true;
            }
            else
            {
                isDateTypeEqual = false;
                throw new NotImplementedException();

                List<EntityAttrValue> utcOffsetValueAll = metaDM.EntityAttrRepository.SelectAttrValues("site", new List<int>() { SiteId },
                    new List<int>() { (int)EnumSiteAttrType.UTCOffset }, null);
                //if (methodFcsSrc.IsDateFcsUTC) //если прогнозы в utc, а считать надо в локальной дате
                //{
                //определяем нужные нам локальные даты
                dateSFcs = dda.IsIncludeF ? DateS.Date.AddHours(dda.HourAddForStart).AddSeconds(1) : DateS.Date.AddHours(dda.HourAddForStart);
                dateFFcs = dda.IsIncludeF ? DateF.Date.AddDays(1).AddHours(dda.HourAddForStart) : DateF.Date.AddDays(1).AddHours(dda.HourAddForStart).AddSeconds(-1);
                //приводим их к utc
                #region DateSF
                DateTime? d = dateLoc2dateUtc(utcOffsetValueAll, dateSFcs);
                if (d != null)
                {
                    dateSFcs = (DateTime)d;
                    var utcOffsetEAV = EntityAttrValue.GetEntityAttrValue(utcOffsetValueAll, SiteId, (int)EnumSiteAttrType.UTCOffset, dateSFcs);
                    utcOffsetValue = utcOffsetValueAll.Where(t => t.DateS >= utcOffsetEAV.DateS).ToList();
                }
                else
                {
                    InsertLog(LogId, true, "Нет utcOffset.@siteId=" + SiteId + "@dateLOC=" + dateSFcs);
                    return;
                }
                d = dateLoc2dateUtc(utcOffsetValueAll, dateFFcs);
                if (d != null)
                    dateFFcs = (DateTime)d;
                else
                {
                    InsertLog(LogId, true, "Нет utcOffset.@siteId=" + SiteId + "@dateLOC=" + dateFFcs);
                    return;
                }
                #endregion

                //}
                //else//если прогнозы в локальной дате
                //{
                //    //определяем нужные нам даты utc
                //    dateSFcs = dda.IsIncludeF ? DateS.Date.AddHours(dda.HourAddForStart).AddSeconds(1) : DateS.Date.AddHours(dda.HourAddForStart);
                //    dateFFcs = dda.IsIncludeF ? DateF.Date.AddDays(1).AddHours(dda.HourAddForStart) : DateF.Date.AddDays(1).AddHours(dda.HourAddForStart).AddSeconds(-1);
                //    //преобразуем их в локальные даты
                //    #region DateSF
                //    DateTime? d = dateUtc2dateLoc(utcOffsetValueAll, dateSFcs);
                //    if (d != null)
                //    {
                //        var utcOffsetEAV = EntityAttrValue.GetEntityAttrValue(utcOffsetValueAll, SiteId, (int)EnumSiteAttrType.UTCOffset, dateSFcs);
                //        utcOffsetValue = utcOffsetValueAll.Where(t => t.DateS >= utcOffsetEAV.DateS).ToList();
                //        dateSFcs = (DateTime)d;
                //    }
                //    else
                //    {
                //        InsertLog(LogId, true, "Нет utcOffset.@siteId=" + SiteId + "@dateUTC=" + dateSFcs);
                //        return;
                //    }
                //    d = dateUtc2dateLoc(utcOffsetValueAll, dateFFcs);
                //    if (d != null)
                //        dateFFcs = (DateTime)d;
                //    else
                //    {
                //        InsertLog(LogId, true, "Нет utcOffset.@siteId=" + SiteId + "@dateUTC=" + dateFFcs);
                //        return;
                //    }
                //    #endregion
                //}
            }

            Data.DataForecastRepository dfr = Data.DataManager.GetInstance().DataForecastRepository;
            List<Catalog> catalogList = metaDM.CatalogRepository.Select(
                new List<int>() { SiteId }, new List<int>() { VariableId },
                 new List<int>() { MethodSrcSetId }, new List<int>() { SourceDstId },
                 new List<int>() { OffsetTypeId }, OffsetValue);
            if (catalogList.Count != 1)
            {
                InsertLog(LogId, true, "Нет записи в таблице data.catalog. "
                    + "@siteId=" + SiteId
                    + ";@variabliId=" + VariableId
                    + ";@methodId=" + MethodSrcSetId
                    + ";@OffsetTypeId=" + OffsetTypeId
                    + ";@OffsetValue=" + OffsetValue);
                InsertLog(LogId, false, "Finish whith error.");
                return;
            }
            int catalogIdSrc = catalogList[0].Id;
            // выбираем данные для агрегации
            List<DataForecast> dataSrc = dfr.Select(catalogIdSrc, dateSFcs, dateFFcs, null, true);

            Dictionary<DateTime /*dateDay*/, Dictionary<DateTime/*DateFcs*/, Dictionary<DateTime/*date_fcs_start*/, DataForecast>>> dataDate =
                            new Dictionary<DateTime, Dictionary<DateTime, Dictionary<DateTime, DataForecast>>>();
            Dictionary<DateTime/*dateDay*/, List<DateTime>/*date_fcs_start*/> dateDayDateFcs = new Dictionary<DateTime, List<DateTime>>();

            foreach (var df in dataSrc)
            {
                DateTime dateDay;//расчитываем из dateFcs
                DateTime dateFcs;
                DateTime dateFcsStart;
                if (isDateTypeEqual) //в случае если прогностические данные хранятся с типом даты, которая нужна для агрегации
                {
                    dateFcs = df.DateFcs;
                    dateFcsStart = df.DateIni;
                }
                else
                { //нужно преобразовать прогностические даты при помощи utcOffset датам типом, который нужен для агрегации
                    throw new NotImplementedException();
                    //if (methodFcsSrc.IsDateFcsUTC)//если прогнозы хранятя в дате utc, а считать надо по локальной дате
                    //{ 
                    //преобразуем прогнозные даты utc->loc
                    //dateFcs = (DateTime)dateUtc2dateLoc(utcOffsetValue, df.DateFcs);
                    //dateFcsStart = (DateTime)dateUtc2dateLoc(utcOffsetValue, df.DateIni);
                    //}
                    //else //если прогнозы хранятя в локальной дате, а считать надо по дате utc
                    //{ //преобразуем прогнозные даты loc->utc
                    //    dateFcs = (DateTime)dateLoc2dateUtc(utcOffsetValue, df.DateFcs);
                    //    dateFcsStart = (DateTime)dateLoc2dateUtc(utcOffsetValue, df.DateIni);
                    //}
                }
                dateDay = getDateDay(dateFcs, dda.HourAddForStart, dda.IsIncludeF);
                Dictionary<DateTime/*DateFcs*/, Dictionary<DateTime/*date_fcs_start*/, DataForecast>> dic;
                if (dataDate.TryGetValue(dateDay, out dic))
                {
                    Dictionary<DateTime, DataForecast> dt;
                    if (dic.TryGetValue(dateFcs, out dt))
                        dt.Add(dateFcsStart, df);
                    else
                    {
                        dt = new Dictionary<DateTime, DataForecast>();
                        dt.Add(dateFcsStart, df);
                        dic.Add(dateFcs, dt);
                    }
                }
                else
                {
                    dic = new Dictionary<DateTime, Dictionary<DateTime, DataForecast>>();
                    Dictionary<DateTime, DataForecast> dt = new Dictionary<DateTime, DataForecast>();
                    dt.Add(dateFcsStart, df);
                    dic.Add(dateFcs, dt);
                    dataDate.Add(dateDay, dic);
                }
                List<DateTime> ldt;
                if (dateDayDateFcs.TryGetValue(dateDay, out ldt))
                    ldt.Add(dateFcsStart);
                else
                    dateDayDateFcs.Add(dateDay, new List<DateTime>() { dateFcsStart });
            }
            List<DataForecast> ret = new List<DataForecast>();
            foreach (var dateDay in dataDate.Keys)
            {
                List<DateTime> dateFcsS;
                if (!dateDayDateFcs.TryGetValue(dateDay, out dateFcsS))
                    continue;
                //var dateFcs=dataDate[dateDay];
                List<DateTime> dateFcsSSort = dateFcsS.Distinct().OrderBy(t => t).ToList();
                Dictionary<DateTime/*DateFcs*/, Dictionary<DateTime/*date_fcs_start*/, DataForecast>> dateFcsDic;
                dateFcsDic = dataDate[dateDay];
                if (dateFcsDic.Count < countFcsInDay)
                    continue;
                DataForecast[,] data = new DataForecast[dateFcsSSort.Count, dateFcsDic.Count];
                int j = 0;
                foreach (var dateFcs in dateFcsDic.Keys)
                {
                    Dictionary<DateTime/*date_fcs_start*/, DataForecast> dateFcsSDic = dateFcsDic[dateFcs];
                    for (int i = 0; i < dateFcsSSort.Count; i++)
                    {
                        DateTime dFcsS = dateFcsSSort[i];
                        DataForecast df;
                        if (dateFcsSDic.TryGetValue(dFcsS, out df))
                            data[i, j] = df;
                        else
                            data[i, j] = null;
                    }
                    j++;
                }
                for (int i = 0; i < data.GetLength(0); i++)
                {
                    DateTime dateFcsSSrc = dateFcsSSort[i];
                    List<int> listNull = new List<int>();
                    List<double> dataVec = new List<double>();
                    List<DataForecast> fcsVec = new List<DataForecast>();
                    for (int k = 0; k < data.GetLength(1); k++)
                    {
                        if (data[i, k] != null)
                        {
                            dataVec.Add(data[i, k].Value);
                            fcsVec.Add(data[i, k]);
                        }
                        else
                            listNull.Add(k);
                    }
                    if (listNull.Count > countMaxNull)
                        continue;
                    if (listNull.Count != 0)
                        foreach (int k in listNull)
                        {
                            for (int ii = i + 1; ii < data.GetLength(0); ii++)
                            {
                                if (data[ii, k] != null)
                                {
                                    dataVec.Add(data[ii, k].Value);
                                    fcsVec.Add(data[ii, k]);
                                    break;
                                }
                            }
                        }

                    if (dataVec.Count == countFcsInDay)
                    {
                        foreach (var var in varDst)
                        {
                            double value = DataDerived.Derived((EnumDataType)var.DataTypeId, dataVec);
                            DateTime dateFcsSDst;
                            //if (methodFcsSrc.IsDateFcsUTC == methodFcsDst.IsDateFcsUTC)
                            dateFcsSDst = dateFcsSSrc;
                            //else
                            //{
                            //    throw new NotImplementedException();
                            //    if (methodFcsDst.IsDateFcsUTC)//если результирующий прогноз хранится с utc, а исходный в локальной дате
                            //        //надо дату старта прогноза преобразовать из loc->utc
                            //        dateFcsSDst = (DateTime)dateLoc2dateUtc(utcOffsetValue, dateFcsSSrc);
                            //    else
                            //        //надо дату старта прогноза преобразовать из utc->loc
                            //        dateFcsSDst = (DateTime)dateUtc2dateLoc(utcOffsetValue, dateFcsSSrc);
                            //}
                            double lagFcs = getLag(dateFcsSDst, dateDay, methodFcsDst.LeadTimeUnitId);
                            Catalog catalog;
                            if (ctlDstDic.TryGetValue(var, out catalog))
                            {
                                var fcs = dataDM.DataForecastRepository.Select(catalog.Id, dateDay, dateDay, lagFcs, true);
                                if (fcs.Count > 0)
                                    continue;
                                DataForecast r = new DataForecast(catalog.Id, lagFcs, dateDay, dateFcsSDst, value, DateTime.Now);
                                dataDM.DataForecastRepository.Insert(r);
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Определяет разницу между датами в интервалах временного периода
        /// </summary>
        /// <param name="dateS"></param>
        /// <param name="dateF"></param>
        /// <param name="timeId"></param>
        /// <returns></returns>
        private double getLag(DateTime dateS, DateTime dateF, int timeId)
        {
            if (timeId == 103)
                return (dateF - dateS).TotalHours;
            else
                throw new NotImplementedException();
        }
        /// <summary>
        /// Процедура определяет сутки для заданной даты учитывая параметры
        /// </summary>
        /// <param name="date">дата</param>
        /// <param name="hoursAddS">Количество часов, сколько надо добавить к началу текущих суток для получения начала суточного периода агрегации</param>
        /// <param name="isIncludeF">Включать конец (true) или начало (false) суточного интервала</param>
        /// <returns></returns>
        private DateTime getDateDay(DateTime date, int hoursAddS, bool isIncludeF)
        {
            int day_add;
            int hour_s;
            DateTime date_out;
            day_add = hoursAddS / 24;
            hour_s = hoursAddS % 24;
            if (hour_s < 0)
            {
                hour_s = 24 + hour_s;
                day_add = day_add - 1;
            }
            if (isIncludeF)
            {
                if (date > (date.Date.AddHours(hour_s)))
                    date_out = date.Date;
                else
                    date_out = date.Date.AddDays(-1);
            }
            else
            {
                if (date >= date.Date.AddHours(hour_s))

                    date_out = date.Date;
                else
                    date_out = date.Date.AddDays(-1);
            }
            date_out = date_out.AddDays(-1 * day_add);
            return date_out;
        }
        /// <summary>
        /// преобразуем utc дату в локальную дату на основе данных siteAttrValue
        /// </summary>
        /// <param name="utcOffsetValueAll">siteAttrValue</param>
        /// <param name="date">дата utc</param>
        /// <returns>дата локальная</returns>
        private DateTime? dateUtc2dateLoc(List<EntityAttrValue> utcOffsetValueAll, DateTime date)
        {
            //выбираем utcOffset для заданной даты
            var utcOffsetEAV = EntityAttrValue.GetEntityAttrValue(utcOffsetValueAll, SiteId, (int)EnumSiteAttrType.UTCOffset, date);
            int utcOffset;
            if (utcOffsetEAV != null && int.TryParse(utcOffsetEAV.Value, out utcOffset))
                return date.AddHours(utcOffset);
            else
                return null;
        }

        /// <summary>
        /// преобразуем локальную дату в utc дату на основе данных siteAttrValue
        /// </summary>
        /// <param name="utcOffsetValueAll">siteAttrValue</param>
        /// <param name="dateLoc">дата локальная</param>
        /// <returns>дата utc</returns>
        private DateTime? dateLoc2dateUtc(List<EntityAttrValue> utcOffsetValueAll, DateTime dateLoc)
        {
            //выбираем utcOffset для заданной даты
            var utcOffsetEAV = EntityAttrValue.GetEntityAttrValue(utcOffsetValueAll, SiteId, (int)EnumSiteAttrType.UTCOffset, dateLoc);
            int utcOffset;
            if (!(utcOffsetEAV != null && int.TryParse(utcOffsetEAV.Value, out utcOffset)))
                return null;
            DateTime dateUts = dateLoc.AddHours(-1 * utcOffset);
            utcOffsetEAV = EntityAttrValue.GetEntityAttrValue(utcOffsetValueAll, SiteId, (int)EnumSiteAttrType.UTCOffset, dateUts);
            if (utcOffsetEAV != null && int.TryParse(utcOffsetEAV.Value, out utcOffset))
                return dateLoc.AddHours(-1 * utcOffset);
            else
                return null;
        }
    }

}
