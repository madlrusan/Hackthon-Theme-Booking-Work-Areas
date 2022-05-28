using MediatR;

namespace ITEC.Backend.Application.Commands.CreateFloorsCmd
{
    public class CreateFloorsCommand : IRequest<CreateFloorsCommandResult>
    {
        public string Name { get; set; }
        public int OfficeId { get; set; }
    }
}
