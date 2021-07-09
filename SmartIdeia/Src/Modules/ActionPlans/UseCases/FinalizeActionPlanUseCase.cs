using Microsoft.EntityFrameworkCore;
using SmartIdeia.Database;
using SmartIdeia.Src.Errors;
using SmartIdeia.Src.Modules.ActionPlans.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SmartIdeia.Src.Modules.ActionPlans.UseCases
{
    public class FinalizeActionPlanUseCase
    {
        private readonly DatabaseContext context;

        public FinalizeActionPlanUseCase(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<ActionPlan> Execute(long id)
        {
            var actionPlan = await context.ActionPlans.Where(a => a.Id == id).FirstOrDefaultAsync();

            if(actionPlan == null)
            {
                throw new AppError("Action plan not exists", HttpStatusCode.NotFound);
            }

            actionPlan.FinishedAt = DateTime.Now;
            actionPlan.IsFinished = true;

            await context.SaveChangesAsync();

            return actionPlan;
        }
    }
}
