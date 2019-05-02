using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Common;

namespace SOV.Amur.Meta
{
    public class Instrument : IdNameRE, IParent
    {
        public Instrument(IdNameRE idNames):base(idNames)
        {
        }

        public string SerialNum { get; set; }
        public int? ParentId { get; set; }

        public int GetId()
        {
            return Id;
        }

        public string GetName()
        {
            return NameRus;
        }

        public int? GetParentId()
        {
            return ParentId;
        }
    }
}
