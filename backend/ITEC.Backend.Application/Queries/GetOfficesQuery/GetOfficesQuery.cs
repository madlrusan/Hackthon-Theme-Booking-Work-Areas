using ITEC.Backend.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITEC.Backend.Application.Queries.GetOfficeQuery
{
    public class GetOfficesQuery : IRequest<List<Office>>
    {
        public bool includeFloors { get; set; }
    }
}
