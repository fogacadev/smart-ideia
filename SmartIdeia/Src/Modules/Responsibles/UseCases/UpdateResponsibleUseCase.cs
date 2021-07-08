using Microsoft.EntityFrameworkCore;
using SmartIdeia.Database;
using SmartIdeia.Src.Errors;
using SmartIdeia.Src.Modules.Responsibles.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SmartIdeia.Src.Modules.Responsibles.UseCases
{
    public class UpdateResponsibleUseCase
    {
        private readonly DatabaseContext context;
        public UpdateResponsibleUseCase(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task Execute(Responsible responsible)
        {

            var existingResponsible = await context
                .Responsibles
                .Where(r => r.Id == responsible.Id)
                .FirstOrDefaultAsync();

            if(existingResponsible == null)
            {
                throw new AppError("Responsible not exists", HttpStatusCode.NotFound);
            }

            context.Entry(existingResponsible).State = EntityState.Detached;
            context.Entry(responsible).State = EntityState.Modified;

            responsible.CreatedAt = existingResponsible.CreatedAt;

            await context.SaveChangesAsync();
        }
    }
}
