using Microsoft.EntityFrameworkCore;
using SmartIdeia.Modules.Accounts.Entities;
using SmartIdeia.Modules.Campaigns.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartIdeia.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options): base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
    }
}
