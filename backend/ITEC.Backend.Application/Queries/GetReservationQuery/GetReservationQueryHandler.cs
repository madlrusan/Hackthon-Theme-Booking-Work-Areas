using ITEC.Backend.Persistence.Repositories.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ITEC.Backend.Application.Queries.GetReservationQuery
{
    public class GetReservationQueryHandler : IRequestHandler<GetReservationQuery, GetReservationQueryResult>
    {
        private readonly IDeskReservationRepository _deskReservationRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetReservationQueryHandler(IDeskReservationRepository deskReservationRepository, IHttpContextAccessor httpContextAccessor)
        {
            _deskReservationRepository = deskReservationRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<GetReservationQueryResult> Handle(GetReservationQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var reservations = await _deskReservationRepository.GetFutureReservationsForUser(userId);

            var result = new GetReservationQueryResult() { 
                DeskId = reservations.FirstOrDefault()?.DeskId, 
                HasReservation = reservations.Any(), 
                NumberOfDays =  reservations.Count,
                StartingDate = reservations.FirstOrDefault()?.ReservationDate
            };

            return result;
        }
    }
}
