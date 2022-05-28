using ITEC.Backend.Application.Shared;
using ITEC.Backend.Domain.Models;
using ITEC.Backend.Persistence.Repositories.Abstractions;
using MediatR;

namespace ITEC.Backend.Application.Queries.GetOfficeByIdQuery
{
    public class GetOfficeByIdQueryHandler : IRequestHandler<GetOfficeByIdQuery, Office>
    {
        private readonly IOfficeRepository _officeRepository;

        public GetOfficeByIdQueryHandler(IOfficeRepository officeRepository)
        {
            _officeRepository = officeRepository;
        }

        public async Task<Office> Handle(GetOfficeByIdQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetOfficeByIdQueryValidator();
            var validatorResult = await validator.ValidateAsync(request);
            if (validatorResult.Errors.Count > 0)
            {
                throw new ValidationException(validatorResult);
            }

            var office = await _officeRepository.GetOfficeByIdWithDesks(request.OfficeId);

            return office;
        }
    }
}
