using Microsoft.EntityFrameworkCore;
using SmartIdeia.Database;
using SmartIdeia.Src.Errors;
using SmartIdeia.Src.Modules.Authors.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartIdeia.Src.Modules.Authors.UseCases
{
    public class CreateAuthorUseCase
    {
        private readonly DatabaseContext context;

        public CreateAuthorUseCase(DatabaseContext context)
        {
            this.context = context;
        }


        public async Task<Author> Execute(Author author)
        {

            //Verify if idea exists
            var ideaExists = await context
                .Ideas
                .Where(a => a.Id == author.IdeaId)
                .AnyAsync();

            if (!ideaExists)
            {
                throw new AppError("Idea not exists");
            }


            //Verify if User exists
            var userExists = await context
                .Users
                .Where(u => u.Id == author.UserId)
                .AnyAsync();

            if (!userExists)
            {
                throw new AppError("User not exists");
            }

            //Verify if Author exists
            var authorAlreadyExists = await context
                .Authors
                .Where(a => a.UserId == author.UserId && a.IdeaId == author.IdeaId)
                .AnyAsync();

            if (authorAlreadyExists)
            {
                throw new AppError("Authro alredy exists");
            }


            //Verify if percentage is > 100%
            var sumOfPercentage = await context
                .Authors
                .Where(a => a.IdeaId == author.IdeaId)
                .SumAsync(a => a.PercentageOfParticipation);

            sumOfPercentage += author.PercentageOfParticipation;

            if(sumOfPercentage > 100)
            {
                throw new AppError("The sum of the participation percentage must not be greater than 100%");
            }

            //Save author
            author.Id = 0;
            context.Authors.Add(author);
            await context.SaveChangesAsync();

            return author;
        }
    }
}
