using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Amur.Parser
{
    public class SysObj
    {
        public int Id { get; set; }
        public int SysObjTypeId { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public string Heap { get; set; }
        public string LastStartParam { get; set; }

        public override string ToString()
        {
            return Name + " - " + Id + ", тип " + SysObjTypeId;
        }
    }
}
