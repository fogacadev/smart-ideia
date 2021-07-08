using SmartIdeia.Modules.Ideas.Entities;
using SmartIdeia.Src.Modules.Accounts.Entities;
using System;

namespace SmartIdeia.Src.Modules.Activities.Entities
{
    public class Activity
    {
        public long Id { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Type { get; set; }
        public long? UserId { get; set; }
        public User User { get; set; }
        public Idea Idea { get; set; }
        public long IdeaId { get; set; }
    }
}
