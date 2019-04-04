using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
//using SOV.Common;

namespace SOV.Geo
{
    /// <summary>
    /// 
    /// Регулярная пространственная географическая сетка заданная в МИНУТАХ.
    /// Индексы и обход сетки формируется последовательно для каждой широты по всем долготам.
    /// 
    /// </summary>
    [DataContract]
    public partial class Grid
    {
        /// <summary>
        /// Уникальный код сетки.
        /// </summary>
        // MEMBERS
        [DataMember]
        public int? Id { get; set; }
        /// <summary>
        /// Тип сетки.
        /// </summary>
        [DataMember]
        public int TypeId { get; set; }
        /// <summary>
        /// Количество точек сетки по оси Х.
        /// </summary>
        private int LonsQ { get { return LonsMin.Length; } }
        /// <summary>
        /// Количество точек сетки по оси Y.
        /// </summary>
        private int LatsQ { get { return LatsMin.Length; } }
        /// <summary>
        /// Шаг сетки по оси X (мин).
        /// </summary>
        [DataMember]
        public double LonStepMin { get; set; }
        /// <summary>
        /// Шаг сетки по оси Y (мин).
        /// </summary>
        [DataMember]
        public double LatStepMin { get; set; }
        /// <summary>
        /// Начальная (отсчётная) широта сетки (мин).
        /// </summary>
        [DataMember]
        public double LatStartMin { get; set; }
        /// <summary>
        /// Начальная (отсчётная) долгота сетки (мин).
        /// </summary>
        [DataMember]
        public double LonStartMin { get; set; }
        /// <summary>
        /// Описание сетки.
        /// </summary>
        [DataMember]
        public string Description;
        /// <summary>
        /// Широты сетки.
        /// </summary>
        [DataMember]
        public double[] LatsMin;
        /// <summary>
        /// Долготы сетки.
        /// </summary>
        [DataMember]
        public double[] LonsMin;

        #region GAUSS_LAT_94
        /// <summary>
        /// Массив широт Гауссовой сетки проекта реанализа NCEP/NCAR.
        /// 94 широты
        /// </summary>
        public static double[] GAYSS_LAT_94 = new double[] {
			88.542, 86.653, 84.753, 82.851, 80.947, 79.043, 77.139, 75.235, 73.331, 71.426, 
            69.522, 67.617, 65.713, 63.808, 61.903, 59.999, 58.094, 56.189, 54.285,  52.38, 
            50.475, 48.571, 46.666, 44.761, 42.856, 40.952, 39.047, 37.142, 35.238, 33.333, 
            31.428, 29.523, 27.619, 25.714, 23.809, 21.904, 20, 18.095,  16.19, 14.286, 
            12.381, 10.476,  8.571,  6.667, 4.762, 2.857, 0.952, -0.952, -2.857, -4.762, 
            -6.667, -8.571, -10.476, -12.381, -14.286, -16.19, -18.095, -20, -21.904, -23.809, 
            -25.714, -27.619, -29.523, -31.428, -33.333, -35.238, -37.142, -39.047, -40.952, -42.856, 
            -44.761, -46.666, -48.571, -50.475, -52.38, -54.285, -56.189, -58.094, -59.999, -61.903, 
            -63.808, -65.713, -67.617, -69.522, -71.426, -73.331, -75.235, -77.139, -79.043, -80.947, 
            -82.851, -84.753, -86.653, -88.542
		};
        #endregion

        /// <summary>
        /// Инициализация экземпляра класса типа Projection.LATLON.
        /// </summary>
        public Grid(int? id, int typeId, int latQ, double latStartMin, double latStepMin, int lonQ, double lonStartMin, double lonStepMin, string Description = null)
        {
            this.Id = id;
            this.TypeId = typeId;
            this.LonStepMin = lonStepMin;
            this.LatStepMin = latStepMin;
            this.LatStartMin = latStartMin;
            this.LonStartMin = lonStartMin;
            this.Description = Description;

            LonsMin = ToArrayLonMin(lonQ);
            LatsMin = ToArrayLatMin(latQ);

            switch ((SOV.Geo.EnumProjection)TypeId)
            {
                case SOV.Geo.EnumProjection.LATLON:
                    Test();
                    break;
                default:
                    throw new Exception("(Projection)TypeId");
            }
        }

        /// <summary>
        /// Инициализация экземпляра класса типа Projection.GAUSS
        /// с предопределёнными широтами.
        /// </summary>
        public Grid(int? id, double[] gaussLatsGrd, int lonQ, double lonStartMin, double lonStepMin, string Description = null)
        {
            if (gaussLatsGrd[0] < gaussLatsGrd[gaussLatsGrd.Length - 1]) throw new Exception("(gaussLatsGrd[0] < gaussLatsGrd[gaussLatsGrd.Length - 1])");

            this.Id = id;
            this.TypeId = (int)SOV.Geo.EnumProjection.GAUSS;

            this.LonStepMin = lonStepMin;
            this.LonStartMin = lonStartMin;

            this.LatStepMin = double.NaN;
            this.LatStartMin = double.NaN;

            this.Description = Description;

            LonsMin = ToArrayLonMin(lonQ);
            LatsMin = ToArrayLatMin(gaussLatsGrd);

            Test();
        }

        private void Test()
        {
            if (WestMin > GeoPoint.GEO_MAXLONGITUDE_GRD * 60 || WestMin < GeoPoint.GEO_MINLONGITUDE_GRD * 60
                || EastMin > GeoPoint.GEO_MAXLONGITUDE_GRD * 60 || EastMin < GeoPoint.GEO_MINLONGITUDE_GRD * 60
                || NorthMin > GeoPoint.GEO_MAXLATITUDE_GRD * 60 || NorthMin < GeoPoint.GEO_MINLATITUDE_GRD * 60
                || SouthMin > GeoPoint.GEO_MAXLATITUDE_GRD * 60 || SouthMin < GeoPoint.GEO_MINLATITUDE_GRD * 60
            ) throw new Exception("LATLON grid bounds is wrong : [" + ToString() + "]");

            if (
                (
                    TypeId != (int)EnumProjection.GAUSS &&
                    (Math.IEEERemainder(Math.Abs(NorthMin - SouthMin), LatStepMin) != 0 && LatStepMin != 0)
                )
                ||
                (Math.IEEERemainder(Math.Abs(EastMin - WestMin), LonStepMin) != 0 && LonStepMin != 0)
            ) throw new Exception("LATLON grid step is wrong : [" + ToString() + "]");
        }


        /// <summary>
        /// Получить количество узлов сетки заданной в формальных параметрах метода.
        /// </summary>
        /// <param name="side1Min">Одна широта (догота) сетки.</param>
        /// <param name="side2Min">Другая широта (догота) сетки.</param>
        /// <param name="stepMin">Шаг между заданными широтами (долготами) сетки.</param>
        /// <returns></returns>
        private static int PointsQCalc(double side1Min, double side2Min, double stepMin)
        {
            double size = Math.Abs(side1Min - side2Min);
            stepMin = (stepMin < 0) ? -stepMin : stepMin;
            if (Math.IEEERemainder(size, stepMin) != 0)
                return 0;
            return (int)(size / stepMin + 1);
        }
        /// <summary>
        /// Получить географический регион из границ сетки.
        /// </summary>
        public GeoRectangle GeoRectangle
        {
            get
            {
                return new GeoRectangle(SouthMin / 60, NorthMin / 60, WestMin / 60, EastMin / 60, "Obtained from Grid");
            }
        }
        public Grid Truncate2(GeoRectangle geoReg)
        {
            List<int>[] iLatLon = GetLatLonIdxs(geoReg);

            return new Grid(null, TypeId,
                iLatLon[0].Count, LatsMin[iLatLon[0][0]], LatStepMin,
                iLatLon[1].Count, LonsMin[iLatLon[1][0]], LonStepMin,
                "Trancated from grid [" + this + "]");
        }

        //public Grid Truncate(GeoRectangle geoReg)
        //{
        //    if (geoReg.isIntersectGreenwich) throw new Exception("geoReg.isIntersectGreenwich()");
        //    if (TypeId != (int)Projection.LATLON) throw new Exception("(gridTypeId != Projection.LATLON)");

        //    // LONS
        //    List<double> lonsMin = new List<double>();
        //    for (int i = 0; i < LonsMin.Length; i++)
        //    {
        //        if (geoReg.IsLonGrdBelong(LonsMin[i] / 60))
        //            lonsMin.Add(LonsMin[i]);
        //    }

        //    // LATS & RETURN
        //    List<double> latsMin = new List<double>();
        //    switch (TypeId)
        //    {
        //        case (int)Projection.LATLON:
        //            for (int i = 0; i < LatsMin.Length; i++)
        //            {
        //                if (geoReg.IsLatGrdBelong(LatsMin[i] / 60))
        //                    latsMin.Add(LatsMin[i]);
        //            }
        //            return new Grid(null, TypeId, latsMin.Count, latsMin[0], LatStepMin, lonsMin.Count, lonsMin[0], LonStepMin, "Truncated from grid [" + ToString() + "]");
        //        case (int)Projection.GAUSS:
        //            for (int i = 0; i < LatsMin.Length; i++)
        //            {
        //                if (geoReg.IsLatGrdBelong(LatsMin[i] / 60))
        //                    latsMin.Add(LatsMin[i] / 60);
        //            }
        //            return new Grid(null, latsMin.ToArray(), lonsMin.Count, lonsMin[0], LonStepMin, "Truncated from grid [" + ToString() + "]");
        //        default:
        //            throw new Exception("switch (TypeId) : " + TypeId);
        //    }
        //}

