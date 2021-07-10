using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartIdeia.Src.Modules.Activities.Dtos
{
    public enum ActivityType
    {
        Message,
        Activity,
    }

    public class ActivityDTO
    {
        public long Id { get; set; }
        public string Message { get; set; }
        public ActivityType Type { get; set; }
        public long? UserId { get; set; }
        public long IdeaId { get; set; }
    }
}
