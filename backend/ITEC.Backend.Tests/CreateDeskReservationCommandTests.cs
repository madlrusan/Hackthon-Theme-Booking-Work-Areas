using ITEC.Backend.Application.Commands.CreateReservationCmd;
using ITEC.Backend.Application.Shared;
using ITEC.Backend.Domain.Models;
using ITEC.Backend.Persistence.Repositories.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ITEC.Backend.Tests
{
    public class CreateDeskReservationCommandTests
    {
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private Mock<IDeskReservationRepository> _deskReservationRepositoryMock;
        private Mock<IRepository<Desk>> _deskRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        public CreateDeskReservationCommandTests()
        {
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _deskReservationRepositoryMock = new Mock<IDeskReservationRepository>();
            _deskRepositoryMock = new Mock<IRepository<Desk>>();
            _mediatorMock = new Mock<IMediator>();
        }

        [Fact]
        public async Task ReservationCreationFails_WhenDeskIdIsMissing()
        {
            var cmd = new CreateDeskReservationCommand() { NumberOfDays = 5 };

            var handler = GetHandler();

            var exception = await Assert.ThrowsAsync<ValidationException>(async () => await handler.Handle(cmd, CancellationToken.None));

            Assert.Equal("DeskId cannot be null or 0!", exception.ValidationErrors[0]);
        }

        [Fact]
        public async Task ReservationCreationFails_WhenNumberOfDaysIsMissing()
        {
            var cmd = new CreateDeskReservationCommand() { DeskId = 5 };

            var handler = GetHandler();

            var exception = await Assert.ThrowsAsync<ValidationException>(async () => await handler.Handle(cmd, CancellationToken.None));

            Assert.Equal("NumberOfDays cannot be null or smaller than 1!", exception.ValidationErrors[0]);
        }

        [Fact]
        public async Task ReservationCreationFails_WhenNumberOfDaysIsNegative()
        {
            var cmd = new CreateDeskReservationCommand() { DeskId = 5, NumberOfDays = -1 };

            var handler = GetHandler();

            var exception = await Assert.ThrowsAsync<ValidationException>(async () => await handler.Handle(cmd, CancellationToken.None));

            Assert.Equal("NumberOfDays cannot be null or smaller than 1!", exception.ValidationErrors[0]);
        }

        [Fact]
        public async Task ReservationCreationFails_WhenDeskIdIsInvalid()
        {
            _deskRepositoryMock.Setup(p => p.GetById(It.IsAny<int>()));

            var cmd = new CreateDeskReservationCommand() { DeskId = 5, NumberOfDays = 1 };

            var handler = GetHandler();

            var exception = await Assert.ThrowsAsync<ValidationException>(async () => await handler.Handle(cmd, CancellationToken.None));

            Assert.Equal("Invalid desk id!", exception.ValidationErrors[0]);
            _deskRepositoryMock.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task ReservationCreationFails_WhenDeskIsForHoteling()
        {
            _deskRepositoryMock.Setup(p => p.GetById(It.IsAny<int>())).ReturnsAsync(new Desk() { IsHotelingDesk = true});

            var cmd = new CreateDeskReservationCommand() { DeskId = 5, NumberOfDays = 1 };

            var handler = GetHandler();

            var exception = await Assert.ThrowsAsync<ValidationException>(async () => await handler.Handle(cmd, CancellationToken.None));

            Assert.Equal("The provided desk is not openned for reservations!", exception.ValidationErrors[0]);
            _deskRepositoryMock.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task ReservationCreationFails_WhenDeskAlreadyReserved()
        {
            var currentDate = DateTime.Now.AddDays(1);
            var reservations = new List<DeskReservation>() { new DeskReservation() { ReservationDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day) } };
            _deskRepositoryMock.Setup(p => p.GetById(It.IsAny<int>())).ReturnsAsync(new Desk() { IsHotelingDesk = false });
            _deskReservationRepositoryMock.Setup(p => p.GetFutureReservationsForDesk(It.IsAny<int>()))
                .ReturnsAsync(reservations);

            var cmd = new CreateDeskReservationCommand() { DeskId = 5, NumberOfDays = 1 };

            var handler = GetHandler();

            var exception = await Assert.ThrowsAsync<ValidationException>(async () => await handler.Handle(cmd, CancellationToken.None));

            Assert.Equal("Desk already reserved!", exception.ValidationErrors[0]);
            _deskRepositoryMock.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
            _deskReservationRepositoryMock.Verify(x => x.GetFutureReservationsForDesk(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task ReservationCreationWorks()
        {
            var currentDate = DateTime.Now.AddDays(1);
            var reservations = new List<DeskReservation>() {};
            var claims = new List<Claim>() { new Claim(ClaimTypes.NameIdentifier, "random-id") };
            _deskRepositoryMock.Setup(p => p.GetById(It.IsAny<int>())).ReturnsAsync(new Desk() { IsHotelingDesk = false });
            _deskReservationRepositoryMock.Setup(p => p.GetFutureReservationsForDesk(It.IsAny<int>()))
                .ReturnsAsync(reservations);
            _mediatorMock.Setup(p => p.Publish(It.IsAny<INotification>(), It.IsAny<CancellationToken>()));
            _deskReservationRepositoryMock.Setup(p => p.AddRangeAsync(It.IsAny<List<DeskReservation>>()));
            _deskReservationRepositoryMock.Setup(p => p.AddRangeAsync(It.IsAny<List<DeskReservation>>()));
            _httpContextAccessorMock.Setup(p => p.HttpContext.User).Returns(new ClaimsPrincipal(new ClaimsIdentity(claims)));

            var cmd = new CreateDeskReservationCommand() { DeskId = 5, NumberOfDays = 1 };

            var handler = GetHandler();

            var result = await handler.Handle(cmd, CancellationToken.None);

            Assert.True(result.Success);
            _deskRepositoryMock.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
            _deskReservationRepositoryMock.Verify(x => x.GetFutureReservationsForDesk(It.IsAny<int>()), Times.Once);
            _mediatorMock.Verify(x => x.Publish(It.IsAny<INotification>(), It.IsAny<CancellationToken>()), Times.Once);
            _deskReservationRepositoryMock.Verify(x => x.AddRangeAsync(It.IsAny<List<DeskReservation>>()), Times.Once);
        }

        private CreateDeskReservationCommandHandler GetHandler()
        {
            return new CreateDeskReservationCommandHandler(
                _httpContextAccessorMock.Object,
                _deskReservationRepositoryMock.Object,
                _deskRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
