using Microsoft.EntityFrameworkCore;
using SmartIdeia.Database;
using SmartIdeia.Src.Errors;
using SmartIdeia.Src.Modules.Ratings.Entities;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SmartIdeia.Src.Modules.Ratings.UseCases
{
    public class DeleteRatingGroupUseCase
    {
        private readonly DatabaseContext context;
        public DeleteRatingGroupUseCase(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<RatingGroup> Execute(long id)
        {
            var ratingGroup = await context.RatingGroups.Where(r => r.Id == id).FirstOrDefaultAsync();

            if(ratingGroup == null)
            {
                throw new AppError("Rating group not exists", HttpStatusCode.NotFound);
            }

            //need check if is in use by an idea
            var ratingIsInUse = await context
                .IdeaRatings
                .Include(i => i.Rating)
                .Where(i => i.Rating.RatingGroupId == ratingGroup.Id)
                .AnyAsync();

            if (ratingIsInUse)
            {
                throw new AppError("The review cannot be deleted because it is in use by one or more ideas", HttpStatusCode.BadRequest);
            }

            context.RatingGroups.Remove(ratingGroup);
            await context.SaveChangesAsync();

            return ratingGroup;
        }

    }
}
