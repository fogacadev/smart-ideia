using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartIdeia.Modules.Accounts.Entities
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LoggedIn { get; set; }
        public string PasswordToken { get; set; }
        public DateTime TokenCreatedAt { get; set; }
        public int LoginAttempts { get; set; }
        public bool BlockedByLoginAttempt { get; set; }
    }
}
