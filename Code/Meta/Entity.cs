using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Amur.Meta
{
    public class Entity
    {
        public string NameEng { get; set; }
        public string NameRus { get; set; }

        public Entity(string nameEng, string nameRus)
        {
            this.NameEng = nameEng;
            this.NameRus = nameRus;
        }
    }
}
