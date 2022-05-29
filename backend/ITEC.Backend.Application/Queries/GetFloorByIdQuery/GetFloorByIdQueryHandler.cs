using ITEC.Backend.Application.Shared;
using ITEC.Backend.Domain.Models;
using ITEC.Backend.Persistence.Repositories.Abstractions;
using MediatR;

namespace ITEC.Backend.Application.Queries.GetFloorByIdQuery
{
    public class GetFloorByIdQueryHandler : IRequestHandler<GetFloorByIdQuery, Floor>
    {
        private readonly IFloorRepository _floorRepository;

        public GetFloorByIdQueryHandler(IFloorRepository floorRepository)
        {
            _floorRepository = floorRepository;
        }

        public async Task<Floor> Handle(GetFloorByIdQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetFloorByIdQueryValidator();
            var validatorResult = await validator.ValidateAsync(request);
            if (validatorResult.Errors.Count > 0)
            {
                throw new ValidationException(validatorResult);
            }
            var floor = await _floorRepository.GetFloorWithDesksById(request.FloorId);

            return floor;

        }
    }
}
