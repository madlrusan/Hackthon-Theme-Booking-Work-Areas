using ITEC.Backend.Application.Commands;
using ITEC.Backend.Application.Options;
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
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtOptions _jwtOptions;

        public IdentityController(UserManager<IdentityUser> userManager, IOptions<JwtOptions> jwtOptions)
        {
            _userManager = userManager;
            _jwtOptions = jwtOptions.Value;
        }
        
        [HttpPost]
        public async Task<IActionResult> Register(RegisterCmd cmd)
        {
            var user = await _userManager.FindByEmailAsync(cmd.Email);

            if (user != null)
                return BadRequest("User already exist!");

            var newUser = new IdentityUser
            {
                Email = cmd.Email,
                UserName = cmd.Email,
            };

            var result = await _userManager.CreateAsync(newUser, cmd.Password);

            if (result.Succeeded)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(JwtRegisteredClaimNames.Sub, newUser.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, newUser.Email)
                    }),
                    Expires = DateTime.UtcNow.AddDays(60),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return Ok(tokenHandler.WriteToken(token));
            }

            return BadRequest(result.Errors);
        }
    }
}
