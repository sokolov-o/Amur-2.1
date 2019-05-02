using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viaware.Sakura;
using SaruraDb = Viaware.Sakura.Db;
using Viaware.Sakura.Db.HCR;
using amurMeta = FERHRI.Amur.Meta;
using amurData = FERHRI.Amur.Data;
using System.Diagnostics;

namespace Sakura.HCR
{
    class Program
    {
        static void Main(string[] args)
        {
            int ctlSetId = 104;
            string cnnAmur = "Server=192.168.203.163;Port=5432;User Id=NDruz;Password=qq;Database=ferhri.amur";
            Import(ctlSetId, cnnAmur);
        }
        static int meteo_dayId = 327;
        static List<amurMeta.Variable> getVarAmur(CtlRec ctl, out int method_id, amurMeta.VariableRepository vr, int stnTimeZone)
        {
            int variableTypeId, timeId, unitId, dataTypeId, generalCategoryId = 2, sampleMediumId, valueTypeId;
            int[] timeSupport;

            switch (ctl.paramId)
            {
                case (int)Enums.Parameter.TA:
                    variableTypeId = 269;
                    unitId = 96;
                    break;
                case (int)Enums.Parameter.RELATIVE_HUMID:
                    variableTypeId = 219;
                    unitId = 1;
                    break;
                case (int)Enums.Parameter.RR:
                    variableTypeId = 200;
                    unitId = 54;
                    break;
                case (int)Enums.Parameter.WIND_DIR:
                    variableTypeId = 292;
                    unitId = 3;
                    break;
                case (int)Enums.Parameter.WIND_VELOS:
                    variableTypeId = 293;
                    unitId = 119;
                    break;
                case (int)Enums.Parameter.P:
                    variableTypeId = 16;
                    unitId = 90;
                    break;
                case (int)Enums.Parameter.TD:
                    variableTypeId = 84;
                    unitId = 96;
                    break;
                case (int)Enums.Parameter.WATERVAPOR_UPR:
                    variableTypeId = 275;
                    unitId = 90;
                    break;
                case (int)Enums.Parameter.SATD:
                    variableTypeId = 276;
                    unitId = 90;
                    break;
                default:
                    throw new Exception("UNKNOWN ctl.paramId=" + ctl.paramId + " for variableTypeId and unitId");
            }
            switch (ctl.timeId)
            {
                case (int)Enums.TimePeriod.DAY:
                    timeId = 104;
                    timeSupport = new int[] { 1 };
                    valueTypeId = 1;
                    method_id = 10;
                    break;

                case (int)Enums.TimePeriod.HOUR8:
                    if (ctl.paramId == (int)Enums.Parameter.RR)
                    {
                        if (stnTimeZone >= 9 && stnTimeZone <= 11)
                            timeSupport = new int[] { 43200, 21600 };
                        else
                            timeSupport = new[] { 43200 };
                    }
                    else
                        if (ctl.paramId == (int)Enums.Parameter.WIND_VELOS)
                            timeSupport = new int[] { 600 };
                        else
                            timeSupport = new int[] { 0 };
                    timeId = 100;
                    if (ctl.paramId == (int)Enums.Parameter.SATD || ctl.paramId == (int)Enums.Parameter.WATERVAPOR_UPR)
                    {
                        valueTypeId = 1;
                        method_id = 15;
                    }
                    else
                    {
                        valueTypeId = 2;
                        method_id = 0;
                    }
                    break;
                default:
                    throw new Exception("UNKNOWN ctl.timeId=" + ctl.timeId + " for timeSupport, timeId, methodId and valueTypeId");
            }
            switch (ctl.paramId)
            {
                case (int)Enums.Parameter.TA:
                case (int)Enums.Parameter.RELATIVE_HUMID:
                case (int)Enums.Parameter.WIND_DIR:
                case (int)Enums.Parameter.WIND_VELOS:
                case (int)Enums.Parameter.P:
                case (int)Enums.Parameter.TD:
                case (int)Enums.Parameter.WATERVAPOR_UPR:
                case (int)Enums.Parameter.SATD:
                    sampleMediumId = 1;
                    break;
                case (int)Enums.Parameter.RR:
                    sampleMediumId = 5;
                    break;
                default:
                    throw new Exception("UNKNOWN ctl.paramId=" + ctl.paramId + " for sampleMediumId");
            }
            switch (ctl.actionId)
            {
                case (int)Enums.Action.VALUE:

                    switch (ctl.paramId)
                    {
                        case (int)Enums.Parameter.RR:
                            dataTypeId = 6;
                            break;
                        case (int)Enums.Parameter.WIND_MAX8:
                            dataTypeId = 8;
                            break;
                        case (int)Enums.Parameter.WIND_DIR:
                            dataTypeId = 11;
                            break;
                        case (int)Enums.Parameter.WIND_VELOS:
                            dataTypeId = 1;
                            break;
                        case (int)Enums.Parameter.TA:
                        case (int)Enums.Parameter.RELATIVE_HUMID:
                        case (int)Enums.Parameter.P:
                        case (int)Enums.Parameter.TD:
                        case (int)Enums.Parameter.WATERVAPOR_UPR:
                        case (int)Enums.Parameter.SATD:
                            dataTypeId = 5;
                            break;
                        default:
                            throw new Exception("UNKNOWN ctl.paramId=" + ctl.paramId + " for dataTypeId");
                    }
                    break;
                case (int)Enums.Action.AVG:
                    dataTypeId = 1;
                    break;
                case (int)Enums.Action.MAX:
                    dataTypeId = 8;
                    break;
                case (int)Enums.Action.MIN:
                    dataTypeId = 10;
                    break;
                case (int)Enums.Action.SUM:
                    dataTypeId = 6;
                    break;
                default:
                    throw new Exception("UNKNOWN ctl.actionId=" + ctl.actionId + " for dataTypeId");

            }
            var ret = vr.Select(new List<int>() { variableTypeId }, new List<int>() { timeId }, new List<int>() { unitId },
                new List<int>() { dataTypeId }, new List<int>() { generalCategoryId },
                new List<int>() { sampleMediumId }, timeSupport.ToList(), new List<int>() { valueTypeId });

            return ret;

        }
        static void Import(int ctlSetId, string cnnAmur)
        {
            int utcOffsetAttrTypeId = 1003;
            amurData.DataManager adDM = amurData.DataManager.GetInstance(cnnAmur);
            amurMeta.DataManager amDM = amurMeta.DataManager.GetInstance(cnnAmur);

            Dictionary<int, Dictionary<int, bool>> hoursRRDicLast = new Dictionary<int, Dictionary<int, bool>>();
            for (int timeZone = 6; timeZone <= 12; timeZone++)
            {
                if (timeZone >= 6 && timeZone <= 8)
                {
                    hoursRRDicLast.Add(timeZone, getRRIsExsist(new int[] { 0, 12 }));
                }
                if (timeZone >= 9 && timeZone <= 11)
                    hoursRRDicLast.Add(timeZone, getRRIsExsist(new int[] { 9, 21 }));
                if (timeZone == 12)
                    hoursRRDicLast.Add(timeZone, getRRIsExsist(new int[] { 18, 6 }));
            }
            Dictionary<int, bool> hoursRRDic4Sr = getRRIsExsist(new int[] { 21, 0, 9, 12 });

            SaruraDb.DbHmDic dbHmdic = new SaruraDb.DbHmDic();
            SaruraDb.DbHmdCatalog dbCtl = new SaruraDb.DbHmdCatalog();
            int dbSrcListId = (int)Enums.DbList.HCRStn;
            var ctlRecs = dbCtl.SelectCtlRecCollection(ctlSetId);
            string cnns = dbHmdic.DbCnnString(dbSrcListId, null, null);
            DbHCR dbHcrStn = new DbHCR(cnns, dbSrcListId);
            List<int> notImportCtlId = new List<int>();
            foreach (CtlRec ctl in ctlRecs)
            {
                if (ctl.SpaceId != Enums.Space.STATION)
                {
                    Console.WriteLine("ERROR! ctl.Sapace!=Station for ctlId=" + ctl.id);
                    notImportCtlId.Add(ctl.id);
                    continue;
                }
                int? stId = dbHmdic.GetStationId(ctl);
                if (!stId.HasValue)
                {
                    Console.WriteLine("ERROR! stationId is null for ctlId=" + ctl.id);
                    notImportCtlId.Add(ctl.id);
                    continue;
                }
                Viaware.Sakura.Station station = dbHmdic.SelectStation((int)stId);
                if (station.stnTypeId != Enums.StationType.HMStation)
                {
                    Console.WriteLine("ERROR! stationType!= HMStation for ctlId=" + ctl.id + " and stationId=" + station.id);
                    notImportCtlId.Add(ctl.id);
                    continue;
                }
                if (ctl.levelTypeId != 1)
                {
                    Console.WriteLine("ERROR! ctl.levelTypeId!=уровень станции, моря for ctlId=" + ctl.id);
                    notImportCtlId.Add(ctl.id);
                    continue;
                }




                int offset_type_id = 0;
                int offset_value = ctl.levelValue;
                int method_id;
                int source_id = 3;//ВНИГМИ
                int site_type_id = 1;

                int code = station.stIndex.HasValue ? (int)station.stIndex : -1;
                amurMeta.Station stn = amDM.StationRepository.Select(code.ToString());
                List<amurMeta.Site> sites = amDM.SiteRepository.Select(stn.Id, site_type_id);
                if (sites.Count != 1)
                {
                    Console.WriteLine("ERROR! sites.Count!=1 for ctlId=" + ctl.id + " and stationIndex=" + station.stIndex);
                    notImportCtlId.Add(ctl.id);
                    continue;
                }

                amurMeta.Site site = sites[0];

                var listUTCOffset = amDM.EntityAttrRepository.SelectAttrValues("site", new List<int>() { site.Id }, new List<int>() { utcOffsetAttrTypeId });

                //amurMeta.EntityAttrValue ea;
                //ea.AttrTypeId
                //ea.
                //    amurMeta.EntityAttrValue
                if (!station.timeZone.HasValue)
                {
                    Console.WriteLine("ERROR! !station.timeZone.HasValue for ctlId=" + ctl.id + " and stationIndex=" + station.stIndex);
                    notImportCtlId.Add(ctl.id);
                    continue;
                }

                var variableList = getVarAmur(ctl, out method_id, amDM.VariableRepository, (int)station.timeZone);
                if (variableList.Count == 0)
                {
                    Console.WriteLine("ERROR! UNKNOWN Amur.VarId for ctlId=" + ctl.id);
                    notImportCtlId.Add(ctl.id);
                    continue;
                }
                DbHCR dbSrc;
                if (ctl.dbListId == dbSrcListId)
                    dbSrc = dbHcrStn;
                else
                {
                    string cnns1 = dbHmdic.DbCnnString(ctl.dbListId, null, null);
                    dbSrc = new DbHCR(cnns1, ctl.dbListId);
                }
                DataCollection dataSrc = dbSrc.SelectData(new int[] { ctl.id }, null, null, null, true, false);
                dataSrc.OrderBy(t => t.Date);
                var dvs = new List<amurData.DataValue>();
                if (ctl.paramId == (int)Enums.Parameter.RR && ctl.TimeId == (int)Enums.TimePeriod.HOUR8)
                {
                    //специально для осадков срочных

                    int utcHour4MeteoDayStart = (int)dbHmdic.DateMeteoStart((int)station.timeZone);

                    Dictionary<int/*количество сроков*/ , amurData.Catalog> ctlsAmur = new Dictionary<int, amurData.Catalog>();
                    foreach (var v in variableList)
                    {
                        var testCtlAmur = adDM.CatalogRepository.Select(new List<int>() { site.Id }, new List<int> { v.Id },
                                                                offset_type_id, method_id, source_id, offset_value);
                        amurData.Catalog ctlAmur;
                        if (testCtlAmur.Count == 0)
                        {
                            ctlAmur = new amurData.Catalog(-1, site.Id, v.Id, method_id, source_id, offset_type_id, offset_value);
                            ctlAmur = adDM.CatalogRepository.Insert(ctlAmur);
                        }
                        else
                            ctlAmur = testCtlAmur[0];

                        switch (v.TimeSupport)
                        {
                            case 43200:
                                ctlsAmur.Add(2, ctlAmur);
                                break;
                            case 21600:
                                ctlsAmur.Add(4, ctlAmur);
                                break;
                        }
                    }

                    Dictionary<int/*срок*/, bool/* */> hoursRREx2Sr;
                    if (!hoursRRDicLast.TryGetValue((int)station.timeZone, out hoursRREx2Sr))
                    {
                        Console.WriteLine("ERROR! no hoursRRDicLast for ctlId=" + ctl.id + " and station.timeZone=" + (int)station.timeZone);
                        notImportCtlId.Add(ctl.id);
                        continue;
                    }


                    int utcOffset;
                    foreach (var dataValueHcr in dataSrc)
                    {
                        int? ctlAmurId = null;
                        DateTime date = dataValueHcr.Date;
                        var sav = amurMeta.EntityAttrValue.GetEntityAttrValue(listUTCOffset, site.Id, utcOffsetAttrTypeId, date);
                        if (!(sav != null))
                        {
                            Console.WriteLine("CtlId=" + ctl.Id + "For date=" + date.ToString() + " no utcOffSet.Value");
                            continue;
                        }
                        utcOffset = int.Parse(sav.Value);
                        DateTime dateLoc = date.AddHours(utcOffset);
                        double value = dataValueHcr.Value;
                        bool isExRR;

                        int yearMeteo = ((DateTime)DateTimeProcess.DateUTC2Meteo(date, utcHour4MeteoDayStart)).Year;
                        if ((int)station.timeZone >= 9 && (int)station.timeZone <= 11 && yearMeteo >= 1966 && yearMeteo <= 1986)
                        {
                            //4 срока
                            if (!hoursRRDic4Sr.TryGetValue(date.Hour, out isExRR))
                                throw new Exception("ERROR: !hoursRRDic4Sr.TryGetValue(date.Hour, out isExRR)");
                            amurData.Catalog ctlId;
                            if (isExRR && ctlsAmur.TryGetValue(4, out ctlId))
                                ctlAmurId = ctlId.Id;
                        }
                        else
                        {
                            //2 срока
                            if (!hoursRREx2Sr.TryGetValue(date.Hour, out isExRR))
                                throw new Exception("ERROR: !hoursRREx2Sr.TryGetValue(date.Hour, out isExRR)");
                            amurData.Catalog ctlId;
                            if (isExRR && ctlsAmur.TryGetValue(2, out ctlId))
                                ctlAmurId = ctlId.Id;
                        }
                        if (ctlAmurId.HasValue)
                        {
                            amurData.DataValue dv = new amurData.DataValue(-1, (int)ctlAmurId, value, dateLoc, 0, utcOffset);
                            dvs.Add(dv);
                        }
                    }

                }
                else
                {
                    if (variableList.Count != 1)
                        throw new Exception("variableList.Count != 1");
                    List<amurData.Catalog> ctlsAmur = adDM.CatalogRepository.Select(new List<int>() { site.Id },
                        new List<int>() { variableList[0].Id }, offset_type_id, method_id, source_id, offset_value);
                    amurData.Catalog ctlAmur;
                    if (ctlsAmur.Count == 0)
                    {
                        ctlAmur = new amurData.Catalog(-1, site.Id, variableList[0].Id, method_id, source_id, offset_type_id, offset_value);
                        ctlAmur = adDM.CatalogRepository.Insert(ctlAmur);
                    }
                    else
                        ctlAmur = ctlsAmur[0];

                    int utcOffset = 0;
                    foreach (var dataValueHcr in dataSrc)
                    {
                        DateTime date = dataValueHcr.Date;
                        if (ctl.TimeId == (int)Enums.TimePeriod.HOUR8 || ctl.TimeId == (int)Enums.TimePeriod.HOUR4)
                        {
                            var sav = amurMeta.EntityAttrValue.GetEntityAttrValue(listUTCOffset, site.Id, utcOffsetAttrTypeId, date);
                            utcOffset = int.Parse(sav.Value);
                            if (!(sav != null))
                            {
                                Console.WriteLine("CtlId=" + ctl.Id + "For date=" + date.ToString() + " no utcOffSet.Value");
                                continue;
                            }
                        }
                        DateTime dateLoc = date.AddHours(utcOffset);
                        double value = dataValueHcr.Value;
                        amurData.DataValue dv = new amurData.DataValue(-1, ctlAmur.Id, value, dateLoc, 0, utcOffset);
                        dvs.Add(dv);
                    }
                }
                Stopwatch sw = Stopwatch.StartNew();
                Console.WriteLine(DateTime.Now.ToShortTimeString());
                adDM.DataValueRepository.Insert(dvs);
                sw.Stop();

                Console.WriteLine(DateTime.Now.ToShortTimeString());
                Console.WriteLine("Write for stn '" + stn.Name + "', par=" + ((Enums.Parameter)ctl.paramId).ToString() + " " + sw.Elapsed.TotalMinutes + "min");

            }
            Console.ReadKey();

        }

        private static Dictionary<int, bool> getRRIsExsist(int[] p)
        {
            Dictionary<int, bool> ret = new Dictionary<int, bool>();
            foreach (var t in p)
                ret.Add(t, true);

            for (int i = 0; i < 24; i += 3)
            {
                bool isEx;
                if (!ret.TryGetValue(i, out isEx))
                    ret.Add(i, false);
            }
            return ret;
        }
    }
}
