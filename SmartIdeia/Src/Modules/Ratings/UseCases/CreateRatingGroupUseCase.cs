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
    public class CreateRatingGroupUseCase
    {
        private readonly DatabaseContext context;
        public CreateRatingGroupUseCase(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<RatingGroup> Execute(RatingGroup ratingGroup)
        {
            //Check if group exists
            var ratingGroupAreadyExists = await context
                .RatingGroups
                .Where(r => r.Name == ratingGroup.Name)
                .AnyAsync();

            if (ratingGroupAreadyExists)
            {
                throw new AppError("Rating group alredy exists", HttpStatusCode.BadRequest);
            }

            //save
            context.RatingGroups.Add(ratingGroup);
            await context.SaveChangesAsync();

            return ratingGroup;
        }
    }
}