        /// <summary>
        /// Получить сетку, включающую в себя все заданные по широте. По долготе от 0 до (360 * 60 - stepX) 
        /// Учитывать совместимость сеток по шагу.
        /// </summary>
        static public Grid getGridMaxOf(Grid[] grids)
        {
            throw new NotImplementedException();

            //int i;
            //double stepX = grids[0].stepXMin, stepY = grids[0].stepYMin,
            //    northBound = grids[0].NorthBoundMin,
            //    southBound = grids[0].SouthBoundMin,
            //    westBound = 0, eastBound = 360 * 60 - stepX;
            //int gridTypeId = grids[0].gridTypeId;
            //char model = (char)grids[0].Model;

            //for (i = 0; i < grids.Length; i++)
            //{
            //    // TEST
            //    if (gridTypeId != grids[i].gridTypeId ||
            //        model != grids[i].Model ||
            //        stepX != grids[i].stepXMin || stepY != grids[i].stepYMin
            //    )
            //        return null;

            //    // Get boundaries
            //    double n = grids[i].NorthBoundMin; if (n > northBound) northBound = n;
            //    double s = grids[i].SouthBoundMin; if (s < southBound) southBound = s;
            //}
            //Grid grid = new Grid
            //(
            //    null,
            //    //null, 
            //    gridTypeId, grids[0].centerId, null, model,
            //    PointsQCalc(eastBound, westBound, stepX),
            //    PointsQCalc(northBound, southBound, stepY),
            //    stepX, stepY,
            //    (stepY > 0) ? southBound : northBound, (stepX > 0) ? westBound : eastBound,
            //    "Grid.getGridMaxOf()"
            //);
            //// Алгоритм не должен позволять строить такое поле? Или как...
            //if (grid.EastBoundMin < grid.WestBoundMin)
            //    throw new Exception("gridMaxOf.getEastBound < gridMaxOf.getWestBound");

            //return grid;
        }
        /// <summary>
        /// Возвращает строку, представляющую текущий экземпляр класса.
        /// </summary>
        /// <returns>Строка, представляющая текущий экземпляр класса.</returns>
        public override string ToString()
        {
            return
                SouthMin / 60 + " : " + NorthMin / 60
                + "; " +
                WestMin / 60 + " : " + EastMin / 60
                + "; "
                + (((EnumProjection)this.TypeId == EnumProjection.GAUSS) ? LatStepMin : LatStepMin / 60) + " : " + LonStepMin / 60
                + "; " +
                LatsQ + "*" + LonsQ + "=" + LatsQ * LonsQ
            ;
        }
        /// <summary>
        /// Получить индекс узла сетки по заданной широте и долготе узла.
        /// </summary>
        /// <returns>Индекс узла или -1, если узел не принадлежит сетке.</returns>
        public int GetNodeIdx(double latMin, double lonMin)
        {
            int latIdx = GetLatIdx(latMin);
            int lonIdx = GetLonIdx(lonMin);

            if (latIdx < 0 || lonIdx < 0)
                return -1;

            return latIdx * LonsQ + lonIdx;
        }
        public int GetNodeIdx(int iLat, int iLon)
        {
            if (iLat >= 0 && iLon >= 0)
                return iLat * LonsQ + iLon;
            return -1;
        }
        /// <summary>
        /// Индексы узлов секи (последовательные) попадающий в регион.
        /// </summary>
        /// <param name="gr"></param>
        /// <returns></returns>
        public List<int> GetNodesIdxs(GeoRectangle gr)
        {
            List<int>[] iLatLon = GetLatLonIdxs(gr);
            return GetNodeIdxs(iLatLon);
        }
        public List<GeoPoint> GetGeoPoints(GeoRectangle gr)
        {
            List<int>[] iLatLon = GetLatLonIdxs(gr);
            List<GeoPoint> ret = new List<GeoPoint>();
            for (int i = 0; i < iLatLon[0].Count; i++)
            {
                for (int j = 0; j < iLatLon[1].Count; j++)
                {
                    ret.Add(new GeoPoint(LatsMin[iLatLon[0][i]] / 60, LonsMin[iLatLon[1][j]] / 60));
                }
            }
            return ret;
        }
        public GeoPoint[] GetGeoPoints4Indeces(List<int> indeces)
        {
            GeoPoint[] ret = new GeoPoint[indeces.Count];
            for (int i = 0; i < indeces.Count; i++)
            {
                ret[i] = GetGeoPoint4Index(indeces[i]);
            }
            return ret;
        }
        public GeoPoint GetGeoPoint4Index(int index)
        {
            int iLat = index / LonsQ;
            int iLon = index - iLat * LonsQ;
            return new GeoPoint(LatsMin[iLat] / 60, LonsMin[iLon] / 60);
        }
        public List<int> GetNodeIdxs(List<int>[] iLatLon)
        {
            List<int> ret = new List<int>();
            for (int i = 0; i < iLatLon[0].Count; i++)
            {
                for (int j = 0; j < iLatLon[1].Count; j++)
                {
                    ret.Add(GetNodeIdx(iLatLon[0][i], iLatLon[1][j]));
                }
            }
            return ret;
        }
        /// <summary>
        /// Пересекает ли сетка меридиан Гринвича?
        /// </summary>
        public bool IsIntersectGreenwich
        {
            get
            {
                return IsIntersectGreenwichFromEast || IsIntersectGreenwichFromWest;
            }
        }
        public bool IsIntersectGreenwichFromEast
        {
            get
            {
                double lonLastMin = LonsMin[0] + LonStepMin * (LonsQ - 1);
                return lonLastMin < 0;
            }
        }
        public bool IsIntersectGreenwichFromWest
        {
            get
            {
                double lonLastMin = LonsMin[0] + LonStepMin * (LonsQ - 1);
                return lonLastMin >= 360 * 60;
            }
        }

        /// <summary>
        /// Получить индекс заданной широты в сетке.
        /// </summary>
        /// <returns>Индекс широты или -1 если сетка не содержит широты.</returns>
        public int GetLatIdx(double latMin)
        {
            int i = GetNearestLatIdx(latMin);
            if (i >= 0 && latMin - LatsMin[i] == 0.0)
                return i;
            //for (int i = 0; i < LatsMin.Length; i++)
            //{
            //    if (latMin == LatsMin[i])
            //        return i;
            //}
            return -1;
        }
        /// <summary>
        /// Получить индекс заданной долготы в сетке.
        /// </summary>
        /// <returns>Индекс долготы или -1 если сетка не содержит долготы.</returns>
        public int GetLonIdx(double lonMin)
        {
            int i = GetNearestLonIdx(lonMin);
            if (i >= 0 && lonMin - LonsMin[i] == 0.0)
                return i;
            //for (int i = 0; i < LonsMin.Length; i++)
            //{
            //    if (lonMin == LonsMin[i])
            //        return i;
            //}
            return -1;
        }
        /// <summary>
        /// Получить индекс ближайшей в сетке долготы к заданной
        /// </summary>
        /// <returns>Индекс ближайшей долготы или -1, если заданная долгота не принадлежит сетке.</returns>
        public int GetNearestLonIdx(double lonMin)
        {
            int iRet = -1;
            if (GeoRectangle.IsLonGrdBelong(lonMin / 60))
            {
                double d = double.MaxValue;
                for (int i = 0; i < LonsMin.Length; i++)
                {
                    double dd = Math.Abs(lonMin - LonsMin[i]);
                    if (d > dd)
                    {
                        d = dd;
                        iRet = i;
                    }
                }
            }
            return iRet;
        }

        /// <summary>
        /// Получить индекс ближайшей в сетке ширготы к заданной
        /// </summary>
        /// <returns>Индекс ближайшей ширготы или -1, если заданная ширгота не принадлежит сетке.</returns>
        public int GetNearestLatIdx(double latMin)
        {
            if (GeoRectangle.IsLatGrdBelong(latMin / 60))
            {
                //double lat = LatsMin.Min(x => Math.Abs(latMin - x));
                //int i = System.Array.IndexOf(LatsMin, lat);
                //return i;

                double d = double.MaxValue;
                int iRet = -1;
                for (int i = 0; i < LatsMin.Length; i++)
                {
                    double dd = Math.Abs(latMin - LatsMin[i]);
                    if (d > dd)
                    {
                        d = dd;
                        iRet = i;
                    }
                }
                return iRet;
            }
            return -1;
        }
        public bool IsLatFromNorth
        {
            get
            {
                return (LatsQ == 1 || LatsMin[0] > LatsMin[1]) ? true : false;
            }
        }
        public bool IsLonFromWest
        {
            get
            {
                return (LonsQ == 1 || LonsMin[0] < LonsMin[1] || IsIntersectGreenwichFromWest) ? true : false;
            }
        }
        /// <summary>
        /// Северная граница сетки (мин).
        /// </summary>
        public double NorthMin
        {
            get
            {
                return IsLatFromNorth ? LatsMin[0] : LatsMin[LatsQ - 1];
            }
        }
        /// <summary>
        /// Южная граница сетки (мин).
        /// </summary>
        public double SouthMin
        {
            get
            {
                return IsLatFromNorth ? LatsMin[LatsQ - 1] : LatsMin[0];
            }
        }
        /// <summary>
        /// Западная граница сетки (мин).
        /// </summary>
        public double WestMin
        {
            get
            {
                return IsLonFromWest ? LonsMin[0] : LonsMin[LonsQ - 1];
            }
        }
        /// <summary>
        /// Восточная граница сетки (мин).
        /// </summary>
        public double EastMin
        {
            get
            {
                return IsLonFromWest ? LonsMin[LonsQ - 1] : LonsMin[0];
            }
        }
        /// <summary>
        /// Получить количество узлов сетки.
        /// </summary>
        public int PointsQ
        {
            get
            {
                return LatsQ * LonsQ;
            }
        }
        /// <summary>
        /// Определить принадлежит ли точка региону сетки.
        /// </summary>
        /// <param name="geoPoint">Точка.</param>
        /// <returns>true, если принадлежит.</returns>
        public bool IsPointBelongGridRectangle(double latGrd, double lonGrd)
        {
            return GeoRectangle.IsPointBelong(latGrd, lonGrd);
        }
        /// <summary>
        /// Получить ближайщий к точке узел сетки.
        /// </summary>
        /// <param name="geoPoint">Точка.</param>
        /// <returns>Ближайший узел сетки или null, если точка не лежит в пределах региона сетки.</returns>
        public GeoPoint GetNearestPoint(GeoPoint geoPoint)
        {
            int[] i = GetNearestPointIdx(geoPoint.LatMin, geoPoint.LonMin);

            if (i != null)
            {
                return new GeoPoint(LatsMin[i[0]] / 60, LonsMin[i[1]] / 60);
            }

            return null;
        }
        /// <summary>
        /// Получить индексы широты и долготы ближайшего узла сетки.
        /// </summary>
        /// <param name="latMin">широта</param>
        /// <param name="lonMin">долгота</param>
        /// <returns>Индексы широты и долготы ближайшего узла сетки</returns>
        public int[/*iLat, iLon*/] GetNearestPointIdx(double latMin, double lonMin)
        {
            int iLat = GetNearestLatIdx(latMin);
            int iLon = GetNearestLonIdx(lonMin);

            if (iLon >= 0 && iLat >= 0)
            {
                return new int[] { iLat, iLon };
            }

            return null;
        }
        /// <summary>
        /// Отбор узла сетки, ближайшего к заданному с условием определения близости.
        /// return int[/*iLat, iLon, iPoint*/]
        /// </summary>
        /// <param name="geoPoint">Точка, к которой ищется ближайший узел сетки.</param>
        /// <param name="nearestType">Условие определения близости узла к точке.</param>
        /// <returns>[/*iLat, iLon, iPoint*/]</returns>
        public int[/*iLat, iLon*/] GetNearestPoint(double latMin, double lonMin, EnumPointNearestType nearestType)
        {
            int[] ret = null;
            List<int[]> nodesIdx = GetNearestPointsIdx(latMin, lonMin);

            if (nodesIdx != null)
            {
                GeoPoint pointIn = new GeoPoint(latMin / 60, lonMin / 60);
                int iPoint = 0;
                GeoPoint pointSel = new GeoPoint(LatsMin[nodesIdx[0][0]] / 60, LonsMin[nodesIdx[0][1]] / 60);
                double distSel = pointSel.getDistMin(pointIn); ;

                for (int i = 1; i < nodesIdx.Count; i++)
                {
                    GeoPoint pointCur = new GeoPoint(LatsMin[nodesIdx[i][0]] / 60, LonsMin[nodesIdx[i][1]] / 60);
                    double distCur = pointCur.getDistMin(pointIn);

                    switch (nearestType)
                    {
                        case EnumPointNearestType.Nearest2East:
                            if (IsIntersectGreenwich) throw new Exception("(IsIntersectGreenwich)");

                            if (pointCur.LonGrd > pointSel.LonGrd)
                            {
                                iPoint = i;
                                pointSel = pointCur;
                                distSel = distCur;
                            }
                            break;
                        case EnumPointNearestType.Nearest:
                            if (distCur < distSel)
                            {
                                iPoint = i;
                                pointSel = pointCur;
                                distSel = distCur;
                            }
                            break;
                        default:
                            throw new NotImplementedException("switch (nearestType) : " + nearestType);
                    }
                }
                ret = nodesIdx[iPoint];
            }
            return ret;
        }
        /// <summary>
        /// Ближайщие к точке узлы сетки (максимально 4 шт) или null, если точка не лежит в пределах региона сетки.
        /// List int[latIdx,lonIdx,Idx]
        /// </summary>
        /// <param name="geoPoint">Точка.</param>
        /// <returns>  List int[latIdx,lonIdx,Idx] </returns>
        public List<GeoPoint> GetNearestPoints(double latMin, double lonMin)
        {
            List<int[/*latIdx,lonIdx*/]> ll = GetNearestPointsIdx(latMin, lonMin);
            if (ll != null)
            {
                List<GeoPoint> ret = new List<GeoPoint>();
                for (int i = 0; i < ll.Count; i++)
                {
                    ret.Add(new GeoPoint(LatsMin[ll[i][0]] / 60, LonsMin[ll[i][1]] / 60));
                }
                return ret;
            }
            return null;
        }
        /// <summary>
        /// Ближайщие к точке узлы сетки (максимально 4 шт) или null, если точка не лежит в пределах региона сетки.
        /// List int[latIdx,lonIdx,Idx]
        /// </summary>
        /// <param name="geoPoint">Точка.</param>
        /// <returns>  List int[latIdx,lonIdx,Idx] </returns>
        public List<int[/*latIdx,lonIdx*/]> GetNearestPointsIdx(double latMin, double lonMin)
        {
            int[] iLatLon = GetNearestPointIdx(latMin, lonMin);

            if (iLatLon != null)
            {
                int iLat1 = iLatLon[0];
                int iLat2 = -1;
                if (LatsMin[iLat1] > latMin)
                {
                    if (IsLatFromNorth) iLat2 = iLat1 + 1;
                    else iLat2 = iLat1 - 1;
                }
                else if (LatsMin[iLat1] < latMin)
                {
                    if (IsLatFromNorth) iLat2 = iLat1 - 1;
                    else iLat2 = iLat1 + 1;
                }

                int iLon1 = iLatLon[1];
                int iLon2 = -1;
                if (LonsMin[iLon1] > lonMin)
                {
                    if (IsLonFromWest) iLon2 = iLon1 - 1;
                    else iLon2 = iLon1 + 1;
                }
                else if (LonsMin[iLon1] < lonMin)
                {
                    if (IsLonFromWest) iLon2 = iLon1 + 1;
                    else iLon2 = iLon1 - 1;
                }

                List<int[]> ret = new List<int[]>();
                ret.Add(new int[] { iLat1, iLon1 });
                if (iLat2 >= 0) ret.Add(new int[] { iLat2, iLon1 });
                if (iLon2 >= 0) ret.Add(new int[] { iLat1, iLon2 });
                if (iLat2 >= 0 && iLon2 >= 0) ret.Add(new int[] { iLat2, iLon2 });

                return ret;
            }
            return null;
        }

