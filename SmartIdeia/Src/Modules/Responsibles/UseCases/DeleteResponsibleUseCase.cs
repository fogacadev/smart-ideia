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
    public class DeleteResponsibleUseCase
    {
        private readonly DatabaseContext context;
        public DeleteResponsibleUseCase(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<Responsible> Execute(long id)
        {
            var responsible = await context
                .Responsibles
                .Where(r => r.Id == id)
                .FirstOrDefaultAsync();

            if(responsible == null)
            {
                throw new AppError("Responsible not exists", HttpStatusCode.NotFound);
            }

            context.Entry(responsible).State = EntityState.Deleted;
            await context.SaveChangesAsync();

            return responsible;
        }

    }
}
