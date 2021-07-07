using SmartIdeia.Modules.Accounts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartIdeia.Modules.Campaigns.Entities
{
    public class Campaign
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string ImageName { get; set; }
        public DateTime CreatedAt { get; set; }
        public long CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; }
    }
}
