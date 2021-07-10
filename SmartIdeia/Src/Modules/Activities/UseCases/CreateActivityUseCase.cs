using Microsoft.EntityFrameworkCore;
using SmartIdeia.Database;
using SmartIdeia.Src.Errors;
using SmartIdeia.Src.Modules.Accounts.Entities;
using SmartIdeia.Src.Modules.Activities.Dtos;
using SmartIdeia.Src.Modules.Activities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SmartIdeia.Src.Modules.Activities.UseCases
{
    public class CreateActivityUseCase
    {
        private readonly DatabaseContext context;

        public CreateActivityUseCase(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<Activity> Execute(long? userId, ActivityDTO activityDTO)
        {
            var ideaExists = await context
                .Ideas
                .Where(i => i.Id == activityDTO.IdeaId)
                .AnyAsync();

            if (!ideaExists)
            {
                throw new AppError("Idea not exists", HttpStatusCode.NotFound);
            }

            User user = null;
            if(userId != null)
            {
                user = await context
                    .Users
                    .Where(u => u.Id == userId)
                    .FirstOrDefaultAsync();

                if(user == null)
                {
                    throw new AppError("User not exists", HttpStatusCode.NotFound);
                }
            }

            var activity = new Activity
            {
                Id = 0,
                CreatedAt = DateTime.Now,
                Type = activityDTO.Type.ToString(),
                UserId = userId,
                Message = activityDTO.Type == ActivityType.Message ? $"{user.Name} added a comment: {activityDTO.Message}" : activityDTO.Message
            };

            context.Activities.Add(activity);
            await context.SaveChangesAsync();

            return activity;
        }
    }
}
