using SmartIdeia.Modules.Ideas.Entities;
using SmartIdeia.Src.Modules.Accounts.Entities;

namespace SmartIdeia.Src.Modules.Authors.Entities
{
    public class Author
    {
        public long Id { get; set; }
        public decimal PercentageOfParticipation { get; set; }
        public User User { get; set; }
        public long UserId { get; set; }
        public Idea Idea { get; set; }
        public long IdeaId { get; set; }
    }
}
