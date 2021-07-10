using SmartIdeia.Src.Modules.Logs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartIdeia.Src.Modules.Logs.Dtos
{
    public class LogDTO
    {
        public LogType Type { get; set; }
        public string Entity { get; set; }
        public long EntityId { get; set; }
        public string JsonObject { get; set; }
        public long? UserId { get; set; }
        public string Description { get; set; }
        public long? IdeaId { get; set; }
    }
}
