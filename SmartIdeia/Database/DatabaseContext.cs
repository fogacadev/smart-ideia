using Microsoft.EntityFrameworkCore;
using SmartIdeia.Modules.Ideas.Entities;
using SmartIdeia.Src.Modules.Accounts.Entities;
using SmartIdeia.Src.Modules.Campaigns.Entities;
using SmartIdeia.Src.Modules.Responsibles.Entities;
using SmartIdeia.Src.Modules.Themes.Entities;
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
        public DbSet<Theme> Themes { get; set; }
        public DbSet<Idea> Ideas { get; set; }
        public DbSet<Responsible> Responsibles { get; set; }
    }
}
