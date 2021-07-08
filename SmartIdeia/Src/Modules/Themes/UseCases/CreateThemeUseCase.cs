using Microsoft.EntityFrameworkCore;
using SmartIdeia.Database;
using SmartIdeia.Src.Errors;
using SmartIdeia.Src.Modules.Themes.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartIdeia.Src.Modules.Themes.UseCases
{
    public class CreateThemeUseCase
    {
        private readonly DatabaseContext context;
        public CreateThemeUseCase(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<Theme> Execute(Theme theme)
        {
            var themeExists = await context
                .Themes
                .AnyAsync(t => t.Title == theme.Title);

            if (themeExists)
            {
                throw new AppError("Theme already exists");
            }

            theme.CreatedAt = DateTime.Now;
            theme.Id = 0;

            context.Themes.Add(theme);
            await context.SaveChangesAsync();

            return theme;
        }
    }
}
