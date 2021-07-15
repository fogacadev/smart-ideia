using Microsoft.EntityFrameworkCore;
using SmartIdeia.Database;
using SmartIdeia.Src.Modules.Ratings.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartIdeia.Src.Modules.Ratings.UseCases
{
    public class ListRatingsUseCase
    {
        private readonly DatabaseContext context;
        public ListRatingsUseCase(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<List<Rating>> Execute(string search)
        {
            List<Rating> ratings;

            if (!string.IsNullOrEmpty(search))
            {
                ratings = await context
                    .Ratings
                    .Where(r => r.Name.Contains(search))
                    .ToListAsync();
            }
            else
            {
                ratings = await context
                    .Ratings
                    .OrderBy(r => r.Name)
                    .ToListAsync();
            }

            return ratings;
        }
    }
}
