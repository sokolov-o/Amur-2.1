using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Common;

namespace SOV.Amur.Sys
{
    public class Log
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }

        public Entity Entity { get; set; }
        public DateTime Date { get; set; }
        public bool IsUrgent { get; set; }
        public string Message { get; set; }

        public Log(int id, Entity entity, DateTime date, bool isUrgent, string message, int? parentId = null)
        {
            Initialize(id, entity, date, isUrgent, parentId, message);
        }
        public Log(Entity entity, bool isUrgent, string message, int? parentId = null)
        {
            Initialize(-1, entity, DateTime.Now, isUrgent, parentId, message);
        }
        void Initialize(int id, Entity entity, DateTime date, bool isUrgent, int? parentId, string message)
        {
            Id = id;
            Entity = entity;
            Date = date;
            IsUrgent = isUrgent;
            Message = message;
            ParentId = parentId;
        }
    }
}
