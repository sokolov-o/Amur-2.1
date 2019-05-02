using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using GribExport;
using Viaware.Sakura;
using System.Configuration;
using FERHRI.Amur.Data;
using FERHRI.Amur.Meta;
using System.Diagnostics;
using System.IO;
using Viaware.Sakura.Db;

namespace FcsGFS
{
    class Program
    {

        static void test(){
            var fs = File.OpenRead(@"D:\ХЛАМ\20_02_2014\GFS Grib2\gfs.t00z.pgrb2.0p50.f006.grb2");//gfs.t00z.pgrb2.0p25.f000.grb2");
    //variable_type="RR"
    //gfs_IsDiscipline="0"
    //gfs_IDCenter_id="7"
    //gfs_PDSProductDefinition="8"
    //gfs_PDSParameterCategory="1"
    //gfs_PDSParameterNumber="8"
    //gfs_PDSTypeFirstFixedSurface="1"
    //gfs_PDSTypeSecondFixedSurface="255"
    //gfs_PDSValueFirstFixedSurface="0"
    //gfs_PDSValueSecondFixedSurface="0"
    //gfs_GDSGdtn="0"

            CtlRecGrib2 ctlRecG2 = new CtlRecGrib2(0,7,8,1,8,1,255,0,0,0);
            var t=FileGrib2.selectData(fs, new List<CtlRecGrib2>() { ctlRecG2 });
            fs.Close();
        
        }

        static void Main(string[] args)
        {
            test();
            //run(@"XMLParams.xml");
            Console.ReadKey();
        }


        static void run(string fileParam)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(fileParam);
            XmlElement xRoot = xDoc.DocumentElement;

            var dataDM = FERHRI.Amur.Data.DataManager.GetInstance(ConfigurationManager.ConnectionStrings["FERHRI.Amur.Properties.Settings.AmurDataConnectionString"].ConnectionString);
            var dataMetaDM = FERHRI.Amur.Meta.DataManager.GetInstance(ConfigurationManager.ConnectionStrings["FERHRI.Amur.Properties.Settings.AmurMetaConnectionString"].ConnectionString);
            var ctlRep = dataMetaDM.CatalogRepository;
            var dataFcsRep = dataDM.DataForecastRepository;
            var entityAttrRep = dataMetaDM.EntityAttrRepository;

