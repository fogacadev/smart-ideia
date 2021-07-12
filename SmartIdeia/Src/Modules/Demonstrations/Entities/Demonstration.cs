using SmartIdeia.Src.Modules.Accounts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartIdeia.Src.Modules.Demonstrations.Entities
{
    public class Demonstration
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public DateTime RealizedAt { get; set; }
    }
}
