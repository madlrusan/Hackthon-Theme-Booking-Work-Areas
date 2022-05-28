
using ITEC.Backend.Application.Commands.UserRegisterCmd;
using MediatR;

namespace ITEC.Backend.Application.Commands
{
    public class UserRegisterCommand : IRequest<UserRegisterCommandResult>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