            foreach (XmlNode nodeGfs in xRoot.SelectNodes("gfs_file"))
            {
                string uri = nodeGfs.Attributes["path"].Value;
                Enums.FileStoreType fileStoreType = (Enums.FileStoreType)int.Parse(nodeGfs.Attributes["file_store_type"].Value);
                Dictionary<CtlRecGrib2, Dictionary<GeoPoint, List<Catalog>>> ctlG2PointsCatalog =
                    new Dictionary<CtlRecGrib2, Dictionary<GeoPoint, List<Catalog>>>(new CtlRecGrib2Comparer());
                Dictionary<Catalog, object[/* variable_type
                                            * List<CtlRecGrib2>
                                            * value2Real
                                            * GeoPoint
                                            */]> catalogObjectDic = new Dictionary<Catalog, object[]>();
                foreach (XmlNode nodeCtl in nodeGfs.SelectNodes("catalog"))
                {
                    double[] val2Real = new double[2] { 0, 1 };
                    if (nodeCtl.Attributes["variable_type"] == null)
                    {
                        Console.WriteLine("нет атрибута variable_type");
                        continue;
                    }
                    string varType = nodeCtl.Attributes["variable_type"].Value;
                    List<CtlRecGrib2> ctlGrb2;
                    if (!getCtlRecGrib2(nodeCtl.Attributes, out ctlGrb2))
                        continue;
                    var strVal = nodeCtl.Attributes["val2real_multip"];
                    if (strVal != null)
                        val2Real[1] = double.Parse(strVal.Value);
                    strVal = nodeCtl.Attributes["val2real_add"];
                    if (strVal != null)
                        val2Real[0] = double.Parse(strVal.Value);
                    string[] ctlIdStr = nodeCtl.Attributes["catalog_id"].Value.Split(',');
                    string[] typeIntStr = nodeCtl.Attributes["type_interpolate"].Value.Split(',');
                    if (ctlIdStr.Length != typeIntStr.Length)
                    {
                        Console.WriteLine("catalog_id (" + nodeCtl.Attributes["catalog_id"].Value +
                            ") и type_interpolate (" + nodeCtl.Attributes["type_interpolate"].Value + ") не согласованы");
                        continue;
                    }
                    for (int i = 0; i < ctlIdStr.Length; i++)
                    {
                        int ctlId;
                        if (!int.TryParse(ctlIdStr[i], out ctlId))
                            continue;
                        Catalog catalog = ctlRep.Select(ctlId);
                        string lat = entityAttrRep.SelectAttrValue("site", catalog.SiteId, 1000, DateTime.Now).Value;
                        string lon = entityAttrRep.SelectAttrValue("site", catalog.SiteId, 1001, DateTime.Now).Value;
                        var gp = new GeoPoint(double.Parse(lat), double.Parse(lon));
                        gp.NearestTypeForPoint = (GeoPoint.NearestType)int.Parse(typeIntStr[i]);
                        #region ctlG2PointsCatalog
                        foreach (var ctlGrb2Item in ctlGrb2)
                        {
                            Dictionary<GeoPoint, List<Catalog>> dic;
                            if (ctlG2PointsCatalog.TryGetValue(ctlGrb2Item, out dic))
                            {
                                List<Catalog> listCtl;
                                if (dic.TryGetValue(gp, out listCtl))
                                {
                                    if (listCtl.IndexOf(catalog) == -1)
                                        listCtl.Add(catalog);
                                }
                                else
                                    dic.Add(gp, new List<Catalog>() { catalog });
                            }
                            else
                            {
                                dic = new Dictionary<GeoPoint, List<Catalog>>();
                                dic.Add(gp, new List<Catalog>() { catalog });
                                ctlG2PointsCatalog.Add(ctlGrb2Item, dic);
                            }
                        }
                        #endregion
                        object[] obj;
                        if (catalogObjectDic.TryGetValue(catalog, out obj))
                            throw new Exception("В xml-файле catalog.id=" + catalog.Id + " присутствует нестколько раз!!!");
                        else
                        {
                            obj = new object[] { varType, ctlGrb2, val2Real, gp };
                            catalogObjectDic.Add(catalog, obj);
                        }
                    }
                }


                Dictionary<DateTime/*date_ini*/, List<int>/*predictTime*/> dateFcs = new Dictionary<DateTime, List<int>>();
                foreach (XmlNode node_date_fcs in xRoot.SelectNodes("date_fcs"))
                {
                    XmlNode node_pt = node_date_fcs.SelectSingleNode("predict_time");
                    List<int> prt = null;

                    if (node_pt != null)
                    {
                        prt = new List<int>();
                        if (!string.IsNullOrEmpty(node_pt.Value))
                        {
                            string[] str = node_pt.Value.Split(',');
                            foreach (var s in str) prt.Add(int.Parse(s));
                        }
                        else
                        {
                            int start, finish, step;
                            if (int.TryParse(node_pt.Attributes["start"].Value, out start) &&
                                int.TryParse(node_pt.Attributes["finish"].Value, out finish) &&
                                int.TryParse(node_pt.Attributes["step"].Value, out step))
                                for (int i = start; i <= finish; i += step)
                                    prt.Add(i);
                        }
                    }
                    XmlNode node_dini = node_date_fcs.SelectSingleNode("date_ini");
                    if (node_dini == null)
                    {
                        Console.WriteLine("На задана дата старта прогноза");
                        continue;
                    }
                    List<DateTime> dateIni = new List<DateTime>();
                    DateTime dateIniValue;
                    if (tryGetDateFromNode(node_dini.SelectSingleNode("value"), out dateIniValue))
                        dateIni.Add(dateIniValue);
                    else
                    {
                        DateTime ds, df;
                        int[] hours = node_dini.SelectSingleNode("hours_date_ini") != null ?
                            Viaware.Sakura.Array.ToInt(node_dini.SelectSingleNode("hours_date_ini").InnerText.Split(',')) :
                            Viaware.Sakura.Array.createArraySF(0, 23);
                        var nodds = node_dini.SelectSingleNode("date_ini_s");
                        bool isDsSrcData;
                        if (nodds.Attributes["from_data_src"] != null && bool.TryParse(nodds.Attributes["from_data_src"].Value, out isDsSrcData)
                            && isDsSrcData)
                        {
                            var dicD = dataFcsRep.SelectMinMaxDates(catalogObjectDic.Keys.Select(t => t.Id).ToList());
                            if (dicD.Count > 0)
                                ds = dicD.Values.Select(t => t[3]).Min().AddSeconds(1);
                            else
                            {
                                Console.WriteLine("Невозможно определить начальную дату старта прогноза");
                                continue;
                            }

                        }
                        else
                            if (!tryGetDateFromNode(nodds, out ds))
                            {
                                Console.WriteLine("Не задана дата старта прогноза");
                                continue;
                            }
                        if (tryGetDateFromNode(node_dini.SelectSingleNode("date_ini_f"), out df))
                        {
                            DateTime d = ds.Date;
                            while (d <= df)
                            {
                                foreach (int hour in hours)
                                {
                                    DateTime di = d.AddHours(hour);
                                    if (di >= ds && di <= df)
                                        dateIni.Add(di);
                                }
                                d = d.AddDays(1);
                            }
                        }

                    }
                    if (dateIni.Count == 0)
                    {
                        Console.WriteLine("На задана дата старта прогноза");
                        continue;
                    }
                    foreach (var dini in dateIni)
                    {
                        dateFcs.Add(dini, prt);
                    }
                }
                if (dateFcs.Count == 0)
                {
                    Console.WriteLine("Нет дат!!!");
                    return;
                }
                Dictionary<CtlRecGrib2, List<GeoPoint>> ctlG2Points = new Dictionary<CtlRecGrib2, List<GeoPoint>>(new CtlRecGrib2Comparer());
                foreach (var ctlG2 in ctlG2PointsCatalog.Keys)
                {
                    Dictionary<GeoPoint, List<Catalog>> dic = ctlG2PointsCatalog[ctlG2];
                    List<GeoPoint> listgp = new List<GeoPoint>();
                    foreach (var gp in dic.Keys)
                        listgp.Add(gp);
                    ctlG2Points.Add(ctlG2, listgp);
                }

                foreach (DateTime dateIni in dateFcs.Keys)
                {
                    Console.WriteLine("dateIni=" + dateIni.ToString("yyyy-MM-dd HH:mm") + "; predictTime=" + string.Join(", ", dateFcs[dateIni]));
                    Stopwatch sw = Stopwatch.StartNew();
                    var dataExp = GFSExport.exportData(uri, fileStoreType, ctlG2Points, dateIni, dateFcs[dateIni]);
                    sw.Stop();
                    Console.WriteLine("Read GFS=" + sw.Elapsed.TotalSeconds + "sec.");
                    sw.Start();
                    List<DataForecast> listDataFcs = new List<DataForecast>();
                    var dataExpDic = dataExp.GroupBy(t => t.CtlGrib2).ToDictionary(group => group.Key, group => group.ToList(), new CtlRecGrib2Comparer());
                    //Dictionary<Catalog, object[/* varType
                    //        List<CtlRecGrib2>
                    //       * value2Real
                    //       * GeoPoint
                    //       */]> catalogObjectDic
                    foreach (var catalog in catalogObjectDic.Keys)
                    {
                        object[] obj = catalogObjectDic[catalog];
                        string varType = (string)obj[0];
                        List<CtlRecGrib2> ctlRecG2 = (List<CtlRecGrib2>)obj[1];
                        double[] val2Real = (double[])obj[2];
                        GeoPoint gp = (GeoPoint)obj[3];
                        switch (varType)
                        {
                            case "WIND":
                            case "WDIR":
                                int indU = -1, indV = -1;
                                for (int i = 0; i < ctlRecG2.Count; i++)
                                {
                                    if (ctlRecG2[i].PDSParameterCategory == 2)
                                    {
                                        switch (ctlRecG2[i].PDSParameterNumber)
                                        {
                                            case 2:
                                            case 23:
                                                indU = i;
                                                break;
                                            case 3:
                                            case 24:
                                                indV = i;
                                                break;
                                        }
                                    }
                                }
                                if (indU == -1 || indV == -1)
                                    continue;
                                List<GFSExportData> dataU, dataV;
                                if (!dataExpDic.TryGetValue(ctlRecG2[indU], out dataU) || !dataExpDic.TryGetValue(ctlRecG2[indV], out dataV))
                                    break;
                                foreach (var dataItemU in dataU)
                                {
                                    GFSExportData dataItemV = null;
                                    foreach (var div in dataV)
                                        if (div.DateFcs == dataItemU.DateFcs && div.DateIni == dataItemU.DateIni && div.PredictTime == dataItemU.PredictTime)
                                        {
                                            dataItemV = div;
                                            break;
                                        }
                                    if (dataItemV == null)
                                        continue;

                                    int ind = dataItemU.PointsPar.IndexOf(gp);
                                    if (ind == -1)
                                        continue;
                                    double valueU = toRealValue(dataItemU.Values[ind], val2Real);

                                    ind = dataItemV.PointsPar.IndexOf(gp);
                                    if (ind == -1)
                                        continue;
                                    double valueV = toRealValue(dataItemV.Values[ind], val2Real);

                                    double lagFcs = dataItemU.PredictTime;
                                    DateTime dateFcsV = dataItemU.DateFcs;
                                    DateTime dateIniV = dataItemU.DateIni;
                                    double value = double.NaN;
                                    if (varType.Equals("WIND") || varType.Equals("WGUST"))
                                        value = Math.Sqrt(Math.Pow(valueU, 2) + Math.Pow(valueV, 2));
                                    if (varType.Equals("WDIR"))
                                        value = Vector.uv2AzimuthFrom(valueU, valueV);
                                    if (double.IsNaN(value))
                                        continue;
                                    DataForecast dfcs = new DataForecast(catalog.Id, lagFcs, dateFcsV, dateIniV, value, DateTime.Now);
                                    if (dataFcsRep.Select(catalog.Id, dateIniV, dateIniV, lagFcs, false).Count == 0)

                                        dataFcsRep.Insert(dfcs);
                                }
                                break;
                            case "RR":
                                Console.WriteLine("Unsupported variable_type=\"" + varType + "\"");
                                break;
                            default:
                                if (ctlRecG2.Count > 1)
                                    Console.WriteLine("Unsupported variable_type=\"" + varType + "\"");
                                else
                                {
                                    List<GFSExportData> data;
                                    if (!dataExpDic.TryGetValue(ctlRecG2[0], out data))
                                        break;
                                    foreach (var dataItem in data)
                                    {
                                        int ind = dataItem.PointsPar.IndexOf(gp);
                                        if (ind == -1)
                                            continue;
                                        double value = dataItem.Values[ind];
                                        value = toRealValue(value, val2Real);
                                        double lagFcs = dataItem.PredictTime;
                                        DateTime dateFcsV = dataItem.DateFcs;
                                        DateTime dateIniV = dataItem.DateIni;
                                        DataForecast dfcs = new DataForecast(catalog.Id, lagFcs, dateFcsV, dateIniV, value, DateTime.Now);
                                        if (dataFcsRep.Select(catalog.Id, dateIniV, dateIniV, lagFcs, false).Count == 0)

                                            dataFcsRep.Insert(dfcs);
                                    }
                                }
                                break;
                        }
                    }
                    Console.WriteLine("Read+Write GFS=" + sw.Elapsed.TotalSeconds + "sec.");
                }
            }
        }

