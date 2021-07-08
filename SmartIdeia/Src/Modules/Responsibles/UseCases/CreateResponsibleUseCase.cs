using Microsoft.EntityFrameworkCore;
using SmartIdeia.Database;
using SmartIdeia.Src.Errors;
using SmartIdeia.Src.Modules.Responsibles.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartIdeia.Src.Modules.Responsibles.UseCases
{
    public class CreateResponsibleUseCase
    {
        private readonly DatabaseContext context;
        public CreateResponsibleUseCase(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<Responsible> Execute(Responsible responsible)
        {
            var responsibleAlreadyExists = await context
                .Responsibles
                .Where(r => r.ThemeId == responsible.ThemeId 
                            && r.UserId == responsible.UserId)
                .AnyAsync();

            if (responsibleAlreadyExists)
            {
                throw new AppError("Responsible already exists");
            }


            responsible.Id = 0;
            responsible.CreatedAt = DateTime.Now;

            context.Responsibles.Add(responsible);
            await context.SaveChangesAsync();

            return responsible;
        }
    }
}
