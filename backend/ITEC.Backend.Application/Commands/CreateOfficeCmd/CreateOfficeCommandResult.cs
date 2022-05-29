using ITEC.Backend.Application.Shared;

namespace ITEC.Backend.Application.Commands.CreateOfficeCmd
{
    public class CreateOfficeCommandResult : BaseCommandResult
    {
        public int OfficeId { get; set; }
        public CreateOfficeCommandResult() : base()
        {

        }
    }
}