        private static bool tryGetDateFromNode(XmlNode node_d, out DateTime dateOut)
        {
            dateOut = DateTime.MinValue;
            if (node_d == null)
                return false;

            string dateStr = node_d.InnerText;
            if (!string.IsNullOrEmpty(dateStr))
            {
                string format = "yyyy.MM.dd";
                if (node_d.Attributes["format"] != null && !string.IsNullOrEmpty(node_d.Attributes["format"].Value))
                    format = node_d.Attributes["format"].Value;
                if (DateTime.TryParseExact(dateStr, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateOut))
                    return true;
                else
                    return false;
            }
            else
            {
                int dayAdd;
                if (node_d.Attributes["day_now_add"] != null && int.TryParse(node_d.Attributes["day_now_add"].Value, out dayAdd))
                {
                    dateOut = DateTime.Now.AddDays(dayAdd);
                    return true;
                }
                else
                    return false;
            }
        }

        private static bool getCtlRecGrib2(XmlAttributeCollection nodeCtlAttributes, out List<CtlRecGrib2> ctlGrb2)
        {
            ctlGrb2 = null;
            string str = nodeCtlAttributes["gfs_count_params"].Value;
            int countCtlGrb2 = 1;
            if (str != null)
                if (!int.TryParse(str, out countCtlGrb2))
                    return false;
            CtlRecGrib2[] ctlGrb2Arr = new CtlRecGrib2[countCtlGrb2];
            string[] IsDiscipline = nodeCtlAttributes["gfs_IsDiscipline"].Value.Split(',');
            string[] IDCenter_id = nodeCtlAttributes["gfs_IDCenter_id"].Value.Split(',');
            string[] PDSProductDefinition = nodeCtlAttributes["gfs_PDSProductDefinition"].Value.Split(',');
            string[] PDSParameterCategory = nodeCtlAttributes["gfs_PDSParameterCategory"].Value.Split(',');
            string[] PDSParameterNumber = nodeCtlAttributes["gfs_PDSParameterNumber"].Value.Split(',');
            string[] PDSTypeFirstFixedSurface = nodeCtlAttributes["gfs_PDSTypeFirstFixedSurface"].Value.Split(',');
            string[] PDSTypeSecondFixedSurface = nodeCtlAttributes["gfs_PDSTypeSecondFixedSurface"].Value.Split(',');
            string[] GDSGdtn = nodeCtlAttributes["gfs_GDSGdtn"].Value.Split(',');
            string[] PDSValueFirstFixedSurface = nodeCtlAttributes["gfs_PDSValueFirstFixedSurface"].Value.Split(',');
            string[] PDSValueSecondFixedSurface = nodeCtlAttributes["gfs_PDSValueSecondFixedSurface"].Value.Split(',');

            for (int i = 0; i < countCtlGrb2; i++)
            {
                int isDiscipline, idCenter_id, pdsProductDefinition, pdsParameterCategory, pdsParameterNumber,
                            pdsTypeFirstFixedSurface, pdsTypeSecondFixedSurface, gdsGdtn;
                float pdsValueFirstFixedSurface, pdsValueSecondFixedSurface;

                if (!tryGetIntValue(i, IsDiscipline, countCtlGrb2, out isDiscipline) ||
                    !tryGetIntValue(i, IDCenter_id, countCtlGrb2, out idCenter_id) ||
                    !tryGetIntValue(i, PDSProductDefinition, countCtlGrb2, out pdsProductDefinition) ||
                    !tryGetIntValue(i, PDSParameterCategory, countCtlGrb2, out pdsParameterCategory) ||
                    !tryGetIntValue(i, PDSParameterNumber, countCtlGrb2, out pdsParameterNumber) ||
                    !tryGetIntValue(i, PDSTypeFirstFixedSurface, countCtlGrb2, out pdsTypeFirstFixedSurface) ||
                    !tryGetIntValue(i, PDSTypeSecondFixedSurface, countCtlGrb2, out pdsTypeSecondFixedSurface) ||
                    !tryGetIntValue(i, GDSGdtn, countCtlGrb2, out gdsGdtn) ||
                    !tryGetFloatValue(i, PDSValueFirstFixedSurface, countCtlGrb2, out pdsValueFirstFixedSurface) ||
                    !tryGetFloatValue(i, PDSValueSecondFixedSurface, countCtlGrb2, out pdsValueSecondFixedSurface)
                    )
                    return false;

                ctlGrb2Arr[i] = new CtlRecGrib2(isDiscipline, idCenter_id, pdsProductDefinition, pdsParameterCategory, pdsParameterNumber,
                            pdsTypeFirstFixedSurface, pdsTypeSecondFixedSurface, pdsValueFirstFixedSurface, pdsValueSecondFixedSurface, gdsGdtn);
            }
            ctlGrb2 = ctlGrb2Arr.ToList();
            return true;
        }
        /// <summary>
        /// Выбирает i-тое значение, если strAttr.Length==count, если strAttr.Length==1, то берет это единственное значение. 
        /// Преобразует в int. В случае удачи возвращает true и в выходном параметре полученное значение, в остальных случаях возвращает 
        /// false и 0 в выходном параметре.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="strAttr"></param>
        /// <param name="count"></param>
        /// <param name="valueItem"></param>
        /// <returns></returns>
        private static bool tryGetIntValue(int i, string[] strAttr, int count, out int valueItem)
        {
            if (strAttr.Length == 1)
                if (int.TryParse(strAttr[0], out valueItem))
                    return true;
                else
                    return false;
            if (strAttr.Length == count)
                if (int.TryParse(strAttr[i], out valueItem))
                    return true;
                else
                    return false;
            valueItem = 0;
            return false;
        }
        /// <summary>
        /// Выбирает i-тое значение, если strAttr.Length==count, если strAttr.Length==1, то берет это единственное значение. 
        /// Преобразует в float. В случае удачи возвращает true и в выходном параметре полученное значение, в остальных случаях возвращает 
        /// false и 0 в выходном параметре.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="strAttr"></param>
        /// <param name="count"></param>
        /// <param name="valueItem"></param>
        /// <returns></returns>
        private static bool tryGetFloatValue(int i, string[] strAttr, int count, out float valueItem)
        {
            if (strAttr.Length == 1)
                if (float.TryParse(strAttr[0], out valueItem))
                    return true;
                else
                    return false;
            if (strAttr.Length == count)
                if (float.TryParse(strAttr[i], out valueItem))
                    return true;
                else
                    return false;
            valueItem = 0;
            return false;
        }
        private static double toRealValue(double value, double[] val2Real)
        {
            return (value + val2Real[0]) * val2Real[1];
        }

