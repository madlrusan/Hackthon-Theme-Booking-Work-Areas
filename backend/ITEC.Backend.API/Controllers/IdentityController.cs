using ITEC.Backend.Application.Commands;
using ITEC.Backend.Application.Commands.UserSignInCmd;
using ITEC.Backend.Application.Options;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ITEC.Backend.API.Controllers
{
    [ApiController]
    [Route("api/identity")]
    public class IdentityController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IdentityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterCommand cmd)
        {
            var result = await _mediator.Send(cmd);
            return Ok(result);
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(UserSignInCommand cmd)
        {
            var result = await _mediator.Send(cmd);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("checksession")]
        public IActionResult CheckSession()
        {
            return Ok();
        }
    }
}
