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
    public class UpdateActionPlanUseCase
    {

        private readonly DatabaseContext context;

        public UpdateActionPlanUseCase(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task Execute(ActionPlan actionPlan)
        {
            //Verify if action plan exists

            var createdActionPlan = await context
                .ActionPlans
                .Where(a => a.Id == actionPlan.Id)
                .FirstOrDefaultAsync();

            if (createdActionPlan == null)
            {
                throw new AppError("Action plan not exists", HttpStatusCode.NotFound);
            }

            actionPlan.CreatedAt = createdActionPlan.CreatedAt;
            actionPlan.IsFinished = createdActionPlan.IsFinished;
            actionPlan.FinishedAt = createdActionPlan.FinishedAt;
            actionPlan.IdeaId = createdActionPlan.IdeaId;

            context.Entry(createdActionPlan).State = EntityState.Detached;
            context.Entry(createdActionPlan).State = EntityState.Modified;


            await context.SaveChangesAsync();
        }
    }
}
