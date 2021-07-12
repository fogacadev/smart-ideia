using Microsoft.EntityFrameworkCore;
using SmartIdeia.Database;
using SmartIdeia.Src.Errors;
using SmartIdeia.Src.Modules.Activities.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SmartIdeia.Src.Modules.Activities.UseCases
{
    public class ListActivityUseCase
    {
        private readonly DatabaseContext context;
        public ListActivityUseCase(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<List<Activity>> Execute(long ideaId)
        {
            //Verify if idea exists 
            var ideaExists = await context
                .Ideas
                .Where(a => a.Id == ideaId)
                .AnyAsync();

            if (!ideaExists)
            {
                throw new AppError("Idea not exists", HttpStatusCode.NotFound);
            }


            //Get ativities
            var acitivities = await context
                .Activities
                .Where(a => a.IdeaId == ideaId)
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();

            return acitivities;
        }
    }
}
