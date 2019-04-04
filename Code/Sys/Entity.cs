using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Common;

namespace SOV.Amur.Sys
{
    public class Entity 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Entity(int id, string name, string description = null)
        {
            Id = id;
            Name = name;
            Description = description;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
