using Microsoft.EntityFrameworkCore;
using SmartIdeia.Database;
using SmartIdeia.Src.Errors;
using SmartIdeia.Src.Modules.Themes.Entities;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SmartIdeia.Src.Modules.Themes.UseCases
{
    public class UpdateThemeUseCase
    {
        private readonly DatabaseContext context;
        public UpdateThemeUseCase(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task Execute(Theme theme)
        {

            var existingTheme = await context
                .Themes
                .Where(t => t.Id == theme.Id)
                .FirstOrDefaultAsync();

            if (existingTheme == null)
            {
                throw new AppError("Theme not exists", HttpStatusCode.NotFound);
            }

            context.Entry(existingTheme).State = EntityState.Detached;
            context.Entry(theme).State = EntityState.Modified;


            theme.UpdatedAt = DateTime.Now;
            theme.CreatedAt = existingTheme.CreatedAt;            

            await context.SaveChangesAsync();
        }
    }
}