        /// <summary>
        /// 
        /// Построить grid по заданной центральной точке и количеству шагов на север/юг и запад/восток.
        /// 
        /// В общем случае заданная в качестве параметра метода центральная точка не совпадает с узлом сетки, 
        /// поэтому по ней сначала определяется ближайшая точка сетки, которая и будет центральной.
        /// 
        /// </summary>
        /// <param name="centralPoint">По данной точке выбирается ближайшая точка сетки, которая и будет центральной точкой новой сетки.</param>
        /// <param name="pointsQ2NorthSouth">Количество узлов новой сетки на юг и на север от центрального узла.</param>
        /// <param name="pointsQ2WestEast">Количество узлов новой сетки на запад и восток от центрального узла.</param>
        /// <returns>Новая сетка</returns>
        public Grid GetGridByCentralPoint(GeoPoint centralPoint, int pointsQ2NorthSouth, int pointsQ2WestEast)
        {
            throw new NotImplementedException();

            //GeoPoint cPoint = getNearestGridPoint(centralPoint);
            //if (cPoint == null) return null;

            //switch ((Projection)TypeId)
            //{
            //    case Projection.LATLON:
            //        return new Grid(
            //            null,
            //            //null, 
            //            this.TypeId, 0, '0', '0',
            //            pointsQ2WestEast * 2 + 1, pointsQ2NorthSouth * 2 + 1, this.LonStepMin, this.LatStepMin,
            //            cPoint.LatMin + pointsQ2NorthSouth * (-this.LatStepMin),
            //            cPoint.LonMin + pointsQ2WestEast * (-this.LonStepMin),
            //            "Generated by Grid.getGridByCentralPoint");
            //    default:
            //        throw new Exception("switch (gridTypeId): " + TypeId);
            //}
        }
        /// <summary>
        /// Проверка совместимости сеток.
        /// </summary>
        /// <param name="grid2">Одна из сеток. Вторая - this.</param>
        /// <returns>true, если сетки совместимы.</returns>
        public bool IsCompatible(Grid grid2)
        {
            return IsCompatible(this, grid2);
        }
        /// <summary>
        /// Проверка совместимости сеток.
        /// </summary>
        /// <param name="grid1">Первая из сеток.</param>
        /// /// <param name="grid2">вторая из сеток.</param>
        /// <returns>true, если сетки совместимы.</returns>
        static public bool IsCompatible(Grid grid1, Grid grid2)
        {
            if (grid1.TypeId != grid2.TypeId && grid1.LatsQ != grid2.LatsQ && grid1.LonsQ != grid2.LonsQ)
                return false;

            for (int i = 0; i < grid1.LonsQ; i++)
            {
                if (grid1.LonsMin[i] != grid2.LonsMin[i])
                    return false;
            }

            for (int i = 0; i < grid1.LatsQ; i++)
            {
                if (grid1.LatsMin[i] != grid2.LatsMin[i])
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Проверка совместимости сеток в заданном регионе.
        /// т.е. все точки grid1, принадлежащие geoRec есть в grid2.
        /// Если в grid1 нет ни одной точки в регионе geoRec, то возвращается false
        /// </summary>
        /// <param name="grid1">Первая из сеток.</param>
        /// <param name="grid2">вторая из сеток.</param>
        /// <param name="geoRec"></param>
        /// <returns>true, если сетки совместимы.</returns>
        static public bool isCompatible(Grid grid1, Grid grid2, GeoRectangle geoRec)
        {
            throw new NotImplementedException();

            //if (isCompatible(grid1, grid2))
            //    return true;
            //if (grid1.gridTypeId != grid2.gridTypeId || geoRec == (GeoRectangle)null)
            //    return false;

            //GridScaner gs = new GridScaner(grid1);
            //int countPoint = 0;
            //while (!gs.EOF)
            //{
            //    GeoPoint curGeoPoint = new GeoPoint(gs.CurLatGrd, gs.CurLonGrd);
            //    if (geoRec.IsPointBelong(curGeoPoint.LatGrd, curGeoPoint.LonGrd))
            //    {
            //        countPoint++;
            //        if (!grid2.isPointBelongGridRectangle(curGeoPoint))
            //            return false;
            //    }
            //    gs.moveNext();
            //}
            //if (countPoint > 0)
            //    return true;
            //else
            //    return false;
        }
        /// <summary>
        /// Получить индексы, соответствующие широты и долготы узлов сетки, попадающих в регион.
        /// return new List<int>[] { ilats, ilons };
        /// </summary>
        /// <param name="geoRec">Регион отбора точек.</param>
        /// <returns>return new List<int>[] { ilats, ilons };</returns>
        public List<int>[/*iLat, iLon*/] GetLatLonIdxs(GeoRectangle geoRec)
        {
            return new List<int>[] { GetLatIdxs(geoRec), GetLonIdxs(geoRec) };
        }
        public List<int> GetLonIdxs(GeoRectangle geoRec)
        {
            //if (geoRec.isIntersectGreenwich || this.IsIntersectGreenwich)
            //    throw new Exception("(geoRec.isIntersectGreenwich || this.IsIntersectGreenwich)");

            List<int> ret = new List<int>();
            for (int i = 0; i < LonsQ; i++)
            {
                if (geoRec.IsLonGrdBelong(LonsMin[i] / 60))
                    ret.Add(i);
            }
            return ret;
        }
        public double[] GetLatsMin(List<int> ilats)
        {
            double[] ret = new double[ilats.Count];
            for (int i = 0; i < ret.Length; i++)
            {
                ret[i] = LatsMin[ilats[i]];
            }
            return ret;
        }
        public double[] GetLonsMin(List<int> ilons)
        {
            double[] ret = new double[ilons.Count];
            for (int i = 0; i < ret.Length; i++)
            {
                ret[i] = LonsMin[ilons[i]];
            }
            return ret;
        }
        public List<int> GetLatIdxs(GeoRectangle geoRec)
        {
            List<int> ret = new List<int>();

            // LAT
            for (int i = 0; i < LatsQ; i++)
            {
                if (geoRec.IsLatGrdBelong(LatsMin[i] / 60))
                    ret.Add(i);
            }
            return ret;
        }
        /// <summary>
        /// Получить индексы узлов сетки this, соответствующие координатам указанным точкам.
        /// </summary>
        /// <param name="pointCoords">Координаты точек (в минутах!!): [0][0...N] - широта, [1][0...N] - долгота.</param>
        /// <returns>Массив индексов широт, долгот узлов сетки, соответствующих координатам указанных точек. null означает отсутствие узла с искомой координатой.</returns>
        public List<int[/*iLat,iLon*/]> GetNodeLLIdxs(List<GeoPoint> points)
        {
            List<int[]> ret = new List<int[]>(points.Count);

            for (int i = 0; i < points.Count; i++)
            {
                int iLat = GetLatIdx(points[i].LatMin);
                int iLon = GetLonIdx(points[i].LonMin);
                if (iLat >= 0 && iLon >= 0)
                    ret.Add(new[] { iLat, iLon });
                else
                    ret.Add(null);
            }
            return ret;
        }
        /// <summary>
        /// Массив долгот сетки в минутах.
        /// Используется только в конструкторах.
        /// </summary>
        private double[] ToArrayLonMin(int lonQ)
        {
            double[] ret = new double[lonQ];
            for (int i = 0; i < lonQ; i++)
            {
                ret[i] = LonStartMin + LonStepMin * i;
            }
            return ret;
        }
        /// <summary>
        /// Массив долгот сетки в минутах.
        /// Используется только в конструкторах.
        /// </summary>
        private double[] ToArrayLatMin(int latQ)
        {
            double[] ret = new double[latQ];
            for (int i = 0; i < latQ; i++)
            {
                ret[i] = LatStartMin + LatStepMin * i;
            }
            return ret;
        }
        private double[] ToArrayLatMin(double[] gaussLatGrd)
        {
            double[] ret = new double[gaussLatGrd.Length];
            for (int i = 0; i < gaussLatGrd.Length; i++)
            {
                ret[i] = gaussLatGrd[i] * 60.0;
            }
            return ret;
        }
        /// <summary>
        /// Перейти к первому узлу сетки.
        /// </summary>
        public void MoveFirst()
        {
            CurLatMin = LatStartMin;
            CurLonMin = LonStartMin;
            curLatIdx = 0;
            CurLonIdx = 0;
            CurPointIdx = 0;
            isEOF = false;
        }
        double _curLonMin = double.NaN;
        /// <summary>
        /// Долгота текущего узла сетки (мин).
        /// </summary>
        public double CurLonMin
        {
            set
            {
                _curLonMin = value;
            }
            get
            {
                if (_curLonMin >= GeoPoint.GEO_MAXLONGITUDE_MIN)
                    return _curLonMin - GeoPoint.GEO_MAXLONGITUDE_MIN;
                if (_curLonMin < 0)
                    return GeoPoint.GEO_MAXLONGITUDE_MIN - _curLonMin;
                return _curLonMin;
            }
        }
        /// <summary>
        /// Индекс текущего узла сетки.
        /// </summary>
        public int CurPointIdx { get; set; }

        bool isEOF = true;
        /// <summary>
        /// Достигнут конец сетки (последняя точка сетки).
        /// </summary>
        public bool EOF
        {
            get { return isEOF; }
        }
        public double CurLatMin;
        int curLatIdx;
        /// <summary>
        /// Индекс текущей широты.
        /// </summary>
        public int CurLatIdx { get; set; }
        /// <summary>
        /// Индекс текущей долготы.
        /// </summary>
        public int CurLonIdx { get; set; }
        /// <summary>
        /// Широта текущего узла сетки (град).
        /// </summary>
        public double CurLatGrd
        {
            get
            {
                return CurLatMin / 60.0;
            }
        }
        /// <summary>
        /// Долгота текущего узла сетки (град).
        /// </summary>
        public double CurLonGrd
        {
            get
            {
                return CurLonMin / 60;
            }
        }
        /// <summary>
        /// Перейти к следующему узлу сетки.
        /// </summary>
        /// <returns>true, если переход успешен. false, если EOF (перебраны все узлы сетки).</returns>
        public bool MoveNext()
        {
            if (isEOF) return false;

            if (CurPointIdx < PointsQ - 1)
            {
                CurPointIdx++;
                if ((CurPointIdx / LonsQ) * LonsQ == CurPointIdx)
                {
                    CurLonMin = LonStartMin;
                    CurLonIdx = 0;

                    curLatIdx++;
                    if ((EnumProjection)TypeId == EnumProjection.GAUSS)
                    {
                        CurLatMin = 60.0 * GAYSS_LAT_94[curLatIdx];
                    }
                    else
                    {
                        CurLatMin += LatStepMin;
                    }
                }
                else
                {
                    CurLonMin += LonStepMin;
                    CurLonIdx++;
                }
                return true;
            }
            else
            {
                isEOF = true;
                return false;
            }
        }

        public int IndexOf(GeoPoint geoPoint)
        {
            int iLat = GetLatIdx(geoPoint.LatMin);
            if (iLat >= 0)
            {
                int iLon = GetLonIdx(geoPoint.LonMin);
                if (iLon >= 0)
                {
                    return iLat * LonsQ + iLon;
                }
            }
            return -1;
        }
    }

    ///// <summary>
    ///// Ближайщие к точке узлы сетки (максимально 4 шт) или null, если точка не лежит в пределах региона сетки.
    ///// List int[latIdx,lonIdx,Idx]
    ///// </summary>
    ///// <param name="geoPoint">Точка.</param>
    ///// <returns>  List int[latIdx,lonIdx,Idx] </returns>
    //public List<int[/*latIdx,lonIdx,Idx*/]> getNearestGridPoints(GeoPoint geoPoint)
    //{
    //    int[] latInd = getNearestLatIdx(geoPoint.LatMin);
    //    int[] lonInd = getNearestLonIdx(geoPoint.LonMin);
    //    if (latInd == null || lonInd == null) { return null; }
    //    List<int[]> ret = new List<int[]>();
    //    for (int i = 0; i < latInd.Length; i++)
    //    {
    //        for (int j = 0; j < lonInd.Length; j++)
    //        {
    //            ret.Add(new int[] { latInd[i], lonInd[j], PointIdxByLatLonIdx(latInd[i], lonInd[j]) });
    //        }
    //    }
    //    return ret;
    //}
    ///// <summary>
    ///// Получить индексы ближайших широт к заданной в сетке
    ///// </summary>
    ///// <returns>индексы ближайших широт к заданной ([0] - меньшей и [1] - большей), в случае совпаения заданной широты и одной из широт сетки выдается индекс этой широты
    ///// null в случае, если широта не входит в границы сетки</returns>
    //public int[] getNearestLatIdx(double latMin)
    //{
    //    switch ((Projection)TypeId)
    //    {
    //        case Projection.GAUSS:
    //            return getNearestLatIdxGauss(latMin / 60);
    //        case Projection.LATLON:
    //            if (latMin > NorthBoundMin || latMin < SouthBoundMin)
    //            {
    //                return null;
    //            }
    //            int ind0 = (int)Math.Floor(Math.Abs(LatStartMin - latMin) / Math.Abs(StepYMin));
    //            int ind1 = ind0 + 1;
    //            if (Math.IEEERemainder(Math.Abs(LatStartMin - latMin), Math.Abs(StepYMin)) == 0)
    //            {
    //                return new int[] { ind0 };
    //            }
    //            if (ind0 == PointsX) { ind1 = 0; }

    //            if (StepXMin > 0)
    //            {
    //                return new int[] { ind0, ind1 };
    //            }
    //            else
    //            {
    //                return new int[] { ind1, ind0 };
    //            }

    //        default:
    //            throw new Exception("Unknown Geo.Projection =" + (Projection)GridTypeId);
    //    }
    //}
    ///// <summary>
    ///// Получить индексы ближайших долгот к заданной в сетке
    ///// </summary>
    ///// <returns>индексы ближайших долгот к заданной ([0] - меньшей и [1] - большей), в случае совпаения заданной долготы и одной из долгот сетки выдается индекс этой долготы
    ///// null в случае, если долгота не входит в границы сетки</returns>
    //public int[] getNearestLonIdx(double lonMin)
    //{
    //    if (lonMin > EastBoundMin || lonMin < WestBoundMin)
    //    {
    //        return null;
    //    }
    //    int ind0 = (int)Math.Floor(Math.Abs(LonStartMin - lonMin) / Math.Abs(StepXMin));
    //    int ind1 = ind0 + 1;
    //    if (Math.IEEERemainder(Math.Abs(LonStartMin - lonMin), Math.Abs(StepXMin)) == 0)
    //    {
    //        return new int[] { ind0 };
    //    }
    //    if (ind0 == PointsX) { ind1 = 0; }

    //    if (StepXMin > 0)
    //    {
    //        return new int[] { ind0, ind1 };
    //    }
    //    else
    //    {
    //        return new int[] { ind1, ind0 };
    //    }
    //}
    ///// <summary>
    ///// Регулярнaя пространственная географическая сетка. Структура класса соответствует структуре таблицы Hmdic..grid.
    ///// </summary>
    //[DataContract]
    //public partial class Grid
    //{
    //    public double[][] LatLonMin { get; set; }
    //    /// <summary>
    //    /// Уникальный код сетки.
    //    /// </summary>
    //    // MEMBERS
    //    [DataMember]
    //    public int Id { get; set; }
    //    /// <summary>
    //    /// Тип сетки.
    //    /// </summary>
    //    [DataMember]
    //    public int GridTypeId { get; set; }
    //    /// <summary>
    //    /// Количество точек сетки по оси Х.
    //    /// </summary>
    //    public int PointsX { get; set; }
    //    /// <summary>
    //    /// Количество точек сетки по оси Y.
    //    /// </summary>
    //    public int PointsY { get; set; }
    //    /// <summary>
    //    /// Шаг сетки по оси X (мин).
    //    /// </summary>
    //    [DataMember]
    //    public double StepXMin { get; set; }
    //    /// <summary>
    //    /// Шаг сетки по оси Y (мин).
    //    /// </summary>
    //    [DataMember]
    //    public double StepYMin { get; set; }
    //    /// <summary>
    //    /// Начальная (отсчётная) широта сетки (мин).
    //    /// </summary>
    //    [DataMember]
    //    public double LatStartMin { get; set; }
    //    /// <summary>
    //    /// Начальная (отсчётная) долгота сетки (мин).
    //    /// </summary>
    //    [DataMember]
    //    public double LonStartMin { get; set; }
    //    /// <summary>
    //    /// Центр, выпускающий данные в сетке, инициатор сетки (по БД ГИС Метео).
    //    /// </summary>
    //    [DataMember]
    //    public int? CenterId { get; set; }
    //    /// <summary>
    //    /// Регион сетки (по БД ГИС Метео).
    //    /// </summary>
    //    [DataMember]
    //    public char? Region { get; set; }
    //    /// <summary>
    //    /// Модель, формирующая сетку (по БД ГИС Метео).
    //    /// </summary>
    //    [DataMember]
    //    public char? Model { get; set; }
    //    /// <summary>
    //    /// Описание сетки.
    //    /// </summary>
    //    [DataMember]
    //    public string Description { get; set; }

    //    #region GAUSS_LAT_94
    //    /// <summary>
    //    /// Массив широт Гауссовой сетки проекта анализа/реанализа NCEP/NCAR.
    //    /// </summary>
    //    private double[] GaussLat94 = new double[]{
    //        88.542f, 86.653f, 84.753f, 82.851f, 80.947f, 79.043f, 77.139f, 75.235f, 73.331f, 71.426f, 
    //        69.522f, 67.617f, 65.713f, 63.808f, 61.903f, 59.999f, 58.094f, 56.189f, 54.285f,  52.38f, 
    //        50.475f, 48.571f, 46.666f, 44.761f, 42.856f, 40.952f, 39.047f, 37.142f, 35.238f, 33.333f, 
    //        31.428f, 29.523f, 27.619f, 25.714f, 23.809f, 21.904f, 20f, 18.095f,  16.19f, 14.286f, 
    //        12.381f, 10.476f,  8.571f,  6.667f, 4.762f, 2.857f, 0.952f, -0.952f, -2.857f, -4.762f, 
    //        -6.667f, -8.571f, -10.476f, -12.381f, -14.286f, -16.19f, -18.095f, -20f, -21.904f, -23.809f, 
    //        -25.714f, -27.619f, -29.523f, -31.428f, -33.333f, -35.238f, -37.142f, -39.047f, -40.952f, -42.856f, 
    //        -44.761f, -46.666f, -48.571f, -50.475f, -52.38f, -54.285f, -56.189f, -58.094f, -59.999f, -61.903f, 
    //        -63.808f, -65.713f, -67.617f, -69.522f, -71.426f, -73.331f, -75.235f, -77.139f, -79.043f, -80.947f, 
    //        -82.851f, -84.753f, -86.653f, -88.542f
    //    };
    //    #endregion

    //    /// <summary>
    //    /// Список широт гауссовой  сетки с севера на юг!!!
    //    /// </summary>
    //    public double[] GaussLat { get; set; }

    //    public Grid getGrid(Grib.Grib1 grib1)
    //    {
    //        return new Grid(
    //            -1,
    //            grib1.dataRepresentationType,
    //            grib1.center, '\0', '\0',
    //            grib1.points_x, grib1.points_y,
    //            Meta.GeoPoint.Grd2Min(grib1.lon_step) * ((grib1.isI2East) ? 1 : -1),
    //            (grib1.dataRepresentationType == 4) ? grib1.lat_step : Meta.GeoPoint.Grd2Min(grib1.lat_step) * ((grib1.isJ2North) ? 1 : -1),
    //            Meta.GeoPoint.Grd2Min(grib1.la1 / 1000d),
    //            Meta.GeoPoint.Grd2Min(grib1.lo1 / 1000d),
    //            "From Grib1"
    //        );
    //    }

    //    /// <summary>
    //    /// Инициализация экземпляра класса.
    //    /// </summary>
    //    public Grid(Grid grid)
    //    {
    //        Initialize(
    //            grid.Id,
    //            //grid.geoRegId,
    //            grid.GridTypeId,
    //            grid.CenterId,
    //            grid.Region,
    //            grid.Model,
    //            grid.PointsX,
    //            grid.PointsY,
    //            grid.StepXMin,
    //            grid.StepYMin,
    //            grid.LatStartMin,
    //            grid.LonStartMin,
    //            grid.Description,
    //            grid.GaussLat
    //        );
    //    }
    //    /// <summary>
    //    /// Получить количество узлов сетки заданной в формальных параметрах метода.
    //    /// </summary>
    //    /// <param name="side1Min">Одна широта (догота) сетки.</param>
    //    /// <param name="side2Min">Другая широта (догота) сетки.</param>
    //    /// <param name="stepMin">Шаг между заданными широтами (долготами) сетки.</param>
    //    /// <returns></returns>
    //    private static int PointsQCalc(double side1Min, double side2Min, double stepMin)
    //    {
    //        double size = Math.Abs(side1Min - side2Min);
    //        stepMin = (stepMin < 0) ? -stepMin : stepMin;
    //        if (Math.IEEERemainder(size, stepMin) != 0)
    //            return 0;
    //        return (int)(size / stepMin + 1);
    //    }
    //    /// <summary>
    //    /// Инициализация экземпляра класса.
    //    /// </summary>
    //    public Grid(GeoRectangle reg, int? geoRegId, Projection prj, int centerId, char region, char model, double latStepMin, double lonStepMin)
    //    {
    //        int PointsX = PointsQCalc(Meta.GeoPoint.Grd2Min(reg.East), Meta.GeoPoint.Grd2Min(reg.West), lonStepMin);
    //        int PointsY = PointsQCalc(Meta.GeoPoint.Grd2Min(reg.North), Meta.GeoPoint.Grd2Min(reg.South), latStepMin);
    //        double LatStartMin = (latStepMin < 0) ? Meta.GeoPoint.Grd2Min(reg.North) : Meta.GeoPoint.Grd2Min(reg.South);
    //        double LonStartMin = Meta.GeoPoint.Grd2Min(reg.West);
    //        if (prj != Projection.LATLON ||
    //            (
    //                Meta.GeoPoint.Min2Grd(LatStartMin + latStepMin * (PointsY - 1)) != reg.South ||
    //                Meta.GeoPoint.Min2Grd(LonStartMin + lonStepMin * (PointsX - 1)) != reg.East ||
    //                reg.West > reg.East ||
    //                lonStepMin <= 0
    //            )
    //        ) throw new Exception("Incorrect region or step: " + reg.ToString() + ";" + latStepMin + ";" + lonStepMin);

    //        Initialize(-1, (int)prj, centerId, region, model, PointsX, PointsY, lonStepMin, latStepMin, LatStartMin, LonStartMin, reg.name, null);
    //        if (this.GeoRectangle != reg)
    //            throw new Exception("Algorithmic error... Call Viator.");
    //        LatLonMin = ToArrayMin();
    //    }
    //    /// <summary>
    //    /// Получить географический регион из границ сетки.
    //    /// </summary>
    //    public GeoRectangle GeoRectangle
    //    {
    //        get
    //        {
    //            return new GeoRectangle(Meta.GeoPoint.Min2Grd(SouthBoundMin), Meta.GeoPoint.Min2Grd(NorthBoundMin), Meta.GeoPoint.Min2Grd(WestBoundMin), Meta.GeoPoint.Min2Grd(EastBoundMin), "Obtained from Grid");
    //        }
    //    }
    //    /// <summary>
    //    /// Можно использовать и как проверку на совместимость регионов
    //    /// </summary>
    //    /// <param name="geoReg"></param>
    //    /// <returns>Truncated Grid</returns>
    //    public Grid Truncate(GeoRectangle geoReg)
    //    {
    //        if (geoReg.isIntersectGreenwich)
    //            throw new Exception("geoReg.isIntersectGreenwich()");
    //        if (GridTypeId != (int)Projection.LATLON)
    //            throw new Exception("(GridTypeId != Projection.LATLON)");

    //        double[][] latlon = ToArrayGrad();
    //        double[] lat = latlon[0];
    //        double[] lon = latlon[1];

    //        int pointY1 = -1, pointY2 = -1;
    //        for (int i = 0; i < lat.Length; i++)
    //        {
    //            if (StepYMin < 0)
    //            {
    //                if (pointY1 < 0)
    //                    if (lat[i] <= geoReg.North)
    //                        pointY1 = i;
    //                if (pointY2 < 0)
    //                    if (lat[i] == geoReg.South) pointY2 = i;
    //                    else if (lat[i] < geoReg.South) pointY2 = i - 1;
    //            }
    //            else
    //            {
    //                if (pointY1 < 0)
    //                    if (lat[i] >= geoReg.South)
    //                        pointY1 = i;
    //                if (pointY2 < 0)
    //                    if (lat[i] == geoReg.North) pointY2 = i;
    //                    else if (lat[i] > geoReg.North) pointY2 = i - 1;
    //            }
    //        }
    //        int pointX1 = -1, pointX2 = -1;
    //        for (int i = 0; i < lon.Length; i++)
    //        {
    //            if (StepXMin < 0)
    //            {
    //                if (pointX1 < 0)
    //                    if (lon[i] <= geoReg.East)
    //                        pointX1 = i;
    //                if (pointX2 < 0)
    //                    if (lon[i] == geoReg.West) pointX2 = i;
    //                    else if (lon[i] < geoReg.West) pointX2 = i - 1;
    //            }
    //            else
    //            {
    //                if (pointX1 < 0)
    //                    if (lon[i] >= geoReg.West)
    //                        pointX1 = i;
    //                if (pointX2 < 0)
    //                    if (lon[i] == geoReg.East) pointX2 = i;
    //                    else if (lon[i] > geoReg.East) pointX2 = i - 1;
    //            }
    //        }
    //        if (pointX1 < 0 || pointX2 < 0 || pointY1 < 0 || pointY2 < 0)
    //            return null;
    //        if (pointX1 > pointX2 || pointY1 > pointY2)
    //            throw new Exception("(pointX1 > pointX2 || pointY1 >pointY2 )");

    //        return new Grid(
    //                -1,
    //                this.GridTypeId,
    //                CenterId,
    //                Region,
    //                Model,
    //                (int)(pointX2 - pointX1 + 1),
    //                (int)(pointY2 - pointY1 + 1),
    //                StepXMin,
    //                StepYMin,
    //                LatStartMin + pointY1 * StepYMin,
    //                LonStartMin + pointX1 * StepXMin,
    //                "Truncated from grid [" + ToString() + "]");
    //    }
    //    /// <summary>
    //    /// Инициализация экземпляра класса.
    //    /// </summary>
    //    public Grid(
    //        int id,
    //        //int? geoRegId,
    //        int GridTypeId,
    //        int? centerId,
    //        char? Region,
    //        char? Model,
    //        int PointsX,
    //        int PointsY,
    //        double StepXMin,
    //        double StepYMin,
    //        double LatStartMin,
    //        double LonStartMin,
    //        string Description
    //    )
    //    {
    //        Initialize(
    //            id,
    //            //geoRegId,
    //            GridTypeId,
    //            centerId,
    //            Region,
    //            Model,
    //            PointsX,
    //            PointsY,
    //            StepXMin, StepYMin, LatStartMin, LonStartMin,
    //            Description,
    //            null
    //        );
    //    }
    //    private void Initialize(
    //        int id,

    //        int GridTypeId,
    //        int? centerId,	// Hmdic.Centers
    //        char? Region,
    //        char? Model,
    //        int PointsX,
    //        int PointsY,
    //        double stepX,		// Minute 
    //        double stepY,		// Minute
    //        double latStart,	// Minute
    //        double lonStart,	// Minute
    //        string Description,
    //        double[] gaussLat
    //    )
    //    {
    //        this.Id = id;
    //        //this.geoRegId = geoRegId;
    //        this.GridTypeId = GridTypeId;
    //        this.CenterId = centerId;
    //        this.Region = Region;
    //        this.Model = Model;
    //        this.PointsX = PointsX;
    //        this.PointsY = PointsY;
    //        this.StepXMin = stepX;
    //        this.StepYMin = stepY;
    //        this.LatStartMin = latStart;
    //        this.LonStartMin = lonStart;
    //        this.Description = Description;

    //        switch ((Projection)GridTypeId)
    //        {
    //            case Projection.LATLON:
    //                if (WestBoundMin > Meta.GeoPoint.GEO_MAXLONGITUDE_GRD_GRD * 60 || WestBoundMin < Meta.GeoPoint.GEO_MINLONGITUDE_GRD_GRD * 60
    //                    || EastBoundMin > Meta.GeoPoint.GEO_MAXLONGITUDE_GRD_GRD * 60 || EastBoundMin < Meta.GeoPoint.GEO_MINLONGITUDE_GRD_GRD * 60
    //                    || NorthBoundMin > Meta.GeoPoint.GEO_MAXLATITUDE_GRD_GRD * 60 || NorthBoundMin < Meta.GeoPoint.GEO_MINLATITUDE_GRD_GRD * 60
    //                    || SouthBoundMin > Meta.GeoPoint.GEO_MAXLATITUDE_GRD_GRD * 60 || SouthBoundMin < Meta.GeoPoint.GEO_MINLATITUDE_GRD_GRD * 60
    //                ) throw new Exception("LATLON grid bounds is wrong : [" + ToString() + "]");

    //                if ((Math.IEEERemainder(Math.Abs(NorthBoundMin - SouthBoundMin), StepYMin) != 0 && StepYMin != 0)
    //                    || (Math.IEEERemainder(Math.Abs(EastBoundMin - WestBoundMin), StepXMin) != 0 && StepXMin != 0)
    //                ) throw new Exception("LATLON grid step is wrong : [" + ToString() + "]");

    //                break;
    //            case Projection.GAUSS:
    //                if (gaussLat != null)
    //                {
    //                    GaussLat = gaussLat;
    //                }
    //                else
    //                {
    //                    if (PointsY == 94 && stepY == 47) { GaussLat = GaussLat94; }
    //                }
    //                if (stepY != 47 || PointsY != 94)
    //                    throw new Exception("GAUSS grid param is wrong : " + stepY + ";" + PointsY);

    //                if (Math.IEEERemainder(Math.Abs(EastBoundMin - WestBoundMin) / 60.0, (float)LonStepGrad) != 0)
    //                    throw new Exception("LATLON grid step is wrong : [" + ToString() + "]");
    //                break;
    //        }
    //        LatLonMin = ToArrayMin();
    //    }

    //    /// <summary>
    //    /// Получить сетку, включающую в себя все заданные по широте. По долготе от 0 до (360 * 60 - stepX) 
    //    /// Учитывать совместимость сеток по шагу.
    //    /// </summary>
    //    static public Grid getGridMaxOf(Grid[] grids)
    //    {
    //        int i;
    //        double stepX = grids[0].StepXMin, stepY = grids[0].StepYMin,
    //            northBound = grids[0].NorthBoundMin,
    //            southBound = grids[0].SouthBoundMin,
    //            westBound = 0, eastBound = 360 * 60 - stepX;
    //        int GridTypeId = grids[0].GridTypeId;
    //        char model = (char)grids[0].Model;

    //        for (i = 0; i < grids.Length; i++)
    //        {
    //            // TEST
    //            if (GridTypeId != grids[i].GridTypeId ||
    //                model != grids[i].Model ||
    //                stepX != grids[i].StepXMin || stepY != grids[i].StepYMin
    //            )
    //                return null;

    //            // Get boundaries
    //            double n = grids[i].NorthBoundMin; if (n > northBound) northBound = n;
    //            double s = grids[i].SouthBoundMin; if (s < southBound) southBound = s;
    //        }
    //        Grid grid = new Grid
    //        (
    //            -1,
    //            //null, 
    //            GridTypeId, grids[0].CenterId, null, model,
    //            PointsQCalc(eastBound, westBound, stepX),
    //            PointsQCalc(northBound, southBound, stepY),
    //            stepX, stepY,
    //            (stepY > 0) ? southBound : northBound, (stepX > 0) ? westBound : eastBound,
    //            "Grid.getGridMaxOf()"
    //        );
    //        // Алгоритм не должен позволять строить такое поле? Или как...
    //        if (grid.EastBoundMin < grid.WestBoundMin)
    //            throw new Exception("gridMaxOf.getEastBound < gridMaxOf.getWestBound");

    //        return grid;
    //    }
    //    /// <summary>
    //    /// Возвращает строку, представляющую текущий экземпляр класса.
    //    /// </summary>
    //    /// <returns>Строка, представляющая текущий экземпляр класса.</returns>
    //    public override string ToString()
    //    {
    //        return
    //            SouthBoundMin / 60 + " : " + NorthBoundMin / 60
    //            + "; " +
    //            WestBoundMin / 60 + " : " + EastBoundMin / 60
    //            + "; "
    //            + (((Projection)this.GridTypeId == Projection.GAUSS) ? StepYMin : StepYMin / 60) + " : " + StepXMin / 60
    //            + "; " +
    //            PointsY + "*" + PointsX + "=" + PointsY * PointsX
    //        ;
    //    }
    //    /// <summary>
    //    /// Получить индекс узла сетки по заданной широте и долготе узла.
    //    /// </summary>
    //    /// <returns>Индекс узла или -1, если узел не принадлежит сетке.</returns>
    //    public int PointIdx(double latMin, double lonMin)
    //    {
    //        int latIdx = LatIdx(latMin);
    //        int lonIdx = LonIdx(lonMin);
    //        return PointIdxByLatLonIdx(latIdx, lonIdx);
    //    }
    //    /// <summary>
    //    /// Получить индекс узла сетки по заданным индексам широты и долготы узла.
    //    /// </summary>
    //    /// <returns>Индекс узла или -1, если узел не принадлежит сетке.</returns>
    //    public int PointIdxByLatLonIdx(int latIdx, int lonIdx)
    //    {
    //        if (latIdx >= 0 && lonIdx >= 0 && latIdx <= PointsY && lonIdx <= PointsX)
    //        {
    //            return latIdx * PointsX + lonIdx;
    //        }
    //        return -1;
    //    }
    //    /// <summary>
    //    /// Пересекает ли сетка меридиан Гринвича?
    //    /// </summary>
    //    public bool isIntersectGreenwich
    //    {
    //        get
    //        {
    //            return LonFinish < LonStartMin;
    //        }
    //    }

    //    /// <summary>
    //    /// Получить индекс заданной широты.
    //    /// </summary>
    //    /// <returns>Индекс широты или -1 если сетка не содержит широты.</returns>
    //    public int LatIdx(double latMin)
    //    {
    //        switch ((Projection)GridTypeId)
    //        {
    //            case Projection.LATLON:
    //                double latCur = LatStartMin;
    //                for (int i = 0; i < PointsX; i++, latCur += StepYMin)
    //                {
    //                    if (latCur == latMin)
    //                        return i;
    //                }
    //                break;
    //            case Projection.GAUSS:
    //                if (PointsY == GaussLat.Length)
    //                {
    //                    return System.Array.IndexOf(GaussLat, latMin);
    //                }
    //                break;
    //        }
    //        return -1;
    //    }
    //    /// <summary>
    //    /// Получить индекс заданной долготы в сетке.
    //    /// </summary>
    //    /// <returns>Индекс долготы или -1 если сетка не содержит долготы.</returns>
    //    public int LonIdx(double lonMin)
    //    {
    //        double lonCur = LonStartMin;
    //        for (int i = 0; i < PointsX; i++, lonCur += StepXMin)
    //        {
    //            if (lonCur == lonMin)
    //                return i;
    //        }
    //        return -1;
    //    }


    //    /// <summary>
    //    /// Получить последнюю долготу сетки (мин).
    //    /// </summary>
    //    public double LonFinish
    //    {
    //        get
    //        {
    //            return LonStartMin + (PointsX - 1) * StepXMin;
    //        }
    //    }
    //    /// <summary>
    //    /// Получить последнюю широту сетки (мин).
    //    /// </summary>
    //    public double LatFinish
    //    {
    //        get
    //        {
    //            return LatStartMin + (PointsY - 1) * StepYMin;
    //        }
    //    }
    //    /// <summary>
    //    /// Северная граница сетки (мин).
    //    /// </summary>
    //    public double NorthBoundMin
    //    {
    //        get
    //        {
    //            return ((StepYMin >= 0) ? LatStartMin + (PointsY - 1) * StepYMin : LatStartMin);
    //        }
    //    }
    //    /// <summary>
    //    /// Южная граница сетки (мин).
    //    /// </summary>
    //    public double SouthBoundMin
    //    {
    //        get
    //        {
    //            return ((StepYMin >= 0) ? LatStartMin : LatStartMin + (PointsY - 1) * StepYMin);
    //        }
    //    }
    //    /// <summary>
    //    /// Западная граница сетки (мин).
    //    /// </summary>
    //    public double WestBoundMin
    //    {
    //        get
    //        {
    //            return ((StepXMin >= 0) ? LonStartMin : LonStartMin + (PointsX - 1) * StepXMin);
    //        }
    //    }
    //    /// <summary>
    //    /// Восточная граница сетки (мин).
    //    /// </summary>
    //    public double EastBoundMin
    //    {
    //        get
    //        {
    //            double d = ((StepXMin >= 0) ? LonStartMin + (PointsX - 1) * StepXMin : LonStartMin);
    //            int maxLon = 360 * 60;

    //            while (d > maxLon)
    //            {
    //                d -= maxLon;
    //            }
    //            return d;
    //        }
    //    }
    //    /// <summary>
    //    /// Шаг в градусах между широтами сетки (по оси Y).
    //    /// </summary>
    //    public Nullable<float> LatStepGrad
    //    {
    //        get
    //        {
    //            switch ((Projection)GridTypeId)
    //            {
    //                case Projection.LATLON:
    //                    return (float)(((double)StepYMin) / 60.0);
    //                case Projection.GAUSS:
    //                default:
    //                    return null;
    //            }
    //        }
    //    }
    //    /// <summary>
    //    /// Шаг в градусах между долготами сетки (по оси X).
    //    /// </summary>
    //    public Nullable<float> LonStepGrad
    //    {
    //        get
    //        {
    //            return (float)(StepXMin / 60.0);
    //        }
    //    }
    //    /// <summary>
    //    /// Получить название типа сетки.
    //    /// </summary>
    //    public string ProjectionStr
    //    {
    //        get
    //        {
    //            if (GridTypeId == (int)Projection.LATLON) return "LATLON";
    //            if (GridTypeId == (int)Projection.GAUSS) return "GAUSS";
    //            return "UNKNOWN PROJECTION";
    //        }
    //    }
    //    /// <summary>
    //    /// Получить количество узлов сетки.
    //    /// </summary>
    //    public int PointsQ
    //    {
    //        get
    //        {
    //            return PointsX * PointsY;
    //        }
    //    }
    //    /// <summary>
    //    /// Получить индексы (или null в элементах массива) узлов сетки для границ входного региона.
    //    /// </summary>
    //    /// <param name="reg">Регион</param>
    //    /// <returns>0 - south, 1 - north, 2 - west, 3 - east indexes or element is null</returns>
    //    public int?[] GridLatLonIdxs(GeoRectangle reg)
    //    {
    //        int?[] idx = new int?[4]; // 0 - south, 1 - north, 2 - west, 3 - east indexes
    //        GridScaner scan = new GridScaner(this);
    //        do
    //        {
    //            if (scan.CurLatGrd == reg.South)
    //                idx[0] = scan.CurLatIdx;
    //            if (scan.CurLatGrd == reg.North)
    //                idx[1] = scan.CurLatIdx;
    //            if (scan.CurLonGrd == reg.West)
    //                idx[2] = scan.CurLonIdx;
    //            if (scan.CurLonGrd == reg.East)
    //                idx[3] = scan.CurLonIdx;
    //        } while (scan.MoveNext());
    //        return idx;
    //    }
    //    /// <summary>
    //    /// Определить принадлежит ли точка региону сетки.
    //    /// </summary>
    //    /// <param name="geoPoint">Точка.</param>
    //    /// <returns>true, если принадлежит.</returns>
    //    public bool isPointBelongGridRectangle(GeoPoint geoPoint)
    //    {
    //        return GeoRectangle.IsPointBelong(geoPoint);
    //    }
    //    /// <summary>
    //    /// Получить ближайщий к точке узел сетки.
    //    /// </summary>
    //    /// <param name="geoPoint">Точка.</param>
    //    /// <returns>Ближайший узел сетки или null, если точка не лежит в пределах региона сетки.</returns>
    //    public GeoPoint getNearestGridPoint(GeoPoint geoPoint)
    //    {
    //        if (!isPointBelongGridRectangle(geoPoint))
    //            return null;
    //        GeoPoint ret = new GeoPoint(Meta.GeoPoint.Min2Grd(LatStartMin), Meta.GeoPoint.Min2Grd(LonStartMin));
    //        double[] dxdy = new double[] { double.MaxValue, double.MaxValue };

    //        GridScaner scan = new GridScaner(this);
    //        do
    //        {
    //            double d = Math.Abs(scan.CurLatMin - geoPoint.LatMin);
    //            if (d < dxdy[0])
    //            {
    //                ret.LatMin = scan.CurLatMin;
    //                dxdy[0] = d;
    //            }
    //            d = Math.Abs(scan.CurLonMin - geoPoint.LonMin);
    //            if (d < dxdy[1])
    //            {
    //                ret.LonMin = scan.CurLonMin;
    //                dxdy[1] = d;
    //            }

    //        } while (scan.MoveNext());
    //        return ret;
    //    }
    //    /// <summary>
    //    /// Отбор узла сетки, ближайшего к заданному с условием определения близости.
    //    /// </summary>
    //    /// <param name="geoPoint">Точка, к которой ищется ближайший узел сетки.</param>
    //    /// <param name="nearestType">Условие определения близости узла к точке.</param>
    //    /// <returns>[/*iLat, iLon, iPoint*/]</returns>
    //    public int[/*iLat, iLon, iPoint*/] GetNearestPoint(GeoPoint geoPoint, GeoPoint.NearestType nearestType)
    //    {
    //        List<int[]> points = getNearestGridPoints(geoPoint);

    //        int iPoint = 0;
    //        GeoPoint gridPoint = GetGeoPoint(points[iPoint][0], points[iPoint][1]);
    //        double dist = geoPoint.getDistMin(gridPoint);

    //        for (int i = 0; i < points.Count; i++)
    //        {
    //            GeoPoint gridPoint1 = GetGeoPoint(points[i][0], points[i][1]);
    //            double dist1 = geoPoint.getDistMin(gridPoint1);

    //            switch (nearestType)
    //            {
    //                case Meta.GeoPoint.NearestType.Nearest2East:
    //                    if (gridPoint1.LonGrd > geoPoint.LonGrd)
    //                    {
    //                        if (dist1 < dist)
    //                        {
    //                            iPoint = i;
    //                            dist = dist1;
    //                            gridPoint = gridPoint1;
    //                        }
    //                    }
    //                    break;
    //                default:
    //                    throw new NotImplementedException("switch (pointType) : " + nearestType);
    //            }
    //        }
    //        return points[iPoint];
    //    }
    //    public List<object[/*(double)latMin,(int)latIdx*/]> NearestGridPoints(double latGrad, double lonGrad)
    //    {
    //        List<object[]> lats = NearestGridLatOrLons(LatOrLon.LAT, latGrad * 60);
    //        List<object[]> lons = NearestGridLatOrLons(LatOrLon.LON, lonGrad * 60);
    //        List<object[]> ret = null;
    //        if (lats != null && lons != null)
    //        {
    //            ret = new List<object[]>();
    //            for (int i = 0; i < lats.Count; i++)
    //                for (int j = 0; j < lons.Count; j++)
    //                    ret.Add(new object[] 
    //                    {   new GeoPoint(((double)lats[i][0]) / 60, ((double)lons[j][0]) / 60), 
    //                        (int)lats[i][1], 
    //                        (int)lons[j][1],
    //                        PointIdxByLatLonIdx((int)lats[i][1], (int)lons[j][1]) 
    //                    });
    //        }
    //        return ret;
    //    }
    //    /// <summary>
    //    /// Поиск ближайших широт или долгот сетки.
    //    /// </summary>
    //    /// <param name="latOrlon">Поиск широты || долготы.</param>
    //    /// <param name="latMin">Искомая широта || долгота (min).</param>
    //    /// <returns>List<object[/*(double)latMin,(int)latIdx*/]></returns>
    //    public List<object[/*(double)latMin,(int)latIdx*/]> NearestGridLatOrLons(LatOrLon latOrlon, double minutes)
    //    {

    //        int i, i1, i2;
    //        double[] array = (latOrlon == LatOrLon.LAT) ? LatLonMin[0] : LatLonMin[1];
    //        double step = (latOrlon == LatOrLon.LAT) ? StepYMin : StepXMin;

    //        for (i = 1, i1 = -1, i2 = -1; i < array.Length; i++)
    //        {
    //            if (step <= 0)
    //            {
    //                i1 = i - 1;
    //                i2 = i;
    //            }
    //            else
    //            {
    //                i1 = i;
    //                i2 = i - 1;
    //            }
    //            if (minutes <= array[i1] && minutes >= array[i2])
    //                break;
    //        }

    //        List<object[]> ret = null;
    //        if (i < array.Length)
    //        {
    //            ret = new List<object[]>();
    //            if (minutes == array[i1])
    //            {
    //                ret.Add(new object[] { array[i1], i1 });
    //            }
    //            else if (minutes == array[i2])
    //            {
    //                ret.Add(new object[] { array[i2], i2 });
    //            }
    //            else
    //            {
    //                ret.Add(new object[] { array[i1], i1 });
    //                ret.Add(new object[] { array[i2], i2 });
    //            }
    //        }
    //        return ret;
    //    }
    //    public GeoPoint GetGeoPoint(int iLat, int iLon)
    //    {
    //        return new GeoPoint(GetLatMin(iLat) / 60, GetLonMin(iLon) / 60);
    //    }
    //    public double GetLonMin(int lonIdx)
    //    {
    //        if (lonIdx > PointsX)
    //        {
    //            throw new Exception("lonIdx > PointsX");
    //        }
    //        return LonStartMin + lonIdx * StepXMin;
    //    }
    //    public double GetLatMin(int latIdx)
    //    {
    //        if (latIdx > PointsY || latIdx < 0)
    //        {
    //            throw new Exception("latIdx>PointsY || latIdx<0");
    //        }
    //        switch ((Projection)GridTypeId)
    //        {
    //            case Projection.GAUSS:
    //                return GaussLat[latIdx];
    //            case Projection.LATLON:
    //                return LatStartMin + latIdx * StepYMin;
    //            default:
    //                throw new Exception("Unknow Geo.Projection=" + (Projection)GridTypeId);
    //        }
    //    }

    //    /// <summary>
    //    ////Для Гауссовой сетки, где 94 широты
    //    /// </summary>
    //    /// <param name="latGrd"></param>
    //    /// <returns></returns>
    //    private int[] getNearestLatIdxGauss(double latGrd)
    //    {
    //        if (latGrd >= GaussLat[0] && latGrd <= 90) { return new int[] { 0 }; }
    //        if (latGrd <= GaussLat[GaussLat.Length - 1] && latGrd <= -90) { return new int[] { GaussLat.Length - 1 }; }
    //        for (int i = 0; i < GaussLat.Length - 1; i++)
    //        {
    //            if (GaussLat[i + 1] <= latGrd && GaussLat[i] >= latGrd)
    //            {
    //                if (latGrd == GaussLat[i]) { return new int[] { i }; }
    //                if (latGrd == GaussLat[i + 1]) { return new int[] { i + 1 }; }
    //                return new int[] { i, i + 1 };
    //            }
    //        }
    //        return null;
    //    }

    //    /// <summary>
    //    /// Построить grid по заданной центральной точке и количеству шагов на север/юг и запад/восток.
    //    /// 
    //    /// В общем случае заданная в качестве параметра метода центральная точка не совпадает с узлом сетки, 
    //    /// поэтому по ней сначала определяется ближайшая точка сетки, которая и будет центральной в grib.
    //    /// </summary>
    //    /// <param name="centralPoint">По данной точке выбирается ближайшая точка сетки, которая и будет центральной точкой новой сетки.</param>
    //    /// <param name="pointsQ2NorthSouth">Количество узлов новой сетки на юг и на север от центрального узла.</param>
    //    /// <param name="pointsQ2WestEast">Количество узлов новой сетки на запад и восток от центрального узла.</param>
    //    /// <returns>Новая сетка</returns>
    //    public Grid getGridByCentralPoint(GeoPoint centralPoint, int pointsQ2NorthSouth, int pointsQ2WestEast)
    //    {
    //        GeoPoint cPoint = getNearestGridPoint(centralPoint);
    //        if (cPoint == null) return null;

    //        switch ((Projection)GridTypeId)
    //        {
    //            case Projection.LATLON:
    //                return new Grid(
    //                    -1,
    //                    this.GridTypeId, 0, '0', '0',
    //                    pointsQ2WestEast * 2 + 1, pointsQ2NorthSouth * 2 + 1, this.StepXMin, this.StepYMin,
    //                    cPoint.LatMin + pointsQ2NorthSouth * (-this.StepYMin),
    //                    cPoint.LonMin + pointsQ2WestEast * (-this.StepXMin),
    //                    "Generated by Grid.getGridByCentralPoint");
    //            default:
    //                throw new Exception("switch (GridTypeId): " + GridTypeId);
    //        }
    //    }
    //    /// <summary>
    //    /// Проверка совместимости сеток.
    //    /// </summary>
    //    /// <param name="grid2">Одна из сеток. Вторая - this.</param>
    //    /// <returns>true, если сетки совместимы.</returns>
    //    public bool isCompatible(Grid grid2)
    //    {
    //        return isCompatible(this, grid2);
    //    }
    //    /// <summary>
    //    /// Проверка совместимости сеток.
    //    /// </summary>
    //    /// <param name="grid1">Первая из сеток.</param>
    //    /// /// <param name="grid2">вторая из сеток.</param>
    //    /// <returns>true, если сетки совместимы.</returns>
    //    static public bool isCompatible(Grid grid1, Grid grid2)
    //    {
    //        if (
    //            grid1.GridTypeId != grid2.GridTypeId
    //        || grid1.LatStartMin != grid2.LatStartMin || grid1.LonStartMin != grid2.LonStartMin
    //        || grid1.PointsX != grid2.PointsX || grid1.PointsY != grid2.PointsY
    //        || grid1.StepXMin != grid2.StepXMin || grid1.StepYMin != grid2.StepYMin
    //        )
    //            return false;
    //        return true;
    //    }

    //    /// <summary>
    //    /// Проверка совместимости сеток в заданном регионе.
    //    /// т.е. все точки grid1, принадлежащие geoRec есть в grid2.
    //    /// Если в grid1 нет ни одной точки в регионе geoRec, то возвращается false
    //    /// </summary>
    //    /// <param name="grid1">Первая из сеток.</param>
    //    /// <param name="grid2">вторая из сеток.</param>
    //    /// <param name="geoRec"></param>
    //    /// <returns>true, если сетки совместимы.</returns>
    //    static public bool isCompatible(Grid grid1, Grid grid2, GeoRectangle geoRec)
    //    {
    //        if (isCompatible(grid1, grid2))
    //            return true;
    //        if (grid1.GridTypeId != grid2.GridTypeId || geoRec == (GeoRectangle)null)
    //            return false;

    //        GridScaner gs = new GridScaner(grid1);
    //        int countPoint = 0;
    //        while (!gs.EOF)
    //        {
    //            GeoPoint curGeoPoint = new GeoPoint(gs.CurLatGrd, gs.CurLonGrd);
    //            if (geoRec.IsPointBelong(curGeoPoint.LatGrd, curGeoPoint.LonGrd))
    //            {
    //                countPoint++;
    //                if (!grid2.isPointBelongGridRectangle(curGeoPoint))
    //                    return false;
    //            }
    //            gs.MoveNext();
    //        }
    //        if (countPoint > 0)
    //            return true;
    //        else
    //            return false;
    //    }

    //    /// <summary>
    //    /// Получить индексы узлов сетки, принадлежащие заданному региону.
    //    /// </summary>
    //    /// <param name="gr">Регион, которому должны принадлежать узлы сетки.</param>
    //    /// <returns>Индексы узлов сетки, принадлежащие заданному региону.</returns>
    //    public List<int> PointsIdxInRegion(GeoRectangle gr)
    //    {
    //        List<int> ret = new List<int>();
    //        Grid.GridScaner gs = new Grid.GridScaner(this);
    //        do
    //        {
    //            if (gr.IsPointBelong(gs.CurLatGrd, gs.CurLonGrd))
    //                ret.Add(gs.CurPointIdx);

    //        } while (gs.MoveNext());
    //        return ret;
    //    }
    //    /// <summary>
    //    /// Получить индексы узлов сетки this, для тех узлов входноой сетки grid, которые принадлежат региону geoRec/
    //    /// </summary>
    //    /// <param name="gridIn">Сетка.</param>
    //    /// <param name="geoRecIn">Регион.</param>
    //    /// <returns>Индексы узлов сетки this</returns>
    //    public int[] GetPointIdxInRegion(Grid gridIn, GeoRectangle geoRecIn)
    //    {
    //        List<int> ret = new List<int>();

    //        GridScaner gs = new GridScaner(gridIn);
    //        do
    //        {
    //            if (geoRecIn.IsPointBelong(gs.CurLatGrd, gs.CurLonGrd))
    //            {
    //                //если точка принадлежит заданному региону
    //                //находим индекс для нее в текущем гриде
    //                int i = PointIdx(gs.CurLatMin, gs.CurLonMin);
    //                if (i != -1)
    //                {
    //                    ret.Add(i);
    //                }
    //            }

    //        } while (gs.MoveNext());
    //        if (ret.Count != gridIn.PointsIdxInRegion(geoRecIn).Count())
    //        {
    //            throw new Exception("ERROR in point counting.");
    //        }
    //        return ret.ToArray();
    //    }
    //    /// <summary>
    //    /// Получить индексы узлов сетки this, для узлов входной сетки grid.
    //    /// </summary>
    //    /// <param name="gridIn">Сетка.</param>
    //    /// <returns>Индексы узлов сетки this, соответствующие узлам входной сетки. Если нет соответствия точки сетки, то индекс равен -1.</returns>
    //    public int[] GetPointIdxs(Grid gridIn)
    //    {
    //        List<int> ret = new List<int>();

    //        GridScaner gs = new GridScaner(gridIn);
    //        do
    //        {
    //            ret.Add(PointIdx(gs.CurLatMin, gs.CurLonMin));
    //        } while (gs.MoveNext());
    //        return ret.ToArray();
    //    }
    //    /// <summary>
    //    /// Получить индексы узлов сетки this, соответствующие координатам указанных точек.
    //    /// </summary>
    //    /// <param name="pointCoords">Координаты точек (в минутах!!): [0][0...N] - широта, [1][0...N] - долгота.</param>
    //    /// <returns>Массив индексов узлов сетки, соответствующих координатам указанных точек. Значение -1 означает отсутствие узла с искомой координатой.</returns>
    //    public int[] GetPointIdxs(double[][] pointCoords)
    //    {
    //        int[] ret = Support.Allocate(pointCoords[0].Length, -1);

    //        GridScaner gs = new GridScaner(this);
    //        do
    //        {
    //            for (int i = 0; i < ret.Length; i++)
    //            {
    //                if (gs.CurLatMin == pointCoords[0][i] && gs.CurLonMin == pointCoords[1][i])
    //                    ret[i] = gs.CurPointIdx;
    //            }
    //        } while (gs.MoveNext());
    //        return ret;
    //    }
    //    /// <summary>
    //    /// Получить узел сетки (точку), соответствующую заданным индексам широты и долготы сетки. ВНИМАНИЕ: отсутствует проверка на наличие таких индексов в сетке.
    //    /// </summary>
    //    /// <param name="latIdx">Индекс широты узла сетки.</param>
    //    /// <param name="lonIdx">Индекс долготы узла сетки.</param>
    //    /// <returns>Точка, соотв-ая узлу сетки.</returns>
    //    public GeoPoint GeoPoint(int latIdx, int lonIdx)
    //    {
    //        return new GeoPoint(Meta.GeoPoint.Min2Grd(LatStartMin + latIdx * StepYMin), Meta.GeoPoint.Min2Grd(LonStartMin + lonIdx * StepXMin));
    //    }
    //    /// <summary>
    //    /// Массивы широт и долгот сетки в мин.
    //    /// </summary>
    //    /// <returns>double[0] - lats; double[1] - lons.</returns>
    //    public double[][] ToArrayGrad()
    //    {
    //        double[][] ret = ToArrayMin();
    //        for (int i = 0; i < ret.Length; i++)
    //        {
    //            for (int j = 0; j < ret[i].Length; j++)
    //            {
    //                ret[i][j] /= 60.0;
    //            }
    //        }
    //        return ret;
    //    }
    //    /// <summary>
    //    /// Массивы широт и долгот сетки в градусах.
    //    /// </summary>
    //    /// <returns>double[0] - lats; double[1] - lons.</returns>
    //    public double[][] ToArrayMin()
    //    {
    //        double[][] ret = new double[][] { new double[PointsY], new double[PointsX] };
    //        for (int i = 0; i < PointsX; i++)
    //        {
    //            ret[1][i] = LonStartMin + StepXMin * i;
    //        }
    //        switch ((Projection)GridTypeId)
    //        {
    //            case Projection.GAUSS:
    //                for (int i = 0; i < GaussLat.Length; i++)
    //                {
    //                    ret[0][i] = GaussLat[i] * 60.0;
    //                }
    //                break;
    //            case Projection.LATLON:
    //                for (int i = 0; i < PointsY; i++)
    //                {
    //                    ret[0][i] = LatStartMin + StepYMin * i;
    //                }
    //                break;
    //            default:
    //                throw new Exception("Unknown Geo.Projection =" + (Projection)GridTypeId);
    //        }
    //        return ret;
    //    }

    //    /// <summary>
    //    /// Массив широт и долгот точек поля.
    //    /// </summary>
    //    /// <returns>double[/*iPoint*/][/*Lat;Lon*/]</returns>
    //    public double[/*iPoint*/][/*Lat;Lon*/] GetPointsLatLonMin()
    //    {
    //        double[][] ret = new double[PointsQ][];
    //        int i = 0;
    //        GridScaner gs = new GridScaner(this);
    //        do
    //        {
    //            ret[i++] = new double[] { gs.CurLatMin, gs.CurLonMin };
    //        } while (gs.MoveNext());
    //        return ret;
    //    }

    //    //------------------------------------------------------------------- SCANNER
    //    /// <summary>
    //    /// Организация процесса сканирование узлов заданной сетки.
    //    /// 
    //    /// grid.MoveFirst();
    //    ///	while (!grid.EOF)
    //    ///	{
    //    ///		...
    //    ///		grid.moveNext();
    //    ///	}
    //    /// </summary>
    //    //------------------------------------------------------------------- SCANNER

    //    bool isEOF = true;
    //    /// <summary>
    //    /// Достигнут конец сетки (последняя точка сетки).
    //    /// </summary>
    //    public bool EOF
    //    {
    //        get { return isEOF; }
    //    }
    //    public double CurLatMin;
    //    int curLatIdx;
    //    /// <summary>
    //    /// Индекс текущей широты.
    //    /// </summary>
    //    public int CurLatIdx { get; set; }
    //    /// <summary>
    //    /// Индекс текущей долготы.
    //    /// </summary>
    //    public int CurLonIdx { get; set; }
    //    /// <summary>
    //    /// Широта текущего узла сетки (град).
    //    /// </summary>
    //    public double CurLatGrd
    //    {
    //        get
    //        {
    //            return Meta.GeoPoint.Min2Grd(CurLatMin);
    //        }
    //    }
    //    private const int MAX_LON = (int)Meta.GeoPoint.GEO_MAXLONGITUDE_GRD_GRD * 60;
    //    double _curLonMin = double.NaN;
    //    /// <summary>
    //    /// Долгота текущего узла сетки (мин).
    //    /// </summary>
    //    public double CurLonMin
    //    {
    //        set
    //        {
    //            _curLonMin = value;
    //        }
    //        get
    //        {
    //            if (_curLonMin >= MAX_LON)
    //                return _curLonMin - MAX_LON;
    //            if (_curLonMin < 0)
    //                return MAX_LON - _curLonMin;
    //            return _curLonMin;
    //        }
    //    }
    //    /// <summary>
    //    /// Долгота текущего узла сетки (град).
    //    /// </summary>
    //    public double CurLonGrd
    //    {
    //        get
    //        {
    //            return Meta.GeoPoint.Min2Grd(CurLonMin);
    //        }
    //    }
    //    /// <summary>
    //    /// Индекс текущего узла сетки.
    //    /// </summary>
    //    public int CurPointIdx { get; set; }
    //    /// <summary>
    //    /// Перейти к первому узлу сетки.
    //    /// </summary>
    //    public void MoveFirst()
    //    {
    //        CurLatMin = LatStartMin;
    //        CurLonMin = LonStartMin;
    //        curLatIdx = 0;
    //        CurLonIdx = 0;
    //        CurPointIdx = 0;
    //        isEOF = false;
    //    }
    //    /// <summary>
    //    /// Перейти к следующему узлу сетки.
    //    /// </summary>
    //    /// <returns>true, если переход успешен. false, если EOF (перебраны все узлы сетки).</returns>
    //    public bool MoveNext()
    //    {
    //        if (isEOF) return false;

    //        if (CurPointIdx < PointsQ - 1)
    //        {
    //            CurPointIdx++;
    //            if ((CurPointIdx / PointsX) * PointsX == CurPointIdx)
    //            {
    //                CurLonMin = LonStartMin;
    //                CurLonIdx = 0;

    //                curLatIdx++;
    //                if ((Projection)GridTypeId == Projection.GAUSS)
    //                {
    //                    CurLatMin = 60.0 * GaussLat[curLatIdx];
    //                }
    //                else
    //                {
    //                    CurLatMin += StepYMin;
    //                }
    //            }
    //            else
    //            {
    //                CurLonMin += StepXMin;
    //                CurLonIdx++;
    //            }
    //            return true;
    //        }
    //        else
    //        {
    //            isEOF = true;
    //            return false;
    //        }
    //    }
    //}

}
