using ITEC.Backend.Domain.Models;
using ITEC.Backend.Persistence.Repositories.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITEC.Backend.Application.Commands.GetOfficeQuery
{
    public class GetOfficeQueryHandler : IRequestHandler<GetOfficeQuery, List<Office>>
    {
        private readonly IOfficeRepository _officeRepository;

        public GetOfficeQueryHandler(IOfficeRepository officeRepository)
        {
            _officeRepository = officeRepository;
        }

        public async Task<List<Office>> Handle(GetOfficeQuery request, CancellationToken cancellationToken)
        {
            return await _officeRepository.GetOffices(request.includeFloors);
        }
    }
}
