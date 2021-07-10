using SmartIdeia.Src.Modules.Accounts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartIdeia.Src.Modules.Logs.Entities
{
    public enum LogType
    {
        Create,
        Update,
        Delete,
        Info,
    }

    public class Log
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public string Entity { get; set; }
        public long EntityId { get; set; }
        public string JsonObject { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public User User { get; set; }
        public long? UserId { get; set; }
        public long? IdeaId { get; set; }
    }
}
