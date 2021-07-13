using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartIdeia.Src.Modules.Accounts.Entities
{
    public class UserToken
    {
        public long Id { get; set; }
        public string RefreshToken { get; set; }
        public long UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime Expires_date { get; set; }
    }
}
