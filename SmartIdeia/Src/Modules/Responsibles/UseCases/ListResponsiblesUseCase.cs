using Microsoft.EntityFrameworkCore;
using SmartIdeia.Database;
using SmartIdeia.Src.Modules.Responsibles.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartIdeia.Src.Modules.Responsibles.UseCases
{
    public class ListResponsiblesUseCase
    {
        private readonly DatabaseContext context;
        public ListResponsiblesUseCase(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<List<Responsible>> Execute(long themeId, string search = "")
        {
            List<Responsible> responsibles;

            if (string.IsNullOrEmpty(search))
            {
                responsibles = await context.Responsibles.Where(r => r.ThemeId == themeId).ToListAsync();
            }
            else
            {
                responsibles = await context
                    .Responsibles
                    .Where(r => r.ThemeId == themeId && r.User.Name.Contains(search))
                    .ToListAsync();
            }

            return responsibles;
        }
    }
}
