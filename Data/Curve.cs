using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Amur.Data
{
    [DataContract]
    public class Curve
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int CatalogIdX { get; set; }
        [DataMember]
        public int CatalogIdY { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public List<Seria> Series { get; set; }

        [DataContract]
        public class Seria
        {
            [DataMember]
            public int Id { get; set; }
            [DataMember]
            public int CurveId { get; set; }
            [DataMember]
            public int CurveSeriaTypeId { get; set; }
            [DataMember]
            public DateTime DateS { get; set; }
            [DataMember]
            public List<Coef> Coefs { get; set; }
            [DataMember]
            public List<Point> Points { get; set; }
            [DataMember]
            public string Description { get; set; }

            [DataContract]
            public class Coef
            {
                [DataMember]
                public int Day { get; set; }
                [DataMember]
                public int Month { get; set; }
                [DataMember]
                public double Value { get; set; }

                static public double[] ToDouble(List<Coef> coefs)
                {
                    if (coefs == null || coefs.Count == 0)
                        return null;

                    List<double> ret = new List<double>();
                    foreach (var item in coefs)
                    {
                        ret.AddRange(new double[] { item.Day, item.Month, item.Value });
                    }
                    return ret.ToArray();
                }
                static public List<Coef> FromDouble(double[] dcoefs)
                {
                    if (dcoefs == null)
                        return null;

                    List<Coef> ret = new List<Coef>();
                    for (int i = 0; i < dcoefs.Length; i += 3)
                    {
                        ret.Add(new Coef() { Day = (int)dcoefs[i], Month = (int)dcoefs[i + 1], Value = dcoefs[i + 2] });
                    }
                    return ret;
                }
                static public Curve.Seria GetSeria4Date(List<Curve.Seria> series, DateTime date)
                {
                    if (series != null && series.Count > 0)
                    {
                        if (series.Exists(x => x.CurveId != series[0].CurveId))
                            throw new Exception("Серии принадлежат не одному пункту.");

                        IEnumerable<Seria> ser = series.Where(y => y.DateS <= date);
                        if (ser.Count() > 0)
                            return series.Find(x => x.DateS == ser.Max(y => y.DateS));
                    }
                    return null;
                }
            }

            [DataContract]
            public class Point
            {
                public static string XYFormat = "F1";

                [DataMember]
                public double X { get; set; }
                [DataMember]
                public double Y { get; set; }
                [DataMember]
                public string Name { get { return ToString(); } }

                public override string ToString()
                {
                    return string.Format("{0:" + XYFormat + "} x {1:" + XYFormat + "}", X, Y);
                }
                /// <summary>
                /// 
                /// Установить новый формат значений 
                /// и переформатировать в соответствие с ним значения координат точек (x,y) набора (округлить значения).
                /// 
                /// </summary>
                /// <param name="xyFormat">Устанавливаемый формат значений Point.</param>
                /// <param name="points">Список точек для переформатирования их значений x,y (округления).</param>
                static public void AcceptXYFormat(string xyFormat, List<Point> points)
                {
                    XYFormat = xyFormat;

                    foreach (var point in points)
                    {
                        point.X = double.Parse(string.Format("{0:" + XYFormat + "}", point.X));
                        point.Y = double.Parse(string.Format("{0:" + XYFormat + "}", point.Y));
                    }
                }
            }
        }
    }
}
