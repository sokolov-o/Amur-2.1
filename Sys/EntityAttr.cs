using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Amur.Sys
{
    public class EntityAttr
    {
        public int Id { get; set; }
        public int EntityId { get; set; }
        public string Name { get; set; }

        public EntityAttr(int id, int entityId, string name)
        {
            Id = id;
            EntityId = entityId;
            Name = name;
        }
    }
}
