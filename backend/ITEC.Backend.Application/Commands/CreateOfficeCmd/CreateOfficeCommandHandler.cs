using ITEC.Backend.Application.Shared;
using ITEC.Backend.Domain.Models;
using ITEC.Backend.Persistence.Repositories.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ITEC.Backend.Application.Commands.CreateOfficeCmd
{
    public class CreateOfficeCommandHandler : IRequestHandler<CreateOfficeCommand, CreateOfficeCommandResult>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOfficeRepository _officeRepository;

        public CreateOfficeCommandHandler(IHttpContextAccessor httpContextAccessor, IOfficeRepository officeRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _officeRepository = officeRepository;
        }

        public async Task<CreateOfficeCommandResult> Handle(CreateOfficeCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateOfficeCommandValidator();
            var validatorResult = await validator.ValidateAsync(request);
            if (validatorResult.Errors.Count > 0)
            {
                throw new ValidationException(validatorResult);
            }

            var office = new Office() { 
                Name = request.Name, 
                CreatedAtTimeUtc = DateTime.UtcNow, 
                CreatedByUserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) 
            };

            var officeId = await _officeRepository.AddAsync(office);

            var result = new CreateOfficeCommandResult() { OfficeId = officeId };
            return result;

        }
    }
}
