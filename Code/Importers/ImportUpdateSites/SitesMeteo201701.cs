using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FERHRI.Amur.Meta;

namespace ImportUpdate
{
    internal class ImportSitesMeteo201701
    {
        internal enum FileType
        {
            /// <summary>
            /// С регионом
            /// </summary>
            Gerasimov,
            /// <summary>
            /// Без региона
            /// </summary>
            Tretyakov
        }
        /// <summary>
        /// Изменение и занесение новых мет. станций по материалам:
        ///       - Герасимова (ДВ УГМС) 2017.04 - с регионом.
        ///       - Третьяков  (ДВ УГМС) 2017.08 - без региона.
        /// 
        ///Index	Name_ru	Name_lat	Lat	Lng	Height	Tz	nRegion
        ///24265	Мар-Кюель	MAR-KYUEL	67.41666667	132.5833333	819	100	16469
        ///24982	Уега	UEGA	60.71666667	142.7833333	396	100	16469
        ///24988	Арка	ARKA	60.08333333	142.3333333	198	100	16469
        ///...
        /// </summary>
        /// <param name="filePath">Файл с мета-данными</param>
        internal static void Run(string[] filePaths, int orgId, DataManager dmMeta, FileType type)
        {
            int[][] regionAccordance = new int[][]
            {
                new int[]{142,16470},
                new int[]{188,16476},
                new int[]{137,16472},
                new int[]{162,16466},
                new int[]{23,16468},
                new int[]{177,16471},
                new int[]{27,16469},
                new int[]{191,16479},
                new int[]{127,16467}
            };

            for (int i = 0; i < filePaths.Length; i++)
            {
                Console.WriteLine(filePaths[i]);
                StreamReader sr = new StreamReader(filePaths[i], Encoding.GetEncoding(1251));
                int stationsInserted = 0;
                int stationTypeId = (int)FERHRI.Amur.Meta.EnumStationType.MeteoStation;
                int siteTypeId = (int)FERHRI.Amur.Meta.EnumStationType.MeteoStation;

                try
                {
                    string line = sr.ReadLine(); // caption
                    string[] cells;

                    // SCAN FILE
                    while (!sr.EndOfStream)
                    {
                        line = sr.ReadLine();
                        cells = line.Split(';');
                        Console.WriteLine(line);

                        int? regId = null;
                        if (type == FileType.Gerasimov)
                            regId = regionAccordance.FirstOrDefault(x => x[1] == int.Parse(cells[7].Trim()))[0];
                        int siteId = -1;
                        string stationCode = cells[0].Trim();
                        Station station = dmMeta.StationRepository.Select(stationCode);

                        // STATION NOT EXISTS?
                        if (station == null)
                        {
                            // INSERT STATION
                            station = new Station(-1, stationCode, cells[1].Trim(), stationTypeId, cells[2].Trim(), regId, orgId);
                            int stationId = dmMeta.StationRepository.Insert(station);
                            station = dmMeta.StationRepository.Select(stationId);
                            // INSERT SITE
                            siteId = dmMeta.SiteRepository.Insert(new Site(-1, stationId, siteTypeId, null, null));
                            stationsInserted++;
                        }
                        else
                        {
                            // UPDATE STATION NAME
                            station.Name = cells[1].Trim();
                            station.NameEng = cells[2].Trim();
                            dmMeta.StationRepository.Update(station);
                            // GET EXISTING SITE ID
                            // TODO: Переделать под несколько пунктов! Иначе для ГП этот алгоритм будет некорректным.
                            List<Site> sites = dmMeta.SiteRepository.Select(station.Id, null);
                            if (sites.Count != 1)
                                throw new Exception("(sites.Count != 1)");
                            siteId = sites[0].Id;
                        }

                        // UPDATE|INSERT SITE ATTR 
                        dmMeta.EntityAttrRepository.InsertUpdateValue("site", siteId, 1000, DateTime.Today, cells[3].Trim()); // lat
                        dmMeta.EntityAttrRepository.InsertUpdateValue("site", siteId, 1001, DateTime.Today, cells[4].Trim()); // lon
                        dmMeta.EntityAttrRepository.InsertUpdateValue("site", siteId, 1006, DateTime.Today, cells[5].Trim()); // высота метеоплощадки
                        dmMeta.EntityAttrRepository.InsertUpdateValue("site", siteId, 1008, DateTime.Today, cells[6].Trim()); // Tz

                        // UPDATE STATION ADDR
                        if (regId.HasValue)
                            dmMeta.StationAddrRegionRepository.InsertUpdate(new StationAddrRegion(station.Id, (int)regId));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    sr.Close();
                    Console.WriteLine("Stations inserted  " + stationsInserted);
                }
            }
        }
    }
}
