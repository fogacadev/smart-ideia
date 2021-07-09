using Microsoft.EntityFrameworkCore;
using SmartIdeia.Database;
using SmartIdeia.Src.Modules.ActionPlans.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartIdeia.Src.Modules.ActionPlans.UseCases
{
    public class ListActionPlanUseCase
    {

        private readonly DatabaseContext context;

        public ListActionPlanUseCase(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<List<ActionPlan>> Execute(long ideaId, string search = "")
        {
            List<ActionPlan> actionPlans = new List<ActionPlan>();

            if (string.IsNullOrEmpty(search))
            {
                actionPlans = await context
                    .ActionPlans
                    .Where(a => a.IdeaId == ideaId &&
                          (a.How.Contains(search) 
                          || a.What.Contains(search) 
                          || a.Where.Contains(search) 
                          || a.Why.Contains(search)))
                    .ToListAsync();
            }
            else
            {
                actionPlans = await context
                    .ActionPlans
                    .Where(a => a.IdeaId == ideaId)
                    .ToListAsync();
            }

            return actionPlans;
        }
    }
}
