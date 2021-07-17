using Microsoft.EntityFrameworkCore;
using SmartIdeia.Database;
using SmartIdeia.Src.Errors;
using SmartIdeia.Src.Modules.Accounts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SmartIdeia.Src.Modules.Accounts.UseCases
{
    public class RefreshTokenUseCase
    {
        private readonly DatabaseContext context;
        private readonly CreateRandomTokenUseCase createRandomTokenUseCase;
        private readonly CreateJwtTokenUseCase createJwtTokenUseCase;
        public RefreshTokenUseCase(DatabaseContext context,
            CreateRandomTokenUseCase createRandomTokenUseCase,
            CreateJwtTokenUseCase createJwtTokenUseCase)
        {
            this.context = context;
            this.createRandomTokenUseCase = createRandomTokenUseCase;
            this.createJwtTokenUseCase = createJwtTokenUseCase;
        }

        public async Task<TokenReturn> Execute(string refreshToken)
        {
            var userToken = await context
                .UserTokens
                .Where(u => u.RefreshToken == refreshToken)
                .FirstOrDefaultAsync();

            //Token exists
            if(userToken == null)
            {
                throw new AppError("Token not exists.", HttpStatusCode.NotFound);
            }

            //Token expired
            if(DateTime.Now > userToken.Expires_date)
            {
                throw new AppError("Token expired.", HttpStatusCode.BadRequest);
            }

            var user = await context
                .Users
                .Where(u => u.Id == userToken.UserId)
                .FirstOrDefaultAsync();

            //User exists
            if(user == null)
            {
                throw new AppError("User not found.", HttpStatusCode.NotFound);
            }

            //create new tokens
            var newToken = createJwtTokenUseCase.Execute(user);
            var newRefreshToken = createRandomTokenUseCase.Execute();

            var newUserToken = new UserToken
            {
                Id = 0,
                CreatedAt = DateTime.Now,
                Expires_date = DateTime.Now.AddDays(30),
                RefreshToken = newRefreshToken,
                UserId = user.Id
            };
            //Add new token and remove old token
            context.UserTokens.Add(newUserToken);
            context.UserTokens.Remove(userToken);

            //Save
            await context.SaveChangesAsync();


            //Return new token
            var tokenReturn = new TokenReturn
            {
                RefreshToken = newRefreshToken,
                Token = newToken,
                User = user
            };

            return tokenReturn;
        }
    }
}
