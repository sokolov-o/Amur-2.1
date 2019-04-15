using GeographicLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            double[] lat = new double[4];
            double[] lon = new double[4];
            DateTime[] trackDates = new DateTime[4];
            lat[0] = 43.116666667; lon[0] = 131.9;          //Владивосток
            lat[1] = 48.483333333; lon[1] = 135.0666666;    //Хабаровск
            lat[2] = 55.016666666; lon[2] = 82.91666666;    //Новосибирск
            lat[3] = 55.755833333; lon[3] = 37.61777777;    //Москва



            trackDates[0] = new DateTime(2016, 2, 4, 0, 0, 0);
            trackDates[1] = new DateTime(2016, 2, 5, 0, 0, 0);
            trackDates[2] = new DateTime(2016, 2, 9, 0, 0, 0);
            trackDates[3] = new DateTime(2016, 2, 11, 0, 0, 0);

            DateTime initDate = trackDates[0];
            initDate = initDate.AddHours(-2);

            Geodesic geod = Geodesic.WGS84;
            GeodesicLine line = geod.InverseLine(lat[0], lon[0], lat[1], lon[1],
                                               GeodesicMask.DISTANCE_IN |
                                               GeodesicMask.LATITUDE |
                                               GeodesicMask.LONGITUDE);

            double az1 = line.Azimuth();
            double[] lags = new double[10];
            for (int i = 0; i < lags.Length; i++) lags[i] = i;
            double speed = 22.23;

            double[,] res1 = GetLatLon4Lags(initDate, lags, lat[0], lon[0], speed, az1);
            Console.WriteLine("Test 0 ______________________________________________________");
            Console.WriteLine("# lat    lon");
            for (int i = 0; i < res1.GetLength(0); i++)
            {
                Console.WriteLine(String.Format("{0} {1:0.0000} {2:0.0000}", i, res1[i, 0], res1[i, 1]));
            }

            //Вторая геодезическая задача тест 1
            lags = new double[30];
            for (int i = 0; i < lags.Length; i++) lags[i] = i * 8;
            //lags[0]=3;
            double[,] res2 = GetLatLon4Lags(initDate, lags, trackDates, lat, lon);
            Console.WriteLine("Test 1 ______________________________________________________");
            Console.WriteLine("# lat    lon");
            for (int i = 0; i < res2.GetLength(0); i++)
            {
                Console.WriteLine(String.Format("{0} {1:0.0000} {2:0.0000}", i, res2[i, 0], res2[i, 1]));
            }

            Console.Read();
        }

      
        /// <summary>
       /// Получить последовательные прогнозопункты от исходной даты в заданные параметром lags моменты времени
       /// для начальной точки, направления движения и скорости.
       /// </summary>
       /// <param name="dateIni">Исходная дата прогноза.</param>
       /// <param name="lags">Массив смещений от dateIni (час) - моменты времени, в которые нужны lat,lon.</param>
       /// <param name="lat0">Широта точки на исх. дату прогноза</param>
       /// <param name="lon0">Долгота точки на исх. дату прогноза</param>
       /// <param name="azimuth">Азимут перемещения объекта (куда).</param>
       /// <param name="speed">Скорость объекта.</param>
       /// <returns></returns>
       static public double[/*индекс точки*/,/*lat,lon*/] GetLatLon4Lags(DateTime dateIni, double[] lags, double lat0, double lon0, double speed, double azimuth)
       {
         double[,] result=new double[lags.Length,2];
        Geodesic geod = Geodesic.WGS84;
        for (int i=0; i<lags.Length; i++)
        {
            double distanceInHour = lags[i]*speed*1000.0*3.6;
            GeodesicLine line = geod.Line(lat0, lon0, azimuth);
            GeodesicData g = line.Position(distanceInHour, GeodesicMask.LATITUDE |
                                                           GeodesicMask.LONGITUDE);
            result[i,0]=g.lat2;
            result[i,1]=g.lon2;
        }
        return result;
       }

       /// <summary>
       /// Получить последовательные прогнозопункты от исходной даты в заданные параметром lags моменты времени
       /// для трека, заданного датами и координатами точек (ломанная).
       /// </summary>
       /// <param name="dateIni">Исх. дата прогноза.</param>
       /// <param name="lags">Массив смещений от dateIni (час) - моменты времени, в которые нужны lat,lon.</param>
       /// <param name="trackDates">Даты точек трека.</param>
       /// <param name="trackLats">Широты точек трека.</param>
       /// <param name="trackLons">Долготы точек трека.</param>
       /// <returns></returns>
       static public double[/*индекс точки*/,/*lat,lon*/] GetLatLon4Lags(DateTime dateIni, double[] lags,
           DateTime[/*заданные точки трека*/] trackDates, double[/*заданные точки трека*/] trackLats, double[/*заданные точки трека*/] trackLons)
       {
           double[,] result = new double[lags.Length, 2];
           for (int i = 0; i < lags.Length; i++)
           {
               DateTime distDate = dateIni.AddHours(lags[i]);
               //distDate = 
               int kl = 0;
               while (!(distDate > trackDates[kl] && distDate <= trackDates[kl + 1]))
               {
                   kl++;
                   if (kl + 1 >= trackDates.Length)
                   {
                       Console.WriteLine("Point " + i + " are out of trackDates");
                       result[i, 0] = double.NaN;
                       result[i, 1] = double.NaN;
                       break;
                   }

               }
               if (kl + 1 >= trackDates.Length)
               {
                   continue;
               }
               Geodesic geod = Geodesic.WGS84;
               GeodesicLine line = geod.InverseLine(trackLats[kl], trackLons[kl], trackLats[kl + 1], trackLons[kl + 1],
                                               GeodesicMask.DISTANCE_IN |
                                               GeodesicMask.LATITUDE |
                                               GeodesicMask.LONGITUDE);
               double dist = line.Distance();
               double a = (distDate - trackDates[kl]).TotalSeconds;
               double b = (trackDates[kl + 1] - trackDates[kl]).TotalSeconds;
               double part = a / b;
               double ds = part * dist;
               GeodesicData g = line.Position(ds,
                                             GeodesicMask.LATITUDE |
                                             GeodesicMask.LONGITUDE);
               result[i, 0] = g.lat2;
               result[i, 1] = g.lon2;
           }
           return result;
       }

        
    }
}
