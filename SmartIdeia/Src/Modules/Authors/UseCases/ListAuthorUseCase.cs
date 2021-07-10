using Microsoft.EntityFrameworkCore;
using SmartIdeia.Database;
using SmartIdeia.Src.Modules.Authors.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartIdeia.Src.Modules.Authors.UseCases
{
    public class ListAuthorUseCase
    {
        private readonly DatabaseContext context;

        public ListAuthorUseCase(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<List<Author>> Execute(long ideaId)
        {

            var authors = await context
                .Authors
                .Where(a => a.IdeaId == ideaId)
                .OrderBy(a => a.User.Name)
                .ToListAsync();

            return authors;
        }
    }
}
