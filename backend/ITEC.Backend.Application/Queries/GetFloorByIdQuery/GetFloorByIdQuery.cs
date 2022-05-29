using ITEC.Backend.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITEC.Backend.Application.Queries.GetFloorByIdQuery
{
    public class GetFloorByIdQuery : IRequest<Floor>
    {
        public int FloorId { get; set; }
    }
}
