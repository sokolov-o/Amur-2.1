using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.GISMeteo;
using SOV.Amur.Importer.GISMeteo.AmurService;
using System.Configuration;
using System.Xml;

namespace SOV.Amur.Importer.GISMeteo
{
    public class Settings
    {
        /// <summary>
        /// Начало периода пополнения данных. 
        /// Если null, то пополнение происходит с точки сохранения mdb (checkpoint).
        /// Если точки сохранения нет, то происходит просмотр всех данных в mdb.
        /// </summary>
        public DateTime? DateS { get; set; }

        public int CodeFormGis { get; set; }

        public int CodeFormIdAmur { get; set; }

        public int SourceId { get; set; }

        public string CheckPointPath { get; set; }

        public List<StationGM> StationGMList { get; set; }

        public List<Param> ParamList { get; set; }

        public List<Catalog> CatalogList { get; set; }

        public int VariableIdWindGustBetweenObs = -1;
        public int VariableIdWindGustHalfDay = -1;

        static public List<Settings> Parse(ServiceClient svc, long hSvc, string settingsXMLPath, string checkPointDir)
        {
            List<Settings> configList = new List<Settings>();
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(settingsXMLPath);

            XmlNode node = xmldoc.DocumentElement.GetElementsByTagName("mdbObsSearchPeriod")[0];
            DateTime? dateS = null;
            if (node.Attributes["dateS"].Value != string.Empty)
            {
                DateTime date;
                if (!SOV.Common.DateTimeProcess.TryParse(node.Attributes["dateS"].Value, "yyyyMMdd HH:mm", out date))
                    throw new Exception("Не удалось определить период пополнения данных из файла Settings: " + node.Attributes["dateS"].Value);
                dateS = date;
            }

            foreach (XmlNode xn in xmldoc.DocumentElement.GetElementsByTagName("codeForm"))
            {
                Settings config = new Settings() { DateS = dateS };
                config.CodeFormIdAmur = Convert.ToInt32(xn.Attributes["hCodeFormId"].Value);
                config.CodeFormGis = Convert.ToInt32(xn.Attributes["gCodeFormId"].Value);
                config.SourceId = Convert.ToInt32(xn.Attributes["hSourceId"].Value);
                config.CheckPointPath = (string.IsNullOrEmpty(checkPointDir) ? "" : checkPointDir + "\\") + xn.Attributes["checkPointPath"].Value;
                config.StationGMList = new List<StationGM>();
                config.ParamList = new List<Param>();

                foreach (XmlNode opt in xn)
                {
                    switch (opt.LocalName)
                    {
                        case "siteGroupId":
                            int siteGroupId = Convert.ToInt32(opt.InnerText);
                            List<Site> sites = svc.GetSitesByGroup(hSvc, siteGroupId);
                            // List<Station> stations = svc.GetStationsByList(hSvc, sites.Select(x => x.StationId).Distinct().ToList());
                            List<EntityAttrValue> utsOffsets = svc.GetSitesAttrValue(hSvc, sites.Select(x => x.Id).Distinct().ToList(), 1003, DateTime.Now);
                            List<Site> siteNoOffset = new List<Site>();
                            foreach (Site site in sites)
                            {
                                // STATION CODE
                                int siteCode = 0;
                                ////Station st = stations.Find(x => x.Id == site.StationId);
                                ////if (!int.TryParse(st.Code, out siteCode))
                                if (!int.TryParse(site.Code, out siteCode))
                                {
                                    continue;
                                }
                                //SITE UTSOffset
                                EntityAttrValue utcOffset = utsOffsets.FirstOrDefault(x => x.EntityId == site.Id);
                                if (utcOffset == null)
                                {
                                    utcOffset = new EntityAttrValue() { Value = "10" };
                                    siteNoOffset.Add(site);
                                }
                                config.StationGMList.Add(new StationGM()
                                {
                                    StationIndex = siteCode,
                                    SiteIdAmur = site.Id,
                                    Name = site.Name,
                                    UTCOffset = float.Parse(utcOffset.Value)
                                }
                                );
                            }
                            if (siteNoOffset.Count > 0)
                            {
                                string msg = "Ошибка. Отсутствует сдвиг от UTC для следующих пунктов:";
                                for (int i = 0; i < siteNoOffset.Count; i++)
                                {
                                    msg += "\n" + (i + 1) + ".\t" + siteNoOffset[i].Code + " " + siteNoOffset[i].Name;
                                }
                                throw new Exception(msg);
                            }
                            break;
                        case "param":
                            int? gisLevelId = null;
                            try
                            {
                                gisLevelId = Convert.ToInt32(opt.Attributes["gLevelId"].Value);
                            }
                            catch { }

                            Param param = new Param(
                                Convert.ToInt32(opt.Attributes["gParamId"].Value),
                                gisLevelId,
                                Convert.ToInt32(opt.Attributes["hParamId"].Value),
                                opt.Attributes["multip"].Value
                            );
                            //param.GisParamId = Convert.ToInt32(opt.Attributes["gParamId"].Value);
                            //param.VariableIdAmur = Convert.ToInt32(opt.Attributes["hParamId"].Value);

                            //try
                            //{
                            //    param.GisLevelId = Convert.ToInt32(opt.Attributes["gLevelId"].Value);
                            //}
                            //catch { }
                            //string separator = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
                            //param.Multip = Convert.ToSingle(opt.Attributes["multip"].Value.Replace(".", separator).Replace(",", separator));
                            config.ParamList.Add(param);
                            break;
                        case "#comment":
                            break;
                        default:
                            throw new Exception("Неизвестный таг файла конфигурации для импорта: " + opt.LocalName);
                    }
                }
                #region FillCatalog

                // Найти код порывов ветра МЕЖДУ СРОКАМИ и ЗА ПОЛУСУТКИ.
                // В settings д.б. задан код переменной для порывов ветра В СРОК.
                Param paramGust = config.ParamList.FirstOrDefault(x => x.GisParamId == 133);
                if (paramGust != null)
                {
                    // В срок
                    Variable varGustInTime = svc.GetVariableById(hSvc, paramGust.VariableIdAmur);
                    // Между сроками
                    Variable varGust = svc.GetVariableByKey(hSvc,
                        varGustInTime.VariableTypeId,
                        varGustInTime.TimeId,
                        varGustInTime.UnitId,
                        varGustInTime.DataTypeId,
                        varGustInTime.GeneralCategoryId,
                        varGustInTime.SampleMediumId,
                        3 * 60 * 60,
                        varGustInTime.ValueTypeId);
                    if (varGust != null)
                        config.VariableIdWindGustBetweenObs = varGust.Id;
                    // За полусутки
                    varGust = svc.GetVariableByKey(hSvc,
                        varGustInTime.VariableTypeId,
                        varGustInTime.TimeId,
                        varGustInTime.UnitId,
                        varGustInTime.DataTypeId,
                        varGustInTime.GeneralCategoryId,
                        varGustInTime.SampleMediumId,
                        12 * 60 * 60,
                        varGustInTime.ValueTypeId);
                    if (varGust != null)
                        config.VariableIdWindGustHalfDay = varGust.Id;

                    if (config.VariableIdWindGustBetweenObs < 1 || config.VariableIdWindGustHalfDay < 1)
                        throw new Exception("В БД отсутствует переменная для скорости порывов ветра между сроками и/или за полусутки.");
                }

                // CATALOGS

                List<int> siteIds = config.StationGMList.Select(x => x.SiteIdAmur).Distinct().ToList();
                List<int> varIds = config.ParamList.Select(x => x.VariableIdAmur).Distinct().ToList();
                varIds.Add(config.VariableIdWindGustBetweenObs);
                varIds.Add(config.VariableIdWindGustHalfDay);


                List<Catalog> catalogList = svc.GetCatalogList(hSvc, siteIds, varIds, null, null, null, null);
                    //new List<int> { 0 }, new List<double> { 0 });
                Catalog ctlTemp = catalogList.FirstOrDefault(x => x.Id == 37533);
                int siteId = siteIds.FirstOrDefault(x => x == 1019);
                catalogList = catalogList.Where(x =>
                    (x.OffsetTypeId == 0/*NoOffset*/ && x.OffsetValue == 0)
                    || x.OffsetTypeId == 1/*MarshrutPoleLes*/).ToList();
                ctlTemp = catalogList.FirstOrDefault(x => x.Id == 37533);
                config.CatalogList = catalogList;

                #endregion FillCatalog

                configList.Add(config);
            }
            return configList;
        }

    }
}
