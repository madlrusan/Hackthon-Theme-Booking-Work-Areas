using MediatR;

namespace ITEC.Backend.Application.Commands.CreateFloorsCmd
{
    public class CreateFloorsCommand : IRequest<CreateFloorsCommandResult>
    {
        public List<CreateFloorCommand> Floors { get; set; }
        public int OfficeId { get; set; }
    }
}
