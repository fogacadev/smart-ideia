using Microsoft.EntityFrameworkCore;
using SmartIdeia.Database;
using SmartIdeia.Src.Modules.Themes.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartIdeia.Src.Modules.Themes.UseCases
{
    public class ListThemesUseCase
    {
        private readonly DatabaseContext context;
        public ListThemesUseCase(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<List<Theme>> List(long ideaId, string search = "")
        {
            List<Theme> themes;

            if (string.IsNullOrEmpty(search))
            {
                themes = await context
                    .Themes
                    .ToListAsync();
            }
            else
            {
                themes = await context
                    .Themes
                    .Where(t => t.Title.Contains(search))
                    .ToListAsync();
            }

            return themes;
        }
    }
}
