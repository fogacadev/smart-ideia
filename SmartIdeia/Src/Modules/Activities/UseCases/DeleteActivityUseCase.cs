using Microsoft.EntityFrameworkCore;
using SmartIdeia.Database;
using SmartIdeia.Src.Errors;
using SmartIdeia.Src.Modules.Activities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SmartIdeia.Src.Modules.Activities.UseCases
{
    public class DeleteActivityUseCase
    {
        private readonly DatabaseContext context;

        public DeleteActivityUseCase(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<Activity> Execute(long id)
        {
            var activity = await context.Activities.Where(a => a.Id == id).FirstOrDefaultAsync();

            if(activity == null)
            {
                throw new AppError("Activity not exists", HttpStatusCode.NotFound);
            }

            context.Activities.Remove(activity);

            await context.SaveChangesAsync();

            return activity;
        }
    }
}
