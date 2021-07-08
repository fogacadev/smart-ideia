using SmartIdeia.Modules.Ideas.Entities;
using SmartIdeia.Src.Modules.Accounts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartIdeia.Src.Modules.ActionPlans.Entities
{
    public class ActionPlan
    {
        public long Id { get; set; }
        public string How { get; set; }
        public string Why { get; set; }
        public string Where { get; set; }
        public string What { get; set; }
        public Idea Idea { get; set; }
        public long IdeaId { get; set; }
        public bool IsFinished { get; set; }
        public User User { get; set; }
        public long UserId { get; set; }
        public DateTime FinishedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime EndsAt { get; set; }
    }
}
