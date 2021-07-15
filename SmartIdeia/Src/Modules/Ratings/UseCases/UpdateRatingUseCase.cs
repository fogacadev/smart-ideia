using Microsoft.EntityFrameworkCore;
using SmartIdeia.Database;
using SmartIdeia.Src.Errors;
using SmartIdeia.Src.Modules.Ratings.Entities;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SmartIdeia.Src.Modules.Ratings.UseCases
{
    public class UpdateRatingUseCase
    {
        private readonly DatabaseContext context;
        public UpdateRatingUseCase(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task Execute(Rating rating)
        {
            var ratingExists = await context
                .Ratings
                .Where(r => r.Id == rating.Id)
                .AnyAsync();

            if (!ratingExists)
            {
                throw new AppError("Rating not exists", HttpStatusCode.NotFound);
            }

            context.Entry(rating).State = EntityState.Modified;

            await context.SaveChangesAsync();
        }

    }
}
