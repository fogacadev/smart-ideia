using Microsoft.EntityFrameworkCore;
using SmartIdeia.Database;
using SmartIdeia.Src.Errors;
using SmartIdeia.Src.Modules.Authors.Entities;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SmartIdeia.Src.Modules.Authors.UseCases
{
    public class DeleteAuthorUseCase
    {
        private readonly DatabaseContext context;

        public DeleteAuthorUseCase(DatabaseContext context)
        {
            this.context = context;
        }


        public async Task<Author> Execute(long authorId)
        {
            var author = await context.Authors.Where(a => a.Id == authorId).FirstOrDefaultAsync();

            if(author == null)
            {
                throw new AppError("Author not exists", HttpStatusCode.NotFound);
            }

            context.Authors.Remove(author);
            await context.SaveChangesAsync();

            return author;
        }
    }
}
