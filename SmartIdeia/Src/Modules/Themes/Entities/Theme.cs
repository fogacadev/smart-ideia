using SmartIdeia.Src.Modules.Responsibles.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartIdeia.Src.Modules.Themes.Entities
{
    public class Theme
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<Responsible> Responsibles { get; set; }
        public bool IsActive { get; set; }
    }
}
