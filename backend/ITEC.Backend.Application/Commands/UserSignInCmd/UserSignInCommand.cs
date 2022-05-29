using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITEC.Backend.Application.Commands.UserSignInCmd
{
    public class UserSignInCommand : IRequest<UserSignInCommandResult>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
