using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SmartIdeia.Database;
using SmartIdeia.Src.Errors;
using SmartIdeia.Src.Modules.Entries.Entities;
using SmartIdeia.Src.Modules.Logs.Dtos;
using SmartIdeia.Src.Modules.Logs.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SmartIdeia.Src.Modules.Entries.UseCases
{
    public class CreateEntryUseCase
    {
        private readonly DatabaseContext context;
        private readonly CreateLogUseCase createLogUseCase;
        public CreateEntryUseCase(DatabaseContext context, CreateLogUseCase createLogUseCase)
        {
            this.context = context;
            this.createLogUseCase = createLogUseCase;
        }

        public async Task<Entry> Execute(long userId, Entry entry)
        {
            //Verify if idea Exists
            var ideaExists = await context.Ideas.Where(a => a.Id == entry.Id).AnyAsync();

            if (!ideaExists)
            {
                throw new AppError("Idea not exists", HttpStatusCode.BadRequest);
            }

            //Verify if entry exists

            var entryAlreadyExists = await context
                .Entries
                .Where(e => e.IdeaId == entry.IdeaId && e.Date == entry.Date)
                .AnyAsync();

            if (entryAlreadyExists)
            {
                throw new AppError("Entry already exists");
            }

            entry.Id = 0;
            entry.CreatedAt = DateTime.Now;
            entry.UserId = userId;

            context.Entries.Add(entry);
            await context.SaveChangesAsync();


            //create log
            var log = new LogDTO
            {
                IdeaId = entry.IdeaId,
                UserId = userId,
                Description = "Entry created",
                Entity = typeof(Entry).ToString(),
                EntityId = entry.Id,
                JsonObject = JsonConvert.SerializeObject(entry),
                Type = Logs.Entities.LogType.Create
            };
            await createLogUseCase.Execute(log);


            return entry;
        }
    }
}
