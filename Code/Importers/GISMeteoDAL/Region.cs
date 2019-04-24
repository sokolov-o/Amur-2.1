using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.GISMeteo
{
    public class Region
    {
        public int South { get; set; }
        public int West { get; set; }
        public int North { get; set; }
        public int East { get; set; }

        public Region(int south, int west, int north, int east)
        {
            if (south > north || west > east)
                throw new Exception("(south > north || west > east)");
            South = south;
            West = west;
            North = north;
            East = east;
        }
    }
}
