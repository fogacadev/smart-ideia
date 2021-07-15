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
    public class UpdateRatingGroupsUseCase
    {
        private readonly DatabaseContext context;
        public UpdateRatingGroupsUseCase(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task Execute(RatingGroup ratingGroup)
        {
            //Check if rating group exists

            var ratingGroupAlreadyExists = await context
                .RatingGroups
                .Where(r => r.Id == ratingGroup.Id)
                .AnyAsync();

            if (!ratingGroupAlreadyExists)
            {
                throw new AppError("Rating group not exists", HttpStatusCode.NotFound);
            }

            context.Entry(ratingGroup).State = EntityState.Modified;

            await context.SaveChangesAsync();
        }
    }
}
