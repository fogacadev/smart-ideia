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
    public class UpdateAuthorUseCase
    {
        private readonly DatabaseContext context;

        public UpdateAuthorUseCase(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task Execute(Author author)
        {
            //verify if author exists

            var createdAuthor = await context.Authors.Where(a => a.Id == author.Id).FirstOrDefaultAsync();

            if(createdAuthor == null)
            {
                throw new AppError("Author not exists");
            }


            //Verify sum of percetage
            var sumOfPercentage = await context
                .Authors
                .Where(a => a.IdeaId == author.IdeaId && a.Id != author.Id)
                .SumAsync(a => a.PercentageOfParticipation);

            sumOfPercentage += author.PercentageOfParticipation;

            if(sumOfPercentage > 100)
            {
                throw new AppError("The sum of the participation percentage must not be greater than 100%");
            }


            createdAuthor.PercentageOfParticipation = author.PercentageOfParticipation;
            createdAuthor.UserId = author.UserId;

            context.Entry(createdAuthor).State = EntityState.Detached;
            context.Entry(author).State = EntityState.Modified;

            await context.SaveChangesAsync();
        }
    }
}
