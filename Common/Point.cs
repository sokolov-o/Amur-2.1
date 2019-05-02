using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Common
{
    public struct Point
    {
        public double x, y;
        public Point(double x = double.NaN, double y = double.NaN)
        {
            this.x = x;
            this.y = y;
        }
    }
}
    