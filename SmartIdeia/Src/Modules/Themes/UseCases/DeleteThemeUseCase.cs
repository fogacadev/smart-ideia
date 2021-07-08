using Microsoft.EntityFrameworkCore;
using SmartIdeia.Database;
using SmartIdeia.Src.Errors;
using SmartIdeia.Src.Modules.Themes.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SmartIdeia.Src.Modules.Themes.UseCases
{
    public class DeleteThemeUseCase
    {
        private readonly DatabaseContext context;
        public DeleteThemeUseCase(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<Theme> Execute(long id)
        {
            //Validate if theme exists
            var theme = await context
                .Themes
                .Where(t => t.Id == id)
                .FirstOrDefaultAsync();

            if(theme == null)
            {
                throw new AppError("Theme not exists", HttpStatusCode.NotFound);
            }

            //Validate if theme is in use
            var themeIsInUse = await context
                .Ideas
                .Where(i => i.ThemeId == id)
                .AnyAsync();

            if (themeIsInUse)
            {
                throw new AppError("Theme is in use and cannot be deleted", HttpStatusCode.BadRequest);
            }


            //remove responsibles
            var responsibles = await context
                .Responsibles
                .Where(r => r.ThemeId == theme.Id)
                .ToListAsync();

            context.Responsibles.RemoveRange(responsibles);
            await context.SaveChangesAsync();

            //remove theme
            context.Themes.Remove(theme);
            await context.SaveChangesAsync();

            return theme;
        }
    }
}
