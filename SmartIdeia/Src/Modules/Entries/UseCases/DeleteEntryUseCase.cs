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
    public class DeleteEntryUseCase
    {
        private readonly DatabaseContext context;
        private readonly CreateLogUseCase createLogUseCase;

        public DeleteEntryUseCase(DatabaseContext context, CreateLogUseCase createLogUseCase)
        {
            this.context = context;
            this.createLogUseCase = createLogUseCase;
        }

        public async Task<Entry> Execute(long userId,long entryId)
        {

            //Verify if entry exists
            var entry = await context
                .Entries
                .Where(e => e.Id == entryId)
                .FirstOrDefaultAsync();

            if(entry == null)
            {
                throw new AppError("Entry not exists", HttpStatusCode.NotFound);
            }

            
            //Delete
            context.Entries.Remove(entry);
            await context.SaveChangesAsync();

            //create log
            var log = new LogDTO
            {
                IdeaId = entry.IdeaId,
                UserId = userId,
                Description = "Entry deleted",
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
