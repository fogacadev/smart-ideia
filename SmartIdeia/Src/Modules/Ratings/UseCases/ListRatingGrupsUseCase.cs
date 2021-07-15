using Microsoft.EntityFrameworkCore;
using SmartIdeia.Database;
using SmartIdeia.Src.Modules.Ratings.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartIdeia.Src.Modules.Ratings.UseCases
{
    public class ListRatingGrupsUseCase
    {
        private readonly DatabaseContext context;
        public ListRatingGrupsUseCase(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<List<RatingGroup>> Execute(string search)
        {
            List<RatingGroup> ratingGroups;
            if (!string.IsNullOrEmpty(search))
            {
                ratingGroups = await context
                    .RatingGroups
                    .Where(r => r.Name.Contains(search))
                    .OrderBy(r => r.Name)
                    .ToListAsync();
            }
            else
            {
                ratingGroups = await context
                    .RatingGroups
                    .OrderBy(r => r.Name)
                    .ToListAsync();
            }

            return ratingGroups;
        }
    }
}
