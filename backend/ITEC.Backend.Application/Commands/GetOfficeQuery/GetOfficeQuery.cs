using ITEC.Backend.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITEC.Backend.Application.Commands.GetOfficeQuery
{
    public class GetOfficeQuery : IRequest<List<Office>>
    {
        public bool includeFloors { get; set; }
    }
}
