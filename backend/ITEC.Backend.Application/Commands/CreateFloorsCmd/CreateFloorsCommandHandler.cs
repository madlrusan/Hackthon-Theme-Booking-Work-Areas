using ITEC.Backend.Application.Shared;
using ITEC.Backend.Domain.Models;
using ITEC.Backend.Persistence.Repositories.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ITEC.Backend.Application.Commands.CreateFloorsCmd
{
    public class CreateFloorsCommandHandler : IRequestHandler<CreateFloorsCommand, CreateFloorsCommandResult>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<Floor> _floorRepository;
        private readonly IOfficeRepository _officeRepository;

        public CreateFloorsCommandHandler(IHttpContextAccessor httpContextAccessor, IRepository<Floor> floorRepository, IOfficeRepository officeRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _floorRepository = floorRepository;
            _officeRepository = officeRepository;
        }
        public async Task<CreateFloorsCommandResult> Handle(CreateFloorsCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateFloorsCommandValidator();
            var validatorResult = await validator.ValidateAsync(request);
            if (validatorResult.Errors.Count > 0)
            {
                throw new ValidationException(validatorResult);
            }

            var office = await _officeRepository.GetById(request.OfficeId);

            if (office is null)
                throw new ValidationException("OfficeId is invalid!");
            var floor = new Floor() { 
                OfficeId = request.OfficeId, 
                CreatedAtTimeUtc = DateTime.UtcNow, 
                Name = request.Name, 
                CreatedByUserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) 
            };
            await _floorRepository.AddAsync(floor);
            return new CreateFloorsCommandResult();
        }
    }
}
