using Microsoft.EntityFrameworkCore;
using SmartIdeia.Database;
using SmartIdeia.Src.Errors;
using SmartIdeia.Src.Modules.Entries.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SmartIdeia.Src.Modules.Entries.UseCases
{
    public class ListEntryUseCase
    {
        private readonly DatabaseContext context;
        public ListEntryUseCase(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<List<Entry>> Execute(long ideaId)
        {
            var ideaExists = await context
                .Ideas
                .Where(i => i.Id == ideaId)
                .AnyAsync();

            if (!ideaExists)
            {
                throw new AppError("Idea not exists", HttpStatusCode.NotFound);
            }

            var entries = await context
                .Entries
                .Where(e => e.IdeaId == ideaId)
                .OrderBy(e => e.Date)
                .ToListAsync();

            return entries;
        }
    }
}
