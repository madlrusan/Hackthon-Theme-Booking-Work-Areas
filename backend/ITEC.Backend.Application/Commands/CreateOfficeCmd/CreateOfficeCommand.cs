using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITEC.Backend.Application.Commands.CreateOfficeCmd
{
    public class CreateOfficeCommand : IRequest<CreateOfficeCommandResult>
    {
        public string Name { get; set; }
    }
}
