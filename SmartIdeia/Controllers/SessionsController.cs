using Microsoft.AspNetCore.Mvc;
using SmartIdeia.Src.Modules.Accounts.Dtos;
using SmartIdeia.Src.Modules.Accounts.Entities;
using SmartIdeia.Src.Modules.Accounts.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartIdeia.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessionsController : ControllerBase
    {
        private readonly CreateSessionUseCase createSessionUseCase;
        private readonly RefreshTokenUseCase refreshTokenUseCase;

        public SessionsController(CreateSessionUseCase createSessionUseCase,
            RefreshTokenUseCase refreshTokenUseCase)
        {
            this.createSessionUseCase = createSessionUseCase;
            this.refreshTokenUseCase = refreshTokenUseCase;
        }

        [HttpPost]
        public async Task<ActionResult<TokenReturn>> Login(CredentialDTO credential)
        {
            var tokenReturn = await createSessionUseCase.Execute(credential);

            return Ok(tokenReturn);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult> RefreshToken(string refreshToken)
        {
            var tokenReturn = await refreshTokenUseCase.Execute(refreshToken);

            return Ok(tokenReturn);
        }
    }
}
