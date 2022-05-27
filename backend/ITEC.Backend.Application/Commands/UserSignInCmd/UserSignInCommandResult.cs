using ITEC.Backend.Application.Shared;

namespace ITEC.Backend.Application.Commands.UserSignInCmd
{
    public class UserSignInCommandResult : BaseCommandResult
    {
        public string Token { get; set; }
        public UserSignInCommandResult() : base()
        {

        }
    }
}