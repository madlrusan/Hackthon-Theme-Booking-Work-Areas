using ITEC.Backend.Domain.Models;
using ITEC.Backend.Persistence.Repositories.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITEC.Backend.Application.Queries.GetOfficeQuery
{
    public class GetOfficesQueryHandler : IRequestHandler<GetOfficesQuery, List<Office>>
    {
        private readonly IOfficeRepository _officeRepository;

        public GetOfficesQueryHandler(IOfficeRepository officeRepository)
        {
            _officeRepository = officeRepository;
        }

        public async Task<List<Office>> Handle(GetOfficesQuery request, CancellationToken cancellationToken)
        {
            return await _officeRepository.GetOffices(request.includeFloors);
        }
    }
}
