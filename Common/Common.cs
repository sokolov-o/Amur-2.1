using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Common
{
    public class Miscel
    {
        public const string TIMESTAMP_FORMAT = "yyyy-MM-dd HH:mm:ss";
        static public int Coalesce(int? i, int iDefault)
        {
            return (i.HasValue) ? (int)i : iDefault;
        }
        static public int Coalesce(string s, int iDefault)
        {
            return string.IsNullOrEmpty(s) ? iDefault : int.Parse(s);
        }
    }

    //////public class MathSupport
    //////{
    //////    public static double InterpolateLine(double x1, double x2, double y1, double y2, double x)
    //////    {
    //////        if (x < x1 || x > x2)
    //////            throw new Exception("(x < x1 || x > x2) : " + x + "/" + x1 + "/" + x2);
    //////        return ((x - x1) / (x2 - x1)) * (y2 - y1) + y1;
    //////    }
    //////}
}
