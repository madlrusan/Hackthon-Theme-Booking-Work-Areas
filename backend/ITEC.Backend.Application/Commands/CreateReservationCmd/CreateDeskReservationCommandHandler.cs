﻿using ITEC.Backend.Application.Commands.SendEmailCmd;
using ITEC.Backend.Application.Shared;
using ITEC.Backend.Domain.Models;
using ITEC.Backend.Persistence.Repositories.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ITEC.Backend.Application.Commands.CreateReservationCmd
{
    public class CreateDeskReservationCommandHandler : IRequestHandler<CreateDeskReservationCommand, CreateDeskReservationCommandResult>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDeskReservationRepository _deskReservationRepository;
        private readonly IRepository<Desk> _deskRepository;
        private readonly IMediator _mediator;

        public CreateDeskReservationCommandHandler(IHttpContextAccessor httpContextAccessor, IDeskReservationRepository deskReservationRepository, IRepository<Desk> deskRepository, IMediator mediator)
        {
            _httpContextAccessor = httpContextAccessor;
            _deskReservationRepository = deskReservationRepository;
            _deskRepository = deskRepository;
            _mediator = mediator;
        }

        public async Task<CreateDeskReservationCommandResult> Handle(CreateDeskReservationCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateDeskReservationCommandValidator();
            var validatorResult = await validator.ValidateAsync(request);
            if (validatorResult.Errors.Count > 0)
            {
                throw new ValidationException(validatorResult);
            }

            var desk = await _deskRepository.GetById(request.DeskId);

            if (desk is null)
                throw new ValidationException("Invalid desk id!");

            if (desk.IsHotelingDesk)
                throw new ValidationException("The provided desk is not openned for reservations!");

            var deskReservations = await _deskReservationRepository.GetFutureReservationsForDesk(request.DeskId);

            var tomorrowDate = DateTime.Now.AddDays(1);

            if (deskReservations.Any(p => new DateTime(p.ReservationDate.Year, p.ReservationDate.Month, p.ReservationDate.Day) == new DateTime(tomorrowDate.Year, tomorrowDate.Month, tomorrowDate.Day)))
                throw new ValidationException("Desk already reserved!");

            List<DeskReservation> reservations = new List<DeskReservation>();
            for(int i = 0; i<request.NumberOfDays; i++)
            {
                var date = tomorrowDate.AddDays(i);
                var reservation = new DeskReservation()
                {
                    DeskId = request.DeskId,
                    CreatedAtTimeUtc = DateTime.UtcNow,
                    ReservationDate = new DateTime(date.Year, date.Month, date.Day),
                    CreatedByUserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)
                };
                reservations.Add(reservation);
            }
            

            await _deskReservationRepository.AddRangeAsync(reservations);

            var emailNotification = new SendEmailCommand()
            {
                Receiver = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email),
                Subject = "Desk reservation confirmation",
                Body = @$"<h3>Hello,</h3><p>We want to let you know you've booked the desk <b>{desk.Name}</b> for <b>{request.NumberOfDays}</b> days starting with <b>{tomorrowDate.ToString("dddd, dd MMMM yyyy")}</b><p>Cheers and see you there!</p>"
            };
            await _mediator.Publish(emailNotification);
            return new CreateDeskReservationCommandResult();

        }
    }
}
