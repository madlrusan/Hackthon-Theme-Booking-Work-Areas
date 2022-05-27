using ITEC.Backend.Application.Options;
using ITEC.Backend.Application.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ITEC.Backend.Application.Commands.UserRegisterCmd
{
    public class UserRegisterCommandHandler : IRequestHandler<UserRegisterCommand, UserRegisterCommandResult>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtOptions _jwtOptions;

        public UserRegisterCommandHandler(UserManager<IdentityUser> userManager, IOptions<JwtOptions> jwtOptions)
        {
            _userManager = userManager;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task<UserRegisterCommandResult> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
        {
            var validator = new UserRegisterCommandValidator();
            var validatorResult = await validator.ValidateAsync(request);
            if (validatorResult.Errors.Count > 0)
            {
                throw new ValidationException(validatorResult);
            }

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user != null)
                throw new ValidationException("User already exist!");

            var newUser = new IdentityUser
            {
                Email = request.Email,
                UserName = request.Email,
            };

            var userResult = await _userManager.CreateAsync(newUser, request.Password);

            if (!userResult.Succeeded)
                throw new ValidationException("Error creating the user");

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

            var result = new UserRegisterCommandResult();
            result.Token = tokenHandler.WriteToken(token);
            return result;
        }

    }
}
