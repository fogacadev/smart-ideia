using Microsoft.EntityFrameworkCore;
using SmartIdeia.Database;
using SmartIdeia.Src.Errors;
using SmartIdeia.Src.Modules.Ratings.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SmartIdeia.Src.Modules.Ratings.UseCases
{
    public class DeleteRatingUseCase
    {
        private readonly DatabaseContext context;
        public DeleteRatingUseCase(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<Rating> Execute(long id)
        {
            //checking if rating exists
            var rating = await context
                .Ratings
                .Where(r => r.Id == id)
                .FirstOrDefaultAsync();

            if(rating == null)
            {
                throw new AppError("Rating not exists", HttpStatusCode.NotFound);
            }

            //Checking if is in use

            var ratingIsInUse = await context.IdeaRatings.Where(i => i.RatingId == id).AnyAsync();

            if (ratingIsInUse)
            {
                throw new AppError("Rating cannot be deleted because its is in use by one or more ideas");
            }

            context.Ratings.Remove(rating);
            await context.SaveChangesAsync();

            return rating;

        }
    }
}
