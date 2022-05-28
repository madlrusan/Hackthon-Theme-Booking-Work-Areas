using ITEC.Backend.Application.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITEC.Backend.Application.Commands.UserRegisterCmd
{
    public class UserRegisterCommandResult : BaseCommandResult
    {
        public string Token { get; set; }
        public UserRegisterCommandResult() : base()
        {

        }
    }
}
