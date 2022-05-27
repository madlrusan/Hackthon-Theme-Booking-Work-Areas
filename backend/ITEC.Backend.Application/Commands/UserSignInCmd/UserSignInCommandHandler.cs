using ITEC.Backend.Application.Options;
using ITEC.Backend.Application.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ITEC.Backend.Application.Commands.UserSignInCmd
{
    public class UserSignInCommandHandler : IRequestHandler<UserSignInCommand, UserSignInCommandResult>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtOptions _jwtOptions;

        public UserSignInCommandHandler(UserManager<IdentityUser> userManager, IOptions<JwtOptions> jwtOptions)
        {
            _userManager = userManager;
            _jwtOptions = jwtOptions.Value;
        }
        public async Task<UserSignInCommandResult> Handle(UserSignInCommand request, CancellationToken cancellationToken)
        {
            var validator = new UserSignInCommandValidator();
            var validatorResult = await validator.ValidateAsync(request);
            if (validatorResult.Errors.Count > 0)
            {
                throw new ValidationException(validatorResult);
            }
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
                throw new ValidationException("User does not exist or wrong password!");

            var isValidPassword = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!isValidPassword)
                throw new ValidationException("User does not exist or wrong password!");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var result = new UserSignInCommandResult();
            result.Token = tokenHandler.WriteToken(token);
            return result;
        }
    }
}
