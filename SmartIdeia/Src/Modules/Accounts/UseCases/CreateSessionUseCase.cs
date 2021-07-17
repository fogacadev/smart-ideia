using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SmartIdeia.Database;
using SmartIdeia.Src.Errors;
using SmartIdeia.Src.Modules.Accounts.Dtos;
using SmartIdeia.Src.Modules.Accounts.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SmartIdeia.Src.Modules.Accounts.UseCases
{
    public class CreateSessionUseCase
    {
        private readonly DatabaseContext context;
        private readonly CreateJwtTokenUseCase createJwtTokenUseCase;
        private readonly CreateRandomTokenUseCase createRandomTokenUseCase;

        public CreateSessionUseCase(DatabaseContext context, 
            CreateJwtTokenUseCase createJwtTokenUseCase,
            CreateRandomTokenUseCase createRandomTokenUseCase)
        {
            this.createRandomTokenUseCase = createRandomTokenUseCase;
            this.createJwtTokenUseCase = createJwtTokenUseCase;
            this.context = context;
        }

        public async Task<TokenReturn> Execute(CredentialDTO credential)
        {
            var user = await context.Users.Where(u => u.Email == credential.Email).FirstOrDefaultAsync();

            //Checking if user exists
            if(user == null)
            {
                throw new AppError("User or password incorrect", HttpStatusCode.NotFound);
            }

            //Checking if password match
            var match = BCrypt.Net.BCrypt.Verify(credential.Password, user.Password);
            if (!match)
            {
                throw new AppError("User or password incorrect", HttpStatusCode.NotFound);
            }

            user.LoggedIn = DateTime.Now;

            //Create Token
            var token = createJwtTokenUseCase.Execute(user);

            //remove refrash tokens
            var userTokens = await context
                .UserTokens
                .Where(t => t.UserId == user.Id)
                .ToListAsync();
            context.UserTokens.RemoveRange(userTokens);

            //Create refresh_token
            var userToken = new UserToken
            {
                CreatedAt = DateTime.Now,
                Expires_date = DateTime.Now.AddHours(1),
                RefreshToken = createRandomTokenUseCase.Execute(),
                UserId = user.Id
            };

            context.UserTokens.Add(userToken);
            await context.SaveChangesAsync();

            //Return Token
            var tokenReturn = new TokenReturn
            {
                User = user,
                Token = token,
                RefreshToken = userToken.RefreshToken
            };

            return tokenReturn;
        }
    }
}