        public class CtlRecGrib2Comparer : EqualityComparer<CtlRecGrib2>
        {
            public override bool Equals(CtlRecGrib2 x, CtlRecGrib2 y)
            {
                return x.GDSGdtn == y.GDSGdtn &&
                    x.IDCenter_id == y.IDCenter_id &&
                    x.IsDiscipline == y.IsDiscipline &&
                    x.PDSParameterCategory == y.PDSParameterCategory &&
                    x.PDSParameterNumber == y.PDSParameterNumber &&
                    x.PDSProductDefinition == y.PDSProductDefinition &&
                    x.PDSTypeFirstFixedSurface == y.PDSTypeFirstFixedSurface &&
                    x.PDSTypeSecondFixedSurface == y.PDSTypeSecondFixedSurface &&
                    x.PDSValueFirstFixedSurface == y.PDSValueFirstFixedSurface &&
                    x.PDSValueSecondFixedSurface == y.PDSValueSecondFixedSurface;
            }
            public override int GetHashCode(CtlRecGrib2 x)
            {
                return (x.GDSGdtn + ";" + x.IDCenter_id + ";" + x.IsDiscipline + ";" + x.PDSParameterCategory
                    + ";" + x.PDSParameterNumber + ";" + x.PDSProductDefinition + ";" + x.PDSTypeFirstFixedSurface
                    + ";" + x.PDSTypeSecondFixedSurface + ";" + x.PDSValueFirstFixedSurface + ";" +
                    x.PDSValueSecondFixedSurface).GetHashCode();
            }
        }
    }
}
