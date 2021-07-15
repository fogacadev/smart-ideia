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
    public class CreateRatingUseCase
    {
        private readonly DatabaseContext context;
        public CreateRatingUseCase(DatabaseContext context)
        {
            this.context = context;
        }


        public async Task<Rating> Execute(Rating rating)
        {
            //check if exists
            var ratingAlreadyExists = await context
                .Ratings
                .Where(r => r.Name == rating.Name && r.RatingGroupId == rating.RatingGroupId)
                .AnyAsync();

            if (ratingAlreadyExists)
            {
                throw new AppError("Rating already exists", HttpStatusCode.BadRequest);
            }

            context.Ratings.Add(rating);
            await context.SaveChangesAsync();

            return rating;
        }
    }
}
