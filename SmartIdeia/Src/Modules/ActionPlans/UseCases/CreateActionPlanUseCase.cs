using Microsoft.EntityFrameworkCore;
using SmartIdeia.Database;
using SmartIdeia.Src.Errors;
using SmartIdeia.Src.Modules.ActionPlans.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartIdeia.Src.Modules.ActionPlans.UseCases
{
    public class CreateActionPlanUseCase
    {
        private readonly DatabaseContext context;
        public CreateActionPlanUseCase(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<ActionPlan> Execute(ActionPlan actionPlan)
        {
            //Verify if ideia exists

            var ideaExists = await context
                .Ideas
                .Where(i => i.Id == actionPlan.IdeaId)
                .AnyAsync();

            if (!ideaExists)
            {
                throw new AppError("Idea not exists");
            }

            actionPlan.Id = 0;
            actionPlan.IsFinished = false;
            actionPlan.CreatedAt = DateTime.Now;

            context.ActionPlans.Add(actionPlan);
            await context.SaveChangesAsync();

            return actionPlan;
        }
    }
}
