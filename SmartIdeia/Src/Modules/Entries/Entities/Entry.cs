using SmartIdeia.Modules.Ideas.Entities;
using SmartIdeia.Src.Modules.Accounts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartIdeia.Src.Modules.Entries.Entities
{
    public class Entry
    {
        public long Id { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
        public User User { get; set; }
        public long UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public long IdeaId { get; set; }
        public Idea Idea { get; set; }
    }
}
