using Microsoft.EntityFrameworkCore;
using SmartIdeia.Database;
using SmartIdeia.Src.Errors;
using SmartIdeia.Src.Modules.Activities.Entities;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SmartIdeia.Src.Modules.Activities.UseCases
{
    public class FindActivityUseCase
    {

        private readonly DatabaseContext context;

        public FindActivityUseCase(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<Activity> Execute(long id)
        {
            var activity = await context
                .Activities
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();

            if(activity == null)
            {
                throw new AppError("Activity not exists", HttpStatusCode.NotFound);
            }

            return activity;
        }
    }
}
