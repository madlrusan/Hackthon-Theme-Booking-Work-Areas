using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITEC.Backend.Application.Commands.CreateReservationCmd
{
    public class CreateDeskReservationCommand : IRequest<CreateDeskReservationCommandResult>
    {
        public int DeskId { get; set; }
        public int NumberOfDays { get; set; }
    }
}
