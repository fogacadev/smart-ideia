using Microsoft.EntityFrameworkCore;
using SmartIdeia.Database;
using SmartIdeia.Src.Modules.Accounts.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartIdeia.Src.Modules.Accounts.UseCases
{
    public class CreateUserUseCase
    {
        private readonly DatabaseContext context;
        public CreateUserUseCase(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<User> Execute(User user)
        {
            var userAlreadyExists = await context
                .Users
                .Where(c => c.Email == user.Email)
                .AnyAsync();

            if (userAlreadyExists)
            {
                throw new Exception("User alredy exists!");
            }

            user.CreatedAt = DateTime.Now;
            user.IsActive = true;
            user.LoginAttempts = 0;
            user.LoggedIn = DateTime.Now;
            user.PasswordToken = "";
            user.TokenCreatedAt = DateTime.Now;
            user.BlockedByLoginAttempt = false;

            context.Users.Add(user);

            await context.SaveChangesAsync();

            return user;
        }

    }
}
