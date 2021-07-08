using SmartIdeia.Src.Modules.Accounts.Entities;
using SmartIdeia.Src.Modules.Themes.Entities;
using System;

namespace SmartIdeia.Src.Modules.Responsibles.Entities
{
    public class Responsible
    {
        public long Id { get; set; }
        public User User { get; set; }
        public long UserId { get; set; }
        public Theme Theme { get; set; }
        public long ThemeId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
