using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Amur.Meta
{
    public class PileSet
    {
        public int Id { get; set; }
        public string NameRus { get; set; }
        public string NameEng { get; set; }

        public List<Pile> Piles { get; set; }

        public PileSet()
        {
            Piles = new List<Pile>();
        }
        public class Pile
        {
            public int Id { get; set; }
            public int PileSetId { get; set; }
            public string NameRus { get; set; }
            public string NameEng { get; set; }
            public double? Value1 { get; set; }
            public double? Value2 { get; set; }
            public int OrderBy { get; set; }
        }
    }
}
