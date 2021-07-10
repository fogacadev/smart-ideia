using SmartIdeia.Database;
using SmartIdeia.Src.Modules.Logs.Dtos;
using SmartIdeia.Src.Modules.Logs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartIdeia.Src.Modules.Logs.UseCases
{
    public class CreateLogUseCase
    {
        private readonly DatabaseContext context;
        public CreateLogUseCase(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<Log> Execute(LogDTO logDTO)
        {
            var log = new Log
            {
                Id = 0,
                CreatedAt = DateTime.Now,
                Entity = logDTO.Entity,
                EntityId = logDTO.EntityId,
                JsonObject = logDTO.JsonObject,
                UserId = logDTO.UserId,
                Type = logDTO.Type.ToString(),
                Description = logDTO.Description,
                IdeaId = logDTO.IdeaId
            };

            context.Logs.Add(log);
            await context.SaveChangesAsync();

            return log;
        }
    }
}
